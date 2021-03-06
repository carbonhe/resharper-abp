using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperAbp.Highlightings;

namespace ReSharperAbp.Features.Modular.Highlightings
{
    [RegisterConfigurableSeverity(
        SeverityId,
        null,
        AbpHighlightingGroup.Id,
        SeverityId,
        null,
        Severity.ERROR)]
    [ConfigurableSeverityHighlighting(
        SeverityId,
        CSharpLanguage.Name,
        OverlapResolve = OverlapResolveKind.NONE,
        OverloadResolvePriority = 0)]
    public class ModuleCyclicDependencyError : IHighlighting
    {
        private readonly IClassDeclaration _declaration;

        public ModuleCyclicDependencyError(IClassDeclaration declaration, string toolTip)
        {
            ToolTip = toolTip;
            _declaration = declaration;
        }

        private const string SeverityId = "Module cyclic dependency";

        public bool IsValid() => _declaration == null || _declaration.IsValid();

        public DocumentRange CalculateRange() => _declaration.NameIdentifier.GetHighlightingRange();
        public string ToolTip { get; }
        public string ErrorStripeToolTip => ToolTip;
    }
}
