namespace FelicaLiteIssuance {
	partial class FelicaLiteIssuance {
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
			System.Windows.Forms.PictureBox pictureBox1;
			this.buttonIssuance1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxMasterKey = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxKeyVersion = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxDFD = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxTest = new System.Windows.Forms.TextBox();
			this.buttonTest = new System.Windows.Forms.Button();
			this.textBoxResult = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxSafety = new System.Windows.Forms.TextBox();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pictureBox1.Location = new System.Drawing.Point(16, 168);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(307, 5);
			pictureBox1.TabIndex = 7;
			pictureBox1.TabStop = false;
			// 
			// buttonIssuance1
			// 
			this.buttonIssuance1.Enabled = false;
			this.buttonIssuance1.Location = new System.Drawing.Point(202, 139);
			this.buttonIssuance1.Name = "buttonIssuance1";
			this.buttonIssuance1.Size = new System.Drawing.Size(122, 23);
			this.buttonIssuance1.TabIndex = 0;
			this.buttonIssuance1.Text = "一次発行のまね";
			this.buttonIssuance1.UseVisualStyleBackColor = true;
			this.buttonIssuance1.Click += new System.EventHandler(this.buttonIssuance1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "個別化マスター鍵";
			// 
			// textBoxMasterKey
			// 
			this.textBoxMasterKey.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxMasterKey.Location = new System.Drawing.Point(12, 24);
			this.textBoxMasterKey.Name = "textBoxMasterKey";
			this.textBoxMasterKey.Size = new System.Drawing.Size(311, 19);
			this.textBoxMasterKey.TabIndex = 2;
			this.textBoxMasterKey.TextChanged += new System.EventHandler(this.textBoxMasterKey_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "鍵バージョン";
			// 
			// textBoxKeyVersion
			// 
			this.textBoxKeyVersion.Location = new System.Drawing.Point(10, 99);
			this.textBoxKeyVersion.Name = "textBoxKeyVersion";
			this.textBoxKeyVersion.Size = new System.Drawing.Size(100, 19);
			this.textBoxKeyVersion.TabIndex = 4;
			this.textBoxKeyVersion.TextChanged += new System.EventHandler(this.textBoxKeyVersion_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(178, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(28, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "DFD";
			// 
			// textBoxDFD
			// 
			this.textBoxDFD.Location = new System.Drawing.Point(180, 99);
			this.textBoxDFD.Name = "textBoxDFD";
			this.textBoxDFD.Size = new System.Drawing.Size(100, 19);
			this.textBoxDFD.TabIndex = 6;
			this.textBoxDFD.TextChanged += new System.EventHandler(this.textBoxDFD_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 226);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 12);
			this.label4.TabIndex = 8;
			this.label4.Text = "テスト";
			// 
			// textBoxTest
			// 
			this.textBoxTest.Location = new System.Drawing.Point(12, 241);
			this.textBoxTest.Name = "textBoxTest";
			this.textBoxTest.Size = new System.Drawing.Size(311, 19);
			this.textBoxTest.TabIndex = 9;
			// 
			// buttonTest
			// 
			this.buttonTest.Location = new System.Drawing.Point(201, 289);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(122, 23);
			this.buttonTest.TabIndex = 10;
			this.buttonTest.Text = "テスト";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// textBoxResult
			// 
			this.textBoxResult.Location = new System.Drawing.Point(95, 289);
			this.textBoxResult.Name = "textBoxResult";
			this.textBoxResult.ReadOnly = true;
			this.textBoxResult.Size = new System.Drawing.Size(100, 19);
			this.textBoxResult.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(241, 49);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 12;
			this.label5.Text = "安全性";
			// 
			// textBoxSafety
			// 
			this.textBoxSafety.Location = new System.Drawing.Point(288, 46);
			this.textBoxSafety.Name = "textBoxSafety";
			this.textBoxSafety.ReadOnly = true;
			this.textBoxSafety.Size = new System.Drawing.Size(35, 19);
			this.textBoxSafety.TabIndex = 13;
			// 
			// FelicaLiteIssuance
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(336, 327);
			this.Controls.Add(this.textBoxSafety);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxResult);
			this.Controls.Add(this.buttonTest);
			this.Controls.Add(this.textBoxTest);
			this.Controls.Add(this.label4);
			this.Controls.Add(pictureBox1);
			this.Controls.Add(this.textBoxDFD);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxKeyVersion);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxMasterKey);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonIssuance1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "FelicaLiteIssuance";
			this.Text = "FeliCa Lite発行";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FelicaLiteIssuance_FormClosed);
			((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonIssuance1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxMasterKey;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxKeyVersion;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxDFD;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxTest;
		private System.Windows.Forms.Button buttonTest;
		private System.Windows.Forms.TextBox textBoxResult;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxSafety;
	}
}

