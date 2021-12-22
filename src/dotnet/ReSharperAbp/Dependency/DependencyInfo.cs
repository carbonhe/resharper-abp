using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.RiderTutorials.Utils;
using ReSharperAbp.Abstraction;
using ReSharperAbp.Extension;
using ReSharperAbp.Legacy;
using ReSharperAbp.Util;

namespace ReSharperAbp.Dependency
{
    [CannotApplyEqualityOperator]
    public class DependencyInfo : ITopologyNode<DependencyInfo>, IEquatable<DependencyInfo>
    {
        [NotNull]
        public IClass Implementation { get; }

        public DependencyLifecycle Lifecycle { get; }

        public ISet<ITypeElement> Components { get; }


        private readonly IDependency _checker;

        public DependencyInfo([NotNull] IClass clazz, [NotNull] IDependency checker)
        {
            Check.NotNull(clazz, nameof(clazz));
            Check.NotNull(checker, nameof(checker));


            _checker = checker;
            _checker.AssertIsDependency(clazz);
            Implementation = clazz;
            Components = new HashSet<ITypeElement>();


            foreach (var superType in Implementation.GetSuperTypeElements())
            {
                if (superType.GetClrName().Equals(LegacyAbp.PredefinedType.ITransientDependency))
                {
                    Lifecycle = DependencyLifecycle.Transient;
                }
                else if (superType.GetClrName().Equals(LegacyAbp.PredefinedType.ISingletonDependency))
                {
                    Lifecycle = DependencyLifecycle.Singleton;
                }
                else
                {
                    throw new InvalidOperationException($"Can't determine the lifecycle of dependency `${Implementation.GetFullClrName()}`");
                }
            }
        }


        public IEnumerable<DependencyInfo> GetOutgoingNodes()
        {
            if (Implementation.Constructors.Count() > 1)
            {
                throw new MultipleConstructorException(Implementation);
            }

            var constructor = Implementation.Constructors.First();


            var parameters = constructor.Parameters.Where(p => p.IsValid() && p.Type.GetScalarType() != null);
            foreach (var parameter in parameters)
            {
                var type = parameter.Type.GetScalarType()!;

                if (type.Resolve().DeclaredElement is ITypeElement component)
                {
                    var dependencyInfo = _checker.FindImplementation(component);

                    if (dependencyInfo != null)
                    {
                        yield return dependencyInfo;
                    }
                }
            }
        }


        public IEnumerable<DependencyInfo> GetIncomingNodes()
        {
            throw new NotImplementedException();
        }

        public bool Equals(DependencyInfo other)
        {
            return other != null && Implementation.Equals(other.Implementation);
        }
    }
}
