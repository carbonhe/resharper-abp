using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.Util;
using ReSharperAbp.Abstraction;
using ReSharperAbp.Extension;
using ReSharperAbp.Module;

namespace ReSharperAbp.Legacy
{
    public class LegacyModule : IModule
    {
        public string DependsOnAttribute { get; } = "Abp.Modules.DependsOnAttribute";

        [ContractAnnotation("null => false")]
        public bool IsAbpModule(IClass clazz)
        {
            return clazz is { IsAbstract: false }
                   && clazz.TypeParameters.IsEmpty()
                   && clazz.GetAllSuperClasses()
                       .Any(c => c.GetClrName().FullName == "Abp.Modules.AbpModule");
        }

        public ModuleInfo CreateModuleInfo(IClass clazz)
        {
            return ModuleInfo.Create(clazz, this);
        }

        public IEnumerable<IClass> GetModuleDependencies(IClass clazz)
        {
            this.AssertIsAbpModule(clazz);
            var values = clazz
                .GetAttributeInstances(new ClrTypeName(DependsOnAttribute), AttributesSource.All)
                .SingleOrDefault()?.PositionParameter(0).ArrayValue;

            if (values != null)
            {
                foreach (var value in values)
                {
                    var dependency = value.TypeValue.GetClassType();

                    if (dependency != null)
                    {
                        yield return dependency;
                    }
                }
            }
        }
    }
}