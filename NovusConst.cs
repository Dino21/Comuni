using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novus
{
    public static class novConst
    {
        public const string CASHDATA_TYPE_UNFITSAFE_LD = "unfitSafeLastDeposit";
        public const string CASHDATA_TYPE_UNFIT_LD = "unfitLastDeposit";
        public const string CASHDATA_TYPE_SUSPECT_NA = "suspectNotAuthenticated";
        public const string CASHDATA_TYPE_SUSPECT = "suspect";
        public const string CASHDATA_TYPE_UNFIT_CLASS_DET = "unfitClassificationDetails";
        public const string CASHDATA_TYPE_NOT_AUTH = "notAuthenticated";
        public const string STATUSTYPE_EXTENDED = "extended";
        public const string STATUSTYPE_POCKET_COVER = "pocketsAndCover";
        public const string DLINK_CMDMODE_SYNC = "SYNC";
        public const string DLINK_CMDMODE_ASYNC = "ASYNC";
        public const string DLINK_CONNTYPE_USB = "USB";
        public const string DLINK_CONNTYPE_LAN = "LAN";
        public const string DLINK_CONNTYPE_RS232 = "RS232";
        public const string DLINK_ENCRYPT_NONE = "NO_ENC";
        public const string DLINK_ENCRYPT_SSL = "SSL";
        public const string DLINK_PROTOCOL_STD = "STANDARD";
        public const string DLINK_PROTOCOL_EASY = "SIMPLIFIED";
        public const string PSW_STD = "123456";
        public const string OPSIDE_LEFT = "L";
        public const string OPSIDE_RIGHT = "R";
        public const string UPDATEVERSION_MODE_ACTIVATE = "activate";
        public const string UPDATEVERSION_MODE_INIT = "init";
        public const string UPDATEVERSION_MODE_CHECK = "check";
        public const string UPDATEVERSION_TYPE_SUITE = "Suite";
        public const string UPDATEVERSION_TYPE_REFERENCE = "Reference";
        public const string DEVICE_RESET_MODE_FORCED = "forced";

        // ------------------------------------------------------------------------
        //                      REPLY-CODE
        // ------------------------------------------------------------------------

        public const int AR_OK = 0;
        // ------------------------------------------------------------------------
        //                      ERRORS
        // ------------------------------------------------------------------------
        public const int AR_SYSTEM_ERROR = 1;
        public const int AR_UNIT_BUSY = 102;
        public const int AR_COVER_OPEN = 103;
        public const int AR_SAFE_OPEN = 104;

        public const int AR_OUT_OF_SERVICE = 106;

        public const int AR_SW_SYNTAX_ERROR = 201;
        public const int AR_NOTE_IN_LEFT_OUTPUT = 205;
        public const int AR_NOTE_IN_REJECT_POCKET = 206;
        public const int AR_NOTE_FEEDER_EMPTY = 207;
        public const int AR_NOTE_ON_FEEDER_INPUT = 208;
        public const int AR_CASSETTE_FULL = 209;
        public const int AR_DENOMINATION_EMPTY = 210;
        public const int AR_WRONG_SIDE = 213;
        public const int AR_NID_NOT_PRESENT = 217;
        public const int AR_RECORD_NOT_PRESENT = 219;


        public const int AR_LINK_SYSTEM_ERR = 1050; //"Function communication device error"
        public const int AR_LINK_CONNECTION_ERR = 1051; //"Invalid handle"
        public const int AR_LINK_ALREADY_CONNECTED_ERR = 1052;  //"Connection already connected"
        public const int AR_LINK_CONNECT_PARAMETERS_ERR = 1053; //"Syntax error on connect operation"
        public const int AR_LINK_WRITE_ERR = 1054;  //"WriteFile Function on device has failed"
        public const int AR_LINK_READ_ERR = 1055;   //"ReadFile Function on device has failed"
        public const int AR_LINK_LAN_OPEN_ERR = 1056;   //"Error on TCP/IP socket connection routines"
        public const int AR_LINK_LAN_WRITE_ERR = 1057;  //"Write on LAN has failed"
        public const int AR_LINK_LAN_READ_ERR = 1058;   //"Read on LAN has failed"
        public const int AR_LINK_TIMEOUT_ERR = 1059;    //"Time command has expired"
        public const int AR_LINK_OVERFLOW_ERR = 1060;   //"Buffer of reply command has been exeed"
        public const int AR_LINK_BCC_ERR = 1061;    //"Trasmission error, wrong BCC/char"
        public const int AR_LINK_PROTOCOL_ERR = 1062;   //"Trasmission error, wrong protocol "
        public const int AR_LINK_CDM_IN_PROGRESS = 1063;    //"Command in progress"
        public const int AR_LINK_TERMINATE_EMULATOR = 1064; //"Command in progress"
        public const int AR_LINK_SEQUENCE_ERR = 1065;   //"Command in progress"
        public const int AR_LINK_LAN_CONNECTION_REFUSED = 1066; //"LAN Connection refused 10061"
        public const int AR_LINK_SYNTAX_ERR = 1067; //"CMLINK Syntax Error"
        public const int AR_LINK_LAN_CONNECTION_RESET = 1068;   //"LAN 10054 - Connection reset by peer"
        public const int AR_LINK_LANSERVER_CLIENT_CLOSE = 1069; //"layer Close session by client in LanServer"
        public const int AR_LINK_LIBUSB_NOT_FOUND = 1070;   //"LibUsb not found"
        public const int AR_LINK_LIBUSB_ERROR = 1071;   //"LibUsb Error generic"

        //errori DLL..
        public const int AR_LIBRARY_NOT_INITIALIZED = 1700;
        public const int AR_OPEN_NOT_DONE = 1701;
        public const int AR_INVALID_LIBRARY_VERSIONING = 1710;
        public const int AR_JSON_MISSING_CONNECTIONTYPE = 1711;
        public const int AR_JSON_INVALID_CONNECTIONTYPE = 1712;
        public const int AR_JSON_MISSING_PROTOCOL_SET = 1713;
        public const int AR_JSON_INVALID_PROTOCOL_SET = 1714;
        public const int AR_JSON_MISSING_IP_ADDRESS = 1715;
        public const int AR_JSON_MISSING_IP_PORT = 1716;
        public const int AR_JSON_MISSING_USB_SERIALNUMBER = 1717;
        public const int AR_JSON_MISSING_COM_NAME = 1718;
        public const int AR_JSON_MISSING_COM_BAUDRATE = 1719;
        public const int AR_JSON_MISSING_COM_PARITY = 1720;
        public const int AR_JSON_MISSING_COM_DATABITS = 1721;
        public const int AR_JSON_MISSING_COM_STOPBITS = 1722;
        public const int AR_JSON_MISSING_OPSIDE = 1723;
        public const int AR_STRING_TRUNCATED = 1724;
        public const int AR_JSON_MISSING_CMD_MODE = 1725;
        public const int AR_JSON_INVALID_CMD_MODE = 1726;
        public const int AR_JSON_INVALID_PARITY = 1727;
        public const int AR_JSON_MISSING_NR_ELEMENTS = 1728;
        public const int AR_JSON_INVALID_DISPENSE_ELEMENT = 1729;
        public const int AR_JSON_MISSING_DRAWER_ID = 1730;
        public const int AR_JSON_INVALID_DRAWER_ID = 1731;
        public const int AR_JSON_MISSING_PASSWORD = 1732;
        public const int AR_JSON_MISSING_END_OPERATION = 1733;
        public const int AR_JSON_INVALID_END_OPERATION = 1734;
        public const int AR_JSON_MISSING_BOUNDLE_COUNTING_MODE = 1735;
        public const int AR_JSON_DEVICE_DATETIME_SECONDS = 1736;
        public const int AR_JSON_DEVICE_DATETIME_MINUTES = 1737;
        public const int AR_JSON_DEVICE_DATETIME_HOURS = 1738;
        public const int AR_JSON_DEVICE_DATETIME_WEEK_DAY = 1739;
        public const int AR_JSON_DEVICE_DATETIME_DAY = 1740;
        public const int AR_JSON_DEVICE_DATETIME_MONTH = 1741;
        public const int AR_JSON_DEVICE_DATETIME_YEAR = 1742;

        public const int AR_REPLY_IN_OUTDATA_TRUNCATED = 1788;
        public const int AR_INSUFFICIENT_SIZE_outdata_BUFFER = 1789;
        public const int AR_INVALID_HCONNECT = 1790;
        public const int AR_INVALID_INDATA_JSON = 1791;
        public const int AR_DEVLNK_UNKNOWN_REPLY = 1798;
        public const int AR_DEVICE_UNKNOWN_REPLY = 1799;

        //da gestire ...
        public const int AR_APP_GENERIC_ERROR = 3001;
        public const int AR_APP_UNAUTHORIZED = 3002;
        public const int AR_APP_LIC_DATES_MISS = 3003;
        public const int AR_APP_WRONG_DATES = 3004;
        public const int AR_APP_LIC_USERNAME_ERR = 3005;
        public const int AR_APP_WRONG_USERNAME = 3006;
        public const int AR_APP_LIC_PASSW_ERR = 3007;
        public const int AR_APP_WRONG_PWD = 3008;
        public const int AR_APP_LIC_DATE_EXPIRED = 3009;
        public const int AR_APP_ARCALD_BASE = 3020;
    }
}
