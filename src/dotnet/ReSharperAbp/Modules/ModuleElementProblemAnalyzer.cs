using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.RiderTutorials.Utils;
using JetBrains.Util;
using ReSharperAbp.Abstraction;
using ReSharperAbp.Analyzers;
using ReSharperAbp.Extension;
using ReSharperAbp.Highlightings;
using ReSharperAbp.Util;

namespace ReSharperAbp.Module
{
    public sealed class ModuleElementProblemAnalyzer
    {
        protected void Run([NotNull] ICSharpTreeNode element, [NotNull] ElementProblemAnalyzerData data,
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
                consumer.AddHighlighting(new ModuleMarkOnGutter(declaration));
            }
        }

        private static void AnalyzeDependsOnAttribute(IAttribute attribute, IHighlightingConsumer consumer,
            IAbp checker)
        {
            var clazz = attribute.GetParentOfType<IClassDeclaration>()?.DeclaredElement;


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
                        consumer.AddHighlighting(new InvalidModuleDependencyError(expression.TypeName));
                    }
                }
            }
        }
    }
}
