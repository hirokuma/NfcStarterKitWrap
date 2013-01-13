namespace HandoverWifi_FeliCaLite {
	partial class HandoverWifi {
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
			this.textBefore = new System.Windows.Forms.TextBox();
			this.buttonRead = new System.Windows.Forms.Button();
			this.textMcBefore = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.textSsid = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textKey = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboAuth = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.comboEnc = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textMac = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBefore
			// 
			this.textBefore.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBefore.Location = new System.Drawing.Point(12, 24);
			this.textBefore.Multiline = true;
			this.textBefore.Name = "textBefore";
			this.textBefore.ReadOnly = true;
			this.textBefore.Size = new System.Drawing.Size(307, 180);
			this.textBefore.TabIndex = 0;
			// 
			// buttonRead
			// 
			this.buttonRead.Location = new System.Drawing.Point(128, 260);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(75, 23);
			this.buttonRead.TabIndex = 1;
			this.buttonRead.Text = "Read";
			this.buttonRead.UseVisualStyleBackColor = true;
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// textMcBefore
			// 
			this.textMcBefore.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textMcBefore.Location = new System.Drawing.Point(12, 235);
			this.textMcBefore.Name = "textMcBefore";
			this.textMcBefore.ReadOnly = true;
			this.textMcBefore.Size = new System.Drawing.Size(307, 19);
			this.textMcBefore.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 220);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(22, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "MC";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "User Data";
			// 
			// buttonWrite
			// 
			this.buttonWrite.Enabled = false;
			this.buttonWrite.Location = new System.Drawing.Point(411, 253);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(75, 23);
			this.buttonWrite.TabIndex = 1;
			this.buttonWrite.Text = "Write";
			this.buttonWrite.UseVisualStyleBackColor = true;
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(349, 26);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(30, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "SSID";
			// 
			// textSsid
			// 
			this.textSsid.Location = new System.Drawing.Point(385, 23);
			this.textSsid.Name = "textSsid";
			this.textSsid.Size = new System.Drawing.Size(122, 19);
			this.textSsid.TabIndex = 7;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(354, 62);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 12);
			this.label7.TabIndex = 8;
			this.label7.Text = "Key";
			// 
			// textKey
			// 
			this.textKey.Location = new System.Drawing.Point(385, 59);
			this.textKey.Name = "textKey";
			this.textKey.Size = new System.Drawing.Size(122, 19);
			this.textKey.TabIndex = 9;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(349, 98);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(29, 12);
			this.label8.TabIndex = 10;
			this.label8.Text = "Auth";
			// 
			// comboAuth
			// 
			this.comboAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAuth.FormattingEnabled = true;
			this.comboAuth.Items.AddRange(new object[] {
            "OPEN",
            "WPA/PSK",
            "SHARED",
            "WPA",
            "WPA2",
            "WPA2/PSK"});
			this.comboAuth.Location = new System.Drawing.Point(385, 95);
			this.comboAuth.Name = "comboAuth";
			this.comboAuth.Size = new System.Drawing.Size(122, 20);
			this.comboAuth.TabIndex = 11;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(335, 135);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(44, 12);
			this.label9.TabIndex = 12;
			this.label9.Text = "Encrypt";
			// 
			// comboEnc
			// 
			this.comboEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboEnc.FormattingEnabled = true;
			this.comboEnc.Items.AddRange(new object[] {
            "NONE",
            "WEP",
            "TKIP",
            "AES"});
			this.comboEnc.Location = new System.Drawing.Point(385, 132);
			this.comboEnc.Name = "comboEnc";
			this.comboEnc.Size = new System.Drawing.Size(122, 20);
			this.comboEnc.TabIndex = 13;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(348, 172);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(30, 12);
			this.label10.TabIndex = 14;
			this.label10.Text = "MAC";
			// 
			// textMac
			// 
			this.textMac.Location = new System.Drawing.Point(386, 169);
			this.textMac.Name = "textMac";
			this.textMac.Size = new System.Drawing.Size(122, 19);
			this.textMac.TabIndex = 15;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(366, 204);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(120, 12);
			this.label11.TabIndex = 16;
			this.label11.Text = "※内容はチェックしません";
			// 
			// HandoverWifi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 296);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textMac);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.comboEnc);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.comboAuth);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textKey);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textSsid);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textMcBefore);
			this.Controls.Add(this.buttonWrite);
			this.Controls.Add(this.buttonRead);
			this.Controls.Add(this.textBefore);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "HandoverWifi";
			this.Text = "Wi-Fi Handover Card";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBefore;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.TextBox textMcBefore;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textSsid;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textKey;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboAuth;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboEnc;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textMac;
		private System.Windows.Forms.Label label11;
	}
}

