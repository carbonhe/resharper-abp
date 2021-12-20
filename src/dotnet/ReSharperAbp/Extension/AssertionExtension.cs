using System;
using JetBrains.ReSharper.Psi;
using JetBrains.RiderTutorials.Utils;
using ReSharperAbp.Abstraction;

namespace ReSharperAbp.Extension
{
    public static class AssertionExtension
    {
        public static void AssertIsAbpModule(this IModule checker, params IClass[] classes)
        {
            foreach (var clazz in classes)
            {
                if (!checker.IsAbpModule(clazz))
                {
                    throw new InvalidOperationException($"{clazz.GetFullClrName()} not an Abp module");
                }
            }
        }
    }
}