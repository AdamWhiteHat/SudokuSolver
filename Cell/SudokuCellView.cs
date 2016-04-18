/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 *    http://arakaska.wix.com/intelligentsoftware
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EditLabelControl;

namespace SudokuGame
{
	public partial class SudokuCell : FlowLayoutPanel
	{
		private Font Font_ClueView = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Bold);
		private Font Font_GuessView = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular);
		private Font Font_CandidatesView = new Font(new FontFamily("Consolas"), 8);

		private EditLabel guess;
		private Label clue;
		private List<Label> markings;

		void InitializeView()
		{
			markings = new List<Label>();
			if (!IsClue && Value == 0)
			{
				foreach (int number in Enumerable.Range(1, StaticSudoku.Dimension))
				{
					Label marking = new Label();
					marking.Name = string.Format("Candidate{0}", number);
					marking.Font = Font_CandidatesView;
					marking.AutoSize = true;
					marking.Margin = new Padding(0);
					marking.Padding = new Padding(1, 0, 2, 0);
					marking.Text = Candidates.Contains(number) ? number.ToString() : " ";

					marking.Click += new EventHandler(
						delegate
						{
							Pencil(marking, this, number);
						}
					);

					markings.Add(marking);
				}

				if (markings != null && markings.Count > 0)
				{
					Controls.AddRange(markings.ToArray());
				}
			}

			clue = new Label();
			clue.Name = string.Format("Clue");
			clue.Margin = new Padding(0);
			clue.Font = Font_ClueView;
			clue.AutoSize = false;
			clue.Size = new Size(49, 41);
			clue.TextAlign = ContentAlignment.MiddleCenter;
			Controls.Add(clue);
			Paint_Clue();

			guess = new EditLabel();
			guess.Name = string.Format("Guess");
			guess.Margin = new Padding(0);
			guess.Font = Font_GuessView;
			guess.AutoSize = false;
			guess.Size = new Size(49, 41);
			guess.TextAlign = ContentAlignment.MiddleCenter;
			Controls.Add(guess);
			Paint_Guess();

			Dock = DockStyle.Fill;
			Margin = new Padding(0);
			Padding = new Padding(0);
		}

		private void Pencil(Control Ctrl, SudokuCell Cell, int Number)
		{
			if (Ctrl.Text == " ")
			{
				Ctrl.Text = Number.ToString();
				if (!Cell.Candidates.Contains(Number))
				{
					Cell.Candidates.Add(Number);
				}
			}
			else
			{
				Ctrl.Text = " ";
				Cell.Candidates.Remove(Number);
				Cell.CheckForNakedSingle();
			}
		}
			
		public void HighlightError()
		{
			if (this.BackColor != DefaultErrorColor)
			{
				this.DefaultBackgroundColor = this.BackColor;
				SetControlsBackColor(DefaultErrorColor);
			}
		}

		public void HighlightDarkGrey()
		{
			DefaultBackgroundColor = Color.DarkGray;
			SetControlsBackColor(Color.DarkGray);
		}

		public void HighlightDefault()
		{
			if (this.BackColor != this.DefaultBackgroundColor)
			{
				SetControlsBackColor(DefaultBackgroundColor);
			}
		}

		private void SetControlsBackColor(Color backColor)
		{
			this.BackColor = backColor;
		}

		public override string ToString()
		{
			return (Value == 0) ? FormatCandidatesString_NewLines() : string.Format("({0})", Value);
		}

		public void PaintCell()
		{
			if (Candidates != null)
			{
				this.SuspendLayout();
				Paint_Clue();
				Paint_Guess();
				Paint_Candidates();
				this.ResumeLayout();
			}
		}

		private void Paint_Clue()
		{
			if (clue != null)
			{
				if (ShowClue())
				{
					clue.Visible = true;
					clue.Text = string.Format("({0})", Value);
				}
				else
				{
					clue.Text = string.Empty;
					clue.Visible = false;
				}
			}
		}

		private void Paint_Guess()
		{
			if (guess != null)
			{
				if (ShowGuess())
				{
					guess.Visible = true;
					guess.Text = string.Format("({0})", Value);
				}
				else
				{
					guess.Text = string.Empty;
					guess.Visible = false;
				}
			}
		}

		private void Paint_Candidates()
		{
			if (markings != null && markings.Count > 0)
			{
				foreach (Control ctrl in Controls)
				{
					if (ctrl.Name.Contains("Candidate"))
					{
						if (ShowCandidates())
						{
							ctrl.Visible = true;
							string strNumber = ctrl.Name.Replace("Candidate", "");
							int number = Convert.ToInt32(strNumber);
							if (Candidates.Contains(number))
							{
								ctrl.Text = number.ToString();
							}
							else
							{
								ctrl.Text = " ";
							}
						}
						else
						{
							ctrl.Text = string.Empty;
							ctrl.Visible = false;
						}
					}
				}
			}
		}

		private bool ShowClue()
		{
			return (IsClue && Value != 0);
		}
		private bool ShowGuess()
		{
			return (!IsClue && Value != 0);
		}
		private bool ShowCandidates()
		{
			return (!IsClue && Value == 0);
		}

		public string FormatCandidatesString_Compact()
		{
			return StaticSudoku.ArrayToString(this.Candidates.Select(c => c.ToString()), "");
		}

		public string FormatCandidatesString_NewLines()
		{
			string spacer = " ";
			string[] numbers = { Environment.NewLine, spacer, spacer, spacer, spacer, spacer, spacer, spacer, spacer, spacer };

			foreach (int digit in Candidates)
			{
				numbers[digit] = digit.ToString();
			}

			return string.Format("{1}{10}{2}{10}{3}{0}{4}{10}{5}{10}{6}{0}{7}{10}{8}{10}{9}", numbers[0],
								 numbers[1], numbers[2], numbers[3],
								 numbers[4], numbers[5], numbers[6],
								 numbers[7], numbers[8], numbers[9], spacer);
		}
	}
}
