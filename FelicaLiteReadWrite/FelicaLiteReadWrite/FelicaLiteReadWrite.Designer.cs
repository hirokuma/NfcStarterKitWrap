namespace FelicaLiteReadWrite {
	partial class FelicaLiteReadWrite {
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
			System.Windows.Forms.GroupBox groupBox1;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.GroupBox groupBox2;
			System.Windows.Forms.Label label2;
			this.textBoxReadValue = new System.Windows.Forms.TextBox();
			this.buttonRead = new System.Windows.Forms.Button();
			this.comboBoxReadBlock = new System.Windows.Forms.ComboBox();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.textBoxWriteValue = new System.Windows.Forms.TextBox();
			this.comboBoxWriteBlock = new System.Windows.Forms.ComboBox();
			this.textBoxSc = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxIDm = new System.Windows.Forms.TextBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			label1 = new System.Windows.Forms.Label();
			groupBox2 = new System.Windows.Forms.GroupBox();
			label2 = new System.Windows.Forms.Label();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(this.textBoxReadValue);
			groupBox1.Controls.Add(this.buttonRead);
			groupBox1.Controls.Add(this.comboBoxReadBlock);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new System.Drawing.Point(13, 13);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(345, 107);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Read";
			// 
			// textBoxReadValue
			// 
			this.textBoxReadValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxReadValue.Location = new System.Drawing.Point(8, 76);
			this.textBoxReadValue.Name = "textBoxReadValue";
			this.textBoxReadValue.ReadOnly = true;
			this.textBoxReadValue.Size = new System.Drawing.Size(331, 19);
			this.textBoxReadValue.TabIndex = 3;
			// 
			// buttonRead
			// 
			this.buttonRead.Location = new System.Drawing.Point(187, 27);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(75, 23);
			this.buttonRead.TabIndex = 2;
			this.buttonRead.Text = "Read";
			this.buttonRead.UseVisualStyleBackColor = true;
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// comboBoxReadBlock
			// 
			this.comboBoxReadBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxReadBlock.FormattingEnabled = true;
			this.comboBoxReadBlock.Items.AddRange(new object[] {
            "PAD0",
            "PAD1",
            "PAD2",
            "PAD3",
            "PAD4",
            "PAD5",
            "PAD6",
            "PAD7",
            "PAD8",
            "PAD9",
            "PAD10",
            "PAD11",
            "PAD12",
            "PAD13",
            "REG",
            "RC",
            "MAC",
            "ID",
            "D_ID",
            "SER_C",
            "SYS_C",
            "CKV",
            "CK",
            "MC",
            "WCNT",
            "MAC_A",
            "STATE",
            "CRC_CHECK"});
			this.comboBoxReadBlock.Location = new System.Drawing.Point(50, 29);
			this.comboBoxReadBlock.Name = "comboBoxReadBlock";
			this.comboBoxReadBlock.Size = new System.Drawing.Size(121, 20);
			this.comboBoxReadBlock.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(6, 32);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(38, 12);
			label1.TabIndex = 0;
			label1.Text = "ブロック";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(this.buttonWrite);
			groupBox2.Controls.Add(this.textBoxWriteValue);
			groupBox2.Controls.Add(label2);
			groupBox2.Controls.Add(this.comboBoxWriteBlock);
			groupBox2.Location = new System.Drawing.Point(13, 149);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(345, 108);
			groupBox2.TabIndex = 1;
			groupBox2.TabStop = false;
			groupBox2.Text = "Write";
			// 
			// buttonWrite
			// 
			this.buttonWrite.Enabled = false;
			this.buttonWrite.Location = new System.Drawing.Point(187, 29);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(75, 23);
			this.buttonWrite.TabIndex = 4;
			this.buttonWrite.Text = "Write";
			this.buttonWrite.UseVisualStyleBackColor = true;
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// textBoxWriteValue
			// 
			this.textBoxWriteValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxWriteValue.Location = new System.Drawing.Point(8, 75);
			this.textBoxWriteValue.Name = "textBoxWriteValue";
			this.textBoxWriteValue.Size = new System.Drawing.Size(331, 19);
			this.textBoxWriteValue.TabIndex = 3;
			this.textBoxWriteValue.TextChanged += new System.EventHandler(this.textBoxWriteValue_TextChanged);
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(6, 34);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(38, 12);
			label2.TabIndex = 0;
			label2.Text = "ブロック";
			// 
			// comboBoxWriteBlock
			// 
			this.comboBoxWriteBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWriteBlock.FormattingEnabled = true;
			this.comboBoxWriteBlock.Items.AddRange(new object[] {
            "PAD0",
            "PAD1",
            "PAD2",
            "PAD3",
            "PAD4",
            "PAD5",
            "PAD6",
            "PAD7",
            "PAD8",
            "PAD9",
            "PAD10",
            "PAD11",
            "PAD12",
            "PAD13",
            "REG",
            "RC",
            "MAC",
            "ID",
            "D_ID",
            "SER_C",
            "SYS_C",
            "CKV",
            "CK",
            "MC",
            "WCNT",
            "MAC_A",
            "STATE",
            "CRC_CHECK"});
			this.comboBoxWriteBlock.Location = new System.Drawing.Point(50, 31);
			this.comboBoxWriteBlock.Name = "comboBoxWriteBlock";
			this.comboBoxWriteBlock.Size = new System.Drawing.Size(121, 20);
			this.comboBoxWriteBlock.TabIndex = 1;
			// 
			// textBoxSc
			// 
			this.textBoxSc.Location = new System.Drawing.Point(39, 275);
			this.textBoxSc.Name = "textBoxSc";
			this.textBoxSc.ReadOnly = true;
			this.textBoxSc.Size = new System.Drawing.Size(100, 19);
			this.textBoxSc.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 278);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 12);
			this.label3.TabIndex = 3;
			this.label3.Text = "SC";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(169, 278);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 12);
			this.label4.TabIndex = 4;
			this.label4.Text = "IDm";
			// 
			// textBoxIDm
			// 
			this.textBoxIDm.Location = new System.Drawing.Point(200, 275);
			this.textBoxIDm.Name = "textBoxIDm";
			this.textBoxIDm.ReadOnly = true;
			this.textBoxIDm.Size = new System.Drawing.Size(152, 19);
			this.textBoxIDm.TabIndex = 5;
			// 
			// FelicaLiteReadWrite
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 309);
			this.Controls.Add(this.textBoxIDm);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxSc);
			this.Controls.Add(groupBox2);
			this.Controls.Add(groupBox1);
			this.Name = "FelicaLiteReadWrite";
			this.Text = "FeliCa Lite Read/Write";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FelicaLiteReadWrite_FormClosed);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxReadValue;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.ComboBox comboBoxReadBlock;
		private System.Windows.Forms.TextBox textBoxWriteValue;
		private System.Windows.Forms.ComboBox comboBoxWriteBlock;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.TextBox textBoxSc;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxIDm;
	}
}

