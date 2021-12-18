using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.RiderTutorials.Utils;
using ReSharperAbp.Checker;

namespace ReSharperAbp
{
    public static class TreeNodeExtensions
    {
        #region TreeNode

        [CanBeNull]
        public static AbpChecker GetChecker(this ITreeNode node)
        {
            return node.GetProject()?.GetData(AbpChecker.Key);
        }

        #endregion


        #region Attribute

        public static bool IsDependsOnAttribute(this IAttribute attribute)
        {
            var checker = attribute.GetChecker();

            var reference = attribute.TypeReference;

            return checker != null && reference?.Resolve().Result.DeclaredElement is IClass clazz &&
                   clazz.GetFullClrName() == checker.BuiltinTypes.DependsOnAttribute;
        }

        #endregion
    }
}
