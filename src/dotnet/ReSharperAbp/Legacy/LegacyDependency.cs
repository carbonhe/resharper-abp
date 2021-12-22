using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using ReSharperAbp.Abstraction;
using ReSharperAbp.Dependency;
using CollectionUtil = JetBrains.Util.CollectionUtil;

namespace ReSharperAbp.Legacy
{
    public class LegacyDependency : IDependency
    {
        public DependencyComponentAnalyzer ComponentAnalyzer { get; }


        public LegacyDependency()
        {
            ComponentAnalyzer = new DependencyComponentAnalyzer();

            // Analyze by conventions
            ComponentAnalyzer.Add(clazz =>
            {
                var components = new HashSet<ITypeElement>
                {
                    clazz
                };

                var supers = clazz.GetSuperTypeElements()
                    .Where(e => !e.GetClrName().Equals(LegacyAbp.PredefinedType.ISingletonDependency)
                                || !e.GetClrName().Equals(LegacyAbp.PredefinedType.ITransientDependency));

                foreach (var super in supers)
                {
                    var name = super.ShortName;
                    if (name.StartsWith("I") && clazz.ShortName.EndsWith(name[1..]))
                    {
                        components.Add(super);
                    }
                }

                return components;
            });
        }

        [ContractAnnotation("null => false")]
        public bool IsDependency(IClass clazz)
        {
            if (clazz == null)
            {
                return false;
            }

            return CollectionUtil.IsEmpty(clazz.TypeParameters) && clazz.GetSuperTypeElements()
                .Any(s =>
                    s.GetClrName().Equals(LegacyAbp.PredefinedType.ISingletonDependency) ||
                    s.GetClrName().Equals(LegacyAbp.PredefinedType.ITransientDependency));
        }

        public DependencyInfo CreateDependencyInfo(IClass clazz)
        {
            return new DependencyInfo(clazz, this);
        }

        [CanBeNull]
        public DependencyInfo FindImplementation([NotNull] ITypeElement type)
        {
            var elements = type.GetPsiServices().Symbols.GetSymbolScope(LibrarySymbolScope.FULL, true).GetAllTypeElementsGroupedByName();
            var dependencyClasses = elements.OfType<IClass>().Where(IsDependency);
            var clazz = dependencyClasses.FirstOrDefault(c => ComponentAnalyzer.Analyze(c).Contains(type));

            return clazz == null ? null : CreateDependencyInfo(clazz);
        }
    }
}
