using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueModelling;

namespace QueueModellingTests
{
    [TestClass]
    public class StatsTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        private Stats CreateStats()
        {
            return new Stats();
        }

        [TestMethod()]
        public void StatsTest()
        {
            // Arrange
            var unitUnderTest = CreateStats();

            unitUnderTest.currentTime = 0;            
            unitUnderTest.endWork = 10;
            unitUnderTest.startQTime = 0;
            unitUnderTest.endQTime = 5;
            unitUnderTest.startWork = 5;

            Assert.AreEqual(5, unitUnderTest.QWaitTime);
            Assert.AreEqual(6, unitUnderTest.workTime);
            Assert.AreEqual(11, unitUnderTest.timeInSystem);
            Assert.IsTrue((.54 < unitUnderTest.touchTimeRatio) && (unitUnderTest.touchTimeRatio < .55));
        }
    }
}
