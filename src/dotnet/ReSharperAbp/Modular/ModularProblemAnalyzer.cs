using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.RiderTutorials.Utils;
using JetBrains.Util;
using ReSharperAbp.Checker;
using ReSharperAbp.Extension;
using ReSharperAbp.Util;

namespace ReSharperAbp.Modular
{
    [ElementProblemAnalyzer(
        typeof(IAttribute), typeof(IClassDeclaration),
        HighlightingTypes = new[]
        {
            typeof(ReferenceNonAbpModuleTypeError), typeof(DependsOnAttributeUsageError), typeof(CyclicDependencyError)
        })]
    public sealed class ModularProblemAnalyzer : AbpProblemAnalyzer<ICSharpTreeNode>
    {
        protected override void Run([NotNull] ICSharpTreeNode element, [NotNull] ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer, [NotNull] AbpChecker checker)
        {
            switch (element)
            {
                case IAttribute attribute:
                    AnalyzeDependsOnAttribute(attribute, consumer, checker);
                    break;
                case IClassDeclaration classDeclaration:
                    AnalyzeCyclicDependency(classDeclaration, consumer, checker);
                    AnalyzeModuleGutterIcon(classDeclaration, consumer, checker);
                    break;
            }
        }


        private static void AnalyzeCyclicDependency([NotNull] IClassDeclaration declaration,
            IHighlightingConsumer consumer, AbpChecker checker)
        {
            if (declaration.DeclaredElement != null
                && checker.IsAbpModule(declaration.DeclaredElement))
            {
                try
                {
                    checker.ToModuleInfo(declaration.DeclaredElement).GetAllOutgoingNodes();
                }
                catch (CyclicDependencyException<ModuleInfo> exception)
                {
                    var message =
                        $"Abp Module Cyclic Dependency: {exception.Segments.Select(m => m.Class.GetFullClrName()).Join(" -> ")}";

                    consumer.AddHighlighting(new CyclicDependencyError(declaration, message));
                }
            }
        }

        private static void AnalyzeModuleGutterIcon([NotNull] IClassDeclaration declaration,
            IHighlightingConsumer consumer, AbpChecker checker)
        {
            if (checker.IsAbpModule(declaration.DeclaredElement))
            {
                consumer.AddHighlighting(new ModuleIndicator(declaration));
            }
        }

        private static void AnalyzeDependsOnAttribute(IAttribute attribute, IHighlightingConsumer consumer,
            AbpChecker checker)
        {
            var clazz = attribute.GetParentOfType<IClassDeclaration>()?.DeclaredElement;
            if (!attribute.IsDependsOnAttribute() || clazz == null)
            {
                return;
            }

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
