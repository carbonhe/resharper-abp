using System.Collections.Generic;
using System.Linq;
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


        [CanBeNull, ItemNotNull]
        public IEnumerable<IClass> FindDirectDependencies([NotNull] IClass clazz)
        {
            _checker.AssertIsAbpModule(clazz);
            var values = clazz
                .GetAttributeInstances(new ClrTypeName(_checker.BuiltinTypes.DependsOnAttribute), AttributesSource.All)
                .SingleOrDefault()?.PositionParameter(0).ArrayValue;

            if (values != null)
            {
                foreach (var value in values)
                {
                    var dependency = value.TypeValue.GetClassType();
                    if (dependency != null
                        && dependency.GetFullClrName() == clazz.GetFullClrName()
                        && _checker.IsAbpModule(dependency))
                    {
                        yield return dependency;
                    }
                }
            }
        }

        [CanBeNull, ItemNotNull]
        public IEnumerable<IClass> FindAllDependencies([NotNull] IClass clazz)
        {
            var queue = new Queue<IClass>();
            var directDependencies = FindDirectDependencies(clazz);
            if (directDependencies != null)
            {
                foreach (var dependency in directDependencies)
                {
                    queue.Enqueue(dependency);
                }
            }

            while (!queue.IsEmpty())
            {
                var dependency = queue.Dequeue();
                var dependencies = FindDirectDependencies(dependency);
                if (dependencies != null)
                {
                    foreach (var d in dependencies)
                    {
                        queue.Enqueue(d);
                    }
                }

                yield return dependency;
            }
        }
    }
}
