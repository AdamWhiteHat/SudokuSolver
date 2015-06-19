/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 *    http://arakaska.wix.com/intelligentsoftware
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
