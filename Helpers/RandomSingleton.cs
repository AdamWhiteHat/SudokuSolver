/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 *    http://arakaska.wix.com/intelligentsoftware
 * 
 */
using System;
using System.Threading;

namespace SudokuGame
{
	public static class StaticRandom
	{
		public static Random Instance { get { return _thread.Value; } }
		static int seed;
		static ThreadLocal<Random> _thread = new ThreadLocal<Random>( () => new Random(Interlocked.Increment(ref seed)) );
		static StaticRandom() { seed = Environment.TickCount; }		
//		public static int Next(int minValue, int maxValue)
//		{
//			return Instance.Next(minValue, maxValue+1);
//		}
//		
//		public static int Next(int maxValue)
//		{
//			return Instance.Next(maxValue)+1;
//		}
	}
}
