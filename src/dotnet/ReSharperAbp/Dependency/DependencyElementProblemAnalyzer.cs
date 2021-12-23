using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperAbp.Abstraction;
using ReSharperAbp.Analyzers;

namespace ReSharperAbp.Dependency
{
    public sealed class DependencyElementProblemAnalyzer
    {
        protected  void Run(ICSharpTreeNode element, ElementProblemAnalyzerData data,
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
