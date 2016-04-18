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
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using SudokuSolver;

namespace SudokuGame
{
	public partial class SudokuGrid : TableLayoutPanel
	{
		private List<SudokuCell> _cells = null;
		public List<SudokuCell> Cells { get { return _cells; } }
		public bool HighlightOnError { get; set; }
		
		public SudokuGrid(List<SudokuCell> cells) : base()
		{
			this._cells = cells;
			this.DoubleBuffered = true;
			
			/**** Create Grid ****/
			this.Name = "SudokuGrid";
			this.RowCount = StaticSudoku.Dimension;
			this.ColumnCount = StaticSudoku.Dimension;            
			
			this.Size = new System.Drawing.Size(450, 378); // Size(55*9, 42*9); // 495
			this.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
			this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

			// Settings
			this.HighlightOnError = true;
			
			for(int counter=0; counter<StaticSudoku.Dimension; counter++)
			{
				// Auto
				//this.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				//this.RowStyles.Add(new RowStyle(SizeType.AutoSize));

				// Absolute
				this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,49));
				this.RowStyles.Add(new RowStyle(SizeType.Absolute,41));

				// Percent
				//this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));
				//this.RowStyles.Add(new RowStyle(SizeType.Percent, percent));
			}
		
			PopulateGrid();
			
			HighlightBlocks();
			
			PaintGrid();

			SubscribeToEvents();
		}

		void cellValueChanged(SudokuCell sender, int newValue)
		{
			IEnumerable<SudokuCell> cellsInScope = GetCellsInScope(sender);
			List<int> values = GetValuesInCells(cellsInScope.ToArray());

			if (values.Contains(newValue))
			{
				if (HighlightOnError)
				{
					sender.HighlightError();
				}				
			}
			else
			{
				sender.HighlightDefault();
			}
		}

		private void PopulateGrid()
		{
			foreach(SudokuCell cell in Cells)
			{
				this.Controls.Add((FlowLayoutPanel)cell, cell.GridPosition.Column-1, cell.GridPosition.Row-1);
			}		
		}

		private void SubscribeToEvents()
		{
			foreach (SudokuCell cell in Cells)
			{				
				cell.ValueChanged += cellValueChanged;				
			}
		}
				
		public bool IsSolved()
		{
			return Cells.All(c => c.Value != 0);
		}
		
		public SudokuCell GetCell(int Column, int Row)
		{
			return Cells.Where(c => c.Row == Row && c.Column == Column).First();
		}
		
		public int RemoveCellCandidates(int Column, int Row, List<int> CandidatesToRemove)
		{
			foreach(SudokuCell cell in Cells)
			{
				if(cell.Column == Column && cell.Row == Row)
				{
					if(cell.Candidates.Count == 1)
					{
//						cell.Value = cell.Candidates.First();
//						cell.Candidates.Clear();
//						DebugWrite("@@@ AutoSolve {0} @ block {1}, column {2} row {3}.", cell.Value, cell.Block, cell.Column, cell.Row);
						return 0;
					}					
					int eliminated = cell.RemoveCandidates(CandidatesToRemove);					
					return eliminated;
				}
			}
			
			return 0; // Cells.First(cell => (cell.Row == Row && cell.Column == Column)).RemoveCandidates(CandidatesToRemove)
		}

		public List<int> GetValuesInCells(params SudokuCell[] Cells)
		{
			if (Cells == null || Cells.Length < 1)
				return new List<int>();
			else
				return Cells.Select(cell => cell.Value).Where(val => val != 0).ToList();
		}
		
		public IEnumerable<SudokuCell> GetCellsInScope(SudokuCell Cell)
		{
			List<SudokuCell> result = new List<SudokuCell>();
			result.AddRange( GetBlockScope(Cell.Block) );
			result.AddRange( GetRowScope(Cell.Row) );
			result.AddRange( GetColumnScope(Cell.Column) );
			result = result.Distinct().ToList();
			result.Remove(Cell);
			return result;
		}

		public List<SudokuCell> GetRowScope(int Index)
		{
			return Cells.Where(c => c.Row == Index).ToList();
		}
		
		public List<SudokuCell> GetColumnScope(int Index)
		{
			return Cells.Where(c => c.Column == Index).ToList();
		}
		
		public List<SudokuCell> GetBlockScope(int Index)
		{
			return Cells.Where(c => c.Block == Index).ToList();
		}
	}
}