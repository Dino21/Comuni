using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NovusTool;

namespace NovusTool
{
    class JsonFunctioncs
    {
        //queste sono le define di ARCADevInt.h e ARCAdevsvc.h...devono sempre essere allineate !
        public const string INITILIZE_TRACE_PATH = "pathtrace";
        public const string REPLY_CONTENT = "reply";
        public const string REPLY_CODE = "replyCode";
        public const string REPLY_CODE_DESCRIPTION = "replyCodeDescription";
        public const string HEADER_CONTENT = "content";
        public const string OPERATION_OUTPUT_MESSAGE = "message";
        public const string OPERATION_PASSWORD = "password";
        public const string OPERATION_ENABLE	="enable";
        public const string OPERATION_DISABLE=	"disable";
        public const string OPEN_OPSIDE = "opSide";

        public const string MONDAY = "Monday";
        public const string TUESDAY = "Tuesday";
        public const string WEDNESDAY = "Wednesday";
        public const string THURSDAY = "Thursday";
        public const string FRIDAY = "Friday";
        public const string SATURDAY = "Saturday";
        public const string SUNDAY = "Sunday";

        // Parameter Operation
        public const string COUNTING_START = "StartCounting";
        public const string COUNTING_CONTINUE = "ContinueCounting";
        public const string COUNTING_FULL = "FullCounting";
        public const string COUNTING_START_SAMPLING = "startsampling";



        //connection ...
        public const string CONNECTION_ENCRIPTION = "encryption";
        public const string CONNECTION_ENCRIPTION_SSL = "SSL";
        public const string CONNECTION_ENCRIPTION_TLS = "TLS";

        public const string CONFIG_TYPE = "configType";
        public const string CONFIG_TYPE_SWITCH = "switch";
        public const string CONFIG_TYPE_CONFIG = "config";
        
        public const string CONFIG_TYPE_REF = "references";
        public const string CONFIG_BANK = "bank";
        public const string CONFIG_CASSETTE_ID = "cassetteID";
        public const string CONFIG_CASSETTE_STATUS = "cassetteStatus";

        public const string CONFIG_CASSETTE_ENABLED = "enabled";
       

        public const string CONFIG_DENOMINATION_ID = "denomination";
        public const string CONFIG_DENOMINATION_STATUS = "denominationStatus";

        //public const string TARGET_READER = "3";
        public const string CONFIG_ENABLED = "enable";
        public const string CONFIG_DISABLED = "disable";

        public const string DEPOSIT_END_ACCEPTED = "accept";
        public const string DEPOSIT_END_REJECTED = "undo";
        public const string DEPOSIT_END_LIST = "list";
        public const string SAFE_REPORT = "safeReport";
        public const string NOTE_REPORT = "noteReport";

        public const string DEPOSIT_CASSETTE_DETAIL = "cassetteDetails";
        public const string DEPOSIT_DENOM_ID = "denomID";
        public const string DEPOSIT_MODE_TYPE = "mode";

        public const string DEPOSIT_MODE_TYPE_CONTINUOUS = "continuousFeeding";
        public const string DEPOSIT_MODE_TYPE_CONTINUOUS_TIME = "continuousFeedingTime";
        public const string DEPOSIT_MODE_TYPE_CONTINUOUS_ACTION = "continuousFeedingAction";

        public const string DEPOSIT_MODE_TYPE_NATION = "nation";
        public const string DEPOSIT_MODE_TYPE_FORCED = "forced";
        public const string DEPOSIT_FORCED_MODE = "forcedMode";
        
        public const string DEPOSIT_CASSETTE_ID = "storageID";
        public const string DEPOSIT_CASSETTE_CONF = "storageConf";
        public const string DEPOSIT_NOTE_QTY = "noteQty";

        //--------------------------- Defines for ARCACount -------------
        public const string COUNTING_REPORT = "countReport";
        public const string COUNTING_TOTAL_NOTE = "totalNoteCounted";
        public const string COUNTING_REFUSED = "refused";
        public const string COUNTING_UNRECOGNIZED = "unrecognized";
        public const string COUNTING_NOTE_REPORT_ELEMENTS = "noteReportElements";
        public const string COUNTING_NOTE_REPORT = "noteReport";
        public const string COUNTING_NOTE_TYPE = "noteType";
        public const string COUNTING_NUMBER_NOTE = "numberNote";

        public const string COUNTING_MODE_TYPE = "mode";
        public const string COUNTING_MODE_TYPE_STD_COUNT = "standardCount";
        public const string COUNTING_MODE_TYPE_FIT = "fit";
        public const string COUNTING_MODE_TYPE_SAMPLING = "sampling";
        public const string COUNTING_MODE_SINGLE_EMISSION = "singleEmis";
        public const string COUNTING_MODE_SINGLE_EMISSION_FIT = "singleEmisFit";
        public const string COUNTING_MODE_MULTI_EMISSION = "multiEmis";
        public const string COUNTING_MODE_MULTI_EMISSION_FIT = "multiEmisFit";

        public const string COUNTING_CMD_MODE = "cmdMode";
        public const string COUNTING_BOUNDLE_SIZE = "boundleSize";
        public const string COUNTING_BOUNDLE_COUNTING_MODE = "boundleCountingMode";
        public const string COUNTING_CLASS = "class";
        public const string COUNTING_WAY = "way";
        public const string COUNTING_SESSION = "session";
        public const string COUNTING_CAT = "cat";
        public const string COUNTING_FIT = "fit";
        public const string COUNTING_NOTE = "note";
        public const string COUNTING_DENOM_ID = "denomID";

        //--------------------------- Defines for ARCACashData -------------
        public const string DEPOSIT_MANAGE_LIGHT = "manageLights";
        public const string DEPOSIT_MANAGE_LIGHT_TRUE = "true";

        public const string CASH_DATA_NOTE_TYPE      = "noteType";
        public const string CASH_DATA_NOTE_TYPE_UNFIT   = "unfitSafeLastDeposit";
        public const string CASH_DATA_NOTE_TYPE_UNFIT_DETAILS = "unfitLastDeposit";
        public const string CASH_DATA_NOTE_TYPE_SUSPECT_NOT_AUTH = "suspectNotAuthenticated";
        public const string CASH_DATA_NOTE_TYPE_NOT_AUTHENTICATED = "notAuthenticated";
        public const string CASH_DATA_NOTE_UNFIT_CLASSIF_DETAILS = "unfitClassificationDetails";
        public const string CASH_DATA_NOTE_TYPE_SUSPECT = "suspect";
        public const string CASH_DATA_ADVANCED = "advanced";
        public const string CASH_DATA_NOTE_TYPE_UNFIT_OUTPUT = "unfitOutputLastDeposit";
                        
        
        public const string OUTPUT_NOTE_TYPE = "noteType";
        public const string OUTPUT_NOTE_REPORT = "NoteReport";
        public const string OUTPUT_NOTE_NR_NOTES = "numberNote";
        public const string OUTPUT_NOTE_REPORT_ELEMENTS = "NoteReportElements";

        public const string SLOT_STATISTIC_DESCRIPTION = "description";
        public const string SLOT_STATISTIC_COUNT = "count";

        public const string SLOT_STATISTIC_NPROC = "nProc";
        public const string SLOT_STATISTIC_NACCEPT = "nAccept";
        public const string SLOT_STATISTIC_NREFUSED = "nRefused";
        public const string SLOT_STATISTIC_NSUSP = "nSusp";
        public const string SLOT_STATISTIC_NOT_AUTH = "nNotAuth";
        public const string SLOT_STATISTIC_NCLSF = "nClsf";
        public const string SLOT_STATISTIC_NRID = "nIrd";
        public const string SLOT_STATISTIC_NMAG = "nMag";
        public const string SLOT_STATISTIC_NFORMAT = "nFormat";
        public const string SLOT_STATISTIC_NUNFIT = "nUnfit";
        public const string SLOT_STATISTIC_NOVERRUN = "nOverrun";
        public const string SLOT_STATISTIC_NDISABLECHAIN = "nDisableChain";
        public const string SLOT_STATISTIC_NUV = "nUv";
        public const string SLOT_STATISTIC_NEXTREMEFIT = "nExtremeFit";
        public const string SLOT_STATISTIC_NGENERROR = "nGenericError";
        public const string SLOT_STATISTIC_NOCRCHECK = "nOcrCheck";

