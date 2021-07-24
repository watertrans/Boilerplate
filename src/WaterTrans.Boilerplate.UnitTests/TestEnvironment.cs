using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.Infrastructure.OS;

namespace WaterTrans.Boilerplate.UnitTests
{
    [TestClass]
    public class TestEnvironment
    {
        public static IDateTimeProvider DateTimeProvider { get; } = new DateTimeProvider();

        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
        }
    }
}
