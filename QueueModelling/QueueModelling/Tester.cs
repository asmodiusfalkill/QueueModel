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
	/// Description of Tester.
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
		
		public double getQAvgLength()
		{
			double total = 0;
			foreach (int count in qLengths)
			{
				total = total + count;
			}
			return total/(double)qLengths.Count;
		}
		
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
		
		public double getCmpCount()			
		{
			return (double)QResults.Count;
		}
		
		public double getWaitAvg()			
		{
			double total = 0;
			foreach (Stats infoItem in QResults)
			{
				total = total + infoItem.QWaitTime;
			}
			return total/(double)QResults.Count;
		}
		
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
		
		public double getIdlePercent()
		{
			return (double)worker.getIdleCount()/(double)testDuration;
		}
		
		public double getAvgWorkTime()
		{
			double total = 0;
			foreach (Stats infoItem in QResults)
			{
				total = total + infoItem.workTime;
			}
			return total/(double)QResults.Count;
		}
		
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
		
		public double getAvgLeadTime()
		{
			double total = 0;
			foreach (Stats infoItem in QResults)
			{
				total = total + infoItem.timeInSystem;
			}
			return total/(double)QResults.Count;
		}
		
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
		
		public double getAvgTouchTimePercent()
		{
			double total = 0;
			foreach (Stats infoItem in QResults)
			{
				total = total + infoItem.touchTimeRatio;
			}
			return total/(double)QResults.Count;
		}
		
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
