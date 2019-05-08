/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
	public class CandidatesList : BindingList<int>
	{
		public bool Contains1 { get { return this.Contains(1); } set { this.SetProperty(value, 1); } }
		public bool Contains2 { get { return this.Contains(2); } set { this.SetProperty(value, 2); } }
		public bool Contains3 { get { return this.Contains(3); } set { this.SetProperty(value, 3); } }
		public bool Contains4 { get { return this.Contains(4); } set { this.SetProperty(value, 4); } }
		public bool Contains5 { get { return this.Contains(5); } set { this.SetProperty(value, 5); } }
		public bool Contains6 { get { return this.Contains(6); } set { this.SetProperty(value, 6); } }
		public bool Contains7 { get { return this.Contains(7); } set { this.SetProperty(value, 7); } }
		public bool Contains8 { get { return this.Contains(8); } set { this.SetProperty(value, 8); } }
		public bool Contains9 { get { return this.Contains(9); } set { this.SetProperty(value, 8); } }

		public static readonly CandidatesList None;
		public static readonly CandidatesList All;

		static CandidatesList()
		{
			None = new CandidatesList();
			All = new CandidatesList(Enumerable.Range(1, StaticSudoku.Dimension).ToList());
		}

		public CandidatesList()
			: base()
		{ }

		public CandidatesList(IList<int> list)
			: base(list)
		{ }

		private void SetProperty(bool value, int number)
		{
			if (value && !this.Contains(number))
			{
				this.Add(number);
			}
			else if (!value && this.Contains(number))
			{
				this.RemoveItem(number);
			}
		}

		public void Renew()
		{
			AddRange(CandidatesList.All);
		}

		public int AddRange(IList<int> candidatesToAdd)
		{
			if (candidatesToAdd == null && candidatesToAdd.Count < 1)
			{
				return 0;
			}
			int count = 0;
			foreach (int candidate in candidatesToAdd.Where(i => !this.Contains(i)))
			{
				this.Add(candidate);
				count++;
			}
			return count;
		}

		public int RemoveRange(IList<int> candidatesToRemove)
		{
			if (candidatesToRemove == null && candidatesToRemove.Count < 1)
			{
				return 0;
			}
			int count = 0;
			foreach (int candidate in candidatesToRemove.Where(i => this.Contains(i)))
			{
				this.Remove(candidate);
				count++;
			}
			return count;
		}
	}
}
