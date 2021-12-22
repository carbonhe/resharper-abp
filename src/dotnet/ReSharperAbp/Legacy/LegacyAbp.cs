using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;
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

        public IDependency Dependency { get; } = new LegacyDependency();


        // ReSharper disable InconsistentNaming
        public static class PredefinedType
        {
            public static readonly IClrTypeName AbpModule = new ClrTypeName("Abp.Modules.AbpModule");
            public static readonly IClrTypeName ITransientDependency = new ClrTypeName("Abp.Dependency.ITransientDependency");
            public static readonly IClrTypeName ISingletonDependency = new ClrTypeName("Abp.Dependency.ISingletonDependency");
        }
    }
}
