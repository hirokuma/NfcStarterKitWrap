using System;

namespace NfcStarterKitWrap {

	/// <summary>
	/// FeliCaアクセスクラス。
	/// といっても、三者間通信のPUSHしかできない。
	/// </summary>
	public class Felica {

		//------------------------------------------------------------------------------//
		// 公開定義
		//------------------------------------------------------------------------------//

		/// <summary>
		/// 書き込み可能サイズ(単位：byte)
		/// </summary>
		public const int WRITABLE_SIZE = 16;

		/// <summary>
		/// 読み込み可能サイズ(単位：byte)
		/// </summary>
		public const int READABLE_SIZE = 16;


		//------------------------------------------------------------------------------//
		// 非公開定義
		//------------------------------------------------------------------------------//

		private support mFNS = null;
		private String mLastError = "";


		//------------------------------------------------------------------------------//
		// メソッド
		//------------------------------------------------------------------------------//

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="fns">init()済みのNfcStarterKit.support</param>
		public Felica(support fns) {
			mFNS = fns;
		}

		/// <summary>
		/// (未サポート)
		/// </summary>
		public String LastError {
			get { return mLastError; }
		}

		/// <summary>
		/// ポーリング実行
		/// </summary>
		/// <returns>処理結果</returns>
		public bool polling() {
			return mFNS.pollingF();
		}

		/// <summary>
		/// 三者間通信実施
		/// </summary>
		/// <param name="data">送信データ。三者間通信のフォーマットに従っていること。</param>
		/// <param name="data_len">送信データサイズ。最大224byte。</param>
		/// <returns>処理結果</returns>
		public bool push(byte[] data, int data_len) {
			if(data_len > 224) {
				return false;
			}

			byte[] cmd = new byte[1 + 10 + data_len];
			cmd[0] = (byte)cmd.Length;
			cmd[1] = (byte)0xb0;
			Buffer.BlockCopy(mFNS.NfcId, 0, cmd, 2, 8);
			cmd[10] = (byte)data_len;
			Buffer.BlockCopy(data, 0, cmd, 11, data_len);
			UInt16 cmd_len = (UInt16)cmd[0];
			byte[] res = new byte[256];
			UInt16 res_len = 0x00;
			bool bRet = mFNS.felica_nfc.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcThru\n");
				mFNS.unpoll();
				return false;
			}


			// xx:IDm
			// [cmd]a4 xx xx xx xx xx xx xx xx 00
			cmd[0] = 11;
			cmd[1] = (byte)0xa4;			//inactivate? activate2?
			cmd[10] = 0x00;
			cmd_len = (UInt16)cmd[0];
			res_len = 0x00;
			bRet = mFNS.felica_nfc.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcThru\n");
				mFNS.unpoll();
				return false;
			}

			return true;
		}

		/// <summary>
		/// 三者間通信：URL
		/// </summary>
		/// <param name="url">送信するURL</param>
		/// <returns>処理結果</returns>
		public bool pushUrl(string url) {
			bool ret = false;
			byte[] data = null;
			int data_len = 0;

			byte[] str_byte = System.Text.Encoding.ASCII.GetBytes(url);
			short str_len = (short)(url.Length + 2);
			data = new byte[256];

			int chksum = 0;
			int cnt = 0;

			//
			data[cnt] = 0x01;
			chksum += data[cnt] & 0xff;
			cnt++;

			// header
			data[cnt] = 0x02;		//URL
			chksum += data[cnt] & 0xff;
			cnt++;
			data[cnt] = (byte)(str_len & 0x00ff);
			chksum += data[cnt] & 0xff;
			cnt++;
			data[cnt] = (byte)((str_len & 0xff00) >> 8);
			chksum += data[cnt] & 0xff;
			cnt++;

			str_len -= 2;

			// param
			data[cnt] = (byte)(str_len & 0x00ff);
			chksum += data[cnt] & 0xff;
			cnt++;
			data[cnt] = (byte)((str_len & 0xff00) >> 8);
			chksum += data[cnt] & 0xff;
			cnt++;
			for(int i=0; i<str_len; i++) {
				data[cnt] = str_byte[i];
				chksum += data[cnt] & 0xff;
				cnt++;
			}

			//check sum
			short sum = (short)-chksum;
			data[cnt] = (byte)((sum & 0xff00) >> 8);
			cnt++;
			data[cnt] = (byte)(sum & 0x00ff);
			cnt++;

			data_len = cnt;
			ret = push(data, data_len);

			return ret;
		}
	}
}
