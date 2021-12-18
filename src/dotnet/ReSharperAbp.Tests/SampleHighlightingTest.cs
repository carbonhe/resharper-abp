using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;
using ReSharperAbp.Modular;

namespace ReSharperAbp.Tests
{
    public class SampleHighlightingTest : CSharpHighlightingTestBase
    {
        protected override string RelativeTestDataPath => "CSharp";

        protected override bool HighlightingPredicate(
            IHighlighting highlighting,
            IPsiSourceFile sourceFile,
            IContextBoundSettingsStore settingsStore)
        {
            return highlighting is ReferenceNonAbpModuleTypeError;
        }

        [Test]
        public void TestModularProblemAnalyzer()
        {
            DoNamedTest2();
        }
    }
}
