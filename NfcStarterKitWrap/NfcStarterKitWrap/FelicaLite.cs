using System;
using System.Security.Cryptography;
using System.IO;

namespace NfcStarterKitWrap {

	/// <summary>
	/// FeliCa Liteアクセスクラス。
	/// NFC-Fでアクセスする。
	/// </summary>
	public class FelicaLite {

		//------------------------------------------------------------------------------//
		// 公開定義
		//------------------------------------------------------------------------------//

		/// <summary>
		/// ユーザブロック：PAD0
		/// </summary>
		public const UInt16 BLOCK_PAD0 = 0x00;

		/// <summary>
		/// ユーザブロック：PAD1
		/// </summary>
		public const UInt16 BLOCK_PAD1 = 0x01;

		/// <summary>
		/// ユーザブロック：PAD2
		/// </summary>
		public const UInt16 BLOCK_PAD2 = 0x02;

		/// <summary>
		/// ユーザブロック：PAD3
		/// </summary>
		public const UInt16 BLOCK_PAD3 = 0x03;

		/// <summary>
		/// ユーザブロック：PAD4
		/// </summary>
		public const UInt16 BLOCK_PAD4 = 0x04;

		/// <summary>
		/// ユーザブロック：PAD5
		/// </summary>
		public const UInt16 BLOCK_PAD5 = 0x05;

		/// <summary>
		/// ユーザブロック：PAD6
		/// </summary>
		public const UInt16 BLOCK_PAD6 = 0x06;

		/// <summary>
		/// ユーザブロック：PAD7
		/// </summary>
		public const UInt16 BLOCK_PAD7 = 0x07;

		/// <summary>
		/// ユーザブロック：PAD8
		/// </summary>
		public const UInt16 BLOCK_PAD8 = 0x08;

		/// <summary>
		/// ユーザブロック：PAD9
		/// </summary>
		public const UInt16 BLOCK_PAD9 = 0x09;

		/// <summary>
		/// ユーザブロック：PAD10
		/// </summary>
		public const UInt16 BLOCK_PAD10 = 0x0a;

		/// <summary>
		/// ユーザブロック：PAD11
		/// </summary>
		public const UInt16 BLOCK_PAD11 = 0x0b;

		/// <summary>
		/// ユーザブロック：PAD12
		/// </summary>
		public const UInt16 BLOCK_PAD12 = 0x0c;

		/// <summary>
		/// ユーザブロック：PAD13
		/// </summary>
		public const UInt16 BLOCK_PAD13 = 0x0d;

		/// <summary>
		/// ユーザブロック：REG
		/// </summary>
		public const UInt16 BLOCK_REG = 0x0e;


		/// <summary>
		/// 認証機能用ブロック：RC
		/// </summary>
		public const UInt16 BLOCK_RC = 0x80;

		/// <summary>
		/// 認証機能用ブロック：MAC
		/// </summary>
		public const UInt16 BLOCK_MAC = 0x81;

		/// <summary>
		/// システムブロック：ID
		/// </summary>
		public const UInt16 BLOCK_ID = 0x82;

		/// <summary>
		/// システムブロック：D_ID
		/// </summary>
		public const UInt16 BLOCK_D_ID = 0x83;

		/// <summary>
		/// システムブロック：SER_C
		/// </summary>
		public const UInt16 BLOCK_SER_C = 0x84;

		/// <summary>
		/// システムブロック：SYS_C
		/// </summary>
		public const UInt16 BLOCK_SYS_C = 0x85;

		/// <summary>
		/// システムブロック：CKV
		/// </summary>
		public const UInt16 BLOCK_CKV = 0x86;

		/// <summary>
		/// システムブロック：CK
		/// </summary>
		public const UInt16 BLOCK_CK = 0x87;

		/// <summary>
		/// システムブロック：MC
		/// </summary>
		public const UInt16 BLOCK_MC = 0x88;

		/// <summary>
		/// システムブロック：WCNT
		/// </summary>
		public const UInt16 BLOCK_WCNT = 0x90;

		/// <summary>
		/// システムブロック：MAC_A
		/// </summary>
		public const UInt16 BLOCK_MAC_A = 0x91;

		/// <summary>
		/// システムブロック：STATE
		/// </summary>
		public const UInt16 BLOCK_STATE = 0x92;

		/// <summary>
		/// システムブロック：CRC_CHECK
		/// </summary>
		public const UInt16 BLOCK_CRC_CHECK = 0xa0;


		/// <summary>
		/// 片側認証：マスター鍵サイズ
		/// </summary>
		public const int MASTERKEY_SIZE = 24;

