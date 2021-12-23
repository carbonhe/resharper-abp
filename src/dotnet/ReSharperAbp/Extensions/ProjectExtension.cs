using System.Linq;
using JetBrains.ProjectModel;

namespace ReSharperAbp.Extensions
{
    public static class ProjectExtension
    {
        public static bool ContainsAbpReference(this IProject project)
        {
            var frameworkId = project.GetCurrentTargetFrameworkId();
            return project.GetModuleReferences(frameworkId).Any(r => r.Name == "Abp");
        }
    }
}