        public const string SLOT_STATISTIC_NSKEW = "nSkew";
        public const string SLOT_STATISTIC_NDOUBLE = "nDouble";
        public const string SLOT_STATISTIC_NDIMENSION = "nDimension";
        public const string SLOT_STATISTIC_NABORT = "nAbort";
        public const string SLOT_STATISTIC_NLOCALIZZ = "nLocalizz";
        public const string SLOT_STATISTIC_NOTHER = "nOther";
        public const string SLOT_STATISTIC_NACCFIT = "nAccFit";
        public const string SLOT_STATISTIC_NACCUFIT = "nAccUnFit";
        public const string SLOT_STATISTIC_NRECJBYRTC = "nRejRtc";


        public const string DEPOSIT_NOTE_REPORT_ELEMENTS = "noteReportElements";
        public const string DEPOSIT_NOTE_REPORT = "noteReport";
        public const string DEPOSIT_NOTE_TYPE = "noteType";
        public const string DEPOSIT_NUMBER_NOTE = "numberNote";

        public const string DISPENSE_MODE = "alternateDispense";
        //public const string DISPENSE_MODE_ALTERNATE = "timeDispense";

        public const string DEVICE_VERSION_TYPE = "versionType";
        public const string DEVICE_VERSION_TYPE_RTC	="rtc";
        public const string DEVICE_VERSION_TYPE_IDENTIFIER = "reader";
        public const string DEVICE_VERSION_TYPE_SAFE = "safe";
        public const string DEVICE_VERSION_TYPE_FPGA = "fpga";
        public const string DEVICE_VERSION_TYPE_OSC = "osc";
        public const string DEVICE_VERSION_TYPE_SHUTTER = "shutter";
        public const string DEVICE_VERSION_UI = "userInterface";
        public const string DEVICE_VERSION_DISPLAY = "displayVersion";

        public const string DEVICE_VERSION_TYPE_CASSETTE_A = "cassetteA";
        public const string DEVICE_VERSION_TYPE_CASSETTE_B = "cassetteB";
        public const string DEVICE_VERSION_TYPE_CASSETTE_C = "cassetteC";
        public const string DEVICE_VERSION_TYPE_CASSETTE_D = "cassetteD";
        public const string DEVICE_VERSION_TYPE_CASSETTE_E = "cassetteE";
        public const string DEVICE_VERSION_TYPE_CASSETTE_F = "cassetteF";
        public const string DEVICE_VERSION_TYPE_CASSETTE_G = "cassetteG";
        public const string DEVICE_VERSION_TYPE_CASSETTE_H = "cassetteH";
        public const string DEVICE_VERSION_TYPE_CASSETTE_I = "cassetteI";
        public const string DEVICE_VERSION_TYPE_CASSETTE_J = "cassetteJ";
        public const string DEVICE_VERSION_TYPE_CASSETTE_K = "cassetteK";
        public const string DEVICE_VERSION_TYPE_CASSETTE_L = "cassetteL";
        public const string DEVICE_VERSION_TYPE_BOOT_V = "Boot version";
        public const string DEVICE_VERSION_NAME_CONTROLLER = "controller_name";
        public const string DEVICE_VERSION_SUITE = "suite_name";
        public const string DEVICE_VERSION_SUITE_FF = "suite";

        public const string OPERATION_CONFIG_TYPE  = "configType";
        public const string GET_CASSETTE_NUMBER = "cassetteNumber";
        public const string CASSETTE_NUMBER = "numCassette";
        //public const string OPERATION_CASSETTE_PROTOCAS_SVC = "numSvcProtoCas";

        public const string PROTOCOL_CASSETTE_NR = "protocolCassetteNumber";
        public const string PROTOCOL_CASSETTE_PROTOCAS = "numProtoCas";


        public const string FILE_TRANSFER_TYPE = "TransferType";
        public const string FILE_TRANSFER_MODE = "TransferMode";
        public const string FILE_TRANSFER_FILE = "TransferFile";
        public const string FILE_TRANSFER_DOWNLOAD = "Download";
        public const string FILE_TRANSFER_UPLOAD = "Upload";
        public const string FILE_TRANSFER_DOWNLOAD_INIT = "Init";
        public const string FILE_TRANSFER_DOWNLOAD_TRANSFER = "Transfer";
        public const string FILE_TRANSFER_DOWNLOAD_END = "End";
        public const string FILE_TRANSFER_DOWNLOAD_ABORT = "Abort";
        public const string FILE_TRANSFER_DOWNLOAD_SERVICE = "Service";
        public const string FILE_TRANSFER_FILE_LOG = "Log";
        public const string FILE_TRANSFER_FILE_SUITE = "Suite";
        public const string FILE_TRANSFER_FILE_MODULE = "Module";
        public const string FILE_TRANSFER_FILE_LOG_NAME = "ZipfileName";

        
      
        public const string OPERATION_CONN_VALUE = "CMxxx";
        public const string OPERATION_SET_OUTPUT_SETTING_BUNDLE = "bundleSize";

        //defines for ARCAsetReaderSettings
        public const string READER_SETTING_TYPE = "type";
        public const string READER_SETTING_NBANKS = "numBanks";
        public const string READER_SETTING_ACTIVEBANKS = "activeBanks";
        public const string READER_SETTING_BANK_NUM = "bank_num";
        public const string READER_SETTING_BANK_TO_ACT = "bank_to_act";
        public const string READER_SETTING_REFERENCES = "references";
        public const string READER_SETTING_REF_1 = "enable_ref1";
        public const string READER_SETTING_REF_2 = "enable_ref2";
        public const string READER_SETTING_REF_3 = "enable_ref3";
        public const string READER_SETTING_REF_4 = "enable_ref4";
        public const string READER_SETTING_SLOT = "slot";
        public const string READER_SETTING_BANKID = "bankId";





        public const string UNIT_STATUS_STATUS_TYPE = "statusType";
        public const string UNIT_STATUS_STATUS_POCKET = "pocketsAndCover";
        public const string UNIT_STATUS_STATUS_EXTEND = "extended";

        public const string UNIT_STATUS_FEEDER = "feeder";
        public const string UNIT_STATUS_RTC = "RTC";
        public const string UNIT_STATUS_NOTE_READER = "noteReader";
        public const string UNIT_STATUS_SAFE_CONTROLLER			=	"safeController";
        public const string UNIT_CASSETTE_CODE = "cassetteCode";
      

        public const string SLOT_STATUS_SAFE_DOOR   =       "safeDoor";
        public const string SLOT_STATUS_UPPER_COVER =       "cover";
        public const string SLOT_STATUS_FEEDER =            "feeder";
        public const string SLOT_STATUS_INPUT_SLOT =        "inputSlot";
        public const string SLOT_STATUS_REJECT_SLOT =       "rejectSlot";
        public const string SLOT_STATUS_LEFT_SLOT =         "leftSlot";
        public const string SLOT_STATUS_RIGHT_SLOT =        "rightSlot";
        public const string SLOT_STATUS_LEFT_LIGHT =        "leftexternbutton";
        public const string SLOT_STATUS_RIGHT_LIGHT =       "rightexternalbutton";
        public const string SLOT_STATUS_FKP_POCKET =        "Cat2Boxpresent";
        public const string SLOT_STATUS_FKS_POCKET   =      "Cat2Boxstatus";
        public const string SLOT_STATUS_CLOSED          =   "closed";
        public const string SLOT_STATUS_OPEN         =      "open";
        public const string SLOT_STATUS_EMPTY			=   "empty";
        public const string SLOT_STATUS_NOT_EMPTY	=		"not empty";
        public const string SLOT_STATUS_LIGHT_OFF	=		"light off";
        public const string SLOT_STATUS_LIGHT_ON	=		"ligth on";
        public const string SLOT_STATUS_PRESENT		=	"present";
        public const string SLOT_STATUS_NOT_PRESENT	=	"not present";

        //define for ARCAexeSecurity
        public const string SECURITY_AREA = "area";
        public const string SECURITY_UPPER = "upperTransport";
        public const string SECURITY_OPERATION = "operation";
        public const string SECURITY_OPERATION_LOCK = "lock";
        public const string SECURITY_OPERATION_UNLOCK = "unlock";

