using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CM50Class
{
    public class Reply
    {
        public const string REPLY_BOOT_MODE = "BOOT MODE";
        public const string REPLY_OK = "OK";
        public const string REPLY_BUSY = "BUSY";
        public const string REPLY_COVER_OPEN = "COVER OPEN";
        public const string REPLY_SAFE_OPEN = "SAFE OPEN";
        public const string REPLY_FEEDER_OPEN = "FEEDER OPEN";
        public const string REPLY_OUT_OF_SERVICE = "OUT OF SERVICE";
        public const string REPLY_DEPOSIT_OUT_OF_SERVICE = "DEPOSIT OUT OF  SERVICE";
        public const string REPLY_SAFE_OUT_OF_SERVICE = "SAFE OUT OF SERVICE";
        public const string REPLY_CM_LOCKED = "CM LOCKED";
        public const string REPLY_CAGE_OPEN = "CAGE OPEN";
        public const string REPLY_OSC_SHUTDOWN = "OSC SHUTDOWN";
        public const string REPLY_SYNTAX_ERROR = "SYNTAX ERROR";
        public const string REPLY_SOFTWARE_ERROR = "SOFTWARE ERROR";
        public const string REPLY_WRONG_FIRMWARE = "WRONG FIRMWARE";
        public const string REPLY_NOTES_IN_OUTPUT_BIN_RIGHT = "NOTES IN OUTPUT BIN RIGHT";
        public const string REPLY_NOTES_IN_OUTPUT_BIN_LEFT = "NOTES IN OUTPUT BIN LEFT";
        public const string REPLY_NOTES_IN_REJECT_BIN = "NOTES IN REJECT BIN";
        public const string REPLY_NO_NOTES_IN_INPUT_BIN = "NO NOTES IN INPUT BIN";
        public const string REPLY_NOTE_IN_INPUT_BIN = "NOTE IN INPUT BIN";
        public const string REPLY_DENOMINATION_FULL = "DENOMINATION FULL";
        public const string REPLY_DENOMINATION_EMPTY = "DENOMINATION EMPTY";
        public const string REPLY_WRONG_KEY = "WRONG KEY";
        public const string REPLY_NO_CASSETTE_ENABLE = "NO CASSETTE ENABLE";
        public const string REPLY_WRONG_SIDE = "WRONG SIDE";
        public const string REPLY_REQUEST_OVER_LIMIT = "REQUEST OVER LIMIT";
        public const string REPLY_CASSETTE_NOT_PRESENT = "CASSETTE NOT PRESENT";
        public const string REPLY_CASSETTE_DENOMINATION_NOT_EMPTY = "CASSETTE DENOMINATION NOT EMPTY";
        public const string REPLY_DENOMINATION_NOT_PRESENT = "DENOMINATION NOT PRESENT";
        public const string REPLY_RECORD_NOT_PRESENT = "RECORD NOT PRESENT";
        public const string REPLY_UNEXPECTED_CMD = "UNEXPECTED CMD";
        public const string REPLY_BOOKING_NOT_ACTIVE = "BOOKING NOT ACTIVE";
        public const string REPLY_COIN_ERROR = "COIN ERROR";
        public const string REPLY_PHOTO_SENSOR_FAULT = "PHOTO SENSOR FAULT";
        public const string REPLY_FEEDER_JAM = "FEEDER JAM";
        public const string REPLY_VALIDATOR_JAM = "VALIDATOR JAM";
        public const string REPLY_UPPER_TRACK_JAM = "UPPER TRACK JAM";
        public const string REPLY_SAFE_INPUT_JAM = "SAFE INPUT JAM";
        public const string REPLY_LOWER_TRACK_JAM = "LOWER TRACK JAM";
        public const string REPLY_OUTPUT_TRACK_JAM = "OUTPUT TRACK JAM";
        public const string REPLY_CASSETTE_A_JAM = "CASSETTE A JAM";
        public const string REPLY_CASSETTE_B_JAM = "CASSETTE B JAM";
        public const string REPLY_CASSETTE_C_JAM = "CASSETTE C JAM";
        public const string REPLY_CASSETTE_D_JAM = "CASSETTE D JAM";
        public const string REPLY_CASSETTE_E_JAM = "CASSETTE E JAM";
        public const string REPLY_CASSETTE_F_JAM = "CASSETTE F JAM";
        public const string REPLY_CASSETTE_G_JAM = "CASSETTE G JAM";
        public const string REPLY_CASSETTE_H_JAM = "CASSETTE H JAM";
        public const string REPLY_CASSETTE_I_JAM = "CASSETTE I JAM";
        public const string REPLY_CASSETTE_J_JAM = "CASSETTE J JAM";
        public const string REPLY_RESERVED = "RESERVED";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_A = "MISSING NOTE ON CASSETTE A";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_B = "MISSING NOTE ON CASSETTE B";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_C = "MISSING NOTE ON CASSETTE C";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_D = "MISSING NOTE ON CASSETTE D";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_E = "MISSING NOTE ON CASSETTE E";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_F = "MISSING NOTE ON CASSETTE F";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_G = "MISSING NOTE ON CASSETTE G";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_H = "MISSING NOTE ON CASSETTE H";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_I = "MISSING NOTE ON CASSETTE I";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_J = "MISSING NOTE ON CASSETTE J";
        public const string REPLY_CASSETTE_K_JAM = "CASSETTE K JAM";
        public const string REPLY_CASSETTE_L_JAM = "CASSETTE L JAM";
        public const string REPLY_CASSETTE_M_JAM = "CASSETTE M JAM";
        public const string REPLY_CASSETTE_N_JAM = "CASSETTE N JAM";
        public const string REPLY_CASSETTE_O_JAM = "CASSETTE O JAM";
        public const string REPLY_CASSETTE_P_JAM = "CASSETTE P JAM";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_K = "MISSING NOTE ON CASSETTE K";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_L = "MISSING NOTE ON CASSETTE L";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_M = "MISSING NOTE ON CASSETTE M";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_N = "MISSING NOTE ON CASSETTE N";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_O = "MISSING NOTE ON CASSETTE O";
        public const string REPLY_MISSING_NOTE_ON_CASSETTE_P = "MISSING NOTE ON CASSETTE P";
        public const string REPLY_MISFEEDING = "MISFEEDING";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_A = "CASSETTE OUT OF SERVICE A";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_B = "CASSETTE OUT OF SERVICE B";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_C = "CASSETTE OUT OF SERVICE C";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_D = "CASSETTE OUT OF SERVICE D";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_E = "CASSETTE OUT OF SERVICE E";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_F = "CASSETTE OUT OF SERVICE F";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_G = "CASSETTE OUT OF SERVICE G";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_H = "CASSETTE OUT OF SERVICE H";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_I = "CASSETTE OUT OF SERVICE I";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_J = "CASSETTE OUT OF SERVICE J";
        public const string REPLY_INTERNALCOMM_ERROR = "INTERNALCOMM. ERROR";
        public const string REPLY_NOTE_OVERFLOW = "NOTE OVERFLOW";
        public const string REPLY_COUNT_ERROR = "COUNT ERROR";
        public const string REPLY_READER_COM_ERROR = "READER COM ERROR";
        public const string REPLY_SAFE_FAILURE = "SAFE FAILURE";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_K = "CASSETTE OUT OF SERVICE K";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_L = "CASSETTE OUT OF SERVICE L";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_M = "CASSETTE OUT OF SERVICE M";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_N = "CASSETTE OUT OF SERVICE N";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_O = "CASSETTE OUT OF SERVICE O";
        public const string REPLY_CASSETTE_OUT_OF_SERVICE_P = "CASSETTE OUT OF SERVICE P";
        public const string REPLY_SHUTTER_FAILURE = "SHUTTER FAILURE";
        public const string REPLY_UPPER_LOCK_FAILURE = "UPPER LOCK FAILURE";
        public const string REPLY_COM_OPEN_ERROR = "COM OPEN ERROR";
        public const string REPLY_COM_COMMUNICATION_GENERIC_ERROR = "COM COMMUNICATION GENERIC ERROR";
        public const string REPLY_COM_READ_COMMUNICATION_ERROR = "COM READ COMMUNICATION ERROR";
        public const string REPLY_COM_TIMEOUT_COMMUNICATION_ERROR = "COM TIMEOUT COMMUNICATION ERROR";
        public const string REPLY_LAN_READ_ERROR = "LAN READ ERROR";
        public const string REPLY_CRC_ERROR_ON_FILE_TRANSFER = "CRC ERROR ON FILE TRANSFER";
        public const string REPLY_PROTOCOL_SEQUENCE_ERROR_DURING_FILE_TRANSFER = "PROTOCOL SEQUENCE ERROR DURING FILE TRANSFER";
        public const string REPLY_NAK_ERROR_ON_USB_BETWEEN_OSC_AND_RTC = "NAK ERROR ON USB BETWEEN OSC AND RTC";
        public const string REPLY_TEST_FAILURE = "TEST FAILURE";
        public const string REPLY_PERIPHERAL_NOT_FOUND = "PERIPHERAL NOT FOUND";
        public const string REPLY_GENERIC_ERROR_IN_COMMUNICATION_BETWEEN_OSC_AND_RTC = "GENERIC ERROR IN COMMUNICATION BETWEEN OSC AND RTC";
        public const string REPLY_FILE_CREATION_ERROR = "FILE CREATION ERROR";
        public const string REPLY_FILE_WRITE_ERROR = "FILE WRITE ERROR";
        public const string REPLY_FILE_READ_ERROR = "FILE READ ERROR";
        public const string REPLY_FILE_OPEN_ERROR = "FILE OPEN ERROR";
        public const string REPLY_FILE_NOT_FOUND = "FILE NOT FOUND";
        public const string REPLY_USER_FILE_WRITE_ERROR = "USER FILE WRITE ERROR";
        public const string REPLY_USER_FILE_READ_ERROR = "USER FILE READ ERROR";
        public const string REPLY_SYSTEM_ERROR = "SYSTEM ERROR";

    }

    #region CLASSI UTILIZZATE PER LE INFORMAZIONI RICAVATE DAL DATABASE
    public class client
    {
        public int idClient { get; set; } // ID of the client
        public string clientName { get; set; } // Name of the client
    }

    public class cm50ddcode
    {
        public int idDD { get; set; } // ID of the DD code
        public string code { get; set; } // DD code
        public string internalName { get; set; } // Internal name of the DD
        public string aSISversion { get; set; } // Version of the aSIS
        public string transitionDDCode { get; set; } // Code for the transition DD
        public string transitionFileName { get; set; } // File name for the transition DD
    }
    public class cm50unitconfiguration
    {
        public int idUnitConfiguration { get; set; } // ID of the unit configuration
        public string cfg { get; set; } // Configuration
        public string optCfg { get; set; } // Optional configuration
        public string optOneCfg { get; set; } // Optional configuration one
        public string optTwoCfg { get; set; } // Optional configuration two
        public string optThreeCfg { get; set; } // Optional configuration three
    }

    public class cm50cdfcode
    {
        public int idCdf { get; set; } // ID of the CDF code
        public string code { get; set; } // CDF code
        public string internalName { get; set; } // Internal name of the CDF
        public string[] currCode { get; set; } // Array of currency codes
    }

    public class cm50assetcode
    {
        public int idAsset { get; set; } // ID of the asset code
        public string code { get; set; } // Asset code
        public string shortFileName { get; set; } // Short file name for the asset
        public string tallFileName { get; set; } // Tall file name for the asset
    }

    public class cm50prodcode
    {
        public string code { get; set; } // Product code
        public string description { get; set; } // Description of the product
        public int cassetteNumber { get; set; } // Number of cassettes in the product
        public string machineType { get; set; } // Type of machine
        public string alarmType { get; set; } // Type of alarm
        public string cassetteName { get; set; } // Name of the cassette
        public string upperLockPresent { get; set; } // Indicates if the upper lock is present
        public string lowerLockPresent { get; set; } // Indicates if the lower lock is present
        public string baracodeReaderPresent { get; set; } // Indicates if the barcode reader is present
        public string readingStationPresent { get; set; } // Indicates if a reading station is present

    }

    public class cm50ReaderCfg
    {
        public int idReaderCfg { get; set; } // ID of the reader configuration
        public int activeBank { get; set; } // Active bank
        public int slot0 { get; set; } // Slot 0
        public int slot1 { get; set; } // Slot 1
        public int slot2 { get; set; } // Slot 2
        public int slot3 { get; set; } // Slot 3
        public cm50SingleReaderBank[] banks { get; set; } // Array of banks for the reader configuration
    }
    public class cm50SingleReaderBank
    {
        public string name { get; set; } // Name of the bank
        public string ver { get; set; } // Version of the bank
    }

    public class cm50CassetteConfiguration
    {
        public int idCassetteCfg { get; set; } // ID of the cassette configuration
        public string[] cassDenom { get; set; } // Array of cassette codes
    }

    public class cm50db
    {
        public cm50prodcode product { get; set; } // Product code
        public client client { get; set; } // Client information
        public string ddTransitionCode { get; set; } // Code for the DD transition
        public cm50ddcode dd { get; set; } // DD code
        public cm50assetcode asset { get; set; } // Asset code
        public cm50cdfcode cdf { get; set; } // CDF code
        public cm50unitconfiguration unitConfiguration { get; set; } // Unit configuration
        public cm50ReaderCfg readerCfg { get; set; } // Reader configuration
        public cm50CassetteConfiguration cassetteCfg { get; set; } // Cassette configuration

    }
    #endregion

    #region CLASSI PER LE INFORMAZIONI RICEVUTE DALLA MACCHINA
    public class Ccm50
    {
        public CModuleStatus[] moduleStatus { get; set; } // Status of the module
        public cm50unitconfiguration unitConfiguration { get; set; } // Unit configuration
        public cm50ddcode dd { get; set; } // DD code
        public cm50cdfcode cdf { get; set; } // CDF code
        public cm50assetcode asset { get; set; } // Asset code
        public CDepositDetail lastDeposit { get; set; } // Information of last deposit
        public CCashDataDetails cashData { get; set; } // Array of cashdata information
        public CLanConfiguration lanCfg { get; set; } // IP address of the machine
        public CSerials serials { get; set; } // Serial numbers of the machine
        public CTrace traces { get; set; } // Trace information
    }

    public class CTrace
    {
        public string firstFilename { get; set; } // Name of the first trace file
        public string finalFilename { get; set; } // Name of the final trace file
    }
    public class CSerials
    {
        public string product { get; set; } // Serial number of the machine
        public string rtc { get; set; } // Serial number of the RTC
        public string reader { get; set; } // Serial number of the reader
        public string feeder { get; set; } // Serial number of the feeder
        public string safe { get; set; } // Serial number of the safe
        public string[] cassette { get; set; } // Array of cassette serial numbers
    }

    public class CLanConfiguration
    {
        public string ipAddress { get; set; } // IP address of the machine
        public string subnetMask { get; set; } // Subnet mask of the machine
        public string deviceName { get; set; } // Device name of the machine
        public string dhcpEnabled { get; set; } // Indicates if DHCP is enabled (1=enabled, 0=disabled)
        public string gateway { get; set; } // Gateway of the machine
        public string dns { get; set; } // DNS of the machine
        public string macAddress { get; set; } // MAC address of the machine
    }
    public class CCashDataDetails
    {
        public int total { get; set; } // total notes in safe
        public CCashDataSingleNote[] cassette { get; set; } // array of denominations deposited in safe
    }

    public class CCashDataSingleNote
    {
        public string denomination { get; set; } // Note denomination
        public int qty { get; set; } // Note quantity
        public int freeCap { get; set; } // Cassette free capacity
        public string cassette { get; set; } // Cassette used
        public string enableSwitch { get; set; } // Y=enabled, N=disabled, O=SwitchedOff, X=CassetteUnloked
        public int value { get; set; } // note value
    }
    public class CDepositDetail
    {
        public int totalDeposit { get; set; } // Total of notes deposit in safe
        public int totalOutput { get; set; } // Total of notes on output slot
        public int totalReject { get; set; } // Total of notes on reject slot
        public CDepositedNote[] notes { get; set; } // Array of deposited notes
    }
    public class CDepositedNote
    {
        public string denomination { get; set; } // Note denomination
        public int qty { get; set; } // Note quantity
    }
    public class CModuleStatus
    {
        public string moduleName { get; set; } // Module name
        public string moduleId { get; set; } // ID of the module
        public string statusHex { get; set; } // Status in hexadecimal format
        public string statusDescription { get; set; } // Description of the status
        public string moduleInfo { get; set; } // Information about the module

        public CModuleStatus()
        {

        }
    }


    public class CGeneralExtendedStatus
    {
        public string reply { get; set; } // Reply from the machine
        public CModuleStatus[] moduleStaus { get; set; } // Array of module statuses
    }
    #endregion


}
