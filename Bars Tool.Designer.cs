﻿namespace BarsTool {
	partial class BarsViewerForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarsViewerForm));
			this.filePathTB = new System.Windows.Forms.TextBox();
			this.fileChooseB = new System.Windows.Forms.Button();
			this.fileOpenB = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.fileLB = new System.Windows.Forms.ListBox();
			this.extractB = new System.Windows.Forms.Button();
			this.replaceB = new System.Windows.Forms.Button();
			this.consoleTB = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.searchTB = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.fileInfoTB = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.sortCB = new System.Windows.Forms.CheckBox();
			this.selectAllB = new System.Windows.Forms.Button();
			this.previewB = new System.Windows.Forms.Button();
			this.rstbChooseB = new System.Windows.Forms.Button();
			this.rstbPathTB = new System.Windows.Forms.TextBox();
			this.rstbUpdateB = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// filePathTB
			// 
			this.filePathTB.Enabled = false;
			this.filePathTB.Location = new System.Drawing.Point(12, 25);
			this.filePathTB.Name = "filePathTB";
			this.filePathTB.Size = new System.Drawing.Size(533, 20);
			this.filePathTB.TabIndex = 0;
			// 
			// fileChooseB
			// 
			this.fileChooseB.Location = new System.Drawing.Point(551, 23);
			this.fileChooseB.Name = "fileChooseB";
			this.fileChooseB.Size = new System.Drawing.Size(73, 23);
			this.fileChooseB.TabIndex = 1;
			this.fileChooseB.Text = "Choose File";
			this.fileChooseB.UseVisualStyleBackColor = true;
			this.fileChooseB.Click += new System.EventHandler(this.FileChooseB_Click);
			// 
			// fileOpenB
			// 
			this.fileOpenB.Location = new System.Drawing.Point(672, 23);
			this.fileOpenB.Name = "fileOpenB";
			this.fileOpenB.Size = new System.Drawing.Size(55, 23);
			this.fileOpenB.TabIndex = 2;
			this.fileOpenB.Text = "Open";
			this.fileOpenB.UseVisualStyleBackColor = true;
			this.fileOpenB.Click += new System.EventHandler(this.FileOpenB_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 106);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Files:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = ".bars file path:";
			// 
			// fileLB
			// 
			this.fileLB.FormattingEnabled = true;
			this.fileLB.Location = new System.Drawing.Point(12, 122);
			this.fileLB.Name = "fileLB";
			this.fileLB.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.fileLB.Size = new System.Drawing.Size(379, 459);
			this.fileLB.TabIndex = 7;
			this.fileLB.SelectedIndexChanged += new System.EventHandler(this.FileLB_SelectedIndexChanged);
			this.fileLB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileLB_MouseDoubleClick);
			// 
			// extractB
			// 
			this.extractB.Enabled = false;
			this.extractB.Location = new System.Drawing.Point(397, 246);
			this.extractB.Name = "extractB";
			this.extractB.Size = new System.Drawing.Size(75, 23);
			this.extractB.TabIndex = 8;
			this.extractB.Text = "Export";
			this.extractB.UseVisualStyleBackColor = true;
			this.extractB.Click += new System.EventHandler(this.ExtractB_Click);
			// 
			// replaceB
			// 
			this.replaceB.Enabled = false;
			this.replaceB.Location = new System.Drawing.Point(397, 275);
			this.replaceB.Name = "replaceB";
			this.replaceB.Size = new System.Drawing.Size(75, 23);
			this.replaceB.TabIndex = 9;
			this.replaceB.Text = "Replace";
			this.replaceB.UseVisualStyleBackColor = true;
			this.replaceB.Click += new System.EventHandler(this.ReplaceB_Click);
			// 
			// consoleTB
			// 
			this.consoleTB.Location = new System.Drawing.Point(397, 364);
			this.consoleTB.Multiline = true;
			this.consoleTB.Name = "consoleTB";
			this.consoleTB.ReadOnly = true;
			this.consoleTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.consoleTB.Size = new System.Drawing.Size(391, 217);
			this.consoleTB.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(397, 348);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Console:";
			// 
			// searchTB
			// 
			this.searchTB.Location = new System.Drawing.Point(210, 99);
			this.searchTB.Name = "searchTB";
			this.searchTB.Size = new System.Drawing.Size(181, 20);
			this.searchTB.TabIndex = 12;
			this.searchTB.TextChanged += new System.EventHandler(this.SearchTB_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(160, 102);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Search:";
			// 
			// fileInfoTB
			// 
			this.fileInfoTB.Location = new System.Drawing.Point(511, 141);
			this.fileInfoTB.Multiline = true;
			this.fileInfoTB.Name = "fileInfoTB";
			this.fileInfoTB.ReadOnly = true;
			this.fileInfoTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.fileInfoTB.Size = new System.Drawing.Size(277, 217);
			this.fileInfoTB.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(508, 125);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 13);
			this.label5.TabIndex = 15;
			this.label5.Text = "Selected file info:";
			// 
			// sortCB
			// 
			this.sortCB.AutoSize = true;
			this.sortCB.Checked = true;
			this.sortCB.CheckState = System.Windows.Forms.CheckState.Checked;
			this.sortCB.Location = new System.Drawing.Point(103, 101);
			this.sortCB.Name = "sortCB";
			this.sortCB.Size = new System.Drawing.Size(51, 17);
			this.sortCB.TabIndex = 16;
			this.sortCB.Text = "Sort?";
			this.sortCB.UseVisualStyleBackColor = true;
			this.sortCB.CheckedChanged += new System.EventHandler(this.SortCB_CheckedChanged);
			// 
			// selectAllB
			// 
			this.selectAllB.Enabled = false;
			this.selectAllB.Location = new System.Drawing.Point(397, 120);
			this.selectAllB.Name = "selectAllB";
			this.selectAllB.Size = new System.Drawing.Size(75, 23);
			this.selectAllB.TabIndex = 19;
			this.selectAllB.Text = "Select All";
			this.selectAllB.UseVisualStyleBackColor = true;
			this.selectAllB.Click += new System.EventHandler(this.SelectAllB_Click);
			// 
			// previewB
			// 
			this.previewB.Enabled = false;
			this.previewB.Location = new System.Drawing.Point(397, 189);
			this.previewB.Name = "previewB";
			this.previewB.Size = new System.Drawing.Size(75, 23);
			this.previewB.TabIndex = 20;
			this.previewB.Text = "Preview";
			this.previewB.UseVisualStyleBackColor = true;
			this.previewB.Visible = false;
			this.previewB.Click += new System.EventHandler(this.PreviewB_Click);
			// 
			// rstbChooseB
			// 
			this.rstbChooseB.Location = new System.Drawing.Point(551, 62);
			this.rstbChooseB.Name = "rstbChooseB";
			this.rstbChooseB.Size = new System.Drawing.Size(73, 23);
			this.rstbChooseB.TabIndex = 22;
			this.rstbChooseB.Text = "Choose File";
			this.rstbChooseB.UseVisualStyleBackColor = true;
			this.rstbChooseB.Click += new System.EventHandler(this.RstbChooseB_Click);
			// 
			// rstbPathTB
			// 
			this.rstbPathTB.Enabled = false;
			this.rstbPathTB.Location = new System.Drawing.Point(12, 64);
			this.rstbPathTB.Name = "rstbPathTB";
			this.rstbPathTB.Size = new System.Drawing.Size(533, 20);
			this.rstbPathTB.TabIndex = 21;
			// 
			// rstbUpdateB
			// 
			this.rstbUpdateB.Location = new System.Drawing.Point(630, 61);
			this.rstbUpdateB.Name = "rstbUpdateB";
			this.rstbUpdateB.Size = new System.Drawing.Size(97, 23);
			this.rstbUpdateB.TabIndex = 23;
			this.rstbUpdateB.Text = "Update RSTB";
			this.rstbUpdateB.UseVisualStyleBackColor = true;
			this.rstbUpdateB.Click += new System.EventHandler(this.RstbUpdateB_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 13);
			this.label6.TabIndex = 24;
			this.label6.Text = "RSTB file path:";
			// 
			// BarsViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 593);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.rstbUpdateB);
			this.Controls.Add(this.rstbChooseB);
			this.Controls.Add(this.rstbPathTB);
			this.Controls.Add(this.previewB);
			this.Controls.Add(this.selectAllB);
			this.Controls.Add(this.sortCB);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.fileInfoTB);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.searchTB);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.consoleTB);
			this.Controls.Add(this.replaceB);
			this.Controls.Add(this.extractB);
			this.Controls.Add(this.fileLB);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.fileOpenB);
			this.Controls.Add(this.fileChooseB);
			this.Controls.Add(this.filePathTB);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "BarsViewerForm";
			this.Text = "Bars Tool";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox filePathTB;
		private System.Windows.Forms.Button fileChooseB;
		private System.Windows.Forms.Button fileOpenB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox fileLB;
		private System.Windows.Forms.Button extractB;
		private System.Windows.Forms.Button replaceB;
		private System.Windows.Forms.TextBox consoleTB;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox searchTB;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox fileInfoTB;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox sortCB;
		private System.Windows.Forms.Button selectAllB;
		private System.Windows.Forms.Button previewB;
		private System.Windows.Forms.Button rstbChooseB;
		private System.Windows.Forms.TextBox rstbPathTB;
		private System.Windows.Forms.Button rstbUpdateB;
		private System.Windows.Forms.Label label6;
	}
}

