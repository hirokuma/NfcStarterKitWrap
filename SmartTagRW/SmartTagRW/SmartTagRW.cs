using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;

namespace SmartTagRW {
	public partial class SmartTagRW : Form {

		private Bitmap bitmapFile;
		private Bitmap bitmapBW;
		private NfcStarterKitWrap.nfc mFNS = new NfcStarterKitWrap.nfc();
		private SmartTag mSmartTag = null;

		private const int PIX_WHITE = 255;
		private const int PIX_GRAY = 250;
		private const int PIX_BLACK = 0;

		public SmartTagRW() {
			if(!mFNS.init()) {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
				return;
			}
			InitializeComponent();

			mSmartTag = new SmartTag(mFNS);
			updateStatus();

			trackThresh2.Value = trackThreshold.Value;
		}


		private void updateStatus() {
			SmartTag.TagStatus stat = mSmartTag.Status;

			switch(stat.Proc) {
			case SmartTag.TagStatus.ProcStat.BUSY:
				textBoxStatus.Text = "BUSY";
				break;
			case SmartTag.TagStatus.ProcStat.COMPLETED:
				textBoxStatus.Text = "Completed";
				break;
			case SmartTag.TagStatus.ProcStat.INIT:
				textBoxStatus.Text = "Initialized";
				break;
			default:
				textBoxStatus.Text = "unknown status";
				break;
			}

			switch(stat.Pow) {
			case SmartTag.TagStatus.PowerStat.NORMAL1:
				textBoxStatus.Text += "\r\n" + "Normal 1";
				break;
			case SmartTag.TagStatus.PowerStat.NORMAL2:
				textBoxStatus.Text += "\r\n" + "Normal 2";
				break;
			case SmartTag.TagStatus.PowerStat.LOW1:
				textBoxStatus.Text += "\r\n" + "Low 1";
				break;
			case SmartTag.TagStatus.PowerStat.LOW2:
				textBoxStatus.Text += "\r\n" + "Low 1";
				break;
			default:
				textBoxStatus.Text += "\r\n" + "unknown power";
				break;
			}

			textBoxStatus.Text += "\r\n" + stat.Ver.ToString();
		}

		private Bitmap imageBlackWhite(Bitmap bm, int threshold, int threshold2) {
			if(bm == null) {
				return null;
			}

			int th1, th2;	// th1 > th2
			if(threshold > threshold2) {
				th1 = threshold;
				th2 = threshold2;
			}
			else {
				th1 = threshold2;
				th2 = threshold;
			}

			// to Black & White
			Bitmap bm_bw = new Bitmap(picBW.Width, picBW.Height);
			for(int y = 0; y < picBW.Size.Height; y++) {
				for(int x = 0; x < picBW.Size.Width; x++) {
					byte bw = 255;
					if(x < bm.Width && y < bm.Height) {
						Color col = bm.GetPixel(x, y);
						bw = (byte)(col.R * 0.298912 + col.G * 0.586611 + col.B * 0.114478);
						if(bw < th2) {
							bw = PIX_BLACK;
						}
						else if(bw < th1) {
							bw = PIX_GRAY;
						}
						else {
							bw = PIX_WHITE;
						}
					}
					bm_bw.SetPixel(x, y, Color.FromArgb(bw, bw, bw));
				}
			}

			return bm_bw;
		}

		private Bitmap text2Image(string text) {
			Bitmap bm_bw = new Bitmap(picBW.Width, picBW.Height);
			Graphics g = Graphics.FromImage(bm_bw);
			g.Clear(Color.FromArgb(PIX_WHITE, PIX_WHITE, PIX_WHITE));

			StringReader strReader = new StringReader(text);

			BdfFont bf = BdfFont.getInstance();
			int chX = picBW.Width / bf.Width;
			int chY = picBW.Height / bf.Height;
			for(int yy = 0; yy < chY; yy++) {
				string lineText = Strings.StrConv(strReader.ReadLine(), VbStrConv.Wide);
				if(lineText == null) {
					break;
				}
				int len = lineText.Length;
				if(len > chX) {
					len = chX;
				}
				int count = 0;
				for(int xx = 0; xx < len; xx++) {
					byte[] font = bf.getFontData(lineText[count]);
					for(int y = 0; y < bf.Height; y++) {
						int mask = 0x8000;
						int xbyte = 2;	//めんどくさいので横幅2byte限定
						int xdata = font[xbyte * y] << 8 | font[xbyte * y + 1];
						for(int x = 0; x < bf.Width; x++) {
							int pix = xdata & mask;
							int bw;
							if(pix == 0) {
								bw = PIX_WHITE;
							}
							else {
								bw = PIX_BLACK;
							}
							bm_bw.SetPixel(xx*bf.Width + x, yy*bf.Height + y, Color.FromArgb(bw, bw, bw));
							mask >>= 1;
						}
					}
					count++;
				}
			}
			return bm_bw;
		}

