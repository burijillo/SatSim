namespace SatSim.Visualization_Graphs
{
	partial class MainGraphVisualization_form
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.VisualizationGraphToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.SatSelectedEventToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.ControllerGroupBox = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.IterationsNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.ResetSeriesButton = new System.Windows.Forms.Button();
			this.AddButton = new System.Windows.Forms.Button();
			this.SeriesTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.GraphPanel = new System.Windows.Forms.Panel();
			this.MainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.VisualizationGraphToolStrip.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.mainTableLayoutPanel.SuspendLayout();
			this.ControllerGroupBox.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.IterationsNumericUpDown)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.GraphPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainChart)).BeginInit();
			this.SuspendLayout();
			// 
			// VisualizationGraphToolStrip
			// 
			this.VisualizationGraphToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.VisualizationGraphToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.SatSelectedEventToolStripLabel,
            this.toolStripSeparator1});
			this.VisualizationGraphToolStrip.Location = new System.Drawing.Point(0, 552);
			this.VisualizationGraphToolStrip.Name = "VisualizationGraphToolStrip";
			this.VisualizationGraphToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.VisualizationGraphToolStrip.Size = new System.Drawing.Size(1339, 25);
			this.VisualizationGraphToolStrip.TabIndex = 0;
			this.VisualizationGraphToolStrip.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(72, 22);
			this.toolStripLabel1.Text = "Selected sat:";
			this.toolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SatSelectedEventToolStripLabel
			// 
			this.SatSelectedEventToolStripLabel.AutoSize = false;
			this.SatSelectedEventToolStripLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.SatSelectedEventToolStripLabel.Name = "SatSelectedEventToolStripLabel";
			this.SatSelectedEventToolStripLabel.Size = new System.Drawing.Size(100, 22);
			this.SatSelectedEventToolStripLabel.Text = "Not selected";
			this.SatSelectedEventToolStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1339, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// mainTableLayoutPanel
			// 
			this.mainTableLayoutPanel.ColumnCount = 2;
			this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.mainTableLayoutPanel.Controls.Add(this.ControllerGroupBox, 1, 0);
			this.mainTableLayoutPanel.Controls.Add(this.GraphPanel, 0, 0);
			this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 24);
			this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
			this.mainTableLayoutPanel.RowCount = 1;
			this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTableLayoutPanel.Size = new System.Drawing.Size(1339, 528);
			this.mainTableLayoutPanel.TabIndex = 2;
			// 
			// ControllerGroupBox
			// 
			this.ControllerGroupBox.Controls.Add(this.panel1);
			this.ControllerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ControllerGroupBox.Location = new System.Drawing.Point(940, 3);
			this.ControllerGroupBox.Name = "ControllerGroupBox";
			this.ControllerGroupBox.Size = new System.Drawing.Size(396, 522);
			this.ControllerGroupBox.TabIndex = 0;
			this.ControllerGroupBox.TabStop = false;
			this.ControllerGroupBox.Text = "Main controller";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(390, 84);
			this.panel1.TabIndex = 5;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.IterationsNumericUpDown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.SeriesTypeComboBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 84);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(150, 28);
			this.label2.TabIndex = 6;
			this.label2.Text = "Iterations";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// IterationsNumericUpDown
			// 
			this.IterationsNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.IterationsNumericUpDown.Location = new System.Drawing.Point(159, 31);
			this.IterationsNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.IterationsNumericUpDown.Name = "IterationsNumericUpDown";
			this.IterationsNumericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.IterationsNumericUpDown.Size = new System.Drawing.Size(228, 20);
			this.IterationsNumericUpDown.TabIndex = 2;
			this.IterationsNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.IterationsNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.ResetSeriesButton, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.AddButton, 0, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(156, 56);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(190, 28);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// ResetSeriesButton
			// 
			this.ResetSeriesButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResetSeriesButton.Location = new System.Drawing.Point(98, 3);
			this.ResetSeriesButton.Name = "ResetSeriesButton";
			this.ResetSeriesButton.Size = new System.Drawing.Size(89, 22);
			this.ResetSeriesButton.TabIndex = 4;
			this.ResetSeriesButton.Text = "Reset series";
			this.ResetSeriesButton.UseVisualStyleBackColor = true;
			this.ResetSeriesButton.Click += new System.EventHandler(this.ResetSeriesButton_Click);
			// 
			// AddButton
			// 
			this.AddButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AddButton.Location = new System.Drawing.Point(3, 3);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(89, 22);
			this.AddButton.TabIndex = 3;
			this.AddButton.Text = "Add series";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// SeriesTypeComboBox
			// 
			this.SeriesTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SeriesTypeComboBox.FormattingEnabled = true;
			this.SeriesTypeComboBox.Location = new System.Drawing.Point(159, 3);
			this.SeriesTypeComboBox.Name = "SeriesTypeComboBox";
			this.SeriesTypeComboBox.Size = new System.Drawing.Size(228, 21);
			this.SeriesTypeComboBox.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(150, 28);
			this.label1.TabIndex = 5;
			this.label1.Text = "Serie type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GraphPanel
			// 
			this.GraphPanel.Controls.Add(this.MainChart);
			this.GraphPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GraphPanel.Location = new System.Drawing.Point(3, 3);
			this.GraphPanel.Name = "GraphPanel";
			this.GraphPanel.Size = new System.Drawing.Size(931, 522);
			this.GraphPanel.TabIndex = 1;
			// 
			// MainChart
			// 
			this.MainChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainChart.Location = new System.Drawing.Point(0, 0);
			this.MainChart.Name = "MainChart";
			this.MainChart.Size = new System.Drawing.Size(931, 522);
			this.MainChart.TabIndex = 0;
			// 
			// MainGraphVisualization_form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1339, 577);
			this.Controls.Add(this.mainTableLayoutPanel);
			this.Controls.Add(this.VisualizationGraphToolStrip);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainGraphVisualization_form";
			this.Text = "Plots controller";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainGraphVisualization_form_FormClosing);
			this.VisualizationGraphToolStrip.ResumeLayout(false);
			this.VisualizationGraphToolStrip.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.mainTableLayoutPanel.ResumeLayout(false);
			this.ControllerGroupBox.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.IterationsNumericUpDown)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.GraphPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainChart)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip VisualizationGraphToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripLabel SatSelectedEventToolStripLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
		private System.Windows.Forms.GroupBox ControllerGroupBox;
		private System.Windows.Forms.Panel GraphPanel;
		private System.Windows.Forms.DataVisualization.Charting.Chart MainChart;
		private System.Windows.Forms.NumericUpDown IterationsNumericUpDown;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.ComboBox SeriesTypeComboBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button ResetSeriesButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	}
}