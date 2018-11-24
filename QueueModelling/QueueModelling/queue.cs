/*
 * Created by SharpDevelop.
 * User: asmo
 * Date: 11/24/2018
 * Time: 7:34 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace QueueModelling
{
	/// <summary>
	/// Description of queue.
	/// </summary>
	public class queue
	{
		private Queue<WorkItem> currentQueue;
		
		public queue()
		{
			currentQueue = new Queue<WorkItem>();
		}
		
		public void enqueue(WorkItem item, int currentTime)
		{
			item.enQTime = currentTime;
			currentQueue.Enqueue(item);
		}
		
		public WorkItem dequeue(int currentTime)
		{
			WorkItem returnItem = null;
			if (currentQueue.Count > 0)
			{
				returnItem = currentQueue.Dequeue();
				returnItem.deQTime = currentTime;
			}			
			return returnItem;
		}
		
		public int getCount()
		{
			return currentQueue.Count;
		}
	}
}
