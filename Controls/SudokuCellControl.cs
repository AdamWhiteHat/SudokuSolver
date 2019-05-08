/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EditLabelControl;

namespace SudokuGame.Controls
{
	public partial class SudokuCellControl : UserControl
	{
		public CandidatesList Candidates;
		public event PropertyChangedEventHandler PropertyChanged;

		private bool? _readonly = null;
		public bool ReadOnly
		{
			get { return (bool)_readonly; }
			set { if (_readonly == null) { _readonly = value; } }
		}

		private int _value = -1;
		public int Value
		{
			get { return this._value; }
			set
			{
				if (this._value != value)
				{
					this._value = value;
					this.NotifyPropertyChanged();
					bool isSet = IsSolved;
					this.ShowValue = isSet;
					this.ShowCandidates = !isSet;
				}
			}
		}

		public bool IsSolved { get { return (ReadOnly || (this.Value > 0 && this.Value <= StaticSudoku.Dimension)) ? true : false; } }

		private bool _showValue = true;
		public bool ShowValue
		{
			get { return _showValue; }
			set { if (value != _showValue) { _showValue = value; this.NotifyPropertyChanged(); } }
		}

		private bool _showCandidates = false;
		public bool ShowCandidates
		{
			get { return _showCandidates; }
			set { if (value != _showCandidates) { _showCandidates = value; this.NotifyPropertyChanged(); } }
		}

		private Color DefaultErrorColor = Color.MistyRose;
		private Color DefaultBackgroundColor = Color.FromKnownColor(KnownColor.Control);
		private Font Font_Clue = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Bold);

		public SudokuCellControl(int value)
		{
			InitializeComponent();

			this.Value = value;
			ReadOnly = (Value != 0);

			if (ReadOnly)
			{
				Candidates = new CandidatesList();
				this.flowLayoutPanel.Visible = false;
				this.editLabel.Visible = true;
				this.editLabel.Font = Font_Clue;
				this.editLabel.IsEditable = false;
				this.editLabel.Text = Value.ToString();
			}
			else
			{
				this.editLabel.DataBindings.Add("Text", this, "Value");
				this.editLabel.DataBindings.Add("Visible", this, "ShowValue");
				this.flowLayoutPanel.DataBindings.Add("Visible", this, "ShowCandidates");

				Candidates = new CandidatesList(CandidatesList.All);
				this.candidate1.DataBindings.Add("Visible", Candidates, "Contains1");
				this.candidate2.DataBindings.Add("Visible", Candidates, "Contains2");
				this.candidate3.DataBindings.Add("Visible", Candidates, "Contains3");
				this.candidate4.DataBindings.Add("Visible", Candidates, "Contains4");
				this.candidate5.DataBindings.Add("Visible", Candidates, "Contains5");
				this.candidate6.DataBindings.Add("Visible", Candidates, "Contains6");
				this.candidate7.DataBindings.Add("Visible", Candidates, "Contains7");
				this.candidate8.DataBindings.Add("Visible", Candidates, "Contains8");
				this.candidate9.DataBindings.Add("Visible", Candidates, "Contains9");
			}

			PaintCell();
		}

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void candidateClick(object sender, System.EventArgs e)
		{
			Label ctrl = sender as Label;
			if (ctrl != null)
			{
				ctrl.Visible = !ctrl.Visible;
			}
		}





		public void HighlightError()
		{
			if (this.BackColor != DefaultErrorColor)
			{
				this.BackColor = DefaultErrorColor;
			}
		}

		public void HighlightDefault()
		{
			if (this.BackColor != DefaultBackgroundColor)
			{
				this.BackColor = DefaultBackgroundColor;
			}
		}





		public void PaintCell()
		{
			this.Invalidate(true);
		}

		internal bool CheckForNakedSingle()
		{
			if (!ReadOnly && !IsSolved && Candidates.Count == 1)
			{
				Candidates.Clear();
				Value = Candidates.Single();
				PaintCell();
				return true;
			}
			return false;
		}


	}
}
