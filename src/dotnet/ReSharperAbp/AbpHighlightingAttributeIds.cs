using JetBrains.TextControl.DocumentMarkup;
using ReSharperAbp.Modular;

namespace ReSharperAbp
{
    [RegisterHighlighter(AbpModuleGutterIconAttribute, EffectType = EffectType.GUTTER_MARK,
        GutterMarkType = typeof(ModuleIndicator.IconGutterMark))]
    public static class AbpHighlightingAttributeIds
    {
        public const string AbpModuleGutterIconAttribute = nameof(AbpModuleGutterIconAttribute);
    }
}