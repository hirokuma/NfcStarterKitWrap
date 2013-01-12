/*
 * felica_nfc_dll_wrapper_basic.cs
 * Copyright 2009,2011 Sony Corporation
 */
using System;
using System.Text;
using System.Runtime.InteropServices;

// structs

/// <summary>
/// DEVICE_DATA_NFC_14443A_18092_106K
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct DEVICE_DATA_NFC_14443A_18092_106K
{
	/// <summary>
	/// ターゲット数
	/// </summary>
    public UInt32 target_number;
	/// <summary>
	/// SENS_RES
	/// </summary>
    public UInt16 sens_res;
	/// <summary>
	/// SEL_RES
	/// </summary>
    public byte sel_res;
	/// <summary>
	/// NFCID1サイズ
	/// </summary>
    public byte NFCID1_size;
	/// <summary>
	/// NFCID1
	/// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public byte[] NFCID1;
	/// <summary>
	/// ATSサイズ
	/// </summary>
    public byte ATS_size;
	/// <summary>
	/// ATS
	/// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public byte[] ATS;
}

/// <summary>
/// DEVICE_DATA_NFC_14443B_106K
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct DEVICE_DATA_NFC_14443B_106K
{
	/// <summary>
	/// ターゲット数
	/// </summary>
    public UInt32 target_number;
	/// <summary>
	/// ATQB
	/// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
    public byte[] ATQB;
	/// <summary>
	/// ATTRIBサイズ
	/// </summary>
    public byte ATTRIB_size;
	/// <summary>
	/// ATTRIB
	/// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public byte[] ATTRIB;
}

/// <summary>
/// DEVICE_DATA_NFC_18092_212_424K
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct DEVICE_DATA_NFC_18092_212_424K
{
	/// <summary>
	/// ID?
	/// </summary>
    public byte id;
	/// <summary>
	/// ターゲット数
	/// </summary>
    public UInt32 target_number;
	/// <summary>
	/// NFCID2(IDm)
	/// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] NFCID2;
	/// <summary>
	/// PAD(PMm?)
	/// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] Pad;
	/// <summary>
	/// RDサイズ
	/// </summary>
    public byte RD_size;
	/// <summary>
	/// RD
	/// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte[] RD;
}

/// <summary>
/// DEVICE_INFO
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct DEVICE_INFO
{
	/// <summary>
	/// ターゲット数
	/// </summary>
    public UInt32 target_device;
	/// <summary>
	/// デバイス情報
	/// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public byte[] dev_info;
}
class felica_nfc_dll_wrapper
{
    //DllImport
	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_initialize();

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_uninitialize();

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_open(StringBuilder port_name);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_close();

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_start_poll_mode(UInt32 target_device);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_stop_poll_mode();

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_start_dev_access(UInt32 target_number);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_stop_dev_access(UInt32 stop_mode);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_select_device();

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_deselect_device();

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_thru(
            byte[] command_packet_data,
            UInt16 command_packet_data_length,
            byte[] response_packet_data,
        ref UInt16 response_packet_data_length);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_set_timeout(UInt32 timeout);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_get_timeout(ref UInt32 timeout);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_set_poll_callback_parameters(
        IntPtr handle,
        String msg_str_of_find,
        String msg_str_of_enable);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_set_pnp_callback_parameters(
        IntPtr handle,
        String msg_str_of_find,
        String msg_str_of_loss);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_start_plug_and_play();

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_stop_plug_and_play();

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_get_last_error(UInt32[] error_info);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_start_logging(String filename);

