using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Application.changes;
using JetBrains.Diagnostics;
using JetBrains.Lifetimes;
using JetBrains.Metadata.Utils;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.Assemblies.Impl;
using JetBrains.ProjectModel.Tasks;
using JetBrains.Util;
using JetBrains.Util.Reflection;
using ReSharperAbp.Checker;

namespace ReSharperAbp
{
    [SolutionComponent]
    public class AbpFrameworkInitializer : IChangeProvider
    {
        private static readonly ICollection<AssemblyNameInfo> AbpAssemblyNames = new HashSet<AssemblyNameInfo>
        {
            AssemblyNameInfoFactory.Create(VNextAbpChecker.PackageName),
            AssemblyNameInfoFactory.Create(LegacyAbpChecker.PackageName),
        };

        private readonly ISolution _solution;
        private readonly ChangeManager _changeManager;
        private readonly Lifetime _lifetime;
        private readonly ILogger _logger;
        private readonly ModuleReferenceResolveSync _moduleReferenceResolveSync;
        private readonly IViewableProjectsCollection _projectView;

        public bool HasReferenceAbp => _solution.GetTopLevelProjects().Any(p => p.GetData(AbpChecker.Key) != null);


        public AbpFrameworkInitializer(
            ISolution solution, ISolutionLoadTasksScheduler scheduler,
            ChangeManager changeManager,
            Lifetime lifetime, ModuleReferenceResolveSync moduleReferenceResolveSync,
            ILogger logger,
            IViewableProjectsCollection projectView)
        {
            _solution = solution;
            _changeManager = changeManager;
            _lifetime = lifetime;
            _moduleReferenceResolveSync = moduleReferenceResolveSync;
            _logger = logger;
            _projectView = projectView;

            scheduler.EnqueueTask(
                new SolutionLoadTask("Prepare Abp framework",
                    SolutionLoadTaskKinds.PreparePsiModules,
                    OnPreparePsiModules));
        }


        private void OnPreparePsiModules()
        {
            _changeManager.RegisterChangeProvider(_lifetime, this);
            _changeManager.AddDependency(_lifetime, this, _moduleReferenceResolveSync);

            _projectView.Projects.View(_lifetime, (_, project) => UpdateAbpChecker(project));
        }

        private static void UpdateAbpChecker([NotNull] IProject project)
        {
            var frameworkId = project.GetCurrentTargetFrameworkId();

            var moduleNames = project.GetModuleReferences(frameworkId).Select(r => r.Name).ToArray();

            if (moduleNames.Contains(VNextAbpChecker.PackageName))
            {
                project.PutData(AbpChecker.Key, VNextAbpChecker.Instance);
            }
            else if (moduleNames.Contains(LegacyAbpChecker.PackageName))
            {
                project.PutData(AbpChecker.Key, LegacyAbpChecker.Instance);
            }
        }


        public object Execute(IChangeMap changeMap)
        {
            var projectModelChange = changeMap.GetChange<ProjectModelChange>(_solution);

            var changes = ReferencedAssembliesService.TryGetAssemblyReferenceChanges(projectModelChange,
                AbpAssemblyNames,
                _logger.Trace());


            foreach (var change in changes)
            {
                if (change.IsAdded)
                {
                    UpdateAbpChecker(change.GetNewProject());
                }
            }


            return null;
        }
    }
}