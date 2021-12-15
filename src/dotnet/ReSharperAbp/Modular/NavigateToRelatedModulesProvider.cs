using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Application.DataContext;
using JetBrains.Application.UI.TreeModels;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Occurrences;
using JetBrains.ReSharper.Feature.Services.Tree;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperAbp.Modular
{
    [ContextNavigationProvider]
    public class NavigateToRelatedModulesProvider : IContextSearchProvider, INavigateFromHereProvider
    {
        public Action GetSearchesExecution(IDataContext context, INavigationExecutionHost host)
        {
            return () =>
                host.ShowContextPopupMenu(context, SearchRelatedModule(context),
                    () => new OccurrenceDescriptor(context.GetData(ProjectModelDataConstants.SOLUTION)!),
                    OccurrencePresentationOptions.DefaultOptions, false, "Abp Dependencies");
        }

        public IEnumerable<ContextNavigation> CreateWorkflow(IDataContext dataContext)
        {
            var solution = dataContext.GetData(ProjectModelDataConstants.SOLUTION);
            var host = solution!.GetComponent<INavigationExecutionHost>();
            yield return new ContextNavigation("Abp Dependency", null, NavigationActionGroup.Other,
                GetSearchesExecution(dataContext, host));
        }

        private static ICollection<IOccurrence> SearchRelatedModule(IDataContext context)
        {
            var occurrences = new List<IOccurrence>();
            var declaration = context.GetSelectedTreeNode<IClassDeclaration>();
            var checker = declaration?.GetChecker();
            var clazz = declaration?.DeclaredElement;

            if (clazz != null && checker != null && checker.IsAbpModule(clazz))
            {
                var result = checker.ModuleFinder.FindDependencies(clazz);
                if (result.Dependencies != null)
                {
                    foreach (var module in result.Dependencies)
                    {
                        occurrences.Add(new DeclaredElementOccurrence(module.Class));
                    }
                }
            }

            return occurrences;
        }

        private class OccurrenceDescriptor : OccurrenceBrowserDescriptor
        {
            public OccurrenceDescriptor([NotNull] ISolution solution) : base(solution)
            {
            }

            public override TreeModel Model
            {
                get
                {
                    return OccurrenceSections
                        .Select(section => section.Model)
                        .FirstOrDefault();
                }
            }
        }
    }
}
