using System;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperAbp.Abstraction;

namespace ReSharperAbp.Dependency
{
    public class DependencyProblemAnalyzer : AbpProblemAnalyzer<ICSharpTreeNode>
    {
        protected override void Run(ICSharpTreeNode element, ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer, IAbp checker)
        {
            throw new NotImplementedException();
        }
    }
}