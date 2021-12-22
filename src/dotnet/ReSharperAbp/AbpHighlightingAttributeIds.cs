using JetBrains.TextControl.DocumentMarkup;
using ReSharperAbp.Dependency;
using ReSharperAbp.Module;

namespace ReSharperAbp
{
    [RegisterHighlighter(AbpModuleGutterIconAttribute, EffectType = EffectType.GUTTER_MARK,
        GutterMarkType = typeof(ModuleMarkOnGutter.IconGutterMark))]
    [RegisterHighlighter(AbpDependencyGutterIconAttribute, EffectType = EffectType.GUTTER_MARK,
        GutterMarkType = typeof(DependencyMarkOnGutter.IconGutterMark))]
    public static class AbpHighlightingAttributeIds
    {
        public const string AbpModuleGutterIconAttribute = nameof(AbpModuleGutterIconAttribute);
        public const string AbpDependencyGutterIconAttribute = nameof(AbpDependencyGutterIconAttribute);
    }
}