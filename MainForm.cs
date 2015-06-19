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
			InitializeGrid();
			this.Controls.Add(sudokuGrid);
		}

		public void DebugWrite(string MessageFormat, params object[] Args)
		{
			this.Invoke(new MethodInvoker(delegate { tbDebug.Text += string.Format("{0}{1}{2}", MessageFormat, Args, Environment.NewLine); } ));
		}

		private void HideGrid()
		{
		}

		private void ShowGrid()
		{
		}
		
		void InitializeGrid(string puzzleFile="")
		{
			//string puzEZ = "test_puzzleEZ.puz";
			//string puz4 = "puzzle4.puz";
			//string puzNakedPair = "test_puzzleNakedPair.puz";
			//string puzNakedPair2 = "test_puzzleNakedPair2.puz";
			
			List<SudokuCell> puzzle = new List<SudokuCell>();
			
			if(!String.IsNullOrWhiteSpace(puzzleFile))
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
			tbDebug.Text += string.Format("Puzzle: \"{0}\"{1}", StaticSudoku.ArrayToString(puzzleArray), Environment.NewLine);
		}
		
		void BtnNewPuzzleClick(object sender, EventArgs e)
		{
			this.Controls.Remove(sudokuGrid);
			InitializeGrid();
			sudokuGrid.PaintGrid();
			this.Controls.Add(sudokuGrid);			
		}
		
		void BtnRefreshClick(object sender, EventArgs e)
		{
			sudokuGrid.PaintGrid();
		}
		
		void BtnSolveClick(object sender, EventArgs e)
		{
			//sudokuGrid.Visible = false;
			this.Controls.Remove(sudokuGrid);
			Tuple<int,int,int> totals = moderateSolver.Solve();
			//sudokuGrid.Visible = true;
			DisplayResults("Full",totals);
			this.Controls.Add(sudokuGrid);			
		}
		
		void BtnSolveEasyClick(object sender, EventArgs e)
		{
			//sudokuGrid.Visible = false;
			this.Controls.Remove(sudokuGrid);
			Tuple<int,int,int> totals = simpleSolver.Solve();
			//sudokuGrid.Visible = true;
			DisplayResults("Easy",totals);
			this.Controls.Add(sudokuGrid);			
		}
		
		void BtnSolveModerateClick(object sender, EventArgs e)
		{
			this.Controls.Remove(sudokuGrid);
			//sudokuGrid.Visible = false;
			Tuple<int, int, int> totals = moderateSolver.SolveModerate();
			//sudokuGrid.Visible = true;
			DisplayResults("Moderate", totals);
			this.Controls.Add(sudokuGrid);
		}
		
		void DisplayResults(string StrategyDescription, Tuple<int,int,int> totals)
		{
			if(sudokuGrid.IsSolved())
			{
				DebugWrite(string.Format("Solved with {0} strategy! Totals: {1} removes, {2} solves in {3} rounds.",
				                         StrategyDescription, totals.Item1, totals.Item2, totals.Item3));
			}
			else
			{
				DebugWrite(string.Format("Unable to solve with {0} strategy. {1} removes, {2} solves in {3} rounds.",
				                         StrategyDescription, totals.Item1, totals.Item2, totals.Item3));
			}
		}
		
		void BtnBrowsePuzClick(object sender, EventArgs e)
		{
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				InitializeGrid(openFileDialog.FileName);
			}
		}
	}
}
