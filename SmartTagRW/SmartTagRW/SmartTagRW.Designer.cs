namespace SmartTagRW {
	partial class SmartTagRW {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.picFile = new System.Windows.Forms.PictureBox();
			this.picBW = new System.Windows.Forms.PictureBox();
			this.trackThreshold = new System.Windows.Forms.TrackBar();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.listLayoutNo = new System.Windows.Forms.ListBox();
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.buttonSendImage = new System.Windows.Forms.Button();
			this.buttonRegImage = new System.Windows.Forms.Button();
			this.buttonSelectImg = new System.Windows.Forms.Button();
			this.buttonGetStatus = new System.Windows.Forms.Button();
			this.textMemo = new System.Windows.Forms.TextBox();
			this.buttonSetText = new System.Windows.Forms.Button();
			this.trackThresh2 = new System.Windows.Forms.TrackBar();
			this.checkThresh2 = new System.Windows.Forms.CheckBox();
			this.panelProc = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.picFile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picBW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackThreshold)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackThresh2)).BeginInit();
			this.SuspendLayout();
			// 
			// picFile
			// 
			this.picFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picFile.Location = new System.Drawing.Point(69, 96);
			this.picFile.Name = "picFile";
			this.picFile.Size = new System.Drawing.Size(200, 96);
			this.picFile.TabIndex = 0;
			this.picFile.TabStop = false;
			this.picFile.Click += new System.EventHandler(this.picFile_Click);
			// 
			// picBW
			// 
			this.picBW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picBW.Location = new System.Drawing.Point(291, 96);
			this.picBW.Name = "picBW";
			this.picBW.Size = new System.Drawing.Size(200, 96);
			this.picBW.TabIndex = 0;
			this.picBW.TabStop = false;
			// 
			// trackThreshold
			// 
			this.trackThreshold.Location = new System.Drawing.Point(291, 47);
			this.trackThreshold.Maximum = 255;
			this.trackThreshold.Name = "trackThreshold";
			this.trackThreshold.Size = new System.Drawing.Size(200, 42);
			this.trackThreshold.TabIndex = 1;
			this.trackThreshold.TickFrequency = 16;
			this.trackThreshold.Value = 128;
			this.trackThreshold.ValueChanged += new System.EventHandler(this.trackThreshold_ValueChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Location = new System.Drawing.Point(43, 249);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(480, 5);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// listLayoutNo
			// 
			this.listLayoutNo.FormattingEnabled = true;
			this.listLayoutNo.ItemHeight = 12;
			this.listLayoutNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
			this.listLayoutNo.Location = new System.Drawing.Point(12, 47);
			this.listLayoutNo.Name = "listLayoutNo";
			this.listLayoutNo.Size = new System.Drawing.Size(21, 160);
			this.listLayoutNo.TabIndex = 3;
			this.listLayoutNo.SelectedIndexChanged += new System.EventHandler(this.listLayoutNo_SelectedIndexChanged);
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.Location = new System.Drawing.Point(12, 282);
			this.textBoxStatus.Multiline = true;
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.ReadOnly = true;
			this.textBoxStatus.Size = new System.Drawing.Size(100, 77);
			this.textBoxStatus.TabIndex = 6;
			// 
			// buttonSendImage
			// 
			this.buttonSendImage.Location = new System.Drawing.Point(291, 198);
			this.buttonSendImage.Name = "buttonSendImage";
			this.buttonSendImage.Size = new System.Drawing.Size(75, 23);
			this.buttonSendImage.TabIndex = 7;
			this.buttonSendImage.Text = "send";
			this.buttonSendImage.UseVisualStyleBackColor = true;
			this.buttonSendImage.Click += new System.EventHandler(this.buttonSendImage_Click);
			// 
			// buttonRegImage
			// 
			this.buttonRegImage.Location = new System.Drawing.Point(416, 198);
			this.buttonRegImage.Name = "buttonRegImage";
			this.buttonRegImage.Size = new System.Drawing.Size(75, 23);
			this.buttonRegImage.TabIndex = 8;
			this.buttonRegImage.Text = "register";
			this.buttonRegImage.UseVisualStyleBackColor = true;
			this.buttonRegImage.Click += new System.EventHandler(this.buttonRegImage_Click);
			// 
			// buttonSelectImg
			// 
			this.buttonSelectImg.Location = new System.Drawing.Point(69, 198);
			this.buttonSelectImg.Name = "buttonSelectImg";
			this.buttonSelectImg.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectImg.TabIndex = 9;
			this.buttonSelectImg.Text = "select";
			this.buttonSelectImg.UseVisualStyleBackColor = true;
			this.buttonSelectImg.Click += new System.EventHandler(this.buttonSelectImg_Click);
			// 
			// buttonGetStatus
			// 
			this.buttonGetStatus.Location = new System.Drawing.Point(12, 12);
			this.buttonGetStatus.Name = "buttonGetStatus";
			this.buttonGetStatus.Size = new System.Drawing.Size(75, 23);
			this.buttonGetStatus.TabIndex = 10;
			this.buttonGetStatus.Text = "Status";
			this.buttonGetStatus.UseVisualStyleBackColor = true;
			this.buttonGetStatus.Click += new System.EventHandler(this.buttonGetStatus_Click);
			// 
			// textMemo
			// 
			this.textMemo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textMemo.Location = new System.Drawing.Point(287, 273);
			this.textMemo.Multiline = true;
			this.textMemo.Name = "textMemo";
			this.textMemo.Size = new System.Drawing.Size(210, 116);
			this.textMemo.TabIndex = 11;
			this.textMemo.TextChanged += new System.EventHandler(this.textMemo_TextChanged);
			// 
			// buttonSetText
			// 
			this.buttonSetText.Location = new System.Drawing.Point(287, 395);
			this.buttonSetText.Name = "buttonSetText";
			this.buttonSetText.Size = new System.Drawing.Size(75, 23);
			this.buttonSetText.TabIndex = 12;
			this.buttonSetText.Text = "Text";
			this.buttonSetText.UseVisualStyleBackColor = true;
			this.buttonSetText.Click += new System.EventHandler(this.buttonSetText_Click);
			// 
			// trackThresh2
			// 
			this.trackThresh2.Enabled = false;
			this.trackThresh2.Location = new System.Drawing.Point(291, 0);
			this.trackThresh2.Maximum = 255;
			this.trackThresh2.Name = "trackThresh2";
			this.trackThresh2.Size = new System.Drawing.Size(200, 42);
			this.trackThresh2.TabIndex = 13;
			this.trackThresh2.TickFrequency = 16;
			this.trackThresh2.Value = 80;
			this.trackThresh2.Visible = false;
			this.trackThresh2.ValueChanged += new System.EventHandler(this.trackThresh2_ValueChanged);
			// 
			// checkThresh2
			// 
			this.checkThresh2.AutoSize = true;
			this.checkThresh2.Location = new System.Drawing.Point(270, 5);
			this.checkThresh2.Name = "checkThresh2";
			this.checkThresh2.Size = new System.Drawing.Size(15, 14);
			this.checkThresh2.TabIndex = 14;
			this.checkThresh2.UseVisualStyleBackColor = true;
			this.checkThresh2.CheckedChanged += new System.EventHandler(this.checkThresh2_CheckedChanged);
			// 
			// panelProc
			// 
			this.panelProc.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.panelProc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelProc.Location = new System.Drawing.Point(12, 12);
			this.panelProc.Name = "panelProc";
			this.panelProc.Size = new System.Drawing.Size(511, 401);
			this.panelProc.TabIndex = 15;
			this.panelProc.Visible = false;
			// 
			// SmartTagRW
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(535, 425);
			this.Controls.Add(this.panelProc);
			this.Controls.Add(this.checkThresh2);
			this.Controls.Add(this.trackThresh2);
			this.Controls.Add(this.buttonSetText);
			this.Controls.Add(this.textMemo);
			this.Controls.Add(this.buttonGetStatus);
			this.Controls.Add(this.buttonSelectImg);
			this.Controls.Add(this.buttonRegImage);
			this.Controls.Add(this.buttonSendImage);
			this.Controls.Add(this.textBoxStatus);
			this.Controls.Add(this.listLayoutNo);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.trackThreshold);
			this.Controls.Add(this.picBW);
			this.Controls.Add(this.picFile);
			this.Name = "SmartTagRW";
			this.Text = "SmartTagRW";
			((System.ComponentModel.ISupportInitialize)(this.picFile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picBW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackThreshold)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackThresh2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picFile;
		private System.Windows.Forms.PictureBox picBW;
		private System.Windows.Forms.TrackBar trackThreshold;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ListBox listLayoutNo;
		private System.Windows.Forms.TextBox textBoxStatus;
		private System.Windows.Forms.Button buttonSendImage;
		private System.Windows.Forms.Button buttonRegImage;
		private System.Windows.Forms.Button buttonSelectImg;
		private System.Windows.Forms.Button buttonGetStatus;
		private System.Windows.Forms.TextBox textMemo;
		private System.Windows.Forms.Button buttonSetText;
		private System.Windows.Forms.TrackBar trackThresh2;
		private System.Windows.Forms.CheckBox checkThresh2;
		private System.Windows.Forms.Panel panelProc;
	}
}

