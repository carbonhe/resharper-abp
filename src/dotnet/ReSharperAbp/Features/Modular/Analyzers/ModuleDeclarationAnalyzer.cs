using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperAbp.Analyzers;
using ReSharperAbp.Features.Modular.Highlightings;
using ReSharperAbp.Features.Modular.Utils;

namespace ReSharperAbp.Features.Modular.Analyzers
{
    [ElementProblemAnalyzer(typeof(IClassDeclaration), HighlightingTypes = new[]
    {
        typeof(ModuleMarkOnGutter)
    })]
    public class ModuleDeclarationAnalyzer : AbpElementProblemAnalyzer<IClassDeclaration>
    {
        protected override void Run(IClassDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            var clazz = element.DeclaredElement;


            if (clazz.IsAbpModuleClass())
            {
                consumer.AddHighlighting(new ModuleMarkOnGutter(element));
            }
        }
    }
}
