namespace SatSim.WaitForm
{
	partial class WaitingForm_simpleLine
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitingForm_simpleLine));
			this.WaitFormPictureBox = new System.Windows.Forms.PictureBox();
			this.WaitFormLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.WaitFormPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// WaitFormPictureBox
			// 
			this.WaitFormPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.WaitFormPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("WaitFormPictureBox.InitialImage")));
			this.WaitFormPictureBox.Location = new System.Drawing.Point(0, 0);
			this.WaitFormPictureBox.Name = "WaitFormPictureBox";
			this.WaitFormPictureBox.Size = new System.Drawing.Size(212, 212);
			this.WaitFormPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.WaitFormPictureBox.TabIndex = 0;
			this.WaitFormPictureBox.TabStop = false;
			// 
			// WaitFormLabel
			// 
			this.WaitFormLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WaitFormLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.WaitFormLabel.Location = new System.Drawing.Point(0, 212);
			this.WaitFormLabel.Name = "WaitFormLabel";
			this.WaitFormLabel.Size = new System.Drawing.Size(212, 63);
			this.WaitFormLabel.TabIndex = 1;
			this.WaitFormLabel.Text = "WaitFormLabel";
			this.WaitFormLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// WaitingForm_simpleLine
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(212, 275);
			this.ControlBox = false;
			this.Controls.Add(this.WaitFormLabel);
			this.Controls.Add(this.WaitFormPictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WaitingForm_simpleLine";
			this.ShowIcon = false;
			((System.ComponentModel.ISupportInitialize)(this.WaitFormPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.PictureBox WaitFormPictureBox;
		public System.Windows.Forms.Label WaitFormLabel;
	}
}