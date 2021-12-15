using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Impl.Types;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.RiderTutorials.Utils;
using JetBrains.Util;
using ReSharperAbp.Checker;

namespace ReSharperAbp.Modular
{
    public class ModuleFinder
    {
        private readonly AbpChecker _checker;

        public ModuleFinder(AbpChecker checker)
        {
            _checker = checker;
        }


        public IEnumerable<ModuleInfo> FindAllAbpModules()
        {
            var psiServices = AbpFrameworkInitializer.Solution.GetPsiServices();
            var symbolScope = psiServices.Symbols
                .GetSymbolScope(LibrarySymbolScope.REFERENCED, true);

            var abpTypeElement =
                symbolScope.GetTypeElementByCLRName(new ClrTypeName(_checker.BuiltinTypes.ModuleClass));
            if (abpTypeElement == null)
            {
                return null;
            }

            var references = psiServices.Finder
                .FindAllInheritors(new DeclaredTypeFromCLRName(new ClrTypeName(_checker.BuiltinTypes.ModuleClass),
                    NullableAnnotation.Unknown, abpTypeElement.Module));

            return references.Select(r => r.Resolve().DeclaredElement).OfType<IClass>()
                .Where(c => _checker.IsAbpModule(c)).Select(c => ModuleInfo.Create(c, _checker));
        }


        private static void SortAttributeDependenciesRecursively(
            ModuleInfo current,
            Stack<ModuleInfo> visiting,
            ICollection<ModuleInfo> visited)
        {
            if (visiting.Contains(current))
            {
                var circle = new List<ModuleInfo>();

                foreach (var module in visiting)
                {
                    circle.Push(module);
                    if (module.Equals(current))
                    {
                        break;
                    }
                }

                circle.Insert(0, current);
                circle.Reverse();


                throw new ModuleCyclicDependencyException(circle);
            }

            visiting.Push(current);


            var modules = current.GetDependsOnAttributeDependencies();

            if (modules != null)
            {
                foreach (var module in modules)
                {
                    SortAttributeDependenciesRecursively(module, visiting, visited);
                }
            }

            visited.Add(visiting.Pop());
        }


        [NotNull]
        public DependencyResult FindDependencies([NotNull] IClass clazz)
        {
            var module = ModuleInfo.Create(clazz, _checker);

            var visiting = new Stack<ModuleInfo>();
            var visited = new List<ModuleInfo>();

            DependencyResult result;

            try
            {
                SortAttributeDependenciesRecursively(module, visiting, visited);
                result = DependencyResult.ForDependencies(visited);
            }
            catch (ModuleCyclicDependencyException exception)
            {
                result = DependencyResult.ForCircularSegments(exception.Segments);
            }

            return result;
        }


        public class DependencyResult
        {
            public bool HasCircularDependency { get; private init; }

            [CanBeNull]
            public IList<ModuleInfo> CircularSegments { get; private init; }

            [CanBeNull]
            public ICollection<ModuleInfo> Dependencies { get; private init; }

            private DependencyResult()
            {
            }

            public static DependencyResult ForDependencies(ICollection<ModuleInfo> dependencies)
            {
                return new DependencyResult
                {
                    Dependencies = dependencies,
                    HasCircularDependency = false
                };
            }

            public static DependencyResult ForCircularSegments(IList<ModuleInfo> circularPaths)
            {
                return new DependencyResult
                {
                    CircularSegments = circularPaths,
                    HasCircularDependency = true
                };
            }
        }
    }
}
