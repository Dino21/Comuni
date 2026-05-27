using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Resources;
using System.Windows.Forms.VisualStyles;


namespace CM18tool
{
    public class RSDLL
    {
        [DllImport("OEMR_USB.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_OpenUsbConnection")]
        public static extern bool OEMR_OpenUsbConnection(int lpOUIO);

        [DllImport("OEMR_USB.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_CloseUsbConnection")]
        public static extern bool OEMR_CloseUsbConnection();

        [DllImport("OEMR_USB.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_SendUsbCommand")]
        public static extern bool OEMR_SendUsbCommand(byte[] comando, int nByte, int timeout);

        [DllImport("OEMR_USB.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "OEMR_ReadUsbDataStream")]
        public static extern bool OEMR_ReadUsbDataStream(byte endpoint, byte[] ricezione, int nByte, int timeout);

        public bool Connect(int lpOUIO = 0)
        {
            return OEMR_OpenUsbConnection(lpOUIO);
        }

        public bool Disconnect()
        {
            return OEMR_CloseUsbConnection();
        }

        public bool Command(string command, int nByte, int timeout = 6000) //string command, int nByte, int timeout=1000
        {
            bool reply = false;
            byte[] cmd = new byte[command.Length / 2];
            byte[] ricezione = new byte[nByte];
            for (int i = 0; i < command.Length / 2; i++)
                cmd[i] = byte.Parse(command.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);

            reply = OEMR_SendUsbCommand(cmd, cmd.Length, timeout);
            reply = OEMR_ReadUsbDataStream(0, ricezione, 1, 2000);
            return reply;
        }

    }
    public class CmDLLs
    {

        public event EventHandler CommandSent;
        public event EventHandler CommandReceived;

        #region CONSTANTS
        //CONSTANTS
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
        public const byte DLINK_MODE_USB_EASY = 117;    // connection using USB port with SIMPLIFIED protocol
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
        public const byte TRASPARENT_START = 88;     //'X'
        public const byte TRASPARENT_END = 89;     //'Y'
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

        #endregion

        #region VARIABLES
        public static short hCon;
        public static ConnectionParam ConnPar;
        public static short hTrace;
        public static string stTrace;
        public static UInt32 lastError;
        public static string commandSent;
        public string commandAns;
        public string[] commandSingleAns;
        public int n = 1;
        #endregion

        #region STRUCTURES

        //OPEN
        public struct IOpen
        {
            public byte side;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LEN_PASSWORD + 1)]
            public string password;
            public string opt;
        }
        public struct OOpen
        {
            public byte side;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_CHARDENOM + 1)]
            public string fwVersion;
            public Int16 rc;
        }


        public struct SProduct
        {

        }
        public static SProduct myProduct;

        public struct SReplyOpen
        {
            public string side;
            public string rc;
        }
        public static SReplyOpen replyOpen;

        public struct SCassetteInfo
        {
            public string noteId;
            public string bnNumber;
            public string bnFreeCap;
            public string name;
            public string enabled;
        }
        public static SCassetteInfo CassetteInfo;

        public struct SGetModuleInformation
        {
            public string rc;
            public string name;
            public string ver;
            public string rel;
            public string serialNumber;
        }
        public static SGetModuleInformation replyGetModuleInformation;

        public struct SLANSettings
        {
            public string rc;
            public string dhcpEnabled;
            public string ipAddress;
            public string subnetMask;
            public string gateway;
            public string deviceName;
            public string deviceDescription;
            public string primaryDNS;
            public string secondaryDNS;
            public string useNetBiosName;
        }
        public SLANSettings replyLanSettings;

        public struct SReaderConfiguration
        {
            public string rc;
            public SCDF cdf;
            public int activeBanks;
            public string currency;
            public string bankConfig;
            public string[] refBank;
            public int[] fitnessBank;
            public static SReaderConfiguration Create()
            {
                return new SReaderConfiguration
                {
                    rc = "",
                    cdf = SCDF.Create(),
                    activeBanks = 0,
                    currency = "",
                    bankConfig = "",
                    refBank = new string[4] {"","","",""},
                    fitnessBank = new int[4]
                };
            }

        }
        public SReaderConfiguration replyReaderConfiguration;

        public struct SCDF
        {
            public string code;
            public string internalName;
            public string[] currCode;
            public static SCDF Create()
            {
                var cdf = new SCDF
                {
                    code = "",
                    internalName = "",
                    currCode = new string[16]
                };
                for (int i = 0; i < cdf.currCode.Length; i++)
                    cdf.currCode[i] = "";
                return cdf;
            }
        }
        
        public struct SReferences
        {
            public string name;
            public string ver;
            public int bankId;
        }
        public struct SBanks
        {
            public string bankName;
            public int numBank;
            public string ver;
        }
        public struct SUnitConfig
        {
            public string rc;
            public string[] allCfg;
            public string cfg;
            public string optCfg;
            public string optOneCfg;
            public string optTwoCfg;
            public string optThreeCfg;
        }
        public SUnitConfig replyUnitConfig;

        public struct SGetCashData
        {
            public string side;
            public string rc;
            public SCassetteInfo[] CassetteInfo;
            public string numCasProt;
        }
        public static SGetCashData replyGetCashData;

        public struct SNotesInfo
        {
            public string denomination;
            public int bnNum;
        }
        public static SNotesInfo NotesInfo;

        public struct SDeposit
        {
            public string side;
            public string rc;
            public int bnTotal;
            public int bnOutput;
            public int bnRej;
            public SNotesInfo[] depositInfo;
        }
        public static SDeposit replyDeposit;

        public struct SCounting
        {
            public string side;
            public string rc;
            public int bnAccepted;
            public int bnRej;
            public int bnSuspect;
            public SNotesInfo[] depositInfo;
        }
        public static SCounting replyCounting;

        public struct SUndo
        {
            public string side;
            public string rc;
            public SNotesInfo[] undoInfo;
        }
        public static SUndo replyUndo;

        public struct SWithdrawal
        {
            public string side;
            public string rc;
            public SNotesInfo[] noteInfo;
        }
        public static SWithdrawal replyWithdrawal;

        public struct SCommand
        {
            public string daVedere;
        }

        public struct STransferInfo
        {
            public string noteId;
            public int bnNum;
            public int bnDep;
        }

        public struct STransfer
        {
            public string side;
            public string rc;
            public STransferInfo[] transferInfo;
        }
        public static STransfer replyTranfer;

        public struct SExtendedStatus
        {
            public string rc;
            public string feederStatus;
            public string controllerStatus;
            public string readerStatus;
            public string safeStatus;
            public string cassetteAStatus;
            public string cassetteBStatus;
            public string cassetteCStatus;
            public string cassetteDStatus;
            public string cassetteEStatus;
            public string cassetteFStatus;
            public string cassetteGStatus;
            public string cassetteHStatus;
            public string cassetteIStatus;
            public string cassetteJStatus;
            public string cassetteKStatus;
            public string cassetteLStatus;
            public string escrowQStatus;
            public string depositAStatus;
            public string escrowRStatus;
            public string depositBStatus;
        }
        public static SExtendedStatus replyExtendedStatus;

        public struct SFirmware
        {
            public string ver;
            public string rel;
        }

        public struct SVersion
        {
            public string rc;
            public SFirmware controller;
            public SFirmware feeder;
            public SFirmware realTime;
            public SFirmware reader;
            public SFirmware safe;
            public SFirmware FPGA;
            public SFirmware opSystem;
            public SFirmware cassetteA;
            public SFirmware cassetteB;
            public SFirmware cassetteC;
            public SFirmware cassetteD;
            public SFirmware cassetteE;
            public SFirmware cassetteF;
            public SFirmware cassetteG;
            public SFirmware cassetteH;
            public SFirmware cassetteI;
            public SFirmware cassetteJ;
            public SFirmware cassetteK;
            public SFirmware cassetteL;
            public SFirmware depositA;
            public SFirmware depositB;
        }
        public static SVersion replyVersion;

        public struct SAssign
        {
            public string rc;
            public string message;
        }
        public static SAssign replyAssign;

        public struct SRcOnly
        {
            public string rc;
        }
        public static SRcOnly replySetDateTime;
        public static SRcOnly replyFillCmConfig;
        public static SRcOnly replyFillCmOptionConfig;
        public static SRcOnly replyFillCmOptionOneConfig;
        public static SRcOnly replyFillCmOptionTwoConfig;
        public static SRcOnly replyFillCmUnitIdentification;
        public static SRcOnly replyFillCmCassetteNumber;
        public static SRcOnly replyFillWithdrawallBundleSize;
        public static SRcOnly replyFillCountingBundleSize;
        public static SRcOnly replyFillCmProtCassNumber;
        public static SRcOnly replyFillCD80CassNumber;

        public struct SUnitCoverTest
        {
            public string rc;
            public string safeDoor;
            public string cover;
            public string feeder;
            public string inputSlot;
            public string rejectSlot;
            public string leftSlot;
            public string rightSlot;
        }
        public static SUnitCoverTest replyUnitCoverTest;

        public struct STransparentState
        {
            public string rc;
            public string state;
        }
        public static STransparentState replyTransparentState;

        public struct STransparentCommand
        {
            public string answer;
            public int sizeAnswer;
        }
        public static STransparentCommand replyTransparentCommand;

        #endregion

        #region TRACE
        public enum ELayer : Int32 { CMLINK, CMCOMMAND, SIBCASH, APPLICATION }
        public enum EInfoLayer : Int16 { CMLINK, CMCOMMAND, SIBCASH, APPLICATION }
        public enum ETcrType : Int32 { CM_DIMENSION, CM_DATE }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct SComScope
        {
            //public IntPtr hWin;
            public IntPtr hWin;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct STrComponent
        {
            public EInfoLayer level;
            //public Int16 level;//funziona
        }



        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct STraceDirective
        {
            public ELayer infoLayer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 259)]
            public byte[] tracePathName;
            public Int32 lpComScope; //RIPRISTINARE QUESTO PER IL CORRETTO FUNZIONAMENTO
            public ETcrType trcType;
            public short trcSize;
            public short trcNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public STrComponent[] infoComp;
        }

        [DllImport("CMTrace.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMTrace_SetTraceDirectiveExt")]
        public static extern short SetTraceDirective(STraceDirective traceDirective, ref int lastError);



        #endregion

        #region CONNECT - DISCONNECT

        //CMCommand_Connect
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Connect")]
        public static extern short CMConnect(ref short hCon, ref ConnectionParam ConnParam);

        //CMCommand_Disconnect
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Disconnect")]
        public static extern short CMDisconnect(short hCon);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct ConnectionParam
        {
            public byte ConnectionMode;
            public RsConf pRsConf;
            public TcpIpPar TcpIpConf;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RsConf
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string device;
            public Int32 baudrate;
            public Int32 parity;
            public Int32 stop;
            public Int32 car;
            public bool dtr;
        };
        //public RsConf RSCONFIGURATION;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TcpIpPar
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string clientIpAddr;
            public int portNumber;
        };
        public int CMConnect(ref ConnectionParam param)
        {
            return CMConnect(ref hCon, ref param);
        }

        public int CMDisconnect()
        {
            for (short i = 0; i <= hCon; i++)
                CMDisconnect(i);
            return 0;
        }

        #endregion


        #region SINGLE COMMAND
        //CMCommand_Execute -  = ''
        [DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        public static extern short CMSingleCommand(int hCon, byte idCommand, ref ISingle lpCmd, ref OSingle lpReply, int timeOut);


        public static ISingle InputSingle = new ISingle();
        public static OSingle OutputSingle = new OSingle();
        public struct ISingle
        {
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1700)]
            public string command;
            public int size;
        }
        public struct OSingle
        {
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1700)]
            public string answer;
            public int size;
        }



        public string CMSingleCommand(string command, int posReply = 3, int timeOut = 5000)
        {
            n++;
            if (n == 10)
                n = 1;
            int result;
            string reply;
            InputSingle.command = command;
            InputSingle.size = InputSingle.command.Length;
            OutputSingle.answer = new string(' ', 1700);
            OutputSingle.size = OutputSingle.answer.Length;
            commandSent = command;

            CommandSent(commandSent, null);
            result = CMSingleCommand(hCon, SINGLECMD, ref InputSingle, ref OutputSingle, timeOut);
            commandAns = OutputSingle.answer;
            CommandReceived(commandAns, null);
            commandSingleAns = commandAns.Split(',');
            if (commandSingleAns.Length == 1)
                return result.ToString();
            try
            {
                reply = commandSingleAns[posReply];
                return reply;
            }
            catch (System.IndexOutOfRangeException)
            {
                return result.ToString();
            }

        }
        #endregion


        //[DllImport("CMCommand.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, EntryPoint = "CMCommand_Execute")]
        //public static extern short cmdOpen(int hCon, byte idCommand, ref IOpen lpCmd, ref OOpen lpReply, int timeOut);
        //public static IOpen inputOpen = new IOpen();
        //public static OOpen OutputOpen = new OOpen();

        //public short cmdOpen()
        //{
        //    inputOpen.side = DOWNLOAD;
        //    inputOpen.password = "123456";
        //    inputOpen.opt = null;
        //    cmdOpen(hCon, OPEN, ref inputOpen, ref OutputOpen, 5000);
        //    return OutputOpen.rc;
        //}

        /// <summary>
        /// The Open command is used to establish a link between the Host and the machine
        /// </summary>
        /// <param name="side">Operator side (L/R)</param>
        /// <param name="code">Password relative to the operator selected</param>
        /// <returns></returns>
        public string CMOpen(string side = "L", string code = "123456")
        {
            string rc = CMSingleCommand("O," + n + "," + side + "," + code, 3, 5000);
            replyOpen = new SReplyOpen();
            if (commandSingleAns.Length > 3)
            {
                replyOpen.rc = commandSingleAns[3];
                replyOpen.side = commandSingleAns[2];
            }
            return rc;


        }

        public SUnitConfig CMGetUnitConfig()
        {
            SUnitConfig temp = new SUnitConfig();

            temp.rc = CMSingleCommand($"T,{n},0,15", 4);
            if (temp.rc != "101")
                goto fine;
            temp.cfg = commandSingleAns[5];
            temp.rc = CMSingleCommand($"T,{n},0,28", 4);
            if (temp.rc != "101")
                goto fine;
            temp.optCfg = commandSingleAns[5];
            temp.rc = CMSingleCommand($"T,{n},0,32", 4);
            if (temp.rc != "101")
                goto fine;
            temp.optOneCfg = commandSingleAns[5]; 
            temp.rc = CMSingleCommand($"T,{n},0,49", 4);
            if (temp.rc != "101")
                goto fine;
            temp.optTwoCfg = commandSingleAns[5]; 
            temp.rc = CMSingleCommand($"T,{n},0,51", 4);
            if (temp.rc != "101")
                goto fine;
            temp.optThreeCfg = commandSingleAns[5];
            temp.allCfg = new string[] { temp.cfg, temp.optCfg, temp.optOneCfg, temp.optTwoCfg, temp.optThreeCfg };
        fine:
            return temp;
        }
        public int CMGetActiveBanks()
        {
            int result = -1;
            string rc = CMSingleCommand($"T,{n},3,3", 4);
            if (rc == "101")
                int.TryParse(commandSingleAns[5], out result);
            return result;
        }
        public string CMSetActiveSlots(int numActBnk)
        {
            string rc = CMSingleCommand($"Z,{n},3,2," + numActBnk, 4);
            if (rc == "101")
            { 
            }
            return rc;
        }
        public SBanks CMGetBankConfig()
        {
            SBanks bank = new SBanks();
            string rc = CMSingleCommand($"T,{n},3,1", 4, 50000);
            if (rc == "101")
            {
                
            }
            return bank;
        }

        public SReferences CMGetReaderReferences()
        {
            SReferences references = new SReferences();
            string rc = CMSingleCommand($"T,{n},3,5", 4);
            if (rc == "101")
            {
                references.name = commandSingleAns[5];
                references.ver = commandSingleAns[6];
                int.TryParse(commandSingleAns[7], out references.bankId);
            }
            return references;
        }

        public int CMGetFitnessConfig(int bank)
        {
            int result = -1;
            string rc = CMSingleCommand($"T,{n},3,15,{bank}", 4);
            if (rc == "101")
                int.TryParse(commandSingleAns[5], out result);
            return result;
        }
        public string CMGetInstalledCDF()
        {
            string rc = CMSingleCommand($"T,{n},3,21", 4);
            return rc;
        }

        public string CMSetUnitConfig(string[] allCfg)
        {
            SUnitConfig temp = new SUnitConfig();
            if (allCfg.Length != 5)
                return temp.rc;
            temp.rc = CMSingleCommand($"F,{n},7,{allCfg[0]}");
            if (temp.rc != "101")
                goto fine;
            temp.rc = CMSingleCommand($"F,{n},8,{allCfg[1]}");
            if (temp.rc != "101")
                goto fine;
            temp.rc = CMSingleCommand($"F,{n},9,{allCfg[2]}");
            if (temp.rc != "101")
                goto fine;
            temp.rc = CMSingleCommand($"F,{n},23,{allCfg[3]}");
            if (temp.rc != "101")
                goto fine;
            temp.rc = CMSingleCommand($"F,{n},31,{allCfg[4]}", 3, 14000);
            if (temp.rc != "101")
                goto fine;
            fine:
            return temp.rc;
        }

        public class UnitConfiguration
        {
            public int idUnitConfiguration { get; set; }
            public string cfg { get; set; }
            public string optCfg { get; set; }
            public string optOneCfg { get; set; }
            public string optTwoCfg { get; set; }
            public string optThreeCfg { get; set; }
        }
        public string CMSetUnitConfig(UnitConfiguration unitCfg)
        {
            SUnitConfig temp = new SUnitConfig();
            temp.rc = CMSingleCommand($"F,{n},7,{unitCfg.cfg}");
            if (temp.rc != "101")
                goto fine;
            temp.rc = CMSingleCommand($"F,{n},8,{unitCfg.optCfg}");
            if (temp.rc != "101")
                goto fine;
            temp.rc = CMSingleCommand($"F,{n},9,{unitCfg.optOneCfg}");
            if (temp.rc != "101")
                goto fine;
            temp.rc = CMSingleCommand($"F,{n},23,{unitCfg.optTwoCfg}");
            if (temp.rc != "101")
                goto fine;
            temp.rc = CMSingleCommand($"F,{n},31,{unitCfg.optThreeCfg}", 3, 18000);
            if (temp.rc != "101")
                goto fine;
            fine:
            return temp.rc;
        }

        public SGetModuleInformation CMGetMachineInfo(string module)
        {
            string rc = CMSingleCommand($"T,{n},{module},13", 4);
            replyGetModuleInformation = new SGetModuleInformation();
            replyGetModuleInformation.rc = rc;
            if (rc == "101")
            {
                replyGetModuleInformation.name = commandSingleAns[5];
                replyGetModuleInformation.ver = commandSingleAns[6];
                replyGetModuleInformation.rel = commandSingleAns[7];
                replyGetModuleInformation.serialNumber = commandSingleAns[8];
            }
            return replyGetModuleInformation;
        }

        public SLANSettings CMGetLanSettings()
        {
            replyLanSettings = new SLANSettings();
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,1", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.dhcpEnabled = commandSingleAns[5];
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,2", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.ipAddress = commandSingleAns[5];
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,3", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.subnetMask = commandSingleAns[5];
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,4", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.gateway = commandSingleAns[5];
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,5", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.deviceName = commandSingleAns[5];
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,6", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.deviceDescription = commandSingleAns[5];
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,7", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.primaryDNS = commandSingleAns[5];
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,8", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.secondaryDNS = commandSingleAns[5];
            replyLanSettings.rc = CMSingleCommand($"T,{n},6,15,11", 4);
            if (replyLanSettings.rc != "101")
                goto fine;
            replyLanSettings.useNetBiosName = commandSingleAns[5];

        fine:
            return replyLanSettings;
        }


        //public string CMGetMachineSN()
        //{
        //    string rc = CMSingleCommand($"T,{n},0,13",4);
        //    replyGetModuleInformation = new SGetModuleInformation();
        //    replyGetModuleInformation.rc = rc;
        //    if (rc == "101")
        //    {
        //        replyGetModuleInformation.name = commandSingleAns[5];
        //        replyGetModuleInformation.ver = commandSingleAns[6];
        //        replyGetModuleInformation.rel = commandSingleAns[7];
        //        replyGetModuleInformation.serialNumber = commandSingleAns[8];
        //    }
        //    return replyGetModuleInformation.serialNumber;
        //}

        /// <summary>
        /// Information regarding the cassettes enabled to the side specified (amount of notes contained in every cassette)
        /// </summary>
        /// <param name="side">Operator side (L/R)</param>
        /// <returns></returns>
        public string CMGetCashData(string side = "L")
        {
            string rc = CMSingleCommand("G," + n + "," + side, 3, 5000);

            int dim = ((commandSingleAns.Length) - 4) / 5;
            replyGetCashData = new SGetCashData();
            SCassetteInfo[] cassetteInfo = new SCassetteInfo[dim];
            replyGetCashData.side = commandSingleAns[2];
            replyGetCashData.rc = commandSingleAns[3];
            for (int i = 0; i < dim; i++)
            {
                cassetteInfo[i].noteId = commandSingleAns[i * 5 + 4];
                cassetteInfo[i].bnNumber = commandSingleAns[i * 5 + 5];
                cassetteInfo[i].bnFreeCap = commandSingleAns[i * 5 + 6];
                cassetteInfo[i].name = commandSingleAns[i * 5 + 7];
                cassetteInfo[i].enabled = commandSingleAns[i * 5 + 8];
            }
            replyGetCashData.CassetteInfo = cassetteInfo;
            replyGetCashData.numCasProt = dim.ToString();
            return rc;
        }

        /// <summary>
        /// Standard deposit in SAFE isong nation activated or nation specified in denomination
        /// </summary>
        /// <param name="side">Operator side (L/R)</param>
        /// <returns></returns>
        public string CMDeposit(string side = "L")
        {
            string rc = CMSingleCommand("D," + n + "," + side + ",0,0000", 3, 60000);
            int dim = ((commandSingleAns.Length) - 7) / 2;
            replyDeposit = new SDeposit();
            SNotesInfo[] notesInfo = new SNotesInfo[dim];
            replyDeposit.side = commandSingleAns[2];
            replyDeposit.rc = commandSingleAns[3];
            replyDeposit.bnTotal = Convert.ToInt16(commandSingleAns[4]);
            replyDeposit.bnOutput = Convert.ToInt16(commandSingleAns[5]);
            replyDeposit.bnRej = Convert.ToInt16(commandSingleAns[6]);
            for (int i = 0; i < dim; i++)
            {
                notesInfo[i].denomination = commandSingleAns[i * 2 + 7];
                notesInfo[i].bnNum = Convert.ToInt16(commandSingleAns[i * 2 + 8]);
            }
            replyDeposit.depositInfo = notesInfo;

            return rc;
        }

        /// <summary>
        /// Standard countig note. The notes are all directed to the output(FIT/UNFIT) and reject(suspect, not recognized)
        /// </summary>
        /// <param name="side">Operator side (L/R)</param>
        /// <returns></returns>
        public string CMCounting(string side = "L")
        {
            string rc = CMSingleCommand("D," + n + "," + side + ",3,0000", 3, 60000);
            int dim = ((commandSingleAns.Length) - 7) / 2;
            replyCounting = new SCounting();
            SNotesInfo[] notesInfo = new SNotesInfo[dim];
            replyCounting.side = commandSingleAns[2];
            replyCounting.rc = commandSingleAns[3];
            replyCounting.bnAccepted = Convert.ToInt16(commandSingleAns[4]);
            replyCounting.bnRej = Convert.ToInt16(commandSingleAns[5]);
            replyCounting.bnSuspect = Convert.ToInt16(commandSingleAns[6]);
            for (int i = 0; i < dim; i++)
            {
                notesInfo[i].denomination = commandSingleAns[i * 2 + 7];
                notesInfo[i].bnNum = Convert.ToInt16(commandSingleAns[i * 2 + 8]);
            }
            replyCounting.depositInfo = notesInfo;

            return rc;
        }

        /// <summary>
        /// FIT counting note. The notes are all directed to the output (FIT) and reject(all others)
        /// </summary>
        /// <param name="side">Operator side (L/R)</param>
        /// <returns></returns>
        public string CMFitCounting(string side = "L")
        {
            string rc = CMSingleCommand("D," + n + "," + side + ",10,0000", 3, 60000);
            int dim = ((commandSingleAns.Length) - 7) / 2;
            replyCounting = new SCounting();
            SNotesInfo[] notesInfo = new SNotesInfo[dim];
            replyCounting.side = commandSingleAns[2];
            replyCounting.rc = commandSingleAns[3];
            replyCounting.bnAccepted = Convert.ToInt16(commandSingleAns[4]);
            replyCounting.bnRej = Convert.ToInt16(commandSingleAns[5]);
            replyCounting.bnSuspect = Convert.ToInt16(commandSingleAns[6]);
            for (int i = 0; i < dim; i++)
            {
                notesInfo[i].denomination = commandSingleAns[i * 2 + 7];
                notesInfo[i].bnNum = Convert.ToInt16(commandSingleAns[i * 2 + 8]);
            }
            replyCounting.depositInfo = notesInfo;

            return rc;
        }

        /// <summary>
        /// With this command it is possible to cancel the deposits done until that moment or to accept them or to verify the amount of undo deposit
        /// </summary>
        /// <param name="side">Operator side</param>
        /// <param name="mode">0:UNDO DEPOSIT, 1:ACCEPT DEPOSIT, 2:VIEW LIST DEPOSIT</param>
        /// <returns></returns>
        public string CMUndo(string side = "L", string mode = "0")
        {
            string rc = CMSingleCommand("U," + n + "," + side + "," + mode, 3, 112000);
            int dim = ((commandSingleAns.Length) - 4) / 2;
            replyUndo = new SUndo();
            SNotesInfo[] notesInfo = new SNotesInfo[dim];
            replyUndo.side = commandSingleAns[2];
            replyUndo.rc = commandSingleAns[3];
            for (int i = 0; i < dim; i++)
            {
                notesInfo[i].denomination = commandSingleAns[i * 2 + 4];
                notesInfo[i].bnNum = Convert.ToInt16(commandSingleAns[i * 2 + 5]);
            }
            replyUndo.undoInfo = notesInfo;
            return rc;
        }

        /// <summary>
        /// This command delivers to the operator the requested number of notes for each specific denomination.
        /// </summary>
        /// <param name="noteInfo">stuct(SNotesInfo) of requested notes for each specific denomination</param>
        /// <param name="side">Operator side</param>
        /// <returns></returns>
        public string CMWithdrawal(SNotesInfo[] noteInfo = null, string side = "L")
        {
            string request = "";
            for (int i = 0; i < noteInfo.Length; i++)
            {
                request += "," + noteInfo[i].denomination + "," + noteInfo[i].bnNum;
            }
            string rc = CMSingleCommand("W," + n + "," + side + request, 3, 112000);
            int dim = ((commandSingleAns.Length) - 4) / 2;
            replyWithdrawal = new SWithdrawal();
            SNotesInfo[] notesInfo = new SNotesInfo[dim];
            replyWithdrawal.side = commandSingleAns[2];
            replyWithdrawal.rc = commandSingleAns[3];
            for (int i = 0; i < dim; i++)
            {
                notesInfo[i].denomination = commandSingleAns[i * 2 + 4];
                notesInfo[i].bnNum = Convert.ToInt16(commandSingleAns[i * 2 + 5]);
            }
            replyWithdrawal.noteInfo = notesInfo;
            return rc;
        }

        /// <summary>
        /// This command delivers to the selected CD80 cassettes the requested number of notes for each specific denomination
        /// </summary>
        /// <param name="side">Operator side</param>
        /// <param name="destination">CD80 cassette destination</param>
        /// <param name="noteInfo">stuct(SNotesInfo) of requested notes for each specific denomination</param>
        /// <returns></returns>
        public string CMTransfer(string side = "L", string destination = "A", SNotesInfo[] noteInfo = null)
        {
            string request = "";
            for (int i = 0; i < noteInfo.Length; i++)
            {
                request += "," + noteInfo[i].denomination + "," + noteInfo[i].bnNum;
            }

            string rc = CMSingleCommand("w," + n + "," + side + ",0," + destination + request, 3, 112000);
            int dim = ((commandSingleAns.Length) - 4) / 3;
            replyTranfer = new STransfer();
            STransferInfo[] transferInfo = new STransferInfo[dim];
            replyTranfer.side = commandSingleAns[2];
            replyTranfer.rc = commandSingleAns[3];
            for (int i = 0; i < dim; i++)
            {
                transferInfo[i].noteId = commandSingleAns[i * 3 + 4];
                transferInfo[i].bnNum = Convert.ToInt16(commandSingleAns[i * 3 + 5]);
                transferInfo[i].bnDep = Convert.ToInt16(commandSingleAns[i * 3 + 6]);
            }
            return rc;
        }

        /// <summary>
        /// This command will close the operator work session
        /// </summary>
        /// <param name="side">Operator side</param>
        /// <returns></returns>
        public string CMClose(string side = "L")
        {
            return CMSingleCommand("C," + n + "," + side, 3, 5000);
        }

        public string CMCassetteNumber(int cass_num = 0)
        {
            return CMSingleCommand("F,1,11," + cass_num.ToString());
        }

        public string CMCD80Number(int num_cd80 = 0)
        {
            return CMSingleCommand("F,1,30," + num_cd80);
        }

        /// <summary>
        /// This command give a detailed information regarding the status of every modules.
        /// </summary>
        /// <returns></returns>
        public string CMExtendedStatus()
        {
            string rc = CMSingleCommand("E," + n, 2, 5000);
            replyExtendedStatus = new SExtendedStatus();
            replyExtendedStatus.rc = commandSingleAns[2];
            replyExtendedStatus.feederStatus = commandSingleAns[3].Substring(1);
            replyExtendedStatus.controllerStatus = commandSingleAns[4].Substring(1);
            replyExtendedStatus.readerStatus = commandSingleAns[5].Substring(1);
            replyExtendedStatus.safeStatus = commandSingleAns[6].Substring(1);
            for (int i = 7; i < commandSingleAns.Length; i++)
            {
                switch (commandSingleAns[i].Substring(0, 1))
                {
                    case "A":
                        replyExtendedStatus.cassetteAStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "B":
                        replyExtendedStatus.cassetteBStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "C":
                        replyExtendedStatus.cassetteCStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "D":
                        replyExtendedStatus.cassetteDStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "E":
                        replyExtendedStatus.cassetteEStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "F":
                        replyExtendedStatus.cassetteFStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "G":
                        replyExtendedStatus.cassetteGStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "H":
                        replyExtendedStatus.cassetteHStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "I":
                        replyExtendedStatus.cassetteIStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "J":
                        replyExtendedStatus.cassetteJStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "K":
                        replyExtendedStatus.cassetteKStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "L":
                        replyExtendedStatus.cassetteLStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "q":
                        replyExtendedStatus.escrowQStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "a":
                        replyExtendedStatus.depositAStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "r":
                        replyExtendedStatus.escrowRStatus = commandSingleAns[i].Substring(1);
                        break;
                    case "b":
                        replyExtendedStatus.depositBStatus = commandSingleAns[i].Substring(1);
                        break;
                }
            }
            return rc;
        }

        /// <summary>
        /// This command give the firmware release and version of each single module
        /// </summary>
        /// <param name="module">Module address or name</param>
        /// <returns></returns>
        public string CMVersion(string module)
        {
            string rc;
            if (module.Length > 1)
            {
                switch (module.ToUpper())
                {
                    case "CONTROLLER":
                        module = "0";
                        break;
                    case "FEEDER":
                        module = "1";
                        break;
                    case "RTC":
                    case "REAL TIME":
                    case "REAL TIME CONTROLLER":
                        module = "2";
                        break;
                    case "READER":
                    case "IDENTIFIER":
                        module = "3";
                        break;
                    case "SAFE":
                        module = "4";
                        break;
                    case "5":
                        module = "5";
                        break;
                    case "OP SYSTEM":
                        module = "6";
                        break;
                    case "CASSETTOA":
                        module = "A";
                        break;
                    case "CASSETTOB":
                        module = "B";
                        break;
                    case "CASSETTOC":
                        module = "C";
                        break;
                    case "CASSETTOD":
                        module = "D";
                        break;
                    case "CASSETTOE":
                        module = "E";
                        break;
                    case "CASSETTOF":
                        module = "F";
                        break;
                    case "CASSETTOG":
                        module = "G";
                        break;
                    case "CASSETTOH":
                        module = "H";
                        break;
                    case "CASSETTOI":
                        module = "I";
                        break;
                    case "CASSETTOJ":
                        module = "J";
                        break;
                    case "CASSETTOK":
                        module = "K";
                        break;
                    case "CASSETTOL":
                        module = "L";
                        break;
                    case "DEPOSITA":
                        module = "a";
                        break;
                    case "DEPOSITB":
                        module = "b";
                        break;
                    default:
                        rc = "KO";
                        break;
                }
            }
            rc = CMSingleCommand("V," + n + "," + module, 2, 5000);
            replyVersion = new SVersion();
            replyVersion.rc = commandSingleAns[2];
            switch (module)
            {
                case "0":
                    replyVersion.controller.ver = commandSingleAns[3];
                    replyVersion.controller.rel = commandSingleAns[4];
                    break;
                case "1":
                    replyVersion.feeder.ver = commandSingleAns[3];
                    replyVersion.feeder.rel = commandSingleAns[4];
                    break;
                case "2":
                    replyVersion.realTime.ver = commandSingleAns[3];
                    replyVersion.realTime.rel = commandSingleAns[4];
                    break;
                case "3":
                    replyVersion.reader.ver = commandSingleAns[3];
                    replyVersion.reader.rel = commandSingleAns[4];
                    break;
                case "4":
                    replyVersion.safe.ver = commandSingleAns[3];
                    replyVersion.safe.rel = commandSingleAns[4];
                    break;
                case "5":
                    replyVersion.FPGA.ver = commandSingleAns[3];
                    replyVersion.FPGA.rel = commandSingleAns[4];
                    break;
                case "6":
                    replyVersion.opSystem.ver = commandSingleAns[3];
                    replyVersion.opSystem.rel = commandSingleAns[4];
                    break;
                case "A":
                    replyVersion.cassetteA.ver = commandSingleAns[3];
                    replyVersion.cassetteA.rel = commandSingleAns[4];
                    break;
                case "B":
                    replyVersion.cassetteB.ver = commandSingleAns[3];
                    replyVersion.cassetteB.rel = commandSingleAns[4];
                    break;
                case "C":
                    replyVersion.cassetteC.ver = commandSingleAns[3];
                    replyVersion.cassetteC.rel = commandSingleAns[4];
                    break;
                case "D":
                    replyVersion.cassetteD.ver = commandSingleAns[3];
                    replyVersion.cassetteD.rel = commandSingleAns[4];
                    break;
                case "E":
                    replyVersion.cassetteE.ver = commandSingleAns[3];
                    replyVersion.cassetteE.rel = commandSingleAns[4];
                    break;
                case "F":
                    replyVersion.cassetteF.ver = commandSingleAns[3];
                    replyVersion.cassetteF.rel = commandSingleAns[4];
                    break;
                case "G":
                    replyVersion.cassetteG.ver = commandSingleAns[3];
                    replyVersion.cassetteG.rel = commandSingleAns[4];
                    break;
                case "H":
                    replyVersion.cassetteH.ver = commandSingleAns[3];
                    replyVersion.cassetteH.rel = commandSingleAns[4];
                    break;
                case "I":
                    replyVersion.cassetteI.ver = commandSingleAns[3];
                    replyVersion.cassetteI.rel = commandSingleAns[4];
                    break;
                case "J":
                    replyVersion.cassetteJ.ver = commandSingleAns[3];
                    replyVersion.cassetteJ.rel = commandSingleAns[4];
                    break;
                case "K":
                    replyVersion.cassetteK.ver = commandSingleAns[3];
                    replyVersion.cassetteK.rel = commandSingleAns[4];
                    break;
                case "L":
                    replyVersion.cassetteL.ver = commandSingleAns[3];
                    replyVersion.cassetteL.rel = commandSingleAns[4];
                    break;
                case "a":
                    replyVersion.depositA.ver = commandSingleAns[3];
                    replyVersion.depositA.rel = commandSingleAns[4];
                    break;
                case "b":
                    replyVersion.depositB.ver = commandSingleAns[3];
                    replyVersion.depositB.rel = commandSingleAns[4];
                    break;
            }
            return rc;
        }

        /// <summary>
        /// This command initializes the cassette logical address according to the physical position
        /// </summary>
        /// <returns></returns>
        public string CMAssignCassette()
        {
            string rc = CMSingleCommand("A," + n, 2, 5000);
            replyAssign = new SAssign();
            replyAssign.rc = commandSingleAns[2];
            replyAssign.message = commandSingleAns[3];
            return rc;
        }





        /*    GET CONFIG 2.16 / SET CONFIG 2.17       pg 51/52         */





        /// <summary>
        /// This command will give the date and time info to the device
        /// </summary>
        /// <param name="mm">Month (1 - 12)</param>
        /// <param name="dd">Day (1 - 31)</param>
        /// <param name="hh">Hour (1 - 24)</param>
        /// <param name="pp">Minutes (0 - 59)</param>
        /// <returns></returns>
        public string CMSetDateTime(int mm = 0, int dd = 0, int hh = 0, int pp = 0)
        {
            string rc = "";
            if (mm == 0 && dd == 0)
            {
                mm = Convert.ToInt16(DateTime.Now.Month);
                dd = Convert.ToInt16(DateTime.Now.Day);
            }

            if (hh == 0 && pp == 0)
            {
                hh = Convert.ToInt16(DateTime.Now.Hour);
                pp = Convert.ToInt16(DateTime.Now.Minute);
            }

            if (mm < 1 | mm > 12 | dd < 1 | dd > 31)
            {
                rc = "WRONG DATE";
            }

            if (hh < 1 | hh > 24 | hh < 0 | hh > 59)
            {
                rc += " WRONG TIME";
            }

            if (rc == "")
            {
                rc = CMSingleCommand("F," + n + ",0," + mm + "," + dd + "," + hh + "," + pp, 3, 5000);
            }
            replySetDateTime = new SRcOnly();
            replySetDateTime.rc = commandSingleAns[3];
            return rc;
        }

        /// <summary>
        /// This command configure some functionality parameters on the CM
        /// </summary>
        /// <param name="config">Hex value for entire configuration (4 digit)</param>
        /// <param name="x0001">0/1 - View date & time on display</param>
        /// <param name="x0002">reserved</param>
        /// <param name="x0004">reserved</param>
        /// <param name="x0008">reserved</param>
        /// <param name="x0010">reserved</param>
        /// <param name="x0020">reserved</param>
        /// <param name="x0040">0/1 - Balanced cassette handling</param>
        /// <param name="x0080">0/1 - Alarm 1 handling</param>
        /// <param name="x0100">0/1 - Use delay class dispensing</param>
        /// <param name="x0200">reserved</param>
        /// <param name="x0400">0/1 - Data & time on display in AM/PM format</param>
        /// <param name="x0800">0/1 - Use UNFIT in SAFE</param>
        /// <param name="x1000">reserved</param>
        /// <param name="x2000">reserved</param>
        /// <param name="x4000">reserved</param>
        /// <param name="x8000">0/1 - Enable Journal Log</param>
        /// <returns></returns>
        public string CMFillConfig(string config = "", int x0001 = 0, int x0002 = 0, int x0004 = 0, int x0008 = 0, int x0010 = 0, int x0020 = 0, int x0040 = 0, int x0080 = 0, int x0100 = 0, int x0200 = 0, int x0400 = 0, int x0800 = 0, int x1000 = 0, int x2000 = 0, int x4000 = 0, int x8000 = 0)
        {

            if (config == "")
            {
                x0001 = x0001 == 1 ? 0x0001 : 0;
                x0002 = x0002 == 1 ? 0x0002 : 0;
                x0004 = x0004 == 1 ? 0x0004 : 0;
                x0008 = x0008 == 1 ? 0x0008 : 0;
                x0010 = x0010 == 1 ? 0x0010 : 0;
                x0020 = x0020 == 1 ? 0x0020 : 0;
                x0040 = x0040 == 1 ? 0x0040 : 0;
                x0080 = x0080 == 1 ? 0x0080 : 0;
                x0100 = x0100 == 1 ? 0x0100 : 0;
                x0200 = x0200 == 1 ? 0x0200 : 0;
                x0400 = x0400 == 1 ? 0x0400 : 0;
                x0800 = x0800 == 1 ? 0x0800 : 0;
                x1000 = x1000 == 1 ? 0x1000 : 0;
                x2000 = x2000 == 1 ? 0x2000 : 0;
                x4000 = x4000 == 1 ? 0x4000 : 0;
                x8000 = x8000 == 1 ? 0x8000 : 0;

                config = (x0001 + x0002 + x0004 + x0008 + x0010 + x0020 + x0040 + x0080 + x0100 + x0200 + x0400 + x0800 + x1000 + x2000 + x4000 + x8000).ToString("X4");
            }

            replyFillCmConfig = new SRcOnly();
            replyFillCmConfig.rc = CMSingleCommand("F," + n + ",7," + config, 3, 5000);
            return replyFillCmConfig.rc;
        }

        /// <summary>
        /// With this command it is possible to configure some functionality parameters on the CM and to see yours effects is necessary to switch off and on the machine
        /// </summary>
        /// <param name="config">Hex value for entire configuration (4 digit)</param>
        /// <param name="x0001">Special clean without control</param>
        /// <param name="x0002">reserved</param>
        /// <param name="x0004">reserved</param>
        /// <param name="x0008">Special alarm mode</param>
        /// <param name="x0010">Enable Simplified protocol</param>
        /// <param name="x0020">Execute Close commando also in Jam</param>
        /// <param name="x0040">reserved</param>
        /// <param name="x0080">reserved</param>
        /// <param name="x0100">reserved</param>
        /// <param name="x0200">reserved</param>
        /// <param name="x0400">reserved</param>
        /// <param name="x0800">reserved</param>
        /// <param name="x1000">reserved</param>
        /// <param name="x2000">reserved</param>
        /// <param name="x4000">reserved</param>
        /// <param name="x8000">reserved</param>
        /// <returns></returns>
        public string CMFillOptionConfig(string config = "", int x0001 = 0, int x0002 = 0, int x0004 = 0, int x0008 = 0, int x0010 = 0, int x0020 = 0, int x0040 = 0, int x0080 = 0, int x0100 = 0, int x0200 = 0, int x0400 = 0, int x0800 = 0, int x1000 = 0, int x2000 = 0, int x4000 = 0, int x8000 = 0)
        {

            if (config == "")
            {
                x0001 = x0001 == 1 ? 0x0001 : 0;
                x0002 = x0002 == 1 ? 0x0002 : 0;
                x0004 = x0004 == 1 ? 0x0004 : 0;
                x0008 = x0008 == 1 ? 0x0008 : 0;
                x0010 = x0010 == 1 ? 0x0010 : 0;
                x0020 = x0020 == 1 ? 0x0020 : 0;
                x0040 = x0040 == 1 ? 0x0040 : 0;
                x0080 = x0080 == 1 ? 0x0080 : 0;
                x0100 = x0100 == 1 ? 0x0100 : 0;
                x0200 = x0200 == 1 ? 0x0200 : 0;
                x0400 = x0400 == 1 ? 0x0400 : 0;
                x0800 = x0800 == 1 ? 0x0800 : 0;
                x1000 = x1000 == 1 ? 0x1000 : 0;
                x2000 = x2000 == 1 ? 0x2000 : 0;
                x4000 = x4000 == 1 ? 0x4000 : 0;
                x8000 = x8000 == 1 ? 0x8000 : 0;

                config = (x0001 + x0002 + x0004 + x0008 + x0010 + x0020 + x0040 + x0080 + x0100 + x0200 + x0400 + x0800 + x1000 + x2000 + x4000 + x8000).ToString("X4");
            }

            replyFillCmOptionConfig = new SRcOnly();
            replyFillCmOptionConfig.rc = CMSingleCommand("F," + n + ",8," + config, 3, 5000);
            return replyFillCmOptionConfig.rc;
        }

        /// <summary>
        /// With this command it is possible to configure some functionality parameters on the CM and to see yours effects is necessary to switch off and on the machine.
        /// </summary>
        /// <param name="config">Hex value for entire configuration (4 digit)</param>
        /// <param name="x0001">reserved</param>
        /// <param name="x0002">reserved</param>
        /// <param name="x0004">reserved</param>
        /// <param name="x0008">Impac 0mm on cassette CR37</param>
        /// <param name="x0010">reserved</param>
        /// <param name="x0020">reserved</param>
        /// <param name="x0040">reserved</param>
        /// <param name="x0080">Don't remove cass jam when special clean without control enabled</param>
        /// <param name="x0100">reserved</param>
        /// <param name="x0200">reserved</param>
        /// <param name="x0400">reserved</param>
        /// <param name="x0800">Timeout connect/disconnect LAN</param>
        /// <param name="x1000">Enable time control dispense amount (TCDA)</param>
        /// <param name="x2000">reserved</param>
        /// <param name="x4000">Enable booking open by external button</param>
        /// <param name="x8000">reserved</param>
        /// <returns></returns>
        public string CMFillOptionOneConfig(string config = "", int x0001 = 0, int x0002 = 0, int x0004 = 0,
            int x0008 = 0, int x0010 = 0, int x0020 = 0, int x0040 = 0, int x0080 = 0, int x0100 = 0, int x0200 = 0,
            int x0400 = 0, int x0800 = 0, int x1000 = 0, int x2000 = 0, int x4000 = 0, int x8000 = 0)
        {

            if (config == "")
            {
                x0001 = x0001 == 1 ? 0x0001 : 0;
                x0002 = x0002 == 1 ? 0x0002 : 0;
                x0004 = x0004 == 1 ? 0x0004 : 0;
                x0008 = x0008 == 1 ? 0x0008 : 0;
                x0010 = x0010 == 1 ? 0x0010 : 0;
                x0020 = x0020 == 1 ? 0x0020 : 0;
                x0040 = x0040 == 1 ? 0x0040 : 0;
                x0080 = x0080 == 1 ? 0x0080 : 0;
                x0100 = x0100 == 1 ? 0x0100 : 0;
                x0200 = x0200 == 1 ? 0x0200 : 0;
                x0400 = x0400 == 1 ? 0x0400 : 0;
                x0800 = x0800 == 1 ? 0x0800 : 0;
                x1000 = x1000 == 1 ? 0x1000 : 0;
                x2000 = x2000 == 1 ? 0x2000 : 0;
                x4000 = x4000 == 1 ? 0x4000 : 0;
                x8000 = x8000 == 1 ? 0x8000 : 0;

                config = (x0001 + x0002 + x0004 + x0008 + x0010 + x0020 + x0040 + x0080 + x0100 + x0200 + x0400 + x0800 +
                    x1000 + x2000 + x4000 + x8000).ToString("X4");
            }

            replyFillCmOptionOneConfig = new SRcOnly();
            replyFillCmOptionOneConfig.rc = CMSingleCommand("F," + n + ",9," + config, 3, 5000);
            return replyFillCmOptionOneConfig.rc;
        }

        /// <summary>
        /// With this command it is possible to configure some functionality parameters on the CM and to see yours effects is necessary to switch off and on the machine
        /// </summary>
        /// <param name="config">Hex value for entire configuration (8 digit)</param>
        /// <param name="x00000001">Enable 40mm Safe</param>
        /// <param name="x00000002">BVU synchronous handling</param>
        /// <param name="x00000004">Banknotes orientation in safe</param>
        /// <param name="x00000008">Special clean using display</param>
        /// <param name="x00000010">Handling cat 2 box</param>
        /// <param name="x00000020">reserved</param>
        /// <param name="x00000040">EVO led handling</param>
        /// <param name="x00000080">Separate Cat1 and Cat3</param>
        /// <param name="x00000100">reserved</param>
        /// <param name="x00000200">Feed banknotes with input bin free</param>
        /// <param name="x00000400">Different center AU banknotes</param>
        /// <param name="x00000800">reserved</param>
        /// <param name="x00001000">Block recovery after power off in cash movements</param>
        /// <param name="x00002000">Shutter handling</param>
        /// <param name="x00004000">reserved</param>
        /// <param name="x00008000">Disable message on display</param>
        /// <param name="x00010000">Alarm2 output handling</param>
        /// <returns></returns>
        public string CMFillOptionTwoConfig(string config = "", int x00000001 = 0, int x00000002 = 0, int x00000004 = 0,
            int x00000008 = 0, int x00000010 = 0, int x00000020 = 0, int x00000040 = 0, int x00000080 = 0, int x00000100 = 0,
            int x00000200 = 0, int x00000400 = 0, int x00000800 = 0, int x00001000 = 0, int x00002000 = 0, int x00004000 = 0,
            int x00008000 = 0, int x00010000 = 0)
        {

            if (config == "")
            {
                x00000001 = x00000001 == 1 ? 0x00000001 : 0;
                x00000002 = x00000002 == 1 ? 0x00000002 : 0;
                x00000004 = x00000004 == 1 ? 0x00000004 : 0;
                x00000008 = x00000008 == 1 ? 0x00000008 : 0;
                x00000010 = x00000010 == 1 ? 0x00000010 : 0;
                x00000020 = x00000020 == 1 ? 0x00000020 : 0;
                x00000040 = x00000040 == 1 ? 0x00000040 : 0;
                x00000080 = x00000080 == 1 ? 0x00000080 : 0;
                x00000100 = x00000100 == 1 ? 0x00000100 : 0;
                x00000200 = x00000200 == 1 ? 0x00000200 : 0;
                x00000400 = x00000400 == 1 ? 0x00000400 : 0;
                x00000800 = x00000800 == 1 ? 0x00000800 : 0;
                x00001000 = x00001000 == 1 ? 0x00001000 : 0;
                x00002000 = x00002000 == 1 ? 0x00002000 : 0;
                x00004000 = x00004000 == 1 ? 0x00004000 : 0;
                x00008000 = x00008000 == 1 ? 0x00008000 : 0;
                x00010000 = x00010000 == 1 ? 0x00010000 : 0;

                config = (x00000001 + x00000002 + x00000004 + x00000008 + x00000010 + x00000020 +
                    x00000040 + x00000080 + x00000100 + x00000200 + x00000400 + x00000800 + x00001000 +
                    x00002000 + x00004000 + x00008000 + x00010000).ToString("X8");
            }

            replyFillCmOptionTwoConfig = new SRcOnly();
            replyFillCmOptionTwoConfig.rc = CMSingleCommand("F," + n + ",23," + config, 3, 5000);
            return replyFillCmOptionTwoConfig.rc;
        }

        /// <summary>
        /// With this command it is possible to set the identification code of the device.
        /// </summary>
        /// <param name="identification">Unit identification (CM20, CM18, CM18T, CM18E, CM20E, CM18B)</param>
        /// <returns></returns>
        public string CMFillUnitIdentication(string identification = "CM18T")
        {
            replyFillCmUnitIdentification = new SRcOnly();
            replyFillCmUnitIdentification.rc = CMSingleCommand("F," + n + ",10," + identification, 3, 5000);
            return replyFillCmUnitIdentification.rc;
        }

        /// <summary>
        /// With this command it is possible to set the number of the cassettes of the device.
        /// </summary>
        /// <param name="numCas">Number of cassettes installed (4, 6, 8, 10, 12)</param>
        /// <returns></returns>
        public string CMFillCassetteNumber(int numCas = 8)
        {
            replyFillCmCassetteNumber = new SRcOnly();
            replyFillCmCassetteNumber.rc = CMSingleCommand("F," + n + ",11," + numCas, 3, 5000);
            return replyFillCmCassetteNumber.rc;
        }

        /// <summary>
        /// With this command it is possible to set the maximum number of banknotes in output slot during the operation of withdrawal(the default is 200)
        /// </summary>
        /// <param name="numNotes">Number of notes</param>
        /// <returns></returns>
        public string CMFillWithdrawalBundleSize(int numNotes = 200)
        {
            replyFillWithdrawallBundleSize = new SRcOnly();
            replyFillWithdrawallBundleSize.rc = CMSingleCommand("F," + n + ",15," + numNotes, 3, 5000);
            return replyFillWithdrawallBundleSize.rc;
        }

        /// <summary>
        /// With this command it is possible to set the number of banknotes in output slot during a counting operation(the default is 200)
        /// </summary>
        /// <param name="numNotes">>Number of notes</param>
        /// <returns></returns>
        public string CMFillCountingBundleSize(int numNotes = 200)
        {
            replyFillCountingBundleSize = new SRcOnly();
            replyFillCountingBundleSize.rc = CMSingleCommand("F," + n + ",18," + numNotes, 3, 5000);
            return replyFillCountingBundleSize.rc;
        }

        /// <summary>
        /// With this command it is possible to set the number of the cassettes used by communication protocol. It’s possible to configure this parameter only with CM18T because CM18/CM18E use fixed cassettes number to 8 whereas CM20 / CM20E use fixed cassettes number to 10.
        /// </summary>
        /// <param name="numProtCas">Number of cassette in protocol</param>
        /// <returns></returns>
        public string CMFillProtocolCassetteNumber(int numProtCas = 8)
        {
            replyFillCmProtCassNumber = new SRcOnly();
            replyFillCmProtCassNumber.rc = CMSingleCommand("F," + n + ",19," + numProtCas, 3, 5000);
            return replyFillCmProtCassNumber.rc;
        }

        /// <summary>
        /// With this command it is possible to set the number of CD80 cassettes
        /// </summary>
        /// <param name="numCD80">Number of CD80 cassette installed (0, 2, 4)</param>
        /// <returns></returns>
        public string CMFillCD80CassetteNumber(int numCD80 = 0)
        {
            replyFillCD80CassNumber = new SRcOnly();
            replyFillCD80CassNumber.rc = CMSingleCommand("F," + n + ",30," + numCD80, 3, 5000);
            return replyFillCD80CassNumber.rc;
        }


        /// <summary>
        /// Check the status of all the unit covers/door/input/output slots, such as Safe door, top cover, input slot, etc.
        /// </summary>
        /// <returns></returns>
        public string CMUnitCoverTest()
        {
            replyUnitCoverTest = new SUnitCoverTest();
            replyUnitCoverTest.rc = CMSingleCommand("T," + n + ",0,0", 4, 5000);
            replyUnitCoverTest.safeDoor = commandSingleAns[5];
            replyUnitCoverTest.cover = commandSingleAns[7];
            replyUnitCoverTest.feeder = commandSingleAns[8];
            replyUnitCoverTest.inputSlot = commandSingleAns[9];
            replyUnitCoverTest.rejectSlot = commandSingleAns[10];
            replyUnitCoverTest.leftSlot = commandSingleAns[11];
            replyUnitCoverTest.rightSlot = commandSingleAns[12];

            return replyUnitCoverTest.rc;
        }

        /// <summary>
        /// This command is used to ENTER in transparent mode
        /// </summary>
        /// <returns></returns>
        public string CMTransparentIn()
        {
            replyTransparentState = new STransparentState();
            replyTransparentState.rc = CMSingleCommand("X," + n, 2, 5000);
            if (replyTransparentState.rc == "101" | replyTransparentState.rc == "1")
            {
                replyTransparentState.state = "ON";
            }
            return replyTransparentState.rc;
        }

        /// <summary>
        /// This command is used to EXIT from transparent mode
        /// </summary>
        /// <returns></returns>
        public string CMTransparentOut()
        {
            replyTransparentState = new STransparentState();
            replyTransparentState.rc = CMSingleCommand("Y," + n, 2, 5000);
            if (replyTransparentState.rc == "101" | replyTransparentState.rc == "1")
            {
                replyTransparentState.state = "OFF";
            }
            return replyTransparentState.rc;
        }

        /// <summary>
        /// This command send a Transparent command. It works only if the transparent mode is active
        /// </summary>
        /// <param name="command"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public string CMTransparentCommand(string command, int timeOut = 1000)
        {

            replyTransparentCommand = new STransparentCommand();

            if (replyTransparentState.state != "ON")
            {
                return "TRASP OFF";
            }
            int result;
            InputSingle.command = command;
            InputSingle.size = InputSingle.command.Length;
            OutputSingle.answer = new string(' ', 1700);
            OutputSingle.size = OutputSingle.answer.Length;
            commandSent = command;
            CommandSent(commandSent, null);
            //ComScope.AddListBoxItem("SND: " + commandSent);
            result = CMSingleCommand(hCon, TRANSPARENT, ref InputSingle, ref OutputSingle, timeOut);
            commandAns = OutputSingle.answer;
            CommandReceived(commandAns, null);
            replyTransparentCommand.answer = commandAns;
            //ComScope.AddItem("RCV: " + commandAns);
            commandSingleAns = commandAns.Split(',');
            try
            {

                return replyTransparentCommand.answer;
            }
            catch (System.IndexOutOfRangeException)
            {
                return result.ToString();
            }


        }
    }
    
}
