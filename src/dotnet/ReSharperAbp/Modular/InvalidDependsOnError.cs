using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperAbp.Modular
{

    [ConfigurableSeverityHighlighting(
        SeverityId,
        CSharpLanguage.Name,
        OverlapResolve = OverlapResolveKind.NONE,
        OverloadResolvePriority = 0)]
    public class InvalidDependsOnError : IHighlighting
    {
        private const string SeverityId = "Invalid DependsOn Attribute";

        private readonly IAttribute _attribute;

        public InvalidDependsOnError(IAttribute attribute)
        {
            _attribute = attribute;
        }

        public bool IsValid() => _attribute == null || _attribute.IsValid();

        public DocumentRange CalculateRange() => _attribute.GetHighlightingRange();

        public string ToolTip => "Abp: DependsOn attribute can only decorate an module";
        public string ErrorStripeToolTip => ToolTip;
    }
}
