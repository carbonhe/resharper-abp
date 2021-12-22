using JetBrains.TextControl.DocumentMarkup;
using ReSharperAbp.Module;

namespace ReSharperAbp
{
    [RegisterHighlighter(AbpModuleGutterIconAttribute, EffectType = EffectType.GUTTER_MARK,
        GutterMarkType = typeof(ModuleMarkOnGutter.IconGutterMark))]
    public static class AbpHighlightingAttributeIds
    {
        public const string AbpModuleGutterIconAttribute = nameof(AbpModuleGutterIconAttribute);
    }
}