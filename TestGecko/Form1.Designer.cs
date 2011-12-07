namespace TestGecko
{
	partial class Form1
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
			this.lbNotSorted = new System.Windows.Forms.ListBox();
			this.lbSorted = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnReseed = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.lblElapsed = new System.Windows.Forms.Label();
			this.nudMax = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.nudMin = new System.Windows.Forms.NumericUpDown();
			this.btnClean = new System.Windows.Forms.Button();
			this.btnSort = new System.Windows.Forms.Button();
			this.comboAlgorithms = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lblPath = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnStartParse = new System.Windows.Forms.Button();
			this.lblRetCode = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtLogs = new System.Windows.Forms.TextBox();
			this.dgvFile = new System.Windows.Forms.DataGridView();
			this.btnSearch = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.txtBody = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.txtTo = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txtFrom = new System.Windows.Forms.TextBox();
			this.bgwTextFileParser = new System.ComponentModel.BackgroundWorker();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMax)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbNotSorted
			// 
			this.lbNotSorted.FormattingEnabled = true;
			this.lbNotSorted.Location = new System.Drawing.Point(6, 32);
			this.lbNotSorted.Name = "lbNotSorted";
			this.lbNotSorted.Size = new System.Drawing.Size(120, 199);
			this.lbNotSorted.TabIndex = 0;
			// 
			// lbSorted
			// 
			this.lbSorted.FormattingEnabled = true;
			this.lbSorted.Location = new System.Drawing.Point(132, 32);
			this.lbSorted.Name = "lbSorted";
			this.lbSorted.Size = new System.Drawing.Size(120, 199);
			this.lbSorted.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Not Sorted";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnReseed);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.lblElapsed);
			this.groupBox1.Controls.Add(this.nudMax);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.nudMin);
			this.groupBox1.Controls.Add(this.btnClean);
			this.groupBox1.Controls.Add(this.btnSort);
			this.groupBox1.Controls.Add(this.comboAlgorithms);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.lbSorted);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.lbNotSorted);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(258, 340);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Sortings";
			// 
			// btnReseed
			// 
			this.btnReseed.Location = new System.Drawing.Point(198, 274);
			this.btnReseed.Name = "btnReseed";
			this.btnReseed.Size = new System.Drawing.Size(54, 23);
			this.btnReseed.TabIndex = 13;
			this.btnReseed.Text = "Reseed";
			this.btnReseed.UseVisualStyleBackColor = true;
			this.btnReseed.Click += new System.EventHandler(this.btnReseed_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 300);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Elapsed Time (ms):";
			// 
			// lblElapsed
			// 
			this.lblElapsed.BackColor = System.Drawing.SystemColors.Window;
			this.lblElapsed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblElapsed.Location = new System.Drawing.Point(6, 313);
			this.lblElapsed.Name = "lblElapsed";
			this.lblElapsed.Size = new System.Drawing.Size(246, 24);
			this.lblElapsed.TabIndex = 11;
			this.lblElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nudMax
			// 
			this.nudMax.Location = new System.Drawing.Point(102, 277);
			this.nudMax.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.nudMax.Name = "nudMax";
			this.nudMax.Size = new System.Drawing.Size(90, 20);
			this.nudMax.TabIndex = 10;
			this.nudMax.Value = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(99, 261);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(27, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Max";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 261);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Min";
			// 
			// nudMin
			// 
			this.nudMin.Location = new System.Drawing.Point(6, 277);
			this.nudMin.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.nudMin.Name = "nudMin";
			this.nudMin.Size = new System.Drawing.Size(90, 20);
			this.nudMin.TabIndex = 7;
			// 
			// btnClean
			// 
			this.btnClean.Location = new System.Drawing.Point(165, 237);
			this.btnClean.Name = "btnClean";
			this.btnClean.Size = new System.Drawing.Size(42, 21);
			this.btnClean.TabIndex = 6;
			this.btnClean.Text = "Clean";
			this.btnClean.UseVisualStyleBackColor = true;
			this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
			// 
			// btnSort
			// 
			this.btnSort.Location = new System.Drawing.Point(212, 237);
			this.btnSort.Name = "btnSort";
			this.btnSort.Size = new System.Drawing.Size(40, 21);
			this.btnSort.TabIndex = 5;
			this.btnSort.Text = "Sort";
			this.btnSort.UseVisualStyleBackColor = true;
			this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
			// 
			// comboAlgorithms
			// 
			this.comboAlgorithms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAlgorithms.FormattingEnabled = true;
			this.comboAlgorithms.Location = new System.Drawing.Point(6, 237);
			this.comboAlgorithms.Name = "comboAlgorithms";
			this.comboAlgorithms.Size = new System.Drawing.Size(153, 21);
			this.comboAlgorithms.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(129, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Sorted";
			// 
			// lblPath
			// 
			this.lblPath.BackColor = System.Drawing.SystemColors.Window;
			this.lblPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPath.Location = new System.Drawing.Point(6, 19);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(507, 23);
			this.lblPath.TabIndex = 4;
			this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnStartParse);
			this.groupBox2.Controls.Add(this.lblRetCode);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.txtLogs);
			this.groupBox2.Controls.Add(this.dgvFile);
			this.groupBox2.Controls.Add(this.btnSearch);
			this.groupBox2.Controls.Add(this.lblPath);
			this.groupBox2.Location = new System.Drawing.Point(276, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(554, 340);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Text File Parser";
			// 
			// btnStartParse
			// 
			this.btnStartParse.Location = new System.Drawing.Point(510, 231);
			this.btnStartParse.Name = "btnStartParse";
			this.btnStartParse.Size = new System.Drawing.Size(38, 24);
			this.btnStartParse.TabIndex = 11;
			this.btnStartParse.Text = "Go!";
			this.btnStartParse.UseVisualStyleBackColor = true;
			this.btnStartParse.Click += new System.EventHandler(this.btnStartParse_Click);
			// 
			// lblRetCode
			// 
			this.lblRetCode.AutoSize = true;
			this.lblRetCode.Location = new System.Drawing.Point(292, 245);
			this.lblRetCode.Name = "lblRetCode";
			this.lblRetCode.Size = new System.Drawing.Size(13, 13);
			this.lblRetCode.TabIndex = 10;
			this.lblRetCode.Text = "0";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(216, 245);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70, 13);
			this.label7.TabIndex = 9;
			this.label7.Text = "Return Code:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 245);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(33, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Logs:";
			// 
			// txtLogs
			// 
			this.txtLogs.BackColor = System.Drawing.SystemColors.Window;
			this.txtLogs.Location = new System.Drawing.Point(6, 261);
			this.txtLogs.Multiline = true;
			this.txtLogs.Name = "txtLogs";
			this.txtLogs.ReadOnly = true;
			this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLogs.Size = new System.Drawing.Size(542, 73);
			this.txtLogs.TabIndex = 7;
			// 
			// dgvFile
			// 
			this.dgvFile.AllowUserToAddRows = false;
			this.dgvFile.AllowUserToDeleteRows = false;
			this.dgvFile.AllowUserToResizeColumns = false;
			this.dgvFile.AllowUserToResizeRows = false;
			this.dgvFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dgvFile.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvFile.Location = new System.Drawing.Point(6, 45);
			this.dgvFile.Name = "dgvFile";
			this.dgvFile.ReadOnly = true;
			this.dgvFile.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.dgvFile.Size = new System.Drawing.Size(542, 180);
			this.dgvFile.TabIndex = 6;
			this.dgvFile.VirtualMode = true;
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(519, 19);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(29, 23);
			this.btnSearch.TabIndex = 5;
			this.btnSearch.Text = "...";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnSend);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.txtServer);
			this.groupBox3.Controls.Add(this.txtBody);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.txtSubject);
			this.groupBox3.Controls.Add(this.txtTo);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.txtFrom);
			this.groupBox3.Location = new System.Drawing.Point(12, 358);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(818, 266);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Mail Agent";
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(372, 231);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 10;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 234);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(41, 13);
			this.label12.TabIndex = 9;
			this.label12.Text = "Server:";
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(58, 231);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(100, 20);
			this.txtServer.TabIndex = 8;
			// 
			// txtBody
			// 
			this.txtBody.Location = new System.Drawing.Point(58, 97);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtBody.Size = new System.Drawing.Size(754, 128);
			this.txtBody.TabIndex = 7;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 100);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(34, 13);
			this.label11.TabIndex = 6;
			this.label11.Text = "Body:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 74);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(46, 13);
			this.label10.TabIndex = 5;
			this.label10.Text = "Subject:";
			// 
			// txtSubject
			// 
			this.txtSubject.Location = new System.Drawing.Point(58, 71);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(754, 20);
			this.txtSubject.TabIndex = 4;
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(58, 45);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(754, 20);
			this.txtTo.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 48);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(23, 13);
			this.label9.TabIndex = 2;
			this.label9.Text = "To:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(33, 13);
			this.label8.TabIndex = 1;
			this.label8.Text = "From:";
			// 
			// txtFrom
			// 
			this.txtFrom.Location = new System.Drawing.Point(58, 19);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.Size = new System.Drawing.Size(754, 20);
			this.txtFrom.TabIndex = 0;
			// 
			// bgwTextFileParser
			// 
			this.bgwTextFileParser.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTextFileParser_DoWork);
			this.bgwTextFileParser.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTextFileParser_RunWorkerCompleted);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(842, 636);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Test";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMax)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lbNotSorted;
		private System.Windows.Forms.ListBox lbSorted;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboAlgorithms;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.DataGridView dgvFile;
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.Button btnClean;
		private System.Windows.Forms.Label lblElapsed;
		private System.Windows.Forms.NumericUpDown nudMax;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudMin;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnReseed;
		private System.Windows.Forms.Label lblRetCode;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtLogs;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtBody;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtServer;
		private System.ComponentModel.BackgroundWorker bgwTextFileParser;
		private System.Windows.Forms.Button btnStartParse;

	}
}

