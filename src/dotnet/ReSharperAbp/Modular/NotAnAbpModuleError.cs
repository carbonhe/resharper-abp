using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperAbp.Modular
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
    public class NotAnAbpModuleError : IHighlighting
    {
        private const string SeverityId = "Not an Abp module";
        private readonly ITypeofExpression _exp;


        public NotAnAbpModuleError(ITypeofExpression exp)
        {
            _exp = exp;
        }

        public bool IsValid() => _exp == null || _exp.IsValid();


        public DocumentRange CalculateRange()
        {
            return _exp.TypeName.GetHighlightingRange();
        }

        public string ToolTip =>
            $"{_exp.ArgumentType} is not an Abp module";

        public string ErrorStripeToolTip => ToolTip;
    }
}