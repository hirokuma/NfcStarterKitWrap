using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NfcStarterKitWrap {

	/// <summary>
	/// felica_nfc_dll_wrapper_basicのラッパクラス
	/// SDK for NFC Starter KitのAPIを直接アクセスするのが面倒なので作った。
	/// </summary>
	public class nfc {

		//------------------------------------------------------------------------------//
		// 公開定義
		//------------------------------------------------------------------------------//

		/// <summary>
		/// NFC-Aをポーリングする。
		/// polling()の引数に使用する。
		/// </summary>
		public const UInt32 DEVICE_TYPE_NFCA = felica_nfc_dll_wrapper.DEVICE_TYPE_NFC_14443A_18092_106K;

		/// <summary>
		/// NFC-Bをポーリングする。
		/// polling()の引数に使用する。
		/// </summary>
		public const UInt32 DEVICE_TYPE_NFCB = felica_nfc_dll_wrapper.DEVICE_TYPE_NFC_14443B_106K;

		/// <summary>
		/// NFC-Fを212Kbpsでポーリングする。
		/// polling()の引数に使用する。
		/// </summary>
		public const UInt32 DEVICE_TYPE_NFCF_212K = felica_nfc_dll_wrapper.DEVICE_TYPE_NFC_18092_212K;

		/// <summary>
		/// NFC-Fを424Kbpsでポーリングする。
		/// polling()の引数に使用する。
		/// </summary>
		public const UInt32 DEVICE_TYPE_NFCF_424K = felica_nfc_dll_wrapper.DEVICE_TYPE_NFC_18092_424K;

		/// <summary>
		/// Mifareをポーリングする。
		/// polling()の引数に使用する。
		/// </summary>
		public const UInt32 DEVICE_TYPE_MIFARE = DEVICE_TYPE_NFCA;

		/// <summary>
		/// FeliCaを212Kbpsでポーリングする。
		/// polling()の引数に使用する。
		/// </summary>
		public const UInt32 DEVICE_TYPE_FELICA_212K = DEVICE_TYPE_NFCF_212K;

		/// <summary>
		/// FeliCaを424Kbpsでポーリングする。
		/// polling()の引数に使用する。
		/// </summary>
		public const UInt32 DEVICE_TYPE_FELICA_424K = DEVICE_TYPE_NFCF_424K;

		/// <summary>
		/// FeliCaをポーリングする。
		/// polling()の引数に使用する。
		/// </summary>
		public const UInt32 DEVICE_TYPE_FELICA = (UInt32)(DEVICE_TYPE_FELICA_212K | DEVICE_TYPE_FELICA_424K);

		/// <summary>
		/// [NFC-B]NFCID0の最大サイズ(単位：byte)。
		/// 4byte固定。
		/// </summary>
		public const int NFCID0_SIZE = 4;

		/// <summary>
		/// [NFC-A]NFCID1の最大サイズ(単位：byte)。
		/// simple:4byte / double:7byte / triple:10byte
		/// </summary>
		public const int NFCID1_SIZE = 10;

		/// <summary>
		/// [NFC-F]NFCID2のサイズ(単位：byte)。
		/// 8byte固定。
		/// </summary>
		public const int NFCID2_SIZE = 8;

		/// <summary>
		/// NFCIDの最大サイズ。
		/// </summary>
		public const int NFCID_SIZE_MAX = 10;

		/// <summary>
		/// メモリアクセス時の最大ブロックサイズ(単位：byte)
		/// </summary>
		public const int BLOCK_SIZE = 16;

		/// <summary>
		/// [NFC-A]１回で読み込むことができるサイズ(単位：byte)
		/// </summary>
		public const int READABLE_SIZE_NFCA = 16;

		/// <summary>
		/// [NFC-A]１回で書き込むことができるサイズ(単位：byte)
		/// </summary>
		public const int WRITABLE_SIZE_NFCA = 4;

		/// <summary>
		/// [NFC-F]１回で読み込むことができるサイズ(単位：byte)
		/// </summary>
		public const int READABLE_SIZE_NFCF = 16;

		/// <summary>
		/// [NFC-F]１回で書き込むことができるサイズ(単位：byte)
		/// </summary>
		public const int WRITABLE_SIZE_NFCF = 16;

		/// <summary>
		/// [NFC-F]サービス：ReadOnly
		/// </summary>
		public readonly byte[] SERVICE_READONLY_NFCF = new byte[2] { 0x0b, 0x00 };

		/// <summary>
		/// [NFC-F]サービス：Read/Write
		/// </summary>
		public readonly byte[] SERVICE_READWRITE_NFCF = new byte[2] { 0x09, 0x00 };

		/// <summary>
		/// [NFC-A]RD配列のSEL_RES要素番号。
		/// RD[RD_SELRES]のように使う。
		/// </summary>
		public const int RD_SELRES = 0;

		/// <summary>
		/// [NFC-F]RD配列のシステムコード(1byte目)要素番号。
		/// RD[RD_SYSCODE1]のように使う。
		/// </summary>
		public const int RD_SYSCODE1 = 0;

		/// <summary>
		/// [NFC-F]RD配列のシステムコード(2byte目)要素番号。
		/// RD[RD_SYSCODE2]のように使う。
		/// </summary>
		public const int RD_SYSCODE2 = 1;


		//------------------------------------------------------------------------------//
		// 外部
		//------------------------------------------------------------------------------//

		[DllImport("User32.dll")]
		extern static UInt32 RegisterWindowMessage(String lpString);


		//------------------------------------------------------------------------------//
		// 非公開定義
		//------------------------------------------------------------------------------//

		// SDK for NFC Starter KitのC#ラッパ
		private felica_nfc_dll_wrapper mFeliCaNfcDllWrapperClass = null;

		// WIN32 API用
		// メッセージループを回したいので使っている。
		private String kMsgFind = "find";
		private String kMsgEnable = "enable";
		private static ListenerWindow mListener = new ListenerWindow();
		private MessageHandler mMessageHandler = null;

		private byte[] mNfcId = null;	// NFCID
		private byte[] mRD = null;		// Request Data


		//------------------------------------------------------------------------------//
		// メソッド
		//------------------------------------------------------------------------------//

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public nfc() {
			;
		}

		//------------------------------------------------------------------------------//
		// プロパティ
		//------------------------------------------------------------------------------//

		/// <summary>
		/// felica_nfc_dll_wrapperを取得する。
		/// 直接SDK for NFC Starter Kitを制御したい場合に用いる。
		/// </summary>
		internal felica_nfc_dll_wrapper felica_nfc {
			get { return mFeliCaNfcDllWrapperClass; }
		}

		/// <summary>
		/// 取得した最新のNFCID。IDmなど。
		/// nullもあり得る。
		/// polling()成功時に更新。
		/// unpoll()したときにしか消さない。
		/// </summary>
		public byte[] NfcId {
			get { return mNfcId; }
		}

		/// <summary>
		/// 取得したRequest Data(RD)。
		/// MifareならSEL_RES。
		/// FeliCaならシステムコード。
		/// </summary>
		public byte[] RD {
			get { return mRD; }
		}


		//------------------------------------------------------------------------------//
		// 基本処理
		//------------------------------------------------------------------------------//

		/// <summary>
		/// 初期化。
		/// 最初に必ず呼び出すこと。
		/// </summary>
		/// <returns>初期化処理結果</returns>
		public bool init() {
			bool bRet = false;

			if(mFeliCaNfcDllWrapperClass != null) {
				//初期化済み。
				return true;
			}

			// SDK for NFC Starter Kitのサンプル提供C#ラッパ
			mFeliCaNfcDllWrapperClass = new felica_nfc_dll_wrapper();

			// Win32メッセージ登録
#if true
			bRet = mListener.init(kMsgFind, kMsgEnable, ref mMessageHandler);
			if(!bRet) {
				return false;
			}
#else
			UInt32 card_find_message = RegisterWindowMessage(kMsgFind);
			if(card_find_message == 0) {
				Console.Write("Failed: RegisterWindowMessage\n");
				return false;
			}

			UInt32 card_enable_message = RegisterWindowMessage(kMsgEnable);
			if(card_enable_message == 0) {
				Console.Write("Failed: RegisterWindowMessage\n");
				return false;
			}

			bRet = mListener.WatchMessage(card_find_message);
			if(bRet == false) {
				Console.Write("Failed: WatchMessage\n");
				return false;
			}

			bRet = mListener.WatchMessage(card_enable_message);
			if(bRet == false) {
				Console.Write("Failed: WatchMessage\n");
				return false;
			}

			mMessageHandler
				= new MessageHandler(card_find_message, card_enable_message);

			mListener.handler
				+= new MessageReceivedEventHandler(mMessageHandler.messageHandlerFunc);
#endif

			// SDK for NFC Starter Kit初期化
			bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcInitialize();
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcInitialize\n");
				ErrorRoutine();
				return false;
			}

			// R/Wオープン
			StringBuilder port_name = new StringBuilder("USB0");
			bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcOpen(port_name);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcOpen\n");
				ErrorRoutine();
				return false;
			}

			return true;
		}


		/// <summary>
		/// 終了処理。
		/// init()後は、アプリ終了時までに呼び出すこと
		/// </summary>
		public void term() {
			unpoll();

			bool bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcClose();
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcClose\n");
				ErrorRoutine();
			}

			bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcUninitialize();
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcUninitialize\n");
				ErrorRoutine();
			}

			mListener.Close();
		}


		/// <summary>
		/// タイムアウト時間の設定
		/// </summary>
		/// <param name="time">タイムアウト時間(単位：ミリ秒)</param>
		/// <returns>処理結果</returns>
		public bool setTimeout(uint time) {
			return mFeliCaNfcDllWrapperClass.FeliCaLibNfcSetTimeout(time);
		}
	
		/// <summary>
		/// 指定されたpolling実効。
		/// 成功した場合、NFCIDとRDが更新される。
		/// 失敗した場合、NFCIDとRDはnullになる。
		/// </summary>
		/// <param name="target_device">DEVICE_TYPE_XXX</param>
		/// <returns>ポーリング結果</returns>
		public bool polling(UInt32 target_device) {
			unpoll();

			bool bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcSetPollCallbackParameters(
				mListener.Handle,
				kMsgFind,
				kMsgEnable);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcSetPollCallbackParameters\n");
				return false;
			}

			bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcStartPollMode(target_device);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcStartPollMode\n");
				return false;
			}

			//メッセージループを回すためにこうしているが、なんとかならんか。
			//まあ、なんとかしたかったら非同期にするしかないんだろうがね。
			mListener.Visible = false;
			mListener.ShowDialog();

			bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcStopPollMode();
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcStopPollMode\n");
				ErrorRoutine();
			}

			if(mMessageHandler.Result == false) {
				return false;
			}
			if(mMessageHandler.NfcId == null) {
				return false;
			}
			if((mMessageHandler.TargetDevice & target_device) == 0) {
				return false;
			}

			mRD = (byte[])mMessageHandler.RD.Clone();
			mNfcId = (byte[])mMessageHandler.NfcId.Clone();

			return true;
		}

		/// <summary>
		/// polling(NFC-A用)
		/// </summary>
		/// <returns>処理結果</returns>
		public bool pollingA() {
			return polling(DEVICE_TYPE_MIFARE);
		}

		/// <summary>
		/// polling(NFC-F用)
		/// </summary>
		/// <returns>処理結果</returns>
		public bool pollingF() {
			return polling(DEVICE_TYPE_FELICA);
		}

		/// <summary>
		/// polling後、カードを使い終わったら呼び出す。
		/// 呼び出さなかった場合、次にpollingしたときの動作が遅くなる。
		/// NFCIDとRDをnullにする。
		/// </summary>
		public void unpoll() {
			mNfcId = null;
			mRD = null;

			UInt32 stop_mode = felica_nfc_dll_wrapper.RE_NOTIFICATION_SAME_DEVICE;
			bool bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcStopDevAccess(stop_mode);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcStopDevAccess\n");
				ErrorRoutine();
				return;
			}

			bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcStopPollMode();
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcStopPollMode\n");
				ErrorRoutine();
				return;
			}
		}


		/// <summary>
		/// SDK for NFC Starter Kitが返すエラー。
		/// コンソールに出すようになっているが、どうしたもんだか。
		/// </summary>
		private void ErrorRoutine() {
			UInt32[] error_info = new UInt32[2] { 0, 0 };
			mFeliCaNfcDllWrapperClass.FeliCaLibNfcGetLastError(error_info);
			Console.Write("error_info[0]: 0x{0:X8}\nerror_info[1]: 0x{1:X8}\n", error_info[0], error_info[1]);

			//mFeliCaNfcDllWrapperClass.FeliCaLibNfcClose();
			//mFeliCaNfcDllWrapperClass.FeliCaLibNfcUninitialize();
		}


		//------------------------------------------------------------------------------//
		// [NFC-A]カードアクセス
		//------------------------------------------------------------------------------//

		/// <summary>
		/// 読み込み(NFC-A用)
		/// 16byte読み込む。
		/// </summary>
		/// <param name="buf">読み込み結果。newして返すのでメモリを確保する必要はない。</param>
		/// <param name="block">読み込むブロック番号(値チェックなし)</param>
		/// <returns>処理結果</returns>
		public bool NfcA_Read(ref byte[] buf, byte block) {
			byte[] cmd = new byte[2] { 0x30, block };
			UInt16 cmd_len = (UInt16)cmd.Length;
			byte[] res = new byte[256];
			UInt16 res_len = 0x00;
			bool bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcThru\n");
				ErrorRoutine();
				return false;
			}

			if(res_len != READABLE_SIZE_NFCA) {
				return false;
			}
			buf = new byte[res_len];
			Buffer.BlockCopy(res, 0, buf, 0, res_len);

			return true;
		}

		/// <summary>
		/// 書き込み(NFC-A用)
		/// 4byte書き込む。
		/// </summary>
		/// <param name="buf">書き込みデータ</param>
		/// <param name="block">書き込み先ブロック(値チェックなし)</param>
		/// <returns>処理結果</returns>
		public bool NfcA_Write(byte[] buf, byte block) {
			if(buf.Length < WRITABLE_SIZE_NFCA) {
				// サイズが足りない
				return false;
			}

			byte[] cmd = new byte[2 + WRITABLE_SIZE_NFCA];
			cmd[0] = 0xa2;
			cmd[1] = block;
			Buffer.BlockCopy(buf, 0, cmd, 2, WRITABLE_SIZE_NFCA);
			UInt16 cmd_len = (UInt16)cmd.Length;
			byte[] res = new byte[256];
			UInt16 res_len = 0x00;
			bool bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcThru\n");
				ErrorRoutine();
				return false;
			}

			if(res_len != 0) {
				return false;
			}

			return true;
		}

		//------------------------------------------------------------------------------//
		// [NFC-F]カードアクセス
		//------------------------------------------------------------------------------//

		/// <summary>
		/// 読み込み(NFC-F用)
		/// 面倒なので、サービスは１種類にしている。
		/// </summary>
		/// <param name="buf">受信バッファ。関数内でnewして返すので、メモリは確保しなくてよい。</param>
		/// <param name="block">読み込み開始ブロック</param>
		/// <param name="block_num">読み込みブロック数。blockよりも小さい場合は、blockの先頭からblock_numだけ読み込む。</param>
		/// <param name="svc">サービス。ServiceReadOnlyNfcFかServiceReadWriteNfcFがよいだろう。</param>
		/// <returns>処理結果</returns>
		public bool NfcF_Read(ref byte[] buf, UInt16[] block, byte block_num, byte[] svc) {
			if(block.Length < block_num) {
				//あっとらんよ
				return false;
			}
			byte[] cmd = new byte[1 + 1 + 8 + 1 + 2 + 1 + block_num * 2];
			cmd[0] = (byte)cmd.Length;		//LEN
			cmd[1]  = 0x06;
			Buffer.BlockCopy(mNfcId, 0, cmd, 2, 8);	//IDm
			cmd[10]  = 0x01;		//services
			cmd[11] = svc[0];
			cmd[12] = svc[1];
			cmd[13] = block_num;
			for(int i=0; i<block_num; i++) {
				UInt16 bl = CreateBlockList2(block[i], 0);
				cmd[14 + i*2] = (byte)(bl >> 8);		//BE
				cmd[15 + i*2] = (byte)(bl & 0x00ff);	//BE
			}
			UInt16 cmd_len = cmd[0];
			byte[] res = new byte[256];
			UInt16 res_len = 0x00;
			bool bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcThru\n");
				ErrorRoutine();
				return false;
			}

			int offset = 1+1+8;
			if(res[offset] != 0x00) {
				return false;
			}
			offset += 2;	//read blocks
			buf = new byte[res[offset] * 16];
			offset++;
			Buffer.BlockCopy(res, offset, buf, 0, buf.Length);

			return true;
		}

		/// <summary>
		/// 書き込み(NFC-F用)
		/// 面倒なので、サービスは１種類にしている。
		/// </summary>
		/// <param name="buf">書き込みデータ</param>
		/// <param name="block">書き込み開始ブロック</param>
		/// <param name="block_num">書き込みブロック数。blockよりも小さい場合は、blockの先頭からblock_numだけ書き込む。</param>
		/// <param name="svc">サービス。ServiceReadWriteNfcFがよいだろう。</param>
		/// <returns>処理結果</returns>
		public bool NfcF_Write(byte[] buf, UInt16[] block, byte block_num, byte[] svc) {
			if(block.Length < block_num) {
				//あっとらんよ
				return false;
			}
			byte[] cmd = new byte[1 + 1 + 8 + 1 + 2 + 1 + 2 + nfc.BLOCK_SIZE * block_num];
			cmd[0] = (byte)cmd.Length;		//LEN
			cmd[1] = 0x08;
			Buffer.BlockCopy(mNfcId, 0, cmd, 2, 8);	//IDm
			cmd[10] = 0x01;		//services
			cmd[11] = svc[0];
			cmd[12] = svc[1];
			cmd[13] = block_num;
			for(int i = 0; i < block_num; i++) {
				UInt16 bl = CreateBlockList2(block[i], 0);
				cmd[14 + i * 2] = (byte)(bl >> 8);		//BE
				cmd[15 + i * 2] = (byte)(bl & 0x00ff);	//BE
			}
			Buffer.BlockCopy(buf, 0, cmd, 14 + block_num * 2, nfc.BLOCK_SIZE * block_num);
			UInt16 cmd_len = cmd[0];
			byte[] res = new byte[256];
			UInt16 res_len = 0x00;
			bool bRet = mFeliCaNfcDllWrapperClass.FeliCaLibNfcThru(
								cmd,
								cmd_len,
								res,
								ref res_len);
			if(bRet == false) {
				Console.Write("Failed: FeliCaLibNfcThru\n");
				ErrorRoutine();
				return false;
			}

			int offset = 1 + 1 + 8;
			if(res[offset] != 0x00) {
				return false;
			}

			return true;
		}

		/// <summary>
		/// [NFC-F]2バイトブロックリストを作る
		/// </summary>
		/// <param name="block">ブロック番号</param>
		/// <param name="svc_num">サービス</param>
		/// <returns>2バイトブロックリスト値</returns>
		private UInt16 CreateBlockList2(UInt16 block, byte svc_num) {
			const UInt16 BM_LEN2 = 0x8000;
			const UInt16 AM_NORMAL = 0x0000;
			return (UInt16)(BM_LEN2 | AM_NORMAL | (svc_num << 8) | block);
		}


		//------------------------------------------------------------------------------//
		// Win32メッセージループ関係
		//------------------------------------------------------------------------------//

		internal delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

		/// <summary>
		/// 使うな
		/// </summary>
		internal class MessageReceivedEventArgs : EventArgs {
			private readonly Message _message;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="message"></param>
			public MessageReceivedEventArgs(Message message) {
				_message = message;
			}

			/// <summary>
			/// 
			/// </summary>
			public Message Message {
				get { return _message; }
			}
		}

		/// <summary>
		/// メッセージを受け取るウィンドウ
		/// </summary>
		internal class ListenerWindow : Form {
			private const Int32 MAX_MESSAGES = 2;
			public event MessageReceivedEventHandler handler;
			private UInt32[] messageSet = new UInt32[MAX_MESSAGES];
			private Int32 registeredMessage = 0;

			/// <summary>
			/// 内部用
			/// </summary>
			public ListenerWindow() {
				this.Visible = false;
			}
			
			/// <summary>
			/// 初期化
			/// </summary>
			/// <param name="msgFind">Windowメッセージ文字列(カード検出)</param>
			/// <param name="msgEnable">Windowメッセージ文字列(カード使用可能)</param>
			/// <param name="msgHandler">メッセージ処理ハンドラ</param>
			/// <returns></returns>
			public bool init(String msgFind, String msgEnable, ref MessageHandler msgHandler) {

				// Win32メッセージ登録
				UInt32 card_find_message = RegisterWindowMessage(msgFind);
				if(card_find_message == 0) {
					Console.Write("Failed: RegisterWindowMessage\n");
					return false;
				}

				UInt32 card_enable_message = RegisterWindowMessage(msgEnable);
				if(card_enable_message == 0) {
					Console.Write("Failed: RegisterWindowMessage\n");
					return false;
				}

				messageSet[0] = card_find_message;
				messageSet[1] = card_enable_message;

				msgHandler =
					new MessageHandler(card_find_message, card_enable_message);
				
				
				handler +=
					new MessageReceivedEventHandler(msgHandler.messageHandlerFunc);

				return true;
			}

			public bool WatchMessage(UInt32 message) {
				if(registeredMessage < messageSet.Length) {
					messageSet[registeredMessage] = message;
					registeredMessage++;
					return true;
				}
				else {
					return false;
				}
			}


			/// <summary>
			/// Window属性
			/// </summary>
			protected override CreateParams CreateParams {
				get {
					const Int32 WS_EX_TOOLWINDOW = 0x80;
					const Int64 WS_POPUP = 0x80000000;
					const Int32 WS_VISIBLE = 0x10000000;
					const Int32 WS_SYSMENU = 0x80000;
					const Int32 WS_MAXIMIZEBOX = 0x10000;

					CreateParams cp = base.CreateParams;
					cp.ExStyle = WS_EX_TOOLWINDOW;
					cp.Style = unchecked((Int32)WS_POPUP) |
						WS_VISIBLE | WS_SYSMENU | WS_MAXIMIZEBOX;
					cp.Width = 0;
					cp.Height = 0;

					return cp;
				}
			}

			/// <summary>
			/// Windowメッセージ処理
			/// </summary>
			/// <param name="m"></param>
			protected override void WndProc(ref Message m) {
				bool handleMessage = false;
				for(Int32 i = 0; i < MAX_MESSAGES; i++) {
					if(messageSet[i] == m.Msg) {
						handleMessage = true;
					}
				}

				if(handleMessage && handler != null) {
					handler(null, new MessageReceivedEventArgs(m));
				}
				base.WndProc(ref m);
				return;
			}
		}


		////////////////////////////////////////////////////////////////////////////


		/// <summary>
		/// メッセージハンドラクラス
		/// </summary>
		internal class MessageHandler {
			private bool mResult;

			private UInt32 mTargetNumber;
			private UInt32 mCardFindMessage;
			private UInt32 mCardEnableMessage;
			private static felica_nfc_dll_wrapper mFeliCaNfcDllWrapperClass =
				new felica_nfc_dll_wrapper();

			private byte[] mNfcId = null;
			private UInt32 mTargetDevice = 0;
			private byte[] mRD = null;


			/// <summary>
			/// ポーリング結果
			/// </summary>
			public bool Result {
				get { return mResult; }
			}


			/// <summary>
			/// NFCID
			/// </summary>
			public byte[] NfcId {
				get { return mNfcId; }
			}

			/// <summary>
			/// Request Data
			/// </summary>
			public byte[] RD {
				get { return mRD; }
			}

			/// <summary>
			/// ポーリングで検出したターゲット。
			/// </summary>
			public UInt32 TargetDevice {
				get { return mTargetDevice; }
			}

			/// <summary>
			/// コンストラクタ。
			/// </summary>
			/// <param name="findMsg"></param>
			/// <param name="enableMsg"></param>
			public MessageHandler(UInt32 findMsg, UInt32 enableMsg) {
				mCardFindMessage = findMsg;
				mCardEnableMessage = enableMsg;
			}

			/// <summary>
			/// メッセージハンドラ本体
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			public void messageHandlerFunc(object sender, MessageReceivedEventArgs e) {
				mResult = false;
				if(e.Message.Msg == mCardFindMessage) {
					IntPtr pDevInfo = e.Message.LParam;
					IntPtr pDeviceData;

					mNfcId = null;
					mRD = null;

					// OSの32bit/64bit判別
					DEVICE_INFO dev_info = (DEVICE_INFO)Marshal.PtrToStructure(pDevInfo, typeof(DEVICE_INFO));
					mTargetDevice = dev_info.target_device;
					if(IntPtr.Size == 8) {
						pDeviceData = (IntPtr)((Int64)pDevInfo
							+ (Int64)Marshal.OffsetOf(typeof(DEVICE_INFO), "dev_info"));
					}
					else {
						pDeviceData = (IntPtr)((Int32)pDevInfo
							+ (Int32)Marshal.OffsetOf(typeof(DEVICE_INFO), "dev_info"));
					}

					//解析
					switch(mTargetDevice) {
					case felica_nfc_dll_wrapper.DEVICE_TYPE_NFC_14443A_18092_106K:
						detectNfcA(pDeviceData);
						break;

					case felica_nfc_dll_wrapper.DEVICE_TYPE_NFC_18092_212K:
					case felica_nfc_dll_wrapper.DEVICE_TYPE_NFC_18092_424K:
						detectNfcF(pDeviceData);
						break;

					default:
						break;
					}
				}
				else if(e.Message.Msg == mCardEnableMessage) {
					mResult = true;
					mListener.Close();
					return;
				}

				//mListenerWindow.Close();
				return;
			}

			/// <summary>
			/// ポーリング結果がNFC-Aだった場合
			/// </summary>
			/// <param name="pDeviceData">DEVICE_INFO.dev_infoへのポインタ</param>
			private void detectNfcA(IntPtr pDeviceData) {
				DEVICE_DATA_NFC_14443A_18092_106K DeviceData_A =
					(DEVICE_DATA_NFC_14443A_18092_106K)Marshal.PtrToStructure(pDeviceData,
												typeof(DEVICE_DATA_NFC_14443A_18092_106K));

				mTargetNumber = DeviceData_A.target_number;
				mResult = mFeliCaNfcDllWrapperClass.FeliCaLibNfcStartDevAccess(mTargetNumber);
				if(mResult == false) {
					ErrorRoutine();
					return;
				}
				mNfcId = new byte[DeviceData_A.NFCID1_size];
				Buffer.BlockCopy(DeviceData_A.NFCID1, 0, mNfcId, 0, DeviceData_A.NFCID1_size);
				mRD = new byte[1];
				mRD[RD_SELRES] = DeviceData_A.sel_res;
			}

			/// <summary>
			/// ポーリング結果がNFC-Fだった場合
			/// </summary>
			private void detectNfcF(IntPtr pDeviceData) {
				DEVICE_DATA_NFC_18092_212_424K DeviceData_F =
					(DEVICE_DATA_NFC_18092_212_424K)Marshal.PtrToStructure(pDeviceData,
												typeof(DEVICE_DATA_NFC_18092_212_424K));

				mTargetNumber = DeviceData_F.target_number;
				mResult = mFeliCaNfcDllWrapperClass.FeliCaLibNfcStartDevAccess(mTargetNumber);
				if(mResult == false) {
					ErrorRoutine();
					return;
				}
				mNfcId = new byte[NFCID2_SIZE];
				Buffer.BlockCopy(DeviceData_F.NFCID2, 0, mNfcId, 0, NFCID2_SIZE);
				mRD = new byte[2];
				mRD[RD_SYSCODE1] = DeviceData_F.RD[0];
				mRD[RD_SYSCODE2] = DeviceData_F.RD[1];
			}

			/// <summary>
			/// SDK for NFC Starter Kitが返すエラー。
			/// コンソールに出すようになっているが、どうしたもんだか。
			/// </summary>
			private void ErrorRoutine() {
				UInt32[] error_info = new UInt32[2] { 0, 0 };
				mFeliCaNfcDllWrapperClass.FeliCaLibNfcGetLastError(error_info);
				Console.Write("error_info[0]: 0x{0:X8}\nerror_info[1]: 0x{1:X8}\n", error_info[0], error_info[1]);

				//mFeliCaNfcDllWrapperClass.FeliCaLibNfcClose();
				//mFeliCaNfcDllWrapperClass.FeliCaLibNfcUninitialize();
			}
		}
	}
}
