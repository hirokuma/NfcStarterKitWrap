using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FelicaLiteReadWrite {
	public partial class UltralightReadWrite : Form {

		private NfcStarterKitWrap.nfc mFNS = new NfcStarterKitWrap.nfc();
		private NfcStarterKitWrap.MifareUltralight mLight = null;
		private byte[] mWriteValue = new byte[NfcStarterKitWrap.MifareUltralight.WRITABLE_SIZE];

		public UltralightReadWrite() {
			if(!mFNS.init()) {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
				return;
			}
			InitializeComponent();
			comboBoxReadBlock.SelectedIndex = 0;
			comboBoxWriteBlock.SelectedIndex = 0;

			mLight = new NfcStarterKitWrap.MifareUltralight(mFNS);
		}

		private void FelicaLiteReadWrite_FormClosed(object sender, FormClosedEventArgs e) {
			mFNS.term();
		}

		private void textBoxWriteValue_TextChanged(object sender, EventArgs e) {
			buttonWrite.Enabled = false;

			if(textBoxWriteValue.Text.Length != NfcStarterKitWrap.MifareUltralight.WRITABLE_SIZE * 2) {
				return;
			}
			for(int len = 0; len < NfcStarterKitWrap.MifareUltralight.WRITABLE_SIZE; len++) {
				try {
					mWriteValue[len] = (byte)Convert.ToInt32(textBoxWriteValue.Text.Substring(len * 2, 2), 16);
				}
				catch(Exception) {
					return;
				}
			}

			buttonWrite.Enabled = true;
		}

		private void buttonRead_Click(object sender, EventArgs e) {
			textBoxReadValue.Text = "";
			textBoxUID.Text = "";
			textBoxSAK.Text = "";

			bool ret;

			ret = mFNS.pollingA();
			if(!ret) {
				MessageBox.Show("Polling fail");
				return;
			}
			textBoxUID.Text = BitConverter.ToString(mFNS.NfcId);
			textBoxSAK.Text = mFNS.RD[NfcStarterKitWrap.nfc.RD_SELRES].ToString("x2");

			byte block = (byte)comboBoxReadBlock.SelectedIndex;

			byte[] rbuf = null;
			ret = mLight.Read(ref rbuf, block);
			if(!ret) {
				MessageBox.Show("Read fail");
				return;
			}
			textBoxReadValue.Text = BitConverter.ToString(rbuf);
		}

		private void writeWidgetEnabled(bool b) {
			buttonWrite.Enabled = b;
			comboBoxWriteBlock.Enabled = b;
			textBoxWriteValue.Enabled = b;
		}

		private void buttonWrite_Click(object sender, EventArgs e) {
			writeWidgetEnabled(false);
			textBoxUID.Text = "";

			bool ret;

			ret = mFNS.pollingA();
			if(!ret) {
				MessageBox.Show("Polling fail");
				writeWidgetEnabled(true);
				return;
			}
			textBoxUID.Text = BitConverter.ToString(mFNS.NfcId);

			// 書き込めるのはpage4以降にしておく
			byte block = (byte)(comboBoxWriteBlock.SelectedIndex + 4);

			ret = mLight.Write(mWriteValue, block);
			if(!ret) {
				MessageBox.Show("Write fail");
				writeWidgetEnabled(true);
				return;
			}

			writeWidgetEnabled(true);
		}
	}
}
