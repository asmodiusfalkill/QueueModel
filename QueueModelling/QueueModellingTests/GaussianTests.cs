using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QueueModelling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueModellingTests
{
    [TestClass]
    public class GaussianTests
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

        private Gaussian CreateGaussian()
        {
            return new Gaussian();
        }

        [TestMethod]
        public void RandomGauss_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = CreateGaussian();
            double thresholdPercent = .2;
            int sampleCount = 1000;

            thresholdPercent = .05;
            sampleCount = 1000000;
            distroTest(thresholdPercent, sampleCount, unitUnderTest.RandomGauss);
        }

        [TestMethod]
        public void RandomGauss_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var unitUnderTest = CreateGaussian();
            double mu = 5;
            double sigma = .1;
            double thresholdPercent = .2;
            int sampleCount = 1000;


            for (int i = 1; i < 100; i++)
            {
                mu = i;
                for (double j = .1; j < 2.0; j = j + .1)
                {
                    sigma = j;
                    distroTest(mu, sigma, thresholdPercent, sampleCount, unitUnderTest.RandomGauss);
                }
            }

            mu = 5;
            sigma = .1;
            thresholdPercent = .05;
            sampleCount = 1000000;
            distroTest(mu, sigma, thresholdPercent, sampleCount, unitUnderTest.RandomGauss);

            mu = 17;
            sigma = 3.8;
            thresholdPercent = .05;
            sampleCount = 1000000;
            distroTest(mu, sigma, thresholdPercent, sampleCount, unitUnderTest.RandomGauss);
        }

        [TestMethod]
        public void RandomGauss_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var unitUnderTest = CreateGaussian();
            double sigma = .1;
            double thresholdPercent = .2;
            int sampleCount = 1000;


            for (double j = .1; j < 2.0; j = j + .1)
            {
                sigma = j;
                distroTest(sigma, thresholdPercent, sampleCount, unitUnderTest.RandomGauss);
            }

            sigma = .1;
            thresholdPercent = .05;
            sampleCount = 1000000;
            distroTest(sigma, thresholdPercent, sampleCount, unitUnderTest.RandomGauss);
                        
            sigma = 3.8;
            thresholdPercent = .05;
            sampleCount = 1000000;
            distroTest(sigma, thresholdPercent, sampleCount, unitUnderTest.RandomGauss);
        }

        public delegate double MyFunction(double x, double y);
        public delegate double MyFunction1(double x);
        public delegate double MyFunction2();

        /// <summary>
        /// Gethers a list of items at a desired sample size from a desired distribution. It then 
        /// Calculates the average and standard deviation and compares them against he requested distribution 
        /// using the threshold percent for upper and lower limits. 
        /// </summary>
        /// <param name="avgToTest">Desired average to test</param>
        /// <param name="stdevToTest">Desired Standard deviation to test</param>
        /// <param name="thresholdPercent">The upper and lower bounds by precent test</param>
        /// <param name="sampleCount">Number of samples to generate for the test.</param>
        private void distroTest(double avgToTest, double stdevToTest, double thresholdPercent, int sampleCount, MyFunction testFunction)
        {
            List<double> testList = new List<double>();

            //Run lots of samples.
            for (int i = 0; i < 1000; i++)
            {
                var unitUnderTest = testFunction(avgToTest, stdevToTest);
                testList.Add(unitUnderTest);
            }
            double avg = testList.Average();
            double stdDev = Math.Sqrt(testList.Average(v => Math.Pow(v - avg, 2)));
            // Assert
            Assert.IsTrue(((avgToTest * (1 - thresholdPercent)) < avg) && (avg < (avgToTest * (1 + thresholdPercent))));
            Assert.IsTrue(((stdevToTest * (1 - thresholdPercent)) < stdDev) && (stdDev < (stdevToTest * (1 + thresholdPercent))));
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
        private void distroTest(double stdevToTest, double thresholdPercent, int sampleCount, MyFunction1 testFunction)
        {
            List<double> testList = new List<double>();

            //Run lots of samples.
            for (int i = 0; i < 1000; i++)
            {
                var unitUnderTest = testFunction(stdevToTest);
                testList.Add(unitUnderTest);
            }
            double avg = testList.Average();
            double stdDev = Math.Sqrt(testList.Average(v => Math.Pow(v - avg, 2)));
            // Assert
            Assert.IsTrue((-.2 < avg) && (avg < .2));
            Assert.IsTrue(((stdevToTest * (1 - thresholdPercent)) < stdDev) && (stdDev < (stdevToTest * (1 + thresholdPercent))));
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
        private void distroTest(double thresholdPercent, int sampleCount, MyFunction2 testFunction)
        {
            List<double> testList = new List<double>();
            double stdevToTest = 1;

            //Run lots of samples.
            for (int i = 0; i < 1000; i++)
            {
                var unitUnderTest = testFunction();
                testList.Add(unitUnderTest);
            }
            double avg = testList.Average();
            double stdDev = Math.Sqrt(testList.Average(v => Math.Pow(v - avg, 2)));
            // Assert
            Assert.IsTrue((-.2 < avg) && (avg < .2));
            Assert.IsTrue(((stdevToTest * (1 - thresholdPercent)) < stdDev) && (stdDev < (stdevToTest * (1 + thresholdPercent))));
        }
    }
}
