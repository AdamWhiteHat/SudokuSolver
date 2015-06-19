/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 *    http://arakaska.wix.com/intelligentsoftware
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SudokuGame
{
	public static class StaticSudoku
	{
		public delegate void DisplayOutputDelegate(string MessageFormat, params object[] Args);

		public static int Root = 3;
		public static readonly int Dimension = 9;
		public static readonly int Size = 81;
		
		public static SortedSet<int> GetAllPossibleCandidates()
		{
			return new SortedSet<int>(Enumerable.Range(1, 10));
		}
		
		public static int GetRandomDigit()
		{
			return StaticRandom.Instance.Next(1, Dimension+1);
		}
		
		public static List<SudokuCell> TranslateBoard(List<int> board)
		{
			if(board.Count != StaticSudoku.Size) { throw new ArgumentException(String.Format("Parameter must have exactly 81 items.",StaticSudoku.Size) ); }
			
			int cellCounter = 0;
			List<SudokuCell> result = new List<SudokuCell>(StaticSudoku.Size);
			
			for(int row=1; row<=StaticSudoku.Dimension; row++)
			{
				for(int col=1; col<=StaticSudoku.Dimension; col++)
				{
					result.Add(new SudokuCell(col, row, board[cellCounter]));
					cellCounter++;
				}
			}
			return result;
		}
		
		public static string ArrayToString<T>(IEnumerable<T> Input, string Delimiter = "")
		{
			if(Input == null)
			{
				return "(null)";
			}
			else if (Input.Count() == 0)
			{
				return "(empty)";
			}
			else
			{
				return Input.Select(v => v.ToString()).Aggregate((a,b) => (string.Concat(a,Delimiter,b)));
			}
		}
	}
	
	public static class TupleExtensionMethods
	{
		// Add 3 tuple to 3 tuple
		public static Tuple<int, int, int> Add(this Tuple<int, int, int> Input, Tuple<int, int, int> Value)
		{
			return new Tuple<int, int, int>(Input.Item1+Value.Item1, Input.Item2+Value.Item2, Input.Item3+Value.Item3);
		}
		
		public static Tuple<int, int, int> Add(this Tuple<int, int, int> Input, int Value1, int Value2, int Value3=1)
		{
			return new Tuple<int, int, int>(Input.Item1+Value1, Input.Item2+Value2, Input.Item3+Value3);
		}
		
		// Add 2 tuple to 3 tuple
		public static Tuple<int, int, int> Add(this Tuple<int, int, int> Input, Tuple<int, int> Value)
		{
			return new Tuple<int, int, int>(Input.Item1+Value.Item1, Input.Item2+Value.Item2, Input.Item3);
		}
	}
}