using System.Linq;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Util;

namespace ReSharperAbp.Checker
{
    public class LegacyAbpChecker : AbpChecker
    {
        public const string PackageName = "Abp";

        public static readonly AbpChecker Instance = new LegacyAbpChecker();


        private LegacyAbpChecker()
        {
        }

        public override IAbpBuiltinTypes BuiltinTypes { get; } = new LegacyBuiltinTypes();


        private class LegacyBuiltinTypes : IAbpBuiltinTypes
        {
            public string ModuleClass => "Abp.Modules.AbpModule";
            public string DependsOnAttribute => "Abp.Modules.DependsOnAttribute";
        }
    }
}
