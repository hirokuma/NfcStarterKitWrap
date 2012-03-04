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
			System.Windows.Forms.GroupBox groupBox1;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.GroupBox groupBox2;
			System.Windows.Forms.Label label2;
			this.comboBoxReadBlock = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxReadValue = new System.Windows.Forms.TextBox();
			this.buttonRead = new System.Windows.Forms.Button();
			this.comboBoxReadSector = new System.Windows.Forms.ComboBox();
			this.comboBoxWriteBlock = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.textBoxWriteValue = new System.Windows.Forms.TextBox();
			this.comboBoxWriteSector = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxUID = new System.Windows.Forms.TextBox();
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
			groupBox1.Controls.Add(this.comboBoxReadBlock);
			groupBox1.Controls.Add(this.label4);
			groupBox1.Controls.Add(this.textBoxReadValue);
			groupBox1.Controls.Add(this.buttonRead);
			groupBox1.Controls.Add(this.comboBoxReadSector);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new System.Drawing.Point(13, 13);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(345, 107);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Read";
			// 
			// comboBoxReadBlock
			// 
			this.comboBoxReadBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxReadBlock.FormattingEnabled = true;
			this.comboBoxReadBlock.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
			this.comboBoxReadBlock.Location = new System.Drawing.Point(50, 47);
			this.comboBoxReadBlock.Name = "comboBoxReadBlock";
			this.comboBoxReadBlock.Size = new System.Drawing.Size(93, 20);
			this.comboBoxReadBlock.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 12);
			this.label4.TabIndex = 4;
			this.label4.Text = "Block";
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
			this.buttonRead.Location = new System.Drawing.Point(253, 27);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(75, 23);
			this.buttonRead.TabIndex = 2;
			this.buttonRead.Text = "Read";
			this.buttonRead.UseVisualStyleBackColor = true;
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// comboBoxReadSector
			// 
			this.comboBoxReadSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxReadSector.FormattingEnabled = true;
			this.comboBoxReadSector.Items.AddRange(new object[] {
            "0x00",
            "0x01",
            "0x02",
            "0x03",
            "0x04",
            "0x05",
            "0x06",
            "0x07",
            "0x08",
            "0x09",
            "0x0A",
            "0x0B",
            "0x0C",
            "0x0D",
            "0x0E",
            "0x0F"});
			this.comboBoxReadSector.Location = new System.Drawing.Point(50, 24);
			this.comboBoxReadSector.Name = "comboBoxReadSector";
			this.comboBoxReadSector.Size = new System.Drawing.Size(93, 20);
			this.comboBoxReadSector.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(6, 27);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(38, 12);
			label1.TabIndex = 0;
			label1.Text = "Sector";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(this.comboBoxWriteBlock);
			groupBox2.Controls.Add(this.label5);
			groupBox2.Controls.Add(this.buttonWrite);
			groupBox2.Controls.Add(this.textBoxWriteValue);
			groupBox2.Controls.Add(label2);
			groupBox2.Controls.Add(this.comboBoxWriteSector);
			groupBox2.Location = new System.Drawing.Point(13, 149);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(345, 108);
			groupBox2.TabIndex = 1;
			groupBox2.TabStop = false;
			groupBox2.Text = "Write";
			// 
			// comboBoxWriteBlock
			// 
			this.comboBoxWriteBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWriteBlock.FormattingEnabled = true;
			this.comboBoxWriteBlock.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
			this.comboBoxWriteBlock.Location = new System.Drawing.Point(50, 50);
			this.comboBoxWriteBlock.Name = "comboBoxWriteBlock";
			this.comboBoxWriteBlock.Size = new System.Drawing.Size(93, 20);
			this.comboBoxWriteBlock.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(10, 53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 12);
			this.label5.TabIndex = 5;
			this.label5.Text = "Block";
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
			label2.Location = new System.Drawing.Point(6, 29);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(38, 12);
			label2.TabIndex = 0;
			label2.Text = "Sector";
			// 
			// comboBoxWriteSector
			// 
			this.comboBoxWriteSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWriteSector.FormattingEnabled = true;
			this.comboBoxWriteSector.Items.AddRange(new object[] {
            "0x00",
            "0x01",
            "0x02",
            "0x03",
            "0x04",
            "0x05",
            "0x06",
            "0x07",
            "0x08",
            "0x09",
            "0x0A",
            "0x0B",
            "0x0C",
            "0x0D",
            "0x0E",
            "0x0F"});
			this.comboBoxWriteSector.Location = new System.Drawing.Point(50, 26);
			this.comboBoxWriteSector.Name = "comboBoxWriteSector";
			this.comboBoxWriteSector.Size = new System.Drawing.Size(93, 20);
			this.comboBoxWriteSector.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 276);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "UID";
			// 
			// textBoxUID
			// 
			this.textBoxUID.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxUID.Location = new System.Drawing.Point(49, 273);
			this.textBoxUID.Name = "textBoxUID";
			this.textBoxUID.ReadOnly = true;
			this.textBoxUID.Size = new System.Drawing.Size(179, 19);
			this.textBoxUID.TabIndex = 3;
			// 
			// UltralightReadWrite
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 309);
			this.Controls.Add(this.textBoxUID);
			this.Controls.Add(this.label3);
			this.Controls.Add(groupBox2);
			this.Controls.Add(groupBox1);
			this.Name = "UltralightReadWrite";
			this.Text = "Mifare Classic Read/Write";
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
		private System.Windows.Forms.ComboBox comboBoxReadSector;
		private System.Windows.Forms.TextBox textBoxWriteValue;
		private System.Windows.Forms.ComboBox comboBoxWriteSector;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxUID;
		private System.Windows.Forms.ComboBox comboBoxReadBlock;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxWriteBlock;
		private System.Windows.Forms.Label label5;
	}
}

