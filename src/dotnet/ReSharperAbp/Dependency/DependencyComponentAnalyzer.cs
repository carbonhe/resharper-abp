using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;
using ReSharperAbp.Util;

namespace ReSharperAbp.Dependency
{
    public class DependencyComponentAnalyzer
    {
        private readonly ISet<Func<IClass, ICollection<ITypeElement>>> _analyzers;

        public DependencyComponentAnalyzer()
        {
            _analyzers = new HashSet<Func<IClass, ICollection<ITypeElement>>>();
        }


        public void Add([NotNull] Func<IClass, ICollection<ITypeElement>> analyzer)
        {
            Check.NotNull(analyzer, nameof(analyzer));
            _analyzers.Add(analyzer);
        }

        [NotNull]
        public ICollection<ITypeElement> Analyze([NotNull] IClass clazz)
        {
            var components = new HashSet<ITypeElement>();

            foreach (var analyzer in _analyzers)
            {
                components.AddRange(analyzer(clazz));
            }

            return components;
        }
    }
}
