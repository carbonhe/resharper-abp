using ReSharperAbp.Abstraction;

namespace ReSharperAbp.VNext
{
    public class VNextAbp : IAbp
    {
        public const string PackageName = "Volo.Abp.Core";


        public static readonly IAbp Instance = new VNextAbp();

        private VNextAbp()
        {
        }

        public IModule Module { get; }

        public IDependency Dependency { get; }
    }
}