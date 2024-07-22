using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using INI;
using System.Xml;
//using Dino;
using CM18tool;
using Newtonsoft.Json;
using System.Timers;
//using System.Collections;
//using System.Reflection;
//using System.Diagnostics;
//using System.Windows.Forms;


namespace myArcaNameSpace
{

    public class Module
    {
        public struct SNovus
        {
            public string xmlFileName;
            public string startDate;
            public string startTime;
            public string productCode;
            public string clientName;
            public int productID;
            public int clientID;
            public int cassetteNumber;
            public string cassetteName;
            public string cdfInternalName;
            public string cdfCode;
            public int idReaderCfg;
            public int idCassetteCfg;
            public int idUnitCfg;
            public string alarmType;
            public string assetShortFilename;
            public string assetTallFilename;
            public string assetCode;
            public string ddInternalName;
            public string ddCode;
            public string ddTransitionCode;
            public string ddTransitionFilename;
            public string machineType;
            public string serialNumber;
            public string cmName;
            public string compSW;
            public string compHw;
            public SFirmwares fw;
            public SLanConfig lanConfig;
            public SLanConfig testingLanConfig;
            public SSigleCassette[] drum;
            public SUnitConfiguration dbUnitConfiguration;
            public SUnitConfiguration unitConfiguration;
            public SReader reader;
            public SCassetteConfiguration[] cassetteCfg;
            public string tl;
            public ushort status;
            public SElectronicBoard controller;
            public SElectronicBoard pho1;
            public SElectronicBoard pho2;
            public SElectronicBoard safe;
            public SPhotoList photo;
            public string MACaddress;
            public int allBnDeposited;
            public int singleTestBnDep;
            public int singleTestBnPrel;
            public string upperLock;
            public string lowerLock;
            public string readingStation;
            public string barcodeReader;
        }
        
        public struct SR5000Feeder
        {
            public string code;
            public string tl;
            public string xmlFileName;
            public string startDate;
            public string startTime;
            public Module.SFw ph02;
            public string serialNumber;
            public string labelSerialNumber;
            public ushort status;
            public Module.SPhoto Feedn;
            public int totalBnProcessed;
            public int totalDoubleBnReader;
            public int totalDobuleBnFeeder;
        }

        public struct SCassetteConfiguration
        {
            public string cassName;
            public string idNote;
            public int num;
            public string enabled;
        }

        public struct SReaderConfiguration
        {
            public int id;
            public int activeBank;
            public int[] slotid;
            public SReaderBank[] bank;

        }

        public struct SReaderBank
        {
            public string name;
            public string ver;
        }

        public struct SUnitConfiguration
        {
            public int id;
            public string config;
            public string optCfg;
            public string optOneCfg;
            public string optTwoCfg;
            public string optThreeCfg;
        }
        public struct SLanConfig
        {
            public string dhcpEnabled;
            public string IpAddress;
            public string subnetMask;
            public string gateway;
            public string deviceName;
            public string deviceDescription;
            public string primaryDns;
            public string secondaryDns;
            public string useNetBios;
            public string ipv6Enabled;
        }

        public struct SFirmwares
        {
            public string ddFilename;
            public SFw ui;
            public SFw display;
            public SFw controller;
            public SFw safe;
            public SFw reader;
            public SFw[] cassette;
        }
        public struct SLTM
        {
            public string code;
            public string tl;
            public string xmlFileName;
            public string startDate;
            public string startTime;
            public Module.SFw safeFw;
            public Module.SFw alarmFw;
            public string serialNumber;
            public string labelSerialNumber;
            public ushort status;
            public short cassetteNumber;
            public Module.SPhoto cashL;
            public Module.SPhoto cashR;
        }
        public struct SUTM
        {
            public SReader reader;
            public string type;
            public string code;
            public string tl;
            public string xmlFileName;
            public string startDate;
            public string startTime;
            public string serialNumber;
            public string labelSerialNumber;
            public ushort status;
            public SElectronicBoard controller;
            public SElectronicBoard pho1;
            public SElectronicBoard pho2;
            public SELM elmSorterTrvHigh;
            public SELM elmSorterTrvLow;
            public SELM elmSorterDout;
            public SELM carrello;
            public SMotor motFeeder;
            public SMotor motHorTransp;
            public SMotor motInput;
            public SMotor motVerTransp;
            public SUpperModuleConfiguration configuration;
            public SUim uim;
            public SPhotoList photo;
            public SBin bin;
            public SModuleInfo[] infoModules;
            public SReaderInfo infoReader;
            public SCassetteInfo[] infoCassette;
        }

        public struct SReaderInfo
        {
            public string name;
            public string commander;
            public string validator;
            public string nios;
            public string tape;
            public string magnetico;
            public string suite;
            public string boardSerial;
        }
        public struct SCassetteInfo
        {
            public string name;
            public string version;
            public string release;
            public string boardSerial;
            public string moduleSerial;
            public string concName;
            public string concVer;
            public string concRel;
            public string bootVer;
            public string bootRel;
        }
        public struct SModuleInfo
        {
            public string name;
            public string version;
            public string release;
            public string moduleSerial;
            public string boardSerial;
            public string bootVer;
            public string bootRel;

        }
        public struct SBin
        {
            public short number;
            public SPhotoBoard Output;
            public SPhotoBoard Reject;
        }

        public struct SPhotoBoard
        {
            public byte type;
        }

        public struct SUpperModuleConfiguration
        {
            public byte standalone; // 0x00
            public byte Aux; // 0x01
            public byte Crm; // 0x02
            public byte Srn; // 0x03
            public byte Elm; // 0x04
            public byte UimArca; // 0x05
            public byte dataLogger; // 0x06
            public byte dataLoggerMaster; // 0x07
            public byte smartJam; // 0x08
            public byte bnSerial; // 0x09
            public byte cat2cat3; // 0x0A
            public byte testState; // 0x0B
            public byte testResult; // 0x0C
            public byte highSafe; // 0x0D
            public byte Shutter; // 0x0E
            public byte disableReaderError; // 0x0F
            public byte disableBnSfrangiata; // 0x10
            public byte checkSysIntegrity; // 0x11
        }

        public struct SReaderModuleConfiguration
        {
            public byte Srn; // 0x00
            public byte bnSerial; // 0x01
        }

        public struct SSafeModuleConfiguration
        {
            public byte highSafe; // 0x00
            public byte phisycInterlock; // 0x01
            public byte simulatedInterlock; // 0x02
            public byte miv2A; // 0x03
            public byte lock1; // 0x04
            public byte lock2; // 0x05
            public byte doorUnlock; // 0x06
            public byte realTimeLog; // 0x07
            public byte cageSwitch; // 0x08
        }
        public struct SCassetteModuleConfiguration
        {
            public byte saveBnSerial; // 0x00
            public byte realTimeLog; // 0x01
            public byte sorterFeedback; // 0x02
        }
        public struct SUim
        {
            public string type;
            public string release;
        }
        public struct SPhotoList
        {
            public SOpb binIn;
            public SPhoto strobeFeed;
            public SOpb feed1;
            public SOpb feed2;
            public SOpb feed3;
            public SOpb feed4;
            public SOpb stackOutR1;
            public SOpb stackOutR2;
            public SOpb stackOutL1;
            public SOpb stackOutL2;
            public SPhoto feedOpen;
            public SOpb binOutL;
            public SOpb binOutC;
            public SOpb binOutR;
            public SOpb syncTrvR1;
            public SOpb syncTrvR2;
            public SOpb syncTrvL1;
            public SOpb syncTrvL2;
            public SPhoto trvH;
            public SPhoto trvL;
            public SOpb rejL;
            public SOpb rejC;
            public SOpb rejR;
            public SOpb binRejL;
            public SOpb binRejC;
            public SOpb binRejR;
            public SOpb stackRejR1;
            public SOpb stackRejR2;
            public SOpb stackRejL1;
            public SOpb stackRejL2;
            public SPhoto sorterOut;
            public SOpb syncOutL;
            public SOpb syncOutC;
            public SOpb syncOutR;
            public SPhoto trackOpen;
        }
        public struct SMotor
        {
            public byte id;
            public string name;
            public short corrMaxAvv;
            public short corrPreEcc;
            public short maintCurr;
            public short corrPostEcc;
            public short nominalSpeed;
            public short initSpeed;
            public short finalSpeed;
            public short acceleration;
            public short deceleration;
            public short durataCicloVelCost;
            public short durataTotale;
            public short jitterNeg;
            public short jitterPos;
            public short timing;
            public short meanSpeed;
            public short stallCurrMaint;
            public short stallCurrMaxAvv;
            public short stallCurrPostEcc;
            public short stallCurrPreEcc;
        }

        public struct STransferBn
        {
            public STransferData[] bn;
            public float meanSkew;
            public short meanDelta1;
            public short meanDelta2;
            public short meanDelta3;
        }
        public struct STransferData
        {
            public byte skewFeed;
            public byte skewReader;
            public byte skewTrv;
            public short deltaTrv;
            public byte skewSyncOut;
            public short deltaSyncOut;
            public byte skewStackOut;
            public short deltaStackOut;
            public byte skewReject;
            public short deltaReject;
            public byte skewStackReject;
            public short deltaStackReject;
        }

        public struct SReader
        {
            public string serialNumber;
            public string comPort;
            public int activeBank;
            public int[] slotBankId;
            public SReaderBank[] bank;
        }

        public struct SELM
        {
            public string name;
            public int activationTime;
            public int deactivationTime;
            public short currMax;
            public short speedUpCurr;
            public short speedUpTime;
            public short maintCurr;
            public short maintTime;
            public short failCurrent;
            public byte id;
            public byte photoID;
        }
        public struct SDDM
        {
            public string code;
            public string tl;
            public string xmlFileName;
            public string startDate;
            public string startTime;
            public SSigleCassette[] id;
        }
        public struct SSigleCassette
        {
            public string name;
            public string id;
            public SFw fw;
            public string serialNumber;
            public SBasc basc;
            public ushort status;
            public SPhoto PhOutL;
            public SPhoto PhOutR;
            public SPhoto PhTrack;
            public SPhoto PhEnd;
            //public int misin;
            //public int tapeLenght;
            //public int blackTapeLenght;
        }

        public struct SOpb
        {
            public byte id;
            public string name;
            public byte presence;
            public byte voidThreshold;
            public byte voidAgc;
            public short voidCurrent;
            public byte docThreshold;
            public byte docAgc;
            public short docCurrent;
            public byte wpThreshold;
            public byte wpAgc;
            public short wpCurrent;
            public byte polarity;
            public byte driverType;
            public int lvlVoid;
            public int lvlDoc;
            public int lvlWp;
            public int delta;
            public string result;
        }
        public struct SPhoto
        {
            public byte presence;
            public byte sogliaDoc;
            public byte agcDoc;
            public short currentDoc;
            public byte sogliaNoDoc;
            public byte agcNoDoc;
            public short currentNoDoc;
            public byte sogliaWork;
            public byte agcWork;
            public short currentWork;
            public byte id;
            public byte outputPolarity;
            public byte drainSelect;
            public double lvlVoid;
            public double lvlDoc;
            public string name;
            public string result;
        }
        public struct SBasc
        {
            public string name;
            public SFw fw;
        }

        public struct SFw
        {
            public string name;
            public string release;
            public string version;
            public string moduleSN;
            public string boardSN;
        }
        public struct SElectronicBoard
        {
            public string name;
            public string moduleSN;
            public string boardSN;
            public SFw fw;
        }
    }

    public class UtmProtocol
    {
        #region REPLY CODE
        const byte REPLY_OK                 = 0x0000; // Successo, comando eseguito
        const byte REPLY_PROC_NOT_ALLOWED   = 0x0001; // Procedura non consentita
        const byte REPLY_MSGID_KO           = 0x0002; // Identificativo messaggio (MsgId) non valido
        const byte REPLY_MSGOPCODE_KO       = 0x0003; // Codice operativo (MsgOpCode) non valido
        const byte REPLY_NOBN_INPUT_BIN     = 0x0004; // Banconote non presenti nella bocchetta di input
        const byte REPLY_BN_INOUTPUT_BIN    = 0x0005; // Banconote presenti nella bocchetta di output
        const byte REPLY_BN_INREJECT_BIN    = 0x0006; // Banconote presenti nella bocchetta di reject
        const byte REPLY_INVALID_PAR        = 0x0007; // Parametro del comando non valido
        const byte REPLY_BUSY               = 0x0008; // Procedura già in corso
        const byte REPLY_CMD_NOT_ALLOWED    = 0x0009; // Comando non consentito
        const byte REPLY_SESSION_CLOSED     = 0x000A; // Sessione di lavoro chiusa
        const byte REPLY_WRONG_LEN_PAR      = 0x000B; // Lunghezza parametri sbagliata
        const byte REPLY_CMD_NOT_EXECUTED   = 0x000C; // Comando non eseguito correttamente
        #endregion

        #region COMANDI
        public struct SUICommand
        {
            public byte opCode;
            public short timeOut;
            public byte bytesAnswer;
        }

        public struct SCommand
        {
            public byte msgId;
            public byte[] msgOpCode;
            public int timeOut;
            public byte bytesAnswer;
        }

        enum PresetArea
        {
            AllSystem=0x00,
            UpperArea = 0x01
        }

        public string dataSent;
        public string dataReceived;

        public SCommand Preset = new SCommand();
        public SCommand Deposit = new SCommand();
        public SCommand Dispense = new SCommand();
        public SCommand Counting = new SCommand();
        public SCommand Recovery = new SCommand();
        public SCommand GetReport = new SCommand();
        public SCommand GetLastDepositInfo = new SCommand();
        public SCommand Open = new SCommand();
        public SCommand Close = new SCommand();
        public SCommand SetCurrencies = new SCommand();
        public SCommand GetCurrencies = new SCommand();
        public SCommand EnableCurrencies = new SCommand();
        public SCommand EnableDenom = new SCommand();
        public SCommand SetDrumDenom = new SCommand();
        public SCommand GetDrumDenom = new SCommand();
        public SCommand SetConfiguration = new SCommand();
        public SCommand GetConfiguration = new SCommand();
        public SCommand UpdateFirmware = new SCommand();
        public SCommand InitializeDrum = new SCommand();
        public SCommand AssignDrumName = new SCommand();
        public SCommand SetModuleInfo = new SCommand();
        public SCommand GetModuleInfo = new SCommand();
        public SCommand TroyHorse = new SCommand();
        public SCommand LockUnlockUpperPart = new SCommand();
        public SCommand ImportLog = new SCommand();
        public SCommand GetUpperBlockState = new SCommand();
        public SCommand GetDrumsPhysicalPos = new SCommand();
        public SCommand Reassign = new SCommand();
        public SCommand GetAllarmBoardType = new SCommand();
        public SCommand GetSystemLife = new SCommand();
        public SCommand GetActiveCurrencies = new SCommand();
        public SCommand SetNumActiveCurr = new SCommand();
        public SCommand GetNumActiveCurr = new SCommand();
        public SCommand SetActiveCurr = new SCommand();
        public SCommand SetSystemLife = new SCommand();
        public SCommand SetDrumName = new SCommand();
        public SCommand GetNoteCounters = new SCommand();
        public SCommand CheckCashDataError = new SCommand();
        public SCommand ResetCashDataError = new SCommand();
        public SCommand ReaderCalibration = new SCommand();
        public SCommand SystemReset = new SCommand();
        public SCommand GetSystemConfiguration = new SCommand();
        public SCommand GetStatus = new SCommand();
        public SCommand GetCashData = new SCommand();
        public SCommand GetCashCount = new SCommand();
        public SCommand Hello = new SCommand();
        public SCommand CheckStressTest = new SCommand();
        public SCommand StopCashOperation = new SCommand();
        public SCommand EndProcedure = new SCommand();
        public SCommand MechanicalPartOpenClose = new SCommand();
        public SCommand NotePresenceInBin = new SCommand();
        public SCommand AlarmWarning = new SCommand();
        public SCommand Tamper = new SCommand();
        public SCommand TroyHorseReply = new SCommand();
        public SCommand StressTest = new SCommand();
        public SCommand StartupCompleted = new SCommand();
        public SCommand ExportSystemTraceData = new SCommand();
        public SCommand GetPhotoStatus = new SCommand();
        public SCommand PhotoAdjust = new SCommand();
        public SCommand GetPhotoParameters = new SCommand();
        public SCommand SetPhotoParameters = new SCommand();
        public SCommand GetCassetteFunctionParameters = new SCommand();
        public SCommand GetPhotoCalibrationValues = new SCommand();
        public SCommand AdjustBin = new SCommand();
        public SCommand PhotoAudit = new SCommand();
        public SCommand FactoryAdjust = new SCommand();
        public SCommand PhotoAdjustFactoryMode = new SCommand();
        public SCommand GetSwitchStatus = new SCommand();
        public SCommand GetDiverterParameters = new SCommand();
        public SCommand SetDiverterParameters = new SCommand();
        public SCommand OperateDiverter = new SCommand();
        public SCommand TestDiverter = new SCommand();
        public SCommand GetMotorParameters = new SCommand();
        public SCommand SetMotorParameters = new SCommand();
        public SCommand OperateMotor = new SCommand();
        public SCommand StopMotor = new SCommand();
        public SCommand TestMotor = new SCommand();
        public SCommand GetFeedStrobeRevolutionNumber = new SCommand();
        public SCommand GetPowerParameters = new SCommand();
        public SCommand SetPowerParameters = new SCommand();
        public SCommand OperatePower = new SCommand();
        public SCommand SetReferenceValue = new SCommand();
        public SCommand GetReferenceValue = new SCommand();
        public SCommand TestModeEnableDisable = new SCommand();
        public SCommand TransferBanknotes = new SCommand();
        public SCommand CheckHorTranspSpeed = new SCommand();
        public SCommand TestCassetteModule = new SCommand();
        public SCommand OperateShutter = new SCommand();
        public SCommand GetShutterPosition = new SCommand();
        public SCommand SetTestState = new SCommand();
        public SCommand GetTestState = new SCommand();
        public SCommand ResetE2P = new SCommand();
        public SCommand SetModuleConfiguration = new SCommand();
        public SCommand GetModuleConfiguration = new SCommand();
        public SCommand TestReaderMagSensor = new SCommand();
        public SCommand GetOpbBoardBinType = new SCommand();
        public SCommand GetFeederReport = new SCommand();
        public SCommand GetPhBinCalibration = new SCommand();
        public SCommand GetPowerState = new SCommand();
        public SCommand CheckMotorMoving = new SCommand();
        public SCommand CheckLtmCableConnection = new SCommand();

        public SUICommand UiSetSolidLight = new SUICommand(); 
        public SUICommand UiGetStatus = new SUICommand();
        public SUICommand UiGetFwRelease = new SUICommand();
        public SUICommand UiStartLightEffect = new SUICommand();
        public SUICommand UiStopLightEffect = new SUICommand();
        public SUICommand UiWriteLightEffect = new SUICommand();
        public SUICommand UiReadLightEffect = new SUICommand();


        //public SCommand  = new SCommand();
        #endregion

        public UtmProtocol(ref LibUsbDotNet.UsbDevice myUsbDevice)
        {
            InitializeCommands();
        }
        public UtmProtocol()
        {
            InitializeCommands();
        }

        void InitializeCommands()
        {
            Preset.msgId = 0x01;
            Preset.msgOpCode = new byte[] { 0x00, 0x01 };
            Preset.bytesAnswer = 0x02;
            Deposit.msgId = 0x01;
            Deposit.msgOpCode = new byte[] { 0x00, 0x02 };
            Deposit.bytesAnswer = 0x02;
            Dispense.msgId = 0x01;
            Dispense.msgOpCode = new byte[] { 0x00, 0x03 };
            Dispense.bytesAnswer = 0x02;
            Counting.msgId = 0x01;
            Counting.msgOpCode = new byte[] { 0x00, 0x04 };
            Counting.bytesAnswer = 0x02;
            Counting.timeOut = 3000;
            Recovery.msgId = 0x01;
            Recovery.msgOpCode = new byte[] { 0x00, 0x05 };
            Recovery.bytesAnswer = 0x02;
            GetReport.msgId = 0x01;
            GetReport.msgOpCode = new byte[] { 0x00, 0x06 };
            GetReport.bytesAnswer = 0x80;// deposit=128(0x80) / dispence=38(0x26) / counting=63(0x3F)
            GetLastDepositInfo.msgId = 0x01;
            GetLastDepositInfo.msgOpCode = new byte[] { 0x00, 0x07 };
            GetLastDepositInfo.bytesAnswer = 0x14;
            Open.msgId = 0x01;
            Open.msgOpCode = new byte[] { 0x00, 0x08 };
            Open.bytesAnswer = 0x02;
            Close.msgId = 0x01;
            Close.msgOpCode = new byte[] { 0x00, 0x09 };
            Close.bytesAnswer = 0x02;
            SetCurrencies.msgId = 0x01;
            SetCurrencies.msgOpCode = new byte[] { 0x00, 0x0A };
            SetCurrencies.bytesAnswer = 0x02;
            GetCurrencies.msgId = 0x01;
            GetCurrencies.msgOpCode = new byte[] { 0x00, 0x0B };
            GetCurrencies.bytesAnswer = 0x2B;
            EnableCurrencies.msgId = 0x01;
            EnableCurrencies.msgOpCode = new byte[] { 0x00, 0x0C };
            EnableCurrencies.bytesAnswer = 0x02;
            EnableDenom.msgId = 0x01;
            EnableDenom.msgOpCode = new byte[] { 0x00, 0x0D };
            EnableDenom.bytesAnswer = 0x02;
            SetDrumDenom.msgId = 0x01;
            SetDrumDenom.msgOpCode = new byte[] { 0x00, 0x0E };
            SetDrumDenom.bytesAnswer = 0x02;
            GetDrumDenom.msgId = 0x01;
            GetDrumDenom.msgOpCode = new byte[] { 0x00, 0x0F };
            GetDrumDenom.bytesAnswer = 0x0B;
            SetConfiguration.msgId = 0x01;
            SetConfiguration.msgOpCode = new byte[] { 0x00, 0x10 };
            SetConfiguration.bytesAnswer = 0x02;
            GetConfiguration.msgId = 0x01;
            GetConfiguration.msgOpCode = new byte[] { 0x00, 0x11 };
            GetConfiguration.bytesAnswer = 0x02;//allarmi=0x04 / dataora / cassetti / hub / unfit / cassetti
            UpdateFirmware.msgId = 0x01;
            UpdateFirmware.msgOpCode = new byte[] { 0x00, 0x12 };
            UpdateFirmware.bytesAnswer = 0x02;
            UpdateFirmware.timeOut = 1000;
            InitializeDrum.msgId = 0x01;
            InitializeDrum.msgOpCode = new byte[] { 0x00, 0x13 };
            InitializeDrum.bytesAnswer = 0x02;
            AssignDrumName.msgId = 0x01;
            AssignDrumName.msgOpCode = new byte[] { 0x00, 0x14 };
            AssignDrumName.bytesAnswer = 0x02;
            SetModuleInfo.msgId = 0x01;
            SetModuleInfo.msgOpCode = new byte[] { 0x00, 0x15 };
            SetModuleInfo.bytesAnswer = 0x02;
            SetModuleInfo.timeOut = 1500;
            GetModuleInfo.msgId = 0x01;
            GetModuleInfo.msgOpCode = new byte[] { 0x00, 0x16 };
            GetModuleInfo.bytesAnswer = 0x30; // lettore=0x76 / cassetto=0x3A
            TroyHorse.msgId = 0x01;
            TroyHorse.msgOpCode = new byte[] { 0x00, 0x17 };
            TroyHorse.bytesAnswer = 0x02; // + 2 + lunghezza risposta 
            LockUnlockUpperPart.msgId = 0x01;
            LockUnlockUpperPart.msgOpCode = new byte[] { 0x00, 0x18 };
            LockUnlockUpperPart.bytesAnswer = 0x02;
            ImportLog.msgId = 0x01;
            ImportLog.msgOpCode = new byte[] { 0x00, 0x19 };
            ImportLog.bytesAnswer = 0x05; // + lunghezza log (0-1024)
            GetUpperBlockState.msgId = 0x01;
            GetUpperBlockState.msgOpCode = new byte[] { 0x00, 0x1A };
            GetUpperBlockState.bytesAnswer = 0x03;
            GetDrumsPhysicalPos.msgId = 0x01;
            GetDrumsPhysicalPos.msgOpCode = new byte[] { 0x00, 0x1B };
            GetDrumsPhysicalPos.bytesAnswer = 0x09;
            Reassign.msgId = 0x01;
            Reassign.msgOpCode = new byte[] { 0x00, 0x1C };
            Reassign.bytesAnswer = 0x04;
            GetAllarmBoardType.msgId = 0x01;
            GetAllarmBoardType.msgOpCode = new byte[] { 0x00, 0x1D };
            GetAllarmBoardType.bytesAnswer = 0x03;
            GetSystemLife.msgId = 0x01;
            GetSystemLife.msgOpCode = new byte[] { 0x00, 0x1E };
            GetSystemLife.bytesAnswer = 0x2C;
            GetActiveCurrencies.msgId = 0x01;
            GetActiveCurrencies.msgOpCode = new byte[] { 0x00, 0x1F };
            GetActiveCurrencies.bytesAnswer = 0x04;
            SetNumActiveCurr.msgId = 0x01;
            SetNumActiveCurr.msgOpCode = new byte[] { 0x00, 0x20 };
            SetNumActiveCurr.bytesAnswer = 0x02;
            GetNumActiveCurr.msgId = 0x01;
            GetNumActiveCurr.msgOpCode = new byte[] { 0x00, 0x21 };
            GetNumActiveCurr.bytesAnswer = 0x03;
            SetActiveCurr.msgId = 0x01;
            SetActiveCurr.msgOpCode = new byte[] { 0x00, 0x22 };
            SetActiveCurr.bytesAnswer = 0x02;
            SetSystemLife.msgId = 0x01;
            SetSystemLife.msgOpCode = new byte[] { 0x00, 0x23 };
            SetSystemLife.bytesAnswer = 0x02;
            SetDrumName.msgId = 0x01;
            SetDrumName.msgOpCode = new byte[] { 0x00, 0x24 };
            SetDrumName.bytesAnswer = 0x02;
            GetNoteCounters.msgId = 0x01;
            GetNoteCounters.msgOpCode = new byte[] { 0x00, 0x25 };
            GetNoteCounters.bytesAnswer = 0x30;
            CheckCashDataError.msgId = 0x01;
            CheckCashDataError.msgOpCode = new byte[] { 0x00, 0x26 };
            CheckCashDataError.bytesAnswer = 0x03;
            ResetCashDataError.msgId = 0x01;
            ResetCashDataError.msgOpCode = new byte[] { 0x00, 0x27 };
            ResetCashDataError.bytesAnswer = 0x02;
            ReaderCalibration.msgId = 0x01;
            ReaderCalibration.msgOpCode = new byte[] { 0x00, 0x28 };
            ReaderCalibration.bytesAnswer = 0x02;
            //set safe door electronic lock // 0x01 0x0029
            // move shutter // 0x01 0x002B
            // read shutter position // 0x01 0x002C
            // program modules boot loader 0x01 0x002D
            // check consistency of system configuration // 0x01 0x002E
            // red sn of bn in drum module // 0x01 0x002F
            // get drum physical pos and mcu // 0x01 0x0030
            // read E2P block data // 0x01 0x0031
            // write E2P block data // 0x01 0x0032
            SystemReset.msgId = 0x01;
            SystemReset.msgOpCode = new byte[] { 0x00, 0x33 };
            SystemReset.bytesAnswer = 0x02;
            GetSystemConfiguration.msgId = 0x02;
            GetSystemConfiguration.msgOpCode = new byte[] { 0x00, 0x50 };
            GetSystemConfiguration.bytesAnswer = 0x08;
            GetStatus.msgId = 0x02;
            GetStatus.msgOpCode = new byte[] { 0x00, 0x51 };
            GetStatus.bytesAnswer = 0x04; // modulo
            GetCashData.msgId = 0x02;
            GetCashData.msgOpCode = new byte[] { 0x00, 0x52 };
            GetCashData.bytesAnswer = 0x0C;
            GetCashCount.msgId = 0x02;
            GetCashCount.msgOpCode = new byte[] { 0x00, 0x53 };
            GetCashCount.bytesAnswer = 0x0A;
            Hello.msgId = 0x02;
            Hello.msgOpCode = new byte[] { 0x00, 0x54 };
            Hello.bytesAnswer = 0x02;
            CheckStressTest.msgId = 0x02;
            CheckStressTest.msgOpCode = new byte[] { 0x00, 0x55 };
            CheckStressTest.bytesAnswer = 0x04; // +xx
            StopCashOperation.msgId = 0x02;
            StopCashOperation.msgOpCode = new byte[] { 0x00, 0x56 };
            StopCashOperation.bytesAnswer = 0x02;
            EndProcedure.msgId = 0x04;
            EndProcedure.msgOpCode = new byte[] { 0x00, 0x01 };
            EndProcedure.bytesAnswer = 0x02;
            MechanicalPartOpenClose.msgId = 0x04;
            MechanicalPartOpenClose.msgOpCode = new byte[] { 0x00, 0x02 };
            MechanicalPartOpenClose.bytesAnswer = 0x02;
            NotePresenceInBin.msgId = 0x04;
            NotePresenceInBin.msgOpCode = new byte[] { 0x00, 0x03 };
            NotePresenceInBin.bytesAnswer = 0x02;
            AlarmWarning.msgId = 0x04;
            AlarmWarning.msgOpCode = new byte[] { 0x00, 0x04 };
            AlarmWarning.bytesAnswer = 0x02;
            Tamper.msgId = 0x04;
            Tamper.msgOpCode = new byte[] { 0x00, 0x05 };
            Tamper.bytesAnswer = 0x02;
            TroyHorseReply.msgId = 0x04;
            TroyHorseReply.msgOpCode = new byte[] { 0x00, 0x06 };
            TroyHorseReply.bytesAnswer = 0x02;
            StressTest.msgId = 0x04;
            StressTest.msgOpCode = new byte[] { 0x00, 0x07 };
            StressTest.bytesAnswer = 0x02;
            CheckStressTest.msgId = 0x04;
            CheckStressTest.msgOpCode = new byte[] { 0x00, 0x08 };
            CheckStressTest.bytesAnswer = 0x02;
            StopCashOperation.msgId = 0x04;
            StopCashOperation.msgOpCode = new byte[] { 0x00, 0x09 };
            StopCashOperation.bytesAnswer = 0x02;
            GetPhotoStatus.msgId = 0x01;
            GetPhotoStatus.msgOpCode = new byte[] { 0x01, 0x00 };
            GetPhotoStatus.bytesAnswer = 0x03;
            PhotoAdjust.msgId = 0x01;
            PhotoAdjust.msgOpCode = new byte[] { 0x01, 0x01 };
            PhotoAdjust.bytesAnswer = 0x06;
            GetPhotoParameters.msgId = 0x01;
            GetPhotoParameters.msgOpCode = new byte[] { 0x01, 0x02 };
            GetPhotoParameters.bytesAnswer = 0x09;
            SetPhotoParameters.msgId = 0x01;
            SetPhotoParameters.msgOpCode = new byte[] { 0x01, 0x03 };
            SetPhotoParameters.bytesAnswer = 0x02;
            AdjustBin.msgId = 0x01;
            AdjustBin.msgOpCode = new byte[] { 0x01, 0x04 };
            AdjustBin.bytesAnswer = 0x02;
            AdjustBin.timeOut = 5000;
            PhotoAudit.msgId = 0x01;
            PhotoAudit.msgOpCode = new byte[] { 0x01, 0x05 };
            PhotoAudit.bytesAnswer = 0x05;
            FactoryAdjust.msgId = 0x01;
            FactoryAdjust.msgOpCode = new byte[] { 0x01, 0x06 };
            FactoryAdjust.bytesAnswer = 0x02;
            FactoryAdjust.timeOut = 1000;
            PhotoAdjustFactoryMode.msgId = 0x01;
            PhotoAdjustFactoryMode.msgOpCode = new byte[] { 0x01, 0x07 };
            PhotoAdjustFactoryMode.bytesAnswer = 0x0A;
            GetCassetteFunctionParameters.msgId = 0x03;
            GetCassetteFunctionParameters.msgOpCode = new byte[] { 0x01, 0x08 };
            GetCassetteFunctionParameters.bytesAnswer = 0x0E;
            GetPhotoCalibrationValues.msgId = 0x01;
            GetPhotoCalibrationValues.msgOpCode = new byte[] { 0x01, 0x09 };
            GetPhotoCalibrationValues.bytesAnswer = 0x0E;
            GetSwitchStatus.msgId = 0x01;
            GetSwitchStatus.msgOpCode = new byte[] { 0x01, 0x0A };
            GetSwitchStatus.bytesAnswer = 0x03;
            GetDiverterParameters.msgId = 0x01;
            GetDiverterParameters.msgOpCode = new byte[] { 0x01, 0x10 };
            GetDiverterParameters.bytesAnswer = 0x0C;
            SetDiverterParameters.msgId = 0x01;
            SetDiverterParameters.msgOpCode = new byte[] { 0x01, 0x11 };
            SetDiverterParameters.bytesAnswer = 0x02;
            OperateDiverter.msgId = 0x01;
            OperateDiverter.msgOpCode = new byte[] { 0x01, 0x12 };
            OperateDiverter.bytesAnswer = 0x04;
            OperateDiverter.timeOut = 2000;
            TestDiverter.msgId = 0x01;
            TestDiverter.msgOpCode = new byte[] { 0x01, 0x13 };
            TestDiverter.bytesAnswer = 0x02;
            GetMotorParameters.msgId = 0x01;
            GetMotorParameters.msgOpCode = new byte[] { 0x01, 0x20 };
            GetMotorParameters.bytesAnswer = 0x18;
            SetMotorParameters.msgId = 0x01;
            SetMotorParameters.msgOpCode = new byte[] { 0x01, 0x21 };
            SetMotorParameters.bytesAnswer = 0x02;
            OperateMotor.msgId = 0x01;
            OperateMotor.msgOpCode = new byte[] { 0x01, 0x22 };
            OperateMotor.bytesAnswer = 0x02;
            StopMotor.msgId = 0x01;
            StopMotor.msgOpCode = new byte[] { 0x01, 0x23 };
            StopMotor.bytesAnswer = 0x02;
            StopMotor.timeOut = 5000;
            TestMotor.msgId = 0x01;
            TestMotor.msgOpCode = new byte[] { 0x01, 0x24 };
            TestMotor.bytesAnswer = 0x02;
            GetFeedStrobeRevolutionNumber.msgId = 0x01;
            GetFeedStrobeRevolutionNumber.msgOpCode = new byte[] { 0x01, 0x25 };
            GetFeedStrobeRevolutionNumber.bytesAnswer = 0x08;
            GetFeedStrobeRevolutionNumber.timeOut = 10000;
            GetPowerParameters.msgId = 0x01;
            GetPowerParameters.msgOpCode = new byte[] { 0x01, 0x30 };
            GetPowerParameters.bytesAnswer = 0x0B;
            SetPowerParameters.msgId = 0x01;
            SetPowerParameters.msgOpCode = new byte[] { 0x01, 0x31 };
            SetPowerParameters.bytesAnswer = 0x02;
            OperatePower.msgId = 0x01;
            OperatePower.msgOpCode = new byte[] { 0x01, 0x32 };
            OperatePower.bytesAnswer = 0x02;
            OperatePower.timeOut = 5000;
            SetReferenceValue.msgId = 0x01;
            SetReferenceValue.msgOpCode = new byte[] { 0x01, 0x33 };
            SetReferenceValue.bytesAnswer = 0x02;
            GetReferenceValue.msgId = 0x01;
            GetReferenceValue.msgOpCode = new byte[] { 0x01, 0x34 };
            GetReferenceValue.bytesAnswer = 0x04;
            TestModeEnableDisable.msgId = 0x01;
            TestModeEnableDisable.msgOpCode = new byte[] { 0x01, 0x3A };
            TestModeEnableDisable.bytesAnswer = 0x02;
            TransferBanknotes.msgId = 0x01;
            TransferBanknotes.msgOpCode = new byte[] { 0x01, 0x3B };
            TransferBanknotes.bytesAnswer = 0x03;
            TransferBanknotes.timeOut = 12000;
            CheckHorTranspSpeed.msgId = 0x01;
            CheckHorTranspSpeed.msgOpCode = new byte[] { 0x01, 0x3C };
            CheckHorTranspSpeed.bytesAnswer = 0x08;
            CheckHorTranspSpeed.timeOut = 10000;
            TestCassetteModule.msgId = 0x01;
            TestCassetteModule.msgOpCode = new byte[] { 0x01, 0x3D };
            TestCassetteModule.bytesAnswer = 0x02;
            OperateShutter.msgId = 0x01;
            OperateShutter.msgOpCode = new byte[] { 0x01, 0x3E };
            OperateShutter.bytesAnswer = 0x02;
            GetShutterPosition.msgId = 0x01;
            GetShutterPosition.msgOpCode = new byte[] { 0x01, 0x3F };
            GetShutterPosition.bytesAnswer = 0x03;
            SetTestState.msgId = 0x01;
            SetTestState.msgOpCode = new byte[] { 0x01, 0x40 };
            SetTestState.bytesAnswer = 0x02;
            GetTestState.msgId = 0x01;
            GetTestState.msgOpCode = new byte[] { 0x01, 0x41 };
            GetTestState.bytesAnswer = 0x04;
            ResetE2P.msgId = 0x01;
            ResetE2P.msgOpCode = new byte[] { 0x01, 0x42 };
            ResetE2P.bytesAnswer = 0x02;
            ResetE2P.timeOut = 5000;
            SetModuleConfiguration.msgId = 0x01;
            SetModuleConfiguration.msgOpCode = new byte[] { 0x01, 0x43 };
            SetModuleConfiguration.bytesAnswer = 0x02;
            SetModuleConfiguration.timeOut = 8000;
            GetModuleConfiguration.msgId = 0x01;
            GetModuleConfiguration.msgOpCode = new byte[] { 0x01, 0x44 };
            GetModuleConfiguration.bytesAnswer = 0x03;
            TestReaderMagSensor.msgId = 0x01;
            TestReaderMagSensor.msgOpCode = new byte[] { 0x01, 0x45 };
            GetOpbBoardBinType.msgId = 0x01;
            GetOpbBoardBinType.msgOpCode = new byte[] { 0x01, 0x46 };
            GetOpbBoardBinType.bytesAnswer = 17;
            GetFeederReport.msgId = 0x01;
            GetFeederReport.msgOpCode = new byte[] { 0x01, 0x47 };
            GetFeederReport.bytesAnswer = 9;
            GetFeederReport.timeOut = 5000;
            GetPhBinCalibration.msgId = 0x01;
            GetPhBinCalibration.msgOpCode = new byte[] { 0x01, 0x48 };
            GetPhBinCalibration.bytesAnswer = 6;
            GetPowerState.msgId = 0x01;
            GetPowerState.msgOpCode = new byte[] { 0x01, 0x49 };
            GetPowerState.bytesAnswer = 2;
            CheckMotorMoving.msgId = 0x01;
            CheckMotorMoving.msgOpCode = new byte[] { 0x01, 0x4A };
            CheckMotorMoving.bytesAnswer = 4;
            CheckMotorMoving.timeOut = 6000;
            CheckLtmCableConnection.msgId = 0x01;
            CheckLtmCableConnection.msgOpCode = new byte[] { 0x01, 0x4B };
            CheckLtmCableConnection.bytesAnswer = 3;

            UiSetSolidLight.opCode = 0x4A;
            UiSetSolidLight.bytesAnswer = 0x00;
            UiSetSolidLight.timeOut = 100;
            UiGetStatus.opCode = 0x53;
            UiGetStatus.bytesAnswer = 0x02;
            UiGetStatus.timeOut = 100;
            UiGetFwRelease.opCode = 0x46;
            UiGetFwRelease.bytesAnswer = 0x02;
            UiGetFwRelease.timeOut = 100;
            UiStartLightEffect.opCode = 0x4C;
            UiStartLightEffect.bytesAnswer = 0x00;
            UiStartLightEffect.timeOut = 100;
            UiStopLightEffect.opCode = 0x4D;
            UiStopLightEffect.bytesAnswer = 0x00;
            UiStopLightEffect.timeOut = 500;
            UiWriteLightEffect.opCode = 0x4E;
            UiWriteLightEffect.bytesAnswer = 0x00;
            UiWriteLightEffect.timeOut = 500;
            UiReadLightEffect.opCode = 0x4F;
            UiReadLightEffect.bytesAnswer = 0x0A;
            UiReadLightEffect.timeOut = 100;
        }

