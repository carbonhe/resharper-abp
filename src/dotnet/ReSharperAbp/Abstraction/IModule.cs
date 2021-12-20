using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using ReSharperAbp.Module;

namespace ReSharperAbp.Abstraction
{
    public interface IModule
    {
        string DependsOnAttribute { get; }

        bool IsAbpModule(IClass clazz);

        ModuleInfo CreateModuleInfo(IClass clazz);

        IEnumerable<IClass> GetModuleDependencies(IClass clazz);
    }
}