		private void selectImgFile() {
			OpenFileDialog fd = new OpenFileDialog();
			fd.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
			if(fd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				bitmapFile = new Bitmap(fd.FileName);
				picFile.Image = bitmapFile;

				bitmapBW = imageBlackWhite(bitmapFile, trackThreshold.Value, trackThresh2.Value);
				picBW.Image = bitmapBW;
			}
		}

		private void picFile_Click(object sender, EventArgs e) {
			selectImgFile();
		}

		private void buttonSelectImg_Click(object sender, EventArgs e) {
			selectImgFile();
		}

		private void trackThreshold_ValueChanged(object sender, EventArgs e) {
			if(bitmapFile == null) {
				return;
			}
			if(trackThresh2.Enabled == false) {
				trackThresh2.Value = trackThreshold.Value;
			}
			bitmapBW = imageBlackWhite(bitmapFile, trackThreshold.Value, trackThresh2.Value);
			picBW.Image = bitmapBW;
		}

		private void trackThresh2_ValueChanged(object sender, EventArgs e) {
			if(bitmapFile == null) {
				return;
			}
			bitmapBW = imageBlackWhite(bitmapFile, trackThreshold.Value, trackThresh2.Value);
			picBW.Image = bitmapBW;
		}

		private void checkThresh2_CheckedChanged(object sender, EventArgs e) {
			trackThresh2.Visible = checkThresh2.Checked;
			trackThresh2.Enabled = checkThresh2.Checked;
			if(checkThresh2.Checked == false) {
				trackThresh2.Value = trackThreshold.Value;
			}
		}

		private void buttonSetText_Click(object sender, EventArgs e) {
			bitmapBW = text2Image(textMemo.Text);
			picBW.Image = bitmapBW;
		}

		private void textMemo_TextChanged(object sender, EventArgs e) {
			StringReader strReader = new StringReader(textMemo.Text);
			string str;
			while((str = strReader.ReadLine()) != null) {
				;
			}
		}


		////////////////////////////////////
		// ここから下は、SmartTagへ命令を出す
		////////////////////////////////////


		private void listLayoutNo_SelectedIndexChanged(object sender, EventArgs e) {
			this.Cursor = Cursors.WaitCursor;
			panelProc.Visible = true;
			bool b = mSmartTag.setLayout(listLayoutNo.SelectedIndex + 1);
			this.Cursor = Cursors.Default;
			panelProc.Visible = false;
			if (!b)
			{
				MessageBox.Show("SetLayout fail");
				return;
			}
		}

		private void buttonRegImage_Click(object sender, EventArgs e) {
			this.Cursor = Cursors.WaitCursor;
			panelProc.Visible = true;
			bool b = mSmartTag.regImage(listLayoutNo.SelectedIndex + 1);
			this.Cursor = Cursors.Default;
			panelProc.Visible = false;
			if (!b)
			{
				MessageBox.Show("RegImage fail");
				return;
			}
		}

		private void buttonSendImage_Click(object sender, EventArgs e) {
			this.Cursor = Cursors.WaitCursor;
			panelProc.Visible = true;
			Bitmap bmpBW = bitmapBW;

			bool b1 = true, b2;
			if(checkThresh2.Checked) {
				Bitmap bmpGray = new Bitmap(picBW.Width, picBW.Height);
				Graphics g = Graphics.FromImage(bmpGray);
				g.Clear(Color.FromArgb(PIX_WHITE, PIX_WHITE, PIX_WHITE));

				bmpBW = new Bitmap(picBW.Width, picBW.Height);
				g = Graphics.FromImage(bmpBW);
				g.Clear(Color.FromArgb(PIX_WHITE, PIX_WHITE, PIX_WHITE));

				for(int y = 0; y < picBW.Height; y++) {
					for(int x = 0; x < picBW.Width; x++) {
						byte col = bitmapBW.GetPixel(x, y).R;
						switch(col) {
						case PIX_BLACK:
							bmpBW.SetPixel(x, y, Color.FromArgb(PIX_BLACK, PIX_BLACK, PIX_BLACK));
							break;
						case PIX_GRAY:
							bmpGray.SetPixel(x, y, Color.FromArgb(PIX_BLACK, PIX_BLACK, PIX_BLACK));
							break;
						default:
							break;
						}
					}
				}

				b1 = mSmartTag.displayImage(bmpGray);
			}
			b2 = mSmartTag.displayImage(bmpBW);
			this.Cursor = Cursors.Default;
			panelProc.Visible = false;
			if(!b1 || !b2) {
				MessageBox.Show("SendImage fail");
				return;
			}
		}

		private void buttonGetStatus_Click(object sender, EventArgs e) {
			this.Cursor = Cursors.WaitCursor;
			panelProc.Visible = true;
			bool b = mSmartTag.getStatus();
			this.Cursor = Cursors.Default;
			panelProc.Visible = false;
			updateStatus();
			if(!b)
			{
				MessageBox.Show("GetStatus fail");
				return;
			}
		}
	}
}
