using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace CMUtils
{
    public class RSCommand
    {
        bool connected = false;
        Utils utils = new Utils();

        [DllImport("oemr_usb.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_OpenUsbConnection")]
        static extern bool RSConnect(int lpOUIO);

        [DllImport("oemr_usb.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_CloseUsbConnection")]
        static extern void RSDisconnect();

        [DllImport("oemr_usb.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_SendUsbCommand")]
        static extern bool RSSingleCommand(byte[] command, int nByte, int timeout);

        [DllImport("oemr_usb.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_ReadUsbDataStream")]
        static extern bool RSReadDataStream(byte endpoint, byte[] receive, int nByte, int timeout);

        [DllImport("oemr_usb.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_ReadUsbDataPage")]
        static extern bool RSReadDataPage(byte page, byte[] buffer);

        [DllImport("oemr_usb.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_ReadUsbPattern")]
        static extern bool RSReadPattern(byte pattern, byte buffNum, byte[] buffer);

        public string Preset()
        {
            return Command("89",1,100);
        }

        public string Unlock()
        {
            return Command("D37B4657554E4C4F434B", 1, 100);
        }

        public string Download(string data)
        {
            return Command("93193030303030303030" + utils.AsciiToHex(data), 1, 5000);
        }

        public string UsbTest(string text = "123456 prova")
        {
            string test = utils.AsciiToHex(text);
            if (Command("20", 12) == test)
                return "PASS";
            return "FAIL";
        }
        public string Reboot()
        {
            return Command("D37D", 1, 100);
        }

        public string GetStatus()
        {
            return Command("F3", 1, 100);
        }
        public string GetDspStatus()
        {
            return Command("D338", 1, 100);
        }
        public string GetMagStatus()
        {
            return Command("D37E54", 1, 100);
        }
        public string GetTapeStatus()
        {
            return Command("D37C54", 1, 100);
        }
        public string Command(string command, int nByte, int timeout=1000, bool isDMA=false)
        {
            string result = "";
            byte[] receive = new byte[nByte];
            byte[] cmd = new byte[command.Length/2];
            byte endpoint = 0;
            if (!connected)
                Connect();
            if (!connected)
                return "FAIL";
            if (isDMA) endpoint = 2;
            for (int i = 0; i < command.Length; i+=2)
            {
                cmd[i/2] = byte.Parse(command.Substring(i, 2), System.Globalization.NumberStyles.HexNumber) ;
            }
            bool reply =  RSSingleCommand(cmd, cmd.Length, timeout);
            reply = RSReadDataStream(endpoint, receive, nByte, timeout);
            for (int i = 0; i<receive.Length;i++)
            {
                result += receive[i].ToString("X");
            }
            if (result.Length == 1)
                result = "0" + result;
            return result;
        }
        public bool Disconnect()
        {
            RSDisconnect();
            return false;
        }
        public bool Connect()
        {
            connected = RSConnect(0);
            return connected;
        }
    }
    public class CMCommand
    {
        Utils utils = new Utils();
        #region VARIABLES
        short hCon;
        public string comScopeSend, comScopeReceive;
        public string[] singleCommandAnswer;
        #endregion

        #region CONSTANTS
        //CONSTANTS
        public const int PROT_TRASPARENT_OFF = 0;
        public const int PROT_TRASPARENT_ON = 1;
        public const int COMM_TIMER_SECOND5 = 5000;
        public const int QUIT_THREAD = 2;
        public const int THREAD_MESSAGES = 3;
        public const int OPEN_POPUP_MENU = 4;
        public const int PROT_TRANSPARENT_OFF = 0;
        public const int PROT_TRANSPARENT_ON = 1;
        public const int PROTO_JOB_STATUS_READY = 2;
        public const int PROTO_JOB_STATUS_SEND = 4;
        public const int PROTO_JOB_STATUS_RECEIVE = 6;
        public const int PROTO_JOB_STATUS_RECEIVE_DONE = 7;
        public const int PROTO_JOB_STATUS_TRANSPARENT = 8;
        public const int PROTO_JOB_STATUS_RECEIVE_STX = 10;
        public const int PROTO_JOB_STATUS_RECEIVE_ETB_OR_ETX = 11;
        public const int PROTO_JOB_STATUS_RECEIVE_DLE = 12;
        public const int PROTO_JOB_STATUS_RECEIVE_EOT = 13;
        public const int PROTO_JOB_STATUS_RECEIVE_BCC = 14;
        public const int PROTO_JOB_STATUS_RECEIVE_0_OR_1 = 15;
        public const int PROTO_JOB_STATUS_END = 19;
        public const int PROTO_JOB_STATUS_RECEIVE_TRANSPARENT = 20;
        public const int PROTO_JOB_STATUS_RECEIVE_ENQ = 21;
        public const int PROTO_JOB_STATUS_BLOCK = 22;
        public const int PROTO_JOB_STATUS_WAIT_TRANSPARENT = 23;
        public const int PROTO_JOB_STATUS_FILETRN = 24;

        public const int PROTO_TEXT_MAX_LEN = 80;

        public const int PROTO_CHAR_OP_SEPARATOR = 0x2C;

        public const int PROTO_CONTROL_CHAR_STX = 0x02;
        public const int PROTO_CONTROL_CHAR_ETX = 0x03;
        public const int PROTO_CONTROL_CHAR_EOT = 0x04;
        public const int PROTO_CONTROL_CHAR_ENQ = 0x05;
        public const int PROTO_CONTROL_CHAR_ACK = 0x06;
        public const int PROTO_CONTROL_CHAR_DLE = 0x10;
        public const int PROTO_CONTROL_CHAR_0 = 0x30;
        public const int PROTO_CONTROL_CHAR_1 = 0x31;
        public const int PROTO_CONTROL_CHAR_NAK = 0x15;
        public const int PROTO_CONTROL_CHAR_ETB = 0x17;

        public const string PROTO_CONTROL_STRING_DLE0 = "\x10\x30";
        public const string PROTO_CONTROL_STRING_DLE1 = "\x10\x31";
        public const int PROTO_CONTROL_STRING_DLE0_LEN = 2;
        public const int PROTO_CONTROL_STRING_DLE1_LEN = 2;

        public const int LINK_RECONNECTED = 69;

        public const byte DLINK_MODE_SERIAL = 83;	    // connection using RS232 port
        public const byte DLINK_MODE_SERIAL_EASY = 115;    // connection using RS232 port with LAN protocol
        public const byte DLINK_MODE_TCPIP = 76;     // connection using TCP/IP port
        public const byte DLINK_MODE_SSL = 108;    // connection secure SSL
        public const byte DLINK_MODE_TLS = 116;    // connection secure TLS
        public const byte DLINK_MODE_USB = 85;     // connection using USB port
        public const byte DLINK_MODE_EMULATION = 69;     // connection in Emulation Moode
        public const byte DLINK_MODE_USB_EASY = 117;    // connection using USB port with USB protocol
        public const byte DLINK_MODE_BLOCKTRANSFERT = 70;     // connection for UpLoad file tranfert
        public const byte DLINK_MODE_SERVER_LAN = 101;  // connection server for emulator in lan


        //#define WM_EXECUTE_COMPLETE									WM_USER+601     // Notification message to end of the sending operation

        //----------API Return code-----------------------------------------------------
        public const int CM_OK = 0;// Command correctly executed
                                   //#define CM_ERROR                        0xFFFF // Generic Error

        public const int CM_LINK_SYSTEM_ERR = 1050;   //"CMLINK layer -Function communication device error"
        public const int CM_LINK_CONNECTION_ERR = 1051;   //"CMLINK layer -Invalid handle"
        public const int CM_LINK_ALREADY_CONNECTED_ERR = 1052;   //"CMLINK layer -Connection already connected"
        public const int CM_LINK_CONNECT_PARAMETERS_ERR = 1053;   //"CMLINK layer -Syntax error on connect operation"
        public const int CM_LINK_WRITE_ERR = 1054;   //"CMLINK layer -WriteFile Function on device has failed"
        public const int CM_LINK_READ_ERR = 1055;   //"CMLINK layer -ReadFile Function on device has failed"
        public const int CM_LINK_LAN_OPEN_ERR = 1056;   //"CMLINK layer -Error on TCP/IP socket connection routines"
        public const int CM_LINK_LAN_WRITE_ERR = 1057;   //"CMLINK layer -Write on LAN has failed"
        public const int CM_LINK_LAN_READ_ERR = 1058;   //"CMLINK layer -Read on LAN has failed"
        public const int CM_LINK_TIMEOUT_ERR = 1059;   //"CMLINK layer -Time command has expired"
        public const int CM_LINK_OVERFLOW_ERR = 1060;   //"CMLINK layer -Buffer of reply command has been exeed"
        public const int CM_LINK_BCC_ERR = 1061;   //"CMLINK layer -Trasmission error, wrong BCC/char"
        public const int CM_LINK_PROTOCOL_ERR = 1062;   //"CMLINK layer -Trasmission error, wrong protocol "
        public const int CM_LINK_CDM_IN_PROGRESS = 1063;   //"CMLINK layer -Command in progress"
        public const int CM_LINK_TERMINATE_EMULATOR = 1064;   //"CMLINK layer -Command in progress"
        public const int CM_LINK_SEQUENCE_ERR = 1065;   //"CMLINK layer -Command in progress"
        public const int CM_LINK_LAN_CONNECTION_REFUSED = 1066;   //"CMLINK layer -LAN Connection refused 10061"
        public const int CM_LINK_SYNTAX_ERR = 1067;   //"CMLINK Syntax Error"
        public const int CM_LINK_LAN_CONNECTION_RESET = 1068;   //"CMLINK layer -LAN 10054 - Connection reset by peer"
        public const int CM_LINK_LANSERVER_CLIENT_CLOSE = 1069;   //"CMLINK layer Close session by client in LanServer"

        public const int CM_COMMAND_SYNTAXERROR = 2000;   // "CM_COMMAND layer -Error in input parameter"
        public const int CM_COMMAND_INVALID_REPLY_PARAMETER = 2001;   // "CM_COMMAND layer -Output parameter cannot be NULL"
        public const int CM_COMMAND_UNEXPECTEDREPLY = 2002;   // "CM_COMMAND layer -Error in input parameter"
        public const int CM_COMMAND_REFUSED = 2003;   // "CM_COMMAND layer -"
        public const int CM_COMMAND_INVALID_CONNECT_HANDLE = 2004;	//"CM_COMMAND layer -System Error"

        public const byte TRANSPARENT = 35;     //#
        public const byte EMUL_RECEIVE = 64;     //@
        public const int CONNECTION = -1;     //&HFFFF
        public const byte TWS_CTS = 123;    //{
        public const byte EMUL_SEND = 93;     //]

        public const double CM_DEF_TIMEOUT = 0xFFFFFFFF;
        public const double CM_DEF_TIMEOUTx2 = 0xFFFFFFFE;
        public const double CM_DEF_TIMEOUTx3 = 0xFFFFFFFD;
        public const double CM_DEF_TIMEOUTx4 = 0xFFFFFFFC;

        public const short MAX_PATH = 260;
        public const byte LEN_PASSWORD = 6;
        public const byte MAX_SLOT_ACTIVE = 4;
        public const byte MAX_FLASH_REF = 16;
        public const byte MAX_CASSETTES = 24;
        public const byte MAX_DELAYCLASS = 10;
        public const byte MAX_USERID = 10;
        public const byte MAX_MODULE = 30;
        public const byte MAX_CHARDENOM = 4;
        public const byte MAX_CHANNEL = 32;
        public const byte MAX_DEPOSIT = 200;
        public const byte MAX_DENOMINATION = 32;
        public const byte MAX_BOOKING = 29;
        public const byte SERIALNUMBER = 12;
        public const double MAX_LOG = 8192;
        public const byte MAX_HOOPER_COIN = 8;
        public const byte NUM_PROTO_DM = 6;

        public const byte AUTOASSIGNCASSETTES = 65;	    //'A'
        public const byte BELL = 66;     //'B'
        public const byte CLOSE = 67;     //'C'
        public const byte DEPOSIT_CASH = 68;     //'D'
        public const byte EXTENDED_STATUS = 69;     //'E'
        public const byte FILL = 70;     //'F'
        public const byte GET_CASH_DATA = 71;     //'G'
        public const byte ERROR_LOG = 72;     //'H'
        public const byte INIT = 73;     //'I'
        public const byte JOURNAL = 74;     //'J'
        public const byte KEYCHANGE = 75;     //'K'
        public const byte DOWNLOAD = 76;     //'L'
        public const byte OPEN = 79;     //'O'
        public const byte SINGLECMD = 91;     //'['
        public const byte FILETRANSF = 94;     //'^'
        public const byte WITHDRAWAL = 87;     //'W'
        public const byte SETSEVLEVEL = 80;     //'P'
        public const byte SETOIDPAR = 78;     //'N'
        public const byte GETSEVLEVEL = 81;     //'Q'
        public const byte GET_CONFIG = 82;     //'R'
        public const byte SETCONFIG = 83;     //'S'
        public const byte TEST = 84;     //'T'
        public const byte UNDO = 85;     //'U'
        public const byte VERSION = 86;     //'V'
        public const byte TRANSPARENT_START = 88;     //'X'
        public const byte TRANSPARENT_END = 89;     //'Y'
        public const byte MOD_FILL = 90;     //'Z'
        public const byte CTS_TWS = 121;    //'y'

        public const byte NewAssign = 97;     //'a'
        public const byte GET_CASH_MONSTER = 103;    //'g'
        public const byte EXT_STATUS_MONSTER = 101;    //'e'
        public const byte MOVENOTES = 119;    //'w'
        public const byte GET_CNF_MONSTER = 114;    //'r'
        public const byte SWAPNOTES = 115;    //'s'
        public const byte COIN_DSP = 100;    //'d'
        public const byte UTILITY = 117;    //'u'
        public const byte ROBBERY = 98;     //'b'
        public const byte MOD_DEP = 77;     //'M'

        
        public const string RTGETPHSTATUS = "E201";
        public const string RTGETFWVER = "E211";
        public const string RTGETFWREL = "E212";
        public const string RTGETPHFSHIFT = "E220";
        public const string RTGETPHFTINQ = "E221";
        public const string RTGETPHFCOUNT = "E222";
        public const string RTGETPHFC4A = "E223";
        public const string RTGETPHFC3 = "E224";
        public const string RTGETPHFREJ = "E225";
        public const string RTGETPHFINC = "E226";
        public const string RTGETPHFFEED = "E227";
        public const string RTGETPHFC1 = "E228";
        public const string RTGETPHFINL = "E229";
        public const string RTGETPHFOUT = "E22A";
        public const string RTGETPHFHMAX = "E22B";
        public const string RTGETPWMMSO = "E22D";
        public const string RTGETMSVSTEPTIME = "E22E";
        public const string RTGETPHFC4B = "E230";
        public const string RTGETPHFBPKSUS = "E231";
        
        public const string FWPRODOTTO = "FW_PRODOTTO";
        public const string IMMAGINIeVIDEO = "IMMAGINI+VIDEO";

        #endregion

        #region REPLYCODE
        IniFile replyIni = new IniFile("replycode.ini");
        #endregion

        #region TRACE

        public short HTRACE;
        
        public enum LAYER
        {
            CMLINK, CMCOMMAND,SIBCASH,APPLICATION
        }
        public enum TRCTYPE
        {
            CMDIMENSION,CMDATE
        }
        public struct Component
        {
            public short level;
        }
        public struct ComScope
        {
            public IntPtr hWin;
        }

        
        public struct TraceData
        {
            public short rc;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string desc;
            public byte jam;
        }
        public TraceData TRACEDATA;

        public struct TraceDirective
        {
            public LAYER infoLayer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string tracePathName;
            public ComScope comscope;
            public TRCTYPE trcType;
            public short trcSize;
            public short trcNum;
            [MarshalAs(UnmanagedType.ByValArray,SizeConst = 4)]
            public Component[] infoComp;
        }
        public TraceDirective TRACEDIRECTIVE;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement lpwndpl);

        public struct WindowPlacement
        {
            public uint length;
            public uint flasg;
            public uint showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
            public System.Drawing.Rectangle rcDevice;
        }
        WindowPlacement WINDOWSPLACEMENT;

        [DllImport("CMTrace.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMTrace_SetTraceDirective")]
        static extern short CMTraceSetTraceDirective(TraceDirective directive);

        [DllImport("CMTrace.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMTrace_Exit")]
        static extern short CMTraceExit(short hTrace);

        [DllImport("CMTrace.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMTrace_Init")]
        static extern short CMTraceInit(LAYER layer);

        [DllImport("CMTrace.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMTrace_Trace")]
        static extern short CMTraceTrace(short htrace, int dwTraceErrorLevel, ref IntPtr trace);

        //public void TraceTrace()
        //{
        //    //CMTraceTrace(HTRACE, dw, ref &TRACEDATA.desc);

        //}

        public short TraceInit(LAYER layer)
        {
            HTRACE = CMTraceInit(layer);
            return HTRACE;
        }
        //public static IntPtr FindWindowsByProcessId(int dwProcessId)
        //{
        //    Process proc = Process.GetProcessById(dwProcessId);
        //    return proc.MainWindowHandle;
        //}
        //public static IntPtr FindWindowsByProcessName(string dwProcessName)
        //{
        //    Process[] localByName = Process.GetProcessesByName(dwProcessName);
        //    return localByName[0].MainWindowHandle;
        //}

        //public void TraceExit()
        //{
        //    HTRACE = CMTraceExit(HTRACE);
        //}
        public string SetTraceDirective(TraceDirective directive)
        {
            //Process.Start("notepad.exe");
            //System.Threading.Thread.Sleep(1000);
            //GetWindowPlacement(FindWindowsByProcessId(32),out WINDOWSPLACEMENT);
            //GetWindowPlacement(FindWindowsByProcessName("notepad"), out WINDOWSPLACEMENT);
            //TRACEDIRECTIVE.infoLayer = LAYER.CMLINK;
            //TRACEDIRECTIVE.tracePathName = "prova.txt";
            //TRACEDIRECTIVE.trcSize = 10; //MegaByte
            //TRACEDIRECTIVE.trcType = TRCTYPE.CMDIMENSION;
            //TRACEDIRECTIVE.trcNum = 30;
            //TRACEDIRECTIVE.infoComp = new Component[4];
            //TRACEDIRECTIVE.infoComp[0].level = 4; //CMLINK
            //TRACEDIRECTIVE.infoComp[1].level = 4; //CMCOMMAND
            //TRACEDIRECTIVE.infoComp[2].level = 4;
            //TRACEDIRECTIVE.infoComp[3].level = 4;
            //TRACEDIRECTIVE.comscope.hWin = FindWindowsByProcessName("notepad");
            CMTraceSetTraceDirective(directive);
            return "";
        }

        #endregion

        #region STRUCTURES

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] - Sembra possa essere omesso
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]

        public enum CMVER
        {
            CMDOLD, CMDNEW
        }

        public struct IFn7
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string bitOriented;
        }
        public IFn7 INPUTOPFn7;
        public IFn7 INPUTOPFn8;
        public IFn7 INPUTOPFn9;
        public IFn7 INPUTOPFn23;

        public struct IDownload
        {
            public byte target;
            public byte mode;
            public IntPtr p1;
            public IntPtr p2;
            public IntPtr p3;
            public IntPtr p4;
            public short m;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string fileName;
            public IntPtr fileData;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024*16)]
            public string intelHexFile;
        }
        public IDownload INPUTOPDOWNLOAD;
        
        public struct ISwapB
        {
            public CMVER ver;
            public byte side;
            public short mode;
            public byte gate;
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES)]
            public InfoNotesB[] info;
        }
        public ISwapB INPOPMOVE;

        public struct OSwapB
        {
            public byte side;
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES)]
            public InfoNotesB[] info;
            public short rc;
        }
        public OSwapB OUTOPMOVE;

        public struct InfoNotesB
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
            public short num;
            public short mmm;
        }

        

        public struct Tn01
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string protInterface;
            public short rc;
        }
        public Tn01 OUTOPTn01;

        public struct Tn015
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string bitNotation;
            public short rc;
        }
        public Tn015 OUTOPTn015;
        public Tn015 OUTOPTn028;
        public Tn015 OUTOPTn032;
        public Tn015 OUTOPTn049;

        

        public struct Tn31
        {
            public short numCurr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FLASH_REF)]
            public Currencies[] currInUse;
            public short rc;
        }
        public Tn31 OUTOPTn31;

        public struct Tn32
        {
            public short numTrNote;
            public short numAccNote;
            public short numRefNote;
            public short numFalseNote;
            public short numSuspectNote;
            public short firstFailedClass;
            public short firstFailedInfr;
            public short firstFailedMagn;
            public short firstFailedFormat;
            public short numUnfAccSospNote;
            public short res0;
            public short res1;
            public short res2;
            public short res3;
            public short res4;
            public short res5;
            public short rc;
        }
        Tn32 OUTOPTn32;

        public struct Tn34
        {
            public short numSlot;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SLOT_ACTIVE)]
            public Slot[] slotInUse;
            public short rc;
        }
        public Tn34 OUTOPTn34;

        public struct Tn35
        {
            public short numRef;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FLASH_REF)]
            public References[] refInFlash;
            public short rc;
        }
        public Tn35 OUTOPTn35;

        public struct TnX13
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string name;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string ver;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string rel;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SERIALNUMBER + 1)]
            public string serialNumber;
            public short rc;
        }
        public TnX13 OUTOPTnX13;

        public struct References
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string refName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string refVer;
            public byte enab;
            public short bankId;
        }

        
        public struct Slot
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string refName;
            public byte ena;
        }

        public struct OTn010
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string cc;
            public DataTimeClock dataTimeClock;
            public short rc;
        }
        public OTn010 OUTOPTn010;

        public struct OTn044
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string buff;
            public short rc;
        }
        public OTn044 OUTOPTn044;

        public struct OTn0601
        {
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public short[] photo;
            public short rc;
        }
        public OTn0601 OUTOPTn0601;

        public struct OTn088
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string cmxx;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string name;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string type;
            public short numProtCas;
            public short numCas;
            public short numProtBag;
            public short numBag;
            public short numCd80;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string mode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string led;
            public short rc;
        }
        public OTn088 OUTOPTn088;

        public struct OTn214
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_FLASH_REF + 1)]
            public string serialNum;
            public short rc;
        }
        public OTn214 OUTOPTn214;


        public struct OTn321
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cdfName;
            public short rc;
        }
        public OTn321 OUTOPTn321;

        public struct OTn620
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SERIALNUMBER + 1)]
            public string serialNum;
            public short rc;
        }
        public OTn620 OUTOPTnBox4;

        public struct OTn660
        {
            public ushort value;
            public short rc;
        }
        public OTn660 OUTOPTn660;
        public OTn660 OUTOPTn061;

        

        public struct Currencies
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string curName;
            public short numSlot;
            public byte ena;
        }

        public struct TypeExIfo
        {
            public short type;
            public IntPtr exInfo;
        }
        TypeExIfo INPUTOPFILL;

        public struct IFn4
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string cc;
            public DataTimeClock dataTimeClock;
        }
        public IFn4 INPUTOPFn4;

        //public struct TypeExInfo
        //{
        //    public short type;
        //    public IFn4 exInfo;
        //}

        //public struct IFn4
        //{
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        //    public string cc;
        //    [MarshalAs(UnmanagedType.FunctionPtr)]
        //    public DataTimeClock dataTimeClock;
        //}


        public struct OTn08
        {
            public short safeDoor;
            public short trayCassette;
            public short cover;
            public short feeder;
            public short inputSlot;
            public short rejectSlot;
            public short leftSlot;
            public short rightSlot;
            public short leftExtButton;
            public short rightExtButton;
            public short cageOpen;
            public short gateOpen;
            public short bagOpen;
            public short fkb;
            public short fks;
            public short rc;
        }
        public OTn08 OUTOPTn08;
        public OTn08 OUTOPTn00;

        public struct OutExStatus
        {
            public ErrorTable errorTable;
            public short rc;
        }
        public OutExStatus OUTOPEXSTATUS;
        public struct ErrorTable
        {
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MODULE+NUM_PROTO_DM*4)]
            public InfoErr[] info;
        }
        public struct InfoErr
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string err;
        }

        

        public struct SNull
        { }
        SNull StructureNull;


        public struct OnlyRc
        {
            public short rc;
        }
        public OnlyRc ONLYRC;

        public struct DataTimeClock
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string sec;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string min;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string hour;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string day;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string week;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string month;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string year;
        }
        public DataTimeClock DATATIMECLOCK;
        

        



        //---------------NON FUNZIONANO --------------------------------------------------------------------------------
        public struct IInit
        {
            public byte side;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LEN_PASSWORD + 1)]
            public string password;
            public IntPtr mode;
        }
        IInit INPUTOPINT;

        //[StructLayout(LayoutKind.Sequential)]
        public struct IInitMode
        {
            public short inote;
            public IntPtr bundle;
        }

        public struct OInit
        {
            public byte side;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
            public short ms;
            public short numPhysical;
            public short numLogical;
            public short rc;
        }
        public OInit OUTOPINIT;
        
        public struct IGetConfig
        {
            public byte side;
            public IntPtr bank;
        }
        public IGetConfig INPUTOPGETCONFIG;

        public struct OGetConfig
        {
            public byte side;
            public short bank;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES)]
            public FourChar[] cassDenom;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNEL)]
            public Config[] configInfo;
            public short numItem;
            public short msg;
            public short rc;
        }
        public OGetConfig OUTOPGETCONFIG;

        public struct Config
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string version;
            public short height;
            public byte enable;
        }


        public struct FourChar
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
        }

        public struct ISetConfign
        {
            public byte side;
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES * MAX_CHANNEL)]
            public ISetConfig[] info;
        }
        public ISetConfign INPUTOPSETCONFIG;

        public struct ISetConfig
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
            public byte enab;
            public byte target;
        }
        public ISetConfig SETCONFIGINFO;

        public struct OSetConfign
        {
            public byte side;
            public short msg;
            public short rc;
        }
        public OSetConfign OUTOPSETCONFIG;
        //---------------NON FUNZIONANO --------------------------------------------------------------------------------

        public struct SRSConf
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string device;
            public Int32 baudrate;
            public Int32 parity;
            public Int32 stop;
            public Int32 car;
            public bool dtr;
        }
        public SRSConf RSCONF;
        
        public struct STcpipPar
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string clientIpAddr;
            public int portNumber;
        }
        public STcpipPar TCPIPPAR;

        public struct ConnectionParam
        {
            public byte ConnectionMode;
            public SRSConf pRsConf;
            public STcpipPar TcpIpConf;
        }
        public ConnectionParam CONNECTIONPARAM;

        public struct TBuffer
        {
            public string buffMsg;
            public int lenMsg;
        }
        public TBuffer IOBUFFER;

        public struct ISetOidPar
        {
            public short mode;
            public int baud;
        }
        public ISetOidPar INPUTOPSEOIDPAR;

        public struct InfoDll
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
            public string name;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
            public string version;
        }
        public InfoDll INFODLLS;

        public struct SwDllVersion
        {
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public InfoDll[] info;
        }
        public SwDllVersion SWDLLVER;

        public struct IOpen
        {
            public byte side;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LEN_PASSWORD + 1)]
            public string password;
            public string opt;
        }
        public IOpen INPUTOPOPEN;

        public struct OOpen
        {
            public byte side;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string fwVersion;
            public short rc;
        }
        public OOpen OUTOPOPEN;

        public struct InpOpTest
        {
            public byte module;
            public short type;
            public IntPtr exInfo;
        }
        public InpOpTest INPUTOPTEST;
        public InpOpTest INPUTOPMODFILL;
        public InpOpTest INPUTOPUTILITY;

        public struct InpOpTest2
        {
            public byte module;
            public short type;
            public short exInfo;
        }
        public InpOpTest2 INPUTOPTEST2;

        public struct OVersion
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string ver;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string rel;
            public short rc;
        }
        public OVersion OUTOPVERSION;

        public struct IDeposit
        {
            public byte side;
            public short mode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
            public SNull exInfo;
        }
        public IDeposit INPUTOPDEPOSIT;

        public struct ODeposit
        {
            public byte side;
            public short aaa;
            public short rrr;
            public short uuu;
            public short fff;
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES * MAX_CHANNEL)]
            public InfoNotes[] depNote;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES)]
            public short[] noteInSafe;
            public short rc;
        }
        public ODeposit OUTOPDEPOSIT;

        public struct IClose
        {
            public byte side;
        }
        public IClose INOPCLOSE;

        public struct OClose
        {
            public byte side;
            public byte enable;
            public short rc;
        }
        public OClose OUTOPCLOSE;
        public OClose OUTOPSETSEVLEVEL;
        public OClose OUTOPMn1;
        public OClose OUTOPTnT7;
        public OClose OUTOPTn096;

        public struct IUndo
        {
            public byte side;
            public short mode;
        }
        IUndo INPUTOPUNDO;
        public struct OUndo
        {
            public byte side;
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES * MAX_CHANNEL)]
            public InfoNotes[] info;
            public short rc;
        }
        OUndo OUTOPUNDO;
        public struct InfoNotes
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
            public short num;
        }

        public struct IWithdrawal
        {
            public byte side;
            public short target;
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES)]
            public InfoNotes[] info;
        }
        public IWithdrawal INPUTOPWITHDRAW;
        public IWithdrawal INPOPSWAP;

        public struct OWithdrawal
        {
            public byte side;
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES)]
            public InfoNotes[] info;
            public short rc;
        }
        public OWithdrawal OUTOPWITHDRAW;
        public OWithdrawal OUTOPSWAP;

        public struct IGetCashData
        {
            public byte side;
        }
        public IGetCashData INOPGETCASH;

        public struct OGetCashData
        {
            public byte side;
            public short numItem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CASSETTES * MAX_CHANNEL)]
            public InfoCassettes[] info;
            public short rc;
        }
        public OGetCashData OUTOPGETCASH;
        public struct InfoCassettes
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string denom;
            public int notesNumber;
            public short numFree;
            public byte cassette;
            public byte enable;
        }
                
        public struct OTransparentMode
        {
            public short rc;
        }
        public OTransparentMode OUTOPTRASPMODE;

        public struct OpTransp
        {
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1700)]
            public string buff;
            public int size;
        }
        public OpTransp INTRANSP;
        public OpTransp OUTTRANSP;
        public OpTransp INSINGLE;
        public OpTransp OUTSINGLE;
        public OpTransp INPUTOPTn60;

        
        #endregion

        #region DLLIMPORTS
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        static extern short CMGetCashData(int hCon, byte idCommand, ref IGetCashData lpCmd, ref OGetCashData lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        static extern short CMGetFwSuiteIdentification(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn044 lpReply, int timeOut);
        
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        static extern short CMGetCassetteNumber(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn660 lpReply, int timeOut);
        
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        static extern short CMAssign(int hCon, byte idCommand, ref SNull lpCmd, ref OTn660 lpReply, int timeOut);
        
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Connect")]
        public static extern short CMConnect(ref short hCon, ref ConnectionParam ConnParam);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Disconnect")]
        public static extern short CMDisconnect(short hCon);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        static extern short CMGetMainCtrlModuleName(int hCon, byte idCommand, ref InpOpTest lpCmd, ref TnX13 lpReply, int timeOut);
        
        //[DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        //static extern short CMVersion(int hCon, byte idCommand, ref short lpCmd, ref OVersion lpReply, int timeOut);
        
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        static extern short CMDeposit(int hCon, byte idCommand, ref IDeposit lpCmd, ref ODeposit lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        static extern short CMClose(int hCon, byte idCommand, ref IClose lpCmd, ref OClose lpReply, int timeOut);
        
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short cmdOpen(int hCon, byte idCommand, ref IOpen lpCmd, ref OOpen lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short cmdUndo(int hCon, byte idCommand, ref IUndo lpCmd, ref OUndo lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMWithdrawal(int hCon, byte idCommand, ref IWithdrawal lpCmd, ref OWithdrawal lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMTransparentOn(int hCon, byte idCommand, ref SNull lpCmd, ref OTransparentMode lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMTransparentOff(int hCon, byte idCommand, ref SNull lpCmd, ref OTransparentMode lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMTransparentCommand(int hCon, byte idCommand, ref OpTransp lpCmd, ref OpTransp lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetCassetteConfiguration(int hCon, byte idCommand, ref IGetConfig lpCmd, ref OGetConfig lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetCMConfig(int hCon, byte idCommand, ref InpOpTest lpCmd, ref Tn015 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetCMDateTime(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn010 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMSetCMDateTime(int hCon, byte idCommand, ref TypeExIfo lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetReaderShortStatistics(int hCon, byte idCommand, ref InpOpTest lpCmd, ref Tn32 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetCDFName(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn321 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMEmptyCassette(int hCon, byte idCommand, ref IInit lpCmd, ref OInit lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMExtendedStatus(int hCon, byte idCommand, ref SNull lpCmd, ref OutExStatus lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMUnitCoverTest(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn08 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMSetCMConfig(int hCon, byte idCommand, ref TypeExIfo lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetReaderBanks(int hCon, byte idCommand, ref InpOpTest lpCmd, ref Tn31 lpReply, int timeOut);

        //[DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        //public static extern short CMGetReaderActiveBanks(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn660 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetReaderActiveBanks(int hCon, byte idCommand, ref InpOpTest lpCmd, ref Tn34 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetReaderReferences(int hCon, byte idCommand, ref InpOpTest lpCmd, ref Tn35 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetReaderInformation(int hCon, byte idCommand, ref InpOpTest lpCmd, ref Tn35 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetModulesVersion(int hCon, byte idCommand, ref short lpCmd, ref OVersion lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetUnitProtocolIdentification(int hCon, byte idCommand, ref InpOpTest lpCmd, ref Tn01 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetMachineIdentification(int hCon, byte idCommand, ref InpOpTest lpCmd, ref TnX13 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetCMUnitIdentification(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn044 lpReply, int timeOut);

        //[DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        //public static extern short CMGetCMCassetteNumber(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn660 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMSetCMCassetteNumber(int hCon, byte idCommand, ref TypeExIfo lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMPhotoSensorsRead(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn0601 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMSwitchProtocolMode(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMAdvancedUnitIdentification(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn088 lpReply, int timeOut);

        //[DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        //public static extern short CMGetAudioParameters(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn660 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMSetAudioParameters(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetTime(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetHwControllerSN(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn214 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMBTransferNotes(int hCon, byte idCommand, ref ISwapB lpCmd, ref OSwapB lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMFillCommand(int hCon, byte idCommand, ref TypeExIfo lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMGetBarcodeCD80(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn620 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMSetBarcodeCD80(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMSingleCommand(int hCon, byte idCommand, ref OpTransp lpCmd, ref OpTransp lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_DllsLevelUsed")]
        public static extern short CMDllsLevelUsed(ref SwDllVersion infoDll);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_GetBuffTrace")]
        //[return: MarshalAs(UnmanagedType.LPStr)]
        public static extern IntPtr CMGetBuffTrace(int hCon);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMTestCommand(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn660 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMTestCommand(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OTn0601 lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMTestCommand(int hCon, byte idCommand, ref InpOpTest lpCmd, ref OnlyRc lpReply, int timeOut);

        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMDownload(int hCon, byte idCommand, ref IDownload lpCmd, ref OnlyRc lpReply, int timeOut);
        #endregion

        #region TRANSPARENTCOMMANDS

        // READER TRANSPARENT COMMANDS
        public string RSStatus()
        {
            return TransparentCommand("F3");
        }


        public string RSGetSerialNumber()
        {
            return utils.DecAsciiToAscii(TransparentCommand("E327"));
        }

        public string RSGetFwHostName()
        {
            string host = TransparentCommand("E32B", 2000);
            if (host.Length != 26)
            {
                host = TransparentCommand("E320", 2000);
            }
            if (host.Length != 26)
                return "FAIL";
            return utils.DecAsciiToAscii(host).Substring(0,8);
        }

        public string RSGetFwDspName()
        {
            string dsp = TransparentCommand("E321", 2000);
            if (dsp.Length < 26)
                return "FAIL";
            return utils.DecAsciiToAscii(dsp).Substring(0, 8);
        }

        public string RSGetFwMagName()
        {
            string mag = TransparentCommand("D3320000030E", 2000);
            if (mag.Length < 256)
                return "FAIL";
            mag = utils.GetEvenChars(mag);
            string temp = "";
            for (int i = 0; i<mag.Length;i+=4)
            {
                temp += mag.Substring(i + 2, 2);
            }
            mag = utils.HexToAscii(temp);
            return mag.Substring(0, 8);
        }

        public string RSGetFwUsName()
        {
            string us = TransparentCommand("D37C3F", 6000);
            if (us.Length < 128)
                return "FAIL";
            us = utils.GetEvenChars(us);
            us = utils.HexToAscii(us);
            return us.Substring(0, 8);
        }

        //result = utils.DecAsciiToAscii(cmCommand.TransparentCommand("E320", 2000));
        // SAFE CONTROLLER/BAG TRANSPARENT COMMANDS

        // REALT TIME TRASNPARENT COMMANDS
        public void RTPowerOn()
        {
            TransparentCommand("D233", 100);
        }

        public void RTPowerOff()
        {
            TransparentCommand("D234", 100);
        }
        public void RTAlignHome()
        {
            TransparentCommand("D21C", 100);
        }
        public void RTFeederOff()
        {
            TransparentCommand("D23A", 100);
        }
        public void RTFeederMotorOff()
        {
            TransparentCommand("D219", 100);
        }
        public void RTFeederMotorOn()
        {
            TransparentCommand("D218", 100);
        }

        public void RTMDEHome()
        {
            TransparentCommand("D20A", 100);
        }
        public void RTMDEIn()
        {
            TransparentCommand("D209", 100);
        }
        public void RTMDEOut()
        {
            TransparentCommand("D208", 100);
        }
        public void RTMOTAOff()
        {
            TransparentCommand("D219", 100);
        }
        public void RTMOTAOn()
        {
            TransparentCommand("D218", 100);
        }
        public void RTMSOIn()
        {
            TransparentCommand("D210", 100);
        }
        public void RTMSOOut()
        {
            TransparentCommand("D211", 100);
        }
        public void RTMSOOff()
        {
            TransparentCommand("D212", 100);
        }
        public void RTPhotoAdjustAll()
        {
            TransparentCommand("D23C", 100);
        }
        public void RTPhotoAdjustFC1()
        {
            TransparentCommand("D244", 100);
        }
        public void RTPhotoAdjustFC3()
        {
            TransparentCommand("D241", 100);
        }
        public void RTPhotoAdjustFC4A()
        {
            TransparentCommand("D240", 100);
        }
        public void RTPhotoAdjustFC4B()
        {
            TransparentCommand("D249", 100);
        }
        public void RTPhotoAdjustFCOUNT()
        {
            TransparentCommand("D23F", 100);
        }
        public void RTPhotoAdjustFFEED()
        {
            TransparentCommand("D245", 100);
        }
        public void RTPhotoAdjustFHMAX()
        {
            TransparentCommand("D246", 100);
        }
        public void RTPhotoAdjustFINC()
        {
            TransparentCommand("D243", 100);
        }
        public void RTPhotoAdjustFINL()
        {
            TransparentCommand("D248", 100);
        }
        public void RTPhotoAdjustFINQ()
        {
            TransparentCommand("D23E", 100);
        }
        public void RTPhotoAdjustFOUT()
        {
            TransparentCommand("D247", 100);
        }
        public void RTPhotoAdjustFPBKSUS()
        {
            TransparentCommand("D24A", 100);
        }
        public void RTPhotoAdjustFREJ()
        {
            TransparentCommand("D242", 100);
        }
        public void RTPhotoAdjustFSHIFT()
        {
            TransparentCommand("D23D", 100);
        }
        public void RTPocketHOME()
        {
            TransparentCommand("D24D", 100);
        }
        public void RTPocketIN()
        {
            TransparentCommand("D24B", 100);
        }
        public void RTPocketREJ()
        {
            TransparentCommand("D24C", 100);
        }
        public void RTPreFeedingOFF()
        {
            TransparentCommand("D20F", 100);
        }
        public void RTPreFeedingON()
        {
            TransparentCommand("D20E", 100);
        }
        public void RTPressorCLOSE()
        {
            TransparentCommand("D20D", 100);
        }
        public void RTPressorHOME()
        {
            TransparentCommand("D20B", 100);
        }
        public void RTPressorOPEN()
        {
            TransparentCommand("D20C", 100);
        }
        public void RTPusherCLOSE()
        {
            TransparentCommand("D21F", 100);
        }
        public void RTPusherHOME()
        {
            TransparentCommand("D21D", 100);
        }
        public void RTPusherOPEN()
        {
            TransparentCommand("D21E", 100);
        }
        public void RTShiftON()
        {
            TransparentCommand("D206", 100);
        }
        public void RTShitOFF()
        {
            TransparentCommand("D207", 100);
        }


        #endregion

        #region COMMANDS
        
        public string SetCMConfig(string configuration)
        {
            INPUTOPFILL.type = 7;
            INPUTOPFn7.bitOriented = configuration;
            int sizePtr = Marshal.SizeOf(typeof(IntPtr));
            IntPtr ptrExInfo = Marshal.AllocHGlobal(sizePtr);
            Marshal.StructureToPtr(INPUTOPFn7, ptrExInfo, false);
            INPUTOPFILL.exInfo = ptrExInfo;

            short reply = CMSetCMConfig(hCon, FILL, ref INPUTOPFILL, ref ONLYRC, 5000);
            Marshal.FreeHGlobal(ptrExInfo);
            return CommandReply(ONLYRC.rc);
        }
        public string SetCMOptionConfig(string configuration)
        {
            INPUTOPFILL.type = 8;
            INPUTOPFn7.bitOriented = configuration;
            int sizePtr = Marshal.SizeOf(typeof(IntPtr));
            IntPtr ptrExInfo = Marshal.AllocHGlobal(sizePtr);
            Marshal.StructureToPtr(INPUTOPFn7, ptrExInfo, false);
            INPUTOPFILL.exInfo = ptrExInfo;

            short reply = CMSetCMConfig(hCon, FILL, ref INPUTOPFILL, ref ONLYRC, 5000);
            Marshal.FreeHGlobal(ptrExInfo);
            return CommandReply(ONLYRC.rc);
        }
        public string SetCMOptionOneConfig(string configuration)
        {
            INPUTOPFILL.type = 9;
            INPUTOPFn7.bitOriented = configuration;
            int sizePtr = Marshal.SizeOf(typeof(IntPtr));
            IntPtr ptrExInfo = Marshal.AllocHGlobal(sizePtr);
            Marshal.StructureToPtr(INPUTOPFn7, ptrExInfo, false);
            INPUTOPFILL.exInfo = ptrExInfo;

            short reply = CMSetCMConfig(hCon, FILL, ref INPUTOPFILL, ref ONLYRC, 5000);
            Marshal.FreeHGlobal(ptrExInfo);
            return CommandReply(ONLYRC.rc);
        }
        public string SetCMOptionTwoConfig(string configuration)
        {
            INPUTOPFILL.type = 23;
            INPUTOPFn7.bitOriented = configuration;
            int sizePtr = Marshal.SizeOf(typeof(IntPtr));
            IntPtr ptrExInfo = Marshal.AllocHGlobal(sizePtr);
            Marshal.StructureToPtr(INPUTOPFn7, ptrExInfo, false);
            INPUTOPFILL.exInfo = ptrExInfo;

            short reply = CMSetCMConfig(hCon, FILL, ref INPUTOPFILL, ref ONLYRC, 5000);
            Marshal.FreeHGlobal(ptrExInfo);
            return CommandReply(ONLYRC.rc);
        }

        public string UninstallFiles(short directory)
        {
            INPUTOPDOWNLOAD.target = (byte)'6';
            INPUTOPDOWNLOAD.mode = (byte)'U';
            INPUTOPDOWNLOAD.m = directory;
            short reply = CMDownload(hCon, DOWNLOAD, ref INPUTOPDOWNLOAD, ref ONLYRC, 5000);
            return CommandReply(ONLYRC.rc);
        }

        public string DllsLevelUsed()
        {
            short reply = CMDllsLevelUsed(ref SWDLLVER);
            return ""; 
        }

        public string GetBuffTrace()
        {
            IntPtr buffer;
            buffer = CMGetBuffTrace(hCon);
            return Marshal.PtrToStringAnsi(buffer);
        }

        public string SingleCommand(string command, int timeout=1000)
        {
            INSINGLE.buff = command;
            INSINGLE.size = command.Length;
            OUTSINGLE.buff = new string((char)0, 1700);
            OUTSINGLE.size = OUTSINGLE.buff.Length;
            CMSingleCommand(hCon, SINGLECMD, ref INSINGLE, ref OUTSINGLE, timeout);
            return OUTSINGLE.buff;
        }


        public string FillCD80CassetteNumber(int CD80Number)
        {
            INPUTOPFILL.type = 30;
            unsafe
            {
                ushort cd80NumUnsafe = (ushort)CD80Number;
                ushort* ptr = &cd80NumUnsafe;
                INPUTOPFILL.exInfo = (IntPtr)ptr;
            }
            CMFillCommand(hCon, FILL, ref INPUTOPFILL, ref ONLYRC, 5000);
            return CommandReply(ONLYRC.rc);
        }

        public string GetBarcodeCD80(char module = 'A')
        {
            INPUTOPTEST.module = (byte)module;
            INPUTOPTEST.type = 4;

            CMGetBarcodeCD80(hCon, TEST, ref INPUTOPTEST, ref OUTOPTnBox4, 5000);
            return CommandReply(OUTOPTnBox4.rc);
        }

        public string SetShiftCenterValue(ushort value)
        {
            INSINGLE.buff = "T,1,0,63,1," + value;
            INSINGLE.size = INSINGLE.buff.Length;
            OUTSINGLE.buff = new string((char)0, 1700);
            OUTSINGLE.size = OUTSINGLE.buff.Length;
            int cmdReply = CMSingleCommand(hCon, SINGLECMD, ref INSINGLE, ref OUTSINGLE, 5000);
            string[] reply = OUTSINGLE.buff.Split(',');
            if (reply.Length == 1)
                return "FAIL";
            return CommandReply(Convert.ToInt16(reply[5]));


            //INPUTOPTEST.module = (byte)'0';
            //INPUTOPTEST.type = 63;
            //unsafe
            //{
            //    ushort centerValue = value;
            //    ushort* ptr = &centerValue;
            //    INPUTOPTEST.exInfo = (IntPtr)ptr;
            //}
            //int reply = CMTestCommand(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn660, 5000);
            //return CommandReply(OUTOPTn660.rc);
        }
        public string TransferNotesBag(char side = 'R')
        {
            INPOPMOVE.ver = CMVER.CMDNEW;
            INPOPMOVE.side = (byte)side;
            INPOPMOVE.mode = 0;
            INPOPMOVE.gate = (byte)'q';
            INPOPMOVE.numItem = 1;

            InfoNotesB[] notes = new InfoNotesB[MAX_CASSETTES];
            notes[0].denom = "CPEA";
            notes[0].num = 2;
            notes[0].mmm = 1;
            INPOPMOVE.info = notes;

            CMBTransferNotes(hCon, MOVENOTES, ref INPOPMOVE, ref OUTOPMOVE, 5000);
            return CommandReply(OUTOPMOVE.rc);
        }

        public string GetHwControllerSN()
        {
            INPUTOPTEST.module = (byte)'2';
            INPUTOPTEST.type = 14;

            CMGetHwControllerSN(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn214, 5000);
            return CommandReply(OUTOPTn214.rc);
        }

        public string FillHwControllerSN(string serial)
        {
            INSINGLE.buff = "Z,1,0,0," + serial;
            INSINGLE.size = INSINGLE.buff.Length;
            CMSingleCommand(hCon, TEST, ref INSINGLE, ref OUTSINGLE, 5000);
            string[] reply = OUTSINGLE.buff.Split(',');
            if (reply.Length == 1)
                return "FAIL";
            return CommandReply(Convert.ToInt16(reply[4]));
        }

        public string FillMACaddress(string lastFourHex, string firstEightHex="000E7281")
        {
            INSINGLE.buff = "Z,1,0,0," + firstEightHex + lastFourHex;
            INSINGLE.size = INSINGLE.buff.Length;
            CMSingleCommand(hCon, TEST, ref INSINGLE, ref OUTSINGLE, 5000);
            string[] reply = OUTSINGLE.buff.Split(',');
            if (reply.Length == 1)
                return "FAIL";
            return CommandReply(Convert.ToInt16(reply[4]));
        }

        public string SetAudioParameters(ushort volume)
        {
            INPUTOPTEST.module = (byte)'6';
            INPUTOPTEST.type = 24;
            unsafe
            {
                ushort* ptr = &volume;
                INPUTOPTEST.exInfo = (IntPtr)ptr;
            }
            CMSetAudioParameters(hCon, MOD_FILL, ref INPUTOPTEST, ref ONLYRC, 5000);
            return CommandReply(ONLYRC.rc);
        }

        public string GetAudioParameters()
        {
            INPUTOPTEST.module = (byte)'6';
            INPUTOPTEST.type = 24;
            CMTestCommand(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn660, 5000);
            return CommandReply(OUTOPTn660.rc);
        }

        public string AdvancedUnitIdentification()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 88;
            CMAdvancedUnitIdentification(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn088, 5000);
            return CommandReply(OUTOPTn088.rc);
        }

        public string SwitchProtocolMode(short value)
        {
            INPUTOPTEST.module = (byte)'6';
            INPUTOPTEST.type = value;
            CMSwitchProtocolMode(hCon, UTILITY, ref INPUTOPTEST, ref ONLYRC, 5000);
            return CommandReply(ONLYRC.rc);
        }

        public string SetCMCassetteNumber(short cassetteNumber)
        {
            INPUTOPFILL.type = 11;
            unsafe
            {
                short* ptrCassetteNumber = &cassetteNumber;
                INPUTOPFILL.exInfo = (IntPtr)ptrCassetteNumber;
            }

            CMSetCMCassetteNumber(hCon, FILL, ref INPUTOPFILL, ref ONLYRC, 5000);
            return CommandReply(ONLYRC.rc);
        }

        public string GetCMCassetteNumber()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 61;

            CMTestCommand(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn660, 5000);
            return CommandReply(OUTOPTn660.rc);
        }

        public string GetShiftCenterValue()
        {
            INSINGLE.buff = "T,1,0,63,0";
            INSINGLE.size = INSINGLE.buff.Length;
            OUTSINGLE.buff = new string((char)0, 1700);
            OUTSINGLE.size = OUTSINGLE.buff.Length;
            OUTOPTn660.value = 0;
            int cmdReply = CMSingleCommand(hCon, SINGLECMD, ref INSINGLE, ref OUTSINGLE, 5000);
            string[] reply = OUTSINGLE.buff.Split(',');
            if (reply.Length == 1)
                return "FAIL";
            OUTOPTn660.value = (ushort)Convert.ToInt16(reply[6]);
            return CommandReply(Convert.ToInt16(reply[5]));

            //INPUTOPMODFILL.module = (byte)'0';
            //INPUTOPMODFILL.type = 63;
            //INPUTOPMODFILL.exInfo = (IntPtr)1;

            //unsafe
            //{
            //    short test = 0;
            //    short* ptr = &test;
            //    INPUTOPMODFILL.exInfo = (IntPtr)test;
            //}
            //int reply = CMTestCommand(hCon, TEST, ref INPUTOPMODFILL, ref OUTOPTn660, 5000);
            //return CommandReply(OUTOPTn660.rc);
        }

        public string GetCMUnitIdentification()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 77;

            CMGetCMUnitIdentification(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn044, 5000);
            return CommandReply(OUTOPTn044.rc);
        }

        public string SetCMUnitIdentification(string unitIdentification)
        {
            INPUTOPFILL.type = 10;

            IntPtr ptrExInfo = Marshal.StringToHGlobalAnsi(unitIdentification);
            INPUTOPFILL.exInfo = ptrExInfo;


            short reply = CMFillCommand(hCon, FILL, ref INPUTOPFILL, ref ONLYRC, 5000);
            Marshal.FreeHGlobal(ptrExInfo);

            return CommandReply(ONLYRC.rc);
        }

        public string InitCassette(string cassetteCode, char side = 'R', string password = "123456")
        {
            string cassette = cassetteCode.Substring(0, 1);
            cassetteCode = cassette.PadRight(4, Convert.ToChar(cassetteCode.Substring(0, 1)));
            INPUTOPINT.side = (byte)side;
            INPUTOPINT.password = password;
            INPUTOPINT.denom = cassetteCode;
            unsafe
            {
                IInitMode mode;
                mode.inote = 1;
                IInitMode* ptrMode = &mode;
                INPUTOPINT.mode = (IntPtr)ptrMode;
            }
            CMEmptyCassette(hCon, INIT, ref INPUTOPINT, ref OUTOPINIT, 5000);
            return CommandReply(OUTOPINIT.rc);
        }

        public string EmptyCassette(string cassetteCode, char side = 'R', short maxNote=200, string password = "123456")
        {
            string cassette = cassetteCode.Substring(0, 1);
            cassetteCode = cassette.PadRight(4, Convert.ToChar(cassetteCode.Substring(0, 1)));
            INPUTOPINT.side = (byte)side;
            INPUTOPINT.password = password;
            INPUTOPINT.denom = cassetteCode;
            unsafe
            {
                short _bundle = 200;
                short* bundle = &_bundle;
                IInitMode mode;
                mode.bundle = (IntPtr)bundle;
                mode.inote = maxNote;
                IInitMode* ptrMode = &mode;
                INPUTOPINT.mode = (IntPtr)ptrMode;
            }
            CMEmptyCassette(hCon, INIT, ref INPUTOPINT, ref OUTOPINIT, 5000);
            return CommandReply(OUTOPINIT.rc);
        }

        public string GetModuleIdentification(char module, short exInfo=99)
        {
            INPUTOPTEST.module = (byte)module;
            INPUTOPTEST.type = 13;
            unsafe
            {
                if (exInfo != 99)
                {
                    short* p = &exInfo;
                    INPUTOPTEST.exInfo = (IntPtr)p;
                }
            }
            CMGetMachineIdentification(hCon, TEST, ref INPUTOPTEST, ref OUTOPTnX13, 5000);
            return CommandReply(OUTOPTnX13.rc);
        }

        public string GetMachineIdentification()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 13;
            CMGetMachineIdentification(hCon, TEST, ref INPUTOPTEST, ref OUTOPTnX13, 5000);
            return CommandReply(OUTOPTnX13.rc);
        }

        public string GetUnitProtocolIdentification()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 1;
            CMGetUnitProtocolIdentification(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn01, 5000);
            return CommandReply(OUTOPTn01.rc);
        }

        /// <summary>
        /// Get the firmware version of the internal modules. It fills the structure OUTOPVERSION and return the replycode description
        /// </summary>
        /// <param name="module">0=MainControllerCompatibility - 1=Feeder - 2=MainController - 3=Reader - 4=SafeController - 5=FPGA - 6=OSC - A/L=Cassettes -a/b=Bag - w/z=Tower</param>
        /// <returns></returns>
        /// 
        public string GetModulesVersion(char module)
        {
            short target = (byte)module;
            CMGetModulesVersion(hCon, VERSION, ref target, ref OUTOPVERSION, 5000);
            return CommandReply(OUTOPVERSION.rc);
        } //2.15.1

        public string GetReaderReferences()
        {
            INPUTOPTEST.module = (byte)'3';
            INPUTOPTEST.type = 5;
            CMGetReaderReferences(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn35, 5000);
            return CommandReply(OUTOPTn35.rc);
        }

        public string GetReaderActiveBanksConfiguration()
        {
            INPUTOPTEST.module = (byte)'3';
            INPUTOPTEST.type = 4;
            CMGetReaderActiveBanks(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn34, 5000);
            return CommandReply(OUTOPTn34.rc);
        }

        public string GetReaderActiveBanks()
        {
            INPUTOPTEST.module = (byte)'3';
            INPUTOPTEST.type = 3;
            CMTestCommand(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn660, 5000);
            return CommandReply(OUTOPTn660.rc);
        }

        public string GetReaderBanks()
        {
            INPUTOPTEST.module = (byte)'3';
            INPUTOPTEST.type = 1;
            CMGetReaderBanks(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn31, 5000);
            return CommandReply(OUTOPTn31.rc);
        }

        /// <summary>
        /// 0=closed,empty,off or notPresent. 
        /// </summary>
        /// <returns></returns>
        public string UnitCoverTest()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 0;
            CMUnitCoverTest(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn08, 5000);
            return CommandReply(OUTOPTn08.rc);
        }
        public string UnitCoverTestAndOperatorLight()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 8;
            CMUnitCoverTest(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn08, 5000);
            return CommandReply(OUTOPTn08.rc);
        }
        public string ExtendedStatus()
        {
            CMExtendedStatus(hCon, EXTENDED_STATUS, ref StructureNull, ref OUTOPEXSTATUS, 5000);
            //GetBuffTrace();
            return CommandReply(OUTOPEXSTATUS.rc);
        }

        public string GetCDFFileName()
        {
            INPUTOPTEST.module = (byte)'3';
            INPUTOPTEST.type = 21;
            CMGetCDFName(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn321, 5000);
            return CommandReply(OUTOPTn321.rc);
        }

        /// <summary>
        /// Get the statistics, by reasons, for notes processed and rejected during a deposit or count transaction. Fille the structure OUTOPTn32
        /// </summary>
        /// <returns></returns>
        public string GetReaderShortStatistics()
        {
            INPUTOPTEST.module = (byte)'3';
            INPUTOPTEST.type = 2;
            CMGetReaderShortStatistics(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn32, 5000);
            return CommandReply(OUTOPTn32.rc);
        } //2.14.1

        public string GetCMDateTime()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 10;
            CMGetCMDateTime(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn010, 5000);
            return CommandReply(OUTOPTn010.rc);
        } //2.11.10

        /// <summary>
        /// Get the first group of configuration bytes. Fills the structure OUTOPTn015
        /// </summary>
        /// <returns></returns>
        public string GetCMConfig()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 15;
            CMGetCMConfig(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn015, 5000);
            return CommandReply(OUTOPTn015.rc);
        } //2.11.1

        /// <summary>
        /// Get the second group of configuration bytes. Fills the structure OUTOPTn028
        /// </summary>
        /// <returns></returns>
        public string GetCMOptionConfig()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 28;
            CMGetCMConfig(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn028, 5000);
            return CommandReply(OUTOPTn028.rc);
        } //2.11.2

        /// <summary>
        /// Get the third group of configuration bytes. Fills the structure OUTOPTn032
        /// </summary>
        /// <returns></returns>
        public string GetCMOptionOneConfig()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 32;
            CMGetCMConfig(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn032, 5000);
            return CommandReply(OUTOPTn032.rc);
        } //2.11.3

        /// <summary>
        /// Get the fourth group of configuration bytes. Fills the structure OUTOPTn049
        /// </summary>
        /// <returns></returns>
        public string GetCMOptionTwoConfig()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 49;
            CMGetCMConfig(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn049, 5000);
            return CommandReply(OUTOPTn015.rc);
        } //2.11.4

        /// <summary>
        /// Send a command in transparent mode. the transparemt mode must to be activated before. The answer will be in the structure OUTTRANSP and returns the connection replycode description.
        /// </summary>
        /// <param name="command">Transparent command to send</param>
        /// <returns></returns>
        public string TransparentCommand(string command, int timeout=1000)
        {
            utils.WaitinigTime(500);
            INTRANSP = new OpTransp();
            OUTTRANSP = new OpTransp();
            INTRANSP.buff = command;
            INTRANSP.size = command.Length;
            OUTTRANSP.buff = new string((char)0, 1700);
            OUTTRANSP.size = OUTTRANSP.buff.Length;
            short i  = CMTransparentCommand(hCon, TRANSPARENT, ref INTRANSP, ref OUTTRANSP, timeout);
            OUTTRANSP.size = OUTTRANSP.buff.Length;
            return OUTTRANSP.buff;// ConnectionReply(i);
        }

        /// <summary>
        /// Set the Transparent mode to ON. It fills the structure OUTOPTRASPMODE and return the reply code description
        /// </summary>
        /// <returns></returns>
        public string TransparentON()
        {
            CMTransparentOn(hCon, TRANSPARENT_START, ref StructureNull, ref OUTOPTRASPMODE, 5000);
            return CommandReply(OUTOPTRASPMODE.rc);
        }

        /// <summary>
        /// Set the Transparent mode to OFF. It fills the structure OUTOPTRASPMODE and return the reply code description
        /// </summary>
        /// <returns></returns>
        public string TransparentOFF()
        {
            CMTransparentOff(hCon, TRANSPARENT_END, ref StructureNull, ref OUTOPTRASPMODE, 5000);
            return CommandReply(OUTOPTRASPMODE.rc);
        }

        /// <summary>
        /// Get the name of the firmware complete suite. It fills the structure OUTOPTn044 and return the replycode description
        /// </summary>
        /// <returns></returns>
        public string GetFwSuiteIdentification()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 44;
            CMGetFwSuiteIdentification(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn044, 1000);
            return CommandReply(OUTOPTn044.rc);
        }
       
        /// <summary>
        /// Get the number of cassettes configured in the device. It fills the structure OUTOPTn061 and return the replycode description
        /// </summary>
        /// <returns></returns>
        public string GetCassetteNumber()
        {
            INPUTOPTEST.module = (byte)'0';
            INPUTOPTEST.type = 61;
            CMTestCommand(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn061, 10000);
            return CommandReply(OUTOPTn061.rc);
        }
       
        /// <summary>
        /// Bind the cassette to its physical positio. The cassettes must be empty of notes. It fills the structure OUTOPTn660 and return the replycode description
        /// </summary>
        /// <returns></returns>
        public string Assign()
        {
            CMAssign(hCon, AUTOASSIGNCASSETTES, ref StructureNull, ref OUTOPTn660, 10000);
            return CommandReply(OUTOPTn660.rc);
        } //2.18.1

        struct prova
        {
            public int a;
            public int b;
        }

        public string CommandReplySingle(string answer, int rc)
        {
            if (answer.Length == 0)
                return "NOANSWER";

            singleCommandAnswer = answer.Split(',');

            if (singleCommandAnswer.Count() == 1)
                return "NOANSER";


            return CommandReply(Convert.ToInt16(singleCommandAnswer[rc]));
        }
        public string PhotoAdjustAll(int phID = 0)
        {

            string reply = CommandReplySingle(SingleCommand("T,1,0,60,0,0", 5000),5);
            return reply;


            //INPUTOPTEST.module = (byte)'0';
            //INPUTOPTEST.type = 60;

            //prova myTest = new prova();
            //myTest.a = 0;
            //myTest.b = 0;
            //unsafe
            //{
            //    prova* ptr = &myTest;
            //    INPUTOPTEST.exInfo = (IntPtr)ptr;
            //}

            //int reply = CMTestCommand(hCon, TEST, ref INPUTOPTEST, ref ONLYRC, 5000);
            
            // OUTSINGLE.buff
            
        }

        public string PhotoSensorRead()
        {
            string reply = CommandReplySingle(SingleCommand("T,1,0,60,1", 5000), 5);
            return reply;

            //INPUTOPTEST.module = (byte)'0';
            //INPUTOPTEST.type = 60;

            //short exInfo = 1;
            //unsafe
            //{
            //    short* ptr = &exInfo;
            //    INPUTOPTEST.exInfo = (IntPtr)ptr;
            //}

            //int result = CMPhotoSensorsRead(hCon, TEST, ref INPUTOPTEST, ref OUTOPTn0601, 5000);
            //return CommandReply(OUTOPTn0601.rc);
        }



        public string WaitRecovery()
        {
            Stopwatch myTimer = new Stopwatch();
            Application.DoEvents();
            myTimer.Start();
            string result = Open('L');
            while (result != "OK" && result != "WRONG SIDE" && result != "BUSY")
            {
                utils.WaitinigTime(500);
                result = Open('L');
                if (myTimer.Elapsed > TimeSpan.FromSeconds(30))
                {
                    result = "FAIL";
                    goto endWaiting;
                }
            }
            myTimer.Stop();
            myTimer.Reset();
            myTimer.Start();
            while (result == "BUSY")
            {
                utils.WaitinigTime(500);
                result = Open('L');
                if (myTimer.Elapsed > TimeSpan.FromSeconds(60))
                {
                    result = "FAIL";
                    goto endWaiting;
                }
            }
            result = "OK";
        endWaiting:
            myTimer.Stop();
            return result;
        }
        /// <summary>
        /// Get information about the CM device model and configuration. It fills the structure OUTOPTnX13 and return the replycode description
        /// </summary>
        /// <returns></returns>
        public string GetMainControllerModuleName()
        {
            INPUTOPTEST.module = (byte)'2';
            INPUTOPTEST.type = 13;
            CMGetMainCtrlModuleName(hCon, TEST, ref INPUTOPTEST, ref OUTOPTnX13, 1000);
            return CommandReply(OUTOPTnX13.rc);
        } //2.15.4
        
        /// <summary>
        /// Start the deposit operation. It fills the structure OUTOPDEPOSIT and return the replycode description
        /// </summary>
        /// <param name="side">The side opened before (L/R)</param>
        /// <param name="mode">0=Deposit using either active currency or currency specified by first note in denomination</param>
        /// <returns></returns>
        public string Deposit(char side = 'R', short mode = 0)
        {
            INPUTOPDEPOSIT.side = (byte)side;
            INPUTOPDEPOSIT.mode = mode;
            INPUTOPDEPOSIT.denom = "0000";
            CMDeposit(hCon, DEPOSIT_CASH, ref INPUTOPDEPOSIT, ref OUTOPDEPOSIT, 60000);
            return CommandReply(OUTOPDEPOSIT.rc);
        } //2.4.1
        
        /// <summary>
        /// Start the withdrawal operation. It fills the structure OUTOPWITHDRAW and return the replycode description
        /// </summary>
        /// <param name="side"></param>
        /// <param name="param">InfoNotes structure or DENOM(4char), Number(max200) for each denomination </param>
        /// <returns></returns>
        public string Withdrawal(char side = 'R', params object[] param)
        {
            INPUTOPWITHDRAW.side = (byte)side;
            INPUTOPWITHDRAW.numItem = 1;
            InfoNotes[] notes = new InfoNotes[MAX_CASSETTES];
            notes[0].denom = "CPEA";
            notes[0].num = 2;
            INPUTOPWITHDRAW.info = notes;
            CMWithdrawal(hCon, WITHDRAWAL, ref INPUTOPWITHDRAW, ref OUTOPWITHDRAW, 60000);
            return CommandReply(OUTOPWITHDRAW.rc);
        } //2.5.1

        /// <summary>
        /// Get the details of note stock physically present into each cassette. It fills the structure OUTOPGETCASH and return the replycode description
        /// </summary>
        /// <param name="side">Operator side opened before</param>
        /// <returns></returns>
        public string GetCashData(char side = 'R')
        {
            INOPGETCASH.side = (byte)side;
            CMGetCashData(hCon, GET_CASH_DATA, ref INOPGETCASH, ref OUTOPGETCASH, 5000);
            return CommandReply(OUTOPGETCASH.rc);
        } //2.8.1

        /// <summary>
        /// Release the control of the CM device. It must be sent after a Open command. It fills the structure OUTOPCLOSE and return the replycode description
        /// </summary>
        /// <param name="side">Operator side opened before</param>
        /// <returns></returns>
        public string Close(char side = 'R')
        {
            INOPCLOSE.side = Convert.ToByte(side);
            CMClose(hCon, CLOSE, ref INOPCLOSE, ref OUTOPCLOSE, 5000);
            return CommandReply(OUTOPCLOSE.rc);
        } //2.1.3

        /// <summary>
        /// Reserve the use of the CM cash recycler. It fills the structure OUTOPOPEN and return the replycode description
        /// </summary>
        /// <param name="side"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Open(char side, string password = "123456")
        {
            INPUTOPOPEN.side = (byte)side;
            INPUTOPOPEN.password = password;
            INPUTOPOPEN.opt = null;
            comScopeSend =((char)OPEN).ToString() + ",n," + side.ToString() + "," + password;
            //OutputOpen.rc = -1;
            cmdOpen(hCon, OPEN, ref INPUTOPOPEN, ref OUTOPOPEN, 5000);
            comScopeReceive = ((char)OPEN).ToString() + ",n," + ((char)OUTOPOPEN.side).ToString() + "," + OUTOPOPEN.rc;
            return CommandReply(OUTOPOPEN.rc);
        } //2.1.1

        /// <summary>
        /// It must be invokd in order to establish a physical connection to the target CM device. It returns the connection reply code
        /// </summary>
        /// <param name="param">Parameters used for the connection</param>
        /// <returns></returns>
        public string Connect(ref ConnectionParam param)
        {
            return ConnectionReply(CMConnect(ref hCon, ref param));
        } //1.1.1

        /// <summary>
        /// It must be invoked in order to close the physical connectio to the target CM device. It returns the disconnection reply code
        /// </summary>
        /// <returns></returns>
        public string Disconnect()
        {
            return ConnectionReply(CMDisconnect(hCon));
        } //1.1.2

        /// <summary>
        /// Dispensing of all notes deposited in the last depoosit operation
        /// </summary>
        /// <param name="side">Operator side opened before</param>
        /// <returns></returns>
        public string Undo(char side = 'R')
        {
            INPUTOPUNDO.side = (byte)side;
            cmdUndo(hCon, UNDO, ref INPUTOPUNDO, ref OUTOPUNDO, 112000);
            return CommandReply(OUTOPOPEN.rc);
        } //2.4.2

        /// <summary>
        /// Returns the description of connection/disconnection reply code
        /// </summary>
        /// <param name="replyCode">reply code to interpretate</param>
        /// <returns></returns>
        string ConnectionReply(int replyCode)
        {
            string result = "";
            switch (replyCode)
            {
                case CM_OK:// Command correctly executed
                    result = "OK";
                    break;
                case CM_LINK_SYSTEM_ERR:   //"CMLINK layer -Function communication device error"
                    result = "Communication device error";
                    break;
                case CM_LINK_CONNECTION_ERR:   //"CMLINK layer -Invalid handle"
                    result = "Invalid Handle";
                    break;
                case CM_LINK_ALREADY_CONNECTED_ERR:   //"CMLINK layer -Connection already connected"
                    result = "Connection Already Connected";
                    break;
                case CM_LINK_CONNECT_PARAMETERS_ERR:   //"CMLINK layer -Syntax error on connect operation"
                    result = "Systax Error";
                    break;
                case CM_LINK_WRITE_ERR:   //"CMLINK layer -WriteFile Function on device has failed"
                    result = "Writefile error";
                    break;
                case CM_LINK_READ_ERR:   //"CMLINK layer -ReadFile Function on device has failed"
                    result = "Readfile error";
                    break;
                case CM_LINK_LAN_OPEN_ERR:   //"CMLINK layer -Error on TCP/IP socket connection routines"
                    result = "TCP/IP Error";
                    break;
                case CM_LINK_LAN_WRITE_ERR:   //"CMLINK layer -Write on LAN has failed"
                    result = "WriteLan Error";
                    break;
                case CM_LINK_LAN_READ_ERR:   //"CMLINK layer -Read on LAN has failed"
                    result = "ReadLan Error";
                    break;
                case CM_LINK_TIMEOUT_ERR:   //"CMLINK layer -Time command has expired"
                    result = "Timeout expired";
                    break;
                case CM_LINK_OVERFLOW_ERR:   //"CMLINK layer -Buffer of reply command has been exeed"
                    result = "Reply too long";
                    break;
                case CM_LINK_BCC_ERR:   //"CMLINK layer -Trasmission error, wrong BCC/char"
                    result = "Wrong BCC";
                    break;
                case CM_LINK_PROTOCOL_ERR:   //"CMLINK layer -Trasmission error, wrong protocol "
                    result = "Wrong Protocol";
                    break;
                case CM_LINK_CDM_IN_PROGRESS:   //"CMLINK layer -Command in progress"
                case CM_LINK_TERMINATE_EMULATOR:   //"CMLINK layer -Command in progress"
                case CM_LINK_SEQUENCE_ERR:   //"CMLINK layer -Command in progress"
                    result = "Command in progress";
                    break;
                case CM_LINK_LAN_CONNECTION_REFUSED:   //"CMLINK layer -LAN Connection refused 10061"
                    result = "Lan Connection Refused";
                    break;
                case CM_LINK_SYNTAX_ERR:   //"CMLINK Syntax Error"
                    result = "Syntax Error";
                    break;
                case CM_LINK_LAN_CONNECTION_RESET:   //"CMLINK layer -LAN 10054 - Connection reset by peer"
                    result = "Connection reset by peer";
                    break;
                case CM_LINK_LANSERVER_CLIENT_CLOSE:   //"CMLINK layer Close session by client in LanServer"
                    result = "Closed by client";
                    break;
                case CM_COMMAND_SYNTAXERROR:   // "CM_COMMAND layer -Error in input parameter"
                    result = "Input Parameter Error";
                    break;
                case CM_COMMAND_INVALID_REPLY_PARAMETER:   // "CM_COMMAND layer -Output parameter cannot be NULL"
                    result = "Output Parameter Error";
                    break;
                case CM_COMMAND_UNEXPECTEDREPLY:   // "CM_COMMAND layer -Error in input parameter"
                    result = "Wrong reply";
                    break;
                case CM_COMMAND_REFUSED:   // "CM_COMMAND layer -"
                    result = "Command refused";
                    break;
                case CM_COMMAND_INVALID_CONNECT_HANDLE:	//"CM_COMMAND layer -System Error"
                    result = "Invalid Connect Handle";
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the description of command reply code
        /// </summary>
        /// <param name="replyCode">reply code to interpretate</param>
        /// <returns></returns>
        string CommandReply(int replyCode)
        {
            return replyIni.GetValue("RC", replyCode.ToString());
        }

        //-------------------------------------------------NON FUNZIONANO--------------------------------------------------------------------------- 

        

        public string SetBarcodeCD80(string mySerial, char module = 'A')
        {
            INPUTOPTEST.module = (byte)module;
            INPUTOPTEST.type = 4;
            //unsafe
            //{
            //    int* ptr = &mySerial;
            //}
            //INPUTOPTEST.exInfo = &serial.sn;

            CMSetBarcodeCD80(hCon, MOD_FILL, ref INPUTOPTEST, ref ONLYRC, 5000);
            return CommandReply(ONLYRC.rc);
        }



        public string SetCMDateTime(DateTime dt)
        {

            DATATIMECLOCK.week = ((int)dt.DayOfWeek).ToString("00");
            DATATIMECLOCK.day = dt.ToString("dd"); ;
            DATATIMECLOCK.year = dt.ToString("yy");
            DATATIMECLOCK.month = dt.ToString("MM");
            DATATIMECLOCK.hour = dt.ToString("HH");
            DATATIMECLOCK.min = dt.ToString("mm");
            DATATIMECLOCK.sec = dt.ToString("ss");
            INPUTOPFn4.cc = "00";
            INPUTOPFn4.dataTimeClock = DATATIMECLOCK;

            int sizePtr = Marshal.SizeOf(typeof(IntPtr));

            IntPtr ptrExInfo = Marshal.AllocHGlobal(sizePtr);
            Marshal.StructureToPtr(INPUTOPFn4, ptrExInfo, false);
            INPUTOPFILL.exInfo = ptrExInfo;
            INPUTOPFILL.type = 4;

            int reply = CMSetCMDateTime(hCon, FILL, ref INPUTOPFILL, ref ONLYRC, 5000);
            if (reply != 0)
                return ConnectionReply(reply);

            return CommandReply(ONLYRC.rc);
        } //2.11.11

        //public string GetCassetteConfiguration(char side = 'R')
        //{
        //    INPUTOPGETCONFIG.side = (byte)side;
        //    unsafe
        //    {
        //        short bank = 0;
        //        short* ptrBank = &bank;
        //        INPUTOPGETCONFIG.bank = (IntPtr)ptrBank;
        //    }

        //    CMGetCassetteConfiguration(hCon, GET_CONFIG, ref INPUTOPGETCONFIG, ref OUTOPGETCONFIG, 5000);
        //    return CommandReply(OUTOPGETCONFIG.rc);
        //}

        





        #endregion

    }

    public class CMStructures
    {
         public struct SCM18
        {
            public string code;
            public string family;
            public string description;
            public string identification;
            public SCLIENT client;
            public SCMREADER reader;
            public STEST test;
            public SDBFW dbfw;
            public SBAG bag;
            public SSafe safe;
            public SRealTime realTime;
            public SConnection connection;
            public SOptions options;
        }

        public struct SRealTime
        {
            public SPHOTO shift;
            public SPHOTO inq;
            public SPHOTO count;
            public SPHOTO c4A;
            public SPHOTO c4B;
            public SPHOTO c3;
            public SPHOTO reject;
            public SPHOTO inCenter;
            public SPHOTO feed;
            public SPHOTO feed2;
            public SPHOTO c1;
            public SPHOTO inLeft;
            public SPHOTO output;
            public SPHOTO hMax;
            public SPHOTO inBox;
        }
        

        public struct SSafe
        {
            public SPHOTO cashLeft;
            public SPHOTO cashRight;
            public SPHOTO inCD80;
            public SPHOTO thick;
            public SPHOTO inRcyc;
            public SPHOTO endV;
        }
        public struct SOptions
        {
            public int shiftCenter;
        }
        public struct SConnection
        {
            public string mode;
            public bool simplified;
            public bool transparentMode;
            public bool connected;
            public bool reader;
        }
        public struct SBAG
        {
            public SPHOTOBAG photo;
            public SFWIRMWARE fwMBAG;
            public SFWIRMWARE fwEMFU;
        }
        public struct SPHOTOBAG
        {
            public SPHOTO transport;
            public SPHOTO notesInLift;
            public SPHOTO ir1EmptyCalib;
            public SPHOTO ir2EmptyCalib;
            public SPHOTO ir1NotEmptyCalib;
            public SPHOTO ir2NotEmptyCalib;
        }

        public struct SFWIRMWARE
        {
            public string name;
            public string version;
            public string release;
        }
        public struct SPHOTO
        {
            public string name;
            public int value;
            public int maxValue;
            public int minValue;
        }

        
        public struct SCLIENT
        {
            public string name;
            public int id;
        }
        public struct SDBFW
        {
            public SCMREADER reader;
            public int unitConfiguration;
            public int CD80;
            public bool CRM;
        }

        public struct SMODULE
        {
            public string code;
            public string serialNumber;
            
        }
        public struct STEST
        {
            public string dateStart;
            public string timeStart;
            public string timeEnd;
            public string cmCommandVer;
            public string cmLinkVer;
            public string cmTraceVer;
            public string systemSN;
            public string readerSN;
            public string controllerSN;
        }

        public struct SCMREADER
        {
            public string code;
            public string model;
            public string cdf;
            public string fwCode;
            public string fwCodeField;
            public string flagProgress;
            public string serialNumber;
        }
    }
    public class Utils
    {
        public const string ENTER = "ENTER";
        public const string ESC = "ESC";
        public int keyCodePressed = 0;
        public string keyPressed = "";

        /// <summary>
        /// Convert an int number to a hex value
        /// </summary>
        /// <param name="value">number to convert in hex value</param>
        /// <param name="nDigit">number of min hex digit</param>
        /// <returns></returns>
        public string DecToHex(int value, int nDigit=0)
        {
            if (nDigit == 4)
                return value.ToString("X4");
            if (nDigit == 8)
                return value.ToString("X8");
            if (nDigit == 16)
                return value.ToString("X16");
            return value.ToString("X");
        }

        /// <summary>
        /// Convert a double number to a hex value
        /// </summary>
        /// <param name="value">number to convert in hex value</param>
        /// <param name="nDigit">number of min hex digit</param>
        /// <returns></returns>
        public string DecToHex(double value, int nDigit = 0)
        {
            string result = "";
            double tmp = 0;
            int rst = 0;
            while (value > 0)
            {
                tmp = Math.Truncate(value / 16);
                rst = (int)(value - (tmp * 16));
                value = tmp;
                result = DecToHex(rst) + result;
            }
            if (nDigit > result.Length)
            {
                string pad = new string('0', nDigit - result.Length);
                result = pad + result;
            }
            return result;
        }

        /// <summary>
        /// Convert a hex value in a int number
        /// </summary>
        /// <param name="value">value to convert in nuumber</param>
        /// <returns></returns>
        public int HexToDec(string value)
        {
            return int.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// Convert a hex value in a double number
        /// </summary>
        /// <param name="value">value to convert in number</param>
        /// <returns></returns>
        public double HexToDouble(string value)
        {
            double result = 0;
            for (int i = value.Length-1; i>=0;i--)
                result += HexToDec(value.Substring(i, 1)) * Math.Pow(16,(value.Length-i-1));
            return result;
        }

        /// <summary>
        /// Convert a single byte value in an ascii character
        /// </summary>
        /// <param name="value">single byte value to convert in char</param>
        /// <returns></returns>
        public string ByteToAscii(byte value)
        {
            return ((char)value).ToString();
        }

        /// <summary>
        /// Convert a hex value in an ascii string
        /// </summary>
        /// <param name="value">value to convert in ascii string</param>
        /// <returns></returns>
        public string HexToAscii(string value)
        {
            string result = "";
            if (value.Length == 1)
                value = "0" + value;
            for (int i = 0; i < value.Length; i+=2)
            {
                result += ((char)int.Parse(value.Substring(i,2), System.Globalization.NumberStyles.HexNumber)).ToString();
            }
            return result;
        }

        public string AsciiToHex(string value)
        {
            string result="";
            byte[] valueBytes = Encoding.Default.GetBytes(value);
            for (int i = 0; i < valueBytes.Length; i++)
                result += DecToHex(valueBytes[i]).ToString();
            return result;
        }


        /// <summary>
        /// Convert a int number in a binary value
        /// </summary>
        /// <param name="value">number to convert in binary</param>
        /// <param name="nDigit">number of min bin digit</param>
        /// <returns></returns>
        public string DecToBin(int value, int nDigit = 0)
        {
            string result = "";
            int tmp = 0;
            int rst = 0;
            while (value > 0)
            {
                tmp = value / 2;
                rst = (int)(value - (tmp * 2));
                value = tmp;
                result = rst + result;
            }
            if (nDigit > result.Length)
            {
                string pad = new string('0', nDigit - result.Length);
                result = pad + result;
            }
            return result;
        }

        /// <summary>
        /// Convert a binary value in a int number
        /// </summary>
        /// <param name="value">binary value to convert in number</param>
        /// <returns></returns>
        public int BinToDec(string value)
        {
            int result = 0;
            for (int i = value.Length - 1; i >= 0; i--)
                result += Convert.ToInt16(value.Substring(i,1)) * (int)Math.Pow(2, (value.Length - i - 1));
            return result;
        }

        /// <summary>
        /// Convert a binary value in a double number
        /// </summary>
        /// <param name="value">binary value to convert in number</param>
        /// <returns></returns>
        public double BinToDbl(string value)
        {
            double result = 0;
            for (int i = value.Length - 1; i >= 0; i--)
                result += Convert.ToInt16(value.Substring(i, 1)) * (int)Math.Pow(2, (value.Length - i - 1));
            return result;
        }

        /// <summary>
        /// Convert a hex value in a binary value
        /// </summary>
        /// <param name="value">hex value to convert in binary</param>
        /// <returns></returns>
        public string HexToBin(string value)
        {
            int tmp = 0;
            tmp = HexToDec(value);
            return DecToBin(tmp);
        }

        /// <summary>
        /// Conver a bin value in a hex value
        /// </summary>
        /// <param name="value">binary value to convert in hex </param>
        /// <returns></returns>
        public string BinToHex(string value)
        {
            double tmp = 0;
            tmp = BinToDbl(value);
            return DecToHex(tmp);
        }

        /// <summary>
        /// Convert a decimal ascii code sequence in a ascii string
        /// </summary>
        /// <param name="value">decimal ascii code sequence to convert in string</param>
        /// <returns></returns>
        public string DecAsciiToAscii(string value)
        {
            //result += (char)Convert.ToInt16(value.Substring(i, 2));
            string result = "";
            int tmp = 0;
            string hex = "";
            for (int i = 0; i < value.Length; i += 2)
            {
                result += HexToAscii(value.Substring(i, 2));
            }
            return result;
        }

        /// <summary>
        /// Get the even character from a string. ex: 04050109->4519
        /// </summary>
        /// <param name="value">string to convert</param>
        /// <returns></returns>
        public string GetEvenChars(string value)
        {
            string result = "";
            //if (value.Length % 2 != 0)
            //    value = "0" + value;
            for (int i = 0; i<value.Length; i+=2)
            {
                result += value.Substring(i + 1, 1);
            }
            return result;
        }

        /// <summary>
        /// Set odd character to a string. ex: 4519->04050109
        /// </summary>
        /// <param name="value">string to elaborating</param>
        /// <param name="chr">character di add in the odd position</param>
        /// <returns></returns>
        public string SetOddChars(string value, char chr)
        {
            string result = "";
            for (int i = 0; i < value.Length; i++)
                result += ByteToAscii((byte)chr) + value.Substring(i, 1);
            return result;
        }

        public string WaitingKey(string specificKey = "")
        {
            start:
            keyCodePressed = 0;
            while (keyCodePressed == 0)
            {
                Application.DoEvents();
            }
            switch(keyCodePressed)
            {
                case 13:
                    keyPressed = ENTER;
                    break;
                case 27:
                    keyPressed = ESC;
                    break;
                default:
                    keyPressed = ((char)keyCodePressed).ToString();
                    break;
            }
            if (specificKey=="")
                return keyPressed.ToString();
            if (keyPressed != specificKey)
                goto start;
            return keyPressed.ToString();
        }

        public void WaitinigTime(int mSec)
        {
            Stopwatch myTimer = new Stopwatch();
            myTimer.Start();
            while (myTimer.Elapsed < TimeSpan.FromMilliseconds(mSec))
            {
                Application.DoEvents();
            }
            myTimer.Stop();
        }
    }
    public class ArcaMessages
    {
        Control lblMessage;
        Control lblTitle;
        Form frmMain;
        List<string> msg;

        public Color backgroundColorError = Color.Red;
        public Color backgroundColorWarning = Color.Fuchsia;
        public Color backgroundColorNormal = Color.FromArgb(88,89,91);
        public Color textColorTitle = Color.White;
        public Color textColorMessage = Color.White;
        public ArcaMessages(Control labelMessage, Form formMain, List<string> messages = null, Control labelTitle = null)
        {
            lblMessage = labelMessage;
            msg = messages;
            frmMain = formMain;
            lblMessage.ForeColor = textColorMessage;
            if (labelTitle != null)
            {
                lblTitle = labelTitle;
                lblTitle.ForeColor = textColorTitle;
            }
        }

        public void Warning(string message)
        {
            frmMain.BackColor = backgroundColorWarning;
            lblMessage.Text = message;
        }
        public void Warning(int messageID)
        {
            frmMain.BackColor = backgroundColorWarning;
            try
            {
                lblMessage.Text = msg[messageID];
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void Warning(string message, params object[] param)
        {
            frmMain.BackColor = backgroundColorWarning;
            lblMessage.Text = string.Format(message, param);
        }
        public void Warning(int messageID, params object[] param)
        {
            frmMain.BackColor = backgroundColorWarning;
            try
            {
                lblMessage.Text = string.Format(msg[messageID], param);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void Error(string message)
        {
            frmMain.BackColor = backgroundColorError;
            lblMessage.Text = message;
        }
        public void Error(int messageID)
        {
            frmMain.BackColor = backgroundColorError;
            try
            {
                lblMessage.Text = msg[messageID];
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void Error(string message, params object[] param)
        {
            frmMain.BackColor = backgroundColorError;
            lblMessage.Text = string.Format(message, param);
        }
        public void Error(int messageID, params object[] param)
        {
            frmMain.BackColor = backgroundColorError;
            try
            {
                lblMessage.Text = string.Format(msg[messageID], param);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        public void Normal(string message)
        {
            frmMain.BackColor = backgroundColorNormal;
            lblMessage.Text = message;
        }
        public void Normal(int messageID)
        {
            frmMain.BackColor = backgroundColorNormal;
            try
            {
                lblMessage.Text = msg[messageID];
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void Normal(string message, params object[] param)
        {
            frmMain.BackColor = backgroundColorNormal;
            lblMessage.Text = string.Format(message,param);
        }
        public void Normal(int messageID, params object[] param)
        {
            frmMain.BackColor = backgroundColorNormal;
            try
            {
                lblMessage.Text = string.Format(msg[messageID], param);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        public void Title(string title)
        {
            if (lblTitle != null)
            {
                frmMain.BackColor = backgroundColorNormal;
                lblTitle.Text = title;
            }
        }
        public void Title(int titleID)
        {
            if (lblTitle != null)
            {
                frmMain.BackColor = backgroundColorNormal;
                try
                {
                    lblTitle.Text = msg[titleID];
                }
                catch (Exception ex)
                {
                    lblTitle.Text = ex.Message;
                }
            }
        }
        public void Title(string title, params object[] param)
        {
            if (lblTitle != null)
            {
                frmMain.BackColor = backgroundColorNormal;
                lblTitle.Text = string.Format(title, param);
            }
        }
        public void Title(int titleID, params object[] param)
        {
            if (lblTitle != null)
            {
                frmMain.BackColor = backgroundColorNormal;
                try
                {
                    lblTitle.Text = string.Format(msg[titleID], param);
                }
                catch (Exception ex)
                {
                    lblTitle.Text = ex.Message;
                }
            }
        }
    }

    public class IniFile
    {
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        public void SetValue(string section, string key, string value)
        {
            WritePrivateProfileString(section ?? EXE, key, value, theFile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFile"/> class.
        /// </summary>
        /// <param name="file">The initialization file path.</param>
        /// <param name="commentDelimiter">The comment delimiter string (default value is ";").
        /// </param>
        public IniFile(string file, string commentDelimiter = ";")
        {
            CommentDelimiter = commentDelimiter;
            TheFile = file;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFile"/> class.
        /// </summary>
        public IniFile()
        {
            CommentDelimiter = ";";
        }

        /// <summary>
        /// The comment delimiter string (default value is ";").
        /// </summary>
        public string CommentDelimiter { get; set; }

        private string theFile = null;

        /// <summary>
        /// The initialization file path.
        /// </summary>
        public string TheFile
        {
            get
            {
                return theFile;
            }
            set
            {
                theFile = null;
                dictionary.Clear();
                if (File.Exists(value))
                {
                    theFile = value;
                    using (StreamReader sr = new StreamReader(theFile))
                    {
                        string line, section = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.Length == 0) continue;  // empty line
                            if (!String.IsNullOrEmpty(CommentDelimiter) && line.StartsWith(CommentDelimiter))
                                continue;  // comment

                            if (line.StartsWith("[") && line.Contains("]"))  // [section]
                            {
                                int index = line.IndexOf(']');
                                section = line.Substring(1, index - 1).Trim();
                                continue;
                            }

                            if (line.Contains("="))  // key=value
                            {
                                int index = line.IndexOf('=');
                                string key = line.Substring(0, index).Trim();
                                string val = line.Substring(index + 1).Trim();
                                string key2 = String.Format("[{0}]{1}", section, key).ToLower();

                                if (val.StartsWith("\"") && val.EndsWith("\""))  // strip quotes
                                    val = val.Substring(1, val.Length - 2);

                                if (dictionary.ContainsKey(key2))  // multiple values can share the same key
                                {
                                    index = 1;
                                    string key3;
                                    while (true)
                                    {
                                        key3 = String.Format("{0}~{1}", key2, ++index);
                                        if (!dictionary.ContainsKey(key3))
                                        {
                                            dictionary.Add(key3, val);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    dictionary.Add(key2, val);
                                }
                            }
                        }
                    }
                }
            }
        }

        // "[section]key"   -> "value1"
        // "[section]key~2" -> "value2"
        // "[section]key~3" -> "value3"
        public  Dictionary<string, string> dictionary = new Dictionary<string, string>();

        private bool TryGetValue(string section, string key, out string value)
        {
            string key2;
            if (section.StartsWith("["))
                key2 = String.Format("{0}{1}", section, key);
            else
                key2 = String.Format("[{0}]{1}", section, key);

            return dictionary.TryGetValue(key2.ToLower(), out value);
        }

        /// <summary>
        /// Gets a string value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value.</returns>
        /// <seealso cref="GetAllValues"/>
        public string GetValue(string section, string key, string defaultValue = "")
        {
            string value;
            if (!TryGetValue(section, key, out value))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Gets a string value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        /// <seealso cref="GetValue"/>
        public string this[string section, string key]
        {
            get
            {
                return GetValue(section, key);
            }
        }

        /// <summary>
        /// Gets an integer value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="minValue">Optional minimum value to be enforced.</param>
        /// <param name="maxValue">Optional maximum value to be enforced.</param>
        /// <returns>The value.</returns>
        public int GetInteger(string section, string key, int defaultValue = 0,
            int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            int value;
            if (!int.TryParse(stringValue, out value))
            {
                double dvalue;
                if (!double.TryParse(stringValue, out dvalue))
                    return defaultValue;
                value = (int)dvalue;
            }

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        /// <summary>
        /// Gets a double floating-point value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="minValue">Optional minimum value to be enforced.</param>
        /// <param name="maxValue">Optional maximum value to be enforced.</param>
        /// <returns>The value.</returns>
        public double GetDouble(string section, string key, double defaultValue = 0,
            double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            double value;
            if (!double.TryParse(stringValue, out value))
                return defaultValue;

            if (value < minValue)
                value = minValue;
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        /// <summary>
        /// Gets a boolean value by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value.</returns>
        public bool GetBoolean(string section, string key, bool defaultValue = false)
        {
            string stringValue;
            if (!TryGetValue(section, key, out stringValue))
                return defaultValue;

            return (stringValue != "0" && !stringValue.StartsWith("f", true, null));
        }

        /// <summary>
        /// Gets an array of string values by section and key.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns>The array of values, or null if none found.</returns>
        /// <seealso cref="GetValue"/>
        public string[] GetAllValues(string section, string key)
        {
            string key2, key3, value;
            if (section.StartsWith("["))
                key2 = String.Format("{0}{1}", section, key).ToLower();
            else
                key2 = String.Format("[{0}]{1}", section, key).ToLower();

            if (!dictionary.TryGetValue(key2, out value))
                return null;

            List<string> values = new List<string>();
            values.Add(value);
            int index = 1;
            while (true)
            {
                key3 = String.Format("{0}~{1}", key2, ++index);
                if (!dictionary.TryGetValue(key3, out value))
                    break;
                values.Add(value);
            }

            return values.ToArray();
        }
    }

    #region Eccezioni 
    public class IniFileNotFoundException : Exception
    {
        public IniFileNotFoundException() { }
        public IniFileNotFoundException(string message) : base(message) { }
    }

    public class IniParameterValueInvalid : Exception
    {
        public IniParameterValueInvalid() { }
        public IniParameterValueInvalid(string message) : base(message) { }
    }
    #endregion

}
