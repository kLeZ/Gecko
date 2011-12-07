namespace Gecko.UI.WinForms
{
	partial class PasswordStrengthControl
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
			this.lbText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbText
			// 
			this.lbText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbText.BackColor = System.Drawing.Color.Transparent;
			this.lbText.Location = new System.Drawing.Point(0, 0);
			this.lbText.Margin = new System.Windows.Forms.Padding(0);
			this.lbText.Name = "lbText";
			this.lbText.Size = new System.Drawing.Size(107, 22);
			this.lbText.TabIndex = 0;
			this.lbText.Text = "Week";
			this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PasswordStrengthControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.lbText);
			this.Name = "PasswordStrengthControl";
			this.Size = new System.Drawing.Size(105, 22);
			this.Load += new System.EventHandler(this.PasswordStrengthControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbText;
	}
}
