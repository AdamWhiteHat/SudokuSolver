/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using EditLabelControl;

namespace SudokuGame
{
	public partial class SudokuCell : FlowLayoutPanel
	{
		private Font Font_Clue = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Bold);
		private Font Font_Guess = new Font(new FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular);
		private Font Font_Candidates = new Font(new FontFamily("Consolas"), 8);

		private EditLabel guess;
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
					marking.Font = Font_Candidates;
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

			guess = new EditLabel();
			guess.Name = string.Format("Guess");
			guess.Margin = new Padding(0);
			guess.AutoSize = false;
			guess.Size = new Size(49, 41);
			guess.TextAlign = ContentAlignment.MiddleCenter;
			if (IsClue)
			{
				guess.Font = Font_Clue;
				guess.Text = this.Value.ToString();
				guess.IsEditable = false;
			}
			else
			{
				guess.Font = Font_Guess;
				guess.Text = "";
				guess.EditingSuccessful += guess_EditingSuccessful;
			}
			Controls.Add(guess);
			Paint_Guess();

			Dock = DockStyle.Fill;
			Margin = new Padding(0);
		}

		void guess_EditingSuccessful(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(guess.Text))
			{
				if (this.Value != 0)
				{
					this.Value = 0;
				}
				return;
			}

			int numberValue = 0;
			string textValue = new string(guess.Text.Where(c => !"()".Contains(c)).ToArray());
			if (string.IsNullOrWhiteSpace(textValue) || !int.TryParse(textValue, out numberValue) || numberValue == 0)
			{
				guess.Text = string.Empty;
				if (this.Value != 0)
				{
					this.Value = 0;
				}
				return;
			}

			if (this.Value != numberValue)
			{
				this.Value = numberValue;
			}
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
				if (Cell.Candidates.Count != 1)
				{
					Ctrl.Text = " ";
					Cell.Candidates.Remove(Number);
				}
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
			return (Value == 0) ? FormatCandidatesString_NewLines() : IsClue ? string.Format("({0})", Value) : Value.ToString();
		}

		public void PaintCell()
		{
			if (Candidates != null)
			{
				this.SuspendLayout();
				Paint_Guess();
				Paint_Candidates();
				this.ResumeLayout();
			}
		}

		private void Paint_Guess()
		{
			if (guess != null)
			{
				if (IsClue)
				{
					guess.Visible = true;
					guess.Text = string.Format("({0})", Value);
				}
				else if (Value != 0)
				{
					guess.Visible = true;
					guess.Text = string.Format("{0}", Value);
				}
				else if (Candidates.Count < 1)  // !IsClue && Value == 0
				{
					guess.Visible = true;
				}
				else // !IsClue && Value == 0 && Candidates.Count > 0
				{
					guess.Visible = false;
					guess.SendToBack();
				}
			}
		}

		private void Paint_Candidates()
		{
			if (markings == null || markings.Count < 1)
			{
				return;
			}

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

		private bool ShowCandidates()
		{
			return (!IsClue && Value == 0 && Candidates.Count > 0);
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
