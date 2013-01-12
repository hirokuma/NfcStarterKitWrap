using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;

namespace SmartTagRW {
	/// <summary>
	/// AIOI SYSTEMSさんのスマートタグST1020用
	/// 
	/// TOUCHやRELEASEは監視してないので、RELEASEしても情報は保持している。
	/// </summary>
	class SmartTag {

		//------------------------------------------------------------------------------//
		// 公開定義
		//------------------------------------------------------------------------------//

		/// <summary>
		/// getStatus()で取得した結果
		/// </summary>
		public class TagStatus {

			public enum ProcStat {
				INIT,			// 0x00
				COMPLETED,		// 0xF0
				BUSY			// 0xF2
			};
			public enum PowerStat {
				NORMAL1,
				NORMAL2,
				LOW1,
				LOW2,
				UNKNOWN
			};

			internal ProcStat proc = ProcStat.INIT;
			internal PowerStat pow = PowerStat.UNKNOWN;
			internal byte ver = 0xff;

			public ProcStat Proc { get { return proc; } }
			public PowerStat Pow { get { return pow; } }
			public byte Ver { get { return ver; } }

			internal void reset() {
				proc = ProcStat.INIT;
				pow = PowerStat.UNKNOWN;
				ver = 0xff;
			}
		};

		/// <summary>
		/// データ部に書き込み可能な最大ブロック数
		/// </summary>
		public const int WRITABLE_DATASIZE = 11;

		/// <summary>
		/// データ部に書き込み可能な最大バイト数
		/// </summary>
		public const int WRITABLE_DATABYTES = NfcStarterKitWrap.nfc.BLOCK_SIZE * WRITABLE_DATASIZE;

		/// <summary>
		/// レイアウト番号の最小値
		/// </summary>
		public const int LAYOUT_MIN = 1;

		/// <summary>
		/// レイアウト番号の最大値
		/// </summary>
		public const int LAYOUT_MAX = 12;

		/// <summary>
		/// 画面の横幅(単位：pixel)
		/// </summary>
		public const int IMG_WIDTH = 200;

		/// <summary>
		/// 画面の縦幅(単位：pixel)
		/// </summary>
		public const int IMG_HEIGHT = 96;


		//------------------------------------------------------------------------------//
		// 非公開定義
		//------------------------------------------------------------------------------//

		private const byte WRITE_USERDATA = 0xb0;
		private const byte READ_USERDATA = 0xc0;
		private const byte GET_STATUS = 0xd0;
		private const byte REG_IMAGE = 0xb2;
		private const byte DISPLAY = 0xa0;

		private NfcStarterKitWrap.nfc mFNS = null;
		private String mLastError = "";
		private TagStatus mStatus = new TagStatus();
		private byte mSeq = 0;


		//------------------------------------------------------------------------------//
		// メソッド
		//------------------------------------------------------------------------------//

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="fns">init()済みのNfcStarterKit.nfc</param>
		public SmartTag(NfcStarterKitWrap.nfc fns) {
			mFNS = fns;
			mFNS.setTimeout(6000);
		}

		/// <summary>
		/// (未実装)
		/// </summary>
		public String LastError {
			get { return mLastError; }
		}

		/// <summary>
		/// 最後に操作したSmartTagのステータス
		/// </summary>
		public TagStatus Status {
			get { return mStatus; }
		}

		private byte SeqNo {
			get {
				byte ret = mSeq;
				mSeq = (byte)(mSeq + 1);
				return ret;
			}
		}

		private bool polling() {
			bool b = mFNS.pollingF();
			if(b) {
				if((mFNS.NfcId[0] == 0x03)
				  && (mFNS.NfcId[1] == 0xfe)
				  && (mFNS.NfcId[2] == 0x00)
				  && (mFNS.NfcId[3] == 0x1d)) {
					// OK
				} else {
					b = false;
				}
			}
			if(!b) {
				mFNS.unpoll();
			}
			return b;
		}

		private bool Read(ref byte[] buf, byte block_num) {
			byte[] svc = new byte[2] { 0x09, 0x00 };
			UInt16[] blocks = new UInt16[block_num];
			for(int i = 0; i < block_num; i++) {
				blocks[i] = (UInt16)0;
			}
			bool b = mFNS.NfcF_Read(ref buf, blocks, block_num, svc);
			if(b) {
				mSeq = (byte)(buf[4] + 1);
			}
			return b;
		}

		private bool Write(byte[] buf, byte block_num) {
			
			byte[] svc = new byte[2] { 0x09, 0x00 };
			UInt16[] blocks = new UInt16[block_num];
			for(int i = 0; i < block_num; i++) {
				blocks[i] = (UInt16)0;
			}
			bool b = mFNS.NfcF_Write(buf, blocks, block_num, svc);
			return b;
		}

