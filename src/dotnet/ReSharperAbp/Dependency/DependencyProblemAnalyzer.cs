using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperAbp.Abstraction;

namespace ReSharperAbp.Dependency
{
    [ElementProblemAnalyzer(typeof(IClassDeclaration))]
    public sealed class DependencyProblemAnalyzer : AbpProblemAnalyzer<ICSharpTreeNode>
    {
        protected override void Run(ICSharpTreeNode element, ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer, IAbp checker)
        {
            switch (element)
            {
                case IClassDeclaration classDeclaration:
                    AnalyzeDependencyDeclaration(classDeclaration, consumer, checker);
                    break;
            }
        }


        private static void AnalyzeDependencyDeclaration(IClassDeclaration declaration, IHighlightingConsumer consumer,
            IAbp checker)
        {
            if (checker.Dependency.IsDependency(declaration.DeclaredElement))
            {
                consumer.AddHighlighting(new DependencyMarkOnGutter(declaration));
            }
        }
    }
}