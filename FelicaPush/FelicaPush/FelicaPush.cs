using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FelicaLiteReadWrite {
	public partial class UltralightReadWrite : Form {

		private NfcStarterKitWrap.support mFNS = new NfcStarterKitWrap.support();
		private NfcStarterKitWrap.Felica mFelica = null;
		private byte[] mWriteValue = new byte[NfcStarterKitWrap.Felica.WRITABLE_SIZE];

		public UltralightReadWrite() {
			if(!mFNS.init()) {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
				return;
			}
			InitializeComponent();

			mFelica = new NfcStarterKitWrap.Felica(mFNS);
		}

		private void FelicaLiteReadWrite_FormClosed(object sender, FormClosedEventArgs e) {
			mFNS.term();
		}

		private void textBoxUrl_TextChanged(object sender, EventArgs e) {
			buttonPush.Enabled = (textBoxUrl.Text.Length != 0);
		}

		private void buttonPush_Click(object sender, EventArgs e) {
			buttonPush.Enabled = false;
			bool b = mFelica.polling();
			if(!b) {
				MessageBox.Show("Polling fail");
				buttonPush.Enabled = true;
				return;
			}
			b = mFelica.pushUrl(textBoxUrl.Text);
			if(!b) {
				MessageBox.Show("Polling fail");
			}
			mFNS.unpoll();
			buttonPush.Enabled = true;
		}

	}
}
