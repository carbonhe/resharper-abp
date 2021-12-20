using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.RiderTutorials.Utils;
using JetBrains.Util;
using ReSharperAbp.Abstraction;
using ReSharperAbp.Extension;
using ReSharperAbp.Util;

namespace ReSharperAbp.Module
{
    [ElementProblemAnalyzer(
        typeof(IAttribute), typeof(IClassDeclaration),
        HighlightingTypes = new[]
        {
            typeof(ReferenceNonAbpModuleTypeError), typeof(DependsOnAttributeUsageError), typeof(CyclicDependencyError)
        })]
    public sealed class ModuleProblemAnalyzer : AbpProblemAnalyzer<ICSharpTreeNode>
    {
        protected override void Run([NotNull] ICSharpTreeNode element, [NotNull] ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer, [NotNull] IAbp checker)
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
            IHighlightingConsumer consumer, IAbp checker)
        {
            if (declaration.DeclaredElement != null
                && checker.Module.IsAbpModule(declaration.DeclaredElement))
            {
                try
                {
                    checker.Module.CreateModuleInfo(declaration.DeclaredElement).GetAllOutgoingNodes();
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
            IHighlightingConsumer consumer, IAbp checker)
        {
            if (checker.Module.IsAbpModule(declaration.DeclaredElement))
            {
                consumer.AddHighlighting(new ModuleIndicator(declaration));
            }
        }

        private static void AnalyzeDependsOnAttribute(IAttribute attribute, IHighlightingConsumer consumer,
            IAbp checker)
        {
            var clazz = attribute.GetParentOfType<IClassDeclaration>()?.DeclaredElement;
            if (!attribute.IsDependsOnAttribute() || clazz == null)
            {
                return;
            }

            if (!checker.Module.IsAbpModule(clazz))
            {
                consumer.AddHighlighting(new DependsOnAttributeUsageError(attribute));
            }

            foreach (var argument in attribute.Arguments)
            {
                if (argument.Expression is ITypeofExpression expression)
                {
                    if (!checker.Module.IsAbpModule(expression.ArgumentType.GetClassType()))
                    {
                        consumer.AddHighlighting(new ReferenceNonAbpModuleTypeError(expression.TypeName));
                    }
                }
            }
        }
    }
}