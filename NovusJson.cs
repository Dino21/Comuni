using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Novus
{
    

    public class Novus_Json
    {

        #region InDataJsonStructures
        /// <summary>
        /// ARCAInitialize inData
        /// </summary>
        public class CinDataInitialize
        {
            public string pathtrace { get; set; }
        }
        public CinDataInitialize InDataInitialize = new CinDataInitialize();

        /// <summary>
        /// ARCAConnect inData
        /// </summary>
        public class CinDataConnect
        {
            public string cmdMode { get; set; }
            public string connectionType { get; set; }
            public string encryption { get; set; }
            public string protocolSet { get; set; }
            public CConnectTCPIPset TCPIPset { get; set; } = new CConnectTCPIPset();
            public string USBSerialNumber { get; set; }
            public CConnectCOMset COMset { get; set; } = new CConnectCOMset();
        }
        public CinDataConnect InDataConnect;

        /// <summary>
        /// CinDataConnect
        /// </summary>
        public class CConnectTCPIPset
        {
            public string address { get; set; }
            public string port { get; set; }
        }

        /// <summary>
        /// CinDataConnect
        /// </summary>
        public class CConnectCOMset
        {
            public string COMname { get; set; }
            public string baudRate { get; set; }
            public string parity { get; set; }
            public string dataBits { get; set; }
            public string stopBits { get; set; }
        }
        /// <summary>
        /// ARCA Close inData
        /// </summary>
        public class CinDataClose
        {
            public string opSide { get; set; }
        }
        public CinDataClose InDataClose;


        public class CinDataExeDeviceReset
        {
            public string mode;
        }
        public CinDataExeDeviceReset InDataExeDeviceReset;

        /// <summary>
        /// ARCADeviceStatus inData
        /// </summary>
        public class CinDataStatus
        {
            public string statusType { get; set; }
        }
        public CinDataStatus InDataStatus;

        public class CinDataeExeTraceTransfer
        {
            public string transferType;
            public string transferMode;
            public string transferFile;
            public string ZipfileName;
        }
        public CinDataeExeTraceTransfer inDataTraceTransfer;

        /// <summary>
        /// ARCAOpen - inData
        /// </summary>
        public class CinDataOpen
        {
            public string opSide { get; set; }
            public string password { get; set; }
        }
        public CinDataOpen InDataOpen;

        /// <summary>
        /// ARCAgetCashData - inData
        /// </summary>
        public class CinDataCashData
        {
            public string noteType { get; set; }
            public string opSide { get; set; }
            public string password { get; set; }
        }
        public CinDataCashData InDataCashData;
        public class CinDataDeposit
        {
            public string cmdMode { get; set; }
            public string cassetteDetails { get; set; }
            //public string mode { get; set; }
        }
        public CinDataDeposit InDataDeposit;

        public class CinDataUpdateVersion
        {
            public string cmdMode;
            public string updateMode;
            public string updateType;
            public string FileName;
            public string FileSize;
            public string FilePath;
        }
        public CinDataUpdateVersion InDataUpdateVersion;

        public class CinDisconnect
        {
        }
        public CinDisconnect inDataDisconnect;

        #endregion

        #region OutDataJsonStructures
        /// <summary>
        /// ARCAConnect outData
        /// </summary>
        public class CoutReplyCode
        {
            public CReplyCodeDetails reply { get; set; }
        }
        public CoutReplyCode OutDataConnect;
        public CoutReplyCode OutDataDisconnect;
        public CoutReplyCode OutDataOpen;
        public CoutReplyCode OutDataClose;
        public CoutReplyCode OutDataUpdateVersion;
        public CoutReplyCode OutDataExeDeviceReset;


        /// <summary>
        /// CoutDataConnect - CoutDataStatus - ...
        /// </summary>
        public class CReplyCodeDetails
        {
            public string replyCode { get; set; }
            public string replyCodeDescription { get; set; }
        }

        
        /// <summary>
        /// ARCADeviceStatus outData
        /// </summary>
        public class CoutDataStatus
        {
            public CReplyCodeDetails reply { get; set; } = new CReplyCodeDetails();
            public CStatusContent content { get; set; } = new CStatusContent();
        }
        public CoutDataStatus OutDataStatus;

        /// <summary>
        /// CoutDeviceStatus
        /// </summary>
        public class CStatusContent
        {
            public CModuleStatus feeder { get; set; } = new CModuleStatus();
            public CModuleStatus RTC { get; set; } = new CModuleStatus();
            public CModuleStatus noteReader { get; set; } = new CModuleStatus();
            public CModuleStatus safeController { get; set; } = new CModuleStatus();
            public CCassetteStatus cassetteA { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteB { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteC { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteD { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteE { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteF { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteG { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteH { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteI { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteJ { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteK { get; set; } = new CCassetteStatus();
            public CCassetteStatus cassetteL { get; set; } = new CCassetteStatus();
        }

        /// <summary>
        /// CStatusContent

        /// </summary>
        public class CModuleStatus
        {
            public string statusCode { get; set; }
            public string description { get; set; }
        }

        /// <summary>
        /// CStatusContent
        /// </summary>
        public class CCassetteStatus
        {
            public string cassetteCode { get; set; }
            public string statusCode { get; set; }
            public string description { get; set; }
        }

        

        public class CoutDataCashDataStd
        {
            public CoutReplyCode reply { get; set; } = new CoutReplyCode();
            public CCashDataContentStd content { get; set; } = new CCashDataContentStd();
        }
        public CoutDataCashDataStd OutDataCashDataStd;

        public class CCashDataContentStd
        {
            public string totalNotes { get; set; }
            public string noteReportElements { get; set; }
            public CnoteReport[] noteReport { get; set; }
        }

        public class CnoteReport
        {
            public string cassetteID { get; set; }
            public string noteID { get; set; }
            public string numberNote { get; set; }
            public string capacity { get; set; }
            public string Switch { get; set; }
        
        }
        
        public class CgetDeviceDateTime
        {
            public CoutReplyCode reply { get; set; } = new CoutReplyCode();
            public CDateTimeContent content { get; set; } = new CDateTimeContent();
        }
        public CgetDeviceDateTime outDeviceDateTime;
        
        public class CDateTimeContent
        {
            public string seconds { get; set; }
            public string minutes { get; set; }
            public string hours { get; set; }
            public string weekDay { get; set; }
            public string day { get; set; }
            public string month { get; set; }
            public string year { get; set; }
        }

        

        public class CoutDataDeposit
        {
            public CoutReplyCode reply { get; set; } = new CoutReplyCode();
            public CDepositContent content { get; set; } = new CDepositContent();
        }
        public CoutDataDeposit OutDataDeposit;
        public class CDepositContent
        {
            public string noteReportElements { get; set; }
            public string safeNumberOfCassettes { get; set; }
            public CSafeReport safeReport { get; set; } = new CSafeReport();
            public CDepositReport depositReport { get; set; } = new CDepositReport();
            public CNoteReport[] noteReport { get; set; } 
        }

        public class CDepositReport
        {
            public string refused { get; set; }
            public string totaleNoteDep { get; set; }
            public string unrecognized { get; set; }
        }
        public class CNoteReport
        {
            public string noteType { get; set; }
            public string numberNote { get; set; }
        }

        public class CSafeReport
        {
            public CCassetteReport id0 { get; set; }
            public CCassetteReport id1 { get; set; }
            public CCassetteReport id2 { get; set; }
            public CCassetteReport id3 { get; set; }
            public CCassetteReport id4 { get; set; }
            public CCassetteReport id5 { get; set; }
            public CCassetteReport id6 { get; set; }
            public CCassetteReport id7 { get; set; }
            public CCassetteReport id8 { get; set; }
            public CCassetteReport id9 { get; set; }
            public CCassetteReport id10 { get; set; }
            public CCassetteReport id11 { get; set; }
            public CCassetteReport id12 { get; set; }
        }

        public class CCassetteReport
        {
            public string noteQty { get; set; }
            public string storageConf { get; set; }
            public string storageID { get; set; }
        }

    }

    #endregion
}
