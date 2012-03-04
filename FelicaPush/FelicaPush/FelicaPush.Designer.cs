namespace FelicaLiteReadWrite {
	partial class UltralightReadWrite {
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
			this.buttonPush = new System.Windows.Forms.Button();
			this.textBoxUrl = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonPush
			// 
			this.buttonPush.Location = new System.Drawing.Point(269, 37);
			this.buttonPush.Name = "buttonPush";
			this.buttonPush.Size = new System.Drawing.Size(75, 23);
			this.buttonPush.TabIndex = 4;
			this.buttonPush.Text = "Push";
			this.buttonPush.UseVisualStyleBackColor = true;
			this.buttonPush.Click += new System.EventHandler(this.buttonPush_Click);
			// 
			// textBoxUrl
			// 
			this.textBoxUrl.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxUrl.Location = new System.Drawing.Point(13, 12);
			this.textBoxUrl.Name = "textBoxUrl";
			this.textBoxUrl.Size = new System.Drawing.Size(331, 19);
			this.textBoxUrl.TabIndex = 3;
			this.textBoxUrl.Text = "http://www.yahoo.co.jp/";
			this.textBoxUrl.TextChanged += new System.EventHandler(this.textBoxUrl_TextChanged);
			// 
			// UltralightReadWrite
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(357, 74);
			this.Controls.Add(this.textBoxUrl);
			this.Controls.Add(this.buttonPush);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "UltralightReadWrite";
			this.Text = "FeliCa Push";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FelicaLiteReadWrite_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxUrl;
		private System.Windows.Forms.Button buttonPush;
	}
}

