using System;

namespace NfcStarterKitWrap {

	/// <summary>
	/// Mifare Ultralightアクセスクラス
	/// </summary>
	public class MifareUltralight {

		//------------------------------------------------------------------------------//
		// 公開定義
		//------------------------------------------------------------------------------//

		/// <summary>
		/// 書き込み可能サイズ(単位：byte)
		/// </summary>
		public const int WRITABLE_SIZE = 4;

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
		public MifareUltralight(support fns) {
			mFNS = fns;
		}

		/// <summary>
		/// (未サポート)
		/// </summary>
		public String LastError {
			get { return mLastError; }
		}

		/// <summary>
		/// 読み込み
		/// </summary>
		/// <param name="buf">読み込み結果。newして返すのでメモリを確保する必要はない。</param>
		/// <param name="block">ブロック</param>
		/// <returns>処理結果</returns>
		public bool Read(ref byte[] buf, byte block) {
			return mFNS.NfcA_Read(ref buf, block);
		}

		/// <summary>
		/// 書き込み
		/// </summary>
		/// <param name="buf">書き込みデータ</param>
		/// <param name="block">ブロック</param>
		/// <returns>処理結果</returns>
		public bool Write(byte[] buf, byte block) {
			return mFNS.NfcA_Write(buf, block);
		}
	}
}
