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
	/// Description of WorkItem.
	/// </summary>
	public class WorkItem
	{
		private double avgWT;
		private double stdevWT;	
		private double requiredWork;
		public int enQTime;
		public int deQTime;
		public int addTime;
		
		public WorkItem(double avgWorkTime, double standardDevWorkTime)
		{
			avgWT = avgWorkTime;
			stdevWT = standardDevWorkTime;
			enQTime = 0;
			deQTime = 0;
			Gaussian randomizer = new Gaussian();
			requiredWork = randomizer.RandomGauss(avgWT, stdevWT);
		}
		
		public double getAvg()
		{
			return avgWT;
		}
		
		public double getStdev()
		{
			return stdevWT;
		}
		
		public double getCurrentRequiredAmount()
		{
			return requiredWork;
		}
	}
}
