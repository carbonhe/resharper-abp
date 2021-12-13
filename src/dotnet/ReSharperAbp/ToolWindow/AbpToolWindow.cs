using JetBrains.Application.UI.Components;
using JetBrains.Application.UI.Options.OptionPages;
using JetBrains.Application.UI.ToolWindowManagement;
using JetBrains.Lifetimes;

namespace ReSharperAbp.ToolWindow
{
    public class AbpToolWindow
    {
        private readonly ToolWindowInstance _instance;

        public AbpToolWindow(Lifetime lifetime, ToolWindowManager toolWindowManager,
            ToolWindowDescriptor descriptor, IUIApplication uiApplication)
        {
            var toolWindowClass = toolWindowManager.Classes[descriptor];

            _instance = toolWindowClass.RegisterInstance<AOptionsPage>(lifetime, "Abp", null);
        }

        public void Show()
        {
            _instance.Show();
        }
    }
}
