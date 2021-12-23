using System.Linq;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.RiderTutorials.Utils;
using JetBrains.Util;
using ReSharperAbp.Exceptions;
using ReSharperAbp.Highlightings;
using ReSharperAbp.Utils;

namespace ReSharperAbp.Analyzers
{
    [ElementProblemAnalyzer(typeof(IAttribute), HighlightingTypes = new[]
    {
        typeof(DependsOnAttributeUsageError), typeof(InvalidModuleDependencyError)
    })]
    public class ModuleDependencyAnalyzer : AbpElementProblemAnalyzer<IAttribute>
    {
        protected override void Run(IAttribute element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            if (!element.IsValid() || !element.IsDependsOnAttribute())
            {
                return;
            }

            var classDeclaration = ClassDeclarationNavigator.GetByAttribute(element);
            var clazz = classDeclaration?.DeclaredElement;
            if (clazz != null)
            {
                if (!clazz.IsAbpModuleClass())
                {
                    consumer.AddHighlighting(new DependsOnAttributeUsageError(element));
                }

                try
                {
                    ModuleUtil.GetDependencies(clazz);
                }
                catch (CyclicDependencyException<IClass> exception)
                {
                    var message = $"Abp Module Cyclic Dependency: {exception.Segments.Select(c => c.GetFullClrName()).Join(" -> ")}";
                    consumer.AddHighlighting(new ModuleCyclicDependencyError(classDeclaration, message));
                }
            }

            var expressions = DependsOnAttributeUtil.GetDependentTypeofExpressions(element);

            if (expressions != null)
            {
                foreach (var expression in expressions)
                {
                    if (expression.IsValid() && expression.ArgumentType.GetClassType()?.IsAbpModuleClass() != true)
                    {
                        consumer.AddHighlighting(new InvalidModuleDependencyError(expression.TypeName));
                    }
                }
            }
        }
    }
}