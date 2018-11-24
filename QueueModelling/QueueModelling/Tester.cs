/*
 * Created by SharpDevelop.
 * User: asmo
 * Date: 11/24/2018
 * Time: 8:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace QueueModelling
{
	/// <summary>
	/// Controls a test run by putting data into the input queue and calculating interesting stats at the end for reporting. 
	/// </summary>
	public class Tester
	{
		private Server worker;
		private queue inputQueue;
		private List<Stats> QResults;
		private int testDuration;
		private double workItemRate;
		private double workItemStdev;
		private List<int> qLengths;
		private List<WorkItem> workToDo;
		
		/// <summary>
		/// Init the tester with work time, deviation, inject rate, inject divation, and test duration.
		/// </summary>
		/// <param name="duration">total ticks for the model to run.</param>
		/// <param name="injectRate">Rate at which new work will be injected.</param>
		/// <param name="injectDeviation">Standard deviation for work injection.</param>
		/// <param name="avgWorkTime">Average time to do work items.</param>
		/// <param name="stdevWorkTime">Standard Deviation of work time.</param>
		public Tester(int duration, double injectRate, double injectDeviation, double avgWorkTime, double stdevWorkTime)
		{	
			inputQueue = new queue();
			QResults = new List<Stats>();
			worker = new Server(inputQueue, QResults);
			testDuration = duration;
			workItemRate = injectRate;
			workItemStdev = injectDeviation;
			qLengths = new List<int>();
			workToDo = new List<WorkItem>();
			generateWorkItemList(testDuration, avgWorkTime, stdevWorkTime);
		}
		
		/// <summary>
		/// Setup initial work item list. 
		/// </summary>
		/// <param name="itemCount">Amount of work items to do.</param>
		/// <param name="avgWT">Average work time in ticks.</param>
		/// <param name="stdvWT">Standard Deviation of work time in ticks.</param>
		private void generateWorkItemList(int itemCount, double avgWT, double stdvWT)
		{
			Gaussian randomizer = new Gaussian();
			int randomInjectTime = 0;			
			for (int i = 0; i < itemCount; i++)
			{
				randomInjectTime = (int)Math.Round(randomizer.RandomGauss(workItemRate, workItemStdev));
				WorkItem newItem = new WorkItem(avgWT, stdvWT);
				if (workToDo.Count == 0)
				{
					newItem.addTime = 0;
				}
				else
				{
					newItem.addTime = workToDo[workToDo.Count - 1].addTime + randomInjectTime;
				}
				workToDo.Add(newItem);
			}
		}
		
		/// <summary>
		/// Executes the test with the generated work item set.
		/// </summary>
		public void executeTest()
		{
			int workItemIterator = 0;
			for(int i = 0; i < testDuration; i++)
			{				
				if (i == workToDo[workItemIterator].addTime)
				{
					inputQueue.enqueue(workToDo[workItemIterator], i);
					workItemIterator++;
				}
				worker.updateWorkItem(i);
				qLengths.Add(inputQueue.getCount());
			}				
		}
		
		/// <summary>
		/// Compute the average Q length for the sim.
		/// </summary>
		/// <returns>Average Q Length</returns>
		public double getQAvgLength()
		{
			double total = 0;
			foreach (int count in qLengths)
			{
				total = total + count;
			}
			return total/(double)qLengths.Count;
		}
		
		/// <summary>
		/// Compute the Q highest enqued count.
		/// </summary>
		/// <returns>High water mark</returns>
		public double getQHighWaterMark()
		{
			double highest = 0;
			foreach (int count in qLengths)
			{
				if (count > highest)
				{
					highest = count;
				}
			}
			return highest;
		}
		
		/// <summary>
		/// Calculate the Q lowest enqued count.
		/// </summary>
		/// <returns>Low Water Mark</returns>
		public double getQLowWaterMark()
		{
			double Lowest = testDuration;
			foreach (int count in qLengths)
			{
				if (count < Lowest)
				{
					Lowest = count;
				}
			}
			return Lowest;
		}
		
		/// <summary>
		/// Calculate the total completed count.
		/// </summary>
		/// <returns>The total number of completed items.</returns>
		public double getCmpCount()			
		{
			return (double)QResults.Count;
		}
		
		/// <summary>
		/// Calucate the average work item wait time.
		/// </summary>
		/// <returns>average wait time.</returns>
		public double getWaitAvg()			
		{
			double total = 0;
			foreach (Stats infoItem in QResults)
			{
				total = total + infoItem.QWaitTime;
			}
			return total/(double)QResults.Count;
		}
		
		/// <summary>
		/// Calculate the min wait time for work items.
		/// </summary>
		/// <returns>min wait time encountered during the sim.</returns>
		public double getWaitMin()			
		{
			double Lowest = testDuration;
			foreach (Stats infoItem in QResults)
			{
				if (infoItem.QWaitTime < Lowest)
				{
					Lowest = infoItem.QWaitTime;
				}
			}
			return Lowest;
		}
		
		/// <summary>
		/// Calculate the Max Wait time for work items in the sim.
		/// </summary>
		/// <returns>Max wait time encountered during the sim.</returns>
		public double getWaitMax()			
		{
			double highest = 0;
			foreach (Stats infoItem in QResults)
			{
				if (infoItem.QWaitTime > highest)
				{
					highest = infoItem.QWaitTime;
				}
			}
			return highest;
		}
		
		/// <summary>
		/// Calculate the worker average idle %
		/// </summary>
		/// <returns>Idle % of the worker</returns>
		public double getIdlePercent()
		{
			return (double)worker.getIdleCount()/(double)testDuration;
		}
		
		/// <summary>
		/// Calculate the Avg Work time for work items in the sim.
		/// </summary>
		/// <returns>Avg Work time encountered during the sim.</returns>
		public double getAvgWorkTime()
		{
			double total = 0;
			foreach (Stats infoItem in QResults)
			{
				total = total + infoItem.workTime;
			}
			return total/(double)QResults.Count;
		}
		
		/// <summary>
		/// Calculate the min work time for work items.
		/// </summary>
		/// <returns>min work time encountered during the sim.</returns>
		public double getMinWorkTime()
		{
			double Lowest = testDuration;
			foreach (Stats infoItem in QResults)
			{
				if (infoItem.workTime < Lowest)
				{
					Lowest = infoItem.workTime;
				}
			}
			return Lowest;
		}
		
		/// <summary>
		/// Calculate the Max Work time for work items in the sim.
		/// </summary>
		/// <returns>Max Work time encountered during the sim.</returns>
		public double getMaxWorkTime()
		{
			double highest = 0;
			foreach (Stats infoItem in QResults)
			{
				if (infoItem.workTime > highest)
				{
					highest = infoItem.workTime;
				}
			}
			return highest;
		}
		
		/// <summary>
		/// Calculate the Avg Lead time for work items in the sim.
		/// </summary>
		/// <returns>Avg Lead time encountered during the sim.</returns>
		public double getAvgLeadTime()
		{
			double total = 0;
			foreach (Stats infoItem in QResults)
			{
				total = total + infoItem.timeInSystem;
			}
			return total/(double)QResults.Count;
		}
		
		/// <summary>
		/// Calculate the min Lead time for work items.
		/// </summary>
		/// <returns>min Lead time encountered during the sim.</returns>
		public double getMinLeadTime()
		{
			double Lowest = testDuration;
			foreach (Stats infoItem in QResults)
			{
				if (infoItem.timeInSystem < Lowest)
				{
					Lowest = infoItem.timeInSystem;
				}
			}
			return Lowest;
		}
		
		/// <summary>
		/// Calculate the Max Lead time for work items in the sim.
		/// </summary>
		/// <returns>Max Lead time encountered during the sim.</returns>
		public double getMaxLeadTime()
		{
			double highest = 0;
			foreach (Stats infoItem in QResults)
			{
				if (infoItem.timeInSystem > highest)
				{
					highest = infoItem.timeInSystem;
				}
			}
			return highest;
		}
		
		/// <summary>
		/// Calculate the Avg Touch time % for work items in the sim.
		/// </summary>
		/// <returns>Avg Touch time % encountered during the sim.</returns>
		public double getAvgTouchTimePercent()
		{
			double total = 0;
			foreach (Stats infoItem in QResults)
			{
				total = total + infoItem.touchTimeRatio;
			}
			return total/(double)QResults.Count;
		}
		
		/// <summary>
		/// Calculate the min Touch time % for work items.
		/// </summary>
		/// <returns>min Touch time % encountered during the sim.</returns>
		public double getMinTouchTimePercent()
		{
			double Lowest = testDuration;
			foreach (Stats infoItem in QResults)
			{
				if (infoItem.touchTimeRatio < Lowest)
				{
					Lowest = infoItem.touchTimeRatio;
				}
			}
			return Lowest;
		}
		
		/// <summary>
		/// Calculate the Max Touch time % for work items in the sim.
		/// </summary>
		/// <returns>Max Touch time % encountered during the sim.</returns>
		public double getMaxTouchTimePercent()
		{
			double highest = 0;
			foreach (Stats infoItem in QResults)
			{
				if (infoItem.touchTimeRatio > highest)
				{
					highest = infoItem.touchTimeRatio;
				}
			}
			return highest;
		}
	}
}
