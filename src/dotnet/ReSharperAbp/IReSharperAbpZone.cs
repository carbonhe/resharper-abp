using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.Platform.RdFramework.Actions.Backend;
using JetBrains.Platform.RdFramework.Actions.Frontend;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;

namespace ReSharperAbp
{
    [ZoneDefinition]
    [ZoneDefinitionConfigurableFeature("Title", "Description", IsInProductSection: false)]
    public interface IReSharperAbpZone : IPsiLanguageZone,
        IRequire<ILanguageCSharpZone>,
        IRequire<DaemonZone>, IRequire<IRdActionsBackendZone>, IRequire<IRdActionsFrontendZone>
    {
    }
}