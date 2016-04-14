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
	public class Moderate : Simple
	{
		Dictionary<string,string> visitedGroups = new Dictionary<string, string>();
		public Moderate(ISudokuGrid Puzzle) : base(Puzzle)
		{
			visitedGroups = new Dictionary<string, string>();
		}
		
		public new Tuple<int,int,int> Solve()
		{
			Tuple<int,int,int> results = new Tuple<int, int, int>(0,0,0);
			
			do
			{  // eliminated, solved, rounds
				Tuple<int,int,int> progress = new Tuple<int, int, int>(0,0,0);
				
				progress = progress.Add(base.Solve());
				
				if(base.IsSolved())
				{
					results = results.Add(progress);
					break;
				}
				
				progress = progress.Add(SolveModerate());
				
				if(progress.Item1==0 && progress.Item2==0)
				{
					results = results.Add(progress);
					break;
				}
				
				results = results.Add(progress);
			}
			while(!base.IsSolved());
			
			return results;
		}

		//int loopPosition = 0;
		// eliminated, solved
		public Tuple<int, int, int> SolveModerate()
		{
			List<SolverDelegate> executionPlan = new List<SolverDelegate>();
			executionPlan.Add(NakedCandidates);
			executionPlan.Add(HiddenCandidates);
			//FunctionList.Add(PointingPairs);

			Tuple<int, int, int> totals = ExecutePlan(executionPlan);
			_sudokuGrid.Visible = false;
			//totals = totals.Add(NakedCandidates_Loop());			
			//totals = totals.Add(HiddenCandidates_Loop());
			//eliminated += PointingPairs_Loop();
			//
			//			if(loopPosition>9) loopPosition = 0;
			//			totals = totals.Add(NakedCandidates(loopPosition++),loopPosition,1));

			_sudokuGrid.Visible = true;
			_sudokuGrid.PaintGrid();
			return totals;
		}

		Tuple<int, int, int> ExecutePlan(List<SolverDelegate> functionList)
		{
			Tuple<int,int,int> total = new Tuple<int, int, int>(0,0,0);
			Tuple<int, int, int> round;

			do
			{
				round = new Tuple<int, int, int>(0,0,0);

				foreach (SolverDelegate function in functionList)
				{
					for (int counter = 1; counter <= StaticSudoku.Dimension; counter++)
					{
						round = round.Add(EliminateCandidates(), 0);
						round = round.Add(function(counter), 0);
						//round = round.Add(RunSolverFunction(function));
						round = round.Add(ScanNakedSingles());
					}
				}
				
				

				total = total.Add(round);

			}
			while (round.Item1 + round.Item2 > 0);


			return total;
		}


		//////////////////////////////////////////////////////////////////////




		//////////////////////////////////////////////////////////////////////

		// eliminated, solved, rounds
		private delegate int SolverDelegate(int blockIndex);
		
		Tuple<int,int,int> RunSolverFunction(SolverDelegate solverFunction)
		{
			Tuple<int,int,int> totals = new Tuple<int, int, int>(0,0,0);
			for(int counter=1;counter<=StaticSudoku.Dimension;counter++)
			{
				int solved = 0;
				int eliminated = solverFunction(counter);
				
				if(eliminated > 0)
				{
					totals = totals.Add(eliminated, solved);
					totals = totals.Add(ScanNakedSingles());
				}
			}
			return totals;
		}
		
		Tuple<int, int, int> NakedCandidates_Loop()
		{
			return RunSolverFunction(NakedCandidates);
		}
		
		Tuple<int, int, int> PointingPairs_Loop()
		{
			return RunSolverFunction(PointingPairs);
		}
		
		Tuple<int, int, int> HiddenCandidates_Loop()
		{
			return RunSolverFunction(HiddenCandidates);
		}		
		
		int NakedCandidates(int counter)
		{
			int totalEliminated = 0;
			
			List<SudokuRegion> regions = new List<SudokuRegion>();
			regions.Add(SudokuRegion.Row);
			regions.Add(SudokuRegion.Column);
			//regions.Add(SudokuRegion.Block);
			foreach(SudokuRegion region in regions)
			{
				List<SudokuCell> scope = GetRegionScope(region,counter);
				
				List<SudokuCell> matchingPairs = FindNakedMatchingCandidates(scope,2);
				if(matchingPairs.Count == 2)
					totalEliminated += FoundNakedMatchingCandidates(matchingPairs,region,counter);
				
				scope = GetRegionScope(region,counter);
				
				List<SudokuCell> matchingTriplets = FindNakedMatchingCandidates(scope,3);
				if(matchingTriplets.Count == 3)
					totalEliminated += FoundNakedMatchingCandidates(matchingTriplets,region,counter);
			}
			
			return totalEliminated;
		}		
		
		List<SudokuCell> FindNakedMatchingCandidates(List<SudokuCell> scope, int groupSize)
		{
			List<string> candidatesAll = scope.Select(c => c.FormatCandidatesString_Compact()).ToList();
			List<string> potentialMatches = candidatesAll.Where(c => c.Length==groupSize).ToList();
			
			if(potentialMatches.Count < groupSize)
			{
				return new List<SudokuCell>();
			}
			
			foreach(string pair in potentialMatches)
			{
				List<string> deDupedList = new List<string>(potentialMatches);
				deDupedList.Remove(pair);
				
				if(deDupedList.Count < (groupSize-1))
				{
					continue;
				}
				
				List<string> matching = deDupedList.Where(c => c == pair).ToList();
				
				if(matching.Count == (groupSize-1))
				{
					List<SudokuCell> foundMatches = scope.Where(c => c.FormatCandidatesString_Compact() == pair).ToList();
					
					// Skip already visited matches
					bool isUnique = true;
					foreach(KeyValuePair<string,string> toSkip in visitedGroups)
					{
						if(!isUnique) break;
						foreach(SudokuCell match in foundMatches)
						{
							// Don't rely matching just values alone, ensure that we are talking about the same position
							if(toSkip.Key==match.GridPosition.ToString())
							{
								if(toSkip.Value == match.FormatCandidatesString_Compact())
								{
									isUnique = false;
									break;
								}
							}
						}
					}
					
					if(isUnique)
					{
						return foundMatches;
					}
				}
			}
			
			return new List<SudokuCell>();
		}
		
		int FoundNakedMatchingCandidates(List<SudokuCell> matchingGroup, SudokuRegion region, int regionIndex)
		{
			List<int> groupValues = matchingGroup[0].Candidates.ToList();
			DebugWrite("Found: Naked Matching Candidates! GroupSize=\"{0}\", Region=\"{1} {2}\", Candidates=\"{3}\".",
			           matchingGroup.Count, Enum.GetName(typeof(SudokuRegion),region), regionIndex, StaticSudoku.ArrayToString(groupValues,","));
			
			
			SudokuCell cell = matchingGroup[0];
			
			foreach(SudokuCell seen in matchingGroup)
			{
				string positionKey = seen.GridPosition.ToString();
				if (!visitedGroups.ContainsKey(positionKey))
				{
					visitedGroups.Add(positionKey, seen.FormatCandidatesString_Compact());
				}
			}
			
			int totalEliminated = 0;
			
			if(InSameRow(matchingGroup))
			{
				List<SudokuCell> rowScope		= _sudokuGrid.GetRowScope(cell.Row).Except(matchingGroup).ToList();
				totalEliminated += RemoveCandidatesValues(groupValues, rowScope);
			}
			else if(InSameColumn(matchingGroup))
			{
				List<SudokuCell> columnScope = _sudokuGrid.GetColumnScope(cell.Column).Except(matchingGroup).ToList();
				totalEliminated += RemoveCandidatesValues(groupValues, columnScope);
			}
			
			if(InSameBlock(matchingGroup))
			{
				List<SudokuCell> blockScope	= _sudokuGrid.GetBlockScope(cell.Block).Except(matchingGroup).ToList();
				totalEliminated += RemoveCandidatesValues(groupValues, blockScope);
			}
			
			return totalEliminated;
		}
		
		
		
		int HiddenSingle(List<SudokuCell> block, int single)
		{
			int solved = 0;
			int eliminatedTotal = 0;

			foreach(SudokuCell cell in block)
			{
				int matches = cell.Candidates.Where(i => i == single).ToList().Count;
				if(matches == 1)
				{
					DebugWrite(string.Format("Removed Hidden Single {0} from cell C{1}R{2}.",single,cell.Column,cell.Row));
					cell.Value = single;
					solved++;
					eliminatedTotal += EliminateCandidatesInScopeOfCell(cell);
				}
			}
			
			return solved;
		}
		
		
		int HiddenCandidates(int blockIndex)
		{
			int totalEliminated = 0;
			SudokuBlock sudokuBlock = new SudokuBlock(_sudokuGrid.GetBlockScope(blockIndex));
			List<SudokuCell> block = GetUnsolvedBlock(blockIndex);
			RankingDictionary<int> candidateRanking = GetCandidateRanking(block);
			
			List<int> hiddenSingle = candidateRanking[1]; // Index == # of occurrences-1, value == candidate digit that appears that number of times
			List<int> hiddenPair = candidateRanking[2];
			List<int> hiddenTriple = candidateRanking[3];
			
//			List<CellEventInfo> history;
			
			if(hiddenSingle.Count != 0)
			{
				totalEliminated += HiddenSingle(block, hiddenSingle.First());
			}
//			if(hiddenPair.Count != 0)
//			{
//				totalEliminated += ExploreHiddenSubset(hiddenPair, block);
//				history = _sudokuGrid.EditHistory;
//			}
//			if(hiddenTriple.Count != 0)
//			{
//				totalEliminated += ExploreHiddenSubset(hiddenTriple, block);
//				history = _sudokuGrid.EditHistory;
//			}
			
			return totalEliminated;
		}
		
		
		int ExploreHiddenSubset(List<int> hiddenSubset, List<SudokuCell> block)
		{
			if(hiddenSubset.Count > 0)
			{
				
				DebugWrite("HiddenSubset: Candidate(s) ({0}) has only {1} entries in block {2}.",
				           StaticSudoku.ArrayToString(hiddenSubset, ", "), hiddenSubset.Count, block[0].Block);
				
				List<SudokuCell> subsetCells = GetCellsWithSubset_Any(block, hiddenSubset);
				
				if(subsetCells.Count == 0)
				{
					return 0;
				}
				
				if(subsetCells.Count==1 && hiddenSubset.Count == 1)
				{
					SudokuCell cell = subsetCells[0];
					int val = hiddenSubset[0];
					DebugWrite("ELIMINATED NAKED Candidate \"{0}\": Block {1}, column {2}, row {3}.", val, cell.Block, cell.Column, cell.Row);
					cell.Value = val;
					
					return RemoveCandidatesValues(hiddenSubset, _sudokuGrid.GetCellsInScope(cell).ToList());
				}
				else if(subsetCells.Count==2 && hiddenSubset.Count == 2)
				{
					int col = subsetCells[0].GridPosition.Column;
					int row = subsetCells[0].GridPosition.Row;
					bool columnMatch = InSameColumn(subsetCells);
					bool rowMatch = InSameRow(subsetCells);
					
					if(columnMatch)
					{
						// search columns
						List<SudokuCell> scope = _sudokuGrid.GetColumnScope(col).Except(subsetCells).ToList();
						DebugWrite("HIDDEN PAIR: Eliminated candidates ({0}) on column {1}.",StaticSudoku.ArrayToString(hiddenSubset,","),col);
						return RemoveCandidatesValues(hiddenSubset, scope);
					}
					else if(rowMatch)
					{
						// search rows
						List<SudokuCell> scope = _sudokuGrid.GetRowScope(row).Except(subsetCells).ToList();
						DebugWrite("HIDDEN PAIR: Eliminated candidates ({0}) on row {1}.",StaticSudoku.ArrayToString(hiddenSubset,","),row);
						return RemoveCandidatesValues(hiddenSubset, scope);
					}
				}
				else if(subsetCells.Count == 3 && hiddenSubset.Count == 3)
				{
					bool columnMatch = InSameColumn(subsetCells);
					bool rowMatch = InSameRow(subsetCells);
					
					if(columnMatch)
					{
						int col = subsetCells[0].GridPosition.Column;
						DebugWrite("_____HIDDEN TRIPLE: On column {0}",col);
					}
					else if(rowMatch)
					{
						int row = subsetCells[0].GridPosition.Row;
						DebugWrite("_____HIDDEN TRIPLE: On row {0}",row);
					}
				}
			}
			return 0;
		}
		
		int PointingPairs(int blockIndex)
		{
			List<SudokuCell> block = GetUnsolvedBlock(blockIndex);
			
			List<SudokuCell> candidatePair = block.Where(c => c.Candidates.Count==2).ToList();
			
			if(candidatePair.Count==2)
			{
				List<int> values = candidatePair[0].Candidates.ToList();
				int columnNumber = candidatePair[0].GridPosition.Column;
				int rowNumber = candidatePair[0].GridPosition.Row;
				string ident = DebugCandidateInfo(candidatePair);
				
				DebugWrite("Found possible pointing pairs: {0}.",ident);
				
				bool isValid = true;
				foreach(int digit in values)
				{
					if(block.Where(c => c.Candidates.Contains(digit)).Count() > 2)
					{
						isValid = false;
					}
				}
				if(!isValid)
				{
					DebugWrite("Failed for {0}.",ident);
					return 0;
				}
				
				if(InSameColumn(candidatePair))
				{
					DebugWrite("Succeeded (column) for {0}.", ident);
					List<SudokuCell> scope = _sudokuGrid.GetColumnScope(columnNumber).Except(candidatePair).ToList();
					return RemoveCandidatesValues(values, scope);
				}
				else if(InSameRow(candidatePair))
				{
					DebugWrite("Succeeded (row) for {0}.", ident);
					List<SudokuCell> scope = _sudokuGrid.GetRowScope(rowNumber).Except(candidatePair).ToList();
					return RemoveCandidatesValues(values, scope);
				}
			}
			if(candidatePair.Count==3)
			{
				string ident = DebugCandidateInfo(candidatePair);
				
				DebugWrite("Possible pointing TRIPPLE pair for {0}.",ident);
			}
			
			return 0;
		}
		
		List<SudokuCell> GetCellsWithSubset_Any(List<SudokuCell> block, List<int> hiddenSubset)
		{
			List<SudokuCell> result = new List<SudokuCell>();
			
			foreach(int digit in hiddenSubset)
			{
				result.AddRange(block.Where(c => c.Candidates.Contains(digit)).ToList());
			}
			
			return result;
		}
		
		List<SudokuCell> GetCellsWithSubset_Exact(List<SudokuCell> block, List<int> hiddenSubset)
		{
			List<SudokuCell> result = new List<SudokuCell>();
			
			foreach(SudokuCell cell in block)
			{
				bool match = true;
				SortedSet<int> digitPool = cell.Candidates;
				
				foreach(int digit in hiddenSubset)
				{
					if(!digitPool.Contains(digit))
						match = false;
					else
						digitPool.Remove(digit);
				}
				
				if(digitPool.Count != 0)
					match = false;
				
				if(match)
				{
					result.Add(cell);
				}
			}
			
			return result;
		}
		
		RankingDictionary<int> GetCandidateRanking(List<SudokuCell> block)
		{
			FrequencyDictionary<int> candidateFrequency = new FrequencyDictionary<int>();
			foreach(SudokuCell cell in block)
			{
				candidateFrequency.AddRange(cell.Candidates.ToArray());
			}
			return candidateFrequency.GetRankingDictionary();
		}
		
		List<SudokuCell> GetUnsolvedBlock(int Index)
		{
			return _sudokuGrid.GetBlockScope(Index).Where(c=>c.Value==0).ToList();
		}
		
		List<SudokuCell> GetUnsolvedRow(int Index)
		{
			return _sudokuGrid.GetRowScope(Index).Where(c=>c.Value==0).ToList();
		}
		
		List<SudokuCell> GetUnsolvedColumn(int Index)
		{
			return _sudokuGrid.GetColumnScope(Index).Where(c=>c.Value==0).ToList();
		}
		
		
		bool InSameRow(List<SudokuCell> cells)
		{
			if(cells.Count < 2)	return false;
			List<int> rows = cells.Select(c => c.GridPosition.Row).ToList();
			return AllSameNumber(rows);
		}
		
		bool InSameColumn(List<SudokuCell> cells)
		{
			if(cells.Count < 2)	return false;
			List<int> cols = cells.Select(c => c.GridPosition.Column).ToList();
			return AllSameNumber(cols);
		}
		
		bool InSameBlock(List<SudokuCell> cells)
		{
			if(cells.Count < 2)	return false;
			List<int> block = cells.Select(c => c.GridPosition.Block).ToList();
			return AllSameNumber(block);
		}
		
		bool AllSameNumber(List<int> digits)
		{
			if(digits.Distinct().Count() == 1)
			{
				return true;
			}
			return false;
		}

		//////////////////////////////////////////////////////////////////////////////


		public StaticSudoku.DisplayOutputDelegate DisplayOutputFunction { get; set; }
		
		void DebugWrite(string format, params object[] args)
		{
			DisplayOutputFunction.Invoke(string.Format(format, args));
		}
		
		string DebugCandidateInfo(List<SudokuCell> candidatePair)
		{
			List<int> values = candidatePair[0].Candidates.ToList();
			int columnNumber = candidatePair[0].GridPosition.Column;
			int rowNumber = candidatePair[0].GridPosition.Row;
			int blockNumber = candidatePair[0].GridPosition.Block;
			return string.Format("Values: {0} Column: {1} Row: {2} Block: {3}",
			                     StaticSudoku.ArrayToString(values,  ", "),
			                     columnNumber, rowNumber, blockNumber);
		}
	}
}
