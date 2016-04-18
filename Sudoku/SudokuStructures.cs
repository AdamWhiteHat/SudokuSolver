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

namespace SudokuGame
{		
	public class SudokuGridPosition
	{
		int _column;
		int _row;
		int _block;
		
		public int Column
		{
			get { return _column; }
			set { _column = CheckValue(value, "Column"); }
		}

		public int Row
		{
			get { return _row; }
			set { _row = CheckValue(value, "Row"); }
		}
		
		public int Block
		{
			get { return _block; }
			set { _block = CheckValue(value, "Block"); }
		}

		public SudokuGridPosition(int Column, int Row)
		{
			this.Column = Column;
			this.Row = Row;
			this.Block = GetBlock();
		}
		
		public static int MinValue = 1;
		public static int MaxValue = StaticSudoku.Dimension;
		
		int CheckValue(int value, string Name="Value")
		{
			if(value < MinValue)		throw new System.ArgumentException( string.Format("{0} cannot be less than {1}.", Name, MinValue) );
			if(value > MaxValue)		throw new System.ArgumentException( string.Format("{0} cannot be greater than {1}.", Name, MaxValue) );
			
			return value;
		}
		
		int GetBlock()
		{
			int result = 1;
			
			if 	 (Column < 4)	result += (int)HorizontalLocation.Left;
			else if(Column < 7)	result += (int)HorizontalLocation.Center;
			else						result += (int)HorizontalLocation.Right;
			
			if 	 (Row < 4)	result += StaticSudoku.Root * (int)VerticalLocation.Top;
			else if(Row < 7)	result += StaticSudoku.Root * (int)VerticalLocation.Middle;
			else					result += StaticSudoku.Root * (int)VerticalLocation.Bottom;
			
			return result;
		}
		
		public override string ToString()
		{
			return string.Format("C{0}R{1}B{2}", _column, _row, _block);
		}
		
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			SudokuGridPosition other = obj as SudokuGridPosition;
			if (other == null)
			{
				return false;
			}
			
			if(this._column == other._column)
			{
				if(this._row == other._row)
				{
					if(this._block == other._block)
					{
						return true;
					}
				}
			}
			
			return false;
		}
		
		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked
			{
				hashCode += 11 * _column.GetHashCode();
				hashCode += 23 * _row.GetHashCode();
				hashCode += 47 * _block.GetHashCode();
			}
			return hashCode;
		}
		
		public static bool operator ==(SudokuGridPosition lhs, SudokuGridPosition rhs)
		{
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}
		
		public static bool operator !=(SudokuGridPosition lhs, SudokuGridPosition rhs)
		{
			return !(lhs == rhs);
		}
		#endregion

	}
}