        //unit config
        public const string TRUE_R = "true";
        public const string FALSE_R = "false";
        //Config
        public const string HEADER_CONFIG = "Config";
        public const string OPTION_DATE_DISPLAY = "viewDateTimeOnDisplay";
        public const string OPTION_BALANCED_CASSETTE = "balancedCassetteHandling";
        public const string OPTION_ALARM_1 = "alarm1Handling";
        public const string OPTION_DELAY = "delayClassDispensing";
        public const string OPTION_DATE_DISPLAY_AM_PM = "viewDateTimeOnDisplayInFormatAMandPM";
        public const string OPTION_UNFIT_SAFE = "useUnfitinSafe";
        public const string OPTION_JOURNAL_LOG = "enableJournallog";

        //OptionConfig
        public const string HEADER_OPTIONCONFIG = "OptionConfig";
        public const string OPTION_SPECIAL_CLEAN = "specialCleanWithoutControl";
        public const string OPTION_SPECIAL_ALARM = "specialAlarmMode";
        public const string OPTION_SPECIAL_PROTOCOL = "simplifiedProtocol";
        public const string OPTION_SPECIAL_CLOSE = "executeCloseCommandAlsoInJam";

        //OptionOneConfig
        public const string HEADER_OPTION_ONE_CONFIG = "OptionOneConfig";
        public const string OPTION_FORCE_RECOVERY = "forceRecoveryWithTestCommand";
        public const string OPTION_JAM_SPECIAL_CLEAN = "donotremovecassettejamifspecialcleanON";
        public const string OPTION_LAN_TIME_OUT = "lanTimeout";
        public const string OPTION_TCDA = "timeControlDispenseamount";
        public const string OPTION_BOOKING_EXTERNAL = "enableBookingExternalButton";

        //OptionTwoConfig
        public const string HEADER_OPTION_TWO_CONFIG = "OptionTwoConfig";
        public const string OPTION_SAFE_40MM = "safeCen40Bit"; 
        public const string OPTION_BVU_HANDLING = "bvuSynchronousHandling";
        public const string OPTION_FACE_ORIENTATION = "faceOrientationHandling";
        public const string OPTION_SPECIAL_CLEAN_DISPLAY = "specialCleanUsingDisplay";
        public const string OPTION_CAT2BOX_HANDLING = "cat2BoxHandling";
        public const string OPTION_EVOLED_HANDLING = "evoLedHandling";
        public const string OPTION_SEPARATE = "separateCat2andCat3";
        public const string OPTION_PUSH_BN = "feedBanknoteswithInputBinFree";
        public const string OPTION_DIFFERENT_CENTER = "differentCenterAUDbn";
        public const string OPTION_DISABLE_MESSAGE = "disableMessageonDisplay";
        public const string OPTION_ALARM2 = "alarm2OutputHandling";
        public const string OPTION_REMAP_JAM = "remapFeederJamtoMissfeeding";
        public const string OPTION_VIRTUAL_D_ENV = "virtualDestkopEnvironment";
        public const string OPTION_NEW_LED_SIDE = "handlingNewLedSide";
        public const string OPTION_DISABLE_LED = "disableCM18evoLedPulse";

        public const string EMPTY_CASSETTE_BOUNDLE_SIZE = "outBoundleSize";

        public const string OPERATION_MODE_UPDATE = "updateMode";
        public const string OPERATION_MODE_UPDATE_TYPE = "updateType";

        public const string OPERATION_UPDATE_FILE_NAME_SUITE     =  "Suite";
        public const string OPERATION_UPDATE_FILE_NAME_REFERENCE = "Reference";
        public const string OPERATION_UPDATE_FILE_NAME_SSL_CERTIFICATE = "SSLcertificate";
        public const string OPERATION_UPDATE_FILE_NAME_SSL_PRIVATE_KEY = "SSLprivatekey";

        public const string OPERATION_UPDATE_FILE_NAME_IPSEC_CERTIFICATE = "IpsecCertificate";
        public const string OPERATION_UPDATE_FILE_NAME_IPSEC_CA_KEY = "IpsecCa";

        public const string OPERATION_MODE_UPDATE_INIT = "init";
        public const string OPERATION_MODE_UPDATE_ACTIVATE = "activate";

        public const string OPERATION_DOWNLOAD_FILE_NAME    = "FileName" ;
        public const string OPERATION_DOWNLOAD_FILE_SIZE	= "FileSize";
        public const string OPERATION_DOWNLOAD_FILE_PATH    = "FilePath";

        public const string OPERATION_TYPE = "operationType";
        public const string OPERATION_NUMBER = "number";
        public const string OPERATION_CONFIGURATION = "configuration";

        //Reader
        public const string FIT_CONFIGURATION = "fitConfiguration";
        public const string FIT_THRESHOLD = "fitThreshold";
        public const string FIT_VERSION = "fitVersion";

        public const string THRESHOLD_FIT_FLAG = "fitnessFlag";
        public const string THRESHOLD_FIT_THRESHOLD = "fitnessThreshold";
        public const string THRESHOLD_FITNESS_V = "fitnessVer";
        //--------------------------- Defines for ARCAsetReaderThreshold -------------


        public const string DEVSVC_FIT_CONFIGURATION = "fitConfiguration";
        public const string DEVSVC_FIT_THRESHOLD = "fitThreshold";
        public const string DEVSVC_THRESHOLD_FIT_FLAG = "fitFlag";
        public const string DEVSVC_THRESHOLD_FIT_T_FITNESS = "fitThresholdFitness";

        public const string READER_GET_CONFIG_ACTIVE_BANK = "activeBank";
        public const string READER_GET_CONFIG_BANK = "bank";

        public const string GETLOG_CONFIG_TYPE = "type";
        public const string GETLOG_CONFIG_TYPE_ERROR_LOG = "errorlog";
        public const string GETLOG_RECORD_NUMBER = "lineNumber";

        public const string GET_LOG_HEADER = "logHeader";
        public const string GETLOG_RECORD_LIFE = "life_num";



        //defines for ARCAgetPocketOutput----------------------------------------------
        public const string GET_BUNDLE_SIZE = "bundleSize";

        //defines for ARCAgetInputtSetting----------------------------------------------
        public const string OPERATION_SET_INPUT_SETTING_BUNDLE = "countingBundleSize";


        //--------------------------- Defines for ARCAgetCommunicationLAN -------------
        //--------------------------- Defines for ARCAsetCommunicationLAN -------------
        public const string LAN_PARAM =                          "lanParam";
        public const string LAN_PARAM_PORT =                     "lanPort";
        public const string IN_LAN_PARAM_VALUE =                "lanParamValue";
        public const string LAN_PORT_STATUS =                   "lanPortStatus";
        public const string LAN_PORT_VALUE =                    "lanPortValue";

        
        public const string IN_LAN_PARAM_WHITELIST =            "whitelist";
        public const string IN_LAN_OP_TYPE =                    "opType";
        public const string LAN_ADDR_TYPE =                     "addressType";
        public const string LAN_ADDR =                          "address";


    
        //--------------------------- Defines for ARCAsetCommunication -------------
        public const string OPERATION_CONN_VALUE_LAN_ENC = "lanEncryption";


        //--------------------------- Defines for ARCAexeCoinDispense -------------
        //IN
        public const string COIN_AMOUNT = "amount";
        public const string COIN_DENOMINATION = "denomination";
        public const string COIN_DISPENSE_DENOMINATION_ID = "denominationID";
        public const string COIN_DISPENSE_ID_VALUE = "value";
        //OUT
        public const string COIN_STATUS_BYTE = "statusByte";

        //--------------------------- Defines for ARCACoinInventory -------------
        //IN
        public const string COIN_CLEAR_INVENTORY = "clear";
        public const string COIN_LOAD_INVENTORY = "load";

        //--------------------------- Defines for ARCAgetCoinConfiguration -------------
        //IN
        public const string IN_COIN_PARAM = "coinParamId";
        //OUT
        public const string IN_COIN_PARAM_VALUE		=		"coinParamValue";
        //--------------------------- Defines for ARCAgetCoinCashData -------------
        public const string COIN_AVAILABLE_AMOUNT = "availableAmount";
        public const string COIN_AVAILABLE_DISPENSE = "dispensedAmount";
        public const string COIN_CLEAR_CASH_BALANCE = "clearCashBalance";


