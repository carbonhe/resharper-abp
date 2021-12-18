using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperAbp.Modular
{
    [StaticSeverityHighlighting(Severity.ERROR, typeof(AbpHighlightingGroup), OverlapResolve = OverlapResolveKind.ERROR,
        ToolTipFormatString = Message)]
    public class ReferenceNonAbpModuleTypeError : IHighlighting
    {
        private const string Message = "{0} is not an Abp module";
        private readonly ITypeUsage _usage;


        public ReferenceNonAbpModuleTypeError(ITypeUsage usage)
        {
            _usage = usage;
        }

        public bool IsValid() => _usage == null || _usage.IsValid();


        public DocumentRange CalculateRange()
        {
            return _usage.GetHighlightingRange();
        }

        public string ToolTip => string.Format(Message, _usage.GetText());

        public string ErrorStripeToolTip => ToolTip;
    }
}