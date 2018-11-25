using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QueueModelling;
using System;
using System.Collections.Generic;

namespace QueueModellingTests
{
    [TestClass]
    public class ServerTests
    {
        private MockRepository mockRepository;

        private Mock<queue> mockqueue;
        private Mock<List<Stats>> mockList;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockqueue = this.mockRepository.Create<queue>();
            this.mockList = this.mockRepository.Create<List<Stats>>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private Server CreateServer()
        {
            return new Server(
                this.mockqueue.Object,
                this.mockList.Object);
        }

        [TestMethod]
        public void updateWorkItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = CreateServer();
            int currentTime = 12;

            // Act
            unitUnderTest.updateWorkItem(
                currentTime);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void getIdleCount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = CreateServer();

            // Act
            var result = unitUnderTest.getIdleCount();

            // Assert
            Assert.Fail();
        }
    }
}
