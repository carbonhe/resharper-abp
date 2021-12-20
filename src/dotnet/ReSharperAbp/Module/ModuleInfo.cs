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
    public class ModuleInfo : ITopologyNode<ModuleInfo>
    {
        private static readonly ConcurrentDictionary<IClass, ModuleInfo> CachedModules = new();


        private readonly IModule _checker;
        public IClass Class { get; }

        public static ModuleInfo Create([NotNull] IClass clazz, [NotNull] IModule checker)
        {
            checker.AssertIsAbpModule(clazz);
            CachedModules.TryGetValue(clazz, out var module);
            module ??= new ModuleInfo(clazz, checker);
            CachedModules[clazz] = module;
            return module;
        }

        private ModuleInfo([NotNull] IClass clazz, [NotNull] IModule checker)
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
                    yield return Create(dependency, _checker);
                }
            }
        }
    }
}