        public byte[] SendCommand(SCommand command, params byte[] parameters)
        {
            // recupero le informazioni sul comando da inviare
            byte[] frame = MakeCmd(command.msgId, command.msgOpCode, parameters);
            if (command.timeOut == 0)
                command.timeOut = 500;
            int nBytes = 0;

            // preparo le stringhe da scrivere nel comScope
            dataReceived = "RCV:";
            dataSent = "SND:" + command.msgId.ToString("X2") + " " + command.msgOpCode[0].ToString("X2") + " " + command.msgOpCode[1].ToString("X2") + " ";
            for (int i = 0; i < parameters.Length; i++)
                dataSent += parameters[i].ToString("X2") + " ";

            // se la porta USB è chiusa la apro

            // apertura porte R/W

            return frame;



        }
        public byte[] MakeCmd(SCommand command, byte[] data = null)
        {
            byte[] frame = MakeCmd(command.msgId, command.msgOpCode, data);
            return frame;
        }

        public byte[] MakeCmd(byte msgId, byte[] msgOpCode, byte[] data)
        {
            byte STX = 0x02;
            byte[] len = new byte[2];
            byte ETX = 03;
            byte[] bCrc = new byte[2];
            byte EOT = 0x04;
            byte[] command = new byte[10];
            ushort crc = 0;
            
            short lenght;
            lenght = (short)(3);
            if (data != null)
            {
                command = new byte[10 + data.Length];
                lenght += (short)(data.Length);
                for (int i = 0; i < data.Length; i++)
                    command[i + 6] = data[i];
            }
            len = Arca.Short2ByteArray(lenght);
            command[0] = STX;
            command[1] = len[0];
            command[2] = len[1];
            command[3] = msgId;
            command[4] = msgOpCode[0];
            command[5] = msgOpCode[1];
            command[command.Length - 4] = ETX;

            for (int i = 0; i < 7; i++)
                crc += command[i];
            if (data != null)
            {
                for (int i = 7; i < 7 + data.Length; i++)
                    crc += command[i];
            }
            crc -= 1;
            bCrc = Arca.Short2ByteArray((short)~(crc));

            command[command.Length - 3] = bCrc[0];
            command[command.Length - 2] = bCrc[1];
            command[command.Length - 1] = EOT;
            return command;
        }
    }
    public class CanBusNovus
    {
        const byte CAN_MSG_EVENT            = 0x00;
        const byte CAN_MSG_CMD              = 0x01;
        const byte CAN_MSG_REPLY            = 0x02;

        const byte CAN_DATA_COMPLETE        = 0x00;
        const byte CAN_DATA_INCOMPLETE      = 0x01;

        const byte CMD_OK                   = 0x01;
        const byte CMD_KO                   = 0x00;
        const byte CMD_CORRUPT_HEADER       = 0x03;

        const byte CMD_GET_STATUS           = 0x01; // richiesta di stato
        const byte CMD_SINGLE_MODULE_PRESET = 0x02; // preset modulo singolo
        const byte CMD_DEPOSIT              = 0x03; // deposito
        const byte CMD_STOP                 = 0x05; // stop
        const byte CMD_SORTER_OPEN          = 0x07; // apri sorter
        const byte CMD_SORTER_CLOSE         = 0x08; // chiudi sorter
        const byte CMD_SET_CASS_ID          = 0x09; // imposta identificativo cassetto
        const byte CMD_GET_CASH_STATUS      = 0x0A; // richiesta di stato cassa
        const byte CMD_GET_DENOMINATION     = 0x0B; // richiesta di denominazione
        const byte CMD_SET_DENOMINATION     = 0x0C; // imposta denominazione
        const byte CMD_GET_MODULE_SN        = 0x0D; // richiesta di numero seriale del modulo
        const byte CMD_SET_MODULE_SN        = 0x0E; // imposta il numero seriale del modulo
        const byte CMD_GET_BOARD_SN         = 0x0F; // richiesta di numero seriale della scheda elettronica
        const byte CMD_SET_BOARD_SN         = 0x10; // imposta il numero seriale della scheda elettronica
        const byte CMD_GET_FW_RELEASE       = 0x11; // richiesta del numero di release del firmware
        const byte CMD_GET_FW_VERSION       = 0x12; // richiesta del numero di versione del firmware
        const byte CMD_REQ_FW_UPDATE        = 0x14; // richiesta di aggiornamento firmware
        const byte CMD_WITHDRAWALL          = 0x15; // prelievo
        const byte CMD_BN_IN_ARRIVE         = 0x16; // banconota in arrivo
        const byte CMD_SAFE_PRESET          = 0x17; // preset complessivo di cassaforte
        const byte CMD_CASSETTE_MAPPING     = 0X18; // esegue la mappa della posizione fisica dei cassetti
        const byte CMD_CASSETTE_ASSIGN      = 0x19; // assegna l'identificativo a tutti i cassetti
        const byte CMD_GET_CASSETTE_POS     = 0x1A; // richiesta posizione fisica dei cassetti
        const byte CMD_GET_MODULE_NAME      = 0x1D; // richiesta nome del modulo
        const byte CMD_PHOTO_ADJ            = 0x1E; // calibrazione foto sensori
        const byte CMD_SET_OP_CASSETTE      = 0x1F; // imposta i cassetti operativi
        const byte CMD_CHANGESTATE_EVENT_T  = 0x20; // test evento di cambio stato
        const byte CMD_LOG_EXPORT           = 0x21; // esporta log
        const byte CMD_SET_POWER            = 0x22; // gestione potenza
        const byte CMD_MOVE_VERT_MOT        = 0x23; // muovi motore traccia verticale
        const byte CMD_ASYNC_SYMC           = 0x24; // sincronizzazione per eventi asincroni
        const byte CMD_GET_PH_PAR           = 0x25; // leggi parametri foto sensore
        const byte CMD_SET_PH_PAR           = 0x26; // imposta parametri foto sensore
        const byte CMD_MOVE_PINCH           = 0x27; // alza / abbassa pinch roller
        const byte CMD_SORTER_MOVE          = 0x28; // muovi sorter
        const byte CMD_GET_PH_STATE         = 0x29; // leggi stato foto sensore
        const byte CMD_GET_SW_STATE         = 0x30; // leggi stato switch
        const byte CMD_GET_MOT_CUR          = 0x31; // leggi correnti motore
        const byte CMD_GET_MOT_VEL          = 0x32; // leggi velocità motore
        const byte CMD_GET_MOT_ACC          = 0x33; // leggi accelerazione motore
        const byte CMD_GET_MOT_PAR          = 0x34; // leggi parametri motore
        const byte CMD_SET_MOT_CUR          = 0x35; // imposta correnti motore
        const byte CMD_SET_MOT_VEL          = 0x36; // imposta velocità motore
        const byte CMD_SET_MOT_ACC          = 0x37; // imposta accelerazione motore
        const byte CMD_SET_MOT_PAR          = 0x38; // imposta parametri motore
        const byte CMD_GET_PWR_TH           = 0x39; // leggi soglia potenza
        const byte CMD_GET_PWR_CUR_V        = 0x3A; // leggi tensione e corrente potenza
        const byte CMD_SET_OVER_CUR         = 0x3B; // imposta soglia di controllo (over current)
        const byte CMD_CASSETTE_UNASSIGN    = 0x3C; // disassegna identificativo a tutti i cassetti
        const byte CMD_MOVE_TAPE            = 0x3D; // svolgi / riavvolgi bandella
        const byte CMD_REQ_BASC_FW_UPDATE   = 0x3E; // richiesta di aggiornamento firmware del basculante
        const byte CMD_BASC_FW_UPDATE       = 0x3F; // aggiornamento firmware basculante
        const byte CMD_GET_BASC_MODE        = 0x40; // modalità basculante
        const byte CMD_SET_PH_MAR           = 0x4D; // imposta percentuale marginatura per la calibrazione di tutti i foto
        const byte CMD_BREAK_PROCEDURE      = 0x41; // interrompi procedura in corso
        const byte CMD_PH_END_ADJ           = 0x42; // calibrazione foto bandella (foto END)
        const byte CMD_GET_UNIQUE_ID        = 0x43; // richiesta identificativo univoco (MCU id)
        const byte CMD_MOVE_CASS_MOT        = 0x47; // muovi motori del cassetto
        const byte CMD_GET_DEP_COUNTER      = 0x48; // restituisci contatori di deposito
        const byte CMD_GET_WITH_COUNTER     = 0x49; // restituisci contatori di prelievo
        const byte CMD_CANC_BASC_FW_UPDATE  = 0x4A; // abortisci aggiornamento firmware del basculante
        const byte CMD_SET_CASS_FLOOR_NUM   = 0x4B; // informa il cassetto su che piano di sorter è posizionato
        const byte CMD_SET_TEST_MODE        = 0x4C; // abilita / disabilita modalità di collaudo
        const byte CMD_SET_PHOTO_TH         = 0x4D; // imposta percentuale di marginazione per la calibrazione di tutti i foto
        const byte CMD_GET_ELM_TIME         = 0x4E; // leggi tempi elettromagnete
        const byte CMD_GET_ELM_CUR          = 0x4F; // leggi correnti elettromagnete
        const byte CMD_SET_ELM_TIME         = 0x50; // imposta tempi elettromagnete
        const byte CMD_SET_ELM_CUR          = 0x51; // imposta correnti elettromagnete
        const byte CMD_MOVE_ELM             = 0x52; // movimentazione Elettromagnete
        const byte CMD_GET_BASC_FW_RELEASE  = 0x53; // Richiesta del numero di release del firmware del basculante
        const byte CMD_GET_BASC_FW_VERSION  = 0x54; // Richiesta del numero di versione del firmware del basculante
        const byte CMD_GET_BASC_NAME        = 0x55; // Richiesta nome del modulo basculante
        const byte CMD_WRITE_E2P            = 0x56; // Scrivi un byte sulla E2P
        const byte CMD_READ_E2P             = 0x57; // Leggi un byte dalla E2P
        const byte CMD_RESTORE_E2P          = 0x58; // Reimpostazione della E2P
        const byte CMD_CASSETTE_IS_EMPTY    = 0x5A; // Verifica se il cassetto è vuoto (solo se il sorter è aperto)
        const byte CMD_GET_TRACKPHOTO_COUNT = 0x5B; // Legge i contatori di banconote dei foto di traccia
        const byte CMD_POSS_CASS_SQ         = 0x5C; // Restituisce l’informazione di possibile squadro del cassetto
        const byte CMD_GET_UNIQUE_CASS_ID   = 0x5D; // Restituisce identificativo univoco del cassetto (MCU)
        const byte CMD_GET_MCU_ID           = 0x5E; // Restituisce identificativo univoco del cassetto che si trova nella posizione specifica
        const byte CMD_CHK_VERT_TRACK       = 0x5F; // Disabilita/Abilita controlli sulla traccia verticale
        const byte CMD_ADD_BN_WITHDR        = 0x60; //Aggiungi numero di banconote al totale da prelevare (solo con procedura in corso)
        const byte CMD_RESET_CASS_SQ        = 0x61; //Azzera la condizione di possibile squadro (solo se il cassetto è vuoto fisicamente)
        const byte CMD_RAW_PH_ADJUST        = 0x62; //Calibrazione grezza dei foto sensori
        const byte CMD_PHOTO_AUDIT          = 0x63; // Esegue audit del foto sensore
        const byte CMD_GET_CASS_WORK        = 0x64; //Legge alcune informazioni di lavoro del cassetto
        const byte CMD_FACTORY_PH_ADJUST    = 0x65; //Calibrazione factory del foto sensore
        const byte CMD_GET_PH_V_ROM         = 0x66; //Lettura parametri del foto sensore da memoria non volatile
        const byte CMD_SET_PH_V_ROM         = 0x67; //Scrittura parametri del foto sensore in memoria non volatile
        const byte CMD_RESTORE_PH_VALUE     = 0x68; //Rispristina parametri del foto sensore
        const byte CMD_PH_AUDIT             = 0x69; // Esegue audit del foto sensore
        const byte CMD_ADC_READ             = 0x6B; // Leggi dati da ADC
        const byte CMD_SET_TEST_VALIDATION  = 0x6C; // Valida collaudo nodulo
        const byte CMD_GET_TEST_VALIDATION  = 0x6E; // Verifica validazione collaudo modulo
        const byte CMD_RESTORE_SAFE_E2P     = 0x6F; // Reimpostazione della E2P SAFE
        const byte CMD_SET_BN_HEIGTH        = 0x70; // Imposta altezza di banconota
        const byte CMD_SET_CFG_PARAM        = 0x71; // Imposta parametro di configurazione
        const byte CMD_GET_CFG_PARAM        = 0x72; // Leggi parametro di configurazione
        const byte CMD_DISABLE_5V           = 0x73; // Imposta disabilitazione tensione logica cassetti (5V)
        const byte CMD_SET_ERROR_STATE      = 0x75; // Imposta stato di errore
        const byte CMD_GET_BL_INFO          = 0x77; // Leggi informazioni del boot loader
        const byte CMD_BL_PROGRAM           = 0x78; // Esegui la programmazione del boot loader
        const byte CMD_SAFE_RECOVERY        = 0x7B; // Recovery di cassaforte
        const byte CMD_FW_UPDATE            = 0x7F; // trasferimento byte del firmware

        const byte MODULE_ID_BROADCAST      = 0x00;
        const byte MODULE_ID_UPPER_CTRL     = 0x01;
        const byte MODULE_ID_READER         = 0x02;
        const byte MODULE_ID_PH_CONC1       = 0x03;
        const byte MODULE_ID_PH_CONC2       = 0x04;
        const byte MODULE_ID_PH_CONC3       = 0x05;
        const byte MODULE_ID_SHUTTER        = 0x06;
        const byte MODULE_ID_SAFE           = 0x14;
        const byte MODULE_ID_CASS_A         = 0x15;  
        const byte MODULE_ID_CASS_B         = 0x16;  
        const byte MODULE_ID_CASS_C         = 0x17;  
        const byte MODULE_ID_CASS_D         = 0x18;  
        const byte MODULE_ID_CASS_E         = 0x19;  
        const byte MODULE_ID_CASS_F         = 0x10;  
        const byte MODULE_ID_CASS_G         = 0x1A;  
        const byte MODULE_ID_CASS_H         = 0x1B;  
        const byte MODULE_ID_CASS_I         = 0x1C;  
        const byte MODULE_ID_CASS_J         = 0x1D;  
        const byte MODULE_ID_CASS_K         = 0x1E;  
        const byte MODULE_ID_CASS_L         = 0x20;
        const byte MODULE_ID_BAG            = 0x21;
        const byte MODULE_ID_ALARM          = 0x22;
        const byte MODULE_ID_BASC_          = 0xE1;
        const byte MODULE_ID_UI             = 0xE0;

        const byte PHOTO_OUTL_ID            = 0x00;
        const byte PHOTO_OUTR_ID            = 0x01;
        const byte PHOTO_TRACK_ID           = 0x02;
        const byte PHOTO_END_ID             = 0x03;
        const byte PHOTO_SORTER_ID          = 0x04;

        const byte ENABLE                   = 0x01; // abilita
        const byte DISABLE                  = 0x00; // disabilita
        const byte PINCH_UP                 = 0x00; // alza
        const byte PINCH_DOWN               = 0x01; // abbassa
        const byte TAPE_WRAP                = 0x01; // avvolgi bandella
        const byte TAPE_UNWRAP              = 0x00; // svolgi bandella
        const byte TAPE_STOP                = 0x02; // ferma bandella
        const byte MOTOR_DIRECTION_INPUT    = 0x00; // Direzine movimentazione motore INPUT
        const byte MOTOR_DIRECTION_OUTPUT   = 0x01; // Direzione movimentazione motore OUTPUT
        const byte MOTOR_DIRECTION_STOP     = 0x02; // Arresto movimentazione motore

        public static SerialPort mySerial = new SerialPort();
        public string commandAnswer = "";
        public string commandSent = "";
        public string serialPortName = "";
        public event EventHandler DataReceived;
        public event EventHandler DataSent;
        int byteReceived = 0;

       

        public struct SCanBusFrame
        {
            public string destinationID;
            public string sourceID;
            public string messageType;
            public string functionID;
            public string dataComplete;
            public byte[] data;
            public byte[] eventData;
        }
        public SCanBusFrame canFrame = new SCanBusFrame();

