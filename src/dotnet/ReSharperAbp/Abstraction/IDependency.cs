using JetBrains.ReSharper.Psi;
using ReSharperAbp.Dependency;

namespace ReSharperAbp.Abstraction
{
    public interface IDependency
    {
        bool IsDependency(IClass clazz);

        DependencyInfo CreateDependencyInfo(IClass clazz);
    }
}