# SudokuSolverAJR
A Sudoku board with logic-based solving routines






Starts with a board such as this. The bolded numbers are the givens. The small numbers are the pencil markings tracking the candidates:
![Screenshot](https://github.com/AdamWhiteHat/SudokuSolverAJR/blob/master/Screenshot001.png)

Clicking Solve will resolve the board to this. The single (larger) digits in the center of a cell are the solutions for that cell that were found:
![Screenshot](https://github.com/AdamWhiteHat/SudokuSolverAJR/blob/master/Screenshot002.png)

SOME NOTES:
 - The solver does not yet support X-Wing and more advanced moves.
 - Because the solvers use only logic to solve the Sudoku puzzles, it will not be able to solve advanced puzzles that require making a guess between two possible candidates to proceed.
 - To solve all puzzles, the solver would have to support guess and check, with backtracking. Perhaps a feature I'll add in the future.
 - It is possible to take the solver even further by using constraint solving techniques, and in particular, looking at the inverse of the constraints as you normally think of them.
 - The paper I was reading on constraint solving for Sudoku claimed that any Sudoku board could be solved in this way, but I do not believe that to be the case.

SOME ADDITIONAL COMMENTS:
 - After I learned that not all Sudoku boards can be solved by pure logic alone, I became disenchanted with the game and this project. But the project was fun to make, and should be interesting to others.
 - I think I made some good coding design choices that made the more advanced logic easy to implement.
 - I made this project before I fully understood the evils of inheritance.
 
 
