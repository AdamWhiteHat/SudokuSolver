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

using SudokuGame;
using SudokuSolver;

namespace SudokuForm
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		SudokuGrid sudokuGrid;
		SudokuSolver.Simple simpleSolver;
		SudokuSolver.Moderate moderateSolver;
		PuzzleSerializer puzzleSerializer;

		public MainForm()
		{
			InitializeComponent();

			puzzleSerializer = new PuzzleSerializer();
			InitializeGrid("easy.puzzle");
			this.Controls.Add(sudokuGrid);
		}

		void InitializeGrid(string puzzleFile = "")
		{
			List<SudokuCell> puzzle = new List<SudokuCell>();

			if (!String.IsNullOrWhiteSpace(puzzleFile))
			{
				puzzle = puzzleSerializer.LoadPuzzle(puzzleFile);
			}
			else
			{
				puzzle = puzzleSerializer.LoadPuzzle();
			}

			sudokuGrid = new SudokuGrid(puzzle);
			sudokuGrid.DisplayOutputFunction = DebugWrite;
			simpleSolver = new SudokuSolver.Simple(sudokuGrid);
			moderateSolver = new SudokuSolver.Moderate(sudokuGrid);
			moderateSolver.DisplayOutputFunction = DebugWrite;

			List<int> puzzleArray = puzzle.Select(c => c.Value).ToList();
			tbDebug.Clear();
			tbDebug.AppendText(string.Format("Puzzle: \"{0}\"{1}", StaticSudoku.ArrayToString(puzzleArray), Environment.NewLine));
		}

		public void LoadPuzzle(string filename)
		{
			HideGrid();
			InitializeGrid(filename);
			sudokuGrid.PaintGrid();
			ShowGrid();		
		}

		void BtnNewPuzzleClick(object sender, EventArgs e)
		{
			LoadPuzzle("easy.puzzle");
		}

		void BtnBrowsePuzClick(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				LoadPuzzle(openFileDialog.FileName);
			}
		}

		void BtnRefreshClick(object sender, EventArgs e)
		{
			HideGrid();
			sudokuGrid.PaintGrid();
			ShowGrid();
		}

		void BtnSolveClick(object sender, EventArgs e)
		{
			HideGrid();
			Tuple<int, int, int> totals = moderateSolver.Solve();
			ShowGrid();
			DisplayResults("Full", totals);
		}

		void BtnSolveEasyClick(object sender, EventArgs e)
		{
			HideGrid();
			Tuple<int, int, int> totals = simpleSolver.Solve();
			ShowGrid();
			DisplayResults("Easy", totals);
		}

		void BtnSolveModerateClick(object sender, EventArgs e)
		{
			HideGrid();
			Tuple<int, int, int> totals = moderateSolver.SolveModerate();
			ShowGrid();
			DisplayResults("Moderate", totals);
		}

		private void HideGrid()
		{
			//sudokuGrid.Visible = false;
			this.Controls.Remove(sudokuGrid); 
			//this.SuspendLayout();
		}

		private void ShowGrid()
		{
			this.Controls.Add(sudokuGrid); 
			//this.ResumeLayout();
			//sudokuGrid.Visible = true;
		}

		void DisplayResults(string StrategyDescription, Tuple<int, int, int> totals)
		{
			if (sudokuGrid.IsSolved())
			{
				DebugWrite("Solved with {0} strategy! Totals: {1} removes, {2} solves in {3} rounds.",
										 StrategyDescription, totals.Item1, totals.Item2, totals.Item3);
			}
			else
			{
				DebugWrite("Unable to solve with {0} strategy. {1} removes, {2} solves in {3} rounds.",
										 StrategyDescription, totals.Item1, totals.Item2, totals.Item3);
			}
		}

		public void DebugWrite(string MessageFormat, params object[] Args)
		{
			if (string.IsNullOrWhiteSpace(MessageFormat))
			{
				DebugWrite("");
			}
			else if (Args == null || Args.Length < 1)
			{
				DebugWrite(MessageFormat);
			}
			else
			{
				DebugWrite(string.Format(MessageFormat, Args));
			}
		}

		public void DebugWrite(string Message)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate { DebugWrite(Message); }));
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(Message))
				{
					tbDebug.AppendText(Message);
				}
				tbDebug.AppendText(Environment.NewLine);
			}
		}
	}
}
