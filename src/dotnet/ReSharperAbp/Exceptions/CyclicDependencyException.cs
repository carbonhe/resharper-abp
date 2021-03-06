using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ReSharperAbp.Exceptions
{
    public class CyclicDependencyException<T> : Exception
    {
        public IList<T> Segments { get; }

        public CyclicDependencyException([NotNull] [ItemNotNull] IList<T> segments)
        {
            Segments = segments;
        }
    }
}