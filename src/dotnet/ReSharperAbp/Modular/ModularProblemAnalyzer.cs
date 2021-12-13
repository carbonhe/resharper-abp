using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.RiderTutorials.Utils;

namespace ReSharperAbp.Modular
{
    [ElementProblemAnalyzer(
        typeof(ITypeofExpression), typeof(IAttribute),
        HighlightingTypes = new[] { typeof(IncorrectDependencyError), typeof(InvalidDependsOnError) })]
    public class ModularProblemAnalyzer : AbpProblemAnalyzer<ICSharpTreeNode>
    {
        protected override void Run(ICSharpTreeNode element, ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer)
        {
            switch (element)
            {
                case ITypeofExpression typeofExpression:
                    ProcessIncorrectDependency(typeofExpression, consumer);
                    break;
                case IAttribute attribute:
                    ProcessInvalidDependsOn(attribute, consumer);
                    break;
            }
        }


        private static void ProcessInvalidDependsOn(IAttribute attribute, IHighlightingConsumer consumer)
        {
            var clazz = attribute.GetParentOfType<IClassDeclaration>()?.DeclaredElement;
            if (clazz != null
                &&
                attribute.IsDependsOnAttribute()
                && !attribute.GetChecker().IsAbpModule(clazz))
            {
                consumer.AddHighlighting(new InvalidDependsOnError(attribute));
            }
        }

        private static void ProcessIncorrectDependency(ITypeofExpression element,
            IHighlightingConsumer consumer)
        {
            if (!element.GetParentOfType<IAttribute>().IsDependsOnAttribute())
            {
                return;
            }


            if (element.TypeName is not IUserTypeUsage usage
                || usage.ScalarTypeName.Reference.Resolve().Result.DeclaredElement is not IClass clazz
                || !element.GetChecker().IsAbpModule(clazz))
            {
                consumer.AddHighlighting(new IncorrectDependencyError(element));
            }
        }
    }
}
