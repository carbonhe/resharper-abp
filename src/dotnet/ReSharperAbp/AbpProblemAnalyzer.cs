using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperAbp.Checker;
using ReSharperAbp.Extension;

namespace ReSharperAbp
{
    public abstract class AbpProblemAnalyzer<T> : ElementProblemAnalyzer<T>, IConditionalElementProblemAnalyzer
        where T : ITreeNode
    {
        public bool ShouldRun(IFile file, ElementProblemAnalyzerData data)
        {
            return file.Language.Name == CSharpLanguage.Name && file.GetProject()?.GetData(AbpChecker.Key) != null;
        }

        protected sealed override void Run(T element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            Run(element, data, consumer, element.GetChecker()!);
        }

        protected abstract void Run(T element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer,
            AbpChecker checker);
    }
}
