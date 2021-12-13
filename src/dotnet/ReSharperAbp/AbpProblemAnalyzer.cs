using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperAbp.Checker;

namespace ReSharperAbp
{
    public abstract class AbpProblemAnalyzer<T> : ElementProblemAnalyzer<T>, IConditionalElementProblemAnalyzer
        where T : ITreeNode
    {
        public bool ShouldRun(IFile file, ElementProblemAnalyzerData data)
        {
            return file.Language.Name == CSharpLanguage.Name && file.GetProject()?.GetData(AbpChecker.Key) != null;
        }
    }
}
