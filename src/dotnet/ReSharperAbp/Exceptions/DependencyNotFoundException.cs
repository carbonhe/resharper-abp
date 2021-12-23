using System;
using JetBrains.ReSharper.Psi;
using JetBrains.RiderTutorials.Utils;

namespace ReSharperAbp.Exceptions
{
    public class DependencyNotFoundException : Exception
    {
        public IDeclaredType DependencyType { get; }

        public IClass Owner { get; }

        public DependencyNotFoundException(IDeclaredType dependencyType, IClass owner)
            : base($"Dependency `{dependencyType.GetClrName().FullName}` not found which needed by `{owner.GetFullClrName()}`")
        {
            DependencyType = dependencyType;
            Owner = owner;
        }
    }
}