using JetBrains.Application.BuildScript.Application.Zones;

namespace ReSharperAbp
{
    [ZoneMarker]
    public class ZoneMarker : IRequire<IReSharperAbpZone>
    {
    }
}