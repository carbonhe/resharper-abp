using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperAbp.Tests
{

    [ZoneDefinition]
    public class ReSharperAbpTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IReSharperAbpZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<ReSharperAbpTestEnvironmentZone> { }
    
    [SetUpFixture]
    public class ReSharperAbpTestsAssembly : ExtensionTestEnvironmentAssembly<ReSharperAbpTestEnvironmentZone> { }
}