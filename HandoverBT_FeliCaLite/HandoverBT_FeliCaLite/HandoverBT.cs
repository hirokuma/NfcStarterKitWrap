using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NfcStarterKitWrap;

namespace HandoverBT_FeliCaLite {
	public partial class HandoverBt : Form {
		private nfc mFNS = new nfc();
		private FelicaLite mLite = null;
		private byte[] mWriteMc = null;
		private byte[] mNfcId2 = new byte[nfc.NFCID2_SIZE];

		public HandoverBt() {
			if(!mFNS.init(this)) {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
				return;
			}

			InitializeComponent();

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
				ret = writeCard(textMac.Text);
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


		private bool writeCard(string mac) {
	
			NdefMessage msg = new NdefMessage();
			NdefRecord rec_hs = new NdefRecord();	//Handover(static)
			NdefRecord rec_ac = new NdefRecord();	//rec_hsのペイロードになる
			NdefRecord rec_cr = new NdefRecord();	//Config Record

			byte[] TYPE_HS = { (byte)'H', (byte)'s' };
			byte[] TYPE_AC = { (byte)'a', (byte)'c' };
			byte[] TYPE_CR_BT = {
								0x61, 0x70, 0x70, 0x6c, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2f,
								//a   p     p     l     i     c     a     t     i     o     n     /
								0x76, 0x6e, 0x64, 0x2e, 0x62, 0x6c, 0x75, 0x65, 0x74, 0x6f, 0x6f, 0x74, 0x68, 0x2e, 0x65, 0x70, 0x2e, 0x6f, 0x6f, 0x62
								//v   n     d     .     b     l     u     e     t     o     o     t     h     .     e     p     .     o     o     b
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
			rec_cr.ME = true;
			rec_cr.setType(NdefRecord.TNF_TYPE.MIME, TYPE_CR_BT);
			rec_cr.ID = new byte[] { (byte)'0' };

			byte[] mac_byte = new byte[6];
			for(int i = 0; i < 6; i++) {
				mac_byte[i] = Convert.ToByte(mac.Substring((5-i) * 2, 2), 16);
			}

            string localname = "hiro99ma";
			List<TLV> tlvs = new List<TLV> { 
				new TLV(0x0d, new byte[] { 0x80, 0x06, 0x04 }),
				new TLV(0x03, new byte[] { 0x01, 0x11 }),
				new TLV(0x09, Encoding.ASCII.GetBytes(localname))
			};
			int tlvs_len = 0;
			foreach(TLV tlv in tlvs) {
				tlvs_len += tlv.getLength();
			}
			byte[] tlvs_byte = new byte[2 + 6 + tlvs_len];
			int pos = 0;

            //OOB LEN
            pos += 2;

            //MAC
            Buffer.BlockCopy(mac_byte, 0, tlvs_byte, pos, mac_byte.Length);
            pos += 6;

            //TLV
			foreach(TLV tlv in tlvs) {
				byte[] tlv_byte = tlv.get();
				Buffer.BlockCopy(tlv_byte, 0, tlvs_byte, pos, tlv_byte.Length);
				pos += tlv_byte.Length;
			}

            //OOB LEN
            ushort oob_len = (ushort)(pos - 2);
            tlvs_byte[0] = (byte)(oob_len & 0xff);
            tlvs_byte[1] = (byte)(oob_len >> 8);
            
            rec_cr.Payload = tlvs_byte;

			int len2 = rec_hs.getLength() + rec_cr.getLength();

			msg.Add(rec_hs);
			msg.Add(rec_cr);

			byte[] msg_byte = msg.getMessage();

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
			private byte mType;
			private byte mLen = 0;          //データ部の長さ(EIR LENは+1する)
			private byte[] mValue = null;

			public TLV() {
			}

			public TLV(byte type,byte[] val) {
				set(type, val);
			}

			public void set(byte type, byte[] val) {
				mType = type;
				mLen = (byte)val.Length;
				mValue = val;
			}

			public byte[] get() {
				byte[] tlv = new byte[2 + mValue.Length];
				tlv[0] = (byte)(mLen + 1);
                tlv[1] = mType;
				Buffer.BlockCopy(mValue, 0, tlv, 2, mValue.Length);

				return tlv;
			}

            /// <summary>
            /// EIR LENを含めたサイズ
            /// </summary>
            /// <returns></returns>
			public int getLength() {
				return mLen + 2;
			}
		}

	}
}
