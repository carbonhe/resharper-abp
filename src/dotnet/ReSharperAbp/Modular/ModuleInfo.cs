using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.RiderTutorials.Utils;
using ReSharperAbp.Checker;

namespace ReSharperAbp.Modular
{
    public class ModuleInfo
    {
        private static readonly ConcurrentDictionary<IClass, ModuleInfo> CachedModules = new();


        private readonly AbpChecker _checker;
        public IClass Class { get; }

        public static ModuleInfo Create([NotNull] IClass clazz, [NotNull] AbpChecker checker)
        {
            CachedModules.TryGetValue(clazz, out var module);

            module ??= new ModuleInfo(clazz, checker);
            CachedModules[clazz] = module;
            return module;
        }

        private ModuleInfo([NotNull] IClass clazz, [NotNull] AbpChecker checker)
        {
            checker.AssertIsAbpModule(clazz);
            Class = clazz;
            _checker = checker;
        }

        public override string ToString()
        {
            return $"Abp module: {Class.GetFullClrName()}";
        }

        [Pure]
        public bool HasDependsOnAttribute()
        {
            return Class.HasAttributeInstance(new ClrTypeName(_checker.BuiltinTypes.DependsOnAttribute), true);
        }


        [CanBeNull, ItemNotNull]
        public IEnumerable<ModuleInfo> GetDependsOnAttributeDependencies()
        {
            var attributes = Class.GetAttributeInstances(new ClrTypeName(_checker.BuiltinTypes.DependsOnAttribute),
                AttributesSource.All);

            foreach (var attribute in attributes)
            {
                var values = attribute.PositionParameter(0).ArrayValue;
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        var moduleClass = value.TypeValue.GetClassType();
                        if (moduleClass != null && _checker.IsAbpModule(moduleClass))
                        {
                            yield return Create(moduleClass, _checker);
                        }
                    }
                }
            }
        }
    }
}
