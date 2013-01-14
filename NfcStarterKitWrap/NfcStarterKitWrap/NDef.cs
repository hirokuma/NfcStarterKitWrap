using System;
using System.Collections.Generic;
using System.Text;

namespace NfcStarterKitWrap {

	/// <summary>
	/// NDEFメッセージ
	/// </summary>
	public class NdefMessage {
		private List<NdefRecord> mRecord = new List<NdefRecord>();

		/// <summary>
		/// NDEFレコード
		/// </summary>
		public List<NdefRecord> Record {
			get { return mRecord; }
		}

		/// <summary>
		/// NDEFレコード追加
		/// </summary>
		/// <param name="rec"></param>
		public void Add(NdefRecord rec) {
			mRecord.Add(rec);
		}

		/// <summary>
		/// NDEFメッセージ全体取得
		/// </summary>
		/// <returns></returns>
		public byte[] getMessage() {
			int len = 0;

			foreach(NdefRecord rec in mRecord) {
				len += rec.getLength();
			}

			byte[] msg = new byte[len];
			int pos = 0;
			foreach(NdefRecord rec in mRecord) {
				byte[] rec_byte = rec.getRecord();
				Buffer.BlockCopy(rec_byte, 0, msg, pos, rec_byte.Length);
				pos += rec_byte.Length;
			}

			return msg;
		}
	}

	/// <summary>
	/// NDEFレコード(Shortのみ)
	/// </summary>
	public class NdefRecord {
		
		/// <summary>
		/// TNF
		/// </summary>
		public enum TNF_TYPE {
			/// <summary>
			/// EMPTY
			/// </summary>
			EMPTY,
			/// <summary>
			/// Well-Known
			/// </summary>
			WKS,
			/// <summary>
			/// MIME
			/// </summary>
			MIME
		};

		/// <summary>
		/// Message Begin
		/// </summary>
		public bool MB {
			get { return ((mHead & 0x80) != 0) ? true : false; }
			set {
				if(value) {
					mHead |= 0x80;
				}
				else {
					mHead &= 0x7f;
				}
			}
		}

		/// <summary>
		/// Message End
		/// </summary>
		public bool ME {
			get { return ((mHead & 0x40) != 0) ? true : false; }
			set {
				if(value) {
					mHead |= 0x40;
				}
				else {
					mHead &= 0xbf;
				}
			}
		}

		/// <summary>
		/// Chunk Flag
		/// </summary>
		public bool CF { get { return ((mHead & 0x20) != 0) ? true : false; } }

		/// <summary>
		/// Short Record
		/// </summary>
		public bool SR { get { return ((mHead & 0x10) != 0) ? true : false; } }

		/// <summary>
		/// ID
		/// </summary>
		public byte[] ID {
			get { return mID; }
			set {
				mID = value;
				if((mID != null) && (mID.Length != 0)) {
					mHead |= 0x08;	//IL=1
				}
				else {
					mHead &= 0xf7;	//IL=0
				}
			}
		}

		/// <summary>
		/// Payload
		/// </summary>
		public byte[] Payload {
			get { return mPayload; }
			set { mPayload = value; }
		}

		private byte mHead = 0x10;	//SR=1
		private TNF_TYPE mTnf = TNF_TYPE.EMPTY;
		private byte[] mType = null;
		private byte[] mID = null;
		private byte[] mPayload = null;

		/// <summary>
		/// Type設定
		/// </summary>
		/// <param name="tnf">TNF</param>
		/// <param name="type">タイプデータ</param>
		public void setType(TNF_TYPE tnf, byte[] type) {
			mTnf = tnf;
			mType = type;

			mHead &= 0xf8;
			switch(mTnf) {
			case TNF_TYPE.WKS:
				mHead |= 0x01;
				break;
			case TNF_TYPE.MIME:
				mHead |= 0x02;
				break;
			}
		}