        public void OpenCom()
        {
            //mySerial.Close();
            try
            {
                if (!mySerial.IsOpen)
                {
                    mySerial.Open();
                    mySerial.DiscardInBuffer();
                    mySerial.DiscardOutBuffer();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void CloseCom()
        {
            if (mySerial.IsOpen)
                mySerial.Close();
        }

        public void ConfigCom(string portName, int baudRate = 2000000, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            mySerial.PortName = portName;
            mySerial.BaudRate = baudRate;
            mySerial.Parity = parity;
            mySerial.DataBits = dataBits;
            mySerial.StopBits = stopBits;
            serialPortName = portName;
            mySerial.ReadTimeout = 5000;
        }

        static byte[] MakeFrameID(int DestinationID, int SourceID, int messageType, int dataComplete, int functionID, params byte[] data) // Command
        {
            byte[] result;
            byte[] prefix = { 0xAA, 0xE0 };
            prefix[1] += (byte)data.Length;
            byte[] suffix = { 0x55 };

            string comando = "000000" + Arca.Dec2Bin(DestinationID, 6) + Arca.Dec2Bin(SourceID, 6) + Arca.Dec2Bin(messageType, 3) + Arca.Dec2Bin(dataComplete, 1) + Arca.Dec2Bin(functionID, 10);
            byte[] comandoB = Arca.Bin2ByteArray(comando);
            Array.Reverse(comandoB);
            result = new byte[comandoB.Length + data.Length + prefix.Length + suffix.Length];
            Array.Copy(prefix, result, prefix.Length);
            Array.Copy(comandoB, 0, result, prefix.Length, comandoB.Length);
            Array.Copy(data, 0, result, comandoB.Length + prefix.Length, data.Length);
            Array.Copy(suffix, 0, result, comandoB.Length + prefix.Length + data.Length, suffix.Length);
            return result;
        }

        public void SetAndStart()
        {
            byte[] setup = { 0xAA, 0x55, 0x12, 0x03, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x17 };
            if (!mySerial.IsOpen)
                mySerial.Open();
            mySerial.Write(setup, 0, setup.Length);
            mySerial.Close();
        }

        public byte[] SendCommand(byte[] dataToSend, int timeout = 1000)
        {
            //WaitEvent();
            List<byte[]> prova = new List<byte[]>();
            mySerial.DiscardInBuffer();
            commandSent = BitConverter.ToString(dataToSend);
            TranslateFrame(dataToSend);
            DataSent(commandSent, null);
            mySerial.Write(dataToSend, 0, dataToSend.Length);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            aspetta:
            byte[] received = new byte[mySerial.BytesToRead];
            if (received.Length == 0 && timeout != 0)
            {
                if (sw.ElapsedMilliseconds > timeout)
                {
                    canFrame.data = new byte[1];
                    DataReceived("NO ANSWER", null);
                    goto fine;
                }
                goto aspetta;
            }
            leggi:
            received = new byte[mySerial.BytesToRead];
            Arca.WaitingTime(15);
            if (received.Length != mySerial.BytesToRead)
                goto leggi;
            mySerial.Read(received, 0, received.Length);
            Arca.WaitingTime(50);
            TranslateFrame(received);
            if (canFrame.data.Length == 0 && canFrame.eventData.Length > 0)
                if (canFrame.eventData[1] == 0x41)
                {
                    attesa:
                    Arca.WaitingTime(100);
                    if (mySerial.BytesToRead == 0 && sw.ElapsedMilliseconds < timeout)
                        goto attesa;
                    goto leggi;
                }
            //if (canFrame.dataComplete == "1" && sw.ElapsedMilliseconds < timeout)
            //    while (sw.ElapsedMilliseconds < timeout)
            //        if (mySerial.BytesToRead > 0)
            //            goto leggi;
            if (received.Length > 0)
                DataReceived(BitConverter.ToString(received), null);
            else
                DataReceived("NO ANSWER", null);
            fine:
            //TranslateFrame(received);
            return canFrame.data;
        }

        public byte[] WaitEvent()
        {
            //mySerial.Open();
            byte[] received = new byte[mySerial.BytesToRead];
            if (received.Length > 0)
            {
                received = new byte[mySerial.BytesToRead];
                mySerial.Read(received, 0, mySerial.BytesToRead);
                DataReceived(BitConverter.ToString(TranslateFrame(received)), null);
            }
            //mySerial.Close();
            return canFrame.data;
        }

        public void SendBytes(byte[] dataToSend, int byteAnswer = 0)
        {
            mySerial.Write(dataToSend, 0, dataToSend.Length);
            if (byteAnswer > 0)
            {
                while (mySerial.BytesToRead == 0)
                {
                    Arca.WaitingTime(1);
                }
                mySerial.DiscardInBuffer();
            }
        }

        public void SendFirmware(byte[] dataToSend)
        {
            mySerial.Write(dataToSend, 0, dataToSend.Length);
            while (mySerial.BytesToRead == 0)
            {
                Arca.WaitingTime(1);
            }
            mySerial.DiscardInBuffer();
            //Arca.WaitingTime(1);
            //byte[] received = new byte[mySerial.BytesToRead];
            //mySerial.Read(received, 0, mySerial.BytesToRead);
        }

        public byte[] TranslateFrame(byte[] received)
        {
            List<byte> dataCmd = new List<byte>();
            List<byte> dataEvent = new List<byte>();
            string frame = "";
            canFrame = new SCanBusFrame();
            int datiLenght = 0, datiTot = 0, esitoComando = -100;
            
            if (received.Length > 6)
            {
                int idFrame = 0;
                while(idFrame < received.Length-1)
                {
                    //if (received.Length - idFrame >= 7)
                    {
                        datiLenght = received[idFrame + 1] - 0xE0;
                        datiTot += datiLenght;
                        frame = Arca.Byte2Bin(received[idFrame + 5]) + Arca.Byte2Bin(received[idFrame + 4]) + Arca.Byte2Bin(received[idFrame + 3]) + Arca.Byte2Bin(received[idFrame + 2]);
                        canFrame.destinationID = Arca.Bin2Hex(frame.Substring(6, 6));
                        canFrame.sourceID = Arca.Bin2Hex(frame.Substring(12, 6));
                        canFrame.messageType = Arca.Bin2Hex(frame.Substring(18, 3));
                        canFrame.functionID = Arca.Bin2Hex(frame.Substring(22, 10));
                        canFrame.dataComplete = Arca.Bin2Hex(frame.Substring(21, 1));
                        int startCmdIndex = 0;
                        switch (canFrame.messageType)
                        {
                            case "0": // EVENT
                                for (int i = received[idFrame + 6]; i < datiLenght; i++)
                                    dataEvent.Add(received[idFrame + 6 + i]);
                                break;
                            case "1": // CMD
                            case "2": // REPLY
                                if (esitoComando != -100)
                                    startCmdIndex = 1;
                                else
                                    esitoComando = received[idFrame + 6];
                                for (int i = startCmdIndex; i < datiLenght; i++)
                                    dataCmd.Add(received[idFrame + 6 + i]);
                                break;
                        }
                    }
                    idFrame += 7 + datiLenght;
                    
                }
                canFrame.data = new byte[dataCmd.Count];
                canFrame.eventData = new byte[dataEvent.Count];
                for (int i = 0; i < canFrame.data.Length; i++)
                    canFrame.data[i] = dataCmd[i];
                for (int i = 0; i < canFrame.eventData.Length; i++)
                    canFrame.eventData[i] = dataEvent[i];
            }

            byte[] error = new byte[1];    
            return error;
        }

        public byte[] CmdSetPower(byte destinationID, byte powerState)
        {
            // 0x22 + azione
            // 0x00=OFF / 0x01=ON
            if(!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_POWER, powerState);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetPower(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_PWR_CUR_V);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdRestoreE2P(byte destinationID)
        {
            byte[] frame = { 0x00 };
            if (!mySerial.IsOpen)
                mySerial.Open();
            switch (destinationID)
            {
                case MODULE_ID_BROADCAST:
                    frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_RESTORE_E2P);
                    break;
                case MODULE_ID_SAFE:
                    frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_RESTORE_SAFE_E2P);
                    break;
            }
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetE2PByte(byte destinationID, byte address)
        {
            byte[] entireAddress = new byte[4];
            entireAddress[3] = address;
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_READ_E2P, entireAddress);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdSetTrayNumber(int cassetteNumber = 8)
        {
            byte casNum = Convert.ToByte(cassetteNumber / 2);
            byte[] bitMask = { 0x0F, 0xFF, casNum };
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(MODULE_ID_SAFE, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_OP_CASSETTE, bitMask);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetStatus(byte destinationID)
        {
            if(!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_STATUS);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        
        public byte[] CmdGetBascMode(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_BASC_MODE);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdDisable5V(byte destinationID, byte state) // state=0x01 = 5V disabilitato - 0x00=5V abilitato
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_DISABLE_5V, state);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdPreset(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SINGLE_MODULE_PRESET);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[]CmdGetModuleName(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_MODULE_NAME);
            byte[] result = SendCommand(frame, 1500);
            mySerial.Close();
            return result;
        }
        public byte[] CmdSetValidateTest(byte destinationID, byte value)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_TEST_VALIDATION, value);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdGetValidateTest(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_TEST_VALIDATION);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdGetBascName(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_BASC_NAME);
            byte[] result = SendCommand(frame, 1500);
            mySerial.Close();
            return result;
        }
        public byte[] CmdGetBascFwRelease(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_BASC_FW_RELEASE);
            byte[] result = SendCommand(frame, 1500);
            mySerial.Close();
            return result;
        }
        public byte[] CmdGetBascFwVersion(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_BASC_FW_VERSION);
            byte[] result = SendCommand(frame, 1500);
            mySerial.Close();
            return result;
        }

