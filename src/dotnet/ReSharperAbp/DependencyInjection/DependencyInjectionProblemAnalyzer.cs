using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperAbp.Checker;

namespace ReSharperAbp.DependencyInjection
{
    public class DependencyInjectionProblemAnalyzer : AbpProblemAnalyzer<ICSharpTreeNode>
    {
        protected override void Run(ICSharpTreeNode element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer, AbpChecker checker)
        {
            throw new System.NotImplementedException();
        }
    }
}