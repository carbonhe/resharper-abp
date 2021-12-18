using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.RiderTutorials.Utils;

namespace ReSharperAbp.Modular
{
    [ElementProblemAnalyzer(
        typeof(IAttribute), typeof(IClassDeclaration),
        HighlightingTypes = new[]
        {
            typeof(ReferenceNonAbpModuleTypeError), typeof(DependsOnAttributeUsageError), typeof(CyclicDependencyError)
        })]
    public class ModularProblemAnalyzer : AbpProblemAnalyzer<ICSharpTreeNode>
    {
        protected override void Run(ICSharpTreeNode element, ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer)
        {
            switch (element)
            {
                case IAttribute attribute:
                    AnalyzeDependsOnAttribute(attribute, consumer);
                    break;
                case IClassDeclaration classDeclaration:
                    AnalyzeCyclicDependency(classDeclaration, consumer);
                    break;
            }
        }


        private static void AnalyzeCyclicDependency([NotNull] IClassDeclaration declaration,
            IHighlightingConsumer consumer)
        {
            var checker = declaration.GetChecker();
            if (declaration.DeclaredElement != null
                && checker != null
                && checker.IsAbpModule(declaration.DeclaredElement))
            {
                var result = checker.ModuleFinder.FindDependencies(declaration.DeclaredElement);

                if (result.HasCircularDependency)
                {
                    consumer.AddHighlighting(new CyclicDependencyError(declaration,
                        ModuleCyclicDependencyException.CreateDetailsMessage(result.CircularSegments)));
                }
            }
        }

        private static void AnalyzeDependsOnAttribute(IAttribute attribute, IHighlightingConsumer consumer)
        {
            var clazz = attribute.GetParentOfType<IClassDeclaration>()?.DeclaredElement;
            if (!attribute.IsDependsOnAttribute() || clazz == null)
            {
                return;
            }

            var checker = attribute.GetChecker()!;
            if (!checker.IsAbpModule(clazz))
            {
                consumer.AddHighlighting(new DependsOnAttributeUsageError(attribute));
            }

            foreach (var argument in attribute.Arguments)
            {
                if (argument.Expression is ITypeofExpression expression)
                {
                    if (!checker.IsAbpModule(expression.ArgumentType.GetClassType()))
                    {
                        consumer.AddHighlighting(new ReferenceNonAbpModuleTypeError(expression.TypeName));
                    }
                }
            }
        }
    }
}