        public byte[]CmdGetFwRelease(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_FW_RELEASE);
            byte[] result = SendCommand(frame, 1500);
            mySerial.Close();
            return result;
        }
        public byte[]CmdGetFwVersion(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_FW_VERSION);
            byte[] result = SendCommand(frame, 1500);
            mySerial.Close();
            return result;
        }
        public byte[]CmdFwUpdateRequest(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_REQ_FW_UPDATE);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdFwBascUpdateRequest(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_REQ_BASC_FW_UPDATE);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdElmOpen(byte destinationID, byte state, byte idElm = 0x00)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_MOVE_ELM, idElm, state);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdFwUpload(byte destinationID, byte[] fwData, byte dataComplete = CAN_DATA_INCOMPLETE)
        {
            //if(!mySerial.IsOpen)
            //    mySerial.Open();
            byte[] result = { 0x00 };
            byte[] data = new byte[8];
            byte[] frame;

            for (int i = 0; i < fwData.Length; i += 8)
            {
                //result = new byte[1];
                if (fwData.Length - i < 8)
                {
                    data = new byte[fwData.Length - i];
                    frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_FW_UPDATE, data);
                }
                else
                    frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_INCOMPLETE, CMD_FW_UPDATE, data);

                Array.Copy(fwData, i, frame, 6, data.Length);
                SendFirmware(frame);
            }
            return result;
        }

        public byte[] UpdateFwCassette(byte destinationID, byte[] fwData)
        {
            byte[] result = { 0x00 };
            int dim = 8;
            byte[] data = new byte[dim];
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_INCOMPLETE, CMD_FW_UPDATE, data);
            for (int col = 0; col < fwData.Length; col += dim)
            {
                if (fwData.Length - col < dim)
                {
                    dim = fwData.Length - col;
                    frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_FW_UPDATE, data);
                }
                Array.Copy(fwData, col, frame, 6, dim);
                SendFirmware(frame);
            }
            dim = 8;
            frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_INCOMPLETE, CMD_FW_UPDATE, data);
            return result;
        }
        public byte[] UpdateFwBasc(byte destinationID, byte[] fwData)
        {
            byte[] result = { 0x00 };
            int dim = 8;
            byte[] data = new byte[dim];
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_INCOMPLETE, CMD_BASC_FW_UPDATE, data);
            for (int col = 0; col < fwData.Length; col += dim)
            {
                if (fwData.Length - col < dim)
                {
                    dim = fwData.Length - col;
                    frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_BASC_FW_UPDATE, data);
                }
                Array.Copy(fwData, col, frame, 6, dim);
                SendFirmware(frame);
            }
            dim = 8;
            frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_INCOMPLETE, CMD_BASC_FW_UPDATE, data);
            return result;
        }

        public byte[] CmdFwUpdateAbort(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_CANC_BASC_FW_UPDATE);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[]CmdSorterOpen(byte destinationID, byte sorterPosition)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SORTER_OPEN);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[]CmdSorterClose(byte destinationID, byte sorterPosition)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SORTER_CLOSE);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[]CmdSetCassetteID(byte cassetteID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(MODULE_ID_BROADCAST, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_CASS_ID, cassetteID);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdAssignAllCassettes(byte cassetteID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(cassetteID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_CASSETTE_ASSIGN);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetCassettePosition()
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(MODULE_ID_SAFE, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_CASSETTE_POS);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[]CmdSetTestMode (byte destinationID, byte testMode)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_TEST_MODE, testMode);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetModuleSN(byte destinationID)
        {
            List<byte> myList = new List<byte>();
            byte[] result = { 0x00 };
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_MODULE_SN);
        invioComando:
            byte[] temp = SendCommand(frame);
            for (int i = 1; i < canFrame.data.Length; i++)
            {
                myList.Add(canFrame.data[i]);
            }
            if (canFrame.dataComplete == "1")
                goto invioComando;
            mySerial.Close();
            result = myList.ToArray();
            return result;
        }

        public byte[]CmdSetModuleSN(byte destinationID, string serialNumber)
        {
            serialNumber = serialNumber.PadRight(16, '0');
            byte[] serial1 = Arca.Ascii2ByteArray(serialNumber.Substring(0, 8));
            byte[] serial2 = Arca.Ascii2ByteArray(serialNumber.Substring(8, 8));
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_INCOMPLETE, CMD_SET_MODULE_SN, serial1);
            byte[] result = SendCommand(frame);
            frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_MODULE_SN, serial2);
            result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdTestValidation(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_TEST_VALIDATION, 0x01);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdSetSafeCfg(byte destinationID, byte parameter, byte value)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] param = { parameter, value };
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_CFG_PARAM, parameter, value); 
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetSafeCfg(byte destinationID, byte parameter)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_CFG_PARAM, parameter);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[]CmdPhotoAdjust(byte destinationID, byte photoID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_PHOTO_ADJ, photoID);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdEventDisabling(byte destinationID, byte disable)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, 268, disable);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetPhotoParameters(byte destinationID, byte photoID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_PH_PAR, photoID);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return canFrame.data;
            
        }

        public byte[] CmdSetPhotoParameters(byte destinationID, byte photoID, byte soglia, byte agc, short corrente, byte polarità)
        {
            byte[] parameters = new byte[6];
            parameters[0] = photoID; // direzione
            parameters[1] = soglia;
            parameters[2] = agc;
            byte[] curr = Arca.Short2ByteArray(corrente);
            Array.Copy(curr, 0, parameters, 3, 2);
            parameters[5] = polarità;
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_PH_PAR, parameters);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return canFrame.data;

        }

        public byte[] CmdPhotoFactoryAdjust(byte destinationID, byte photoID, byte parameter)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_FACTORY_PH_ADJUST, photoID, parameter);
            byte[] result = SendCommand(frame, 10000);
            mySerial.Close();
            return canFrame.data;
        }

        public byte[]CmdPhotoEndAdjust(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_PH_END_ADJ);
            byte[] result = SendCommand(frame, 70000);
            if (result.Length == 1)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (sw.ElapsedMilliseconds < 20000 && result.Length == 1 )
                {
                    Arca.WaitingTime(100);
                    if (mySerial.BytesToRead > 0)
                    {
                        result = new byte[mySerial.BytesToRead];
                        mySerial.Read(result, 0, mySerial.BytesToRead);
                        TranslateFrame(result);
                        DataReceived(BitConverter.ToString(result), null);
                    }
                }
            }
            
            mySerial.Close();
            return canFrame.data;
        }

        public byte[] CmdGetPhotoParameter(byte destinationID, byte photoID, byte parameterType)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_PH_V_ROM, photoID, parameterType);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        

        public byte[]CmdGetPhotoStatus(byte destinationID, byte photoID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_PH_STATE);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetSwitchStatus(byte destinationID, byte switchID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_SW_STATE, switchID);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[]CmdSorterOpen(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SORTER_OPEN);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdMoveSorter(byte sorterID, byte action)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(MODULE_ID_SAFE, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SORTER_MOVE, sorterID, action);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdSetCassFloor(byte floor)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(MODULE_ID_CASS_B, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_CASS_FLOOR_NUM, floor);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        
        public byte[]CmdSorterClose(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SORTER_CLOSE);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[]CmdMovePinchRoller(byte destinationID, byte pinchPosition)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_MOVE_PINCH, pinchPosition);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetADCValues(byte destinationID)
        {
            byte[] result = { 0x00 };
            List<byte> myList = new List<byte>();
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_ADC_READ);
            invioComando:
            byte[] temp = SendCommand(frame);
            for (int i = 1; i < canFrame.data.Length; i++)
            {
                myList.Add(canFrame.data[i]);
            }
            if (canFrame.dataComplete == "1")
                goto invioComando;
            mySerial.Close();
            result = myList.ToArray();
            return result;
        }

        public byte[] CmdGetCurrMotor(byte motorID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(MODULE_ID_SAFE, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_MOT_CUR, motorID);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }

        public byte[] CmdMoveMivMotor(byte direction)
        {
            byte[] parameters = new byte[7];
            parameters[0] = direction; // direzione
            parameters[1] = 0x01;
            parameters[2] = 0x22;
            parameters[3] = 0xFF;
            parameters[4] = 0xFF;
            parameters[5] = 0xFF;
            parameters[6] = 0xFF;
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(MODULE_ID_SAFE, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_MOVE_VERT_MOT, parameters);
            byte[] result = SendCommand(frame, 5000);
            mySerial.Close();
            return result;
        }

        public byte[] CmdStopMivMotor()
        {
            byte[] parameters = new byte[7];
            parameters[0] = 0x02;
            parameters[1] = 0xFF;
            parameters[2] = 0xFF;
            parameters[3] = 0xFF;
            parameters[4] = 0xFF;
            parameters[5] = 0xFF;
            parameters[6] = 0xFF;
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(MODULE_ID_SAFE, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_MOVE_VERT_MOT, parameters);
            byte[] result = SendCommand(frame, 5000);
            mySerial.Close();
            return result;
        }

        public byte[]CmdTapeMove(byte destinationID, byte action)
        {
            //0x00 Svolgi
            //0x01 Avvolgi
            //0x02 Stop
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_MOVE_TAPE, action);
            byte[] result = SendCommand(frame, 5000);
            mySerial.Close();
            return result;
        }
        public byte[]CmdCassetteMotorMove(byte destinationID, byte direction, int velocità, int nPassi)
        {
            byte[] parameters = new byte[7];
            parameters[0] = direction;
            byte[] vel = Arca.Short2ByteArray((short)velocità, 2);
            parameters[1] = vel[0];
            parameters[2] = vel[1];
            byte[] passi = Arca.Int2ByteArray(nPassi, 4);
            parameters[3] = passi[0];
            parameters[4] = passi[1];
            parameters[5] = passi[2];
            parameters[6] = passi[3];
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_MOVE_CASS_MOT, parameters);
            byte[] result = SendCommand(frame, 4000 + nPassi * 3);
            mySerial.Close();
            return result;
        }

        public byte[] CmdGetElmCurrent(byte destinationID)
        {
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_ELM_CUR);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[] CmdSetElmCurrent(byte destinationID, byte elmID, short massima, short impulso, short mantenimento)
        {
            // pre/mant/post -> mA
            // 0x7FFF = default
            byte[] dati = new byte[7];
            dati[0] = elmID;
            byte[] numero = Arca.Short2ByteArray(massima);
            dati[1] = numero[0];
            dati[2] = numero[1];
            numero = Arca.Short2ByteArray(impulso);
            dati[3] = numero[0];
            dati[4] = numero[1];
            numero = Arca.Short2ByteArray(mantenimento);
            dati[5] = numero[0];
            dati[6] = numero[1];
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_ELM_CUR, dati);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }


        public byte[]CmdGetMotorCurrent(byte destinationID, byte motorID)
        {
            // 0x00 Motore trasporto verticale
            // 0x01 Motore left cassetto
            // 0x02 Motore right cassetto
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_GET_MOT_CUR);
            byte[] result = SendCommand(frame);
            mySerial.Close();
            return result;
        }
        public byte[]CmdSetMotorCurrent(byte destinationID, byte motorID, short preEccitazione, short mantenimento, short postEccitazione)
        {
            // pre/mant/post -> mA
            // 0x7FFF = default
            byte[] dati = new byte[7];
            dati[0] = motorID;
            byte[] numero = Arca.Short2ByteArray(preEccitazione);
            dati[1] = numero[0];
            dati[2] = numero[1];
            numero = Arca.Short2ByteArray(mantenimento);
            dati[3] = numero[0];
            dati[4] = numero[1];
            numero = Arca.Short2ByteArray(postEccitazione);
            dati[5] = numero[0];
            dati[6] = numero[1];
            if (!mySerial.IsOpen)
                mySerial.Open();
            byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_SET_MOT_CUR,dati);
            byte[] result = SendCommand(frame,2000);
            mySerial.Close();
            return result;
        }
        //public byte[]Cmd(byte destinationID)
        //{
        //    mySerial.Open();
        //    byte[] frame = MakeFrameID(destinationID, MODULE_ID_UPPER_CTRL, CAN_MSG_CMD, CAN_DATA_COMPLETE, CMD_);
        //    byte[] result = SendCommand(frame);
        //    mySerial.Close();
        //    return result;
        //}
        
    }

    public class RS232Novus

    {
        


        public static SerialPort mySerial = new SerialPort();
        Stopwatch myTimer = new Stopwatch();
        public string answerString;
        public byte[] answerByte;
        public string commandAnswer = "";
        public string commandSent = "";
        public string serialPortName = "";
        public event EventHandler DataReceived;
        public event EventHandler DataSent;
        int byteReceived = 0;
        

        public RS232Novus(string comPort = "COM1", int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            ConfigCom(comPort, baudRate, parity, dataBits, stopBits);
            myTimer.Reset();
        }


        /// <summary>
        /// Open the COM port if closed
        /// </summary>
        public void OpenCom()
        {
            //mySerial.Close();
            try
            {
                if (!mySerial.IsOpen)
                {
                    //mySerial.ReadBufferSize = 100000;
                    //mySerial.WriteBufferSize = 100000;
                    mySerial.Open();
                    mySerial.DiscardInBuffer();
                    mySerial.DiscardOutBuffer();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Close the COM port if opened
        /// </summary>
        public void CloseCom()
        {
            if (mySerial.IsOpen)
                mySerial.Close();
        }

        /// <summary>
        /// Setting of COM port paramiters
        /// </summary>
        /// <param name="portName">Name of the port (ex. COM1)</param>
        /// <param name="baudRate">Baudrate</param>
        /// <param name="parity">Type of parity control</param>
        /// <param name="dataBits">Number of Data bits</param>
        /// <param name="stopBits">Number of Stop bits</param>
        public void ConfigCom(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            mySerial.PortName = portName;
            mySerial.BaudRate = baudRate;
            mySerial.Parity = parity;
            mySerial.DataBits = dataBits;
            mySerial.StopBits = stopBits;
            serialPortName = portName;
            mySerial.ReadTimeout = 5000;
        }

        public string WaitingForData(int timeOut = 5000)
        {
            int bytesReceived = 0;
            commandAnswer = "";
            myTimer.Restart();
            do
            {
                if (myTimer.ElapsedMilliseconds > timeOut)
                {
                    commandAnswer = "NO ANSWER";
                    goto endCommand;
                }
                bytesReceived = mySerial.BytesToRead;
                Arca.WaitingTime(100);
            } while (mySerial.BytesToRead == 0 && commandAnswer == "");
            if (commandAnswer != "")
                goto endCommand;
            byte[] dataReceived = new byte[mySerial.BytesToRead];
            mySerial.Read(dataReceived, 0, mySerial.BytesToRead);
            for (int i = 0; i < dataReceived.Length; i++)
            {
                commandAnswer += Arca.Dec2Hex(dataReceived[i], 2);
            }
            commandAnswer = Arca.Hex2Ascii(commandAnswer);

            endCommand:
            DataReceived(commandAnswer, null);
            return commandAnswer;



        }

        public byte[] SendRecByteArray(byte[] dataToSend, int nByteAnswer = 0, int timeout = 200)
        {
            commandSent = BitConverter.ToString(dataToSend);
            byte[] dataReceived = new byte[1];
            mySerial.DiscardInBuffer();
            commandAnswer = "";
            myTimer.Restart();
            DataSent(commandSent, null);

            mySerial.Write(dataToSend, 0, dataToSend.Length);
            int bytesReceived = 0;
            Arca.WaitingTime(10);
            if (nByteAnswer > 0)
            {
                do
                {
                    if (myTimer.ElapsedMilliseconds > timeout)
                    {
                        commandAnswer = "00";
                        goto endCommand;
                    }
                    bytesReceived = mySerial.BytesToRead;
                    Arca.WaitingTime(10);
                } while (bytesReceived < nByteAnswer);  //while (bytesReceived == 0); //} while (bytesReceived < nByteAnswer);
                dataReceived = new byte[bytesReceived];
                mySerial.Read(dataReceived, 0, bytesReceived);
                for (int i = 0; i < dataReceived.Length; i++)
                {
                    commandAnswer += Arca.Dec2Hex(dataReceived[i], 2);
                }
            }
            else
            {
                Arca.WaitingTime(timeout);
            }

            endCommand:
            myTimer.Reset();
            DataReceived(commandAnswer, null);
            return dataReceived;
        }

        public string SendByteArray(byte[] dataToSend, int nByteAnswer = 0, int timeout = 200)
        {
            commandSent = BitConverter.ToString(dataToSend);

            mySerial.DiscardInBuffer();
            commandAnswer = "";
            myTimer.Restart();
            DataSent(commandSent, null);

            mySerial.Write(dataToSend, 0, dataToSend.Length);
            int bytesReceived = 0;
            Arca.WaitingTime(10);
            if (nByteAnswer > 0)
            {
                do
                {
                    if (myTimer.ElapsedMilliseconds > timeout)
                    {
                        commandAnswer = "00";
                        goto endCommand;
                    }
                    bytesReceived = mySerial.BytesToRead;
                    Arca.WaitingTime(10);
                } while (bytesReceived < nByteAnswer);  //while (bytesReceived == 0); //} while (bytesReceived < nByteAnswer);
                byte[] dataReceived = new byte[bytesReceived];
                mySerial.Read(dataReceived, 0, bytesReceived);
                for (int i = 0; i < dataReceived.Length; i++)
                {
                    commandAnswer += Arca.Dec2Hex(dataReceived[i], 2);
                }
            }
            else
            {
                Arca.WaitingTime(timeout);
            }

            endCommand:
            myTimer.Reset();
            DataReceived(commandAnswer, null);
            return commandAnswer;
        }

        public string WaitingData(int nByteAnswer = 1, int timeOut = 5000)
        {
            if (mySerial.IsOpen == false) { mySerial.Open(); }
            commandAnswer = "";
            int bytesReceived = 0;
            myTimer.Restart();
            if (nByteAnswer > 0)
            {
                do
                {
                    if (myTimer.ElapsedMilliseconds > timeOut)
                    {
                        commandAnswer = "NO ANSWER";
                        goto endCommand;
                    }
                    bytesReceived = mySerial.BytesToRead;
                    Arca.WaitingTime(100);
                } while (bytesReceived < nByteAnswer);  //while (bytesReceived == 0); //} while (bytesReceived < nByteAnswer);
                                                        //byte[] dataReceived = new byte[bytesReceived];
                                                        //mySerial.Read(dataReceived, 0, bytesReceived);


                byte[] dataReceived = new byte[0];
                while (mySerial.BytesToRead > 0)
                {
                    Array.Resize(ref dataReceived, dataReceived.Length + mySerial.BytesToRead);
                    byte[] partialDataReceived = new byte[mySerial.BytesToRead];
                    mySerial.Read(partialDataReceived, 0, mySerial.BytesToRead);
                    partialDataReceived.CopyTo(dataReceived, dataReceived.Length - partialDataReceived.Length);
                    Arca.WaitingTime(100);
                }

                for (int i = 0; i < dataReceived.Length; i++)
                {
                    commandAnswer += Arca.Dec2Hex(dataReceived[i], 2);
                }
            }
            else
            {
                Arca.WaitingTime(timeOut);
            }

            endCommand:
            myTimer.Reset();
            DataReceived(commandAnswer, null);
            mySerial.Close();
            return commandAnswer;
        }

        

        public byte[] GetImageBytes(byte[] command, int nByteAnswer = 1, int timeout = 5000)
        {
            

            inizio:
            answerByte = new byte[nByteAnswer];
            mySerial.ReadTimeout = timeout;
            byteReceived = 0;
            commandAnswer = "";
            string sent = Arca.HexArray2HexString(command);
            if (mySerial.IsOpen == false)
                mySerial.Open();
            while (mySerial.BytesToRead != 0)
            {
                Arca.WaitingTime(100);
                mySerial.ReadExisting();
            }
            DataSent(sent, null);
            mySerial.Write(command, 0, command.Count());
            try
            {
                //Arca.WaitingTime(100);
                byteReceived = mySerial.Read(answerByte, 0, nByteAnswer);
                while (byteReceived < nByteAnswer)
                    byteReceived += mySerial.Read(answerByte, byteReceived, nByteAnswer - byteReceived);
            }
            catch (TimeoutException error)
            {
                string msg = error.Message;
                DataReceived("time out", null);
                goto inizio;
            }

            mySerial.Close();
            DataReceived(answerByte.Length, null);
            return answerByte;
        }

        public void WriteSerial(byte[] command)
        {
            mySerial.Write(command, 0, command.Count());
        }
        public byte[] SendCommandByte (byte[] command, int nByteAnswer = 1, int timeout = 5000)
        {
            answerByte = new byte[nByteAnswer];
            mySerial.ReadTimeout = timeout;
            byteReceived = 0;
            commandAnswer = "";
            string sent = command.Length.ToString();
            if (command.Length < 10)
                sent = Arca.HexArray2HexString(command);
            if (mySerial.IsOpen == false)
                mySerial.Open();
            DataSent(sent, null);
            mySerial.Write(command, 0, command.Count());
            try
            {
                byteReceived = mySerial.Read(answerByte, 0, nByteAnswer);
                while (byteReceived < nByteAnswer)
                    byteReceived += mySerial.Read(answerByte, byteReceived, nByteAnswer - byteReceived);
            }
            catch (TimeoutException error)
            {
                string msg = error.Message;
                DataReceived("NO ANSWER", null);
                mySerial.Close();
                return null;
            }
            for (int i = 0; i < answerByte.Length; i++)
            {
                commandAnswer += Arca.Dec2Hex(answerByte[i], 2);
            }

            DataReceived(commandAnswer, null);
            mySerial.Close();
            if (nByteAnswer == 0)
                Arca.WaitingTime(timeout);
            return answerByte;
        }


        public string SendCommand(string command, int nByteAnswer = 1, int timeOut = 5000)
        {
            commandSent = command;
            byteReceived = 0;
            commandAnswer = "";
            int dim = command.Length % 2;
            int index = 0;
            byte[] commandToSend = new byte[command.Length / 2];
            byte[] dataReceived;

            if (dim != 0)
                command = "0" + command;

            for (int i = 1; i < command.Length; i += 2)
            {
                commandToSend[index] = Convert.ToByte(command.Substring(i - 1, 2), 16);
                index += 1;
            }
            
            dataReceived = SendCommandByte(commandToSend, nByteAnswer, timeOut);

            //for (int i = 0; i < dataReceived.Length; i++)
            //{
            //    commandAnswer += Arca.Dec2Hex(dataReceived[i], 2);
            //}

            return commandAnswer;
        }

        public bool ReadSerialNovus(Byte[] cbVectRx, int iOffset, int iNum)
        {
            byte[] cbNibbleRx = new byte[2];
            int iNumeroRx;
            //iNum -= 1;
            try
            {
                if (mySerial.IsOpen)
                {
                    while (iNum > 0) //&& mySerial.BytesToRead > 0
                    {
                        iNumeroRx = mySerial.Read(cbVectRx, iOffset, iNum);
                        iNum -= iNumeroRx;
                        iOffset += iNumeroRx;
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public byte[] GetScanlinePreset(int cis, int source)
        {
            mySerial.ReadTimeout = 1000;
            commandSent = "";
            byte[] command = { 0xD3, 0x06, 0x14, Convert.ToByte(source + cis * 16) };
            for (int i = 0; i < command.Count(); i++)
                commandSent += command[i].ToString("X2");
            if (mySerial.IsOpen == false) { mySerial.Open(); }
            inizio:
            byteReceived = 0;
            while (mySerial.BytesToRead != 0)
                mySerial.ReadExisting();
            DataSent(commandSent, null);
            mySerial.Write(command, 0, command.Length);
            Arca.WaitingTime(100);
            byte[] bufferDimension = new byte[4];
            int numeroRX = mySerial.Read(bufferDimension, 0, 4);
            int dimensioneFile = ((int)bufferDimension[3] << 24) + ((int)bufferDimension[2] << 16) + ((int)bufferDimension[1] << 8) + (bufferDimension[0]);
            byte[] result = new byte[dimensioneFile];
            Arca.WaitingTime(100);
            try
            {
                byteReceived = mySerial.Read(result, 0, dimensioneFile);
                while (byteReceived < dimensioneFile)
                    byteReceived += mySerial.Read(result, byteReceived, dimensioneFile - byteReceived);
            }
            catch (TimeoutException ex)
            {
                string msg = ex.Message;
                DataReceived("time-out", null);
                goto inizio;
            }
            if (byteReceived != dimensioneFile || mySerial.BytesToRead != 0)
            {
                DataReceived("incomplete", null);
                goto inizio;
            }
            
            DataReceived(result.Length + " bytes", null);

            return result;
        }


        public byte[] GetScanlinePresetRil(int cis, int source)
        {
            mySerial.ReadTimeout = 1000;
            commandSent = "";
            byte[] command = { 0xD3, 0x06, 0x1A, Convert.ToByte(source + cis * 16) };
            for (int i = 0; i < command.Count(); i++)
                commandSent += command[i].ToString("X2");
            if (mySerial.IsOpen == false) { mySerial.Open(); }
            inizio:
            byteReceived = 0;
            while (mySerial.BytesToRead != 0)
                mySerial.ReadExisting();
            DataSent(commandSent, null);
            mySerial.Write(command, 0, command.Length);
            Arca.WaitingTime(100);
            byte[] bufferDimension = new byte[4];
            int numeroRX = mySerial.Read(bufferDimension, 0, 4);
            int dimensioneFile = ((int)bufferDimension[3] << 24) + ((int)bufferDimension[2] << 16) + ((int)bufferDimension[1] << 8) + (bufferDimension[0]);
            byte[] result = new byte[dimensioneFile];
            Arca.WaitingTime(100);
            try
            {
                byteReceived = mySerial.Read(result, 0, dimensioneFile);
                while (byteReceived < dimensioneFile)
                    byteReceived += mySerial.Read(result, byteReceived, dimensioneFile - byteReceived);
            }
            catch (TimeoutException ex)
            {
                string msg = ex.Message;
                DataReceived("time-out", null);
                goto inizio;
            }
            if (byteReceived != dimensioneFile || mySerial.BytesToRead != 0)
            {
                DataReceived("incomplete", null);
                goto inizio;
            }

            DataReceived(result.Length + " bytes", null);

            return result;
        }

        public byte[] GetScanlineFileB(byte[] command, int timeout = 200)
        {
            int nRetry = 0;
            mySerial.ReadTimeout = timeout;
            commandSent = "";
            for (int i = 0; i < command.Count(); i++)
                commandSent += command[i].ToString("X2");
            if (mySerial.IsOpen == false) { mySerial.Open(); }
            inizio:
            byteReceived = 0;
            while (mySerial.BytesToRead != 0)
                mySerial.ReadExisting();
            DataSent(commandSent, null);
            mySerial.Write(command, 0, command.Length);
            byte[] bufferDimension = new byte[4];
            int numeroRX = mySerial.Read(bufferDimension, 0, 4);
            int dimensioneFile = ((int)bufferDimension[3] << 24) + ((int)bufferDimension[2] << 16) + ((int)bufferDimension[1] << 8) + (bufferDimension[0]);
            byte[] scanlineBytes = new byte[dimensioneFile];
            try
            {
                byteReceived = mySerial.Read(scanlineBytes, 0, dimensioneFile);
                while (byteReceived < dimensioneFile)
                    byteReceived += mySerial.Read(scanlineBytes, byteReceived, dimensioneFile - byteReceived);
            }
            catch (TimeoutException ex)
            {
                string msg = ex.Message;
                DataReceived("time-out", null);
                nRetry++;
                goto inizio;
            }
            if (byteReceived != dimensioneFile || mySerial.BytesToRead != 0)
            {
                DataReceived("incomplete", null);
                nRetry++;
                goto inizio;
            }
            commandAnswer = Arca.ByteArray2Ascii(scanlineBytes);
            DataReceived(scanlineBytes.Length + " bytes", null);

            return scanlineBytes;
        }

        public string GetScanlineFile(byte[] command, int timeout = 200)
        {
            int nRetry = 0;
            mySerial.ReadTimeout = timeout;
            commandSent = "";
            for (int i = 0; i < command.Count(); i++)
                commandSent += command[i].ToString("X2");
            if (mySerial.IsOpen == false) { mySerial.Open(); }
            inizio:
            byteReceived = 0;
            while (mySerial.BytesToRead != 0)
                mySerial.ReadExisting();
            DataSent(commandSent, null);
            mySerial.Write(command, 0, command.Length);
            Arca.WaitingTime(200);
            byte[] bufferDimension = new byte[4];
            int numeroRX = mySerial.Read(bufferDimension, 0, 4);
            int dimensioneFile = ((int)bufferDimension[3] << 24) + ((int)bufferDimension[2] << 16) + ((int)bufferDimension[1] << 8) + (bufferDimension[0]);
            byte[] scanlineBytes = new byte[dimensioneFile];
            Arca.WaitingTime(100);
            try
            {
                byteReceived = mySerial.Read(scanlineBytes, 0, dimensioneFile);
                while (byteReceived < dimensioneFile)
                    byteReceived += mySerial.Read(scanlineBytes, byteReceived, dimensioneFile - byteReceived);
            }
            catch (TimeoutException ex)
            {
                string msg = ex.Message;
                DataReceived("time-out", null);
                nRetry++;
                goto inizio;
            }
            if (byteReceived != dimensioneFile || mySerial.BytesToRead != 0)
            {
                DataReceived("incomplete", null);
                nRetry++;
                goto inizio;
            }
            answerByte = scanlineBytes;
            commandAnswer = Arca.ByteArray2Ascii(scanlineBytes);
            DataReceived(scanlineBytes.Length + " bytes", null);

            return commandAnswer;
        }

        bool ProvaRead(byte[] cbVectRx, int iOffset, int iNum)
        {
            int iNumeroRx = 0;
            while (iNum > 0)
            {
                iNumeroRx = mySerial.Read(cbVectRx, iOffset, iNum);
                iNum -= iNumeroRx;
                iOffset += iNumeroRx;
            }
            return true;
            


        }

        public string GetJsonFile(byte[] command, int timeout = 200)
        {
            int nRetry = 0;
            int dimensioneFile = 0;
            int numeroRX = 0;
            byte[] bufferDimension = new byte[4];
            mySerial.ReadTimeout = timeout;

            
            //mySerial.ReadBufferSize = 64000;
            commandSent = "";
            for (int i = 0; i < command.Count(); i++)
                commandSent += command[i].ToString("X2");
            if (mySerial.IsOpen == false) { mySerial.Open(); }
            inizio:
            byteReceived = 0;
            while (mySerial.BytesToRead != 0)
            {
                mySerial.ReadExisting();
                Arca.WaitingTime(100);
            }
            DataSent(commandSent, null);
            
            mySerial.Write(command, 0, command.Length);
            
            Arca.WaitingTime(50);
            
            numeroRX = mySerial.Read(bufferDimension, 0, 4);
            dimensioneFile = ((int)bufferDimension[3] << 24) + ((int)bufferDimension[2] << 16) + ((int)bufferDimension[1] << 8) + (bufferDimension[0]);
            byte[] jsonBytes = new byte[dimensioneFile];
            Arca.WaitingTime(10);
            try
            {
                byteReceived = mySerial.Read(jsonBytes, 0, dimensioneFile);
                while (byteReceived < dimensioneFile)
                    byteReceived += mySerial.Read(jsonBytes, byteReceived, dimensioneFile - byteReceived);
            }
            catch (TimeoutException ex)
            {
                string msg = ex.Message;
                DataReceived("time-out", null);
                nRetry++;
                goto inizio;
            }
            if (byteReceived != dimensioneFile || mySerial.BytesToRead != 0)
            {
                DataReceived("incomplete", null);
                nRetry++;
                goto inizio;
            }
            string result = Arca.ByteArray2Ascii(jsonBytes);
            DataReceived(jsonBytes.Length + " bytes", null);

            return result;
        }

        
    }
    public class RL40
    {
        const byte TRANSP_TAPE_ID = 0x01;
        const byte TRANSP_MAG_ID = 0x02;
        const byte TRANSP_PHOTO_ID = 0x03;
        const byte TRANSP_CIS_FRONT_ID = 0x04;
        const byte TRANSP_CIS_REAR_ID = 0x05;
        const byte TRANSP_REMOTE_FRONT_ID = 0x06;
        const byte TRANSP_REMOTE_REAR_ID = 0x07;

        public RS232Novus comPort = new RS232Novus();
        public struct SReaderIniVariables
        {
            public SCisIniVariables CIS;
            public SDoc1IniVariables doc1;
            public SDoc2IniVariables doc2;
            public SDoc3IniVariables doc3;
            public SDoc4IniVariables doc4;
        }
        public SReaderIniVariables iniVariables = new SReaderIniVariables();

        public struct SDoc4IniVariables
        {
            public SIdDoc ID;
            public SDataDoc4 Data;
        }

        public struct SDataDoc4
        {
            public UInt16 width;
            public UInt16 height;
            public UInt16[] rect1_a_x;
            public UInt16 rect1_y;
            public UInt16[] rect1_a_w;
            public UInt16 rect1_h;
            public float rect1_gr;
            public float rect1_rd;
            public float rect1_ir;
            public float rect1_it;
            public float rect1_bl;
            public float rect1_i3;
            public float rect1_uv;
            public float rect1_wh;
            public UInt16[] area1_a_x0;
            public UInt16[] area1_a_x1;
            public UInt16[] area2_a_x0;
            public UInt16[] area2_a_x1;
            public UInt16[] area3_a_x0;
            public UInt16[] area3_a_x1;
            public int start;
            public int end;
            public int value;
        }

        public struct SDoc3IniVariables
        {
            public SIdDoc ID;
            public SDataDoc3 Data;
        }

        public struct SDataDoc3
        {
            public float width;
            public float height;
            public UInt16[] rect1_a_x;
            public UInt16 rect1_y;
            public UInt16[] rect1_a_w;
            public UInt16 rect1_h;
            public float rect1_gr_min;
            public float rect1_gr_max;
            public float rect1_rd_min;
            public float rect1_rd_max;
            public float rect1_ir_min;
            public float rect1_ir_max;
            public float rect1_bl_min;
            public float rect1_bl_max;
            public float rect1_i3_min;
            public float rect1_i3_max;
            public float rect1_uv_min;
            public float rect1_uv_max;
            public float rect1_wh_min;
            public float rect1_wh_max;
            public float rect1_gr_bl_ratio_min;
            public UInt16[] rect2_a_x;
            public UInt16 rect2_y;
            public UInt16[] rect2_a_w;
            public UInt16 rect2_h;
            public float rect2_gr_min;
            public float rect2_gr_max;
            public float rect2_rd_min;
            public float rect2_rd_max;
            public float rect2_ir_min;
            public float rect2_ir_max;
            public float rect2_bl_min;
            public float rect2_bl_max;
            public float rect2_i3_min;
            public float rect2_i3_max;
            public float rect2_uv_min;
            public float rect2_uv_max;
            public float rect2_wh_min;
            public float rect2_wh_max;
            public int start;
            public int end;
            public int value;
        }
        

        public struct SDoc2IniVariables
        {
            public SIdDoc ID;
            public SDataDoc2 Data;
        }

        public struct SDataDoc2
        {
            public float width;
            public float height;
            public UInt16 rect1_y;
            public UInt16 rect1_h;
            public float rect1_rd;
            public float rect1_gr;
            public float rect1_bl;
            public float rect1_ir;
            public float rect1_it;
            public float rect1_i3;
            public float rect1_uv;
            public float rect1_wh;
            public int start;
            public int end;
            public int value;
        }
        public struct SIdDoc
        {
            public string model;
            public int tech_level;
            public string date;
            public string code;
            public int lot;
            public int id_doc;
        }
        public struct SDoc1IniVariables
        {
            public SIdDoc ID;
            public SDataDoc1 Data;
        }
        public struct SDataDoc1
        {
            public float width;
            public float height;
            public float rect1_rd; 
            public float rect1_gr; 
            public float rect1_bl; 
            public float rect1_ir; 
            public float rect1_it; 
            public float rect1_i3; 
            public float rect1_uv;
            public float rect1_wh;
            public int start;
            public int end;
            public int value;
            public int line_start;
            public int line_step;
            public int line_range;
            public int line_num;
            public int[] columns;
            public int line_pos_th;
            public int line_neg_th;
            public int len_min;
            public int len_max;
            public int len_delta_max;
        }
        public struct SCisIniVariables
        {
            public int[] working_a_x0;
            public int[] working_a_x1;
            public int[] left_bound_a_x0;
            public int[] left_bound_a_x1;
            public int[] right_bound_a_x0;
            public int[] right_bound_a_x1;
            public int[] left_bound_deriv_a_x0;
            public int[] left_bound_deriv_a_x1;
            public int[] right_bound_deriv_a_x0;
            public int[] right_bound_deriv_a_x1;
            public int[] internal_a_x0;
            public int[] internal_a_x1;
            public int[] internal2_a_x0;
            public int[] internal2_a_x1;
            public byte bmp_mean_lines;
            public string main_doc_src_front;
            public string main_doc_src_rear;
            public byte main_doc_th_front;
            public byte main_doc_th_rear;
            public UInt16 doc1_code;
            public float main_fact_rd;
            public float main_fact_gr;
            public float main_fact_bl;
            public float main_fact_ir;
            public float main_fact_it;
            public float main_fact_i3;
            public float main_fact_uv;
            public float main_fact_wh;
            public byte main_target_dk;
            public byte main_adj_retry;
            public byte bound_deriv_max;
            public int bound_deriv_error;
            public byte bound_th;
            public UInt16 offset_max;
            public UInt16 doc2_code;
            public string cfx_view_src_front;
            public string cfx_view_src_rear;
            public float cfx_fact_hi_rd;
            public float cfx_fact_hi_gr;
            public float cfx_fact_hi_bl;
            public float cfx_fact_hi_ir;
            public float cfx_fact_hi_it;
            public float cfx_fact_hi_i2;
            public float cfx_fact_hi_i3;
            public float cfx_fact_hi_uv;
            public float cfx_fact_hi_wh;
            public float cfx_m_min;
            public float cfx_m_t_min;
            public float cfx_m_max;
            public int[] cis_seg_a_w;
            public int[] cfx_deriv2_a_max;
            public int[] cfx_deriv2_seg_a_max;
            public float doc2_mean_neg_hi_rd;
            public float doc2_mean_pos_hi_rd;
            public float doc2_mean_neg_hi_gr;
            public float doc2_mean_pos_hi_gr;
            public float doc2_mean_neg_hi_bl;
            public float doc2_mean_pos_hi_bl;
            public float doc2_mean_neg_hi_ir;
            public float doc2_mean_pos_hi_ir;
            public float doc2_mean_neg_hi_it;
            public float doc2_mean_pos_hi_it;
            public float doc2_mean_neg_hi_i3;
            public float doc2_mean_pos_hi_i3;
            public float doc2_mean_neg_hi_uv;
            public float doc2_mean_pos_hi_uv;
            public float doc2_mean_neg_hi_wh;
            public float doc2_mean_pos_hi_wh;
            public float doc2_sigma_max_hi_rd;
            public float doc2_sigma_max_hi_gr;
            public float doc2_sigma_max_hi_bl;
            public float doc2_sigma_max_hi_ir;
            public float doc2_sigma_max_hi_it;
            public float doc2_sigma_max_hi_i3;
            public float doc2_sigma_max_hi_uv;
            public float doc2_sigma_max_hi_wh;
            public byte doc2_bin_l_hi_rd;
            public byte doc2_bin_r_hi_rd;
            public byte doc2_bin_l_hi_gr;
            public byte doc2_bin_r_hi_gr;
            public byte doc2_bin_l_hi_bl;
            public byte doc2_bin_r_hi_bl;
            public byte doc2_bin_l_hi_ir;
            public byte doc2_bin_r_hi_ir;
            public byte doc2_bin_l_hi_it;
            public byte doc2_bin_r_hi_it;
            public byte doc2_bin_l_hi_i3;
            public byte doc2_bin_r_hi_i3;
            public byte doc2_bin_l_hi_uv;
            public byte doc2_bin_r_hi_uv;
            public byte doc2_bin_l_hi_wh;
            public byte doc2_bin_r_hi_wh;
            public float doc2_frac_th_hi_rd;
            public float doc2_frac_th_hi_gr;
            public float doc2_frac_th_hi_bl;
            public float doc2_frac_th_hi_ir;
            public float doc2_frac_th_hi_it;
            public float doc2_frac_th_hi_i3;
            public float doc2_frac_th_hi_uv;
            public float doc2_frac_th_hi_wh;
            public byte bgnd_avg_width;
            public string bgnd_src_front;
            public byte bgnd_min_front;
            public byte bgnd_max_front;
            public string bgnd_src_rear;
            public byte bgnd_min_rear;
            public byte bgnd_max_rear;
            public UInt16 doc3_code;
            public UInt16 doc4_code;
            public byte doc4_passes;
            public int[] doc4_deriv2_a_max;
            public float doc4_fact_rd;
            public float doc4_fact_gr;
            public float doc4_fact_bl;
            public float doc4_fact_ir;
            public float doc4_fact_it;
            public float doc4_fact_i3;
            public float doc4_fact_uv;
            public float doc4_fact_wh;
            public float doc4_mean_neg_rd;
            public float doc4_mean_pos_rd;
            public float doc4_mean_neg_gr;
            public float doc4_mean_pos_gr;
            public float doc4_mean_neg_bl;
            public float doc4_mean_pos_bl;
            public float doc4_mean_neg_ir;
            public float doc4_mean_pos_ir;
            public float doc4_mean_neg_it;
            public float doc4_mean_pos_it;
            public float doc4_mean_neg_i3;
            public float doc4_mean_pos_i3;
            public float doc4_mean_neg_uv;
            public float doc4_mean_pos_uv;
            public float doc4_mean_neg_wh;
            public float doc4_mean_pos_wh;
            public float doc4_sigma_max_rd;
            public float doc4_sigma_max_gr;
            public float doc4_sigma_max_bl;
            public float doc4_sigma_max_ir;
            public float doc4_sigma_max_it;
            public float doc4_sigma_max_i3;
            public float doc4_sigma_max_uv;
            public float doc4_sigma_max_wh;
            public byte doc4_w_neg;
            public byte doc4_w_pos;
            public byte doc4_h_neg;
            public byte doc4_h_pos;
            public byte doc4_w_diff;
            public byte doc4_h_diff;
            public byte doc4_flatness_max;
            public float doc4_comp_gamma_rd;
            public float doc4_comp_gamma_gr;
            public float doc4_comp_gamma_bl;
            public float doc4_comp_gamma_ir;
            public float doc4_comp_gamma_it;
            public float doc4_comp_gamma_i3;
            public float doc4_comp_gamma_uv;
            public float doc4_comp_gamma_wh;
            public byte doc4_comp_passes;
            public float doc4_comp_mean_pos_rd;
            public float doc4_comp_mean_neg_gr;
            public float doc4_comp_mean_pos_gr;
            public float doc4_comp_mean_neg_bl;
            public float doc4_comp_mean_pos_bl;
            public float doc4_comp_mean_neg_ir;
            public float doc4_comp_mean_pos_ir;
            public float doc4_comp_mean_neg_it;
            public float doc4_comp_mean_pos_it;
            public float doc4_comp_mean_neg_i3;
            public float doc4_comp_mean_pos_i3;
            public float doc4_comp_mean_neg_uv;
            public float doc4_comp_mean_pos_uv;
            public float doc4_comp_mean_neg_wh;
            public float doc4_comp_mean_pos_wh;
            public float doc4_comp_sigma_max_rd;
            public float doc4_comp_sigma_max_gr;
            public float doc4_comp_sigma_max_bl;
            public float doc4_comp_sigma_max_ir;
            public float doc4_comp_sigma_max_it;
            public float doc4_comp_sigma_max_i3;
            public float doc4_comp_sigma_max_uv;
            public float doc4_comp_sigma_max_wh;
            public byte doc4_comp_flatness_max;
            public string model;
            public string code;
            public string revision;
            public string code2;
            public string revision2;
            public int id_param_cis;
            public int id_module;
            public int hw_level;
            public string date;
            public string fw_release;
            public string spt_front;
            public string spt_rear;
        }

        public struct SSptStream
        {
            // stream
            public JsonSptStreamRoot stream;
            public int[,] srcId;
            // streamAdjust
            public JsonSptStreamRoot adjust;
            public int[,] srcAdjId;
            // baseReader
            public JsonSptBaseRoot baseReader;
            // cis - front
            // cis - rear
            public JsonSptCisRoot[] CIS;
            // Mis
            // rilettura - stream
            // rilettura - streamAdjust
            // rilettura - baseReader
            // rilettura - cis - front
            // rilettura - cis - rear
            public SSptRilettura rilettura;
        }
        public SSptStream SPT = new SSptStream();

        public struct SSptRilettura
        {
            // stream
            public JsonSptStreamRoot stream;
            // streamAdjust
            public JsonSptStreamRoot adjust;
            // baseReader
            public JsonSptBaseRoot baseReader;
            // cis - front
            // cis - rear
            public JsonSptCisRoot[] CIS;
            // Mis
        }


        public struct SBoardID
        {
            public string Factory;
            public string Code;
            public string Revision;
            public string Lot;
            public string Serial;
        }

        public struct STableParamSamplingConfig
        {
            public int ScanMode;
            public int LedOn;
            public int DarkComp;
            public int CompCis;
            public int StrobeExt;
            public int CompCoeffK;
            public SSensorPresetMask SensorEnablePresetMask;
            public int EncExt;
            public int FactorMoltUvEna;
            public byte[] reserved;
        }

        public struct SSensorPresetMask
        {
            public int CisF;
            public int CisR;
            public int Tape;
            public int Mag;
        }
        public struct JsonSptBaseRoot
        {
            public SptBase sptBase { get; set; }
            public SptReaderConfig sptReaderConfig { get; set; }
        }
        public struct SptBase
        {
            public string readerFlag { get; set; }
            public string cModuleName { get; set; }
            public string cSerialNum { get; set; }
            public string cBoardId { get; set; }
        }

        public struct SptReaderConfig
        {
            public int spiSpeed { get; set; }
            public int multiBufferMode { get; set; }
            public int numOfActiveBank { get; set; }
            public string wModuleCtrlReg { get; set; }
            public int maxSkew { get; set; }
            public int cisFrontScannerLatency { get; set; }
            public int cisRearScannerLatency { get; set; }
            public int magScannerLatency { get; set; }
            public int tapeScannerLatency { get; set; }
            public int cisRilFrontScannerLatency { get; set; }
            public int cisRilRearScannerLatency { get; set; }
            public int readerSpeedMmSec { get; set; }
            public int periodSynkGiro { get; set; }
            public int encoderCpr { get; set; }
            public int distanceBnOverrunMs { get; set; }
            public int distanceBnTooLongMs { get; set; }
            public string srcFront { get; set; }
            public string srcRear { get; set; }
            public int frontEdgeTh { get; set; }
            public int rearEdgeTh { get; set; }
            public int presenceFrontEdgeTh { get; set; }
            public int presenceRearEdgeTh { get; set; }
            public int ghostTarmCls { get; set; }
            public int triggerBnInput { get; set; }
            public int triggerBnStazioneRil { get; set; }
            public int transportSpeedMax { get; set; }
            public string srcFrontRil { get; set; }
            public string srcRearRil { get; set; }
            public int frontEdgeThRil { get; set; }
            public int rearEdgeThRil { get; set; }
            public int flagSptGen { get; set; }
            public string bitFlagsSensorPreset { get; set; }
            public List<int> configurationBank { get; set; }
            public List<int> switchFitnessFile { get; set; }
            public List<string> denominationSwitchMultibank { get; set; }
            public List<string> algoEnable { get; set; }
        }
        public struct JsonSptStreamRoot
        {
            public List<JsonSptStream> sptStream { get; set; }
        }

        public struct JsonSptStream
        {
            public int sensorId { get; set; }
            public int streamId { get; set; }
            public string typeSensor { get; set; }
            public string colorType { get; set; }
            public string orientation { get; set; }
            public int pixelImageW { get; set; }
            public int pixelImageH { get; set; }
            public float resolutionX { get; set; }
            public float resolutionY { get; set; }
            public int dataPrecision { get; set; }
            public int sensorPositionX { get; set; }
            public int sensorPositionY { get; set; }
            public int sensorSide { get; set; }
            public int sensorOperation { get; set; }
            public int elementSizeX { get; set; }
            public int elementSizeY { get; set; }
            public int spotSpacing { get; set; }
            public float sensorPitch { get; set; }
            public int geometricCorrectionX { get; set; }
            public int geometricCorrectionY { get; set; }
            public int referenceOptySensor { get; set; }
            public int hwLevel { get; set; }
        }


        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class JsonSptCisRoot
        {
            public SptCis sptCis { get; set; }
            public SptAfe sptAfe { get; set; }
        }

        public class SptAfe
        {
            [JsonProperty(PropertyName = "adr-data")] //DA SISTEMARE
            public List<List<string>> AdrData { get; set; }
        }

        public class SptCis
        {
            public int srcNum { get; set; }
            public int cisWindowsSize { get; set; }
            public int numMmAdj { get; set; }
            public List<int> numPixelScanline { get; set; }
            public List<int> numPixelSegment { get; set; }
            public List<int> pixelStart { get; set; }
            public List<int> pixelEnd { get; set; }
            public List<int> sequencySampling { get; set; }
            public List<int> sequencyAdjust { get; set; }
            public List<int> resolutionOptical { get; set; }
            public List<int> highCurrentFlag { get; set; }
            public int dummyLeftPixels { get; set; }
            public int shLatency { get; set; }
            public int modeCntConfig { get; set; }
            public bool mirrorScanline { get; set; }
            public int ledOnPixel { get; set; }
            public int pixelClkPeriod { get; set; }
            public List<int> tacc1PixelStart { get; set; }
            public List<int> tacc1PixelEnd { get; set; }
            public List<int> tacc2PixelStart { get; set; }
            public List<int> tacc2PixelEnd { get; set; }
            public bool tacc1PresenceRefl { get; set; }
            public bool tacc1PresenceTrasp { get; set; }
            public bool tacc2PresenceRefl { get; set; }
            public bool tacc2PresenceTrasp { get; set; }
            public bool transpMaster { get; set; }
            public bool transpSlave { get; set; }
            public int signalClamp { get; set; }
            public float presetMultPwmUv { get; set; }
            public float presetStartPwmPercWhite { get; set; }
            public int darkPixelStart { get; set; }
            public int darkPixelEnd { get; set; }
            public int offsetTarget { get; set; }
            public int offsetTolerance { get; set; }
            public List<UInt16> offsetNativeSegment { get; set; }
            public float mainTolerance { get; set; }
            public float extendedTolerance { get; set; }
            public int pwmMaxPercentAdj { get; set; }
            public List<int> targetAdjust { get; set; }
            public List<int> currentMax { get; set; }
            public List<UInt16> currentNativeSrc { get; set; }
            public List<UInt16> pwmNativeSrc { get; set; }
            public List<int> secondCurrentNativeSrc { get; set; }
            public List<int> secondPwmNativeSrc { get; set; }
            public List<int> PixelStartSecondAdj { get; set; }
            public List<int> PixelEndSecondAdj { get; set; }
            public int uvMirroringFlag { get; set; }
            public int uvMirroringSrc { get; set; }
            public int boundMax { get; set; }
            public int boundRef { get; set; }
            public int presetStartPwmPerc { get; set; }
            public int presetMaxPwmPerc { get; set; }
            public float presetToleranceBound { get; set; }
            public float presetMainTolerance { get; set; }
            public float presetExtendedTolerance { get; set; }
            public List<int> afePga { get; set; }
            public List<float> coeff_k { get; set; }
            public List<List<float>> tacc1Coeff { get; set; }
            public List<List<float>> tacc2Coeff { get; set; }
            public string serialDigital { get; set; }
            public string serialCIS { get; set; }
            public int hwLevel { get; set; }
            public int sensorType { get; set; }
        }


        public struct SReader
        {
            public string code;
            public string revision;
            public string iniFileName;
            public string xmlFileName;
            public string model;
            public string iniModel;
            public string serialNumber;
            public string completeSerialNumber;
            public string id_reader;
            public string id_module;
            public string hw_level;
            public string date;
            public string id_param_set;
            public string doct_UV_iniFile;
            public string doct_US_iniFile;
            public string doct_Mag_iniFile;
            public string doct_CIS_iniFile;
            public string serialPort;
            public string usbPort;
            public string boardID;
            public string linux_OS_version;
            public string fw_suite;
            public string fpga;
            public string cis_fw_release;
            public string us_fw_release;
            public string fw_set;
            public string ref0;
            public string ref1;
            public string ref2;
            public string ref3;
            public int[] ch_ena0;
            public int[] ch_ena1;
            public int[] ch_ena2;
            public int[] ch_ena3;
            public string[,] sourcePresence;
            public string[,] sourceAdjPresence;
            public int sptBmpTapeId;
            public int sptBmpMisId;
            public string readerType;
            public string[] reReadingCode;
            public SRilettura reReading;

        }
        public SReader rl40 = new SReader();
        
        public struct SRilettura
        {
            public string type;
            public int code;
            public string monthProductionDate;
            public string yearProductionDate;
            public int serial;
        }

        public struct SRL40Test
        {
            public string result;
            public string user;
            public string startDate;
            public string startTime;
            public string endDate;
            public string endTime;
            public string actualTest;
            public string station;
        }
        public SRL40Test rl40Test = new SRL40Test();


        public string CmdPortInterfaceTest()
        {
            string answer = "";
            try
            {
                byte[] command = { 0xD3, 0X81 };
                comPort.OpenCom();
                comPort.SendCommandByte(command, 5, 200);
                answer =  Arca.Hex2Ascii(comPort.commandAnswer);
                if (answer == "COM--")
                    rl40.serialPort = comPort.serialPortName;
                if (answer == "USB--")
                    rl40.usbPort = comPort.serialPortName;
                comPort.CloseCom();
            }
            catch { }
            return answer;
        }

        public string CmdCurrentMaxPwmMaxSet(int Cis = 0)
        {
            byte[] command = {0xD3, 0x33, Convert.ToByte(Cis) };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 200);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdParamCisSet(int address)
        {
            int byteAnswer = 16;
            switch (address)
            {
                case 0x00:
                    byteAnswer = 16;
                    break;
                case 0x01:
                    byteAnswer = 16;
                    break;
                case 0x02:
                    byteAnswer = 32;
                    break;
                case 0x03:
                    byteAnswer = 32;
                    break;
                case 0x04:
                    byteAnswer = 16;
                    break;
                case 0x05:
                    byteAnswer = 16;
                    break;
                case 0x06:
                    byteAnswer = 6;
                    break;
                case 0x07:
                    byteAnswer = 6;
                    break;
                case 0x10:
                    byteAnswer = 16;
                    break;
                case 0x11:
                    byteAnswer = 16;
                    break;
                case 0x12:
                    byteAnswer = 32;
                    break;
                case 0x13:
                    byteAnswer = 32;
                    break;
                case 0x14:
                    byteAnswer = 16;
                    break;
                case 0x15:
                    byteAnswer = 16;
                    break;
                case 0x16:
                    byteAnswer = 6;
                    break;
                case 0x17:
                    byteAnswer = 6;
                    break;
            }
            byte[] command = { 0xD3, 0x31, Convert.ToByte(address) };
            comPort.OpenCom();
            comPort.SendCommandByte(command, byteAnswer, 200);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        /// <summary>
        /// Comando per leggere le scanline delle tarature primarie (adjust)
        /// </summary>
        /// <param name="index">0x00-0x0F = CisFront / 0x10-0x1F = CisRear</param>
        /// <returns></returns>
        public string CmdScanlineAdjustUpload(string index)
        {
            byte[] command = { 0xD3, 0x06, 0x12, Convert.ToByte(index) };
            comPort.OpenCom();
            comPort.GetScanlineFile(command, 1000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public byte[] CmdScanlineAdjustUploadBytes(string index)
        {
            byte[] command = { 0xD3, 0x06, 0x12, Convert.ToByte(index) };
            comPort.OpenCom();
            comPort.GetScanlineFile(command, 1000);
            comPort.CloseCom();
            return comPort.answerByte;
        }


        public byte[] CmdScanlineAdjustRilUploadBytes(string index)
        {
            byte[] command = { 0xD3, 0x06, 0x18, Convert.ToByte(index) };
            comPort.OpenCom();
            comPort.GetScanlineFile(command, 1000);
            comPort.CloseCom();
            return comPort.answerByte;
        }

        public string CmdScanlineCoeffAdjRilUpload(string index)
        {
            byte[] command = { 0xD3, 0x06, 0x19, Convert.ToByte(index) };
            comPort.OpenCom();
            comPort.GetScanlineFile(command, 1000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdScanlineCoeffAdjUpload(string index)
        {
            byte[] command = { 0xD3, 0x06, 0x13, Convert.ToByte(index) };
            comPort.OpenCom();
            comPort.GetScanlineFile(command, 1000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public enum CisAdjust
        {
            Dark,
            OptiAdjust,
            CalcCoeffTacc,
            PresetInAdjust
        }

        /// <summary>
        /// Comando di taratura dei CIS
        /// </summary>
        /// <param name="index">0=Dark - 1=OptiAdjust - 2=CalcoloCoeffTaccoletti - 3=PresetInAdjust</param>
        /// <returns></returns>
        public string CmdCisAdjus(int index)
        {
            byte[] command = { 0xD3, 0x30, Convert.ToByte(index) };
            int timeOut = 4000;
            if (index == 1)
                timeOut = 20000;
            if (index == 3)
                timeOut = 5000;
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, timeOut);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdCisAdjusReaderStation(int index)
        {
            byte[] command = { 0xD3, 0x30, Convert.ToByte(index) };
            int timeOut = 3000;
            if (index == 0x11)
                timeOut = 20000;
            if (index == 0x13)
                timeOut = 5000;
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, timeOut);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public void CmdDownloadCoefCompCis(int cis, byte[] fileCfx = null)
        {
            byte[] comando = { 0xD3, 0x07, 0x15 };
            byte[] dimensione = { 0x00, 0x00, 0x01, 0x00 };
            if (cis == 1)
                comando[2] = 0x16;
            int timeOut = 3000;
            
            if (fileCfx == null) // coefficienti standard 10 04 01 00
            {
                fileCfx = new byte[65536];
                for (int i = 0; i < fileCfx.Length; i += 4)
                {
                    fileCfx[i] = 0x10;
                    fileCfx[i + 1] = 0x04;
                    fileCfx[i + 2] = 0x01;
                    fileCfx[i + 3] = 0x00;
                }
            }
            comPort.OpenCom();
            comPort.WriteSerial(comando);
            Arca.WaitingTime(100);
            comPort.WriteSerial(dimensione);
            Arca.WaitingTime(100);
            comPort.WriteSerial(fileCfx);
            comPort.CloseCom();
            Arca.WaitingTime(timeOut);
            
        }

        public void CmdDownloadCoefCompCisRil(int cis, byte[] fileCfx = null)
        {
            byte[] comando = { 0xD3, 0x07, 0x45 };
            byte[] dimensione = { 0x00, 0x00, 0x01, 0x00 };
            if (cis == 1)
                comando[2] = 0x46;
            int timeOut = 3000;

            if (fileCfx == null) // coefficienti standard 10 04 01 00
            {
                fileCfx = new byte[65536];
                for (int i = 0; i < fileCfx.Length; i += 4)
                {
                    fileCfx[i] = 0x10;
                    fileCfx[i + 1] = 0x04;
                    fileCfx[i + 2] = 0x01;
                    fileCfx[i + 3] = 0x00;
                }
            }
            comPort.OpenCom();
            comPort.WriteSerial(comando);
            Arca.WaitingTime(100);
            comPort.WriteSerial(dimensione);
            Arca.WaitingTime(100);
            comPort.WriteSerial(fileCfx);
            comPort.CloseCom();
            Arca.WaitingTime(timeOut);

        }

        public string CmdCisAdjustDwlCisFrontCoeff()
        {
            byte[] command = { 0xD3, 0x07, 0x15 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 3000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdCisAdjustDwlCisRearCoeff()
        {
            byte[] command = { 0xD3, 0x07, 0x16 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 3000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdCisAdjustUplCisFrontCoeff()
        {
            byte[] command = { 0xD3, 0x06, 0x15 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 3000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }


        public string CmdCisAdjustUplCisRearCoeff()
        {
            byte[] command = { 0xD3, 0x06, 0x16 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 3000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        
        public RL40.STableParamSamplingConfig CmdGetTableParamSamplingConfig()
        {
            byte[] answer;
            byte[] command = { 0xD3, 0x32, 0x00 };
            RL40.STableParamSamplingConfig myTable = new STableParamSamplingConfig();
            comPort.OpenCom();
            answer = comPort.SendCommandByte(command, 16);
            comPort.CloseCom();
            if (answer[0]== 1)
                myTable.ScanMode = 1;
            if (answer[1]== 1)
                myTable.LedOn = 1;
            if (answer[2] == 1)
                myTable.DarkComp = 1;
            if (answer[3] == 1)
                myTable.CompCis = 1;
            if (answer[4] == 1)
                myTable.StrobeExt = 1;
            if (answer[5] == 1)
                myTable.CompCoeffK = 1;
            string bitMask = Arca.Byte2Bin(answer[6]);
            if (bitMask.Substring(7, 1) == "1")
                myTable.SensorEnablePresetMask.CisF = 1;
            if (bitMask.Substring(6, 1) == "1")
                myTable.SensorEnablePresetMask.CisR = 1;
            if (bitMask.Substring(5, 1) == "1")
                myTable.SensorEnablePresetMask.Tape = 1;
            if (bitMask.Substring(4, 1) == "1")
                myTable.SensorEnablePresetMask.Mag = 1;
            if (answer[7] == 1)
                myTable.EncExt = 1;
            if (answer[8] == 1)
                myTable.FactorMoltUvEna = 1;
            myTable.reserved = new byte[8];
            for (int i = 0; i < 8; i++)
                myTable.reserved[i] = answer[i];
            return myTable;
        }

        public string CmdSetTableParamSamplingConfig( RL40.STableParamSamplingConfig actualConfig, RL40.STableParamSamplingConfig newConfig)
        {
            byte[] command = { 0xD3, 0x32, 0x01 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 2000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdSetTableParamSamplingConfig(RL40.STableParamSamplingConfig tableConfig)
        {
            string answer;
            string tableParam = "";
            tableParam += tableConfig.ScanMode.ToString("00");
            tableParam += tableConfig.LedOn.ToString("00");
            tableParam += tableConfig.DarkComp.ToString("00");
            tableParam += tableConfig.CompCis.ToString("00");
            tableParam += tableConfig.StrobeExt.ToString("00");
            tableParam += tableConfig.CompCoeffK.ToString("00");
            string bitMask = Arca.Bin2Hex("0000" + tableConfig.SensorEnablePresetMask.CisF.ToString() + tableConfig.SensorEnablePresetMask.CisR.ToString() + tableConfig.SensorEnablePresetMask.Tape.ToString() + tableConfig.SensorEnablePresetMask.Mag.ToString(),2);
            tableParam += bitMask;

            tableParam += tableConfig.EncExt.ToString("00");
            tableParam += tableConfig.FactorMoltUvEna.ToString("00");
            tableParam += "00000000000000";

            comPort.OpenCom();
            answer = comPort.SendCommand("D33201"+tableParam, 1, 2000);
            comPort.CloseCom();
            return answer;
        }

        public string CmdChannelEnable(int bank, int channel)
        {
            byte[] command = { 0xD3, 0x01, Convert.ToByte(bank), Convert.ToByte(channel) };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 2000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }
        public string CmdStatusReader()
        {
            byte[] command = { 0xF3 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 100);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdStatusVal()
        {
            byte[] command = { 0xD3, 0x08, 0x01 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 100);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdStatusTape()
        {
            byte[] command = { 0xD3, 0x22, 0x01, 0x01, 0x01, 0x54 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 3000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdStatusMag()
        {
            byte[] command = { 0xD3, 0x22, 0x02, 0x01, 0x01, 0x54 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 3000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }
        public string CmdStatusCIS(int cis)
        {
            byte[] command = { 0xD3, 0x22, 0x04, 0x01, 0x01, 0x54 };
            comPort.OpenCom();
            if (cis == 0) //FRONT
                comPort.SendCommandByte(command, 1, 3000);
            else
            {
                command[2] = 0x05;
                comPort.SendCommandByte(command, 1, 3000);
            }
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdStatusCISRil(int cis)
        {
            byte[] command = { 0xD3, 0x22, 0x06, 0x01, 0x01, 0x54 };
            comPort.OpenCom();
            if (cis == 0) //FRONT
                comPort.SendCommandByte(command, 1, 3000);
            else
            {
                command[2] = 0x07;
                comPort.SendCommandByte(command, 1, 3000);
            }
            comPort.CloseCom();
            return comPort.commandAnswer;
        }


        public string CmdSetSideCisRil(byte setResetSide)
        {
            byte[] command = { 0xD3, 0x27, setResetSide };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 5000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdCisRilCheck()
        {
            byte[] command = { 0xD3, 0x25 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 2, 100);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdUploadDimBn(byte bnIndex = 0x00)
        {
            byte[] command = { 0xD3, 0x06, 0x06, 0x00};
            command[3] = bnIndex;
            comPort.OpenCom();
            comPort.SendCommandByte(command, 5, 15000); //timeout=100 modificato 06.09.23
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdUploadShortStatistic()
        {
            byte[] command = { 0xD3, 0x06, 0x0A};
            comPort.OpenCom();
            comPort.SendCommandByte(command, 80, 100);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdUploadCompCis(int cis, byte[] CfxArray )
        {
            string answer = "";
            byte[] command = new byte[65542];
            command[0] = 0xD3;
            command[1] = 0x07;
            command[2] = 0x15; // front
            if (cis == 1) 
                command[2] = 0x16;// rear
            command[3] = 0x00;
            command[4] = 0x00;
            command[5] = 0x01;
            command[6] = 0x00;
            Array.Copy(CfxArray, 0, command, 7, 65535);
            comPort.OpenCom();
            comPort.SendCommandByte(command, 0, 3000);
            byte[] status = { 0xF3 };
            comPort.SendCommandByte(status, 1, 200);
            comPort.CloseCom();
            answer = comPort.commandAnswer;
            return answer;
        }

        public string CmdDownloadBytes(byte[] data, int nByteAnswer = 1, int timeout = 2000)
        {
            comPort.OpenCom();
            comPort.SendByteArray(data, nByteAnswer, timeout);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdCdfBankDwlGet()
        {
            byte[] command = { 0xD3, 0x70, 0x09 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 100);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }
        public string CmdCdfBankDwlSet(int bank)
        {
            byte[] command = { 0xD3, 0x70, 0x08, Convert.ToByte(bank) };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 200);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdDimBnUpload(int index)
        {
            byte[] command = { 0xD3, 0x06, 0x06, Convert.ToByte(index) };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 5, 200);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdBoardId()
        {
            byte[] command = { 0xE3, 0x0A };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 32, 100);
            comPort.CloseCom();
            return Arca.Hex2Ascii(comPort.commandAnswer.Substring(0,32));
        }

        public string CmdVersionLinux()
        {
            byte[] command = { 0xE3, 0x07 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 32, 100);
            comPort.CloseCom();
            return Arca.Hex2Ascii(comPort.commandAnswer.Substring(0, 50).Trim((char)0x00));
        }

        public byte[]CmdScanlinePresetUpload(int cis, int src)
        {
            return comPort.GetScanlinePreset(cis, src);
        }

        public byte[] CmdScanlinePresetRilUpload(int cis, int src)
        {
            return comPort.GetScanlinePresetRil(cis, src);
        }
        public byte[] CmdPresetParamCisGet()
        {
            byte[] command = { 0xD3, 0x31, 0x0A };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 16, 200);
            comPort.CloseCom();
            return comPort.answerByte;
        }

        public string CmdCloseSession()
        {
            string result = "";
            byte[] comando = { 0xD0 };
            comPort.OpenCom();
            result = comPort.SendByteArray(comando, 1, 3000);
            comando[0] = 0xF3;
            comPort.SendByteArray(comando, 1, 100);
            //comPort.SendCommand("D0", 0, 100);
            //comPort.SendCommand("F3", 1, 1000);
            comPort.CloseCom();
            return Arca.Hex2Ascii(comPort.commandAnswer);
        }

        public string CmdSuiteFw()
        {
            byte[] command = { 0xE3, 0x0B };
            comPort.OpenCom();
            comPort.SendCommandByte(command,16,100);
            comPort.CloseCom();
            return Arca.ByteArray2Ascii(comPort.answerByte);
        }

        public string CmdVersionFPGA()
        {
            byte[] command = { 0xE3, 0x03 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 16, 100);
            comPort.CloseCom();
            return Arca.ByteArray2Ascii(comPort.answerByte);
        }

        public string CmdVersionCMB()
        {
            byte[] command = { 0xE3, 0x06 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 16, 100);
            comPort.CloseCom();
            return Arca.ByteArray2Ascii(comPort.answerByte);
        }

        public string CmdVersionTape()
        {
            byte[] command = { 0xE3, 0x04 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 16, 100);
            comPort.CloseCom();
            return Arca.Hex2Ascii(comPort.commandAnswer);
        }

        public void CmdSptStreamAdjustRilDwl(string filename)
        {
            byte[] command = { 0xD3, 0x07, 0x42 };
            byte[] length = new byte[4];
            byte[] fileBytes = File.ReadAllBytes(filename);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(250);
            comPort.SendByteArray(length);
            Arca.WaitingTime(250);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }

        public void CmdSptStreamAdjustDwl(string filename)
        {
            byte[] command = { 0xD3, 0x07, 0x11 };
            byte[] length = new byte[4];
            byte[] fileBytes = File.ReadAllBytes(filename);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(250);
            comPort.SendByteArray(length);
            Arca.WaitingTime(250);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }

        public void CmdSptBaseReaderDwl(string filename)
        {
            
            byte[] command = { 0xD3, 0x07, 0x02 };
            byte[] length = new byte[4];
            byte[] fileBytes = File.ReadAllBytes(filename);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(250);
            comPort.SendByteArray(length);
            Arca.WaitingTime(250);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }

        public void CmdSptCisDwl(int side, RL40.JsonSptCisRoot jSpt)
        {
            byte[] command = { 0xD3, 0x07, 0x03 };
            if (side == 1) //REAR
                command[2] = 0x04;

            byte[] length = new byte[4];
            string myJson = JsonConvert.SerializeObject(jSpt,Newtonsoft.Json.Formatting.Indented );
            byte[] fileBytes = Encoding.ASCII.GetBytes(myJson);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(250);
            comPort.SendByteArray(length);
            Arca.WaitingTime(250);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }

        public void CmdSptCisDwl(int side, string filename)
        {
            byte[] command = { 0xD3, 0x07, 0x03 };
            if (side == 1) //REAR
                command[2] = 0x04;

            byte[] length = new byte[4];
            byte[] fileBytes = File.ReadAllBytes(filename);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(250);
            comPort.SendByteArray(length);
            Arca.WaitingTime(250);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }
        public void CmdSptCisRilDwl(int side, RL40.JsonSptCisRoot jSpt)
        {
            byte[] command = { 0xD3, 0x07, 0x43 };
            if (side == 1) //REAR
                command[2] = 0x44;

            byte[] length = new byte[4];
            string myJson = JsonConvert.SerializeObject(jSpt, Newtonsoft.Json.Formatting.Indented);
            byte[] fileBytes = Encoding.ASCII.GetBytes(myJson);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(250);
            comPort.SendByteArray(length);
            Arca.WaitingTime(250);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }

        public void CmdSptCisRilDwl(int side, string filename)
        {
            byte[] command = { 0xD3, 0x07, 0x43 };
            if (side == 1) //REAR
                command[2] = 0x44;

            byte[] length = new byte[4];
            byte[] fileBytes = File.ReadAllBytes(filename);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(250);
            comPort.SendByteArray(length);
            Arca.WaitingTime(250);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }

        public void CmdSptMagDwl(string filename)
        {
            byte[] command = new byte[260];
            command[0] = 0xD3;
            command[1] = 0x20;
            command[2] = 0x21;
            command[3] = 0x4E;
            byte[] fileBytes = File.ReadAllBytes(filename);

            Array.Copy(fileBytes, 0, command, 4, 256);
            comPort.OpenCom();
            comPort.SendByteArray(command, 1, 200);
            comPort.CloseCom();
        }

        public void CmdSptStreamDwl(string filename)
        {
            byte[] command = { 0xD3, 0x07, 0x01 };
            byte[] length = new byte[4];
            byte[] fileBytes = File.ReadAllBytes(filename);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(100);
            comPort.SendByteArray(length);
            Arca.WaitingTime(100);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }

        public void CmdSptStreamRilDwl(string filename)
        {
            byte[] command = { 0xD3, 0x07, 0x41 };
            byte[] length = new byte[4];
            byte[] fileBytes = File.ReadAllBytes(filename);
            int lenFile = fileBytes.Length;
            length[0] = (byte)(lenFile & 0xFF);
            length[1] = (byte)((lenFile >> 8) & 0xFF);
            length[2] = (byte)((lenFile >> 16) & 0xFF);
            length[3] = (byte)((lenFile >> 24) & 0xFF);

            comPort.OpenCom();
            comPort.SendByteArray(command);
            Arca.WaitingTime(100);
            comPort.SendByteArray(length);
            Arca.WaitingTime(100);
            comPort.SendByteArray(fileBytes);
            comPort.CloseCom();
        }

        public string ParamReaderInit(int parameter, string value)
        {
            // 1 = Serial number
            // 2 = Board ID
            // 3 = Module name
            byte[] values = Arca.Hex2ByteArray(Arca.Ascii2Hex(value));
            int dimension = value.Length + 3;
            byte[] command = new byte[value.Length + 3];
            command[0] = 0xD3;
            command[1] = 0x10;
            command[2] = Convert.ToByte(parameter);
            for (int i = 3; i < value.Length + 3; i++)
                command[i] = values[i - 3];
            
            comPort.OpenCom();
            byte[] results = comPort.SendCommandByte(command, 1, 2000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public void CmdReboot()
        {
            byte[] command = { 0xD3, 0x7D };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 0, 1000);
            byte[] commandStatus = { 0xF3 };
            for (int i = 0; i < 50; i++)
            {
                comPort.SendCommandByte(commandStatus, 1, 100);
                if (comPort.answerByte[0] == 0x00)
                    Arca.WaitingTime(100);
                else
                    break;
            }
            comPort.CloseCom();
            if (comPort.answerByte[0] == 0x00)
                System.Windows.Forms.MessageBox.Show("Nessuna risposta dal lettore", "ATTENZIONE", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public void OpenCom()
        {
            comPort.OpenCom();
        }

        public void CloseCom()
        {
            comPort.CloseCom();
        }

        public string SendCommand(string command, int nByteAnswer, int timeOut)
        {
            return comPort.SendCommand(command, nByteAnswer, timeOut);
        }

        public string CmdPreset(int type = 0)
        {
            byte[] command = { 0x83, Convert.ToByte(type) };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 5000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdPresetReaderStation()
        {
            byte[] command = { 0x84 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 1, 5000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public byte[] CmdInfoBn()
        {
            byte[] command = { 0xA3 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 20, 1000);
            comPort.CloseCom();
            return comPort.answerByte;
        }

        public byte[] CmdInfoLastDepositUpload()
        {
            byte[] command = { 0xD3, 0x06, 0x17 };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 4000, 1000);
            comPort.CloseCom();
            return comPort.answerByte;
        }

        public string CmdCisSimmetryTest(int cis)
        {
            byte[] command = { 0xD3, 0x38, Convert.ToByte(cis) };
            comPort.OpenCom();
            comPort.SendCommandByte(command, 6, 1000);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdSptStreamAdjustUpload()
        {
            byte[] command = { 0xD3, 0x06, 0x11 };
            string result = "";
            comPort.OpenCom();
            result = comPort.GetJsonFile(command, 200);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptStreamAdjustRilUpload()
        {
            byte[] command = { 0xD3, 0x06, 0x42 };
            string result = "";
            comPort.OpenCom();
            result = comPort.GetJsonFile(command, 200);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptStreamUpload()
        {
            byte[] command = { 0xD3, 0x06, 0x01 };
            string result = "";
            comPort.OpenCom();
            result = comPort.GetJsonFile(command, 200);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptStreamRilUpload()
        {
            byte[] command = { 0xD3, 0x06, 0x41 };
            string result = "";
            comPort.OpenCom();
            result = comPort.GetJsonFile(command, 200);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptBaseStreamUpload()
        {
            byte[] command = { 0xD3, 0x06, 0x02 };
            string result = "";
            comPort.OpenCom();
            result = comPort.GetJsonFile(command, 200);
            comPort.CloseCom();
            return result;
        }

        public struct SSampling
        {
            public int bnNum;
            public string bnType;
            public int currBank;
            public string denomination;
            public string way;
            public SSamplingFlags flags;
            public int bnHigh;
            public int bnWidth;
            public int bnOffset;
            public int bnSlope;
            public int distance;
            public SSamplingExtra extra;
        }

        public struct SSamplingFlags
        {
            public bool rejOverrun;
            public bool rejdim;
            public bool rejfloat;
            public bool rejskew;
            public bool rejuv;
            public bool rejmag;
            public bool rejir;
            public bool rejcls;
            public bool rejGenericError;
            public bool rejAbort;
            public bool rejLocaliz;
            public bool rejOther;
            public bool rejDenomDisable;
            public bool rejTooLong;
            public bool rejExtremeUnfit;
            public bool unfitGraff;
            public bool unfitContrast;
            public bool unfitFormat;
            public bool unfitCorner;
            public bool unfitUvDeinked;
            public bool unfitRape;
            public bool unfitGrid;
            public bool unfitsoil;
            public bool unfitDeysBorder;
            public bool unfitStain;
            public bool unfitClosedTears;
            public bool unfitOther;

        }
        public struct SSamplingExtra
        {
            public bool presOlSup;
            public bool presOlInf;
        }
        public SSampling CmdSampling()
        {
            // ATTENZIONE - RISPOSTA DA COMPLETARE
            int contatore = 0;
            string result;
            byte[] comando = { 0xD3, 0x0D };
            comPort.OpenCom();
            result = comPort.SendByteArray(comando, 20, 2000);
            comPort.CloseCom();
            SSampling temp = new SSampling();
            while (result.Length != 40)
            {
                contatore++;
                if (contatore > 5)
                    return temp;
            }
            temp.bnNum = Arca.Hex2Dec(result.Substring(2, 2) + result.Substring(0, 2));
            string giudizio = Arca.Hex2Bin(result.Substring(4, 2));
            int type = Arca.Bin2Dec(giudizio.Substring(0, 3));
            switch (type)
            {
                case 1:
                    temp.bnType = "REJECT";
                    break;
                case 2:
                    temp.bnType = "SUSPECT";
                    break;
                case 3:
                    temp.bnType = "NOT CLEARLY AUTHENTICATABLE";
                    break;
                case 4:
                    temp.bnType = "ACCEPTED FIT";
                    break;
                case 5:
                    temp.bnType = "ACCEPTED UNFIT";
                    break;
                default:
                    temp.bnType = "NOTHING";
                    break;
            }
            temp.currBank = Arca.Bin2Dec(giudizio.Substring(3, 3));
            
            return temp;
        }

        public SSampling CmdSamplingReaderStation()
        {
            // ATTENZIONE - RISPOSTA DA COMPLETARE
            string result;
            byte[] comando = { 0xD3, 0x0F};
            comPort.OpenCom();
            
            result = comPort.SendByteArray(comando, 0, 500); // 2000 - modificato time out
            comando = new byte[1];
            comando[0] = 0xA3;
            result = comPort.SendByteArray(comando, 20, 2000);
            comPort.CloseCom();
            SSampling temp = new SSampling
            {
                bnNum = Arca.Hex2Dec(result.Substring(2, 2) + result.Substring(0, 2))
            };
            string giudizio = Arca.Hex2Bin(result.Substring(4, 2));
            int type = Arca.Bin2Dec(giudizio.Substring(0, 3));
            switch (type)
            {
                case 1:
                    temp.bnType = "REJECT";
                    break;
                case 2:
                    temp.bnType = "SUSPECT";
                    break;
                case 3:
                    temp.bnType = "NOT CLEARLY AUTHENTICATABLE";
                    break;
                case 4:
                    temp.bnType = "ACCEPTED FIT";
                    break;
                case 5:
                    temp.bnType = "ACCEPTED UNFIT";
                    break;
                default:
                    temp.bnType = "NOTHING";
                    break;
            }
            temp.currBank = Arca.Bin2Dec(giudizio.Substring(3, 3));

            return temp;
        }

        public string ModeTransparentModule(int IDSensor, int nByteAnswer,  string hexCmd)
        {
            //hexCmd = hexCmd.PadRight(4, '0');
            string command = "D322" + IDSensor.ToString("00") + Arca.Dec2Hex(hexCmd.Length/2,2) + Arca.Dec2Hex(nByteAnswer,2) + hexCmd;
            return command;
        }

        

        public byte[] CmdStreamImageUpload (int imageW, int imageH, int idStream, int idBn = 255)
        {
            Arca.WaitingTime(200);
            byte[] answer;
            byte[] command = { 0xD3, 0x06, 0x05, Convert.ToByte(idBn), Convert.ToByte(idStream) };
            comPort.OpenCom();
            answer = comPort.GetImageBytes(command, imageH * imageW, 200);
            comPort.CloseCom();
            return answer;
        }

        public string CmdSptMagUpload(int page)
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_MAG_ID, 128, "55" + page.ToString("00")), 128);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptCisFrontUpload(int page)
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_CIS_FRONT_ID, 128, "55" + page.ToString("00")), 128);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptCisRearUpload(int page)
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_CIS_REAR_ID, 128, "55" + page.ToString("00")), 128);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptRemoteFrontUpload(int page)
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_REMOTE_FRONT_ID, 128, "55" + page.ToString("00")), 128);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptRemoteRearUpload(int page)
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_REMOTE_REAR_ID, 128, "55" + page.ToString("00")), 128);
            comPort.CloseCom();
            return result;
        }

        public byte[] CmdParamCisGet(int typeData)
        {
            int byteAns = 0;
            byte[] command = { 0xD3, 0x31, Convert.ToByte(typeData) };
            comPort.OpenCom();
            switch (typeData)
            {
                case 0x00: //CURRENT CIS FRONT
                case 0x01: //CURRENT CIS REAR
                case 0x04: //ERROR FLAG SRC CIS FRONT
                case 0x05: //ERROR FLAG SRC CIS REAR
                case 0x10: //CURRENT CIS FRONT - RILETTURA
                case 0x11: //CURRENT CIS REAR - RILETTURA
                case 0x14: //ERROR FLAG SRC CIS FRONT - RILETTURA
                case 0x15: //ERROR FLAG SRC CIS REAR - RILETTURA
                    byteAns = 16;
                    break;
                case 0x02: //PWM CIS FRONT
                case 0x03: //PWM CIS REAR
                case 0x12: //PWM CIS FRONT - RILETTURA
                case 0x13: //PWM CIS REAR - RILETTURA
                    byteAns = 32;
                    break;                                        
                case 0x06: //OFFSET CIS FRONT
                case 0x07: //OFFSET CIS REAR
                case 0x16: //OFFSET CIS FRONT - RILETTURA
                case 0x17: //OFFSET CIS REAR - RILETTURA
                    byteAns = 6;
                    break;
            }
            comPort.SendCommandByte(command, byteAns, 200);
            comPort.CloseCom();
            return comPort.answerByte;
        }

        public string CmdSptCisDownload( RL40.JsonSptCisRoot jSpt, string side = "FRONT")
        {
            byte[] command = { 0xD3, 0x07, 0x03 };
            if (side.ToUpper() != "FRONT")
                command[2] = 0x04;
            comPort.OpenCom();
            comPort.GetJsonFile(command, 200);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }

        public string CmdGetSpt()
        {
            byte[] command = { 0xD3, 0x06, 0x08 };
            comPort.OpenCom();
            comPort.GetJsonFile(command);
            comPort.CloseCom();
            return comPort.commandAnswer;
        }
        
        public string CmdSptCisUpload(int side)
        {
            byte[] command = { 0xD3, 0x06, 0x08 };
            string result = "";
            if (side != 0)
                command[2] = 0x09;
            
            comPort.OpenCom();
            result = comPort.GetJsonFile(command,500);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptCisRilUpload(int side)
        {
            byte[] command = { 0xD3, 0x06, 0x43 };
            string result = "";
            if (side != 0)
                command[2] = 0x44;
            comPort.OpenCom();
            result = comPort.GetJsonFile(command);
            comPort.CloseCom();
            return result;
        }

        public string CmdSptTapeUpload(int page)
        {
            string result;
            int byteAnswer = 128;
            if (page == 0x12)
                byteAnswer = 84;
            comPort.OpenCom();
            //comPort.SendCommandByte(command,byteAnswer,)
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, byteAnswer, "55" + page.ToString("X2")), byteAnswer, 3000);
            comPort.CloseCom();
            return result;
        }

        public void SetDefaultTapeSPT()
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 1, "4E00"), 1);
            comPort.CloseCom();
        }

        public string  SingleCommand(string command, int nByteAnswer = 1, int timeout = 500)
        {
            string result;
            comPort.OpenCom();
            result=comPort.SendCommand(command, nByteAnswer, timeout);
            comPort.CloseCom();
            return result;
        }


        #region TAPE

        public void SPTSet(string page, string address, string value)
        {
            comPort.OpenCom();
            comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 0, "53" + page + address + value),0,200);
            comPort.CloseCom();
        }
        public void SetTapeHwLevel(int hwlevel)
        {
            string hwl = Arca.Dec2Hex(hwlevel);
            comPort.OpenCom();
            for (int i = 0; i < 3; i++)
            {
                comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 0, "5300" + Arca.Dec2Hex(0x78 + i,2) + "3" + hwl.PadLeft(3,'0').Substring(i, 1)), 0, 100);
            }
            comPort.CloseCom();
        }

        public string TapeUpload(byte stream_id, byte channel)
        {
            string result;
            byte nByteAnswer = 0;
            string command = "55" ;
            comPort.OpenCom();
            switch (stream_id)
            {
                case 0x11:
                    command += "11";
                    nByteAnswer = 180;
                    break;
                case 0x12:
                    command += "12";
                    nByteAnswer = 42;
                    break;
            }

            result = comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, nByteAnswer,command + Arca.Dec2Hex(channel,2)),nByteAnswer,100);
            comPort.CloseCom();
            return result;
        }

        public string TapeAdjust()
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 1, "46"),1,30000);
            comPort.CloseCom();
            return result;
        }

        public string TapePreset()
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 1, "5000"));
            comPort.CloseCom();
            return result;
        }

        public string WaitForData(int nByteAnswer, int timeout)
        {
            string result = comPort.WaitingData(nByteAnswer,timeout);
            return result;
        }

        public string SetTapeAdjTargetLevel(int level, int value)
        {
            string result = "";
            string address = "1E";
            if (level == 2)
                address = "20";
            if (level == 3)
                address = "22";
            string hexValue = Arca.Dec2Hex(value, 4);
            comPort.OpenCom();
            comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 0, "5300" + address + hexValue.Substring(0, 2)), 0, 100);
            comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 0, "5300" + (Arca.Hex2Dec(address)+1).ToString("X2") + hexValue.Substring(2, 2)), 0, 100);
            comPort.CloseCom();
            return result;
        }
        public string SetTapeSptCompActive(int value )
        {
            string result="";
            int address = 19;
            comPort.OpenCom();
            comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 0, "5300" + (address).ToString("00") + value.ToString("00")),0, 200);
            comPort.CloseCom();
            return result;
        }

        public string SetTapeSerialNumber(int type, int serialnumber)
        {
            string result = "";
            string sn = Arca.Dec2Hex(serialnumber);
            int address = 70;
            if (type != 1)
                address = 60;
            comPort.OpenCom();
            for (int i = 0; i < 8; i++)
            {
                comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 0, "5300" + (address + i).ToString("00") + "3" + serialnumber.ToString("00000000").Substring(i, 1)), 0, 100);
            }

            //result = comPort.SendCommand(ModeTransparentModule(TRANSP_TAPE_ID, 0, "4C" + type.ToString("00") + serialnumber.ToString("00000000")),0,200);
            comPort.CloseCom();
            return result;
        }
        #endregion

        #region MAG

        public string MagPreset()
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(2, 1, "5000"));
            comPort.CloseCom();
            return result;
        }

        public string MagUpload(string ID, int lenght = 128)
        {
            string result;
            comPort.OpenCom();
            result = comPort.SendCommand(ModeTransparentModule(2, lenght, "55" + ID),lenght);
            comPort.CloseCom();
            return result;
        }

        #endregion
        public string SpeedTransportAnalyses(int testType = 1)
        {
            string result;
            string type = Arca.Dec2Hex(testType, 2);
            int byteAnswer=0;
            int timeout = 1000;
            switch (testType)
            {
                case 1:
                    byteAnswer = 4;
                    timeout = 2000;
                    break;
                case 2:
                    byteAnswer = 16;
                    timeout = 4000;
                    break;
                case 3:
                    byteAnswer = 8192;
                    timeout = 16000;
                    break;
                case 4:
                case 5:
                    byteAnswer = 32768;
                    timeout = 13000;
                    break;
            }
            comPort.OpenCom();
            result = comPort.SendCommand("D30B" + type, byteAnswer, timeout);
            comPort.CloseCom();
            return result;
        }

    }

    public class ArcaXML
    {
        XmlWriterSettings xSettings = new XmlWriterSettings();
        XmlWriter xFile;
        public string readerType;
        string readerXmlFilename;
        public string xmlFilename;
        const string TYPE_READER = "READER";
        const string TYPE_REMOTE = "REMOTE";

        public void FtXmlCreate(string filename, string date, string timeStart, string station, string user, string serial, string swVer = "0.0")
        {
            xmlFilename = "XML\\" + filename;
            xSettings.Indent = true;
            xFile = XmlWriter.Create(xmlFilename, xSettings);
            xFile.WriteStartDocument();
            xFile.WriteStartElement("FT");
            xFile.WriteAttributeString("result", "false");
            xFile.WriteAttributeString("date", date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2));
            xFile.WriteAttributeString("time_start", timeStart.Substring(0, 2) + ":" + timeStart.Substring(2, 2) + ":" + timeStart.Substring(4, 2));
            xFile.WriteAttributeString("time_end", DateTime.Now.ToLongTimeString());
            xFile.WriteAttributeString("station", station);
            xFile.WriteAttributeString("operator", user);
            xFile.WriteAttributeString("serial", serial);
            xFile.WriteAttributeString("sw_version", swVer);
            xFile.WriteEndDocument();
            xFile.Close();
        }
        public void StXmlCreate(string filename, string date, string timeStart, string station, string user, string serial, string swVer = "0.0")
        {
            xmlFilename = "XML\\" + filename;
            xSettings.Indent = true;
            xFile = XmlWriter.Create(xmlFilename, xSettings);
            xFile.WriteStartDocument();
            xFile.WriteStartElement("ST");
            xFile.WriteAttributeString("result", "false");
            xFile.WriteAttributeString("date", date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2));
            xFile.WriteAttributeString("time_start", timeStart.Substring(0, 2) + ":" + timeStart.Substring(2, 2) + ":" + timeStart.Substring(4, 2));
            xFile.WriteAttributeString("time_end", DateTime.Now.ToLongTimeString());
            xFile.WriteAttributeString("station", station);
            xFile.WriteAttributeString("operator", user);
            xFile.WriteAttributeString("serial", serial);
            xFile.WriteAttributeString("sw_version", swVer);
            xFile.WriteEndDocument();
            xFile.Close();
        }
        public void UtmXmlCreate(string filename, string date, string timeStart, string station, string user, string serial, string swVer = "0.0")
        {
            xmlFilename = "XML\\" + filename;
            xSettings.Indent = true;
            xFile = XmlWriter.Create(xmlFilename, xSettings);
            xFile.WriteStartDocument();
            xFile.WriteStartElement("UTM");
            xFile.WriteAttributeString("result", "false");
            xFile.WriteAttributeString("date", date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2));
            xFile.WriteAttributeString("time_start", timeStart.Substring(0, 2) + ":" + timeStart.Substring(2, 2) + ":" + timeStart.Substring(4, 2));
            xFile.WriteAttributeString("time_end", DateTime.Now.ToLongTimeString());
            xFile.WriteAttributeString("station", station);
            xFile.WriteAttributeString("operator", user);
            xFile.WriteAttributeString("serial", serial);
            xFile.WriteAttributeString("sw_version", swVer);
            xFile.WriteEndDocument();
            xFile.Close();
        }

        public void LtmXmlCreate(string filename, string date, string timeStart, string station, string user, string serial, string swVer = "0.0")
        {
            xmlFilename = "XML\\" + filename;
            xSettings.Indent = true;
            xFile = XmlWriter.Create(xmlFilename, xSettings);
            xFile.WriteStartDocument();
            xFile.WriteStartElement("LTM");
            xFile.WriteAttributeString("result", "false");
            xFile.WriteAttributeString("date", date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2));
            xFile.WriteAttributeString("time_start", timeStart.Substring(0, 2) + ":" + timeStart.Substring(2, 2) + ":" + timeStart.Substring(4, 2));
            xFile.WriteAttributeString("time_end", DateTime.Now.ToLongTimeString());
            xFile.WriteAttributeString("station", station);
            xFile.WriteAttributeString("operator", user);
            xFile.WriteAttributeString("serial", serial);
            xFile.WriteAttributeString("sw_version", swVer);
            xFile.WriteEndDocument();
            xFile.Close();
        }

        public void R5000FeederXmlCreate(string filename, string date, string timeStart, string station, string user, string serial, string tl, string swVer = "0.0")
        {
            xmlFilename = "XML\\" + filename;
            xSettings.Indent = true;
            xFile = XmlWriter.Create(xmlFilename, xSettings);
            xFile.WriteStartDocument();
            xFile.WriteStartElement("R5000Feeder");
            xFile.WriteAttributeString("result", "false");
            xFile.WriteAttributeString("date", date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2));
            xFile.WriteAttributeString("time_start", timeStart.Substring(0, 2) + ":" + timeStart.Substring(2, 2) + ":" + timeStart.Substring(4, 2));
            xFile.WriteAttributeString("time_end", DateTime.Now.ToLongTimeString());
            xFile.WriteAttributeString("station", station);
            xFile.WriteAttributeString("operator", user);
            xFile.WriteAttributeString("serial", serial);
            xFile.WriteAttributeString("TL", tl);
            xFile.WriteAttributeString("sw_version", swVer);

            //xFile.WriteStartElement("Test");
            //xFile.WriteFullEndElement();
            //xFile.WriteEndElement();
            xFile.WriteEndDocument();
            xFile.Close();
        }

        public void DdmXmlCreate(string filename, string date, string timeStart, string station, string user, string serial, string tl, string swVer = "0.0")
        {
            xmlFilename = "XML\\" + filename;
            xSettings.Indent = true;
            xFile = XmlWriter.Create(xmlFilename, xSettings);
            xFile.WriteStartDocument();
            xFile.WriteStartElement("DDM");
            xFile.WriteAttributeString("result", "false");
            xFile.WriteAttributeString("date", date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2));
            xFile.WriteAttributeString("time_start", timeStart.Substring(0, 2) + ":" + timeStart.Substring(2, 2) + ":" + timeStart.Substring(4, 2));
            xFile.WriteAttributeString("time_end", DateTime.Now.ToLongTimeString());
            xFile.WriteAttributeString("station", station);
            xFile.WriteAttributeString("operator", user);
            xFile.WriteAttributeString("serial", serial);
            xFile.WriteAttributeString("TL", tl);
            xFile.WriteAttributeString("sw_version", swVer);

            //xFile.WriteStartElement("Test");
            //xFile.WriteFullEndElement();
            //xFile.WriteEndElement();
            xFile.WriteEndDocument();
            xFile.Close();
        }

        /// <summary>
        /// Creazione file XLM di log di collaudo 
        /// </summary>
        /// <param name="filename">Nome del file xml completo di path</param>
        /// <param name="date">Data del collaudo in formato YYYYMMDD</param>
        /// <param name="timeStart">Ora dell'inizio del collaudo in formato HHMMSS</param>
        /// <param name="model">Modello del lettore (4char)</param>
        /// <param name="hwLevel">livello hw del lettore</param>
        /// <param name="idReader">Serial number completo (12char)</param>
        /// <param name="idParamSet">Identificato del parametro</param>
        /// <param name="station">Nome del PC utilizzato per il collaudo</param>
        /// <param name="user">Nome operatore che esegue il collaudo</param>
        /// <param name="serial">Numero di serie (7char)</param>
        public void ReaderXmlCreate(string filename, string date, string timeStart, string model, string hwLevel, string idReader, string idParamSet, string station, string user, string serial)
        {
            readerXmlFilename = "XML\\" + filename;
            xSettings.Indent = true;
            xFile = XmlWriter.Create(readerXmlFilename, xSettings);
            xFile.WriteStartDocument();
            xFile.WriteStartElement("Reader");
            xFile.WriteAttributeString("result", "false");
            xFile.WriteAttributeString("date", date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2));
            xFile.WriteAttributeString("time_start", timeStart.Substring(0, 2) + ":" + timeStart.Substring(2, 2) + ":" + timeStart.Substring(4, 2));
            xFile.WriteAttributeString("time_end", DateTime.Now.ToLongTimeString());
            xFile.WriteAttributeString("model", model);
            xFile.WriteAttributeString("hw_level", hwLevel);
            xFile.WriteAttributeString("id_reader", idReader);
            xFile.WriteAttributeString("id_param_set", idParamSet);
            xFile.WriteAttributeString("station", station);
            xFile.WriteAttributeString("operator", user);
            xFile.WriteAttributeString("series", serial.Substring(0, 1));
            xFile.WriteAttributeString("serial", serial);
            xFile.WriteAttributeString("sw_version", "0.0"); 
            
            xFile.WriteStartElement("Modules");
            xFile.WriteFullEndElement();
            xFile.WriteEndElement();
            xFile.WriteEndDocument();
            xFile.Close();
        }


        public void XmlEditTest(string testName, params string[] additionalAttributes)
        {
            bool trovato = false;
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[i].Name == "Test")
                {
                    for (int x = 0; x < root1.ChildNodes[i].ChildNodes.Count; x++)
                    {
                        if (root1.ChildNodes[i].ChildNodes[x].Name == testName)
                        {
                            trovato = true;

                        }
                    }
                }
            }
            if (trovato == false)
            {
                XmlAddElement(testName, additionalAttributes);
            }
            //xDocument.Save(xmlFilename);
        }
        public void XmlAddElmElement(string elmName, string elementName)
        {

            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            foreach (XmlNode node in root1.ChildNodes)
            {
                if (node.Name == elmName)
                {
                    XmlNode myNode = xDocument.CreateNode(XmlNodeType.Element, elementName, "");
                    node.AppendChild(myNode);
                    break;
                }
            }

            xDocument.Save(xmlFilename);

            XmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
        }

        

        public void XmlAddElement(string elementName, params string[] additionalAttributes)
        {

            string[] stdAttributes = { "result", "false", "date", DateTime.Now.ToString("yyyy-MM-dd"), "time_start", DateTime.Now.ToLongTimeString(), "time_end", DateTime.Now.ToLongTimeString()};
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlNode child = xDocument.CreateNode(XmlNodeType.Element, elementName, "");

            for (int y = 0; y < stdAttributes.Length; y += 2)
            {
                XmlAttribute attrib = xDocument.CreateAttribute(stdAttributes[y]);
                attrib.Value = stdAttributes[y + 1];
                child.Attributes.Append(attrib);
            }

            for (int x = 0; x < additionalAttributes.Length; x += 2)
            {
                XmlAttribute attrib = xDocument.CreateAttribute(additionalAttributes[x]);
                attrib.Value = additionalAttributes[x + 1];
                child.Attributes.Append(attrib);
            }
            root1.AppendChild(child);

            xDocument.Save(xmlFilename);

            XmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
        }

        public void XmlAddPhotoElement(string elementName, string photo, params string[] additionalAttributes)
        {
            string[] stdAttributes = { "result", "false", "date", DateTime.Now.ToString("yyyy-MM-dd"), "time_start", DateTime.Now.ToLongTimeString(), "time_end", DateTime.Now.ToLongTimeString() };
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[i].Name == elementName)
                {
                    XmlNode child = xDocument.CreateNode(XmlNodeType.Element, photo, "");
                    for (int y = 0; y < stdAttributes.Length; y += 2)
                    {
                        XmlAttribute attrib = xDocument.CreateAttribute(stdAttributes[y]);
                        attrib.Value = stdAttributes[y + 1];
                        child.Attributes.Append(attrib);
                    }

                    for (int x = 0; x < additionalAttributes.Length; x += 2)
                    {
                        XmlAttribute attrib = xDocument.CreateAttribute(additionalAttributes[x]);
                        attrib.Value = additionalAttributes[x + 1];
                        child.Attributes.Append(attrib);
                    }
                    root1.ChildNodes[i].AppendChild(child);
                    for (int z = 0; z < root1.ChildNodes[i].ChildNodes.Count; z++)
                    {
                        if (root1.ChildNodes[i].ChildNodes[z].Name == photo)
                        {
                            XmlNode sogliaDoc = xDocument.CreateNode(XmlNodeType.Element, "sogliaDoc","");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(sogliaDoc);
                            XmlNode agcDoc = xDocument.CreateNode(XmlNodeType.Element, "agcDoc", "");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(agcDoc);
                            XmlNode correnteDoc = xDocument.CreateNode(XmlNodeType.Element, "correnteDoc", "");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(correnteDoc);
                            XmlNode sogliaNoDoc = xDocument.CreateNode(XmlNodeType.Element, "sogliaNoDoc", "");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(sogliaNoDoc);
                            XmlNode agcNoDoc = xDocument.CreateNode(XmlNodeType.Element, "agcNoDoc", "");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(agcNoDoc);
                            XmlNode correnteNoDoc = xDocument.CreateNode(XmlNodeType.Element, "correnteNoDoc", "");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(correnteNoDoc);
                            XmlNode sogliaWork = xDocument.CreateNode(XmlNodeType.Element, "sogliaWork", "");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(sogliaWork);
                            XmlNode agcWork = xDocument.CreateNode(XmlNodeType.Element, "agcWork", "");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(agcWork);
                            XmlNode correnteWork = xDocument.CreateNode(XmlNodeType.Element, "correnteWork", "");
                            root1.ChildNodes[i].ChildNodes[z].AppendChild(correnteWork);
                        }
                    }
                    
                    xDocument.Save(xmlFilename);
                    break;
                }
            }



            XmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
        }

        public void XmlAddElementToElement(string fatherElement, string elementName, params string[] additionalAttributes)
        {

            string[] stdAttributes = { "result", "false", "date", DateTime.Now.ToString("yyyy-MM-dd"), "time_start", DateTime.Now.ToLongTimeString(), "time_end", DateTime.Now.ToLongTimeString() };
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[i].Name == fatherElement)
                {
                    XmlNode child = xDocument.CreateNode(XmlNodeType.Element, elementName, "");

                    for (int y = 0; y < stdAttributes.Length; y += 2)
                    {
                        XmlAttribute attrib = xDocument.CreateAttribute(stdAttributes[y]);
                        attrib.Value = stdAttributes[y + 1];
                        child.Attributes.Append(attrib);
                    }

                    for (int x = 0; x < additionalAttributes.Length; x += 2)
                    {
                        XmlAttribute attrib = xDocument.CreateAttribute(additionalAttributes[x]);
                        attrib.Value = additionalAttributes[x + 1];
                        child.Attributes.Append(attrib);
                    }
                    root1.ChildNodes[i].AppendChild(child);
                    xDocument.Save(xmlFilename);
                    break;
                }
            }
            XmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
        }

        public void XmlAddElementToElementToElement(string grandFatherElement, string element, string fatherElement, params string[] additionalAttributes)
        {

            string[] stdAttributes = { "result", "false", "date", DateTime.Now.ToString("yyyy-MM-dd"), "time_start", DateTime.Now.ToLongTimeString(), "time_end", DateTime.Now.ToLongTimeString() };
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[i].Name == grandFatherElement)
                {
                    for (int x = 0; x < root1.ChildNodes.Count; x++)
                    {
                        if (root1.ChildNodes[i].ChildNodes[x].Name == fatherElement)
                        {
                            XmlNode child = xDocument.CreateNode(XmlNodeType.Element, element, "");

                            for (int y = 0; y < stdAttributes.Length; y += 2)
                            {
                                XmlAttribute attrib = xDocument.CreateAttribute(stdAttributes[y]);
                                attrib.Value = stdAttributes[y + 1];
                                child.Attributes.Append(attrib);
                            }

                            for (int z = 0; z < additionalAttributes.Length; z += 2)
                            {
                                XmlAttribute attrib = xDocument.CreateAttribute(additionalAttributes[z]);
                                attrib.Value = additionalAttributes[z + 1];
                                child.Attributes.Append(attrib);
                            }
                            root1.ChildNodes[i].ChildNodes[x].AppendChild(child);
                            xDocument.Save(xmlFilename);
                            break;
                        }
                    }
                    
                }
            }
            XmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
        }

        public void XmlAddFwElement(string elementName, params string[] additionalAttributes)
        {

            string[] stdAttributes = { "result", "false", "date", DateTime.Now.ToString("yyyy-MM-dd"), "time_start", DateTime.Now.ToLongTimeString(), "time_end", DateTime.Now.ToLongTimeString() };
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[i].Name == "FW")
                {
                    XmlNode child = xDocument.CreateNode(XmlNodeType.Element, elementName, "");

                    for (int y = 0; y < stdAttributes.Length; y += 2)
                    {
                        XmlAttribute attrib = xDocument.CreateAttribute(stdAttributes[y]);
                        attrib.Value = stdAttributes[y + 1];
                        child.Attributes.Append(attrib);
                    }

                    for (int x = 0; x < additionalAttributes.Length; x += 2)
                    {
                        XmlAttribute attrib = xDocument.CreateAttribute(additionalAttributes[x]);
                        attrib.Value = additionalAttributes[x + 1];
                        child.Attributes.Append(attrib);
                    }
                    root1.ChildNodes[i].AppendChild(child);
                    xDocument.Save(xmlFilename);
                    break;
                }
            }
            XmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
        }

        /// <summary>
        /// Aggiunge un nuovo modulo nel file xml sotto il ramo 'Modules' (nessun controllo su eventuali duplicati)
        /// </summary>
        /// <param name="elementName">nome del modulo da aggiungere</param>
        /// <param name="attributes">Attributi in aggiunta a quelli standard (result,date_start,time_start,time_end)</param>
        public void ReaderXmlAddModule(string elementName, params string[] additionalAttributes)
        {
            /*SEQUENZA ATTRIBUTI:
             * COMUNI:
             * result
             * date
             * time_start
             * time_end
             * hw_level
             * host_status
             * dsp_status
             * com_status
             * val_status
             * */
            string[] stdAttributes = { "result", "false", "date", DateTime.Now.ToString("yyyy-MM-dd"), "time_start", DateTime.Now.ToLongTimeString(), "time_end", DateTime.Now.ToLongTimeString(), "hw_level", "0", "com_status", "00", "val_status", "00"};
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(readerXmlFilename);
            XmlNode root1 = xDocument.DocumentElement;

            XmlNode child = xDocument.CreateNode(XmlNodeType.Element, elementName, "");

            for (int y = 0; y < stdAttributes.Length; y += 2)
            {
                XmlAttribute attrib = xDocument.CreateAttribute(stdAttributes[y]);
                attrib.Value = stdAttributes[y + 1];
                child.Attributes.Append(attrib);
            }

            for (int x = 0; x < additionalAttributes.Length; x += 2)
            {
                XmlAttribute attrib = xDocument.CreateAttribute(additionalAttributes[x]);
                attrib.Value = additionalAttributes[x + 1];
                child.Attributes.Append(attrib);
            }
            for (int i = 0; i < root1.ChildNodes.Count;i++)
            {
                if (root1.ChildNodes[i].Name == "Modules")
                {
                    if (elementName == "Mag")
                    {
                        for (int moduleID = root1.ChildNodes[i].ChildNodes.Count - 1 ; moduleID >= 0; moduleID--)
                        {
                            if (root1.ChildNodes[i].ChildNodes[moduleID].Name == elementName)
                            {
                                root1.ChildNodes[i].InsertAfter(child, root1.ChildNodes[i].ChildNodes[moduleID]);
                                xDocument.Save(readerXmlFilename);
                                goto fine;
                            }
                        }
                    }
                    root1.ChildNodes[i].AppendChild(child);
                    xDocument.Save(readerXmlFilename);
                }
            }
            fine:
            ReaderXmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
        }

        public void ReaderXmlAddCisElement(int pos, string elementName, string value, params string[] attributes)
        {
            if (readerType == TYPE_REMOTE && pos != 1 || readerType == TYPE_READER)
            {
                XmlDocument xDocument = new XmlDocument();
                xDocument.Load(readerXmlFilename);
                XmlNode root1 = xDocument.DocumentElement;
                XmlElement root = xDocument.DocumentElement;
                XmlNodeList rootNodes = root.ChildNodes;
                foreach (XmlNode rootNode in rootNodes)
                {
                    XmlNodeList moduleNodes = rootNode.ChildNodes;

                    for (int i = moduleNodes.Count - 1; i >= 0; i--)
                    {
                        if (moduleNodes[i].Name == "CIS" && moduleNodes[i].Attributes["pos"].Value == pos.ToString())
                        {
                            XmlNode child = xDocument.CreateElement(elementName);
                            child.InnerText = value;
                            moduleNodes[i].AppendChild(child);
                            xDocument.Save(readerXmlFilename);
                            break;
                        }
                    }
                }
            }
        }

        public void ReaderXmlEditCisElement(int pos, string elementName, string newValue)
        {
            if (readerType == TYPE_REMOTE && pos != 1 || readerType==TYPE_READER)
            {
                bool trovato = false;
                XmlDocument xDocument = new XmlDocument();
                xDocument.Load(readerXmlFilename);
                int idreader, idmodules, idmodule, idelement;
                for (int i = 0; i < xDocument.ChildNodes.Count; i++)
                {
                    if (xDocument.ChildNodes[i].Name == "Reader")
                    {
                        idreader = i;
                        for (int x = 0; x < xDocument.ChildNodes[idreader].ChildNodes.Count; x++)
                        {
                            if (xDocument.ChildNodes[idreader].ChildNodes[x].Name == "Modules")
                            {
                                idmodules = x;
                                for (int y = (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes.Count - 1); y >= 0; y--)
                                {
                                    if (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[y].Name == "CIS" && xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[y].Attributes["pos"].Value == pos.ToString())
                                    {
                                        idmodule = y;
                                        if (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes.Count == 0)
                                            goto salva;
                                        for (int z = (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes.Count - 1); z >= 0; z--)
                                        {
                                            if (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes[z].Name == elementName)
                                            {
                                                trovato = true;
                                                idelement = z;
                                                XmlNode child = xDocument.CreateElement(elementName);
                                                child.InnerText = newValue;
                                                xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ReplaceChild(child, xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes[idelement]);
                                                xDocument.Save(readerXmlFilename);
                                                goto end;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                salva:
                if (trovato == false)
                {
                    ReaderXmlAddCisElement(pos, elementName, newValue);
                }
            }
            end:;
            
        }

        public void ReaderXmlEditCisAttribute(int pos, string attributeName, string newAttributeValue, bool AddIfNotExist = true)
        {
            if (readerType == TYPE_REMOTE && pos != 1 || readerType == TYPE_READER)
            {
                bool trovato = false;
                XmlDocument xDocument = new XmlDocument();
                xDocument.Load(readerXmlFilename);
                XmlNode root1 = xDocument.DocumentElement;
                for (int i = 0; i < root1.ChildNodes[0].ChildNodes.Count; i++)
                {
                    if (root1.ChildNodes[0].ChildNodes[i].Name == "CIS" && root1.ChildNodes[0].ChildNodes[i].Attributes["pos"].Value == pos.ToString())
                    {
                        for (int x = 0; x < root1.ChildNodes[0].ChildNodes[i].Attributes.Count; x++)
                        {
                            if (root1.ChildNodes[0].ChildNodes[i].Attributes[x].Name == "time_end")
                            {
                                root1.ChildNodes[0].ChildNodes[i].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                            }
                            if (root1.ChildNodes[0].ChildNodes[i].Attributes[x].Name == attributeName)
                            {
                                trovato = true;
                                root1.ChildNodes[0].ChildNodes[i].Attributes[x].Value = newAttributeValue;
                            }

                        }
                    }
                }
                if (trovato == false)
                {
                    //Aggiungere l'attributo??

                }
                xDocument.Save(readerXmlFilename);
            }
        }


        public void ReaderXmlAddReaderElement(string elementName, string value, params string[] attributes)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(readerXmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            XmlNode child = xDocument.CreateElement(elementName);
            child.InnerText = value;
            root.AppendChild(child);
            xDocument.Save(readerXmlFilename);
        }

        public void XmlEditPhotoElementAttribute(string photoName, string attributeName, string newAttributeValue, string suffix = "")
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[i].Name == "PhotoAdjust" + suffix)
                    for (int y = 0; y < root1.ChildNodes[i].ChildNodes.Count; y++)
                        if (root1.ChildNodes[i].ChildNodes[y].Name == photoName)
                            for (int x = 0; x < root1.ChildNodes[i].ChildNodes[y].Attributes.Count; x++)
                            {
                                if (root1.ChildNodes[i].ChildNodes[y].Attributes[x].Name == attributeName)
                                    root1.ChildNodes[i].ChildNodes[y].Attributes[x].Value = newAttributeValue;
                                if (root1.ChildNodes[i].ChildNodes[y].Attributes[x].Name == "time_end")
                                    root1.ChildNodes[i].ChildNodes[y].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                            }
            }
            xDocument.Save(xmlFilename);
        }

        public void XmlEditElementAttributeOfElement(string fatherElement, string elementName, string attributeName, string newAttributeValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
                if (root1.ChildNodes[i].Name == fatherElement)
                    for (int y = 0; y < root1.ChildNodes[i].ChildNodes.Count; y++)
                        if (root1.ChildNodes[i].ChildNodes[y].Name == elementName)
                            for (int x = 0; x < root1.ChildNodes[i].ChildNodes[y].Attributes.Count; x++)
                            {
                                if (root1.ChildNodes[i].ChildNodes[y].Attributes[x].Name == attributeName)
                                    root1.ChildNodes[i].ChildNodes[y].Attributes[x].Value = newAttributeValue;
                                if (root1.ChildNodes[i].ChildNodes[y].Attributes[x].Name == "time_end")
                                    root1.ChildNodes[i].ChildNodes[y].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                            }
            xDocument.Save(xmlFilename);
        }
        public void XmlEditElementAttributeOfElementOfElement(string granFatherElement, string fatherElement, string elementName, string attributeName, string newAttributeValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int gf = 0; gf < root1.ChildNodes.Count; gf++)
                if (root1.ChildNodes[gf].Name == granFatherElement)
                    for (int f = 0; f < root1.ChildNodes[gf].ChildNodes.Count; f++)
                        if (root1.ChildNodes[gf].ChildNodes[f].Name == fatherElement)
                            for (int e=0; e < root1.ChildNodes[gf].ChildNodes[f].ChildNodes.Count;e++)
                                if (root1.ChildNodes[gf].ChildNodes[f].ChildNodes[e].Name == elementName)
                                    for (int a = 0; a < root1.ChildNodes[gf].ChildNodes[f].ChildNodes[e].Attributes.Count; a++)
                                    {
                                        if (root1.ChildNodes[gf].ChildNodes[f].ChildNodes[e].Attributes[a].Name == attributeName)
                                            root1.ChildNodes[gf].ChildNodes[f].ChildNodes[e].Attributes[a].Value = newAttributeValue;
                                        if (root1.ChildNodes[gf].ChildNodes[f].ChildNodes[e].Attributes[a].Name == "time_end")
                                            root1.ChildNodes[gf].ChildNodes[f].ChildNodes[e].Attributes[a].Value = DateTime.Now.ToLongTimeString();
                                    }
            xDocument.Save(xmlFilename);
        }

        public void XmlEditFwElementAttribute(string elementName, string attributeName, string newAttributeValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
                if (root1.ChildNodes[i].Name == "FW")
                    for (int y = 0; y < root1.ChildNodes[i].ChildNodes.Count; y++)
                        if (root1.ChildNodes[i].ChildNodes[y].Name == elementName)
                            for (int x = 0; x < root1.ChildNodes[i].ChildNodes[y].Attributes.Count; x++)
                            {
                                if (root1.ChildNodes[i].ChildNodes[y].Attributes[x].Name == attributeName)
                                    root1.ChildNodes[i].ChildNodes[y].Attributes[x].Value = newAttributeValue;
                                if (root1.ChildNodes[i].ChildNodes[y].Attributes[x].Name == "time_end")
                                    root1.ChildNodes[i].ChildNodes[y].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                            }
            xDocument.Save(xmlFilename);
        }

        public void XmlEditElementAttribute(string elementName, string attributeName, string newAttributeValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes.Count; i++)
                if (root1.ChildNodes[i].Name == elementName)
                    for (int x = 0; x < root1.ChildNodes[i].Attributes.Count; x++)
                    {
                        if (root1.ChildNodes[i].Attributes[x].Name == attributeName)
                            root1.ChildNodes[i].Attributes[x].Value = newAttributeValue;
                        if (root1.ChildNodes[i].Attributes[x].Name == "time_end")
                            root1.ChildNodes[i].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                    }
            xDocument.Save(xmlFilename);
        }

        public void XmlEditElementOfElement(string fatherElement, string elementName, string elementValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            foreach (XmlNode rootNode in rootNodes)
                if (rootNode.Name == fatherElement)
                {
                    XmlNodeList moduleNodes = rootNode.ChildNodes;
                    for (int i = moduleNodes.Count - 1; i >= 0; i--)
                        if (moduleNodes[i].Name == elementName)
                        {
                            moduleNodes[i].InnerText = elementValue.ToString();
                            break;
                        }
                }
            xDocument.Save(xmlFilename);
        }

        public void XmlEditElementOfElementOfElement(string grandFatherElement, string fatherElement, string element, string elementValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            foreach (XmlNode rootNode in rootNodes)
                if (rootNode.Name == grandFatherElement)
                {
                    XmlNodeList moduleNodes = rootNode.ChildNodes;
                    for (int i = moduleNodes.Count - 1; i >= 0; i--)
                        if (moduleNodes[i].Name == fatherElement)
                        {
                            for (int x = 0; x < moduleNodes[i].ChildNodes.Count; x++)
                            {
                                if (moduleNodes[i].ChildNodes[x].Name == element)
                                {
                                    moduleNodes[i].InnerText = elementValue.ToString();
                                    break;
                                }
                            }
                            
                        }
                }
            xDocument.Save(xmlFilename);
        }

        public void XmlEditElmElement(string elmName, string elementName, int elementValue, params string[] attributes)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            foreach (XmlNode rootNode in rootNodes)
                if (rootNode.Name == elmName)
                {
                    XmlNodeList moduleNodes = rootNode.ChildNodes;
                    for (int i = moduleNodes.Count - 1; i >= 0; i--)
                        if (moduleNodes[i].Name == elementName)
                        {
                            moduleNodes[i].InnerText = elementValue.ToString();
                            break;
                        }
                }
            xDocument.Save(xmlFilename);
        }
        public void XmlEditElmElement(string elmName, string elementName, string elementValue, params string[] attributes)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            foreach (XmlNode rootNode in rootNodes)
                if (rootNode.Name == elmName)
                {
                    XmlNodeList moduleNodes = rootNode.ChildNodes;
                    for (int i = moduleNodes.Count - 1; i >= 0; i--)
                        if (moduleNodes[i].Name == elementName)
                        {
                            moduleNodes[i].InnerText = elementValue.ToString();
                            break;
                        }
                }
            xDocument.Save(xmlFilename);
        }
        public void XmlEditPhotoAdjustElement(string module, string photoName, Module.SPhoto myPhoto, params string[] attributes)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            foreach (XmlNode rootNode in rootNodes)
                if (rootNode.Name == module)
                {
                    XmlNodeList moduleNodes = rootNode.ChildNodes;
                    for (int i = moduleNodes.Count - 1; i >= 0; i--)
                        if (moduleNodes[i].Name == photoName)
                            for (int x = 0; x < moduleNodes[i].ChildNodes.Count; x++)
                            {
                                switch(moduleNodes[i].ChildNodes[x].Name)
                                {
                                    case "sogliaDoc":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.sogliaDoc.ToString();
                                        break;
                                    case "agcDoc":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.agcDoc.ToString();
                                        break;
                                    case "correnteDoc":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.currentDoc.ToString();
                                        break;
                                    case "sogliaNoDoc":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.sogliaNoDoc.ToString();
                                        break;
                                    case "agcNoDoc":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.agcNoDoc.ToString();
                                        break;
                                    case "correnteNoDoc":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.currentNoDoc.ToString();
                                        break;
                                    case "sogliaWork":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.sogliaWork.ToString();
                                        break;
                                    case "agcWork":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.agcWork.ToString();
                                        break;
                                    case "correnteWork":
                                        moduleNodes[i].ChildNodes[x].InnerText = myPhoto.currentWork.ToString();
                                        break;
                                }
                            }
                }
            xDocument.Save(xmlFilename);
        }

        public void XmlEditFwElementDetail(string module, string elementName, string value, params string[] attributes)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            foreach (XmlNode rootNode in rootNodes)
                if (rootNode.Name.ToUpper() == "FW")
                {
                    XmlNodeList fwNodes = rootNode.ChildNodes;
                    for (int i = fwNodes.Count - 1; i >= 0; i--)
                        if (fwNodes[i].Name == module)
                        {
                            XmlNodeList fwElement = fwNodes[i].ChildNodes;
                            for (int x = 0; x < fwElement.Count; x++)
                            {
                                if (fwElement[x].Name == elementName)
                                {
                                    XmlNode child2 = xDocument.CreateElement(elementName);
                                    child2.InnerText = value;
                                    fwNodes[i].ReplaceChild(child2, fwElement[x]);
                                    goto fine;
                                }
                            }
                            XmlNode child = xDocument.CreateElement(elementName);
                            child.InnerText = value;
                            fwNodes[i].AppendChild(child);
                            goto fine;
                        }
                }
            fine:
            xDocument.Save(xmlFilename);
        }

        
        public void XmlAddTestElement(string testName, string elementName, string value, params string[] attributes)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            foreach (XmlNode rootNode in rootNodes)
            {
                XmlNodeList moduleNodes = rootNode.ChildNodes;
                for (int i = moduleNodes.Count - 1; i >= 0; i--)
                    if (moduleNodes[i].Name == testName)
                    {
                        XmlNode child = xDocument.CreateElement(elementName);
                        child.InnerText = value;
                        moduleNodes[i].AppendChild(child);
                        xDocument.Save(xmlFilename);
                        break;
                    }
            }
        }

        public void ReaderXmlAddModuleElement(string moduleName, string elementName, string value, params string[] attributes)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(readerXmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            XmlElement root = xDocument.DocumentElement;
            XmlNodeList rootNodes = root.ChildNodes;
            foreach (XmlNode rootNode in rootNodes)
            {
                XmlNodeList moduleNodes = rootNode.ChildNodes;
                for (int i = moduleNodes.Count - 1; i >= 0; i--)
                    if (moduleNodes[i].Name == moduleName)
                    {
                        XmlNode child = xDocument.CreateElement(elementName);
                        child.InnerText = value;
                        moduleNodes[i].AppendChild(child);
                        xDocument.Save(readerXmlFilename);
                        break;
                    }
            }
        }

        public void XmlEditElement(string moduleName, string elementName, string newValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            int idreader, idelement;
            for (int i = 0; i < xDocument.ChildNodes.Count; i++)
                if (xDocument.ChildNodes[i].Name == moduleName)
                {
                    idreader = i;
                    for (int z = (xDocument.ChildNodes[idreader].ChildNodes.Count - 1); z >= 0; z--)
                        if (xDocument.ChildNodes[idreader].ChildNodes[z].Name == elementName)
                        {
                            idelement = z;
                            XmlNode child = xDocument.CreateElement(elementName);
                            child.InnerText = newValue;
                            xDocument.ChildNodes[idreader].ReplaceChild(child, xDocument.ChildNodes[idreader].ChildNodes[idelement]);
                            xDocument.Save(xmlFilename);
                            goto end;
                        }
                }
            XmlAddElement(elementName, newValue);
            end:;
        }

        public void ReaderXmlEditReaderElement(string elementName, string newValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(readerXmlFilename);
            int idreader, idelement;
            for (int i = 0; i < xDocument.ChildNodes.Count; i++)
            {
                if (xDocument.ChildNodes[i].Name == "Reader")
                {
                    idreader = i;
                    for (int z = (xDocument.ChildNodes[idreader].ChildNodes.Count - 1); z >= 0; z--)
                    {
                        if (xDocument.ChildNodes[idreader].ChildNodes[z].Name == elementName)
                        {
                            idelement = z;
                            XmlNode child = xDocument.CreateElement(elementName);
                            child.InnerText = newValue;
                            xDocument.ChildNodes[idreader].ReplaceChild(child, xDocument.ChildNodes[idreader].ChildNodes[idelement]);
                            xDocument.Save(readerXmlFilename);
                            goto end;
                        }
                    }
                }
            }
            ReaderXmlAddReaderElement(elementName, newValue);
            end:;

        }

        public void XmlEditTestElement(string moduleName, string testName, string elementName, string newValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            int idreader, idmodules, idmodule, idelement;
            for (int i = 0; i < xDocument.ChildNodes.Count; i++)
            {
                if (xDocument.ChildNodes[i].Name == moduleName)
                {
                    idreader = i;
                    for (int x = 0; x < xDocument.ChildNodes[idreader].ChildNodes.Count; x++)
                    {
                        if (xDocument.ChildNodes[idreader].ChildNodes[x].Name == "Test")
                        {
                            idmodules = x;
                            for (int y = (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes.Count - 1); y >= 0; y--)
                            {
                                if (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[y].Name == testName)
                                {
                                    idmodule = y;
                                    for (int z = (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes.Count - 1); z >= 0; z--)
                                    {
                                        if (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes[z].Name == elementName)
                                        {
                                            idelement = z;
                                            XmlNode child = xDocument.CreateElement(elementName);
                                            child.InnerText = newValue;
                                            xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ReplaceChild(child, xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes[idelement]);
                                            xDocument.Save(xmlFilename);
                                            ReaderXmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
                                            ReaderXmlEditModuleAttribute(testName, "time_end", DateTime.Now.ToShortTimeString().ToString());
                                            goto end;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            XmlAddTestElement(testName, elementName, newValue);

            end:;

        }

        public void ReaderXmlEditModuleElement (string moduleName, string elementName,string newValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(readerXmlFilename);
            int idreader, idmodules, idmodule, idelement;
            for (int i = 0; i < xDocument.ChildNodes.Count; i++) // 
            {
                if (xDocument.ChildNodes[i].Name == "Reader")
                {
                    idreader = i;
                    for (int x = 0; x < xDocument.ChildNodes[idreader].ChildNodes.Count; x++)
                    {
                        if (xDocument.ChildNodes[idreader].ChildNodes[x].Name == "Modules")
                        {
                            idmodules = x;
                            for (int y = (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes.Count-1); y >= 0 ; y--)
                            {
                                if (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[y].Name == moduleName)
                                {
                                    idmodule = y;
                                    if (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes.Count == 0)
                                        goto aggiungi;
                                    for (int z = (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes.Count - 1); z >=0 ; z--)
                                    {
                                        if (xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes[z].Name == elementName)
                                        {
                                            idelement = z;
                                            XmlNode child = xDocument.CreateElement(elementName);
                                            child.InnerText = newValue;
                                            xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ReplaceChild(child, xDocument.ChildNodes[idreader].ChildNodes[idmodules].ChildNodes[idmodule].ChildNodes[idelement]);
                                            xDocument.Save(readerXmlFilename);
                                            ReaderXmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
                                            ReaderXmlEditModuleAttribute(moduleName, "time_end", DateTime.Now.ToShortTimeString().ToString());
                                            goto end;
                                        }
                                    }
                                    goto aggiungi;
                                }
                            }
                        }
                    }
                }
            }
            aggiungi:
            ReaderXmlAddModuleElement(moduleName, elementName, newValue);
            
            end:;

        }

        public void XmlEditTestAttribute(string testName, string attributeName, string newAttributeValue, bool AddIfNotExist = true)
        {
            bool trovato = false;
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes[0].ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[0].ChildNodes[i].Name == testName)
                {
                    for (int x = 0; x < root1.ChildNodes[0].ChildNodes[i].Attributes.Count; x++)
                    {
                        if (root1.ChildNodes[0].ChildNodes[i].Attributes[x].Name == attributeName)
                        {
                            trovato = true;
                            root1.ChildNodes[0].ChildNodes[i].Attributes[x].Value = newAttributeValue;
                        }
                        if (root1.ChildNodes[0].ChildNodes[i].Attributes[x].Name == "time_end")
                        {
                            root1.ChildNodes[0].ChildNodes[i].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                        }
                    }
                }
            }
            xDocument.Save(xmlFilename);
            XmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
            if (trovato == false && AddIfNotExist == true)
                XmlAddTestAttribute(testName, attributeName, newAttributeValue);

        }

        /// <summary>
        /// Modifica un attributo del modulo presente sotto il ramo 'Modules'
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="attributeName"></param>
        /// <param name="newAttributeValue"></param>
        public void ReaderXmlEditModuleAttribute(string moduleName, string attributeName, string newAttributeValue,  bool AddIfNotExist = true)
        {
            bool trovato = false;
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(readerXmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            //for (int i = 0; i < root1.ChildNodes[0].ChildNodes.Count; i++)
            for (int i = root1.ChildNodes[0].ChildNodes.Count - 1; i >= 0; i--)
            {
                if (root1.ChildNodes[0].ChildNodes[i].Name == moduleName)
                {
                    ciclo:
                    for (int x = root1.ChildNodes[0].ChildNodes[i].Attributes.Count - 1; x >= 0 ;x--)
                    {
                        if (trovato == true)
                        {
                            if (root1.ChildNodes[0].ChildNodes[i].Attributes[x].Name == "time_end")
                            {
                                root1.ChildNodes[0].ChildNodes[i].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                                goto salva;
                            }
                        }
                        else
                        {
                            if (root1.ChildNodes[0].ChildNodes[i].Attributes[x].Name == attributeName)
                            {
                                trovato = true;
                                root1.ChildNodes[0].ChildNodes[i].Attributes[x].Value = newAttributeValue;
                                goto ciclo;
                            }
                        }
                    }
                }
            }
            salva:
            xDocument.Save(readerXmlFilename);
            //ReaderXmlEditRootAttribute("time_end", DateTime.Now.ToShortTimeString().ToString());
            if (trovato == false && AddIfNotExist == true)
                ReaderXmlAddModuleAttribute(moduleName, attributeName, newAttributeValue);

        }
        
        public void XmlAddTestAttribute(string testName, string attributeName, string newAttributeValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes[0].ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[0].ChildNodes[i].Name == testName)
                {
                    XmlAttribute attrib = xDocument.CreateAttribute(attributeName);
                    attrib.Value = newAttributeValue;
                    root1.ChildNodes[0].ChildNodes[i].Attributes.Append(attrib);
                    for (int x = 0; x < root1.ChildNodes[0].ChildNodes[i].Attributes.Count; x++)
                    {
                        if (root1.ChildNodes[0].ChildNodes[i].Attributes[x].Name == "time_end")
                        {
                            root1.ChildNodes[0].ChildNodes[i].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                        }
                    }
                }
            }
            xDocument.Save(xmlFilename);
        }
        public void ReaderXmlAddModuleAttribute(string moduleName, string attributeName, string newAttributeValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(readerXmlFilename);
            XmlNode root1 = xDocument.DocumentElement;
            for (int i = 0; i < root1.ChildNodes[0].ChildNodes.Count; i++)
            {
                if (root1.ChildNodes[0].ChildNodes[i].Name == moduleName)
                {
                    XmlAttribute attrib = xDocument.CreateAttribute(attributeName);
                    attrib.Value = newAttributeValue;
                    root1.ChildNodes[0].ChildNodes[i].Attributes.Append(attrib);
                    for (int x = 0; x < root1.ChildNodes[0].ChildNodes[i].Attributes.Count; x++)
                    {
                        if (root1.ChildNodes[0].ChildNodes[i].Attributes[x].Name == "time_end")
                        {
                            root1.ChildNodes[0].ChildNodes[i].Attributes[x].Value = DateTime.Now.ToLongTimeString();
                        }
                    }
                }
            }
            xDocument.Save(readerXmlFilename);
        }

        public void XmlEditRootAttribute(string attributeName, string newAttributeValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(xmlFilename);
            for (int i = 0; i < xDocument.DocumentElement.Attributes.Count; i++)
            {
                if (xDocument.DocumentElement.Attributes[i].Name == attributeName)
                    xDocument.DocumentElement.Attributes[i].Value = newAttributeValue;
                if (xDocument.DocumentElement.Attributes[i].Name == "time_end")
                    xDocument.DocumentElement.Attributes[i].Value = DateTime.Now.ToLongTimeString();
            }
            xDocument.Save(xmlFilename);
        }

        public void ReaderXmlEditRootAttribute(string attributeName, string newAttributeValue)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(readerXmlFilename);
            for (int i = 0; i < xDocument.DocumentElement.Attributes.Count; i++)
            {
                if (xDocument.DocumentElement.Attributes[i].Name == attributeName)
                {
                    xDocument.DocumentElement.Attributes[i].Value = newAttributeValue;
                }
                if (xDocument.DocumentElement.Attributes[i].Name == "time_end")
                {
                    xDocument.DocumentElement.Attributes[i].Value = DateTime.Now.ToLongTimeString();
                }
            }
            xDocument.Save(readerXmlFilename);
        }
    }

    public static class CONSTANTS
    {
        public const string status = "1B76";
        public const string printerIdendity = "1B49";
        public const string printerLineFeed = "0A";
        public const string printerCancelBuffer = "18";
        public const string printerPartialCut = "1B6D";
        public const string printerFullCut = "1B69";

        public const string PASS = "PASS";
        public const string FAIL = "FAIL";
        public const string ESC = "ESC";
        public const string ENTER = "ENTER";
        public const string OPEN = "OPEN";
        public const string CLOSED = "CLOSED";

        public const byte STX = 0x02;
        public const byte ETX = 0x03;
        public const byte EOT = 0x04;
        public const byte ACK = 0x06;
        public const byte NAK = 0x15;
    }
    public class CC3R
    {
        public const string LCSET = "#";
        const string LCGET = "$";
        

        public CC3R()
        {
            
        }

        public struct SPrinterCmd
        {
            
        }
        

        public struct SCC3R
        {
            public string productCode;
            public string productSerialNumber;
            public SSideKeypad sideKeypad;
            public SCoinHopper hopper;
        }
        public SCC3R System;

        public struct SSideKeypad
        {
            public string description;
            public string firwmare;
        }

        


        public string DconCommand(string cmd)
        {
            string command = "";
            string chk = "";
            int temp = 0;
            for (int i = 0; i < cmd.Length; i++)
            {
                command += CM18tool.Arca.Ascii2Hex(cmd.Substring(i, 1)).ToUpper();

            }
            for (int i = 0; i < command.Length; i = i + 2)
            {
                temp += Convert.ToInt16(CM18tool.Arca.Hex2Dec(command.Substring(i, 2)));
            }
            chk = CM18tool.Arca.Dec2Hex(temp);
            chk = CM18tool.Arca.Ascii2Hex(chk.Substring(chk.Length-2, 1)) + CM18tool.Arca.Ascii2Hex(chk.Substring(chk.Length - 1, 1));
            command += chk.Substring(chk.Length - 4, 4);
            command += "0D";
            return command;
        }
        public string DconCommand(string leadChr,int moduleAddress, string cmd)
        {
            cmd = leadChr + CM18tool.Arca.Dec2Hex(moduleAddress, 2) + cmd;
            string command = "";
            string chk = "";
            int temp = 0;
            for (int i = 0; i<cmd.Length;i++)
            {
                command += CM18tool.Arca.Ascii2Hex(cmd.Substring(i, 1)).ToUpper();

            }
            for (int i = 0; i<command.Length;i=i+2)
            {
                temp += Convert.ToInt16(CM18tool.Arca.Hex2Dec(command.Substring(i, 2)));
            }
            chk += CM18tool.Arca.Ascii2Hex(CM18tool.Arca.Dec2Hex(temp, 2).Substring(0, 1)) + CM18tool.Arca.Ascii2Hex(CM18tool.Arca.Dec2Hex(temp, 2).Substring(1, 1));
            command += chk.Substring(chk.Length-4,4);
            command += "0D";
            return command;
        }

        public struct SRemoteIOModule
        {
            public string module;
            public string address;
            public string firmware;
            public string commProt;
            public SRemotePort[] port;
        }
        public SRemoteIOModule remoteModule;

        public struct SRemotePort
        {
            public string description;
            public string value;
            public string status;
        }

        public struct SHopper
        {
            public int address;
            public string equipCategory;
            public string fwUpdateCapability;
            public string manufacturerId;
            public string denomination;
            public string diameter;
            public string productCode;
            public string serialNumber;
            public string softwareVersion;
            public int diameter1;
            public int diameter2;
            public int diameter3;
            public int diameter4;
            public int diameter5;
            public int diameter6;
        }

        public struct SCoinHopper
        {
            public SHopper[] dxRail;
            public SHopper[] sxRail;
        }

        public struct SDConAnswer
        {
            public string lead;
            public string moduleAddress;
            public string data;
            public string chk;
            public string cr;
        }
        public SDConAnswer DconAnswer(string answer)
        {
            SDConAnswer commandAnswer = new SDConAnswer();
            if (answer.Length > 0)
            {
                commandAnswer.lead = CM18tool.Arca.Hex2Ascii(answer.Substring(0, 2));
                commandAnswer.moduleAddress = CM18tool.Arca.Hex2Ascii(answer.Substring(2, 4));
                commandAnswer.data = CM18tool.Arca.Hex2Ascii(answer.Substring(6, answer.Length - 12));
                commandAnswer.chk = CM18tool.Arca.Hex2Ascii(answer.Substring(answer.Length - 6, 2)) + CM18tool.Arca.Hex2Ascii(answer.Substring(answer.Length - 4, 2));
                commandAnswer.cr = CM18tool.Arca.Hex2Ascii(answer.Substring(answer.Length - 2, 2));
            }
            return commandAnswer;
        }

        public struct DconProtocol
        {
            public string leadChr;
            public ModuleAddress module;
            public Command command;
            public string checksum;
            public byte endCommand;
        }

        public enum LeadingCharacter : int { cc,ssw}

        public struct ModuleAddress
        {

        }

        public struct Command
        {

        }

        public struct SCCTalkProtocol
        {
            public string destinationAddress;
            public int bytes;
            public string sourceAddress;
            public string header;
            public string checksum;
            public string data;
        }

        public struct SCCTalkAnswer
        {
            public string destinationAddress;
            public int bytes;
            public string sourceAddress;
            public string header;
            public string checksum;
            public string data;
            public string answer;
        }

        public SCCTalkAnswer CcTalkAnswer(string answer)
        {
            SCCTalkAnswer commandAnswer = new SCCTalkAnswer();
            if (answer.Length > 0)
            {
                try
                {
                    commandAnswer.destinationAddress = CM18tool.Arca.Hex2Ascii(answer.Substring(0, 2));
                    commandAnswer.bytes = Convert.ToInt16(answer.Substring(2, 2));
                    commandAnswer.sourceAddress = answer.Substring(4, 2);
                    commandAnswer.header = answer.Substring(6, 2);
                    commandAnswer.checksum = answer.Substring(8, 2);
                    commandAnswer.answer = answer.Substring(10, answer.Length - 10);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return commandAnswer;
        }
        public string CcTalkCommand(int destination, int bytes, int source, string cmd, string parameter="")
        {
            string[] command = new string[4];
            string chk = "";
            int temp = 0;
            command[0] = CM18tool.Arca.Dec2Hex(destination, 2);
            command[1] = CM18tool.Arca.Dec2Hex(bytes, 2);
            command[2] = CM18tool.Arca.Dec2Hex(source, 2);
            command[3] = cmd;
            for (int i = 0; i < command.Length; i++)
            {
                temp += Convert.ToInt16(CM18tool.Arca.Hex2Dec(command[i]));
            }
            temp = 256 - temp;
            chk=CM18tool.Arca.Dec2Hex(temp, 2);
            cmd = command[0] + command[1] + command[2] + command[3] + chk;
            return cmd;
        }

    }

    #region Eccezioni 
    public class CMConnectionTypeException: Exception
    {
        public CMConnectionTypeException()
        { }

        public CMConnectionTypeException(string message)
            :base (message)
        { }
    }
    #endregion
}



namespace CM18tool
{
    public class Arca
    {
        public static int keyCodePressed = 0;
        public static INI.IniFile iniMessage;
        public static Color messageColor = Color.FromArgb(255, 255, 255, 255); //0x00000000
        public static Color errorColor = Color.FromArgb(255, 255, 0, 0); //0x00FF0000
        public static Color warningColor = Color.FromArgb(255, 255, 184, 0);//0x00FFB800
        public static Color actionColor = Color.FromArgb(255, 0, 0, 255);//0x000000FF
        public static Color correctColor = Color.FromArgb(255, 154, 205, 50);//0x009ACD32
        public static Color waitingColor = Color.FromArgb(255, 154, 205, 50);//0x009ACD32
        public static Color arcaBlackColor = Color.FromArgb(255, 88, 89, 91);
        public static Color arcaLogoColor = Color.FromArgb(255, 85, 22, 110);
        public static Color arcaOemColor = Color.FromArgb(255, 174, 191, 55);
        public static Color arcaRetailColor = Color.FromArgb(255, 228, 86, 32);
        public static Color arcaFinancialColor = Color.FromArgb(255, 26, 50, 129);

        public Arca()
        {
            //public static STower myTower = new STower();
        }
        

        public static System.Drawing.Color ArcaColors(uint argbValue)
        {
            return System.Drawing.Color.FromArgb(Convert.ToInt16(argbValue));
        }

        public struct SSingleTest
        {
            public string description;
            public string result;
            public string logID;
            public string functionName;
            public int testID;
        }

        public struct SMyTeller
        {
            public string partNumber;
            public string description;
            public string monitorModel;
            public string cardreaderModel;
            public string printerModel;
            public string printerA4Model;
            public string EPPModel;
            public string scannerModel;
            public string scannerA4Model;
            public string webcamModel;
            public string barcodereaderModel;
            public string NFCModel;
            public string AudioTest;
            public string UPSModel;
            public string CMModel;
            public string coinModel;
            public string serialNumber;
        }

        public struct SLowerModule
        {
            public string productNumber;
            public string clientName;
            public int clientID;
            public SCd80[] cd80;
            public string fwCd80Result;
            public string cd80TestResult;
            public string suiteCode;
            public string startData;
            public string startTime;
            public string endData;
            public string endTime;
            public string assignResult;
            public string photoAdjResult;
            public string cassetteNumberResult;
            public int cassetteNumber;
            public string cassetteSetup;
            public string depositResult;
            public string testResult;
            public string catDescription;
            public string category;
            public int crmNum;
            public int cd80Num;
            public SCd80Status cd80Status;
            public SCassette[] cassettes;
        } 

        public struct SCd80
        {
            public string serialNumber;
            public string fw;
            public SPhoto[] photo;
            public string testResult;
            public string capacity;

        }
        public struct SCd80Status
        {
            public bool swLockA;
            public bool swLockB;
            public bool tray;
            public string statusA;
            public string statusB;
            public bool swMagA;
            public bool swMagB;
            public bool sorterForkA;
            public bool sorterForkB;
        }
        public struct ConnectionParameter
        {
            public string connection;
            public string simplified;
            public string Name;
            public int baudrate;
            public StopBits stopBit;
            public Parity parity;
            public int dataBits;
            public string protocol;
            public string ipAddress;
            public string hostName;
            public int port;
        }
        public ConnectionParameter myConnectionPar;

        public struct SSystem
        {
            public string partNumber;
            public string clientName;
            public string clientID;
            public string family;
            public string description;
            public string serialNumber;
            public string protInterface;
            public int protCassetteNumber;
            public string suiteCode;
            public int CRM;
            public int cassetteNumber;
            public int cassetteInstalled;
            public int cd80Number;
            public int cd80TrayNumber;
            public SUnitConfig cmConfig;
            public SUnitConfig cmOption;
            public SUnitConfig cmOption1;
            public SUnitConfig cmOption2;
            public SCassette[] cassettes;
            public SCashData cashData;
            public SBag bag;
            public SReader reader;
            public SVersion version;
            public SRealTimePhoto realtimePhoto;
            public SErrorLog[] errorLog;
        }
        public SSystem mySystem;
            
        public struct SJamErrorLog
        {
            public List<SJamDetail> jamDetail;
            public int totalErrors;
            public int feedErrors;
            public int controllerErrors;
            public int readerErrors;
            public int safeErrors;
            public int cassetteAErrors;
            public int cassetteBErrors;
            public int cassetteCErrors;
            public int cassetteDErrors;
            public int cassetteEErrors;
            public int cassetteFErrors;
            public int cassetteGErrors;
            public int cassetteHErrors;
            public int cassetteIErrors;
            public int cassetteJErrors;
            public int cassetteKErrors;
            public int cassetteLErrors;
        }

        public struct SJamDetail
        {
            public int line;
            public string moduleStatus;
            public string module;
        }
        public struct SCashData
        {
            public Int32 bnInCash;
            public SCassette[] cassette;
        }

        public struct SCassette
        {
            public string noteId;
            public Int32 noteNumber;
            public Int32 freeCapacity;
            public string name;
            public string enabled;
            public string serial;
            public SFirmware fw;
            public SPhoto[] photo;
        }

        public struct SErrorLog
        {
            public Int32 recordNumber;
            public Int32 life;
            public string opSide;
            public string opId;
            public string replyCode;
            public string F1Status;
            public string F2Status;
            public string F3Status;
            public string F4Status;
            public string FAStatus;
            public string FBStatus;
            public string FCStatus;
            public string FDStatus;
            public string FEStatus;
            public string FFStatus;
            public string FGStatus;
            public string FHStatus;
            public string FIStatus;
            public string FJStatus;
            public string FKStatus;
            public string FLStatus;
            public string FMStatus;
            public string FNStatus;
            public string FOStatus;
            public string FPStatus;
            public string line;
        }
        public struct SRealTimePhoto
        {
            public SPhoto inCenter;
            public SPhoto inLeft;
            public SPhoto hMax;
            public SPhoto feed;
            public SPhoto c1;
            public SPhoto shift;
            public SPhoto inq;
            public SPhoto count;
            public SPhoto Out;
            public SPhoto c3;
            public SPhoto c4a;
            public SPhoto c4b;
            public SPhoto rej;
            public SPhoto inBox;
            public SPhoto res1;
            public SPhoto res2;
            public SPhoto res3;
            public SPhoto res4;
            public SPhoto res5;
            public SPhoto res6;
            public SPhoto res7;
            public SPhoto cashLeft;
            public SPhoto cashRight;
            public SPhoto thick;
            public SPhoto finrcyc;
            public SPhoto res10;
            public SPhoto res11;
            public SPhoto res12;
            public SPhoto res13;
            public SPhoto res14;
            public SPhoto res15;
            public SPhoto res16;
            public SPhoto res17;
            public SPhoto res18;
            public SPhoto res19;
            public SPhoto res20;
            public SPhoto res21;
            public SPhoto res22;
            public SPhoto res23;
            public SPhoto res24;
            public SPhoto res25;
        }
        public struct SVersion
        {
            public SFirmware compatibility;
            public SFirmware oscBoot;
            public SFirmware oscWce;
            public SFirmware oscxDll;
            public SFirmware oscxApp;
            public SFirmware fpga;
            public SFirmware Controller;
            public SFirmware Controller2;
            public SFirmware readerHost;
            public SFirmware readerDsp;
            public SFirmware readerFpga;
            public SFirmware readerTape;
            public SFirmware readerMagnetic;
            public SFirmware safe;
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
            public SFirmware bagContrA;
            public SFirmware bagSensorA;


        }

        public struct SFirmware
        {
            public string name;
            public string id;
            public string partNumber;
            public string release;
            public string version;
        }

        public struct SReader
        {
            public string serial;
            public string family;
            public string fwCode;
            public string cdfCode;
            public string cdfName;
            public string currencyMode;
            public SBankCfg[] bankCfg;
            public SCurrency[] currency;
        }

        public struct SBankCfg
        {
            public string name;
            public string enabled;
            public string bankId;
        }
        public struct SCurrency
        {
            public string name;
            public string enabled;
            public string bankId;
            public string version;
        }
        public struct SBag
        {
            public string name;
            public string fwVer;
            public string fwRes;
            public string serial;
            public string denomination;
        }

        public struct SUnitConfig
        {
            public SBitUnitConfig[] bit;
        }
        public SUnitConfig CMConfig;

        public struct SBitUnitConfig
        {
            public string address;
            public string description;
            public bool value;
        }
        public struct SHCLow
        {
            public string moduleInError;
            public string moduleError;
            public string towerInTest;
            //public string towerSN;
            //public string towerTL;
            public SPhoto phInA;
            public SPhoto phInB;
            public STower[] tower;
        }
        public SHCLow[] myHcLow;

        public struct STower
        {
            public string status;
            public int idTower;
            public string fwRel;
            public string fwVer;
            public string name;
            public string serialNumber;
            public string snBoard;
            public string mechanicalLevel;
            public string electronicLevel;
            public string technicalLevel;
            public int motorCurrent;
            public string type;
            public SElm sorter;
            public SDrum[] drum;
            public SPhoto phTr1;

        }
        public STower myTower;

        public struct SDrum
        {
            public string status;
            public string name;
            public string address;
            public string denomination;
            public int bnHeight;
            public int bnNumber;
            public string fwVer;
            public string fwRel;
            public string serialNumber;
            public string snHigh;
            public string snMid;
            public string snLow;
            public string snBoard;
            public string mechanicalLevel;
            public string electronicLevel;
            public string technicalLevel;
            public int motorCurrent;
            public int windTapeLenght;
            public int unwindTapeLenght;
            public int tapeLenghtDiff;
            public SElm pinchRoller;
            public SElm sorter;
            public SPhoto phIn;
            public SPhoto phEmpty;
            public SPhoto phFull;
            public SPhoto phFill;
        }
        public SDrum myDrum;

        public struct SSafe
        {
            public string fwRel;
            public string fwVer;
            public string serialNumber;
            public string snBoard;
            public string state;
            public string doorSim;
            public string swDoorMec;
            public string swDoorEl;
            public string mechanicalLevel;
            public string electronicLevel;
            public int motorCurrent;
            public SElm sorter;
            public SPhoto phInA;
            public SPhoto phInB;
            public SPhoto phTr2;
            public SPhoto phTr3;
        }

        public struct SPhoto
        {
            public string name;
            public string idPar;
            public int value;
            public int adjMin;
            public int adjMax;
            public string adjResult;
        }
        public SPhoto myPhoto;

        public struct SElm
        {
            public string name;
            public string idPar;
            public int openingTime;
            public int closingTime;
            public int currentValue;
        }
        public SElm myElm;

        #region HEX / BIN / DEC convertions

        public static string Base64Encode(string value, Base64FormattingOptions lineBreaks = Base64FormattingOptions.InsertLineBreaks)
        {
            string result = "";
            byte[] valueBytes = new byte[value.Length];
            valueBytes = Arca.Hex2ByteArray(value);
            result = Convert.ToBase64String(valueBytes, lineBreaks);
            return result;
        }

        
        public static string ToBase64String(byte[] value, bool reverseArray, Base64FormattingOptions lineBreaks = Base64FormattingOptions.InsertLineBreaks)
        {
            string result = "";
            if (reverseArray == true)
                value = ReverseByteArray(value);
            result = Convert.ToBase64String(value, lineBreaks);
            return result;
        }

        public static string Hex2Base64Binary (string value)
        {
            string result = "";
            byte[] valueBytes = Hex2ByteArray(value);
            result = Convert.ToBase64String(valueBytes,Base64FormattingOptions.InsertLineBreaks);

            return result;
        }

        /// <summary>
        /// Convert a HEX value to a DEC value
        /// </summary>
        /// <param name="value">HEX value</param>
        /// <returns></returns>
        public static int Hex2Dec(string value)
        {
            int result = 0;
            try
            {
                if (value.Length == 0)
                    value = "0";
                value = value.ToUpper();
                result = Convert.ToInt32(value, 16);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return result;
        }

        public static UInt32 Hex2Uint(string value)
        {
            value = value.ToUpper();
            uint result = Convert.ToUInt32(value, 16);// 0;
            return result;

        }
        public static double Hex2Dbl(string value)
        {
            value = value.ToUpper();
            long bits = Convert.ToInt64(value, 16);
            double result = BitConverter.Int64BitsToDouble(bits);

            return result;
        }
        /// <summary>
        /// Convert a HEX value to a BIN value
        /// </summary>
        /// <param name="value">HEX value</param>
        /// <param name="bit">Number of bit (considered only if highter of number of bit calculated)</param>
        /// <returns></returns>
        public static string Hex2Bin(string value,int bit=0)
        {
            value = value.ToUpper();
            string result = "";
            string temp = "";
            if (value != "NO ANSWER")
            {
                for (int i = 1; i <= value.Length; i++)
                {
                    temp = value.Substring(i - 1, 1);
                    switch (temp)
                    {
                        case "0":
                            temp = "0000";
                            break;
                        case "1":
                            temp = "0001";
                            break;
                        case "2":
                            temp = "0010";
                            break;
                        case "3":
                            temp = "0011";
                            break;
                        case "4":
                            temp = "0100";
                            break;
                        case "5":
                            temp = "0101";
                            break;
                        case "6":
                            temp = "0110";
                            break;
                        case "7":
                            temp = "0111";
                            break;
                        case "8":
                            temp = "1000";
                            break;
                        case "9":
                            temp = "1001";
                            break;
                        case "A":
                            temp = "1010";
                            break;
                        case "B":
                            temp = "1011";
                            break;
                        case "C":
                            temp = "1100";
                            break;
                        case "D":
                            temp = "1101";
                            break;
                        case "E":
                            temp = "1110";
                            break;
                        case "F":
                            temp = "1111";
                            break;
                    }
                    result += temp;
                }

                if (bit > result.Length)
                {
                    for (int i = result.Length; i < bit; i++)
                    {
                        result = "0" + result;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Convert DEC number (int) to HEX value
        /// </summary>
        /// <param name="value">DEC (int) number</param>
        /// <param name="bit">Number of bit (considered only if highter of number of bit calculated)</param>
        /// <returns></returns>
        public static string Dec2Hex(int value, int bit = 0)
        {
            return value.ToString("X" + bit);
        }

        /// <summary>
        /// Convert DEC number (double) to HEX value
        /// </summary>
        /// <param name="value">DEC (double) number</param>
        /// <param name="bit">Number of bit (considered only if highter of number of bit calculated)</param>
        /// <returns></returns>
        public static string Uint2Hex(UInt32 value, int bit = 0)
        {
            string result = value.ToString("X" + bit);
            return result;

        }
        /// <summary>
        /// Convert DEC number (int) to HEX value
        /// </summary>
        /// <param name="value">DEC 8int) number</param>
        /// <param name="bit">Number of bit (considered only if highter of number of bit calculated)</param>
        /// <returns></returns>
        public static string Dec2Bin (int value, int bit = 0)
        {
            string result = Convert.ToString(value,2) ;
            if (bit > result.Length)
            {
                for (int i = result.Length;i<bit;i++)
                {
                    result = "0" + result;
                }
            }
            return result;
        }

        struct SCalcoliStatistici
        {
            double somma;
            double mediaAritmetica;
            double sigma;
        }



        public static string ResulRc (string rc)
        {
            if (rc == "101" || rc =="1")
            {
                return "PASS";
            }
            return rc;
        }

        public static bool AsciiDec2Bool (string value)
        {
            if (value == "1")
                return true;
            return false;
        }

        /// <summary>
        /// Convert BIN value to a DEC number (int)
        /// </summary>
        /// <param name="value">BIN value</param>
        /// <returns></returns>
        public static int Bin2Dec (string value)
        {
            int result = 0;
            int temp = 0;

            for (int i = 0; i < value.Length; i++)
            {
                temp = Convert.ToInt16(value.Substring(value.Length - (i+1), 1));
                if (temp == 1)
                {
                    temp = Convert.ToInt16(Math.Pow(2, i));
                }
                result += temp;
            }
            
            return result;
        }

        public static short Bin2Short(string value)
        {
            int result = 0;
            int temp = 0;

            for (int i = 0; i < value.Length; i++)
            {
                temp = Convert.ToInt16(value.Substring(value.Length - (i + 1), 1));
                if (temp == 1)
                {
                    temp = Convert.ToInt16(Math.Pow(2, i));
                }
                result += temp;
            }

            return (short)result;
        }

        public static byte[] Bin2ByteArray (string value)
        {
            inizio:
            int resto = value.Length % 4;
            if (resto != 0)
            {
                value = value.PadLeft(value.Length + resto, '0');
                goto inizio;
            }
            byte[] answer = new byte[value.Length / 8];
            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = (byte)Bin2Dec(value.Substring(i * 8, 8));
            }
            return answer;
        }

        public static byte Bin2Byte(string value)
        {
            inizio:
            int resto = value.Length % 4;
            if (resto != 0)
            {
                value = value.PadLeft(value.Length + resto, '0');
                goto inizio;
            }
            byte answer;
            if (value.Length < 8)
                answer = (byte)Bin2Dec(value.Substring(0));
            else
                answer = (byte)Bin2Dec(value.Substring(0, 8));
            return answer;
        }

        public static string Bin2Hex (string value, int bit = 0)
        {
            int temp = Bin2Dec(value);
            string result = Dec2Hex(temp, bit);
            return result;
        }
        
        public static int Hex2AsciiDec(string value)
        {
            int result = 0;
            if (value != "NO ANSWER")
            {
                string temp = string.Empty;
                for (int i = 0; i < value.Length; i += 2)
                {
                    string char2Convert = value.Substring(i, 2);
                    int n = Convert.ToInt32(char2Convert, 16);
                    char c = (char)n;
                    temp += c.ToString();
                }
                result = Convert.ToInt16(temp);
            }
            return result;
        }

        public static byte[] Ascii2ByteArray(string value)
        {
            byte[] result = new byte[value.Length];
            result = Encoding.UTF8.GetBytes(value);
            //for (int i = 0; i < value.Length; i++)
            //    result[i] = Convert.ToByte(Arca.Ascii2Hex(value.Substring(i, 1)));
            return result;
        }


        public static byte[] Hex2ByteArray(string value)
        {
            byte[] result = new byte[value.Length/2];

            for (int i = 0; i < value.Length; i += 2)
                result[i/2]=Convert.ToByte(Arca.Hex2Dec(value.Substring(i, 2)));
            return result;
        }

        public static short[] Hex2ShortArray(string value)
        {
            short[] result = new short[value.Length / 4];

            for (int i = 0; i < value.Length; i += 4)
                result[i / 4] = Convert.ToInt16(Arca.Hex2Dec(value.Substring(i, 4)));
            return result;
        }

        public static ushort[] Hex2UshortArray(string value)
        {
            ushort[] result = new ushort[value.Length / 4];

            for (int i = 0; i < value.Length; i += 4)
                result[i / 4] = Convert.ToUInt16(Arca.Hex2Dec(value.Substring(i, 4)));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="byteOrder">0=little-endian / 1=big-endian</param>
        /// <returns></returns>
        public static double ByteArray2Double (byte[] source, int byteOrder = 0)
        {
            double answer = 0;

            switch (byteOrder)
            {
                case 0://little endian
                    for (int i = 0; i < source.Count(); i++)
                        answer += source[i] * Math.Pow(256, i);
                    return answer;
                case 1: // big endian
                    for (int i = 0; i < source.Count(); i++)
                    {
                        answer += source[i] * Math.Pow(256, (source.Count() - 1) - i);
                    }
                    return answer;
                default:
                    return 0;
            }
        }

        public static string Float2Binary (float value) //, int nBit = 10
        {
            //float test = value * 256;
            string result = Arca.Dec2Bin((int)value, 10);
            
            return result;
        }

        public static byte[] Float2ByteArray (float value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] Short2ByteArray(short value, int nByte = 0, bool BigEndian = true)
        {
            byte[] result = BitConverter.GetBytes(value);
            if (BigEndian == true)
                result = result.Reverse().ToArray();
            if (nByte > 0)
            {
                byte[] temp = new byte[nByte];
                int id = 0;
                foreach (byte myValue in result)
                {
                    temp[id++] = myValue;
                }
                return temp;
            }
            return result;
        }

        public static byte[] Int2ByteArray(int value, int nByte = 0, bool BigEndian = true)
        {
            byte[] result = BitConverter.GetBytes(value);
            if (BigEndian == true)
                result = result.Reverse().ToArray();
            if (nByte > 0)
            {
                byte[] temp = new byte[nByte];
                for (int id = 0; id < nByte; id++)
                    temp[id] = result[id];
                //foreach (byte myValue in result)
                //{
                //    temp[id++] = myValue;
                //}
                return temp;
            }
            return result;
        }

        public static byte[] Double2ByteArray(double value, int nByte = 0)
        {
            byte[] result = BitConverter.GetBytes(value);
            if (nByte > 0)
            {
                byte[] temp = new byte[nByte];
                for (int i = 0; i < nByte; i++)
                    temp[i] = result[i];
                //int id = 0;
                //foreach (byte myValue in result)
                //{
                //    temp[id++] = myValue;
                //}
                return temp;
            }
            return result;
        }

        public static byte[] FloatArray2ReversedByteArray(float[] values)
        {
            byte[] singleFloatInByte;
            byte[] answer = new byte[values.Length * 4];
            for (int i = 0; i < values.Length; i++)
            {
                singleFloatInByte = Float2ByteArray(values[i]);
                Array.Copy(singleFloatInByte.Reverse().ToArray(), 0, answer, i * 4, 4);
            }
            return answer;
        }

        public static byte[] FloatArray2ByteArray ( float[] value)
        {
            byte[] singleFloatInByte;
            byte[] answer = new byte[value.Length * 4];
            for (int i = 0; i < value.Length; i++)
            {
                singleFloatInByte = Float2ByteArray(value[i]);
                Array.Copy(singleFloatInByte, 0, answer, i * 4, 4);
            }
            return answer;
        }

        public static byte[] ListByteArray2ByteArray(List<byte[]> value)
        {
            byte[] answer;
            int dim = 0;
            int indice = 0;
            for (int i = 0; i < value.Count; i++)
            {
                dim += value[i].Count();
            }
            answer = new byte[dim];
            for (int i = 0; i < value.Count; i++)
            {
                Array.Copy(value[i], 0, answer, indice, value[i].Length);
                indice += value[i].Length;
            }

            return answer; 
        }

        public static string ByteArray2Ascii(byte[] source, int offset = 0, int length = 0)
        {
            string answer = "";
            if (length == 0)
                length = source.Length - offset;
            for (int i = offset; i < length + offset; i++)
                //if (source[i] >= 32)
                    answer += Arca.Hex2Ascii(Arca.Dec2Hex(source[i],2));

            return answer;
        }

        public static string ByteArray2Bin(byte[] source, int offset = 0, int lenght = 0)
        {
            string answer = "";
            if (lenght == 0)
                lenght = source.Count();
            else
                lenght = lenght + offset;
            for (int i = offset; i < lenght; i++)
                answer += Arca.Hex2Bin(Arca.Dec2Hex(source[i], 2));

            return answer;
        }

        public static string ByteArray2Bin(byte[] source)
        {
            string result = "";
            for (int i = 0; i < source.Length; i++)
                result = Convert.ToString(source[i], 2).PadLeft(8, '0');
            return result;
        }
        public static string Byte2Bin (byte source)
        {
            return Convert.ToString(source,2).PadLeft(8, '0');
        }

        public static int ByteArray2Int(byte[] source, int offset, int length, int byteOrder = 0)
        {
            int answer = 0;

            switch (byteOrder)
            {
                case 0://little endian
                    for (int i = offset; i < offset + length; i++)
                        answer += source[i] * (int)Math.Pow(256, i-offset);
                    return answer;
                case 1: // big endian
                    for (int i = offset; i < offset + length; i++)
                    {
                        answer += source[i] * (int)Math.Pow(256, (length - 1) - (i - offset));
                    }
                    return answer;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="byteOrder">0=LITTLE endian / 1=BIG endian</param>
        /// <returns></returns>
        public static short ByteArray2Short(byte[] source, int offset, int length, int byteOrder = 0)
        {
            short answer = 0;
            
            switch (byteOrder)
            {
                case 0://little endian
                    for (int i = offset; i < offset + length; i++)
                        answer += (short)(source[i] * (short)Math.Pow(256, i - offset));
                    return answer;
                case 1: // big endian
                    for (int i = offset; i < offset + length; i++)
                    {
                        answer += (short)(source[i] * (short)Math.Pow(256, (length - 1) - (i - offset)));
                    }
                    return answer;
                default:
                    return 0;
            }
        }

        public static short[] ByteArray2ShortArray(byte[] source, int byteOrder = 0)
        {
            short[] answer = new short[source.Length/2];
            //Array.Reverse(source);
            //Buffer.BlockCopy(source, 0, answer, 0, source.Length);
            short tmpAnswer = 0;
            switch (byteOrder)
            {
                case 0://little endian
                    for (int i = 0; i < source.Length; i += 2)
                    {
                        tmpAnswer =  (short)(source[i] * 256  + source[i + 1]);
                        answer[i / 2] = tmpAnswer;
                    }
                    return answer;
                case 1: // big endian
                    for (int i = 0; i < answer.Length; i++)
                    {
                        tmpAnswer += (short)(source[i] * (short)Math.Pow(256, (answer.Length - 1) - (i)));
                    }
                    return answer;
                default:
                    return answer;
            }
        }

        public static ushort ByteArray2UShort(byte[] source, int offset, int length, int byteOrder = 0)
        {
            ushort answer = 0;

            switch (byteOrder)
            {
                case 0://little endian
                    for (int i = offset; i < offset + length; i++)
                        answer += (ushort)(source[i] * (ushort)Math.Pow(256, i - offset));
                    return answer;
                case 1: // big endian
                    for (int i = offset; i < offset + length; i++)
                    {
                        answer += (ushort)(source[i] * (ushort)Math.Pow(256, (length - 1) - (i - offset)));
                    }
                    return answer;
                default:
                    return 0;
            }
        }

        public static string HexArray2HexString(byte[] value)
        {
            string result = "";
            for (int i = 0; i < value.Length; i++)
                result += value[i].ToString("X2");
            return result;
        }

        

        public static string HexArray2Ascii(byte[] value)
        {
            string result = "";
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] > 0x19)
                    result += Arca.Hex2Ascii(value[i].ToString("X2"));
            }
            return result;
        }

        public static string Hex2Ascii(string value)
        {
            string result = string.Empty;
            if (value != "NO ANSWER")
            {
                for (int i = 0; i < value.Length; i += 2)
                {
                    string char2Convert = value.Substring(i, 2);
                    int n = Convert.ToInt32(char2Convert, 16);
                    char c = (char)n;
                    result += c.ToString();
                }
            }
            return result;
        }
        
        public static string Ascii2Hex (string value)
        {
            StringBuilder sb = new StringBuilder();
            byte[] inputBytes = Encoding.UTF8.GetBytes(value);
            foreach(byte b in inputBytes)
            {
                sb.Append(string.Format("{0:X2}",b));
            }
            return sb.ToString();
        }

        public static float SigmaCalculator(List<byte> values)
        {
            float result = 100;
            float sum = 0;
            float sum2 = 0;
            float mean = 0;
            for (int i = 0; i < values.Count; i++)
            {
                sum += values[i];
            }
            mean = sum / values.Count;

            for (int i = 0; i < values.Count; i++)
            {
                sum2 += (float)Math.Pow(values[i] - mean, 2);
            }
            result = (float)Math.Sqrt(sum2 / values.Count);
            return result;
        }

        public static float SigmaCalculator(byte[] values)
        {
            float result = 100;
            float sum = 0;
            float sum2 = 0;
            float mean = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            mean = sum / values.Length;

            for (int i = 0; i < values.Length; i++)
            {
                sum2 += (float)Math.Pow(values[i] - mean, 2);
            }
            result = (float)Math.Sqrt(sum2 / values.Length);
            return result;
        }

        public static double SigmaCalculator(double[] values)
        {
            double result = 100;
            double sum = 0;
            double sum2 = 0;
            double mean = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            mean = sum / values.Length;

            for (int i = 0; i < values.Length; i++)
            {
                sum2 += Math.Pow(values[i] - mean, 2);
            }
            result = Math.Sqrt(sum2 / values.Length);
            return result;
        }

        public static float SigmaCalculator(int[] values)
        {
            float result = 100;
            float sum = 0;
            float sum2 = 0;
            float mean = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            mean = sum / values.Length;

            for (int i = 0; i < values.Length; i++)
            {
                sum2 += (float)Math.Pow(values[i] - mean, 2);
            }
            result = (float)Math.Sqrt(sum2 / values.Length);
            return result;
        }

        public static float SigmaCalculator(float[] values)
        {
            float result = 100;
            float sum = 0;
            float sum2 = 0;
            float mean = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            mean = sum / values.Length;

            for (int i = 0; i < values.Length; i++)
            {
                sum2 += (float)Math.Pow(values[i] - mean, 2);
            }
            result = (float)Math.Sqrt(sum2 / values.Length);
            return result;
        }

        #endregion

        
        public static byte[] ReverseFloatToByteArray(float value)
        {
            byte[] temp = Float2ByteArray(value);
            return temp.Reverse().ToArray();
        }

        public static byte[,][] ReverseByteArray(byte[,][] source)
        {
            int dimA = source.GetLength(0);
            int dimB = source.GetLength(1);
            byte[,][] reversedArray = new byte[dimA, dimB][];
            for (int a = 0; a < dimA; a++)
                for (int b = 0; b < dimB; b++)
                {
                    if (source[a, b] != null)
                        reversedArray[a, b] = source[a, b].Reverse().ToArray();
                }
            return reversedArray;
        }
        public static byte[][] ReverseByteArray(byte[][] source)
        {
            int dimA = source.GetLength(0);
            byte[][] reversedArray = new byte[dimA][];
            for (int a = 0; a < dimA; a++)
                if (source[a] != null)
                    reversedArray[a] = source[a].Reverse().ToArray();
            return reversedArray;
        }

        public static byte[] ReverseByteArray(byte[] source)
        {
            return source.Reverse().ToArray();
        }
        public static float[,][] ReverseByteArray(float[,][] source)
        {
            int dimA = source.GetLength(0);
            int dimB = source.GetLength(1);
            float[,][] reversedArray = new float[dimA, dimB][];
            for (int a = 0; a < dimA; a++)
                for (int b = 0; b < dimB; b++)
                {
                    if (source[a, b] != null)
                        reversedArray[a, b] = source[a, b].Reverse().ToArray();
                }
            return reversedArray;
        }
        public static float[][] ReverseByteArray(float[][] source)
        {
            int dimA = source.GetLength(0);
            float[][] reversedArray = new float[dimA][];
            for (int a = 0; a < dimA; a++)
                if (source[a] != null)
                    reversedArray[a] = source[a].Reverse().ToArray();
            return reversedArray;
        }

        public static byte[] SingleParametersToArray(params object[] par)
        {
            int dimension = 0;

            foreach (object obj in par)
            {

                if (obj is bool)
                    dimension++;
                if (obj is short || obj is ushort)
                    dimension += 2;
                if (obj is int || obj is uint)
                    dimension += 4;
                if (obj is byte)
                    dimension++;
                if (obj is byte[] bArrayTemp)
                    dimension += bArrayTemp.Length;
                if (obj is string sTemp)
                    dimension += sTemp.Length;
            }
            byte[] parameters = new byte[dimension];
            int index = 0;
            foreach (object obj in par)
            {
                if (obj is bool)
                {
                    byte b = 0x00;
                    if ((bool)obj == true)
                        b = 0x01;
                    parameters[index] = b;
                    index++;
                }
                if (obj is short)
                {
                    byte[] b = Arca.Short2ByteArray((short)obj);
                    for (int i = 0; i < b.Length; i++)
                    {
                        parameters[index] = b[i];
                        index++;
                    }
                }
                if (obj is ushort)
                {
                    byte[] b = Arca.Short2ByteArray((short)((ushort)obj));
                    for (int i = 0; i < b.Length; i++)
                    {
                        parameters[index] = b[i];
                        index++;
                    }
                }
                if (obj is int)
                {
                    byte[] b = Arca.Int2ByteArray((int)obj);
                    for (int i = 0; i < b.Length; i++)
                    {
                        parameters[index] = b[i];
                        index++;
                    }
                }
                if (obj is uint)
                {
                    byte[] b = Arca.Int2ByteArray((int)((uint)obj));
                    for (int i = 0; i < b.Length; i++)
                    {
                        parameters[index] = b[i];
                        index++;
                    }
                }
                if (obj is byte)
                {
                    parameters[index] = (byte)obj;
                    index++;
                }
                if (obj is byte[])
                {
                    byte[] b = (byte[])obj;
                    for (int i = 0; i < b.Length; i++)
                    {
                        parameters[index] = b[i];
                        index++;
                    }
                }
                if (obj is string)
                {
                    byte[] b = Arca.Ascii2ByteArray((string)obj);
                    for (int i = 0; i < b.Length; i++)
                    {
                        parameters[index] = b[i];
                        index++;
                    }
                }
            }
            return parameters;
        }

        public static float[] ReverseByteArray(float[] source)
        {
            return source.Reverse().ToArray();
        }

        public static string WaitingKey(string specificKey = "")
        {
            System.Windows.Forms.Application.DoEvents();
            keyCodePressed = 0;
            bool pressed = false;
            int specificValue = 1000;
            string key = "";
            switch (specificKey)
            {
                case "ENTER":
                case "INVIO":
                    specificValue = 13;
                    break;
                case "ESC":
                    specificValue = 27;
                    break;
                default:
                    if (specificKey.Length>0)
                    specificValue = Convert.ToChar(specificKey);
                    break;
            }
            do
            {
                System.Windows.Forms.Application.DoEvents();
                if (specificValue == 1000 && keyCodePressed != 0)
                { pressed = true; }
                if (specificValue == keyCodePressed)
                { pressed = true; }
            } while (pressed == false);

            switch (keyCodePressed)
            {
                case 13:
                    key = "ENTER";
                    break;
                case 27:
                    key = "ESC";
                    break;
                case 32:
                    key = "SPACE";
                    break;
                case 82:
                case 114:
                    key = "R";
                    break;
                case 76:
                case 108:
                    key = "L";
                    break;
                default:
                    key = Convert.ToChar(keyCodePressed).ToString();
                    break;
            }
            keyCodePressed = 0;
            return key;
        }

        public static void WriteMessage(System.Windows.Forms.Label label,string message, Color Colors = default(Color))
        {
            label.Text = message;
            //if (Colors == default(Color)) { Colors=Color.Green; }
            label.ForeColor = Colors;
            label.Refresh();
        }

        public static void WriteMessage(System.Windows.Forms.Label label, int messageID, Color Colors = default(Color))
        {
            label.Text = iniMessage.GetValue("messages", "msg" + messageID);
            if (Colors == default(Color)) { Colors = Color.White; }
            label.ForeColor = Colors;
            label.Refresh();
        }

        public static void WriteMessage(System.Windows.Forms.Label label, int messageID, Color Colors = default(Color), params object[] param)
        {
            label.Text = string.Format(iniMessage.GetValue("messages", "msg" + messageID),param);
            //if (Colors == default(Color)) { Colors = Color.Green; }
            label.ForeColor = Colors;
            label.Refresh();
        }
        

        public static void WaitingTime(int mSec = 1000)
        {
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(mSec);
        }

        public static void LogWrite(string fileName, string logBlock, bool endLog = false)
        {
            StreamWriter logFile = File.AppendText(fileName);
            if (endLog==true)
            {
                logFile.WriteLine(logBlock);
            }
            else
            {
                logFile.Write(logBlock);
            }
            logFile.Close();
        }

        public static void SendErrorLogs()
        {

        }
        public static void SendLog(string localFile, string serverFolder)
        {
            if (File.Exists(localFile))
            {
                if (serverFolder.Substring(serverFolder.Length - 1) != "\\") { serverFolder += "\\"; }
                try
                {
                    StreamReader srLocalFile = new StreamReader(localFile);
                    StreamWriter serverFile = new StreamWriter(serverFolder + localFile, append: true);
                    serverFile.Write(srLocalFile.ReadToEnd());
                    serverFile.Close();
                    srLocalFile.Close();
                    File.Delete(localFile);
                    //trasferimento file singoli
                    string[] files = Directory.GetFiles(".\\", "*.txt", SearchOption.TopDirectoryOnly);

                    foreach (string s in files)
                    {
                        if (!File.Exists(serverFolder + s))
                            File.Delete(serverFolder + s);
                        File.Move(s, serverFolder + s);
                    }
                }
                catch (System.IO.IOException e)
                {
                    string answer = e.Message;
                }
            }
        }
    }
}

