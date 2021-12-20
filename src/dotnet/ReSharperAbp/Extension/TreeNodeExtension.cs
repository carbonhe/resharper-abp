using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.RiderTutorials.Utils;
using ReSharperAbp.Abstraction;

namespace ReSharperAbp.Extension
{
    public static class TreeNodeExtension
    {
        [CanBeNull]
        public static IAbp GetChecker(this ITreeNode node)
        {
            return node.GetProject()?.GetData(AbpFrameworkInitializer.AbpKey);
        }


        public static bool IsDependsOnAttribute(this IAttribute attribute)
        {
            var checker = attribute.GetChecker();

            var reference = attribute.TypeReference;

            return checker != null && reference?.Resolve().Result.DeclaredElement is IClass clazz &&
                   clazz.GetFullClrName() == checker.Module.DependsOnAttribute;
        }
    }
}