        public const string OUT_COIN_HOPPER_0 = "0";
        public const string OUT_COIN_HOPPER_1 = "1";
        public const string OUT_COIN_HOPPER_2 = "2";
        public const string OUT_COIN_HOPPER_3 = "3";
        public const string OUT_COIN_HOPPER_4 = "4";
        public const string OUT_COIN_HOPPER_5 = "5";
        public const string OUT_COIN_HOPPER_6 = "6";
        public const string OUT_COIN_HOPPER_7 = "7";

        public const string OUT_COIN_HOPPER_VALUE = "d";
        public const string OUT_COIN_HOPPER_LIST = "a";
        public const string OUT_COIN_HOPPER_TYPE = "t";

        //--------------------------- Defines for ARCAgetOperatingHours -------------
        //IN
        public const string OPERATING_HOURS_TYPE = "operatingHoursType";
        public const string OPERATING_HOURS_TYPE_AP = "aperiodic";
        public const string OPERATING_HOURS_TYPE_P = "periodic";
        public const string APERIODC_DAY = "day";
        public const string APERIODC_MONTH = "month";
        public const string APERIODC_YEAR = "year";
        //OUT
         public const string OPERATING_HOURS_DAY = "weekDay";
        public const string OPERATING_HOURS_SWTICH_ON = "switchOn";
        public const string OPERATING_HOURS_SWTICH_OFF = "switchOff";
        public const string OPERATING_HOURS_LUNCH_BREAK_ON = "lunchBreakOn";
        public const string OPERATING_HOURS_LUNCH_BREAK_OFF = "lunchBreakOff";

        //--------------------------- Defines for ARCAexeAlarm -------------
        public const string LOCK_MODE = "lockMode";
        public const string LOCK_ON = "lockOn";
        public const string LOCK_OFF = "lockOff";

        //--------------------------- Defines for ARCAgetTimeZone -------------
        public const string TIME_ZONE_INDEX = "timeZoneIndex";

        //--------------------------- Defines for ARCAgetUsbPortStatus -------------
        public const string PORT_USB_STATUS = "statusUsb";
        public const string PORT_USB = "portUsb";

        ////--------------------------- Defines for ARCAsetDeviceSettings-------------------------------------------------
        public const string OPERATION_TYPE_CM = "value";
        public const string OPERATION_TYPE_CMD_TYPE = "type";
        public const string OPERATION_TYPE_VERSION = "version";
        public const string OPERATION_TYPE_MACHINE = "machine";
        public const string OPERATION_TYPE_CM18 = "CM18";
        public const string OPERATION_TYPE_CM18T = "CM18T";
        public const string OPERATION_TYPE_CM18E =    "CM18E";
        public const string OPERATION_TYPE_EVO = "EVO";
        public const string OPERATION_TYPE_STD = "STD";



        //defines for ARCAgetDevicePhoto----------------------------------------------
        public const string PHOTO_TARGET_MODULE = "targetModule";

        public const string PHOTO_TARGET_MODULE_ALL = "all";
        public const string PHOTO_TARGET_MODULE_UPPER = "upper";
        public const string PHOTO_TARGET_MODULE_LOWER = "lower";
        public const string PHOTO_TARGET_MODULE_BINS = "bins";
        public const string PHOTOS_ID = "phId";

        //[OUT]
        public const string PHOTO_RESULT			    = "resultCode";
        public const string PHOTO_RESULT_DESCRIPTION    = "resultDescription";
        public const string PHOTO_THRESHOLD             = "threshold";
        public const string PHOTO_AGC                   = "agc";
        public const string PHOTO_CURRENT               = "current";


        //CM18 xx section ...
        public const string PHOTO_PH_IN_CENTER = "ph_incenter";
        public const string PHOTO_PH_IN_LEFT = "ph_inleft";
        public const string PHOTO_PH_FEED = "ph_feed";
        public const string PHOTO_PH_C1 = "ph_c1";
        public const string PHOTO_PH_SHIFT = "ph_shift";
        public const string PHOTO_PH_INQ = "ph_inq";
        public const string PHOTO_PH_COUNT = "ph_count";
        public const string PHOTO_PH_OUT = "ph_out";
        public const string PHOTO_PH_C3 = "ph_c3";
        public const string PHOTO_PH_C4A = "ph_c4a";
        public const string PHOTO_PH_C4B = "ph_c4b";
        public const string PHOTO_PH_REJ = "ph_rej";
        public const string PHOTO_PH_IN_BOX = "ph_in_box";
        public const string PHOTO_PH_CASH_LEFT = "ph_cashleft";
        public const string PHOTO_PH_CASH_RIGHT = "ph_cashright";
        public const string PHOTO_H_MAX = "ph_hmax";
        public const string PHOTO_ALL = "ph_all";
        public const string PHOTO_UPPER_PART = "ph_upper";
        public const string PHOTO_LOWER_PART = "ph_lower";


        //--------------------------- Defines for ARCASampling -------------------------------
        public const string SAMPLING_INIT = "init";
        public const string SAMPLING_UNDO_LN = "undoLastNote";
        public const string SAMPLING_UNDO_B = "undoLastBatch";
        public const string SAMPLING_UNDO_SESSION = "undoSession";
        public const string SAMPLING_ACCEPT = "accept";
        public const string SAMPLING_LIST = "list";
        public const string SAMPLING_MODE = "mode";
        public const string SAMPLING_SESSION ="session";

        //--------------------------- Defines for  ARCAsetCommunicationSerial-------------
        public const string PORT_SERIAL_STATUS = "statusSerial";
        public const string PORT_SERIAL = "portSerial";
        public const string OPERATION_OFF = "off";
        public const string OPERATION_PERIPHERAL = "peripheral";
        public const string OPERATION_HOST = "host";


        //--------------------------- Defines for ARCAsetAlarm -------------
        public const string OPERATION_MODULE_ALARM = "alarmID";
        public const string OPERATION_MODULE_ALARM_STATUS = "alarmStatus";

        public const string OPERATION_MODULE_ALARM_1 = "alarm1";
        public const string OPERATION_MODULE_ALARM_2 = "alarm2";
        public const string OPERATION_MODULE_ALARM_SILENT = "alarmSilent";


        //Funzioni generiche    
        public static string ReadJsonTextObjet(string jsonFilePath, string Language_Type, string Nameobject)
        {
            bool ffoundText = false;
            string Description;

            Language_Type = Language_Type.ToLower();

            string jsonFile = File.ReadAllText(jsonFilePath);

            ffoundText = false;
            JObject rss = JObject.Parse(jsonFile);
            var obj = (JObject)rss;
            var userObj = JObject.Parse(jsonFile);
            foreach (var property in obj.Properties())
            {
                if (property.Name == Nameobject)
                {
                    ffoundText = true;
                    break;
                }
            }

            if (ffoundText == true)
            {
                var jsonDes = JsonConvert.DeserializeObject<dynamic>(jsonFile);

                Description = jsonDes[Nameobject][Language_Type];
            }
            else
                Description = " Not Found in file";

            return Description;

        }

