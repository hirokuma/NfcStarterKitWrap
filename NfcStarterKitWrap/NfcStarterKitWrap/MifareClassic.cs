using System;

namespace NfcStarterKitWrap {

	/// <summary>
	/// Mifare Classicアクセスクラス。
	/// </summary>
	public class MifareClassic {

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

		/// <summary>
		/// Authentication with Key A
		/// </summary>
		public const byte CMD_AUTHA = 0x60;

		/// <summary>
		/// Authentication with Key B
		/// </summary>
		public const byte CMD_AUTHB = 0x61;


		//------------------------------------------------------------------------------//
		// 非公開定義
		//------------------------------------------------------------------------------//

		private byte[] AUTH_KEY = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
		private support mFNS = null;
		private String mLastError = "";


		//------------------------------------------------------------------------------//
		// メソッド
		//------------------------------------------------------------------------------//

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="fns">init()済みのNfcStarterKit.support</param>
		public MifareClassic(support fns) {
			mFNS = fns;
		}

		/// <summary>
		/// (未サポート)
		/// </summary>
		public String LastError {
			get { return mLastError; }
		}

		/// <summary>
		/// ポーリング
		/// </summary>
		/// <returns>処理結果</returns>
		public bool polling() {
			bool b = mFNS.pollingA();
			if(b) {
				if(mFNS.NfcId.Length != 4) {
					// UID 4byteタイプしか手元にない
					b = false;
					mFNS.unpoll();
				}
			}
			return b;
		}

		/// <summary>
		/// Authentication with Key Aをデフォルト値で実施
		/// </summary>
		/// <param name="sector">セクタ</param>
		/// <param name="block">ブロック</param>
		/// <returns>処理結果</returns>
		public bool Auth(byte sector, byte block) {
			return Auth(CMD_AUTHA, sector, block, mFNS.NfcId, AUTH_KEY);
		}

		/// <summary>
		/// Authenticationをデフォルト値で実施
		/// </summary>
		/// <param name="auth">鍵の種類。CMD_AUTHx。</param>
		/// <param name="sector">セクタ</param>
		/// <param name="block">ブロック</param>
		/// <returns>処理結果</returns>
		public bool Auth(byte auth, byte sector, byte block) {
			return Auth(auth, sector, block, mFNS.NfcId, AUTH_KEY);
		}

		/// <summary>
		/// Authentication
		/// </summary>
		/// <param name="sector">セクタ</param>
		/// <param name="block">ブロック</param>
		/// <param name="key">鍵</param>
		/// <returns>処理結果</returns>
		public bool Auth(byte sector, byte block, byte[] key) {
			return Auth(CMD_AUTHA, sector, block, mFNS.NfcId, key);
		}

		/// <summary>
		/// Authentication
		/// </summary>
		/// <param name="auth">鍵の種類。CMD_AUTHx。</param>
		/// <param name="sector">セクタ</param>
		/// <param name="block">ブロック</param>
		/// <param name="key">鍵</param>
		/// <returns>処理結果</returns>
		public bool Auth(byte auth, byte sector, byte block, byte[] key) {
			return Auth(auth, sector, block, mFNS.NfcId, key);
		}

		/// <summary>
		/// Authentication
		/// </summary>
		/// <param name="auth">鍵の種類。CMD_AUTHx。</param>
		/// <param name="sector">セクタ</param>
		/// <param name="block">ブロック</param>
		/// <param name="uid">UID</param>
		/// <param name="key">鍵</param>
		/// <returns>処理結果</returns>
		private bool Auth(byte auth, byte sector, byte block, byte[] uid, byte[] key) {
			byte addr = (byte)(sector * 4 + block);
			return Auth(auth, addr, uid, key);
		}

		/// <summary>
		/// Authentication
		/// </summary>
		/// <param name="auth">鍵の種類。CMD_AUTHx。</param>
		/// <param name="addr">アドレス</param>
		/// <param name="uid">UID</param>
		/// <param name="key">鍵</param>
		/// <returns>処理結果</returns>
		private bool Auth(byte auth, byte addr, byte[] uid, byte[] key) {
			byte[] cmd = new byte[12];
			cmd[0] = auth;
			cmd[1] = addr;
			Buffer.BlockCopy(key, 0, cmd, 2, 6);
			Buffer.BlockCopy(uid, 0, cmd, 8, 4);
			UInt16 cmd_len = (UInt16)cmd.Length;
			byte[] res = new byte[256];
			UInt16 res_len = 0x00;
			bool bRet = mFNS.felica_nfc.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				mFNS.unpoll();
				return false;
			}

			if(res_len != 0) {
				mFNS.unpoll();
				return false;
			}

			return true;
		}

		/// <summary>
		/// 読み込み
		/// </summary>
		/// <param name="buf">読み込み結果。newして返すのでメモリを確保する必要はない。</param>
		/// <param name="sector">セクタ</param>
		/// <param name="block">ブロック</param>
		/// <returns>処理結果</returns>
		public bool Read(ref byte[] buf, byte sector, byte block) {
			byte addr = (byte)(sector * 4 + block);

			byte[] cmd = new byte[2] { 0x30, addr };
			UInt16 cmd_len = (UInt16)cmd.Length;
			byte[] res = new byte[256];
			UInt16 res_len = 0x00;
			bool bRet = mFNS.felica_nfc.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				mFNS.unpoll();
				return false;
			}

			if(res_len != READABLE_SIZE) {
				mFNS.unpoll();
				return false;
			}
			buf = new byte[res_len];
			Buffer.BlockCopy(res, 0, buf, 0, res_len);

			return true;
		}

		/// <summary>
		/// 書き込み
		/// </summary>
		/// <param name="buf">書き込みデータ</param>
		/// <param name="sector">セクタ</param>
		/// <param name="block">ブロック</param>
		/// <returns>処理結果</returns>
		public bool Write(byte[] buf, byte sector, byte block) {
			if(buf.Length < WRITABLE_SIZE) {
				return false;
			}
			byte addr = (byte)(sector * 4 + block);
			byte[] cmd = new byte[2 + WRITABLE_SIZE];
			cmd[0] = 0xa0;
			cmd[1] = addr;
			Buffer.BlockCopy(buf, 0, cmd, 2, WRITABLE_SIZE);
			UInt16 cmd_len = (UInt16)cmd.Length;
			byte[] res = new byte[256];
			UInt16 res_len = 0x00;
			bool bRet = mFNS.felica_nfc.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				mFNS.unpoll();
				return false;
			}

			if(res_len != 0) {
				mFNS.unpoll();
				return false;
			}

			return true;
		}

	}
}
