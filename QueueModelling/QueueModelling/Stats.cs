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
		
		public int QWaitTime
		{
			get
			{
				return endQTime - startQTime;
			}
		}
		
		public int workTime
		{
			get
			{
				return endWork - startWork + 1;
			}
		}
		
		public int timeInSystem
		{
			get
			{
				return workTime + QWaitTime;
			}
		}		
		
		public double touchTimeRatio
		{
			get 
			{				
				return (double)workTime/(double)timeInSystem;
			}
		}
		
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
