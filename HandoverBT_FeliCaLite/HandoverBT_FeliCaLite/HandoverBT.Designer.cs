namespace HandoverBT_FeliCaLite {
	partial class HandoverBt {
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
            this.textMac.Text = "112233445566";
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
            // HandoverBt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 296);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textMac);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textMcBefore);
            this.Controls.Add(this.buttonWrite);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.textBefore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "HandoverBt";
            this.Text = "BT Handover Card";
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
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textMac;
		private System.Windows.Forms.Label label11;
	}
}

