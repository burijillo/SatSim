namespace SatSim
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.MainFormMenuStrip = new System.Windows.Forms.MenuStrip();
			this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.historicTLEDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tLEDataSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.MainFormToolStrip = new System.Windows.Forms.ToolStrip();
			this.TLELoadedEventToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.SatSelectedEventToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.MainFormMenuStrip.SuspendLayout();
			this.MainFormToolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// MainFormMenuStrip
			// 
			this.MainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.historicTLEDataToolStripMenuItem});
			this.MainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MainFormMenuStrip.Name = "MainFormMenuStrip";
			this.MainFormMenuStrip.Size = new System.Drawing.Size(800, 24);
			this.MainFormMenuStrip.TabIndex = 0;
			// 
			// configurationToolStripMenuItem
			// 
			this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDatabaseToolStripMenuItem});
			this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
			this.configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
			this.configurationToolStripMenuItem.Text = "Configuration";
			// 
			// loadDatabaseToolStripMenuItem
			// 
			this.loadDatabaseToolStripMenuItem.Name = "loadDatabaseToolStripMenuItem";
			this.loadDatabaseToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.loadDatabaseToolStripMenuItem.Text = "Load Actual Database";
			this.loadDatabaseToolStripMenuItem.Click += new System.EventHandler(this.loadDatabaseToolStripMenuItem_Click);
			// 
			// historicTLEDataToolStripMenuItem
			// 
			this.historicTLEDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tLEDataSelectionToolStripMenuItem});
			this.historicTLEDataToolStripMenuItem.Name = "historicTLEDataToolStripMenuItem";
			this.historicTLEDataToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
			this.historicTLEDataToolStripMenuItem.Text = "Historic TLE Data";
			// 
			// tLEDataSelectionToolStripMenuItem
			// 
			this.tLEDataSelectionToolStripMenuItem.Name = "tLEDataSelectionToolStripMenuItem";
			this.tLEDataSelectionToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.tLEDataSelectionToolStripMenuItem.Text = "TLE Data Selection";
			this.tLEDataSelectionToolStripMenuItem.Click += new System.EventHandler(this.tLEDataSelectionToolStripMenuItem_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(54, 80);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// MainFormToolStrip
			// 
			this.MainFormToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.MainFormToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TLELoadedEventToolStripLabel,
            this.toolStripLabel1,
            this.SatSelectedEventToolStripLabel,
            this.toolStripSeparator1});
			this.MainFormToolStrip.Location = new System.Drawing.Point(0, 425);
			this.MainFormToolStrip.Name = "MainFormToolStrip";
			this.MainFormToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.MainFormToolStrip.Size = new System.Drawing.Size(800, 25);
			this.MainFormToolStrip.TabIndex = 2;
			// 
			// TLELoadedEventToolStripLabel
			// 
			this.TLELoadedEventToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.TLELoadedEventToolStripLabel.AutoSize = false;
			this.TLELoadedEventToolStripLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.TLELoadedEventToolStripLabel.Name = "TLELoadedEventToolStripLabel";
			this.TLELoadedEventToolStripLabel.Size = new System.Drawing.Size(86, 22);
			this.TLELoadedEventToolStripLabel.Text = "TLE not loaded";
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
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(54, 118);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(54, 166);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 5;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(54, 220);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// chart1
			// 
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(367, 60);
			this.chart1.Name = "chart1";
			series1.ChartArea = "ChartArea1";
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			this.chart1.Series.Add(series1);
			this.chart1.Size = new System.Drawing.Size(421, 320);
			this.chart1.TabIndex = 7;
			this.chart1.Text = "chart1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.MainFormToolStrip);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.MainFormMenuStrip);
			this.MainMenuStrip = this.MainFormMenuStrip;
			this.Name = "MainForm";
			this.Text = "Sim Sat";
			this.MainFormMenuStrip.ResumeLayout(false);
			this.MainFormMenuStrip.PerformLayout();
			this.MainFormToolStrip.ResumeLayout(false);
			this.MainFormToolStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.MenuStrip MainFormMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadDatabaseToolStripMenuItem;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolStrip MainFormToolStrip;
		private System.Windows.Forms.ToolStripLabel TLELoadedEventToolStripLabel;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripLabel SatSelectedEventToolStripLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.ToolStripMenuItem historicTLEDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tLEDataSelectionToolStripMenuItem;
	}
}

