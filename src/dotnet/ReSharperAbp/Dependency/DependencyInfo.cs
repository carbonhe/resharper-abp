using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using ReSharperAbp.Util;

namespace ReSharperAbp.Dependency
{
    public class DependencyInfo : ITopologyNode<DependencyInfo>
    {
        public IClass Implementation { get; set; }

        public DependencyLifecycle Lifecycle { get; set; }

        public ICollection<ITypeElement> ExposeServices { get; set; }


        public IEnumerable<DependencyInfo> GetOutgoingNodes()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<DependencyInfo> GetIncomingNodes()
        {
            throw new NotImplementedException();
        }
    }
}