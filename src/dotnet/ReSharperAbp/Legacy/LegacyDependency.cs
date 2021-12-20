using JetBrains.ReSharper.Psi;
using ReSharperAbp.Abstraction;
using ReSharperAbp.Dependency;

namespace ReSharperAbp.Legacy
{
    public class LegacyDependency : IDependency
    {
        public bool IsDependency(IClass clazz)
        {
            throw new System.NotImplementedException();
        }

        public DependencyInfo CreateDependencyInfo(IClass clazz)
        {
            throw new System.NotImplementedException();
        }
    }
}