/*
 * Created by SharpDevelop.
 * User: asmo
 * Date: 11/24/2018
 * Time: 10:30 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace QueueModelling
{
	public class Gaussian
	{
	    private bool _available;
	    private double _nextGauss;
	    private static Random _rng;
	
	    /// <summary>
	    /// Initialize the random gaussian distribution. The seed is static and shared among all instances of this class.
	    /// </summary>
	    public Gaussian()
	    {
	     	if (_rng == null)
	    	{
	        	_rng = new Random();
	    	}
	    }
	
	    // <summary>
	    /// Generates a Random Variable using a normal distribution assuming a sigma of 1 and mu of 0.
	    /// </summary>
	    /// <returns>A value centered around 0 for a normal distribution</returns>
	    public double RandomGauss()
	    {
	       if (_available)
	       {
	           _available = false;
	           return _nextGauss;
	       }
	
	       double u1 = _rng.NextDouble();
	       double u2 = _rng.NextDouble();
	       double temp1 = Math.Sqrt(-2.0*Math.Log(u1));
	       double temp2 = 2.0*Math.PI*u2;
	
	       _nextGauss = temp1 * Math.Sin(temp2);
	       _available = true;
	       return temp1*Math.Cos(temp2);
	    }
	
	    /// <summary>
	    /// Give a random value with a given mu and sigma
	    /// </summary>
	    /// <param name="mu">the mean of the distribution</param>
	    /// <param name="sigma">the standard deviation fo the distribution</param>
	    /// <returns>Random value generated using the distribution</returns>
	    public double RandomGauss(double mu, double sigma)
	    {
	        return mu + sigma*RandomGauss();
	    }
	
	    /// <summary>
	    /// Generate a random variable and scale it by the given sigma.
	    /// </summary>
	    /// <param name="sigma">The standard deviation of the distribution</param>
	    /// <returns>distance from the mean for the distribution</returns>
	    public double RandomGauss(double sigma)
	    {
	        return sigma*RandomGauss();
	    }
	}
}
