/*
 *
 * Developed by Adam White
 *    https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SudokuGame
{
	public partial class SudokuGrid : TableLayoutPanel
	{
		public StaticSudoku.DisplayOutputDelegate DisplayOutputFunction { get; set; }

		//private void DebugWrite(string format, params object[] args)
		//{
		//	DisplayOutputFunction.Invoke(string.Format(format, args));
		//}
		
		private void HighlightBlocks()
		{
			List<SudokuCell> highlighBlocks = new List<SudokuCell>();
			highlighBlocks.AddRange( GetBlockScope(2) );
			highlighBlocks.AddRange( GetBlockScope(4) );
			highlighBlocks.AddRange( GetBlockScope(6) );
			highlighBlocks.AddRange( GetBlockScope(8) );
			highlighBlocks.OfType<SudokuCell>().ToList().ForEach(a => a.HighlightDarkGrey());
		}
		
		public void PaintGrid()
		{
			//this.Visible = false;
			this.Controls.OfType<SudokuCell>().ToList().ForEach(a => a.PaintCell());
			//this.Visible = true;
		}	
	}
}
