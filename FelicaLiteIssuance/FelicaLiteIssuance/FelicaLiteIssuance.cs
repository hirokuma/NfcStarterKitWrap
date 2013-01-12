using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FelicaLiteIssuance {
	public partial class FelicaLiteIssuance : Form {

		private NfcStarterKitWrap.nfc mFNS = new NfcStarterKitWrap.nfc();
		private NfcStarterKitWrap.FelicaLite mLite = null;
		private byte[] mMasterKey = new byte[NfcStarterKitWrap.FelicaLite.MASTERKEY_SIZE];
		private byte[] mKeyVersion = new byte[NfcStarterKitWrap.FelicaLite.KEYVERSION_SIZE];
		private byte[] mDFD = new byte[NfcStarterKitWrap.FelicaLite.DFD_SIZE];


		public FelicaLiteIssuance() {
			if(mFNS.init()) {
				mLite = new NfcStarterKitWrap.FelicaLite(mFNS);
				InitializeComponent();
			} else {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
			}
		}

		private void FelicaLiteIssuance_FormClosed(object sender, FormClosedEventArgs e) {
			mFNS.term();
		}

		private void textBoxMasterKey_TextChanged(object sender, EventArgs e) {
			checkText();
		}

		private void textBoxKeyVersion_TextChanged(object sender, EventArgs e) {
			checkText();
		}

		private void textBoxDFD_TextChanged(object sender, EventArgs e) {
			checkText();
		}

		private void checkText() {
			buttonIssuance1.Enabled = false;
			textBoxSafety.Text = "";

			if(textBoxMasterKey.Text.Length != NfcStarterKitWrap.FelicaLite.MASTERKEY_SIZE*2) {
				return;
			}
			if(textBoxKeyVersion.Text.Length != NfcStarterKitWrap.FelicaLite.KEYVERSION_SIZE*2) {
				return;
			}
			if(textBoxDFD.Text.Length != NfcStarterKitWrap.FelicaLite.DFD_SIZE*2) {
				return;
			}

			for(int len = 0; len < NfcStarterKitWrap.FelicaLite.MASTERKEY_SIZE; len++) {
				try {
					mMasterKey[len] = (byte)Convert.ToInt32(textBoxMasterKey.Text.Substring(len*2, 2), 16);
				}
				catch(Exception) {
					return;
				}
			}
			for(int len = 0; len < NfcStarterKitWrap.FelicaLite.KEYVERSION_SIZE; len++) {
				try {
					mKeyVersion[len] = (byte)Convert.ToInt32(textBoxKeyVersion.Text.Substring(len * 2, 2), 16);
				}
				catch(Exception) {
					return;
				}
			}
			for(int len = 0; len < NfcStarterKitWrap.FelicaLite.DFD_SIZE; len++) {
				try {
					mDFD[len] = (byte)Convert.ToInt32(textBoxDFD.Text.Substring(len * 2, 2), 16);
				}
				catch(Exception) {
					return;
				}
			}

			textBoxSafety.Text = mLite.MasterKeyWeakness(mMasterKey).ToString();

			buttonIssuance1.Enabled = true;
		}


		private void buttonIssuance1_Click(object sender, EventArgs e) {
			
			if(!mLite.CheckSystemCode()) {
				MessageBox.Show("not FeliCa Lite card");
				return;
			}

			if(!mLite.CheckIssued()) {
				MessageBox.Show("Issued FeliCa Lite card");
				return;
			}

			if(!mLite.Issuance1(mDFD, mMasterKey, mKeyVersion)) {
				MessageBox.Show("Fail");
				return;
			}

			MessageBox.Show("Success");
		}


		private void buttonTest_Click(object sender, EventArgs e) {
			textBoxResult.Text = "";

			byte[] testKey = new byte[NfcStarterKitWrap.FelicaLite.MASTERKEY_SIZE];
			if(textBoxTest.Text.Length != NfcStarterKitWrap.FelicaLite.MASTERKEY_SIZE*2) {
				textBoxResult.Text = "invalid1";
				return;
			}
			for(int len = 0; len < NfcStarterKitWrap.FelicaLite.MASTERKEY_SIZE; len++) {
				try {
					testKey[len] = (byte)Convert.ToInt32(textBoxTest.Text.Substring(len * 2, 2), 16);
				}
				catch(Exception) {
					textBoxResult.Text = "invalid2";
					return;
				}
			}

			if(!mLite.CheckSystemCode()) {
				textBoxResult.Text = "invalid4";
				return;
			}
			if(!mLite.CheckIssued()) {
				textBoxResult.Text = "invalid5";
				return;
			}
			if(!mLite.CheckMac(testKey)) {
				textBoxResult.Text = "NG";
				return;
			}

			textBoxResult.Text = "OK";
		}
	}
}
