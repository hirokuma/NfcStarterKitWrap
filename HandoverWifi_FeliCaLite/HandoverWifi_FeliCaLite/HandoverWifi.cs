using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NfcStarterKitWrap;

namespace HandoverWifi_FeliCaLite {
	public partial class HandoverWifi : Form {
		private nfc mFNS = new nfc();
		private FelicaLite mLite = null;
		private byte[] mWriteMc = null;
		private byte[] mNfcId2 = new byte[nfc.NFCID2_SIZE];

		public HandoverWifi() {
			if(!mFNS.init(this)) {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
				return;
			}

			InitializeComponent();

			comboAuth.SelectedIndex = 0;
			comboEnc.SelectedIndex = 0;
			mLite = new NfcStarterKitWrap.FelicaLite(mFNS);
		}

		private void buttonRead_Click(object sender, EventArgs e) {
			bool ret;

			//書き込み不可
			buttonWrite.Enabled = false;
			textBefore.Text = "";

			ret = prevProc();
			if(!ret) {
				afterProc();
				MessageBox.Show("Check fail");
				return;
			}

			StringBuilder sb = readUserBlock();

			//最後まで成功
			if(sb != null) {
				textBefore.Text = sb.ToString();
			}
			else {
				afterProc();
				return;
			}

			sb = readMcBlock();
			if(sb != null) {
				textMcBefore.Text = sb.ToString();
			}
			else {
				afterProc();
				return;
			}

			afterProc();

			//書き込み可能
			buttonWrite.Enabled = true;
		}

		private void buttonWrite_Click(object sender, EventArgs e) {
			bool ret;

			//カードの存在チェック
			ret = prevProc();
			if(!ret) {
				afterProc();
				MessageBox.Show("Polling fail");
				return;
			}

			//Readしたカードと同じかチェック
			for(int i = 0; i < mNfcId2.Length; i++) {
				if(mFNS.NfcId[i] != mNfcId2[i]) {
					afterProc();
					MessageBox.Show("Bad card");
					return;
				}
			}

			DialogResult dr = MessageBox.Show("Write ?", "confirm", MessageBoxButtons.YesNo);
			if(dr == System.Windows.Forms.DialogResult.Yes) {
				ret = writeCard(textSsid.Text, textKey.Text, comboAuth.SelectedIndex, comboEnc.SelectedIndex, textMac.Text);
				if(ret) {
					MessageBox.Show("Success!", "success");
				}
				else {
					MessageBox.Show("Fail...", "fail");
				}
			}


			afterProc();
		}


		/// <summary>
		/// 事前処理
		/// </summary>
		/// <returns></returns>
		private bool prevProc() {
			//入力欄
			if(textSsid.Text.Length == 0) {
				return false;
			}
			if(textKey.Text.Length == 0) {
				return false;
			}
			if(textMac.Text.Length != 12) {
				return false;
			}
			for(int i = 0; i < 12; i += 2) {
				try {
					int b = Convert.ToByte(textMac.Text.Substring(i, 2), 16);
				}
				catch(Exception) {
					return false;
				}
			}


			bool ret = mFNS.pollingF();
			if(ret) {
				Buffer.BlockCopy(mFNS.NfcId, 0, mNfcId2, 0, mNfcId2.Length);
			}
			else {
				Array.Clear(mNfcId2, 0, mNfcId2.Length);
			}

			return ret;
		}

		/// <summary>
		/// 事後処理
		/// </summary>
		private void afterProc() {
			mFNS.unpoll();

			buttonWrite.Enabled = false;
		}

		private StringBuilder readUserBlock() {
			bool ret = false;
			byte[] rbuf;
			StringBuilder sb = new StringBuilder((16 * 2 + 15 + 2) * 14);		//16byteとそのデリミタと\r\n

			for(ushort blk = FelicaLite.BLOCK_PAD0; blk <= FelicaLite.BLOCK_PAD13; blk++) {
				rbuf = null;
				ret = mLite.Read(ref rbuf, blk);
				if(ret) {
					sb.Append(BitConverter.ToString(rbuf, 0));
					sb.Append("\r\n");
				}
				else {
					break;
				}
			}

			if(!ret) {
				sb = null;
			}


			return sb;
		}

		/// <summary>
		/// MCブロックを読む。
		/// 
		/// </summary>
		/// <returns></returns>
		private StringBuilder readMcBlock() {
			bool ret = false;
			StringBuilder sb = new StringBuilder(16 * 2 + 15);		//16byteとそのデリミタと\r\n

			mWriteMc = null;
			ret = mLite.Read(ref mWriteMc, FelicaLite.BLOCK_MC);
			if(ret) {
				sb.Append(BitConverter.ToString(mWriteMc, 0));
				if(mWriteMc[3] == 0x01) {
					//NDEFシステムコードだったら、MCブロックは書き込まない
					mWriteMc = null;
				}
			}

			return sb;
		}