		/// <summary>
		/// 片側認証：鍵バージョンサイズ
		/// </summary>
		public const int KEYVERSION_SIZE = 2;

		/// <summary>
		/// 片側認証：DFDサイズ
		/// </summary>
		public const int DFD_SIZE = 2;


		//------------------------------------------------------------------------------//
		// 非公開定義
		//------------------------------------------------------------------------------//

		private nfc mFNS = null;
		private String mLastError = "";


		//------------------------------------------------------------------------------//
		// メソッド
		//------------------------------------------------------------------------------//

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="fns">init()済みのNfcStarterKit.nfc</param>
		public FelicaLite(nfc fns) {
			mFNS = fns;
		}

		/// <summary>
		/// (未サポート)
		/// </summary>
		public String LastError {
			get { return mLastError; }
		}

		/// <summary>
		/// システムコード
		/// </summary>
		public UInt16 SystemCode {
			get { return (UInt16)((mFNS.RD[nfc.RD_SYSCODE1] << 8) | mFNS.RD[nfc.RD_SYSCODE2]); }
		}

		/// <summary>
		/// 読み込み(1ブロック)
		/// </summary>
		/// <param name="buf">受信バッファ。関数内でnewして返すので、メモリは確保しなくてよい。</param>
		/// <param name="block">読み込みブロック</param>
		/// <returns>処理結果</returns>
		public bool Read(ref byte[] buf, UInt16 block) {
			UInt16[] blocks = new UInt16[] { block };
			return Read(ref buf, blocks, 1);
		}

		/// <summary>
		/// 読み込み
		/// </summary>
		/// <param name="buf">受信バッファ。関数内でnewして返すので、メモリは確保しなくてよい。</param>
		/// <param name="block">読み込み先頭ブロック</param>
		/// <param name="block_num">読み込みブロック数。blockよりも小さい場合は、blockの先頭からblock_numだけ読み込む。</param>
		/// <returns>処理結果</returns>
		public bool Read(ref byte[] buf, UInt16[] block, byte block_num) {
			return mFNS.NfcF_Read(ref buf, block, block_num, mFNS.SERVICE_READONLY_NFCF);
		}

		/// <summary>
		/// 書き込み(1ブロック)
		/// </summary>
		/// <param name="buf">書き込みデータ</param>
		/// <param name="block">書き込みブロック</param>
		/// <returns>処理結果</returns>
		public bool Write(byte[] buf, UInt16 block) {
			UInt16[] blocks = new UInt16[] { block };
			return mFNS.NfcF_Write(buf, blocks, 1, mFNS.SERVICE_READWRITE_NFCF);
		}

		/// <summary>
		/// 書き込み(1ブロック、オフセット付き)
		/// </summary>
		/// <param name="buf">書き込みデータ</param>
		/// <param name="block">書き込みブロック</param>
		/// <param name="offset">書き込みデータの開始オフセット</param>
		/// <returns>処理結果</returns>
		public bool Write(byte[] buf, UInt16 block, int offset) {
			UInt16[] blocks = new UInt16[] { block };
			return mFNS.NfcF_Write(buf, blocks, 1, mFNS.SERVICE_READWRITE_NFCF, offset);
		}

		/// <summary>
		/// １次発行(システムブロックの書き換え禁止設定は行わない)
		/// </summary>
		/// <param name="dfd">DFD</param>
		/// <param name="masterKey">個別化マスター鍵(24byte)</param>
		/// <param name="keyVersion">鍵バージョン</param>
		/// <returns>処理結果</returns>
		public bool Issuance1(byte[] dfd, byte[] masterKey, byte[] keyVersion) {
			// 7.3.3 IDの設定
			if(!writeID(dfd)) {
				mLastError = "fail: write ID";
				return false;
			}

			// 7.3.4 カード鍵の書き込み
			// 7.3.5 カード鍵の確認
			if(!writeCardKey(masterKey)) {
				mLastError = "fail: write Card Key";
				return false;
			}

			// 7.3.6 カード鍵バージョンの書き込み
			if(!writeKeyVersion(keyVersion)) {
				mLastError = "fail: write Key Version";
				return false;
			}

			return true;
		}

		/// <summary>
		/// FeliCa Liteのシステムコードかどうか
		/// </summary>
		/// <returns>true：FeliCa Liteのシステムコードである</returns>
		public bool CheckSystemCode() {
			bool ret = mFNS.pollingF();
			if(!ret) {
				return false;
			}
			byte[] idm = mFNS.NfcId;
			byte[] syscode = mFNS.RD;

			// 7.3.2 システムコードの確認
			if(SystemCode == 0x88b4) {
				//未発行のFeliCa Liteは0x88b4のはず
			}
			else {
				return false;
			}

			return true;
		}
		
