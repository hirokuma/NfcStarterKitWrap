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
		private NfcStarterKitWrap.MifareClassic mClassic = null;
		private byte[] mWriteValue = new byte[NfcStarterKitWrap.MifareClassic.WRITABLE_SIZE];

		public UltralightReadWrite() {
			if(!mFNS.init()) {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
				return;
			}
			InitializeComponent();
			comboBoxReadSector.SelectedIndex = 0;
			comboBoxWriteSector.SelectedIndex = 0;
			comboBoxReadBlock.SelectedIndex = 0;
			comboBoxWriteBlock.SelectedIndex = 0;

			mClassic = new NfcStarterKitWrap.MifareClassic(mFNS);
		}

		private void FelicaLiteReadWrite_FormClosed(object sender, FormClosedEventArgs e) {
			mFNS.term();
		}

		private void textBoxWriteValue_TextChanged(object sender, EventArgs e) {
			buttonWrite.Enabled = false;

			if(textBoxWriteValue.Text.Length != NfcStarterKitWrap.MifareClassic.WRITABLE_SIZE * 2) {
				return;
			}
			for(int len = 0; len < NfcStarterKitWrap.MifareClassic.WRITABLE_SIZE; len++) {
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

			bool ret;

			ret = mFNS.pollingA();
			if(!ret) {
				MessageBox.Show("Polling fail");
				return;
			}
			textBoxUID.Text = BitConverter.ToString(mFNS.NfcId);

			byte sector = (byte)comboBoxReadSector.SelectedIndex;
			byte block = (byte)comboBoxReadBlock.SelectedIndex;

			ret = mClassic.Auth(NfcStarterKitWrap.MifareClassic.CMD_AUTHA, sector, block);
			if(!ret) {
				MessageBox.Show("Auth fail");
				return;
			}

			byte[] rbuf = null;
			ret = mClassic.Read(ref rbuf, sector, block);
			if(!ret) {
				MessageBox.Show("Read fail");
				return;
			}

			mFNS.unpoll();
			textBoxReadValue.Text = BitConverter.ToString(rbuf);
		}

		private void writeWidgetEnabled(bool b) {
			buttonWrite.Enabled = b;
			comboBoxWriteSector.Enabled = b;
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

			byte sector = (byte)comboBoxWriteSector.SelectedIndex;
			byte block = (byte)comboBoxWriteBlock.SelectedIndex;

			ret = mClassic.Auth(sector, block);
			if(!ret) {
				MessageBox.Show("Auth fail");
				return;
			}

			ret = mClassic.Write(mWriteValue, sector, block);
			if(!ret) {
				MessageBox.Show("Write fail");
				writeWidgetEnabled(true);
				return;
			}
			mFNS.unpoll();

			writeWidgetEnabled(true);
		}
	}
}