		private bool writeCard(string ssid, string key, int auth, int enc, string mac) {
			//Authentication Type:インデックス値→WPS値
			byte auth_val;
			switch(auth) {
			case 0:	//OPEN
				auth_val = 0x01;
				break;
			case 1:	//WPA/PSK
				auth_val = 0x02;
				break;
			case 2:	//SHARED
				auth_val = 0x04;
				break;
			case 3:	//WPA
				auth_val = 0x08;
				break;
			case 4:	//WPA2
				auth_val = 0x10;
				break;
			case 5:	//WPA2/PSK
				auth_val = 0x20;
				break;
			default:
				auth_val = 0x00;
				break;
			}

			//Encryption Type:インデックス値→WPS値
			byte enc_val;
			switch(enc) {
			case 0:	//NONE
				enc_val = 0x01;
				break;
			case 1:	//WEP
				enc_val = 0x02;
				break;
			case 2:	//TKIP
				enc_val = 0x04;
				break;
			case 3:	//AES
				enc_val = 0x08;
				break;
			default:
				enc_val = 0x00;
				break;
			}
	
			NdefMessage msg = new NdefMessage();
			NdefRecord rec_hs = new NdefRecord();	//Handover(static)
			NdefRecord rec_ac = new NdefRecord();	//rec_hsのペイロードになる
			NdefRecord rec_cr = new NdefRecord();	//Config Record

			byte[] TYPE_HS = { (byte)'H', (byte)'s' };
			byte[] TYPE_AC = { (byte)'a', (byte)'c' };
			byte[] TYPE_CR_WIFI = {
								0x61, 0x70, 0x70, 0x6c, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2f,
								//a   p     p     l     i     c     a     t     i     o     n     /
								0x76, 0x6e, 0x64, 0x2e, 0x77, 0x66, 0x61, 0x2e, 0x77, 0x73, 0x63,
								//v   n     d     .     w     f     a     .     w     s     c
							 };

			rec_hs.MB = true;
			rec_hs.ME = false;
			rec_hs.setType(NdefRecord.TNF_TYPE.WKS, TYPE_HS);

			rec_ac.MB = true;
			rec_ac.ME = true;
			rec_ac.setType(NdefRecord.TNF_TYPE.WKS, TYPE_AC);
			rec_ac.Payload = new byte[] { 0x01, 0x01, (byte)'0', 0x00 };
			byte[] rec_ac_byte = rec_ac.getRecord();

			byte[] hs_pl = new byte[1 + rec_ac_byte.Length];
			hs_pl[0] = 0x12;		//version
			Buffer.BlockCopy(rec_ac_byte, 0, hs_pl, 1, rec_ac_byte.Length);
			rec_hs.Payload = hs_pl;

			rec_cr.MB = false;
//			rec_cr.ME = true;	//test
			rec_cr.ME = false;
			rec_cr.setType(NdefRecord.TNF_TYPE.MIME, TYPE_CR_WIFI);
			rec_cr.ID = new byte[] { (byte)'0' };

			byte[] ssid_byte = System.Text.Encoding.UTF8.GetBytes(ssid);
			byte[] key_byte = System.Text.Encoding.UTF8.GetBytes(key);
			byte[] mac_byte = new byte[6];
			for(int i = 0; i < 6; i++) {
				mac_byte[i] = Convert.ToByte(mac.Substring(i * 2, 2), 16);
			}
			List<TLV> tlv_cred = new List<TLV> {
				new TLV(0x1026, 0x0001, new byte[] { 0x01 }),
				new TLV(0x1045, (ushort)ssid_byte.Length, ssid_byte),
				new TLV(0x1003, 0x0002, new byte[] { 0x00, auth_val }),
				new TLV(0x100f, 0x0002, new byte[] { 0x00, enc_val }),
				new TLV(0x1027, (ushort)key.Length, key_byte),
				new TLV(0x1020, 0x0006, mac_byte)
			};
			int tlv_cred_len = 0;
			foreach(TLV tlv in tlv_cred) {
				tlv_cred_len += tlv.getLength();
			}
			byte[] tlv_cred_byte = new byte[tlv_cred_len];
			int pos = 0;
			foreach(TLV tlv in tlv_cred) {
				byte[] tlv_byte = tlv.get();
				Buffer.BlockCopy(tlv_byte, 0, tlv_cred_byte, pos, tlv_byte.Length);
				pos += tlv_byte.Length;
			}

			List<TLV> tlvs = new List<TLV> { 
				new TLV(0x104a, 0x0001, new byte[] { 0x10 }),
				new TLV(0x100e, (ushort)tlv_cred_byte.Length, tlv_cred_byte),
				new TLV(0x1049, 0x0006, new byte[] { 0x00, 0x37, 0x2a, 0x00, 0x01, 0x20 })
			};
			int tlvs_len = 0;
			foreach(TLV tlv in tlvs) {
				tlvs_len += tlv.getLength();
			}
			byte[] tlvs_byte = new byte[tlvs_len];
			pos = 0;
			foreach(TLV tlv in tlvs) {
				byte[] tlv_byte = tlv.get();
				Buffer.BlockCopy(tlv_byte, 0, tlvs_byte, pos, tlv_byte.Length);
				pos += tlv_byte.Length;
			}
			rec_cr.Payload = tlvs_byte;

			msg.Add(rec_hs);
			msg.Add(rec_cr);

			byte[] msg_byte = msg.getMessage();

			//Nexus7のバグなのか知らないが、113～127byteのNDEFデータは読めない
			if((113 <= msg_byte.Length) && (msg_byte.Length <= 127)) {
				MessageBox.Show("Nexus7 cannot Read this NDEF data.\nSo padding with Empty NDEF Record");

				byte[] DUMMY_NDEF = new byte[] {
					0x10,	//MB=0, ME=0, SR=1, EMPTY
					0x00,	//Type Length = 0
					0x00,	//Payload Length = 0

					0x10,	//MB=0, ME=0, SR=1, EMPTY
					0x00,	//Type Length = 0
					0x00,	//Payload Length = 0

					0x10,	//MB=0, ME=0, SR=1, EMPTY
					0x00,	//Type Length = 0
					0x00,	//Payload Length = 0

					0x10,	//MB=0, ME=0, SR=1, EMPTY
					0x00,	//Type Length = 0
					0x00,	//Payload Length = 0

					0x50,	//MB=0, ME=1, SR=1, EMPTY
					0x00,	//Type Length = 0
					0x00,	//Payload Length = 0
				};
				byte[] new_msg_byte = new byte[msg_byte.Length + DUMMY_NDEF.Length];
				Buffer.BlockCopy(msg_byte, 0, new_msg_byte, 0, msg_byte.Length);
				Buffer.BlockCopy(DUMMY_NDEF, 0, new_msg_byte, msg_byte.Length, DUMMY_NDEF.Length);
				msg_byte = new_msg_byte;
			}

			bool ret = false;

			//ユーザブロック
			int blocks = (msg_byte.Length + nfc.BLOCK_SIZE - 1) / nfc.BLOCK_SIZE;
			for(ushort blk = 0; blk < blocks; blk++) {
				ret = mLite.Write(msg_byte, (ushort)(1 + blk), 16 * blk);
			}

			//MCブロック
			if(ret && (mWriteMc != null)) {
				ret = mLite.Write(mWriteMc, FelicaLite.BLOCK_MC);
			}

			//Type3ヘッダ
			if(ret) {
				ret = writeType3Head(msg_byte.Length);
				//ret = writeType3Head(128);
			}

			return ret;
		}

