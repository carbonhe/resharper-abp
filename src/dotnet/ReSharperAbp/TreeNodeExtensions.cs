using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.RiderTutorials.Utils;
using ReSharperAbp.Checker;

namespace ReSharperAbp
{
    public static class TreeNodeExtensions
    {
        public static AbpChecker GetChecker(this ITreeNode node)
        {
            return node.GetProject()?.GetData(AbpChecker.Key);
        }

        public static bool IsAbpInstalled(this ITreeNode node)
        {
            return node.GetChecker() != null;
        }


        public static bool IsDependsOnAttribute(this IAttribute attribute)
        {
            return attribute.TypeReference?.Resolve().Result.DeclaredElement is IClass clazz &&
                   clazz.GetFullClrName() == attribute.GetChecker().BuiltinTypes.DependsOnAttribute;
        }
    }
}
