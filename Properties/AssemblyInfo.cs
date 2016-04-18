#region Using directives

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using SudokuGame;
using MultiKeyDictionaries;

#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SudokuSolver_AJR")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("www.csharpprogramming.tips")]
[assembly: AssemblyProduct("SudokuSolver_AJR")]
[assembly: AssemblyCopyright("Copyright 2016 Adam Rakaska")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// DebuggerDisplay Attributes
[assembly: DebuggerDisplay(@"\{Value = {Value}}", Target = typeof(SudokuCell))]
//[assembly: DebuggerDisplay(@"\{ {EventType} : C = {Column} R = {Row} B = {Block} Values = {Values}}", Target = typeof(CellEventInfo))]
//[assembly: DebuggerDisplay(@"\{Value = {Enumerator}}", Target = typeof(RankingDictionary<T>))]
//[assembly: DebuggerDisplay(@"\{Value = {Enumerator}}", Target = typeof(FrequencyDictionary<T>))]


// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
