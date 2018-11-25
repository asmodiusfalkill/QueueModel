using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QueueModelling;
using System;

namespace QueueModellingTests
{
    [TestClass]
    public class queueTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        private queue Createqueue()
        {
            return new queue();
        }

        [TestMethod]
        public void enqueue_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = Createqueue();
            WorkItem item = new WorkItem(5, .1, 6);
            int currentTime = 0;

            // Act
            for (int i = 0; i < 10; i++)
            {
                unitUnderTest.enqueue(
                    item,
                    currentTime);
                int count = unitUnderTest.getCount();
                Assert.AreEqual(i + 1, count);
            }
        }

        [TestMethod]
        public void dequeue_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = Createqueue();
            int currentTime = 12;

            // Act
            var result = unitUnderTest.dequeue(
                currentTime);

            // Assert
            Assert.AreEqual(0, unitUnderTest.getCount());
            Assert.AreEqual(null, result);

            // Arrange
            unitUnderTest = Createqueue();
            WorkItem item = new WorkItem(5, .1, 6);
            currentTime = 12;

            // Act
            unitUnderTest.enqueue(item, currentTime);
            Assert.AreEqual(1, unitUnderTest.getCount());
            result = unitUnderTest.dequeue(
                currentTime);

            // Assert            
            Assert.AreEqual(0, unitUnderTest.getCount());
            Assert.AreEqual(currentTime, result.enQTime);
            Assert.AreEqual(currentTime, result.deQTime);

            result = unitUnderTest.dequeue(
                currentTime);

            // Assert
            Assert.AreEqual(0, unitUnderTest.getCount());
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void getCount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = Createqueue();
            WorkItem item = new WorkItem(5, .1, 6);
            int currentTime = 12;
            int result;

            // Act
            for (int i = 0; i < 100; i++)
            {
                unitUnderTest.enqueue(item, currentTime);
                result = unitUnderTest.getCount();
                Assert.AreEqual(i + 1, result);
            }

            for (int i = 0; i < 100; i++)
            {
                unitUnderTest.dequeue(currentTime);
                result = unitUnderTest.getCount();
                Assert.AreEqual(100 - (i + 1), result);
            }
        }
    }
}
