using System;
using System.Collections.Generic;
using JetBrains.Util;
using ReSharperAbp.Exceptions;

namespace ReSharperAbp.Extensions
{
    public static class TopologyExtension
    {
        public static IEnumerable<TSource> GetAllOutgoingNodesConsiderCyclicDependency<TSource>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>> outgoingSelector)
        {
            var visited = new List<TSource>();
            var visiting = new Stack<TSource>();

            foreach (var node in source)
            {
                FillingOutgoingNodesRecursively(node, outgoingSelector, visiting, visited);
            }

            return visited;
        }


        private static void FillingOutgoingNodesRecursively<TSource>(
            TSource node,
            Func<TSource, IEnumerable<TSource>> outgoingSelector,
            Stack<TSource> visiting,
            ICollection<TSource> visited)
        {
            if (visiting.Contains(node))
            {
                var segments = new List<TSource>();

                foreach (var vn in visiting)
                {
                    segments.Push(vn);
                    if (vn.Equals(node))
                    {
                        break;
                    }
                }

                segments.Insert(0, node);
                segments.Reverse();

                throw new CyclicDependencyException<TSource>(segments);
            }

            visiting.Push(node);

            var nodes = outgoingSelector(node);

            if (nodes != null)
            {
                foreach (var outgoingNode in nodes)
                {
                    FillingOutgoingNodesRecursively(outgoingNode, outgoingSelector, visiting, visited);
                }
            }

            visited.Add(visiting.Pop());
        }
    }
}
