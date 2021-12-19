using System.Collections.Generic;

namespace ReSharperAbp.Util
{
    public interface ITopologyNode<out TNode> where TNode : ITopologyNode<TNode>
    {
        IEnumerable<TNode> GetIncomingNodes();

        IEnumerable<TNode> GetOutgoingNodes();
    }
}
