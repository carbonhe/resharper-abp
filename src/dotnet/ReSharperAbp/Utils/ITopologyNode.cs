using System.Collections.Generic;
using JetBrains.Annotations;

namespace ReSharperAbp.Util
{
    public interface ITopologyNode<out TNode> where TNode : ITopologyNode<TNode>
    {
        [CanBeNull, ItemNotNull]
        IEnumerable<TNode> GetIncomingNodes();

        [CanBeNull, ItemNotNull]
        IEnumerable<TNode> GetOutgoingNodes();
    }
}
