namespace ReSharperAbp.Abstraction
{
    public interface IAbp
    {
        IModule Module { get; }

        IDependency Dependency { get; }
    }
}