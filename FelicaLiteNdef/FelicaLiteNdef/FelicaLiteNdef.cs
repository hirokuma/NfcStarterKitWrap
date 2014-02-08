using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HandoverWifi_FeliCaLite {
	public partial class FelicaLiteNdef : Form {
		private NfcStarterKitWrap.nfc mFNS = new NfcStarterKitWrap.nfc();
		private NfcStarterKitWrap.FelicaLite mLite = null;
		private byte[] mWriteData = null;
		private byte[] kType3Head = {
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
		private byte[] kPart1 = {
								//Handover Select
								0x91,			//Begin Short WKS		[0x10]
								0x01,			//Type Length
								0x0a,			//Payload Length
								(byte)'T',

		};
			
	
		public FelicaLiteNdef() {
			if(!mFNS.init(this)) {
				MessageBox.Show("SDK for NFC Starter Kit fail");
				Environment.Exit(0);
				return;
			}

			InitializeComponent();

			mLite = new NfcStarterKitWrap.FelicaLite(mFNS);


			//書き込みバッファ
			mWriteData = new byte[NfcStarterKitWrap.nfc.BLOCK_SIZE * 0x0e];

			//書き込みデータ構築
			int pos = 0;
			Buffer.BlockCopy(kType3Head, 0, mWriteData, 0, kType3Head.Length);
			pos = kType3Head.Length;

			Buffer.BlockCopy(kPart1, 0, mWriteData, pos, kPart1.Length);
			pos += kPart1.Length;

			//ユーザブロック
			int blocks = mWriteData.Length / NfcStarterKitWrap.nfc.BLOCK_SIZE;
			StringBuilder sb = new StringBuilder((16 * 2 + 15 + 2) * blocks);
			for(int blk = 0; blk < blocks; blk++) {
				sb.Append(BitConverter.ToString(mWriteData, 16 * blk, 16));
				sb.Append("\r\n");
			}
			textAfter.Text = sb.ToString();
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

			DialogResult dr = MessageBox.Show("Write ?", "confirm", MessageBoxButtons.YesNo);
			if(dr == System.Windows.Forms.DialogResult.Yes) {
				ret = writeBlock();
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

			bool ret = mFNS.pollingF();

			return ret;
		}

		/// <summary>
		/// 事後処理
		/// </summary>
		private void afterProc() {
			mFNS.unpoll();
		}


		/// <summary>
		/// FeliCa Liteへの書き込み
		/// </summary>
		/// <returns></returns>
		private bool writeBlock() {
			bool ret;

			//ユーザブロック
			int blocks = mWriteData.Length / NfcStarterKitWrap.nfc.BLOCK_SIZE;
			for(ushort blk = 0; blk < blocks; blk++) {
				ret = mLite.Write(mWriteData, blk, 16 * blk);
				if(!ret) {
					return false;
				}
			}
			return true;
		}
	}
}