		/// <summary>
		/// 未発行確認
		/// </summary>
		/// <returns>true：未発行である</returns>
		public bool CheckIssued() {
			bool b;
			byte[] buf = null;

			b = Read(ref buf, BLOCK_MC);
			if(!b) {
				return false;
			}
			if(buf[2] == 0x00) {
				return false;
			}
			if((buf[1] & 0x80) == 0) {
				return false;
			}
			return true;
		}

		/// <summary>
		/// MAC比較
		/// </summary>
		/// <param name="masterKey">比較する個別化マスター鍵(24byte)</param>
		/// <returns>true：MAC一致</returns>
		public bool CheckMac(byte[] masterKey) {
			byte[] ck = new byte[nfc.BLOCK_SIZE];
			byte[] rbuf = null;
			bool b = Read(ref rbuf, BLOCK_ID);
			if(!b) {
				return false;
			}
			b = calcPersonalCardKey(ck, masterKey, rbuf);
			if(!b) {
				return false;
			}
			return macCheck(ck);
		}

		/// <summary>
		/// 個別化マスター鍵の強さ
		/// </summary>
		/// <param name="masterKey">個別化マスター鍵(24byte)</param>
		/// <returns>0：弱い～100：強い</returns>
		public int MasterKeyWeakness(byte[] masterKey) {
			int same = 0;
			for(int len = 0; len < MASTERKEY_SIZE/3; len++) {
				if((masterKey[len] & 0xfe) == (masterKey[8 + len] & 0xfe)) {
					same++;
				}
				if((masterKey[8 + len] & 0xfe) == (masterKey[16 + len] & 0xfe)) {
					same++;
				}
				if((masterKey[len] & 0xfe) == (masterKey[16 + len] & 0xfe)) {
					same++;
				}
			}
			//max:24 min:0

			return (int)(100.0 * (24 - same) / 24);
		}

		/////////////////////////////////////////////////////////////////////

		/**
		 * ID設定
		 *
		 */
		private bool writeID(byte[] dfd) {
			bool b;

			byte[] id = null;
			b = Read(ref id, BLOCK_D_ID);
			if(!b) {
				return false;
			}

			// DFD(2byte)
			id[8] = dfd[0];
			id[9] = dfd[1];
			// 任意のデータ(6byte)
			id[10] = (byte)'U';
			id[11] = (byte)'e';
			id[12] = (byte)'K';
			id[13] = (byte)'u';
			id[14] = (byte)'m';
			id[15] = (byte)'a';
			b = writeWithCheck(id, BLOCK_ID);
			if(!b) {
				return false;
			}

			return true;
		}


		/**
		 * チェック付きブロック書き込み(16byte)
		 *
		 * @param buf		書き込みデータ
		 * @param blockNo	書き込みブロック番号
		 *
		 * @return	true	チェックOK
		 */
		private bool writeWithCheck(byte[] buf, UInt16 block) {
			bool b;

			b = Write(buf, block);
			if(!b) {
				return false;
			}

			byte[] rbuf = null;
			b = Read(ref rbuf, block);
			if(!b) {
				return false;
			}
			for(int i = 0; i < nfc.BLOCK_SIZE; i++) {
				if(rbuf[i] != buf[i]) {
					return false;
				}
			}

			return true;
		}


		/**
		 * カード鍵書き込み(7.3.4 カード鍵の書き込み)
		 *
		 * @param masterKey	個別化マスター鍵(24byte)
		 *
		 * @return
		 */
		private bool writeCardKey(byte[] masterKey) {
			bool b;
			byte[] ck = new byte[nfc.BLOCK_SIZE];

			byte[] id = null;
			b = Read(ref id, BLOCK_ID);
			if(!b) {
				return false;
			}

			b = calcPersonalCardKey(ck, masterKey, id);
			if(!b) {
				return false;
			}

			b = Write(ck, BLOCK_CK);
			if(!b) {
				return false;
			}

			b = macCheck(ck);
			if(!b) {
				return false;
			}

			return true;
		}


		/**
		 * 鍵バージョン書き込み
		 *
		 * @param keyVersion	鍵バージョン
		 *
		 * @return		true	書き込み成功
		 */
		private bool writeKeyVersion(byte[] keyVersion) {
			byte[] buf = new byte[nfc.BLOCK_SIZE];
			buf[0] = keyVersion[0];
			buf[1] = keyVersion[1];
			bool b = writeWithCheck(buf, BLOCK_CKV);
			if(!b) {
				return false;
			}

			return true;
		}


