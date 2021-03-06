using JetBrains.TextControl.DocumentMarkup;
using ReSharperAbp.Features.Dependency;
using ReSharperAbp.Features.Dependency.Highlightings;
using ReSharperAbp.Features.Modular;
using ReSharperAbp.Features.Modular.Highlightings;

namespace ReSharperAbp.Highlightings
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