        public static void ReadJsonError(string Language_Type, string ErrorCode, ref string ErrorCodeDescription)
        {
            bool ffoundError = false;

            Language_Type = Language_Type.ToLower();

            try
            {
                ffoundError = false;
                string jsonFile = File.ReadAllText(Application.StartupPath + "\\novus_error.json");

                //cerco se l'errore e' presente nel file..
                JObject rss = JObject.Parse(jsonFile);
                var obj = (JObject)rss;
                var userObj = JObject.Parse(jsonFile);
                foreach (var property in obj.Properties())
                {
                    if (property.Name == ErrorCode)
                    {
                        ffoundError = true;
                        break;
                    }
                }

                if (ffoundError == true)
                {
                    var jsonDes = JsonConvert.DeserializeObject<dynamic>(jsonFile);

                    ErrorCodeDescription = jsonDes[ErrorCode]["title"][Language_Type];
                }
                else
                    //ErrorCodeDescription = ErrorCode;
                    ErrorCodeDescription = "";



            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
                // FileNotFoundExceptions are handled here.
                MessageBox.Show("File novus_error.json not found");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


        }


        //funzioni che parserizzano l'output della DLL..
        public static void JsonStatus(string outData, ref string[] Module, ref string[] Status, ref string[] Description, ref int index, ref int indexslot, ref string[] Cassettecode)
        {
           
            try
            {
                if (outData.Length != 0)
                {
                    JObject rss = JObject.Parse(outData.ToString());
                    var obj = (JObject)rss;
                    var userObj = JObject.Parse(outData);
                    foreach (var property in obj.Properties())
                    {
                        if (property.Name != REPLY_CONTENT)
                        {
                            Module[index] = UNIT_STATUS_RTC;
                            Status[index] = Convert.ToString(userObj[property.Name][UNIT_STATUS_RTC]["statusCode"]);
                            Description[index++] = Convert.ToString(userObj[property.Name][UNIT_STATUS_RTC]["description"]);

                            Module[index] = UNIT_STATUS_FEEDER;
                            Status[index] = Convert.ToString(userObj[property.Name][UNIT_STATUS_FEEDER]["statusCode"]);
                            Description[index++] = Convert.ToString(userObj[property.Name][UNIT_STATUS_FEEDER]["description"]);

                            Module[index] = UNIT_STATUS_NOTE_READER;
                            Status[index] = Convert.ToString(userObj[property.Name][UNIT_STATUS_NOTE_READER]["statusCode"]);
                            Description[index++] = Convert.ToString(userObj[property.Name][UNIT_STATUS_NOTE_READER]["description"]);

                            Module[index] = UNIT_STATUS_SAFE_CONTROLLER;
                            Status[index] = Convert.ToString(userObj[property.Name][UNIT_STATUS_SAFE_CONTROLLER]["statusCode"]);
                            Description[index++] = Convert.ToString(userObj[property.Name][UNIT_STATUS_SAFE_CONTROLLER]["description"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_A;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_A]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_A]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_A]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_B;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_B]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_B]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_B]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_C;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_C]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_C]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_C]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_D;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_D]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_D]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_D]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_E;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_E]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_E]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_E]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_F;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_F]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_F]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_F]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_G;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_G]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_G]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_G]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_H;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_H]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_H]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_H]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_I;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_I]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_I]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_I]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_J;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_J]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_J]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_J]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_K;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_K]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_K]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_K]["cassetteCode"]);

                            Module[index] = DEVICE_VERSION_TYPE_CASSETTE_L;
                            Status[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_L]["statusCode"]);
                            Description[index] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_L]["description"]);
                            Cassettecode[index++] = Convert.ToString(userObj[property.Name][DEVICE_VERSION_TYPE_CASSETTE_L]["cassetteCode"]);

                            ////Module[index] = property.Name;
                            ////Status[index] = Convert.ToString(userObj[property.Name]["RTC"]["statusCode"]);
                            ////Description[index] = Convert.ToString(userObj[property.Name]["RTC"]["description"]);
                            //if (Module[index].Substring(0, 3) == "cas")
                            //    {
                            //        Cassettecode[index] = Convert.ToString(userObj[property.Name]["cassetteCode"]);

                            //    }
                            //    index++;
                        }

                    }
                }
                else
                    index = 0;
            }
            catch(Exception ex )
            {
                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.ToString());
            } 


        }

        public static void JsonCashData(string outData, ref string[] drawerID, ref string[] noteID, ref string[] numberNote, ref string[] capacity, ref string[] s_switch, ref int index, ref string totalNotes )
        {

            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);

            totalNotes = (string)rss[HEADER_CONTENT]["totalNotes"];
            string  tot_elemet  = (string)rss[HEADER_CONTENT]["noteReportElements"];  


           // var roles = rss.Value<JArray>("noteReport");
           //if (roles != null )
            {
                //int count = roles.Count;
                foreach (var property in obj.Properties())
                {
                    if (property.Name == HEADER_CONTENT)
                    {
                        for (index = 0; index < Convert.ToInt32(tot_elemet); index++)
                        {
                            try
                            {
                                capacity[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["capacity"]);
                                drawerID[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["cassetteID"]);
                                noteID[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["noteID"]);
                                s_switch[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["switch"]);
                                numberNote[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["numberNote"]);
                            }
                            catch(Exception ex)
                            {
                                //MessageBox.Show("JsonCashData   " + "\n" + ex.ToString());
                                Console.WriteLine(ex.ToString());                          }
                           
                            //if (noteID[index] == Function_Gen.MULTI_NATION_DENOM_SPECIFIC )
                            //{
                            //    Console.WriteLine("aaa");
                            //}
                        }

                    }

                }
            }
        }

        public static void JsonDetailCassetteCashData(string outData, string cassetteID, ref string[] detailDenonimation, ref string[] detailValue, ref int index_detail)
        {

            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);
            string temp;
            int index = 0;

            //var roles = rss.Value<JArray>("noteReport");
            //int count = roles.Count;

            string tot_elemet = (string)rss[HEADER_CONTENT]["noteReportElements"];
            

            foreach (var property in obj.Properties())
            {
                if (property.Name == HEADER_CONTENT)
                {
                    for (index = 0; index < Convert.ToInt32(tot_elemet); index++)
                    {
                        temp = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["cassetteID"]);
                        if (temp == cassetteID)
                        {
                            //leggo i dettagli ...
                            temp = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["detailsCassette"]);
                            if (temp != "")
                            {
                                var objtemp = JObject.Parse(temp);
                                index_detail = 0;
                                foreach (var o in objtemp)
                                {
                                    detailDenonimation[index_detail] = (o.Value.ToString());
                                    detailValue[index_detail] = o.Key.ToString();
                                    index_detail++;

                                }
                            }

                        }
                    }
                }
            }
        }

        public static void JsonDispenseReport(Boolean fbyTarget ,  string outData, ref string[] denominationID, ref string[] numberNote, ref int index, ref string totalElements)
        {
            if (outData != "" )
            {
                JObject rss = JObject.Parse(outData.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outData);

                //totalNotes = (string)rss["totalNotesDispensed"];
                totalElements = (string)rss[HEADER_CONTENT]["noteReportElements"];


                if (totalElements != "0")
                {

                    //var roles = rss.Value<JArray>(NOTE_REPORT);
                    // int count = roles.Count;

                    foreach (var property in obj.Properties())
                    {

                        //if (property.Name == "noteReport")
                        if (property.Name == HEADER_CONTENT)
                        {
                            for (index = 0; index < Convert.ToInt32(totalElements); index++)
                            {
                                if (fbyTarget == true)
                                    denominationID[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["cassetteID"]);
                                else
                                    denominationID[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["denominationID"]);

                                numberNote[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["numberNote"]);
                            }
                        }


                    }
                }
            }
            
        }

        public static void JsonEmptyDrawerReport(string outData, ref string operationCode, ref string startingNotes, ref string outputNotes)
        {

            JObject rss = JObject.Parse(outData.ToString());
            //var obj = (JObject)rss;
            //var userObj = JObject.Parse(outData);

            operationCode = (string)rss[HEADER_CONTENT]["operationCode"];
            startingNotes = (string)rss[HEADER_CONTENT]["startingNotes"];
            outputNotes = (string)rss[HEADER_CONTENT]["outputNotes"];

        }

        public static void JsonVersion(string outData, ref string[] Module, ref string[] Ver, ref string[] Rel, ref int indexslot)
        {
            try
            {
                JObject rss = JObject.Parse(outData.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outData);
                foreach (var property in obj.Properties())
                {
                    Module[indexslot] = property.Name;
                    Ver[indexslot] = Convert.ToString(userObj[property.Name]["Ver"]);
                    Rel[indexslot] = Convert.ToString(userObj[property.Name]["Rel"]);
                    indexslot++;
                }
     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        public static void JsonGetReaderConfiguration(string outData, ref string[] BankNameActive,  ref int indexslot , ref string[] BankPositon)
        {
            string Enabled = "";
            string strNameBank = ""; 
            try
            {
                JObject rss = JObject.Parse(outData.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outData);
                foreach (var property in obj.Properties())
                {
                   if (property.Name == HEADER_CONTENT )
                    {
                        for (int xx = 0; xx < 99; xx ++)
                        {
                            strNameBank = "Bank" + xx.ToString();
                            Enabled = Convert.ToString(userObj[property.Name][strNameBank]["enabled"]);
                            if (Enabled == "Y") //cerco solo i banchi effettivamente abilitati ...
                            {
                                BankNameActive[indexslot] = Convert.ToString(userObj[property.Name][strNameBank]["bankname"]);
                                BankPositon[indexslot] = Convert.ToString(userObj[property.Name][strNameBank]["numbank"]);
                                
                            }
                            indexslot++;
                        }
                       
                    }
                  
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        public static void JsonECB(string outData, string outDataDevice ,ref string Device, ref string Validator , ref string Software )
        {
            string rel_tmp = ""; 
            try
            {
                JObject rss = JObject.Parse(outData.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outData);
                foreach (var property in obj.Properties())
                {
                    if (property.Name == HEADER_CONTENT )
                    {
                        Validator = Convert.ToString(userObj[property.Name]["ECB_header"]["name"]);
                        rel_tmp = Convert.ToString(userObj[property.Name]["ECB_header"]["rel"]);
                        rel_tmp = rel_tmp.Substring(0, 4);   //tolgo gli ultimi due caratteri lasciati per compatibilita'
                        Software = rel_tmp + "." + Convert.ToString(userObj[property.Name]["ECB_header"]["ref_name"]) + Convert.ToString(userObj[property.Name]["ECB_header"]["ref_version"]) + "." + Convert.ToString(userObj[property.Name]["ECB_header"]["FsID"]);
                    }
                  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                JObject rss = JObject.Parse(outDataDevice.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outDataDevice);
                foreach (var property in obj.Properties())
                {
                    if (property.Name == HEADER_CONTENT )
                        Device = Convert.ToString(userObj[property.Name][OPERATION_TYPE_MACHINE]);
                    if (Device == "CM18TNS")
                        Device = "CM18NS"; //29/11/2021 richiesta a.la rosa


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        public static void JsonDenominationGetconfig(string outData, ref string[] Denonimation_Available, ref int index)
        {
            string Enable = "";
            string str_denom = ""; 
            try
            {
                JObject rss = JObject.Parse(outData.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outData);
                foreach (var property in obj.Properties())
                {
                    if (property.Name == HEADER_CONTENT ) //"ReaderConfig")
                    {
                        for (int xx = 0; xx < 99; xx ++ )
                        {
                            str_denom = "Denom_" + xx.ToString();
                            Enable = Convert.ToString(userObj[property.Name][str_denom]["Denom_Enable"]);
                            if (Enable == "Y")
                            {
                                Denonimation_Available[index] = Convert.ToString(userObj[property.Name][str_denom]["Denom_Nid"]);
                                index++;
                            }
                        }
                        
                       
                    }
                    
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }//End JsonDenominationGetconfig

        public static void JsonDenominationGetReaderconfig(string outData, ref string[] Denonimation_Available, ref string[] Version , ref string[] Height , ref string[] status ,ref int index)
        {
            string str_denom = ""; 
            try
            {
                JObject rss = JObject.Parse(outData.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outData);
                foreach (var property in obj.Properties())
                {
                    if (property.Name == HEADER_CONTENT)
                    {
                        for (int xx = 0; xx < 99; xx ++)
                        {
                            str_denom = "Denom_" + xx.ToString();

                            Denonimation_Available[index] = Convert.ToString(userObj[property.Name][str_denom]["Denom_Nid"]);
                            Version[index] = Convert.ToString(userObj[property.Name][str_denom]["Denom_Version"]);
                            Height[index] = Convert.ToString(userObj[property.Name][str_denom]["Denom_Height"]);
                            status[index] = Convert.ToString(userObj[property.Name][str_denom]["Denom_Enable"]);
                            if (status[index] == "Y")
                                status[index] = CONFIG_ENABLED;
                            else
                                status[index] = CONFIG_DISABLED;

                            index++;
                        }
                       
                    }

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }//End JsonDenominationGetReaderconfig

        public static void JsongetOutputDetail(string outData, ref string[] denominationID , ref string[] numberNote, ref int index)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);

            string totalNotes; 

            foreach (var property in obj.Properties())
            {
                if (property.Name == HEADER_CONTENT)
                {
                    totalNotes = (string)rss[HEADER_CONTENT][OUTPUT_NOTE_REPORT_ELEMENTS];

                    for (int xx =  0;  xx < Convert.ToInt32(totalNotes); xx ++ )
                    {
                        denominationID[index] = Convert.ToString(userObj[property.Name][OUTPUT_NOTE_REPORT][index][OUTPUT_NOTE_TYPE]);
                        numberNote[index] = Convert.ToString(userObj[property.Name][OUTPUT_NOTE_REPORT][index][OUTPUT_NOTE_NR_NOTES]);
                        index++;
                    }
                  
                }
            }
        }//JsongetOutputDetail

        public static void JsongetClassificationDetail(string outData, ref string[] denominationID, ref string[] numberNote, ref int index)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);

            string reason;

            foreach (var property in obj.Properties())
            {
                //if (property.Name == OUTPUT_NOTE_REPORT)
                if (property.Name == HEADER_CONTENT)
                {
                    string CASH_DATA_SOILING_TEST = "soilingTest";
                    string CASH_DATA_GRID_CHECK = "gridCheck";
                    string CASH_DATA_TAPE_CHECK = "tapeCheck";
                    string CASH_DATA_UV_DE_INKED_CHECK = "UVDe-InkedCheck";
                    string CASH_DATA_DOG_AERS_CORNER_CHECK = "dogEarsCornerCheck";
                    string CASH_DATA_FORMAT_CHECK = "formatCheck";
                    string CASH_DATA_CONTRAST_CHECK = "contrastCheck";
                    string CASH_DATA_CLOSED_TEARS_CHECK = "closedTearsCheck";
                    string CASH_DATA_GRAFFITTI_CHECK = "graffittiCheck";
                    string CASH_DATA_STAIN_CHECK = "stainCheck";
                    string CASH_DATA_DYE_CHECK = "dyeCheck";
                    string CASH_DATA_NOTE_ID= "noteID";

                    
                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_NOTE_ID];
                    denominationID[index] = Convert.ToString(CASH_DATA_NOTE_ID);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_SOILING_TEST];
                    denominationID[index] = Convert.ToString(CASH_DATA_SOILING_TEST);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_GRID_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_GRID_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_TAPE_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_TAPE_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_UV_DE_INKED_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_UV_DE_INKED_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_DOG_AERS_CORNER_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_DOG_AERS_CORNER_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_FORMAT_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_FORMAT_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_CONTRAST_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_CONTRAST_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_CLOSED_TEARS_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_CLOSED_TEARS_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_GRAFFITTI_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_GRAFFITTI_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_STAIN_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_STAIN_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;

                    reason = (string)rss[property.Name][OUTPUT_NOTE_REPORT][CASH_DATA_DYE_CHECK];
                    denominationID[index] = Convert.ToString(CASH_DATA_DYE_CHECK);
                    numberNote[index] = Convert.ToString(reason);
                    index++;
                }
            }
        }//JsongetOutputDetail

        public static void JsongetStatisticReader(string outData, ref string[] param1, ref string[] param2, ref string[] param3 , ref int index)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);
            //string temp;

            foreach (var property in obj.Properties())
            {

                if (property.Name == HEADER_CONTENT )
                {
                    param1[index] = SLOT_STATISTIC_NPROC;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NPROC][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NPROC][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NACCEPT;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NACCEPT][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NACCEPT][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NREFUSED;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NREFUSED][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NREFUSED][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NSUSP;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NSUSP][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NSUSP][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NOT_AUTH;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOT_AUTH][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOT_AUTH][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NCLSF;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NCLSF][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NCLSF][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NRID;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NRID][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NRID][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NMAG;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NMAG][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NMAG][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NFORMAT;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NFORMAT][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NFORMAT][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NUNFIT;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NUNFIT][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NUNFIT][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NOVERRUN;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOVERRUN][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOVERRUN][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NDISABLECHAIN;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NDISABLECHAIN][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NDISABLECHAIN][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NUV;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NUV][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NUV][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NEXTREMEFIT;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NEXTREMEFIT][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NEXTREMEFIT][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NGENERROR;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NGENERROR][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NGENERROR][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NOCRCHECK;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOCRCHECK][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOCRCHECK][SLOT_STATISTIC_DESCRIPTION]);
                }
               

            }

    }//JsongetStatisticReader

        public static void JsonUndoReport(string outData, ref string[] noteType, ref string[] numberNote, ref int index)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);
            string totalNotes; 

            totalNotes = (string)rss[DEPOSIT_NOTE_REPORT_ELEMENTS];

            if (totalNotes != "0")
            {
                //var roles = rss.Value<JArray>();
                // int count = roles.Count;
                int count = Convert.ToInt32(totalNotes);

                foreach (var property in obj.Properties())
                {
                    if (property.Name == DEPOSIT_NOTE_REPORT)
                    {
                        for (index = 0; index < count; index++)
                        {
                           noteType[index] = Convert.ToString(userObj[property.Name][index][DEPOSIT_NOTE_TYPE]);
                           numberNote[index] = Convert.ToString(userObj[property.Name][index][DEPOSIT_NUMBER_NOTE]);
                        }
                    }
                }
            }

        }//JsonUndoReport

        public static void JsonModuleVersion(string outData, string ModuleName ,ref string[] Module, ref string[] Ver, ref string[] Rel, ref string[] SN , ref int indexslot)
        {
            try
            {
                JObject rss = JObject.Parse(outData.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outData);
                foreach (var property in obj.Properties())
                {
                    if (property.Name == HEADER_CONTENT)
                    {
                        Module[indexslot] = Convert.ToString(userObj[property.Name][ModuleName]["Name"]); //property.Name;
                        Ver[indexslot] = Convert.ToString(userObj[property.Name][ModuleName]["Ver"]);
                        Rel[indexslot] = Convert.ToString(userObj[property.Name][ModuleName]["Rel"]);
                        SN[indexslot] = Convert.ToString(userObj[property.Name][ModuleName]["serial_num"]);

                        if (Module[indexslot] != "" )
                            indexslot++;
                    }
                  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        public static void JsonAdvancedCashData(string outData, ref string[] cassette_status, ref string[] cassette_side_enabled, ref string[] cassette_switch, ref string[] noteID, ref string[] capacity, ref string[] numberNote , ref int index, ref string totalNotes)
        {

            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);

            //totalNotes = (string)rss["totalNotes"];
            string tot_elemet = (string)rss[HEADER_CONTENT]["noteReportElements"];

            //var roles = rss.Value<JArray>("noteReport");
            //if (roles != null)
            {
                //int count = roles.Count;
                foreach (var property in obj.Properties())
                {
                    if (property.Name == HEADER_CONTENT)
                    {
                        for (index = 0; index < Convert.ToInt32(tot_elemet); index++)
                        {
                            try
                            {
                                cassette_status[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["cassetteID"]) + "  " + Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["cassetteIDstatus"]); 
                                cassette_side_enabled[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["side_enabled"]);
                                if (cassette_side_enabled[index] == "Y")
                                    cassette_side_enabled[index] = "YES";
                                else
                                    cassette_side_enabled[index] = "NO";

                                cassette_switch[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["switch"]);
                                if (cassette_switch[index] == "1")
                                    cassette_switch[index] = "ON";
                                else
                                    cassette_switch[index] = "OFF";
                                noteID[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["noteID"]);
                                capacity[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["capacity"]);
                                numberNote[index] = Convert.ToString(userObj[property.Name][NOTE_REPORT][index]["numberNote"]);
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show("JsonCashData   " + "\n" + ex.ToString());
                                Console.WriteLine(ex.ToString());
                            }

                        }

                    }

                }
            }
        }//JsonAdvancedCashData

        public static void JsonUpperLock(string outData, ref string[] Module, ref string[] Status, ref int indexslot)
        {
            try
            {
                 indexslot = 0 ; 
                JObject rss = JObject.Parse(outData.ToString());
                var obj = (JObject)rss;
                var userObj = JObject.Parse(outData);
                foreach (var property in obj.Properties())
                {
                        Module[indexslot] = SLOT_STATUS_SAFE_DOOR ;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_SAFE_DOOR]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_UPPER_COVER;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_UPPER_COVER]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_FEEDER;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_FEEDER]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_INPUT_SLOT;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_INPUT_SLOT]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_REJECT_SLOT;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_REJECT_SLOT]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_LEFT_SLOT;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_LEFT_SLOT]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_RIGHT_SLOT;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_RIGHT_SLOT]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_LEFT_LIGHT;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_LEFT_LIGHT]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_RIGHT_LIGHT;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_RIGHT_LIGHT]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_FKP_POCKET;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_FKP_POCKET]);
                        indexslot++;
                        Module[indexslot] = SLOT_STATUS_FKS_POCKET;
                        Status[indexslot] = Convert.ToString(userObj[property.Name][SLOT_STATUS_FKS_POCKET]);
                        indexslot++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        public static void JsonUnitConfig(string outData, ref string[] UnitConfig, ref string[] UnitOptionConfig , ref string[] UnitOptionOneConfig , ref string[] UnitOptionTwoConfig , ref int index, ref int indexOptionConfig, ref int indexOneConfig, ref int indexTwoConfig)
        {
            try
            {
                if (outData.Length != 0)
                {
                    JObject rss = JObject.Parse(outData.ToString());
                    var obj = (JObject)rss;
                    var userObj = JObject.Parse(outData);
                    foreach (var property in obj.Properties())
                    {
                        if (property.Name == HEADER_CONTENT)
                        {
                            UnitConfig[index++] = Convert.ToString(userObj[property.Name][HEADER_CONFIG][OPTION_DATE_DISPLAY]);
                            UnitConfig[index++] = Convert.ToString(userObj[property.Name][HEADER_CONFIG][OPTION_BALANCED_CASSETTE]);
                            UnitConfig[index++] = Convert.ToString(userObj[property.Name][HEADER_CONFIG][OPTION_ALARM_1]);
                            UnitConfig[index++] = Convert.ToString(userObj[property.Name][HEADER_CONFIG][OPTION_DELAY]);
                            UnitConfig[index++] = Convert.ToString(userObj[property.Name][HEADER_CONFIG][OPTION_DATE_DISPLAY_AM_PM]);
                            UnitConfig[index++] = Convert.ToString(userObj[property.Name][HEADER_CONFIG][OPTION_UNFIT_SAFE]);
                            UnitConfig[index++] = Convert.ToString(userObj[property.Name][HEADER_CONFIG][OPTION_JOURNAL_LOG]);

                            UnitOptionConfig[indexOptionConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTIONCONFIG][OPTION_SPECIAL_CLEAN]);
                            UnitOptionConfig[indexOptionConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTIONCONFIG][OPTION_SPECIAL_ALARM]);
                            UnitOptionConfig[indexOptionConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTIONCONFIG][OPTION_SPECIAL_PROTOCOL]);
                            UnitOptionConfig[indexOptionConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTIONCONFIG][OPTION_SPECIAL_CLOSE]);

                            UnitOptionOneConfig[indexOneConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_ONE_CONFIG][OPTION_FORCE_RECOVERY]);
                            UnitOptionOneConfig[indexOneConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_ONE_CONFIG][OPTION_JAM_SPECIAL_CLEAN]);
                            UnitOptionOneConfig[indexOneConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_ONE_CONFIG][OPTION_LAN_TIME_OUT]);
                            UnitOptionOneConfig[indexOneConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_ONE_CONFIG][OPTION_TCDA]);
                            UnitOptionOneConfig[indexOneConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_ONE_CONFIG][OPTION_BOOKING_EXTERNAL]);

                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_SAFE_40MM]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_BVU_HANDLING]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_FACE_ORIENTATION]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_SPECIAL_CLEAN_DISPLAY]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_CAT2BOX_HANDLING]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_EVOLED_HANDLING]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_SEPARATE]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_PUSH_BN]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_DIFFERENT_CENTER]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_DISABLE_MESSAGE]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_ALARM2]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_REMAP_JAM]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_VIRTUAL_D_ENV]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_NEW_LED_SIDE]);
                            UnitOptionTwoConfig[indexTwoConfig++] = Convert.ToString(userObj[property.Name][HEADER_OPTION_TWO_CONFIG][OPTION_DISABLE_LED]);

                        }
                    }
                }
                else
                {
                    index = 0;
                    indexOptionConfig = 0;
                    indexOneConfig = 0;
                    indexTwoConfig = 0; 
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.ToString());
            }
        }

        public static void JsonGetSlotReader(string outData, ref string[] BankNameActive,  ref string[] BankPositon, ref string[] BankVersion ,ref int indexslot)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);
            int xx = 0;
        
            foreach (var property in obj.Properties())
            {
                if (property.Name == HEADER_CONTENT)
                {
                    try
                    {
                        for ( xx = 0; xx < 99; xx++)
                        {
                            BankNameActive[indexslot] = Convert.ToString(userObj[property.Name]["References"][xx.ToString()]["ref_name"]);
                            BankPositon[indexslot] = Convert.ToString(userObj[property.Name]["References"][xx.ToString()]["bankId"]);
                            BankVersion[indexslot] = Convert.ToString(userObj[property.Name]["References"][xx.ToString()]["ver"]);
                            indexslot++;
                        }

                    }
                    catch(Exception ex)
                    {
                        xx = 99;
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
           
        }//JsonGetSlotReader


        public static void Jsongetidentifierbankconfigurations(string outData, ref string[] BankNameinUse, ref string[] BankNamestatusEnabled, ref int indexslot)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);
            int xx = 0;

            foreach (var property in obj.Properties())
            {
                if (property.Name == HEADER_CONTENT)
                {
                    try
                    {
                        for (xx = 0; xx < 99; xx++)
                        {
                            if ( Convert.ToString(userObj[property.Name][xx.ToString()]["enabled"] ) == "Y" )
                            {
                                BankNamestatusEnabled[indexslot] = Convert.ToString(userObj[property.Name][xx.ToString()]["enabled"]);
                                BankNameinUse[indexslot] = Convert.ToString(userObj[property.Name][xx.ToString()]["bankname"]);
                                indexslot++;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        xx = 99;
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

        }//Jsongetidentifierbankconfigurations

        public static void JsonPeriodicOperatigHours(string outData, int day ,ref string[] SwitchOn, ref string[] SwitchOff, ref string[] LunchBreakOn , ref string[] LunchBreakOff)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);

            foreach (var property in obj.Properties())
            {
                if (property.Name == HEADER_CONTENT)
                {
                    try
                    {
                        SwitchOn[day] = Convert.ToString(userObj[property.Name][OPERATING_HOURS_SWTICH_ON]);
                        SwitchOff[day] = Convert.ToString(userObj[property.Name][OPERATING_HOURS_SWTICH_OFF]);
                        LunchBreakOn[day] = Convert.ToString(userObj[property.Name][OPERATING_HOURS_LUNCH_BREAK_ON]);
                        LunchBreakOff[day] = Convert.ToString(userObj[property.Name][OPERATING_HOURS_LUNCH_BREAK_OFF]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

        }//JsonPeriodicOperatigHours


        public static void JsonAPeriodicOperatigHours(string outData, int index , ref string[]  dayofWeek, ref string[] SwitchOn, ref string[] SwitchOff, ref string[] LunchBreakOn, ref string[] LunchBreakOff)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);

            foreach (var property in obj.Properties())
            {
                if (property.Name == HEADER_CONTENT)
                {
                    try
                    {

                        dayofWeek[index] = Convert.ToString(userObj[property.Name][APERIODC_DAY]) + "-" +  (Convert.ToString(userObj[property.Name][APERIODC_MONTH])  +"-" + Convert.ToString(userObj[property.Name][APERIODC_YEAR])); 
                        SwitchOn[index] = Convert.ToString(userObj[property.Name][OPERATING_HOURS_SWTICH_ON]);
                        SwitchOff[index] = Convert.ToString(userObj[property.Name][OPERATING_HOURS_SWTICH_OFF]);
                        LunchBreakOn[index] = Convert.ToString(userObj[property.Name][OPERATING_HOURS_LUNCH_BREAK_ON]);
                        LunchBreakOff[index] = Convert.ToString(userObj[property.Name][OPERATING_HOURS_LUNCH_BREAK_OFF]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

        }//JsonPeriodicOperatigHours


        public static void JsonPhotoSensorNovus(string outData, ref int index, ref string[] moduleId , ref string[] ph_id , ref string[] result, ref string[] threshold, ref string[] agc, ref string[] current)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);

            foreach (var property in obj.Properties())
            {
                if (property.Name == HEADER_CONTENT)
                {
                    try
                    {
                        for (int xx = 0; xx < FormNovus.MAX_PHOTO_ELEMENT; xx ++ )
                        {
                            moduleId[index] = Convert.ToString(userObj[property.Name][index.ToString()][PHOTOS_ID]);
                            ph_id[index] = Convert.ToString(userObj[property.Name][index.ToString()][PHOTOS_ID]);
                            result[index] = Convert.ToString(userObj[property.Name][index.ToString()][PHOTO_RESULT])  + "-" + Convert.ToString(userObj[property.Name][index.ToString()][PHOTO_RESULT_DESCRIPTION]);
                            threshold[index] = Convert.ToString(userObj[property.Name][index.ToString()][PHOTO_THRESHOLD]);
                            agc[index] = Convert.ToString(userObj[property.Name][index.ToString()][PHOTO_AGC]);
                            current[index] = Convert.ToString(userObj[property.Name][index.ToString()][PHOTO_CURRENT]);

                            index++;
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

        }//JsonPhotoSensorNovus

        public static void JsongetStatisticReaderEx(string outData, ref string[] param1, ref string[] param2, ref string[] param3, ref int index)
        {
            JObject rss = JObject.Parse(outData.ToString());
            var obj = (JObject)rss;
            var userObj = JObject.Parse(outData);

            foreach (var property in obj.Properties())
            {

                if (property.Name == HEADER_CONTENT)
                {
                    param1[index] = SLOT_STATISTIC_NPROC;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NPROC][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NPROC][SLOT_STATISTIC_DESCRIPTION]);

                    //*******************************************************************
                    param1[index] = "";
                    param2[index] = "";
                    param3[index++] = "";

                    param1[index] = "";
                    param2[index] = "";
                    param3[index++] = "";

                    param1[index] = SLOT_STATISTIC_NACCFIT;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NACCFIT][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NACCFIT][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NACCUFIT;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NACCUFIT][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NACCUFIT][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NOT_AUTH;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOT_AUTH][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOT_AUTH][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NSUSP;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NSUSP][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NSUSP][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NREFUSED;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NREFUSED][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NREFUSED][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = "";
                    param2[index] = "";
                    param3[index++] = "";

                    param1[index] = SLOT_STATISTIC_NCLSF;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NCLSF][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NCLSF][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NRID;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NRID][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NRID][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NMAG;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NMAG][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NMAG][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NOVERRUN;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOVERRUN][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOVERRUN][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NDISABLECHAIN;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NDISABLECHAIN][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NDISABLECHAIN][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NUV;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NUV][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NUV][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NEXTREMEFIT;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NEXTREMEFIT][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NEXTREMEFIT][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NGENERROR;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NGENERROR][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NGENERROR][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NSKEW;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NSKEW][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NSKEW][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NDOUBLE;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NDOUBLE][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NDOUBLE][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NDIMENSION;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NDIMENSION][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NDIMENSION][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NABORT;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NABORT][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NABORT][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NLOCALIZZ;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NLOCALIZZ][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NLOCALIZZ][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = SLOT_STATISTIC_NOTHER;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOTHER][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOTHER][SLOT_STATISTIC_DESCRIPTION]);

                    param1[index] = "";
                    param2[index] = "";
                    param3[index++] = "";

                    //*******************************************************************
                    param1[index] = SLOT_STATISTIC_NRECJBYRTC;
                    param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NRECJBYRTC][SLOT_STATISTIC_COUNT]);
                    param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NRECJBYRTC][SLOT_STATISTIC_DESCRIPTION]);

                    //param1[index] = "";
                    //param2[index] = "";
                    //param3[index++] = "";

                    //*******************************************************************


                   
                    //param1[index] = SLOT_STATISTIC_NACCEPT;
                    //param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NACCEPT][SLOT_STATISTIC_COUNT]);
                    //param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NACCEPT][SLOT_STATISTIC_DESCRIPTION]);


                   
                    //param1[index] = SLOT_STATISTIC_NFORMAT;
                    //param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NFORMAT][SLOT_STATISTIC_COUNT]);
                    //param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NFORMAT][SLOT_STATISTIC_DESCRIPTION]);

                    //param1[index] = SLOT_STATISTIC_NUNFIT;
                    //param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NUNFIT][SLOT_STATISTIC_COUNT]);
                    //param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NUNFIT][SLOT_STATISTIC_DESCRIPTION]);

                 
                    //param1[index] = SLOT_STATISTIC_NOCRCHECK;
                    //param2[index] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOCRCHECK][SLOT_STATISTIC_COUNT]);
                    //param3[index++] = Convert.ToString(userObj[property.Name][SLOT_STATISTIC_NOCRCHECK][SLOT_STATISTIC_DESCRIPTION]);


                  }


            }

        }//JsongetStatisticReaderEx

    }//class JsonFunctioncs
}
    
