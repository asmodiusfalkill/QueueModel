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
	
	     public Gaussian()
	     {
	     	if (_rng == null)
	     	{
	        	_rng = new Random();
	     	}
	     }
	
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
	
	    public double RandomGauss(double mu, double sigma)
	    {
	        return mu + sigma*RandomGauss();
	    }
	
	    public double RandomGauss(double sigma)
	    {
	        return sigma*RandomGauss();
	    }
	}
}