		/// <summary>
		/// NDEFレコードからNdefRecordを構築
		/// </summary>
		/// <param name="rec"></param>
		public void setRecord(byte[] rec) {
			mTnf = TNF_TYPE.EMPTY;
			mType = null;
			mID = null;
			mPayload = null;

			if((rec == null) || (rec.Length < 3)) {
				//足りない
				return;
			}
			if(((rec[0] & 0x20) == 0) || ((rec[0] & 0x10) == 0)) {
				//Chunkなし、Short Recordのみにする
				return;
			}
			if(((rec[0] & 0x08) != 0) && (rec.Length < 4)) {
				//IL=1なら4byteは必要
				return;
			}

			//長さチェック
			if((rec[0] & 0x08) != 0) {
				//IL=1
				if(4 + rec[1] + rec[2] + rec[3] > rec.Length) {
					//HEAD+TypeLen+PayloadLen+IDLen+Type+ID+Payloadが全長よりも長いことはない
					return;
				}
			}
			else {
				//IL=0
				if(3 + rec[1] + rec[2] > rec.Length) {
					//HEAD+TypeLen+PayloadLen+Type+Payloadが全長よりも長いことはない
					return;
				}
			}

			switch(rec[0] & 0x07) {
			case 0x01:
				mTnf = TNF_TYPE.WKS;
				break;
			case 0x02:
				mTnf = TNF_TYPE.MIME;
				break;
			default:
				//WKSかMIMEのみにする
				return;
			}

			mHead = rec[0];

			int pos = 1;

			//Type Length
			if(rec[pos] > 0) {
				mType = new byte[rec[1]];
			}
			pos++;

			//Payload Length
			if(rec[pos] > 0) {
				mPayload = new byte[rec[2]];
			}
			pos++;

			//ID Length(存在する場合)
			if((mHead & 0x08) != 0) {
				//IL=1
				if(rec[pos] > 0) {
					mID = new byte[rec[3]];
					mHead |= 0x80;
				}
				pos++;
			}

			if(mType.Length > 0) {
				Buffer.BlockCopy(rec, pos, mType, 0, mType.Length);
				pos += mType.Length;
			}
			if(mID.Length > 0) {
				Buffer.BlockCopy(rec, pos, mID, 0, mID.Length);
				pos += mID.Length;
			}
			if(mPayload.Length > 0) {
				Buffer.BlockCopy(rec, pos, mPayload, 0, mPayload.Length);
				pos += mPayload.Length;
			}
		}

		/// <summary>
		/// NDEFレコードバイナリデータ取得
		/// </summary>
		/// <returns></returns>
		public byte[] getRecord() {
			if(mTnf == TNF_TYPE.EMPTY) {
				mType = null;
				mPayload = null;
			}

			int len = 0;
			if(mType != null) {
				len += mType.Length;
			}
			if(mPayload != null) {
				len += mPayload.Length;
			}
			if((mID == null) || (mID.Length == 0)) {
				len += 3;	//HEAD, TypeLen, PayloadLen
			}
			else {
				len += mID.Length;
				len += 4;	//HEAD, TypeLen, PayloadLen, IDLen
			}

			byte[] rec = new byte[len];
			int pos = 0;
			rec[pos++] = mHead;
			rec[pos++] = (byte)((mType != null) ? mType.Length : 0);
			rec[pos++] = (byte)((mPayload != null) ? mPayload.Length : 0);
			if((mID != null) && (mID.Length > 0)) {
				rec[pos++] = (byte)mID.Length;
			}
			if(mType != null) {
				Buffer.BlockCopy(mType, 0, rec, pos, mType.Length);
				pos += mType.Length;
			}
			if((mID != null) && (mID.Length > 0)) {
				Buffer.BlockCopy(mID, 0, rec, pos, mID.Length);
				pos += mID.Length;
			}
			if(mPayload != null) {
				Buffer.BlockCopy(mPayload, 0, rec, pos, mPayload.Length);
				pos += mPayload.Length;
			}

			return rec;
		}

		/// <summary>
		/// NDEFレコード長取得
		/// </summary>
		/// <returns>NDEFレコード長</returns>
		public int getLength() {
			int len = 3;

			if(mType != null) {
				len += mType.Length;
			}
			if(mPayload != null) {
				len += mPayload.Length;
			}
			if((mID != null) && (mID.Length > 0)) {
				len += 1 + mID.Length;
			}

			return len;
		}
	}
}
