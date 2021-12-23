using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.Util;

namespace ReSharperAbp.Utils
{
    public static class ModuleUtil
    {
        public static bool IsAbpModuleClass(this IClassDeclaration declaration)
        {
            var clazz = declaration.DeclaredElement;
            return clazz.IsAbpModuleClass();
        }

        public static bool IsAbpModuleClass(this IClass clazz)
        {
            return clazz is { IsAbstract: false }
                   && clazz.TypeParameters.IsEmpty()
                   && clazz.IsDescendantOf(TypeFactory.CreateTypeByCLRName(KnownTypes.AbpModule, clazz.Module).GetTypeElement());
        }


        public static IEnumerable<IClass> GetModuleDependencies(IClass clazz)
        {
        }
    }
}
