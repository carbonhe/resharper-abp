using System;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Metadata.Reader.Impl;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.RiderTutorials.Utils;
using JetBrains.Util;
using ReSharperAbp.Modular;

namespace ReSharperAbp.Checker
{
    public abstract class AbpChecker
    {
        public static readonly Key<AbpChecker> Key = new(nameof(AbpChecker));

        public abstract IAbpBuiltinTypes BuiltinTypes { get; }

        public ModuleFinder ModuleFinder { get; }

        protected AbpChecker()
        {
            ModuleFinder = new ModuleFinder(this);
        }

        public virtual bool IsAbpModule([NotNull] IClass clazz)
        {
            return clazz.GetAllSuperClasses().Any(c => c.GetClrName().FullName == BuiltinTypes.ModuleClass);
        }

        public virtual bool IsDependsOn([NotNull] IClass source, [NotNull] IClass target)
        {
            AssertIsAbpModule(source, target);
            var values = source
                .GetAttributeInstances(new ClrTypeName(BuiltinTypes.DependsOnAttribute), AttributesSource.All)
                .SingleOrDefault()?.PositionParameter(0).ArrayValue;

            if (values != null)
            {
                foreach (var value in values)
                {
                    var dependency = value.TypeValue.GetClassType();

                    if (dependency != null)
                    {
                        return dependency.GetFullClrName() == target.GetFullClrName()
                               || IsDependsOn(dependency, target);
                    }
                }
            }

            return false;
        }

        public void AssertIsAbpModule([NotNull] params IClass[] classes)
        {
            foreach (var clazz in classes)
            {
                if (!IsAbpModule(clazz))
                {
                    throw new InvalidOperationException($"{clazz.GetFullClrName()} not an Abp module");
                }
            }
        }
    }


    public interface IAbpBuiltinTypes
    {
        string ModuleClass { get; }

        string DependsOnAttribute { get; }
    }
}
