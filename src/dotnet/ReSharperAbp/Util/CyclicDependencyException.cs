using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace ReSharperAbp.Util
{
    public class CyclicDependencyException<T> : Exception
    {
        public IList<T> Segments { get; }

        public CyclicDependencyException([NotNull] [ItemNotNull] IList<T> segments)
        {
            if (segments == null || segments.Count < 2 || segments.First().Equals(segments.Last()))
            {
                throw new InvalidOperationException("No cyclic dependencies found");
            }

            Segments = segments;
        }
    }
}