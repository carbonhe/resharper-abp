using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.RiderTutorials.Utils;
using ReSharperAbp.Abstraction;
using ReSharperAbp.Extension;
using ReSharperAbp.Util;

namespace ReSharperAbp.Module
{
    [CannotApplyEqualityOperator]
    public class ModuleInfo : ITopologyNode<ModuleInfo>, IEquatable<ModuleInfo>
    {
        private readonly IModule _checker;
        public IClass Class { get; }


        public ModuleInfo([NotNull] IClass clazz, [NotNull] IModule checker)
        {
            checker.AssertIsAbpModule(clazz);
            Class = clazz;
            _checker = checker;
        }

        public override string ToString()
        {
            return $"Abp module: {Class.GetFullClrName()}";
        }


        public IEnumerable<ModuleInfo> GetIncomingNodes() => null;

        public IEnumerable<ModuleInfo> GetOutgoingNodes()
        {
            var dependencies = _checker.GetModuleDependencies(Class);
            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    yield return new ModuleInfo(dependency, _checker);
                }
            }
        }


        public bool Equals(ModuleInfo other)
        {
            return other != null && Class.Equals(other.Class);
        }
    }
}
