/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using SudokuSolver;

namespace SudokuGame
{
	public partial class SudokuCell : FlowLayoutPanel
	{
		public CandidatesList Candidates { get; set; }
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

		private void OnValueChanged(SudokuCell Source, int oldValue, int newValue)
		{
			if (ValueChanged != null)
			{
				ValueChanged.Invoke(Source, oldValue, newValue);
			}
		}

		public SudokuCell(int column, int row, int value)
		{
			this.Visible = false;
			Candidates = new CandidatesList();
			DefaultBackgroundColor = this.BackColor;
			DefaultErrorColor = Color.MistyRose;
			DoubleBuffered = true;
			GridPosition = new SudokuGridPosition(column, row);
			this.Value = value;

			IsClue = (value != 0);

			if (IsClue)
			{
				Candidates.Clear(); // Empty. We don't need candidates because this cell already knows its value.
			}
			else
			{
				Candidates.Renew(); // All possible numbers, 1 through 9
			}

			InitializeView();
			this.Visible = true;
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

		public int RemoveCandidates(List<int> candidateValues)
		{
			return this.Candidates.RemoveRange(candidateValues);
		}
	}
}
