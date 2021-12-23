using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.Util;
using ReSharperAbp.Extensions;

namespace ReSharperAbp.Utils
{
    public static class ModuleUtil
    {
        public static bool IsAbpModuleClass(this IClass clazz)
        {
            return clazz is { IsAbstract: false }
                   && clazz.TypeParameters.IsEmpty()
                   && clazz.IsDescendantOf(TypeFactory.CreateTypeByCLRName(KnownTypes.AbpModule, clazz.Module)
                       .GetTypeElement());
        }


        [CanBeNull, ItemNotNull]
        public static IEnumerable<IClass> GetDirectDependencies(IClass clazz)
        {
            var values = clazz.GetAttributeInstances(KnownTypes.DependsOnAttribute, AttributesSource.All)
                .FirstOrDefault()?.PositionParameter(0).ArrayValue;

            if (values != null)
            {
                foreach (var value in values)
                {
                    var dependency = value.TypeValue.GetClassType();
                    if (dependency != null && dependency.IsAbpModuleClass())
                    {
                        yield return dependency;
                    }
                }
            }
        }

        public static IEnumerable<IClass> GetDependencies(IClass clazz)
        {
            return GetDirectDependencies(clazz).GetAllOutgoingNodesConsiderCyclicDependency(GetDirectDependencies);
        }
    }
}