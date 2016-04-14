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
using MultiKeyDictionaries;

using SudokuGame;

namespace SudokuSolver
{
	public class Simple
	{
		protected ISudokuGrid _sudokuGrid = null;
		private delegate List<SudokuCell> ScopeRegionDelegate(int Index);

		public Simple(ISudokuGrid BoardToSolve)
		{
			_sudokuGrid = BoardToSolve;
		}

		public Tuple<int, int, int> Solve()
		{
			int rounds = 0;
			int solvedTotal = 0;
			int solvedRound = 0;
			int eliminatedTotal = 0;
			int eliminatedRound = 0;

			do
			{
				eliminatedRound = EliminateCandidates2();
				Tuple<int, int> scanResult = ScanNakedSingles();
				eliminatedRound += scanResult.Item1;
				solvedRound = scanResult.Item2;

				eliminatedTotal += eliminatedRound;
				solvedTotal += solvedRound;

				rounds++; // Thread.Sleep(100);
			}
			while (eliminatedRound != 0 && solvedRound != 0);

			_sudokuGrid.PaintGrid();

			return new Tuple<int, int, int>(eliminatedTotal, solvedTotal, rounds);
		}

		public bool IsSolved()
		{
			return _sudokuGrid.IsSolved();
		}

		public int EliminateCandidates2()
		{
			int candidatesRemoved = 0;
			foreach (SudokuCell cell in _sudokuGrid.Cells)
			{				
				List<int> valuesInScope =_sudokuGrid.GetValuesInCells(_sudokuGrid.GetCellsInScope(cell).ToArray()).Distinct().ToList();
				candidatesRemoved += cell.RemoveCandidates(valuesInScope);
			}
			return candidatesRemoved;
		}













		public Tuple<int, int> ScanNakedSingles()
		{
			int eliminatedTotal = 0; int solvedTotal = 0;

			IEnumerable<SudokuCell> nakedSingles = _sudokuGrid.Cells.Where(c => c.Candidates.Count == 1);

			foreach (SudokuCell cell in nakedSingles)
			{
					cell.Value = cell.Candidates.First();
					cell.Candidates.Clear();
					solvedTotal++;

					eliminatedTotal += EliminateCandidatesInScopeOfCell(cell);
			}

			return new Tuple<int, int>(eliminatedTotal, solvedTotal);
		}

		public int EliminateCandidatesInScopeOfCell(SudokuCell Cell)
		{
			int eliminatedTotal = EliminateCandidatesInScope(GetRegionScope(SudokuRegion.Row, Cell.Row));
			eliminatedTotal += EliminateCandidatesInScope(GetRegionScope(SudokuRegion.Column, Cell.Column));
			eliminatedTotal += EliminateCandidatesInScope(GetRegionScope(SudokuRegion.Block, Cell.Block));
			return eliminatedTotal;
		}

		public int EliminateCandidates()
		{
			int eliminatedTotal = 0; int eliminatedRound = 0;
			do
			{
				eliminatedRound = EliminateEveryCandidateInRegion(SudokuRegion.Column);
				eliminatedRound += EliminateEveryCandidateInRegion(SudokuRegion.Block);
				eliminatedRound += EliminateEveryCandidateInRegion(SudokuRegion.Row);
				eliminatedTotal += eliminatedRound;
			}
			while (eliminatedRound != 0);

			return eliminatedTotal;
		}

		public int EliminateEveryCandidateInRegion(SudokuRegion Region)
		{
			int eliminatedTotal = 0; int eliminatedRound = 0;
			do
			{
				eliminatedRound = EliminateCandidatesInRegion(Region);
				eliminatedTotal += eliminatedRound;
			}
			while (eliminatedRound != 0);

			return eliminatedTotal;
		}

		public int EliminateCandidatesInRegion(SudokuRegion Region)
		{
			int eliminatedTotal = 0; int eliminatedRound = 0;

			ScopeRegionDelegate ScopeFunction = GetScopeDelegate(Region);
			int counter = 1;
			do
			{
				List<SudokuCell> scope = ScopeFunction(counter);
				eliminatedRound = EliminateCandidatesInScope(scope);
				eliminatedTotal += eliminatedRound;
			}
			while (counter++ <= StaticSudoku.Dimension);

			return eliminatedTotal;
		}

		public int EliminateCandidatesInScope(List<SudokuCell> Scope)
		{
			// IF Value == 0, SudokuCell is empty (does not have a guess). ELSE, value is the number entered into the cell and has no candidates left
			// First, limit the scope only to cells worth updating
			List<SudokuCell> scopeToUpdate = Scope.Where(cell => (cell.Value == 0 && cell.Candidates.Count > 1)).ToList();

			// Get a list of values that we want to eliminate from the scope
			List<int> valuesToRemove = Scope.Select(cell => cell.Value).Where(val => val != 0).Distinct().ToList();

			// This function does the actual work
			return RemoveCandidatesValues(valuesToRemove, scopeToUpdate);
		}

		public int RemoveCandidatesValues(List<int> ValuesToRemove, List<SudokuCell> Scope)
		{
			int eliminatedTotal = 0;// int eliminatedRound = 0;

			foreach (SudokuCell cell in Scope)
			{
				eliminatedTotal += _sudokuGrid.RemoveCellCandidates(cell.Column, cell.Row, ValuesToRemove);
			}

			return eliminatedTotal;
		}

		public List<SudokuCell> GetRegionScope(SudokuRegion Region, int Index)
		{
			return GetScopeDelegate(Region).Invoke(Index);
		}

		private ScopeRegionDelegate GetScopeDelegate(SudokuRegion Region)
		{
			if (Region == SudokuRegion.Column) return (ScopeRegionDelegate)_sudokuGrid.GetColumnScope;
			else if (Region == SudokuRegion.Row) return (ScopeRegionDelegate)_sudokuGrid.GetRowScope;
			else if (Region == SudokuRegion.Block) return (ScopeRegionDelegate)_sudokuGrid.GetBlockScope;
			else throw new ArgumentException("The SudokuRegion supplied is not supported.");
		}
	}
}