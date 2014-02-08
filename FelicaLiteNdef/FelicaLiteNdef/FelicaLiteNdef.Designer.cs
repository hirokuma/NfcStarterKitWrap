namespace HandoverWifi_FeliCaLite {
	partial class FelicaLiteNdef {
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
			this.textAfter = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textAfter
			// 
			this.textAfter.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textAfter.Location = new System.Drawing.Point(12, 24);
			this.textAfter.Multiline = true;
			this.textAfter.Name = "textAfter";
			this.textAfter.ReadOnly = true;
			this.textAfter.Size = new System.Drawing.Size(307, 225);
			this.textAfter.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "User Data";
			// 
			// buttonWrite
			// 
			this.buttonWrite.Location = new System.Drawing.Point(12, 255);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(75, 23);
			this.buttonWrite.TabIndex = 1;
			this.buttonWrite.Text = "Write";
			this.buttonWrite.UseVisualStyleBackColor = true;
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// FelicaLiteNdef
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 290);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonWrite);
			this.Controls.Add(this.textAfter);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FelicaLiteNdef";
			this.Text = "Wi-Fi Handover Card";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textAfter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonWrite;
	}
}

