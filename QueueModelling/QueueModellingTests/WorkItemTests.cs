using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueModelling;
using System;
using System.Linq;
using System.Collections.Generic;

namespace QueueModellingTests
{
    [TestClass]
    public class WorkItemTests
    {
        [TestInitialize]
        public void TestInitialize()
        {



        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        private WorkItem CreateWorkItem()
        {
            return new WorkItem(
                5,
                .1);
        }

        private WorkItem CreateWorkItem(double average, double stdDev)
        {
            return new WorkItem(
                average,
                stdDev);
        }

        [TestMethod]
        public void getAvg_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = CreateWorkItem();

            // Act
            var result = unitUnderTest.getAvg();

            // Assert
            Assert.AreEqual(5, result);

            for (int i = 0; i < 100; i++)
            {
                // Arrange
                unitUnderTest = CreateWorkItem(i, .1);

                // Act
                result = unitUnderTest.getAvg();

                // Assert
                Assert.AreEqual(i, result);
            }
        }

        [TestMethod]
        public void getStdev_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = CreateWorkItem();

            // Act
            var result = unitUnderTest.getStdev();

            // Assert
            Assert.AreEqual(.1, result);

            for (int i = 0; i < 100; i++)
            {
                // Arrange
                unitUnderTest = CreateWorkItem(5, i);

                // Act
                result = unitUnderTest.getStdev();

                // Assert
                Assert.AreEqual(i, result);
            }
        }

        /// <summary>
        /// Tests the required work amount. It tests many different samples and compares them
        /// against the requested distribution to ensure they desired distribution and the requested
        /// distribution match. The exhaustive check is with 1000 samples and a anything within 20% is good enough. 
        /// There is a detail check against much more preceise and larger sample. 
        /// </summary>
        [TestMethod]
        public void getCurrentRequiredAmount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            double avgToTest = 5;
            double stdevToTest = .1;
            double threholdPrecent = .2;
            int sampleCount = 1000;

            for (int i = 1; i < 100; i++)
            {
                avgToTest = i;
                for (double j = .1; j < 2.0; j = j + .1)
                {
                    stdevToTest = j;
                    distroTest(avgToTest, stdevToTest, threholdPrecent, sampleCount);
                }
            }

            avgToTest = 5;
            stdevToTest = .1;
            threholdPrecent = .05;
            sampleCount = 1000000;
            distroTest(avgToTest, stdevToTest, threholdPrecent, sampleCount);

            avgToTest = 17;
            stdevToTest = 3.8;
            threholdPrecent = .05;
            sampleCount = 1000000;
            distroTest(avgToTest, stdevToTest, threholdPrecent, sampleCount);
        }

        /// <summary>
        /// Gethers a list of items at a desired sample size from a desired distribution. It then 
        /// Calculates the average and standard deviation and compares them against he requested distribution 
        /// using the threshold percent for upper and lower limits. 
        /// </summary>
        /// <param name="avgToTest">Desired average to test</param>
        /// <param name="stdevToTest">Desired Standard deviation to test</param>
        /// <param name="thresholdPercent">The upper and lower bounds by precent test</param>
        /// <param name="sampleCount">Number of samples to generate for the test.</param>
        private void distroTest(double avgToTest, double stdevToTest, double thresholdPercent, int sampleCount)
        {
            List<double> testList = new List<double>();

            //Run lots of samples.
            for (int i = 0; i < 1000; i++)
            {
                var unitUnderTest = CreateWorkItem(avgToTest, stdevToTest);
                testList.Add(unitUnderTest.getCurrentRequiredAmount());
            }
            double avg = testList.Average();
            double stdDev = Math.Sqrt(testList.Average(v => Math.Pow(v - avg, 2)));
            // Assert
            Assert.IsTrue(((avgToTest * (1 - thresholdPercent)) < avg) && (avg < (avgToTest * (1 + thresholdPercent))));
            Assert.IsTrue(((stdevToTest * (1- thresholdPercent)) < stdDev) && (stdDev < (stdevToTest * (1 + thresholdPercent))));
        }
    }
}
