/*
 * Created by SharpDevelop.
 * User: asmo
 * Date: 11/24/2018
 * Time: 7:46 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace QueueModelling
{
	/// <summary>
	/// An object that abstracts a work center or worker in a queue system. 
	/// </summary>
	public class Server
	{
		private queue inQ;
		private List<Stats> statsQ;
		private WorkItem currentWorkItem;
		private int workTime;
		private int startTime;
		private int idleCount;
		
		/// <summary>
		/// Setup a server or work to take items from the Q and work on them.
		/// </summary>
		/// <param name="inputQueue">queue to take work from.</param>
		/// <param name="statsList">List of stats for finished items.</param>
		public Server(queue inputQueue, List<Stats> statsList)
		{
			inQ = inputQueue;
			statsQ = statsList;
			workTime = 0;
			idleCount = 0;
			startTime = 0;
		}
		
		/// <summary>
		/// tell the current server to move forward 1 tick. 
		/// </summary>
		/// <param name="currentTime">current tick value in relation to the 0 tick of time.</param>
		public void updateWorkItem(int currentTime)
		{
			//server doing nothing condition
			if (currentWorkItem == null)
			{
                currentWorkItem = inQ.dequeue(currentTime);

				workTime = 0;
				//no work to do.
				if (currentWorkItem == null)
				{					
					idleCount++;
				}
			}
			//Dowork
			if (currentWorkItem != null)
			{
				if (workTime == 0)
				{
					startTime = currentTime;
				}
				workTime++;
				//workDone
				if (workTime >= currentWorkItem.getCurrentRequiredAmount())
				{
					Stats info = new Stats();
					info.currentTime = currentTime;
					info.startQTime = currentWorkItem.enQTime;
					info.endQTime = currentWorkItem.deQTime;
					info.startWork = startTime;
					info.endWork = currentTime;
					statsQ.Add(info);
					currentWorkItem = null;
				}
			}			
		}
		
		/// <summary>
		/// Get the number of ticks the server was idle.
		/// </summary>
		/// <returns>idle tick count total</returns>
		public int getIdleCount()
		{
			return idleCount;
		}
	}
}
