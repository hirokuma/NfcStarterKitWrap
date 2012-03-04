using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FelicaLiteReadWrite {
	public partial class FelicaLiteReadWrite : Form {

		private NfcStarterKitWrap.support mFNS = new NfcStarterKitWrap.support();
		private NfcStarterKitWrap.FelicaLite mLite = null;
		private byte[] mWriteValue = new byte[NfcStarterKitWrap.support.BLOCK_SIZE];

		public FelicaLiteReadWrite() {
			if(!mFNS.init()) {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
				return;
			}
			InitializeComponent();
			comboBoxReadBlock.SelectedIndex = 0;
			comboBoxWriteBlock.SelectedIndex = 0;

			mLite = new NfcStarterKitWrap.FelicaLite(mFNS);
		}

		private void FelicaLiteReadWrite_FormClosed(object sender, FormClosedEventArgs e) {
			mFNS.term();
		}

		private void textBoxWriteValue_TextChanged(object sender, EventArgs e) {
			buttonWrite.Enabled = false;

			if(textBoxWriteValue.Text.Length != NfcStarterKitWrap.support.BLOCK_SIZE* 2) {
				return;
			}
			for(int len = 0; len < NfcStarterKitWrap.support.BLOCK_SIZE; len++) {
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

			bool ret;

			ret = mFNS.pollingF();
			if(!ret) {
				MessageBox.Show("Polling fail");
				return;
			}

			UInt16 block;
			if(comboBoxReadBlock.SelectedIndex <= NfcStarterKitWrap.FelicaLite.BLOCK_REG) {
				block = (UInt16)comboBoxReadBlock.SelectedIndex;
			}
			else {
				block = (UInt16)(comboBoxReadBlock.SelectedIndex
						- NfcStarterKitWrap.FelicaLite.BLOCK_REG - 1 + NfcStarterKitWrap.FelicaLite.BLOCK_RC);
			}

			byte[] rbuf = null;
			ret = mLite.Read(ref rbuf, block);
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

			bool ret;

			ret = mFNS.pollingF();
			if(!ret) {
				MessageBox.Show("Polling fail");
				writeWidgetEnabled(true);
				return;
			}

			UInt16 block;
			if(comboBoxWriteBlock.SelectedIndex <= NfcStarterKitWrap.FelicaLite.BLOCK_REG) {
				block = (UInt16)comboBoxWriteBlock.SelectedIndex;
			}
			else {
				MessageBox.Show("out of range");
				writeWidgetEnabled(true);
				return;
			}

			ret = mLite.Write(mWriteValue, block);
			if(!ret) {
				MessageBox.Show("Write fail");
				writeWidgetEnabled(true);
				return;
			}

			writeWidgetEnabled(true);
		}
	}
}
