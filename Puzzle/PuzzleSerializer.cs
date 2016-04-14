/*
 *
 * Developed by Adam Rakaska
 *  http://csharpcodewhisperer.blogspot.com
 *    http://arakaska.wix.com/intelligentsoftware
 *
 *
 * Made using SharpDevelop
 *
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;

namespace SudokuGame
{	

	public class PuzzleSerializer
	{
		string[] puzzleFiles = new string[]{ string.Empty };
		static readonly string puzzleFilePattern = "*.txt";
		
		public PuzzleSerializer()
		{
			puzzleFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, puzzleFilePattern, SearchOption.TopDirectoryOnly);
		}
		
		public string GetRandomFile()
		{
			int numFiles = puzzleFiles.Length;
			if(numFiles > 0)
			{
				return puzzleFiles[StaticRandom.Instance.Next(1,numFiles)];
			}
			return string.Empty;
		}
		
		public List<SudokuCell> LoadPuzzle()
		{
			string filename = "easy.puzzle";
			return LoadPuzzle(filename);
		}
		
		public List<SudokuCell> LoadPuzzle(string Filename)
		{
			List<SudokuCell> result = new List<SudokuCell>();
			
			if(File.Exists(Filename))
			{
				List<string> puzzles = ParsePuzzleFile(Filename);
				
				//File.WriteAllText(filename + ".output.txt", puzzles.Aggregate((a, b)=> a + Environment.NewLine + b) );
				
				if(puzzles.Count > 0)
				{
					List<int> puzzle = puzzles[ StaticRandom.Instance.Next(0,puzzles.Count) ].Select(c => Convert.ToInt32(c.ToString())).ToList();
					result = StaticSudoku.TranslateBoard(puzzle);
				}
			}	
			
			return result;			
		}
		
		public bool SavePuzzle(List<SudokuCell> Puzzle, string Filename=null)
		{
			if(Filename == null)
			{
				int puzzleNumber = Environment.TickCount;
				string puzzleFilename = string.Format("puzzle{0}.txt", puzzleNumber);
				Filename = puzzleFilename;
			}
			
			List<int> board = Puzzle.Select(c => c.Value).ToList();
			string[] cells = board.Select(c => c.ToString()).ToArray();
			if(cells.Length == StaticSudoku.Size)
			{
				int counter = 0;
				StringBuilder sb = new StringBuilder();
				foreach(string cell in cells)
				{
					if(counter == 0)
					{
						sb.AppendLine();
					}
					else if(counter%27 ==0)
					{
						sb.AppendLine();
						sb.AppendLine();
					}
					else if(counter%9 ==0)
					{
						sb.AppendLine();
					}
					else if(counter%3 ==0)
					{
						sb.Append(',');
						sb.Append('\t');
					}
					else
					{
						sb.Append(',');
					}
					
					sb.Append(cell);
					counter++;
				}
				
				string text = sb.ToString();
				
				text = text.Replace("0","_");
				
				File.WriteAllText(Filename, text);
				return true;
			}
			return false;
		}
		
		
		List<string> ParsePuzzleFile(string Filename)
		{
			string text = File.ReadAllText(Filename);
			
			// Remove whitespace & grid lines
			text = text.Replace("\t","");
			text = text.Replace(" ","");
			text = text.Replace("-----------","");
			text = text.Replace("+---+---+---+","");
			
			// Replace unsolved value cells
			text = text.Replace("_","0");
			text = text.Replace("?","0");
			text = text.Replace(".","0");
			text = text.Replace("-","0");
			text = text.Replace("X","0");
			text = text.Replace("*","0");
			
			// Remove formatting chars
			text = text.Replace(",","");
			text = text.Replace("+","");
			text = text.Replace("|","");
			
			// Remove section headers
			text = text.Replace("[Colors]","");
			text = text.Replace("[State]","");
			text = text.Replace("[PencilMarks]","");
			text = text.Replace("[Puzzle]","");

			// Split by lines
			List<string> lines = text.Split(new string[] { Environment.NewLine },StringSplitOptions.None).ToList();
			
			// # Remove comments & whitespace
			lines = lines.Where(ln => !ln.StartsWith("#")).ToList();
			lines = lines.Select(l => l.Replace(" ","")).ToList();
			
			List<string> result = new List<string>();
			int counter = 0;
			do
			{
				string buffer = string.Empty; //if(lines[counter].Any(c => char.IsNumber(c)))
				do
				{
					if(!string.IsNullOrWhiteSpace(lines[counter]))
					{
						buffer += lines[counter];
					}
					counter++;
				}
				while(buffer.Length<StaticSudoku.Size && counter<lines.Count);
				if(!string.IsNullOrWhiteSpace(buffer))
				{
					if(buffer.Length==StaticSudoku.Size)
					{
						result.Add(buffer);
					}
				}
			}
			while(counter<lines.Count);
			
			return result;
		}
		
		string GetHeaderSection(List<string> lines, int headerIndex)
		{
//			if(!lines[headerIndex].Contains("[Puzzle]"))
//			{
//				return string.Empty;
//			}
			int index = headerIndex;
			string result = string.Empty;
			while(result.Length < StaticSudoku.Size)
			{
				string currLine = lines[index].Replace(" ","");
				
				foreach(char c in currLine)
				{
					if(char.IsNumber(c))
					{
						result += c.ToString();
					}
				}
				index++;
			}
			return result;
		}
		
		
	}
}