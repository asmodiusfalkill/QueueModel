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
		
		/// <summary>
		/// Create an empty queue.
		/// </summary>
		public queue()
		{
			currentQueue = new Queue<WorkItem>();
		}
		
		/// <summary>
		/// Enqueue a work item.
		/// </summary>
		/// <param name="item">Work item to enqueue</param>
		/// <param name="currentTime">current time iteration in ticks from 0 tick</param>
		public void enqueue(WorkItem item, int currentTime)
		{
			item.enQTime = currentTime;
			currentQueue.Enqueue(item);
		}
		
		/// <summary>
		/// Dequeue a work item.
		/// </summary>
		/// <param name="currentTime">The current time in ticks from 0 tick.</param>
		/// <returns>The work item. It is null if there is nothing on the queue.</returns>
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
		
		/// <summary>
		/// Gives the current count of enqued items.
		/// </summary>
		/// <returns>The current length of the queue</returns>
		public int getCount()
		{
			return currentQueue.Count;
		}
	}
}
