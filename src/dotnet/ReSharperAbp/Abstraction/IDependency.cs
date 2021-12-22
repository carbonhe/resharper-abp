using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using ReSharperAbp.Dependency;

namespace ReSharperAbp.Abstraction
{
    public interface IDependency
    {
        DependencyComponentAnalyzer ComponentAnalyzer { get; }

        bool IsDependency(IClass clazz);

        DependencyInfo CreateDependencyInfo(IClass clazz);

        DependencyInfo FindImplementation([NotNull] ITypeElement type);
    }
}