		private bool writeType3Head(int len) {
			byte[] Type3Head = {
								0x10,						//Ver		[0x00]
								0x04,						//Nbr
								0x01,						//Nbw
								0x00, 0x0d,					//Nmaxb
								0x00, 0x00, 0x00, 0x00,		//resv
								0x00,						//WriteF
								0x01,						//RW
								0x00, 0x00, 0x00,			//Ln
								0x00, 0x00					//ChkSum
			};

			Type3Head[0x0b] = (byte)((len >> 16) & 0xff);
			Type3Head[0x0c] = (byte)((len >> 8) & 0xff);
			Type3Head[0x0d] = (byte)(len & 0xff);
			ushort sum = 0;
			for(int i = 0; i < 14; i++) {
				sum += (ushort)Type3Head[i];
			}
			Type3Head[0x0e] = (byte)((sum >> 8) & 0xff);	//ChkSum
			Type3Head[0x0f] = (byte)(sum & 0xff);			//ChkSum

			bool ret = mLite.Write(Type3Head, 0);
			return ret;
		}


		class TLV {
			private ushort mType;
			private ushort mLen = 0;
			private byte[] mValue = null;

			public TLV() {
			}

			public TLV(ushort type, ushort len, byte[] val) {
				set(type, len, val);
			}

			public void set(ushort type, ushort len, byte[] val) {
				mType = type;
				mLen = len;
				if(mLen > 0) {
					mValue = val;
				}
				else {
					mValue = null;
				}
			}

			public byte[] get() {
				byte[] tlv = new byte[4 + mValue.Length];
				tlv[0] = (byte)(mType >> 8);
				tlv[1] = (byte)(mType & 0xff);
				tlv[2] = (byte)(mLen >> 8);
				tlv[3] = (byte)(mLen & 0xff);
				if(mLen > 0) {
					Buffer.BlockCopy(mValue, 0, tlv, 4, mValue.Length);
				}

				return tlv;
			}

			public int getLength() {
				return mLen + 4;
			}
		}

	}
}
