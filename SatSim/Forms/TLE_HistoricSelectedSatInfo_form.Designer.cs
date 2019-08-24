namespace SatSim.Forms
{
    partial class TLE_HistoricSelectedSatInfo_form
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
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DataLimitSelectorTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.DataLimitTextBox = new System.Windows.Forms.TextBox();
            this.SearchHistoricTLEButton = new System.Windows.Forms.Button();
            this.SelectedSatComboBox = new System.Windows.Forms.ComboBox();
            this.MainTableLayoutPanel.SuspendLayout();
            this.DataLimitSelectorTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 1;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.DataLimitSelectorTableLayoutPanel, 0, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(800, 450);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // DataLimitSelectorTableLayoutPanel
            // 
            this.DataLimitSelectorTableLayoutPanel.ColumnCount = 3;
            this.DataLimitSelectorTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DataLimitSelectorTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.DataLimitSelectorTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.DataLimitSelectorTableLayoutPanel.Controls.Add(this.label1, 1, 0);
            this.DataLimitSelectorTableLayoutPanel.Controls.Add(this.DataLimitTextBox, 2, 0);
            this.DataLimitSelectorTableLayoutPanel.Controls.Add(this.SearchHistoricTLEButton, 2, 1);
            this.DataLimitSelectorTableLayoutPanel.Controls.Add(this.SelectedSatComboBox, 0, 0);
            this.DataLimitSelectorTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataLimitSelectorTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.DataLimitSelectorTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DataLimitSelectorTableLayoutPanel.Name = "DataLimitSelectorTableLayoutPanel";
            this.DataLimitSelectorTableLayoutPanel.RowCount = 2;
            this.DataLimitSelectorTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DataLimitSelectorTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DataLimitSelectorTableLayoutPanel.Size = new System.Drawing.Size(800, 50);
            this.DataLimitSelectorTableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(403, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data limit";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DataLimitTextBox
            // 
            this.DataLimitTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataLimitTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataLimitTextBox.Location = new System.Drawing.Point(643, 3);
            this.DataLimitTextBox.Name = "DataLimitTextBox";
            this.DataLimitTextBox.Size = new System.Drawing.Size(154, 20);
            this.DataLimitTextBox.TabIndex = 1;
            // 
            // SearchHistoricTLEButton
            // 
            this.SearchHistoricTLEButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchHistoricTLEButton.Location = new System.Drawing.Point(643, 28);
            this.SearchHistoricTLEButton.Name = "SearchHistoricTLEButton";
            this.SearchHistoricTLEButton.Size = new System.Drawing.Size(154, 19);
            this.SearchHistoricTLEButton.TabIndex = 2;
            this.SearchHistoricTLEButton.Text = "Search";
            this.SearchHistoricTLEButton.UseVisualStyleBackColor = true;
            this.SearchHistoricTLEButton.Click += new System.EventHandler(this.SearchHistoricTLEButton_Click);
            // 
            // SelectedSatComboBox
            // 
            this.SelectedSatComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedSatComboBox.FormattingEnabled = true;
            this.SelectedSatComboBox.Location = new System.Drawing.Point(3, 3);
            this.SelectedSatComboBox.Name = "SelectedSatComboBox";
            this.SelectedSatComboBox.Size = new System.Drawing.Size(394, 21);
            this.SelectedSatComboBox.TabIndex = 3;
            // 
            // TLE_HistoricSelectedSatInfo_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "TLE_HistoricSelectedSatInfo_form";
            this.Text = "TLE_HistoricSelectedSatInfo_form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TLE_HistoricSelectedSatInfo_form_FormClosing);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.DataLimitSelectorTableLayoutPanel.ResumeLayout(false);
            this.DataLimitSelectorTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel DataLimitSelectorTableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DataLimitTextBox;
        private System.Windows.Forms.Button SearchHistoricTLEButton;
        private System.Windows.Forms.ComboBox SelectedSatComboBox;
    }
}