/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 *    http://arakaska.wix.com/intelligentsoftware
 * 
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SudokuGame;

namespace MultiKeyDictionaries
{
	/// <summary>
	/// Lictionary is a sorted dictionary with multiple values per key (List).
	/// Key = # of occurrences, Value = List of numbers that occurred that many times.
	/// </summary>
	//[DebuggerDisplay("Value = {Enumerator}")]
	public class RankingDictionary<V>
	{
		public int Count { get { return this._dictionary.Count; } }
		public IEnumerable<int> Keys			{ get { return this._dictionary.Keys;	} }
		public IEnumerable<List<V>> Values	{ get { return this._dictionary.Values;} }
		
		SortedDictionary<int,List<V>> _dictionary = new SortedDictionary<int,List<V>>(new IntegerComparer());
		
		public class IntegerComparer : IComparer<int>
		{
			public int Compare(int one,int two) { if(one == two) return 0; else if(one > two) return -1; else return 1; }
		}

		public void Add(int key, V value)
		{
			List<V> list = new List<V>();
			list.Add(value);
			this._dictionary[key] = list; // List<V> list; if(this._dictionary.TryGetValue(key, out list)) list.Add(value); else this._dictionary[key] = new List<V>().Add(value);
		}
		
		public IEnumerator<KeyValuePair<int,List<V>>> GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}
		
		public override string ToString()
		{	// Key = # of occurrences, Value = List of numbers that occurred that many times.
			StringBuilder str = new StringBuilder();			
			foreach(var kvp in _dictionary)
			{
				str.AppendFormat("[{0}(key) occurrences of value(s): ({1})]", kvp.Key, StaticSudoku.ArrayToString<V>(kvp.Value,","));
			}			
			return str.ToString();
		}

		public List<V> this[int key]
		{
			get
			{
				List<V> list;
				if (this._dictionary.TryGetValue(key,out list))
				{
					return list;
				}
				return new List<V>();
			} // 
		}
	}
	
	// Key == number, Value == # of occurrences
	//[DebuggerDisplay("Value = {Enumerator}")]
	public class FrequencyDictionary<T>
	{
		Dictionary<T, int> _dictionary;
		
		public int Count { get { return this._dictionary.Count; } }
		public IEnumerable<T> Keys { get { return this._dictionary.Keys; } }
		public FrequencyDictionary() { _dictionary = new Dictionary<T, int>(); }
				
		public void AddRange(T[] items)
		{
			foreach(T item in items) { this.Add(item); }
		}

		public void Add(T item)
		{
			if 	( this._dictionary.ContainsKey(item))	{ this._dictionary[item]++; }
			else	{ this._dictionary.Add(item,1); }
		}
		
		public KeyValuePair<T,int> GetMostFrequent()
		{
			int maxValue = this._dictionary.Values.Max();
			return this._dictionary.Where(kvp => kvp.Value == maxValue).FirstOrDefault();
		}
		
		public IEnumerator<KeyValuePair<T,int>> GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		public int this[T Item]
		{
			get { if (this._dictionary.ContainsKey(Item)) { return this._dictionary[Item]; } return 0; }
		}
		
		public override string ToString()
		{	 // Key == number, Value == # of occurrences
			StringBuilder str = new StringBuilder();			
			foreach(var kvp in _dictionary)
			{
				str.AppendFormat("[Key={0} Occurrences={1}]", kvp.Key, kvp.Value );
			}			
			return str.ToString();
		}

		public RankingDictionary<T> GetRankingDictionary()
		{
			RankingDictionary<T> result = new RankingDictionary<T>();
			foreach(KeyValuePair<T,int> kvp in _dictionary)
			{
				result.Add(kvp.Value,kvp.Key);
			}
			return result;
		}
	}
}
