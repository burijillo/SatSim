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
            this.TLEHistoricDataSetInformationTab = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.MainTLEHistoricInfoDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.TLEDataCountTextBox = new System.Windows.Forms.TextBox();
            this.MainTableLayoutPanel.SuspendLayout();
            this.DataLimitSelectorTableLayoutPanel.SuspendLayout();
            this.TLEHistoricDataSetInformationTab.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainTLEHistoricInfoDataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.ColumnCount = 1;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.DataLimitSelectorTableLayoutPanel, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.tabControl1, 0, 1);
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
            // TLEHistoricDataSetInformationTab
            // 
            this.TLEHistoricDataSetInformationTab.Controls.Add(this.tableLayoutPanel1);
            this.TLEHistoricDataSetInformationTab.Location = new System.Drawing.Point(4, 22);
            this.TLEHistoricDataSetInformationTab.Name = "TLEHistoricDataSetInformationTab";
            this.TLEHistoricDataSetInformationTab.Padding = new System.Windows.Forms.Padding(3);
            this.TLEHistoricDataSetInformationTab.Size = new System.Drawing.Size(786, 368);
            this.TLEHistoricDataSetInformationTab.TabIndex = 0;
            this.TLEHistoricDataSetInformationTab.Text = "TLE information";
            this.TLEHistoricDataSetInformationTab.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TLEHistoricDataSetInformationTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(794, 394);
            this.tabControl1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.MainTLEHistoricInfoDataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 362);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // MainTLEHistoricInfoDataGridView
            // 
            this.MainTLEHistoricInfoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainTLEHistoricInfoDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTLEHistoricInfoDataGridView.Location = new System.Drawing.Point(3, 153);
            this.MainTLEHistoricInfoDataGridView.Name = "MainTLEHistoricInfoDataGridView";
            this.MainTLEHistoricInfoDataGridView.Size = new System.Drawing.Size(774, 206);
            this.MainTLEHistoricInfoDataGridView.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.TLEDataCountTextBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(780, 150);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "TLE data count";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TLEDataCountTextBox
            // 
            this.TLEDataCountTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLEDataCountTextBox.Location = new System.Drawing.Point(237, 3);
            this.TLEDataCountTextBox.Name = "TLEDataCountTextBox";
            this.TLEDataCountTextBox.ReadOnly = true;
            this.TLEDataCountTextBox.Size = new System.Drawing.Size(150, 20);
            this.TLEDataCountTextBox.TabIndex = 1;
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
            this.TLEHistoricDataSetInformationTab.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainTLEHistoricInfoDataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel DataLimitSelectorTableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DataLimitTextBox;
        private System.Windows.Forms.Button SearchHistoricTLEButton;
        private System.Windows.Forms.ComboBox SelectedSatComboBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TLEHistoricDataSetInformationTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView MainTLEHistoricInfoDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TLEDataCountTextBox;
    }
}