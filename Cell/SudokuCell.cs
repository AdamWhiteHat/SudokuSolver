/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 *    http://arakaska.wix.com/intelligentsoftware
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using SudokuSolver;

namespace SudokuGame
{
//	public interface ISudokuCell
//	{
//		int Value { get; set; }
//		bool IsClue { get; }
//		int Row { get; }
//		int Block { get; }
//		int Column { get; }
//		SortedSet<int> Candidates { get; set; }
//		SudokuGridPosition GridPosition  { get; }
//		//int Answer { get; }
//		//bool IsCorrect();
//		int RemoveCandidate(int Candidate);
//		int RemoveCandidates(List<int> candidates);
//		// Events
//		event EventHandler CellChangedEvent;
//	}
	
//	public abstract class BaseSudokuCell
//	{
//		public delegate void SudokuCellValueSet(SudokuGridPosition position, int[] values);
//		public static event SudokuCellValueSet OnValueSetEvent;
//
//		public delegate void SudokuCellCandidateChanged(SudokuGridPosition position, int[] values);
//		public static event SudokuCellCandidateChanged OnCandidateRemovedEvent;
//
//		public delegate void SudokuCellOneCandidateLeft(SudokuGridPosition position);
//		public static event SudokuCellOneCandidateLeft OnOneCandidateLeftEvent;
//	}


	public partial class SudokuCell : FlowLayoutPanel
	{
		public bool AutoSolve = true;
		public SortedSet<int> Candidates { get; set; }
		public SudokuGridPosition GridPosition { get; private set; }
		public int Row { get { return GridPosition.Row; } }
		public int Block { get { return GridPosition.Block; } }
		public int Column { get { return GridPosition.Column; } }
		public Color DefaultBackColor { get; set; }
		public Color DefaultErrorColor { get; set; }

		public delegate void CellValueChangedHandler(SudokuCell Source, int NewValue);
		public event CellValueChangedHandler ValueChanged;

		bool? _isClue = null;
		public bool IsClue
		{
			get { return (bool)_isClue; }
			set
			{
				if (_isClue == null) { _isClue = value; }
			}
		}

		int _value;
		public int Value
		{
			get { return this._value; }
			set
			{
				this._value = value;
				this.OnValueChanged(this, value);
				// if(value != 0)	Candidates = new SortedSet<int>();
				// else				Candidates = StaticSudoku.GetAllPossibleCandidates();
			}
		}

		public SudokuCell(int Column, int Row, int Value)
		{
			this.Visible = false;
			DefaultBackColor = this.BackColor;
			DefaultErrorColor = Color.MistyRose;
			this.DoubleBuffered = true;
			GridPosition = new SudokuGridPosition(Column,Row);
			this.Value = Value;

			IsClue = (Value != 0);
						
			if(IsClue)
			{	
				// Empty. We dont need candidates because this cell already knows its value.
				Candidates = new SortedSet<int>();
			}
			else
			{	// All possible numbers, 1 thru 9
				Candidates = new SortedSet<int>(Enumerable.Range(1, StaticSudoku.Dimension) );
			}
			
			InitializeView();
			this.Visible = true;
		}

		public void OnValueChanged(SudokuCell Source, int NewValue)
		{
			if (ValueChanged != null)
			{
				ValueChanged.Invoke(Source, NewValue);
			}
		}
		
		/// <summary>
		/// Removes a candidate from the cells list of possible candidate values.
		/// </summary>
		/// <param name="Value"></param>
		/// <returns>The number of candidates removed from the cell's list.</returns>
		public int RemoveCandidate(int Candidate)
		{
			if(IsClue || Candidates.Count==0) return 0;
			bool removed = Candidates.Remove(Candidate);
			//CheckForNakedSingle();
			return removed ? 1 : 0;
		}
		
		public int RemoveCandidates(List<int> candidates)
		{
			if(this.Candidates.Count==0 && this.Value == 0)
			{
				throw new WarningException("No possibilities left and Value is still not set. This cell is unsolvable; there is a bug in the program.");
			}
			
			if(IsClue || this.Candidates.Count==0) return 0;
			
			int countBefore = this.Candidates.Count;
			this.Candidates = new SortedSet<int>( this.Candidates.Except(candidates) );
			
			//CheckForNakedSingle();
			return countBefore - this.Candidates.Count;
		}
		
		bool CheckForNakedSingle()
		{
			if(AutoSolve)
			{
				if(this.Candidates.Count==1)
				{
					Value = this.Candidates.First();
					RemoveCandidate(Value);
					PaintCell();
					return true;
				}				
			}
			return false;
		}
	}
}
