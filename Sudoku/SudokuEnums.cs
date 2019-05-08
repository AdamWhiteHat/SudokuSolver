/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
namespace SudokuGame
{
	public enum SudokuRegion
	{
		Column,
		Row,
		Block
	}
	
	public enum VerticalLocation
	{
		Top = 0,
		Middle = 1,
		Bottom = 2
	}
	
	public enum HorizontalLocation
	{
		Left = 0,
		Center = 1,
		Right = 2
	}
	
	public enum CellEventType
	{
		RemovedCandidates,
		SetValue
	}
}
