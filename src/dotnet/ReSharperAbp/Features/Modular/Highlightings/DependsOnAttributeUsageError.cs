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
    public class DependsOnAttributeUsageError : IHighlighting
    {
        private const string SeverityId = "DependsOn attribute usage error";

        private readonly IAttribute _attribute;

        public DependsOnAttributeUsageError(IAttribute attribute)
        {
            _attribute = attribute;
        }

        public bool IsValid() => _attribute == null || _attribute.IsValid();

        public DocumentRange CalculateRange() => _attribute.GetHighlightingRange();

        public string ToolTip => "DependsOn attribute can only be used on Abp module";
        public string ErrorStripeToolTip => ToolTip;
    }
}
