using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.RiderTutorials.Utils;
using JetBrains.Util;

namespace ReSharperAbp.Modular
{
    public class ModuleCyclicDependencyException : Exception
    {
        public IList<ModuleInfo> Segments { get; }

        public ModuleCyclicDependencyException([NotNull] [ItemNotNull] IList<ModuleInfo> segments)
        {
            if (segments == null || segments.Count < 2 || segments.First() != segments.Last())
            {
                throw new InvalidOperationException("No circular dependencies found");
            }

            Segments = segments;
        }

        public override string Message => CreateDetailsMessage(Segments);


        public static string CreateDetailsMessage(IEnumerable<ModuleInfo> segments) =>
            $"Cyclic dependencies: {segments.Select(s => s.Class.GetFullClrName()).Join(" => ")}";
    }
}
