using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperAbp.Highlightings;
using ReSharperAbp.Utils;

namespace ReSharperAbp.Analyzers
{
    [ElementProblemAnalyzer(typeof(IClassDeclaration), HighlightingTypes = new[]
    {
        typeof(ModuleMarkOnGutter)
    })]
    public class ModuleDeclarationAnalyzer : AbpElementProblemAnalyzer<IClassDeclaration>
    {
        protected override void Run(IClassDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            if (element.IsAbpModuleClass())
            {
                consumer.AddHighlighting(new ModuleMarkOnGutter(element));
            }
        }
    }
}
