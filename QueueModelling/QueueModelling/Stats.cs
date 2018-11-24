/*
 * Created by SharpDevelop.
 * User: asmo
 * Date: 11/24/2018
 * Time: 9:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace QueueModelling
{
	/// <summary>
	/// Description of Stats.
	/// </summary>
	public class Stats
	{
		public int currentTime;
		public int startQTime;
		public int endQTime;
		public int startWork;
		public int endWork;
		
		/// <summary>
		/// Q wait time item stayed in the queue.
		/// </summary>
		public int QWaitTime
		{
			get
			{
				return endQTime - startQTime;
			}
		}
		
		/// <summary>
		/// Total amount of time the item was in work.
		/// </summary>
		public int workTime
		{
			get
			{
				return endWork - startWork + 1;
			}
		}
		
		/// <summary>
		/// Total amount of time the item was in the system.
		/// </summary>
		public int timeInSystem
		{
			get
			{
				return workTime + QWaitTime;
			}
		}		
		
		/// <summary>
		/// Ratio of work time to total time in system.
		/// </summary>
		public double touchTimeRatio
		{
			get 
			{				
				return (double)workTime/(double)timeInSystem;
			}
		}
		
		/// <summary>
		/// Init all items to a 0.
		/// </summary>
		public Stats()
		{
			currentTime = 0;
			startQTime = 0;
			endQTime = 0;
			startWork = 0;
			endWork = 0;
		}
	}
}
