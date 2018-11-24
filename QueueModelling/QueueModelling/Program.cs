/*
 * Created by SharpDevelop.
 * User: asmo
 * Date: 11/24/2018
 * Time: 7:30 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace QueueModelling
{
    /// <summary>
    /// Class for execute the top level function. 
    /// </summary>
	class Program
	{
		/// <summary>
		/// Execute the queue model. It sets it up and then reports the results to the command line.
		/// </summary>
		/// <param name="args">Currently ignored</param>
		public static void Main(string[] args)
		{
			Console.WriteLine("Running Simulation");
			
			Tester simRun = new Tester(100, 6.8, 1.9, 11.2, 6.7);
			simRun.executeTest();
			Console.WriteLine("Q Average Capacity: " + simRun.getQAvgLength().ToString());
			Console.WriteLine("Q Low Water Mark: " + simRun.getQLowWaterMark().ToString());
			Console.WriteLine("Q High Water Mark: " + simRun.getQHighWaterMark().ToString());
			Console.WriteLine("Work Item Completion Count: " + simRun.getCmpCount().ToString());
			Console.WriteLine("Work Item Avg Wait: " + simRun.getWaitAvg().ToString());
			Console.WriteLine("Work Item Min Wait: " + simRun.getWaitMin().ToString());
			Console.WriteLine("Work Item Max Wait: " + simRun.getWaitMax().ToString());
			Console.WriteLine("Worker Idle %: " + simRun.getIdlePercent().ToString());
			Console.WriteLine("Worker Utilization %: " + (1 - simRun.getIdlePercent()).ToString());
			
			Console.WriteLine("Work Item Avg Work Time: " + simRun.getAvgWorkTime().ToString());
			Console.WriteLine("Work Item Min Work Time: " + simRun.getMinWorkTime().ToString());
			Console.WriteLine("Work Item Max Work Time: " + simRun.getMaxWorkTime().ToString());
			
			Console.WriteLine("Work Item Avg Lead Time: " + simRun.getAvgLeadTime().ToString());
			Console.WriteLine("Work Item Min Lead Time: " + simRun.getMinLeadTime().ToString());
			Console.WriteLine("Work Item Max Lead Time: " + simRun.getMaxLeadTime().ToString());
			
			Console.WriteLine("Work Item Avg Touch Time %: " + simRun.getAvgTouchTimePercent().ToString());
			Console.WriteLine("Work Item Min Touch Time %: " + simRun.getMinTouchTimePercent().ToString());
			Console.WriteLine("Work Item Max Touch Time %: " + simRun.getMaxTouchTimePercent().ToString());
			                  
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}