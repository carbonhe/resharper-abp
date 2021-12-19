using System.Collections.Generic;
using JetBrains.Util;
using ReSharperAbp.Util;

namespace ReSharperAbp.Extension
{
    public static class TopologyNodeExtension
    {
        public static ICollection<TNode> GetAllOutgoingNodes<TNode>(this TNode node)
            where TNode : ITopologyNode<TNode>
        {
            var visited = new List<TNode>();
            var visiting = new Stack<TNode>();
            FillingOutgoingNodesRecursively(node, visiting, visited);
            return visited;
        }

        private static void FillingOutgoingNodesRecursively<TNode>(
            TNode node,
            Stack<TNode> visiting,
            ICollection<TNode> visited)
            where TNode : ITopologyNode<TNode>
        {
            if (visiting.Contains(node))
            {
                var segments = new List<TNode>();

                foreach (var vn in visiting)
                {
                    segments.Push(vn);
                    if (vn.Equals(node))
                    {
                        break;
                    }

                    segments.Insert(0, node);
                    segments.Reverse();

                    throw new CyclicDependencyException<TNode>(segments);
                }
            }

            visiting.Push(node);

            var nodes = node.GetOutgoingNodes();

            if (nodes != null)
            {
                foreach (var outgoingNode in nodes)
                {
                    FillingOutgoingNodesRecursively(outgoingNode, visiting, visited);
                }
            }

            visited.Add(visiting.Pop());
        }
    }
}