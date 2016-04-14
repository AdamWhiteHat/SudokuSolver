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

namespace SudokuForm
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{			
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnNewPuzzle = new System.Windows.Forms.Button();
			this.btnSolve = new System.Windows.Forms.Button();
			this.btnSolveEasy = new System.Windows.Forms.Button();
			this.btnSolveModerate = new System.Windows.Forms.Button();
			this.tbDebug = new System.Windows.Forms.TextBox();
			this.btnBrowsePuz = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(320, 454);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(100, 23);
			this.btnRefresh.TabIndex = 2;
			this.btnRefresh.Text = "Refresh Grid";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.BtnRefreshClick);
			// 
			// btnNewPuzzle
			// 
			this.btnNewPuzzle.Location = new System.Drawing.Point(320, 404);
			this.btnNewPuzzle.Name = "btnNewPuzzle";
			this.btnNewPuzzle.Size = new System.Drawing.Size(100, 23);
			this.btnNewPuzzle.TabIndex = 3;
			this.btnNewPuzzle.Text = "Random Puzzle";
			this.btnNewPuzzle.UseVisualStyleBackColor = true;
			this.btnNewPuzzle.Click += new System.EventHandler(this.BtnNewPuzzleClick);
			// 
			// btnSolve
			// 
			this.btnSolve.Location = new System.Drawing.Point(214, 404);
			this.btnSolve.Name = "btnSolve";
			this.btnSolve.Size = new System.Drawing.Size(100, 23);
			this.btnSolve.TabIndex = 5;
			this.btnSolve.Text = "Solve (All)";
			this.btnSolve.UseVisualStyleBackColor = true;
			this.btnSolve.Click += new System.EventHandler(this.BtnSolveClick);
			// 
			// btnSolveEasy
			// 
			this.btnSolveEasy.Location = new System.Drawing.Point(214, 429);
			this.btnSolveEasy.Name = "btnSolveEasy";
			this.btnSolveEasy.Size = new System.Drawing.Size(100, 23);
			this.btnSolveEasy.TabIndex = 7;
			this.btnSolveEasy.Text = "Solve Easy";
			this.btnSolveEasy.UseVisualStyleBackColor = true;
			this.btnSolveEasy.Click += new System.EventHandler(this.BtnSolveEasyClick);
			// 
			// btnSolveModerate
			// 
			this.btnSolveModerate.Location = new System.Drawing.Point(214, 454);
			this.btnSolveModerate.Name = "btnSolveModerate";
			this.btnSolveModerate.Size = new System.Drawing.Size(100, 23);
			this.btnSolveModerate.TabIndex = 8;
			this.btnSolveModerate.Text = "Solve Moderate";
			this.btnSolveModerate.UseVisualStyleBackColor = true;
			this.btnSolveModerate.Click += new System.EventHandler(this.BtnSolveModerateClick);
			// 
			// tbDebug
			// 
			this.tbDebug.Dock = System.Windows.Forms.DockStyle.Right;
			this.tbDebug.Location = new System.Drawing.Point(459, 0);
			this.tbDebug.Multiline = true;
			this.tbDebug.Name = "tbDebug";
			this.tbDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbDebug.Size = new System.Drawing.Size(485, 507);
			this.tbDebug.TabIndex = 9;
			// 
			// btnBrowsePuz
			// 
			this.btnBrowsePuz.Location = new System.Drawing.Point(320, 429);
			this.btnBrowsePuz.Name = "btnBrowsePuz";
			this.btnBrowsePuz.Size = new System.Drawing.Size(100, 23);
			this.btnBrowsePuz.TabIndex = 10;
			this.btnBrowsePuz.Text = "Browse...";
			this.btnBrowsePuz.UseVisualStyleBackColor = true;
			this.btnBrowsePuz.Click += new System.EventHandler(this.BtnBrowsePuzClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(944, 507);
			this.Controls.Add(this.btnBrowsePuz);
			this.Controls.Add(this.tbDebug);
			this.Controls.Add(this.btnSolveModerate);
			this.Controls.Add(this.btnSolveEasy);
			this.Controls.Add(this.btnSolve);
			this.Controls.Add(this.btnNewPuzzle);
			this.Controls.Add(this.btnRefresh);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.Text = "Adam\'s Sudoku Solver - www.csharpprogramming.tips";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnBrowsePuz;
		private System.Windows.Forms.TextBox tbDebug;
		private System.Windows.Forms.Button btnSolveModerate;
		private System.Windows.Forms.Button btnSolveEasy;
		private System.Windows.Forms.Button btnSolve;
		private System.Windows.Forms.Button btnNewPuzzle;
		private System.Windows.Forms.Button btnRefresh;
	}
}
