using JetBrains.Application.DataContext;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperAbp.Checker;
using ReSharperAbp.Modular;

namespace ReSharperAbp
{
    [Action("SampleAction", "Sample Action")]
    public class AbpAction : IExecutableAction
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            return true;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            var element = context.GetSelectedTreeNode<IClassDeclaration>();
        }
    }
}
