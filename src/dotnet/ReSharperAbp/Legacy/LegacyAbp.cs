using ReSharperAbp.Abstraction;

namespace ReSharperAbp.Legacy
{
    public class LegacyAbp : IAbp
    {
        public const string PackageName = "Abp";

        public static readonly IAbp Instance = new LegacyAbp();

        private LegacyAbp()
        {
        }

        public IModule Module { get; } = new LegacyModule();

        public IDependency Dependency { get; }
    }
}