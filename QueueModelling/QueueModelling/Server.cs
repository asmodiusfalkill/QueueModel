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
	/// Description of Server.
	/// </summary>
	public class Server
	{
		private queue inQ;
		private List<Stats> statsQ;
		private WorkItem currentWorkItem;
		private int workTime;
		private int startTime;
		private int idleCount;
		public Server(queue inputQueue, List<Stats> statsQueue)
		{
			inQ = inputQueue;
			statsQ = statsQueue;
			workTime = 0;
			idleCount = 0;
			startTime = 0;
		}
		
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
		
		public int getIdleCount()
		{
			return idleCount;
		}
	}
}
