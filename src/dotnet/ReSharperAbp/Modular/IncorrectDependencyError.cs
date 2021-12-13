using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperAbp.Modular
{
    [RegisterConfigurableSeverity(
        SeverityId,
        null,
        AbpFrameworkInitializer.HighlightingGroupId,
        SeverityId,
        null,
        Severity.ERROR)]
    [ConfigurableSeverityHighlighting(
        SeverityId,
        CSharpLanguage.Name,
        OverlapResolve = OverlapResolveKind.NONE,
        OverloadResolvePriority = 0)]
    public class IncorrectDependencyError : IHighlighting
    {
        private const string SeverityId = "IncorrectAbpModuleDependency";
        private readonly ITypeofExpression _exp;


        public IncorrectDependencyError(ITypeofExpression exp)
        {
            _exp = exp;
        }

        public bool IsValid() => _exp == null || _exp.IsValid();


        public DocumentRange CalculateRange()
        {
            return _exp.TypeName.GetHighlightingRange();
        }

        public string ToolTip =>
            $"Abp: {_exp.ArgumentType} is not an module";

        public string ErrorStripeToolTip => ToolTip;
    }
}
