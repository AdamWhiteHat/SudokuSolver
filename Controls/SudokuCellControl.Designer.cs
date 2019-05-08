using EditLabelControl;

namespace SudokuGame.Controls
{
	partial class SudokuCellControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.editLabel = new EditLabelControl.EditLabel(null, "");
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.candidate1 = new System.Windows.Forms.Label();
			this.candidate2 = new System.Windows.Forms.Label();
			this.candidate3 = new System.Windows.Forms.Label();
			this.candidate4 = new System.Windows.Forms.Label();
			this.candidate5 = new System.Windows.Forms.Label();
			this.candidate6 = new System.Windows.Forms.Label();
			this.candidate7 = new System.Windows.Forms.Label();
			this.candidate8 = new System.Windows.Forms.Label();
			this.candidate9 = new System.Windows.Forms.Label();
			this.flowLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// editLabel
			// 
			this.editLabel.AutoSize = true;
			this.editLabel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.editLabel.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.editLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.editLabel.IsEditable = true;
			this.editLabel.Location = new System.Drawing.Point(0, 0);
			this.editLabel.Margin = new System.Windows.Forms.Padding(0);
			this.editLabel.MinimumSize = new System.Drawing.Size(0, 13);
			this.editLabel.Name = "editLabel";
			this.editLabel.Size = new System.Drawing.Size(49, 41);
			this.editLabel.TabIndex = 0;
			this.editLabel.Text = "";
			this.editLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Controls.Add(this.candidate1);
			this.flowLayoutPanel.Controls.Add(this.candidate2);
			this.flowLayoutPanel.Controls.Add(this.candidate3);
			this.flowLayoutPanel.Controls.Add(this.candidate4);
			this.flowLayoutPanel.Controls.Add(this.candidate5);
			this.flowLayoutPanel.Controls.Add(this.candidate6);
			this.flowLayoutPanel.Controls.Add(this.candidate7);
			this.flowLayoutPanel.Controls.Add(this.candidate8);
			this.flowLayoutPanel.Controls.Add(this.candidate9);
			this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(49, 41);
			this.flowLayoutPanel.TabIndex = 1;
			// 
			// candidate1
			// 
			this.candidate1.AutoSize = true;
			this.candidate1.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate1.Location = new System.Drawing.Point(0, 0);
			this.candidate1.Margin = new System.Windows.Forms.Padding(0);
			this.candidate1.Name = "candidate1";
			this.candidate1.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate1.Size = new System.Drawing.Size(16, 13);
			this.candidate1.TabIndex = 0;
			this.candidate1.Text = "1";
			// 
			// candidate2
			// 
			this.candidate2.AutoSize = true;
			this.candidate2.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate2.Location = new System.Drawing.Point(16, 0);
			this.candidate2.Margin = new System.Windows.Forms.Padding(0);
			this.candidate2.Name = "candidate2";
			this.candidate2.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate2.Size = new System.Drawing.Size(16, 13);
			this.candidate2.TabIndex = 1;
			this.candidate2.Text = "2";
			// 
			// candidate3
			// 
			this.candidate3.AutoSize = true;
			this.candidate3.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate3.Location = new System.Drawing.Point(32, 0);
			this.candidate3.Margin = new System.Windows.Forms.Padding(0);
			this.candidate3.Name = "candidate3";
			this.candidate3.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate3.Size = new System.Drawing.Size(16, 13);
			this.candidate3.TabIndex = 2;
			this.candidate3.Text = "3";
			// 
			// candidate4
			// 
			this.candidate4.AutoSize = true;
			this.candidate4.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate4.Location = new System.Drawing.Point(0, 13);
			this.candidate4.Margin = new System.Windows.Forms.Padding(0);
			this.candidate4.Name = "candidate4";
			this.candidate4.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate4.Size = new System.Drawing.Size(16, 13);
			this.candidate4.TabIndex = 3;
			this.candidate4.Text = "4";
			// 
			// candidate5
			// 
			this.candidate5.AutoSize = true;
			this.candidate5.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate5.Location = new System.Drawing.Point(16, 13);
			this.candidate5.Margin = new System.Windows.Forms.Padding(0);
			this.candidate5.Name = "candidate5";
			this.candidate5.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate5.Size = new System.Drawing.Size(16, 13);
			this.candidate5.TabIndex = 4;
			this.candidate5.Text = "5";
			// 
			// candidate6
			// 
			this.candidate6.AutoSize = true;
			this.candidate6.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate6.Location = new System.Drawing.Point(32, 13);
			this.candidate6.Margin = new System.Windows.Forms.Padding(0);
			this.candidate6.Name = "candidate6";
			this.candidate6.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate6.Size = new System.Drawing.Size(16, 13);
			this.candidate6.TabIndex = 5;
			this.candidate6.Text = "6";
			// 
			// candidate7
			// 
			this.candidate7.AutoSize = true;
			this.candidate7.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate7.Location = new System.Drawing.Point(0, 26);
			this.candidate7.Margin = new System.Windows.Forms.Padding(0);
			this.candidate7.Name = "candidate7";
			this.candidate7.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate7.Size = new System.Drawing.Size(16, 13);
			this.candidate7.TabIndex = 6;
			this.candidate7.Text = "7";
			// 
			// candidate8
			// 
			this.candidate8.AutoSize = true;
			this.candidate8.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate8.Location = new System.Drawing.Point(16, 26);
			this.candidate8.Margin = new System.Windows.Forms.Padding(0);
			this.candidate8.Name = "candidate8";
			this.candidate8.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate8.Size = new System.Drawing.Size(16, 13);
			this.candidate8.TabIndex = 7;
			this.candidate8.Text = "8";
			// 
			// candidate9
			// 
			this.candidate9.AutoSize = true;
			this.candidate9.Font = new System.Drawing.Font("Consolas", 8F);
			this.candidate9.Location = new System.Drawing.Point(32, 26);
			this.candidate9.Margin = new System.Windows.Forms.Padding(0);
			this.candidate9.Name = "candidate9";
			this.candidate9.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
			this.candidate9.Size = new System.Drawing.Size(16, 13);
			this.candidate9.TabIndex = 8;
			this.candidate9.Text = "9";
			// 
			// SudokuCellControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.flowLayoutPanel);
			this.Controls.Add(this.editLabel);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "SudokuCellControl";
			this.Size = new System.Drawing.Size(49, 41);
			this.flowLayoutPanel.ResumeLayout(false);
			this.flowLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private EditLabelControl.EditLabel editLabel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.Label candidate1;
		private System.Windows.Forms.Label candidate2;
		private System.Windows.Forms.Label candidate3;
		private System.Windows.Forms.Label candidate4;
		private System.Windows.Forms.Label candidate5;
		private System.Windows.Forms.Label candidate6;
		private System.Windows.Forms.Label candidate7;
		private System.Windows.Forms.Label candidate8;
		private System.Windows.Forms.Label candidate9;		
	}
}
