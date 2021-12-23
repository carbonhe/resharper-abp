using JetBrains.Metadata.Reader.API;
using JetBrains.Metadata.Reader.Impl;

namespace ReSharperAbp
{
    public static class KnownTypes
    {
        public static readonly IClrTypeName AbpModule = new ClrTypeName("Abp.Modules.AbpModule");
        public static readonly IClrTypeName DependsOnAttribute = new ClrTypeName("Abp.Modules.DependsOnAttribute");

        public static readonly IClrTypeName TransientDependency = new ClrTypeName("Abp.Dependency.ITransientDependency");
        public static readonly IClrTypeName SingletonDependency = new ClrTypeName("Abp.Dependency.ISingletonDependency");
    }
}
