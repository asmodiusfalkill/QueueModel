/*
 * Created by SharpDevelop.
 * User: asmo
 * Date: 11/24/2018
 * Time: 7:35 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace QueueModelling
{
	/// <summary>
	/// Describes a work item by how long it will take to do, when it was enqueued and dequed, and the deviation and the work time needed to complete this item.
	/// </summary>
	public class WorkItem
	{
		private double avgWT;
		private double stdevWT;	
		private double requiredWork;
		public int enQTime;
		public int deQTime;
		public int addTime;
		
		/// <summary>
		/// Initialize a work item to basis values and with a required work amount.
		/// </summary>
		/// <param name="avgWorkTime"></param>
		/// <param name="standardDevWorkTime"></param>
		public WorkItem(double avgWorkTime, double standardDevWorkTime)
		{
			avgWT = avgWorkTime;
			stdevWT = standardDevWorkTime;
			enQTime = 0;
			deQTime = 0;
			Gaussian randomizer = new Gaussian();
			requiredWork = randomizer.RandomGauss(avgWT, stdevWT);
		}
		
		/// <summary>
		/// Returns previously set work average.
		/// </summary>
		/// <returns>Work item average.</returns>
		public double getAvg()
		{
			return avgWT;
		}
		
		/// <summary>
		/// Gives the standard deviation previouly set.
		/// </summary>
		/// <returns>standard deviation of the work item.</returns>
		public double getStdev()
		{
			return stdevWT;
		}
		
		/// <summary>
		/// Returns previously calculated work required amount.
		/// </summary>
		/// <returns></returns>
		public double getCurrentRequiredAmount()
		{
			return requiredWork;
		}
	}
}
