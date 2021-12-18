using JetBrains.ReSharper.Feature.Services.Daemon;

namespace ReSharperAbp
{
    [RegisterConfigurableHighlightingsGroup(Id, Title)]
    [RegisterStaticHighlightingsGroup(Title, true)]
    public static class AbpHighlightingGroup
    {
        public const string Id = "Abp";
        public const string Title = "Abp Framework";
    }
}