	[DllImport("felica_nfc_library.dll", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool felicalib_nfc_stop_logging();


	/* felicalib_nfc_start_poll_mode() target_device */
	public const uint DEVICE_TYPE_NFC_14443A_18092_106K = 0x00000001;
	public const uint DEVICE_TYPE_NFC_18092_212K = 0x00000002;
	public const uint DEVICE_TYPE_NFC_18092_424K = 0x00000004;
	public const uint DEVICE_TYPE_NFC_14443B_106K = 0x00000008;
	public const uint DEVICE_TYPE_NFC_JEWEL_106K = 0x00000010;

	/* felicalib_nfc_stop_dev_access() stop_mode */
	public const uint RE_NOTIFICATION_SAME_DEVICE = 0x00000000;
	public const uint NOT_RE_NOTIFICATION_SAME_DEVICE = 0x00000001;

	/* sel_res */
	public const byte SELRES_ULTRALIGHT = 0x00;
	public const byte SELRES_CLASSIC_1K = 0x08;
	public const byte SELRES_CLASSIC_MINI = 0x09;
	public const byte SELRES_CLASSIC_4K = 0x18;
	public const byte SELRES_DESFIRE = 0x20;
	public const byte SELRES_JCOP30 = 0x28;
	public const byte SELRES_GEMPLUS = 0x98;

    //Wrapper functions
    public bool FeliCaLibNfcInitialize(){
		return felicalib_nfc_initialize();
	}

    public bool FeliCaLibNfcUninitialize(){
        return felicalib_nfc_uninitialize();
    }

    public bool FeliCaLibNfcOpen(StringBuilder port_name){
        return felicalib_nfc_open(port_name);
    }

    public bool FeliCaLibNfcClose(){
        return felicalib_nfc_close();
    }

    public bool FeliCaLibNfcStartPollMode(
        UInt32 target_device){
        return felicalib_nfc_start_poll_mode(
            target_device);
    }

    public bool FeliCaLibNfcStopPollMode(){
        return felicalib_nfc_stop_poll_mode();
    }

    public bool FeliCaLibNfcStartDevAccess
        (UInt32 target_number){
        return felicalib_nfc_start_dev_access(
            target_number);
    }

    public bool FeliCaLibNfcStopDevAccess(
        UInt32 stop_mode){
        return felicalib_nfc_stop_dev_access(
            stop_mode);
    }

    public bool FeliCaLibNfcSelectDevice(){
        return felicalib_nfc_select_device();
    }

    public bool FeliCaLibNfcDeselectDevice(){
        return felicalib_nfc_deselect_device();
    }

    public bool FeliCaLibNfcThru(
            byte[] command_packet_data,
            UInt16 command_packet_data_length,
            byte[] response_packet_data,
        ref UInt16 response_packet_data_length){
        return felicalib_nfc_thru(
                command_packet_data,
                command_packet_data_length,
                response_packet_data,
            ref response_packet_data_length);
    }

    public bool FeliCaLibNfcSetTimeout(
        UInt32 timeout){
        return felicalib_nfc_set_timeout(
            timeout);
    }

    public bool FeliCaLibNfcGetTimeout(
        ref UInt32 timeout){
        return felicalib_nfc_get_timeout(
            ref timeout);
    }

    public bool FeliCaLibNfcSetPollCallbackParameters(
        IntPtr handle,
        String msg_str_of_find,
        String msg_str_of_enable){
        return felicalib_nfc_set_poll_callback_parameters(
            handle,
            msg_str_of_find,
            msg_str_of_enable);
    }

    public bool FeliCaLibNfcSetPnpCallbackParameters(
        IntPtr handle,
        String msg_str_of_find,
        String msg_str_of_loss){
        return felicalib_nfc_set_pnp_callback_parameters(
            handle,
            msg_str_of_find,
            msg_str_of_loss);
    }

    public bool FeliCaLibNfcStartPlugAndPlay(){
        return felicalib_nfc_start_plug_and_play();
    }

    public bool FeliCaLibNfcStopPlugAndPlay(){
        return felicalib_nfc_stop_plug_and_play();
    }

    public bool FeliCaLibNfcGetLastError(
        UInt32[] error_info){
        return felicalib_nfc_get_last_error(
            error_info);
    }

    public bool FeliCaLibNfcStartLogging(
        String filename){
        return felicalib_nfc_start_logging(
            filename);
    }

    public bool FeliCaLibNfcStopLogging(){
        return felicalib_nfc_stop_logging();
    }
}