		private bool WriteCommand(byte cmd, byte[] param, byte[] data) {
			//分割
			bool ret = false;
			int data_blocks = 0;
			int data_rest = 0;
			if(data != null) {
				data_rest = data.Length;
				data_blocks = (data.Length + NfcStarterKitWrap.nfc.BLOCK_SIZE - 1) / NfcStarterKitWrap.nfc.BLOCK_SIZE;
			}
			int frames = (data_blocks + WRITABLE_DATASIZE - 1) / WRITABLE_DATASIZE;
			if(frames == 0) {
				/* データがなくても1つは送るのだよ */
				frames = 1;
			}
			int offset = 0;

			byte[] frm_data = new byte[256];
			frm_data[0] = cmd;
			frm_data[1] = (byte)frames;
			frm_data[3] = 0;
			if(param != null) {
				Buffer.BlockCopy(param, 0, frm_data, 8, 8);
			}
			for(int frm = 0; frm < frames; frm++) {
				frm_data[2] = (byte)(frm + 1);
				frm_data[4] = (byte)((cmd != GET_STATUS) ? SeqNo : 0);
				byte len = 0;
				if(data_rest > 0) {
					len = (byte)data_rest;
					if(data_rest > WRITABLE_DATABYTES) {
						len = WRITABLE_DATABYTES;
					}
					Buffer.BlockCopy(data, offset, frm_data, 16, len);

					offset += len;
					data_rest -= len;

					frm_data[3] = (byte)len;
				}
				ret = Write(frm_data, (byte)(1 + (frm_data[3] + NfcStarterKitWrap.nfc.BLOCK_SIZE - 1) / NfcStarterKitWrap.nfc.BLOCK_SIZE));
				if(!ret) {
					break;
				}

			}

			if(!ret) {
				mStatus.reset();
			}

			return ret;
		}

		/// <summary>
		/// ステータス更新
		/// </summary>
		/// <returns>処理結果</returns>
		public bool getStatus() {
			bool b;

			b = polling();
			if(!b) {
				return false;
			}

			b = WriteCommand(GET_STATUS, null, null);
			if(b) {
				byte[] buf = new byte[NfcStarterKitWrap.nfc.BLOCK_SIZE * 2];
				b = Read(ref buf, 2);
				if(b) {
					mStatus.proc = (TagStatus.ProcStat)buf[3];
					mStatus.pow = (TagStatus.PowerStat)buf[5];
					mStatus.ver = buf[15];
				}
			}
			mFNS.unpoll();
			return b;
		}

		/// <summary>
		/// レイアウト設定
		/// </summary>
		/// <param name="layout">レイアウト番号</param>
		/// <returns>処理結果</returns>
		public bool setLayout(int layout) {
			if(layout < LAYOUT_MIN || layout > LAYOUT_MAX) {
				return false;
			}

			bool b;

			b = polling();
			if(!b) {
				return false;
			}

			byte[] chg_layout = new byte[] {
				0x01, 0x01, 0xff, 0xff, 0x19, 0x03, (byte)layout, 0x01
			};
			b = WriteCommand(DISPLAY, chg_layout, null);
			mFNS.unpoll();
			return b;
		}

		private bool displayImage(byte[] img) {
			if(img == null) {
				return false;
			}
			if(img.Length < IMG_WIDTH * IMG_HEIGHT / 8) {
				return false;
			}

			bool b;

			b = polling();
			if(!b) {
				return false;
			}

			byte[] disp = new byte[] {
				0x01, 0x01, 0x00, 0x00, 0x19, 0x00, 0x00, 0x03
			};
			b = WriteCommand(DISPLAY, disp, img);
			mFNS.unpoll();
			return b;
		}

		/// <summary>
		/// 画像を転送
		/// パソコンでの「白」は画素があるところだが、SmartTagでは「黒」が画素ありということになる。
		/// そのため、内部で1/0反転させる。
		/// </summary>
		/// <param name="bm">転送する画像</param>
		/// <returns>処理結果</returns>
		public bool displayImage(Bitmap bm) {
			if(bm == null) {
				return false;
			}
			if(bm.Width < IMG_WIDTH || bm.Height < IMG_HEIGHT) {
				return false;
			}

			bool b;

			b = polling();
			if(!b) {
				return false;
			}

			byte[] img = new byte[IMG_WIDTH * IMG_HEIGHT / 8];
			int pos = 0;
			for(int y = 0; y < IMG_HEIGHT; y++) {
				for(int x = 0; x < IMG_WIDTH; x += 8) {
					byte pix = 0;
					for(int bit = 7; bit >= 0; bit--) {
						//ここで白黒反転させよう
						if(bm.GetPixel(x + 7 - bit, y).R == 0) {
							pix |= (byte)(1 << bit);
						}
					}
					img[pos++] = pix;
				}
			}
					
			b = displayImage(img);
			mFNS.unpoll();
			return b;
		}

		/// <summary>
		/// 画像登録
		/// </summary>
		/// <param name="layout">登録するレイアウト番号</param>
		/// <returns>処理結果</returns>
		public bool regImage(int layout) {
			if(layout < LAYOUT_MIN || layout > LAYOUT_MAX) {
				return false;
			}

			bool b;

			b = polling();
			if(!b) {
				return false;
			}

			byte[] reg_img = new byte[8];
			reg_img[0] = (byte)layout;
			b = WriteCommand(REG_IMAGE, reg_img, null);
			mFNS.unpoll();
			return b;
		}
	}
}
