namespace SatSim.Forms
{
	partial class TLE_SelectedSatInfo_form
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
			this.AdditionalInfoRichTextBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// AdditionalInfoRichTextBox
			// 
			this.AdditionalInfoRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.AdditionalInfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AdditionalInfoRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AdditionalInfoRichTextBox.Location = new System.Drawing.Point(0, 0);
			this.AdditionalInfoRichTextBox.Name = "AdditionalInfoRichTextBox";
			this.AdditionalInfoRichTextBox.ReadOnly = true;
			this.AdditionalInfoRichTextBox.Size = new System.Drawing.Size(330, 245);
			this.AdditionalInfoRichTextBox.TabIndex = 0;
			this.AdditionalInfoRichTextBox.Text = "";
			// 
			// TLE_SelectedSatInfo_form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(330, 245);
			this.Controls.Add(this.AdditionalInfoRichTextBox);
			this.Name = "TLE_SelectedSatInfo_form";
			this.Text = "TLE_SelectedSatInfo_form";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TLE_SelectedSatInfo_form_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.RichTextBox AdditionalInfoRichTextBox;
	}
}