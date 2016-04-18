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
	public partial class SudokuCell : FlowLayoutPanel
	{
		public SortedSet<int> Candidates { get; set; }
		public SudokuGridPosition GridPosition { get; private set; }
		public int Row { get { return GridPosition.Row; } }
		public int Block { get { return GridPosition.Block; } }
		public int Column { get { return GridPosition.Column; } }
		public Color DefaultBackgroundColor { get; set; }
		public Color DefaultErrorColor { get; set; }

		public delegate void CellValueChangedHandler(SudokuCell Source, int OldValue, int NewValue);
		public event CellValueChangedHandler ValueChanged;

		private bool? _isClue = null;
		public bool IsClue
		{
			get { return (bool)_isClue; }
			set { if (_isClue == null) { _isClue = value; } }
		}

		private int _value;
		public int Value
		{
			get { return this._value; }
			set
			{
				if (this._value != value)
				{
					int oldValue = this._value;
					this._value = value;
					this.OnValueChanged(this, oldValue, value);
				}
			}
		}

		public void OnValueChanged(SudokuCell Source, int oldValue, int newValue)
		{
			if (ValueChanged != null)
			{
				ValueChanged.Invoke(Source, oldValue, newValue);
			}
		}

		public SudokuCell(int column, int row, int value)
		{
			this.Visible = false;
			Candidates = new SortedSet<int>();
			DefaultBackgroundColor = this.BackColor;
			DefaultErrorColor = Color.MistyRose;
			DoubleBuffered = true;
			GridPosition = new SudokuGridPosition(column, row);
			this.Value = value;

			IsClue = (value != 0);

			if (IsClue)
			{
				Candidates = new SortedSet<int>(); // Empty. We don't need candidates because this cell already knows its value.
			}
			else
			{
				ResetCandidates(); // All possible numbers, 1 through 9
			}

			InitializeView();
			this.Visible = true;
		}

		internal void ResetCandidates()
		{
			Candidates = new SortedSet<int>(Enumerable.Range(1, StaticSudoku.Dimension));
		}

		public int RemoveCandidates(List<int> candidates)
		{
			if (Candidates.Count == 0 && Value == 0)
			{
				HighlightError();				
				return 0;
			}

			if (IsClue || Candidates.Count == 0)
			{
				return 0;
			}

			int countBefore = Candidates.Count;
			Candidates = new SortedSet<int>(Candidates.Except(candidates));

			CheckForNakedSingle();
			return countBefore - Candidates.Count;
		}

		internal bool CheckForNakedSingle()
		{
			if (Candidates.Count == 1)
			{
				Value = Candidates.Single();
				Candidates.Clear();
				PaintCell();
				return true;
			}
			return false;
		}
	}
}
