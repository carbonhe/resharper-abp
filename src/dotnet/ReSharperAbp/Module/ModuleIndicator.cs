using System.Collections.Generic;
using JetBrains.Application.UI.Controls.BulbMenu.Items;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl.DocumentMarkup;
using JetBrains.Util;

namespace ReSharperAbp.Module
{
    [StaticSeverityHighlighting(Severity.INFO, typeof(AbpHighlightingGroup),
        AttributeId = AbpHighlightingAttributeIds.AbpModuleGutterIconAttribute,
        OverlapResolve = OverlapResolveKind.NONE,
        ToolTipFormatString = Message)]
    public class ModuleIndicator : IHighlighting
    {
        private const string Message = "Abp Module";

        private readonly IClassDeclaration _classDeclaration;

        public ModuleIndicator(IClassDeclaration classDeclaration)
        {
            _classDeclaration = classDeclaration;
        }


        public bool IsValid() => _classDeclaration == null || _classDeclaration.IsValid();

        public DocumentRange CalculateRange() => _classDeclaration.NameIdentifier.GetHighlightingRange();

        public string ToolTip => Message;
        public string ErrorStripeToolTip => ToolTip;

        public class IconGutterMark : IconGutterMarkType
        {
            public IconGutterMark() : base(AbpIcons.Module.Id)
            {
            }

            public override IEnumerable<BulbMenuItem> GetBulbMenuItems(IHighlighter highlighter)
            {
                return EmptyList<BulbMenuItem>.Instance;
            }
        }
    }
}
