using JetBrains.Application;
using JetBrains.Application.Resources;
using JetBrains.Application.UI.ToolWindowManagement;

namespace ReSharperAbp.ToolWindow
{
    [ToolWindowDescriptor(
            ProductNeutralId = "Abp",
            Text = "Abp",
            Icon = typeof(IdeThemedIcons.TextDocument),
            Type = ToolWindowType.MultiInstance,
            VisibilityPersistenceScope = ToolWindowVisibilityPersistenceScope.Global,
            InitialDocking = ToolWindowInitialDocking.Right
        )
    ]
    public class AbpToolWindowDescriptor : ToolWindowDescriptor
    {
        public AbpToolWindowDescriptor(IApplicationHost host) : base(host)
        {
        }
    }
}