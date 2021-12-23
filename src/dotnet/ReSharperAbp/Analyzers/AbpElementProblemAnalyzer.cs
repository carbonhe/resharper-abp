using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperAbp.Extensions;

namespace ReSharperAbp.Analyzers
{
    public abstract class AbpElementProblemAnalyzer<T> : ElementProblemAnalyzer<T>, IConditionalElementProblemAnalyzer
        where T : ITreeNode
    {
        public bool ShouldRun(IFile file, ElementProblemAnalyzerData data)
        {
            return file.Language.Name == CSharpLanguage.Name && file.GetProject() != null && file.GetProject().ContainsAbpReference();
        }
    }
}