		/**
		 * MAC比較(7.3.5 カード鍵の確認)
		 *
		 * @param ck		CKとして使用する
		 *
		 * @return		true	MAC一致
		 */
		private bool macCheck(byte[] ck) {
			byte[] rc = new byte[nfc.BLOCK_SIZE];
			Random rnd = new Random(Environment.TickCount);
			for(int i = 0; i < rc.Length; i++) {
				rc[i] = (byte)rnd.Next(0x100);
			}

			bool b = Write(rc, BLOCK_RC);
			if(!b) {
				return false;
			}
			UInt16[] blkNo = new UInt16[] { BLOCK_ID, BLOCK_MAC };
			byte[] rbuf = null;
			b = Read(ref rbuf, blkNo, 2);
			if(!b) {
				return false;
			}
			// rbuf[0-15]:ID, buf[16-23]:MAC

			// CKとRC、読み込んだIDからMACの理論値を計算
			byte[] mac = null;
			b = calcMac(ref mac, ck, rbuf, rc);
			if(!b) {
				return false;
			}

			// 理論値MACとカードMACの比較
			b = true;
			for(int i = 0; i < 8; i++) {
				if(mac[i] != rbuf[16 + i]) {
					b = false;
					break;
				}
			}

			return b;
		}


		/**
		 * MAC計算
		 *
		 * @param mac	MAC計算結果(メモリを割り当てて返す)
		 * @param ck	カード鍵(16byte)
		 * @param id	ID(16byte)
		 * @param rc	ランダムチャレンジブロック(16byte)
		 *
		 * @return		true	MAC計算成功
		 */
		private bool calcMac(ref byte[] mac, byte[] ck, byte[] id, byte[] rc) {
			mac = new byte[8];
			byte[] sk = new byte[nfc.BLOCK_SIZE];
			byte[] ips = null;

			byte[] key = new byte[24];
			for(int i = 0; i < 8; i++) {
				key[i] = key[16 + i] = ck[7 - i];
				key[8 + i] = ck[15 - i];
			}

			byte[] rc1 = new byte[8];
			byte[] rc2 = new byte[8];
			byte[] id1 = new byte[8];
			byte[] id2 = new byte[8];
			for(int i = 0; i < 8; i++) {
				rc1[i] = rc[7 - i];
				rc2[i] = rc[15 - i];
				id1[i] = id[7 - i];
				id2[i] = id[15 - i];
			}

			// RC[1]==(CK)==>SK[1]
			ips = new byte[8];	//zero
			bool b = enc83(sk, 0, key, rc1, 0, ips);		//RC1-->SK1
			if(!b) {
				return false;
			}
			// SK[1] =(iv)> RC[2] =(CK)=> SK[2]
			Buffer.BlockCopy(sk, 0, ips, 0, 8);	//SK1
			b = enc83(sk, 8, key, rc2, 0, ips);	//RC2-->SK2
			if(!b) {
				return false;
			}

			/////////////////////////////////////////////////////////
			for(int i = 0; i < 8; i++) {
				key[i] = key[16 + i] = sk[i];
				key[8 + i] = sk[8 + i];
			}

			// RC[1] =(iv)=> ID[1] =(SK)=> tmp
			Buffer.BlockCopy(rc1, 0, ips, 0, 8);	//RC1
			b = enc83(mac, 0, key, id1, 0, ips);	//ID1-->tmp
			if(!b) {
				return false;
			}

			// tmp =(iv)=> ID[2] =(SK)=> tmp
			ips = mac;			//tmp
			b = enc83(mac, 0, key, id2, 0, ips);	//ID1-->tmp
			if(!b) {
				return false;
			}

			for(int i = 0; i < 4; i++) {
				byte swp = mac[i];
				mac[i] = mac[7 - i];
				mac[7 - i] = swp;
			}

			return true;
		}


