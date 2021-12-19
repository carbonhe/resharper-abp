using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using ReSharperAbp.Util;

namespace ReSharperAbp.DependencyInjection
{
    public class ServiceInfo : ITopologyNode<ServiceInfo>
    {
        public IClass Implementation { get; set; }

        public ServiceLifecycle Lifecycle { get; set; }

        public ICollection<ITypeElement> ExposeServices { get; set; }


        public IEnumerable<ServiceInfo> GetOutgoingNodes()
        {
            throw new System.NotImplementedException();
        }


        public IEnumerable<ServiceInfo> GetIncomingNodes()
        {
            throw new System.NotImplementedException();
        }
    }
}