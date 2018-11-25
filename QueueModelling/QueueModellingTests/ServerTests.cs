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
            this.mockRepository = new MockRepository(MockBehavior.Strict) { CallBase = true };

            this.mockqueue = this.mockRepository.Create<queue>();
            this.mockList = this.mockRepository.Create<List<Stats>>();
            WorkItem mockItem = new WorkItem(5, .1, 6)
            {
                enQTime = 0,
                deQTime = 2,
                addTime = 0
            };
            this.mockqueue.Setup(m => m.dequeue(It.IsAny<int>())).Returns(mockItem);
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

        /// <summary>
        /// This tests update work item and idle count. It calls idle queue as well.
        /// </summary>
        [TestMethod]
        public void updateWorkItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = CreateServer();            

            // Act
            for (int i = 0; i < 20; i++)
            {
                unitUnderTest.updateWorkItem(
                    i);
            }
            int result = unitUnderTest.getIdleCount();
            
            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