		/**
		 * 個別化カード鍵作成
		 *
		 * @param personalKey	生成した個別化カード鍵(16byte)
		 * @param masterKey		個別化マスター鍵(24byte)
		 * @param id			IDブロック(16byte)
		 *
		 * @return		true	作成成功
		 */
		private bool calcPersonalCardKey(byte[] personalKey, byte[] masterKey, byte[] id) {
			byte[] ips = new byte[8];
			byte[] enc1 = new byte[8];
			byte[] text = new byte[8];
			bool b = enc83(enc1, 0, masterKey, text, 0, ips);
			if(!b) {
				return false;
			}

			bool msb = false;
			for(int i = 7; i >= 0; i--) {
				bool bak = msb;
				msb = ((enc1[i] & 0x80) != 0) ? true : false;
				enc1[i] <<= 1;
				if(bak) {
					enc1[i] |= 0x01;
				}
			}
			if(msb) {
				enc1[7] ^= 0x1b;
			}

			byte[] id1 = new byte[8];		//M1
			byte[] id2 = new byte[8];		//M2
			for(int i = 0; i < 8; i++) {
				id1[i] = id[7 - i];
				id2[i] = (byte)(id[15 - i] ^ enc1[i]);
			}

			byte[] c1 = new byte[8];
			b = enc83(c1, 0, masterKey, id1, 0, ips);	//c1
			if(!b) {
				return false;
			}

			ips = c1;
			byte[] t = new byte[8];
			b = enc83(t, 0, masterKey, id2, 0, ips);	//t
			if(!b) {
				return false;
			}

			id1[0] ^= 0x80;		//M1'
			ips = new byte[8];
			b = enc83(c1, 0, masterKey, id1, 0, ips);	//c1'

			ips = c1;	//c1'
			b = enc83(c1, 0, masterKey, id2, 0, ips);	//t'
			if(!b) {
				return false;
			}

			for(int i = 0; i < 8; i++) {
				personalKey[i] = t[i];
				personalKey[8 + i] = c1[i];
			}

			return true;
		}


		/**
		 * Triple-DES暗号化
		 *
		 * @param outBuf		暗号化出力バッファ(8byte以上)
		 * @param outOffset		暗号化出力バッファへの書き込み開始位置(ここから8byte書く)
		 * @param key			秘密鍵(24byte [0-7]KEY1, [8-15]KEY2, [16-23]KEY1)
		 * @param inBuf			平文バッファ(8byte以上)
		 * @param inOffset		平文バッファの読み込み開始位置(ここから8byte読む)
		 * @param ips			初期ベクタ(8byte)
		 *
		 * @return		true	暗号化成功
		 */
		private bool enc83(byte[] outBuf, int outOffset, byte[] key, byte[] inBuf, int inOffset, byte[] ips) {
			byte[] key1 = new byte[8];
			byte[] key2 = new byte[8];
			byte[] key3 = new byte[8];

			Buffer.BlockCopy(key, 0, key1, 0, 8);
			Buffer.BlockCopy(key, 8, key2, 0, 8);
			Buffer.BlockCopy(key, 16, key3, 0, 8);

			byte[] in1 = new byte[8];
			Buffer.BlockCopy(inBuf, inOffset, in1, 0, 8);
			byte[] out1 = new byte[8];
			enc8(out1, key1, in1, ips);

			byte[] out2 = new byte[8];
			dec8(out2, key2, out1, ips);

			byte[] out3 = new byte[8];
			enc8(out3, key3, out2, ips);

			Buffer.BlockCopy(out3, 0, outBuf, outOffset, 8);

			return true;
		}


		/**
		 * DES暗号化
		 *
		 * @param outBuf		暗号化出力バッファ(8byte以上)
		 * @param key			秘密鍵(8byte)
		 * @param inBuf			平文バッファ(8byte以上)
		 * @param ips			初期ベクタ(8byte)
		 *
		 * @return		true	暗号化成功
		 */
		private bool enc8(byte[] outBuf, byte[] key, byte[] inBuf, byte[] ips) {
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			des.Padding = PaddingMode.None;
			MemoryStream ms = new MemoryStream();
			using(CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, ips), CryptoStreamMode.Write)) {
				cs.Write(inBuf, 0, inBuf.Length);
			}
			byte[] encryption = ms.ToArray();
			Array.Copy(encryption, encryption.Length - 8, outBuf, 0, 8);
			return true;
		}


		/**
		 * DES復号化
		 *
		 * @param outBuf		暗号化出力バッファ(8byte以上)
		 * @param key			秘密鍵(8byte)
		 * @param inBuf			平文バッファ(8byte以上)
		 * @param ips			初期ベクタ(8byte)
		 *
		 * @return		true	復号化成功
		 */
		private bool dec8(byte[] outBuf, byte[] key, byte[] inBuf, byte[] ips) {
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			des.Padding = PaddingMode.None;
			MemoryStream ms = new MemoryStream();
			using(CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, ips), CryptoStreamMode.Write)) {
				cs.Write(inBuf, 0, inBuf.Length);
			}
			byte[] encryption = ms.ToArray();
			Array.Copy(encryption, encryption.Length - 8, outBuf, 0, 8);
			return true;
		}

		//http://stackoverflow.com/questions/744530/tripledes-specified-key-is-a-known-weak-key-for-tripledes-and-cannot-be-used
	}
}
