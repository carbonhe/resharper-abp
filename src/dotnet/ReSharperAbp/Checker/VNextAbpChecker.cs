namespace ReSharperAbp.Checker
{
    public class VNextAbpChecker : AbpChecker
    {
        public const string PackageName = "Volo.Abp.Core";

        public static readonly AbpChecker Instance = new VNextAbpChecker();

        public override IAbpBuiltinTypes BuiltinTypes { get; } = new VNextBuiltinTypes();

        private VNextAbpChecker()
        {
        }


        private class VNextBuiltinTypes : IAbpBuiltinTypes
        {
            public string ModuleClass { get; }
            public string DependsOnAttribute { get; }
        }
    }
}