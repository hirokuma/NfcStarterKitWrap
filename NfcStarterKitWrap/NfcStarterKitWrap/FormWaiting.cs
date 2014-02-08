using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NfcStarterKitWrap {
	/// <summary>
	/// とりあえず「かざして」画面を出すことにした。
	/// キャンセルはできない。。。
	/// </summary>
	public partial class FormWaiting : nfc.ListenerWindow {
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormWaiting() {
			InitializeComponent();
		}
	}
}
