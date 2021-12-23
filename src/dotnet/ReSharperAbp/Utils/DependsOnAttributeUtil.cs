using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperAbp.Utils
{
    public static class DependsOnAttributeUtil
    {
        public static bool IsDependsOnAttribute(this IAttribute attribute)
        {
            var element = attribute.TypeReference?.Resolve().DeclaredElement;
            return element is IClass clazz && clazz.GetClrName().Equals(KnownTypes.DependsOnAttribute);
        }


        [CanBeNull, ItemNotNull]
        public static IEnumerable<ITypeofExpression> GetDependentTypeofExpressions(IAttribute attribute)
        {
            if (!attribute.IsValid() || !attribute.IsDependsOnAttribute())
            {
                yield break;
            }

            foreach (var argument in attribute.Arguments)
            {
                if (argument.Value is ITypeofExpression expression)
                {
                    yield return expression;
                }
            }
        }
    }
}
