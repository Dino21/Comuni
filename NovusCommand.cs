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
    public class NovusCommand
    {

        // CONSTANTS
        const float LIBRARY_VERSION_1_0 = 1;

        const int STRDIM_LITTLE = 10000;
        const int STRDIM_MID = 30000;
        const int STRDIM_BIG = 50000;

        // VARIABLES
        public Novus_Json novus = new Novus_Json();
        public string logPath = "C:\\";
        public static int hCon = 0;
        public bool dllInitialized = false;

        #region DLL IMPORT
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EventCallback(string data);

        public event EventHandler ArcaEventReceived;


        #region ArcaLibLog
        [DllImport("ArcaLibLog.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int ARCAlibLogComscope(IntPtr hwin);
        #endregion

        #region ArcaDevInt
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //public delegate void DownloadCallback(string data);

        [DllImport("ARCADevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADevSvcSingleCommand(int hConnect, string inData);

        [DllImport("ARCAdevint.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAInitialize(float version, StringBuilder inData);

        [DllImport("ARCAdevint.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAConnect(StringBuilder inData, EventCallback ec, ref int hConnect, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADisconnect(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCALibraryVersion(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADeviceStatus(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAInitAsyncEvent(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAOpen(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAClose(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCashData(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADepositStart(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADeposit(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADepositReport(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADepositEnd(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCACount(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCACountReport(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADispense(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCADispenseReport(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDeviceDateTime(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceDateTime(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeCassetteEmpty(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeCassetteEmptyReport(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevint.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetPocketOutput(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDeviceVersion(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDeviceType(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeDeviceReset(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetReaderCurrency(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetReaderBanks(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetReaderVersion(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCassetteSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetReaderStatistics(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCassetteSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetConfigReader(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDeviceHistory(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetMacAddress(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetReaderThreshold(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDeviceSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetAlarm(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDelays(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeAlarm(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetReaderIdentifier(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevint.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeTraceTransfer(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);



        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCAexeAlarmTemporaryLock(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCAUnitInfoConfiguration(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCAsetUpperTransportLock(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCAexeDeviceShutdown(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCAgetAudioSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCAgetProtocolSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCAgetReaderStatisticsEx(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCADevInt.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCASampling(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        #endregion

        #region ArcaDevSvc
        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAInitializeSrv(float version, StringBuilder inData);

        //[DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCALibraryVersion(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAUpdateVersion(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeCassetteInit(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeCassetteAssign(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceLAN(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDeviceLAN(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeRecoverySuspend(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeRecoveryContinue(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeJournalInit(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDevicePhoto(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDevicePhoto(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeFileTransfer(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        //[DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCAgetPocketOutput(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetPocketOutput(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDispenceSecurityLimit(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDelays(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCACassette(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCommunicationLAN(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCommunicationLAN(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCassette(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCassetteChangeKey(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetLogServiceRecord(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCassetteDenomination(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetReaderSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceIdentification(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetShiftCenterSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetShiftCenterSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceProtocolType(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetInputSetting(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetInputSetting(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceSerialNumber(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetAudioSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetOperatingHours(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetOperatingHours(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetReaderThreshold(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetTimeZone(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetTimeZone(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCommunicationUsb(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCommunicationUsb(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCommunicationSerial(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCoinCashData(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCACoinInventory(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCACoinDispense(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCoinConfiguration(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);
        

        #endregion

        #endregion


        /// <summary>
        /// Initialize DLL
        /// </summary>
        /// <returns></returns>
        public int Initialize()
        {
            int reply;
            dllInitialized = false;
            novus.InDataInitialize = new Novus_Json.CinDataInitialize();
            novus.InDataInitialize.pathtrace = logPath;
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataInitialize));
            reply = ARCAInitialize(LIBRARY_VERSION_1_0, inData);
            if (reply == 0)
                dllInitialized = true;
            return reply;
        }

        public int InitializeSrv()
        {
            int reply;
            dllInitialized = false;
            novus.InDataInitialize = new Novus_Json.CinDataInitialize();
            novus.InDataInitialize.pathtrace = logPath;
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataInitialize));
            reply = ARCAInitializeSrv(LIBRARY_VERSION_1_0, inData);
            if (reply == 0)
                dllInitialized = true;
            return reply;
        }

        /// <summary>
        /// Initialize DLL
        /// </summary>
        /// <param name="logPath">path where DLL saves the log files</param>
        /// <returns></returns>
        public int Initialize(string logPath)
        {
            int reply;
            dllInitialized = false;
            novus.InDataInitialize = new Novus_Json.CinDataInitialize();
            novus.InDataInitialize.pathtrace = logPath;
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataInitialize));
            reply = ARCAInitialize(LIBRARY_VERSION_1_0, inData);
            if (reply == 0)
                dllInitialized = true;
            return reply;
        }

        public int InitializeSrv(string logPath)
        {
            int reply;
            dllInitialized = false;
            novus.InDataInitialize = new Novus_Json.CinDataInitialize();
            novus.InDataInitialize.pathtrace = logPath;
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataInitialize));
            reply = ARCAInitializeSrv(LIBRARY_VERSION_1_0, inData);
            if (reply == 0)
                dllInitialized = true;
            return reply;
        }



        /// <summary>
        /// Connectio to NOVUS
        /// </summary>
        /// <returns></returns>
        public int ConnectEvent(Novus_Json.CinDataConnect inDataConnect)
        {
            int reply = 0;

            novus.OutDataConnect = new Novus_Json.CoutReplyCode();

            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(inDataConnect));
            StringBuilder outData = new StringBuilder(STRDIM_LITTLE);

            reply = ARCAConnect(inData, NovusFactoryTool.Form1.cbEvent, ref hCon, STRDIM_LITTLE, outData);
            JObject jConnect = JObject.Parse(outData.ToString());
            var obj = (JObject)jConnect;
            var userObj = JObject.Parse(outData.ToString());

            novus.OutDataConnect = new Novus_Json.CoutReplyCode();
            foreach (var prop in obj.Properties())
            {
                switch (prop.Name)
                {
                    case "reply":
                        Console.WriteLine((string)jConnect["reply"]["replyCode"]);
                        Console.WriteLine((string)jConnect["reply"]["replyCodeDescription"]);
                        break;
                }
            }
            novus.OutDataConnect = JsonConvert.DeserializeObject<Novus_Json.CoutReplyCode>(outData.ToString());

            Object jsonEvent = new JObject
            {
                { "cashFlowEvent", "true" },
                { "coversEvent", "true" },
                { "downloadEvent", "true" },
                { "errorsEvent", "true" },
                { "keepAliveEvent", "true" },
                { "lockEvent", "true" },
                { "operationEvent","true"},
                { "pocketsEvent","true"},
                { "qrCodeEvent","true"},
                { "remindersEvent","true"}

            };

            //Object jsonEvent = new JObject
            //{
            //    { "enableEvent", "true" },
            //    { "disableEvent", "false" },
            //    { "errors", "false" },
            //    { "pocketsEvent", "false" },
            //    { "safeEvent", "false" },
            //    { "keepAliveEvent", "false" }
            //};

            inData.Clear();
            inData.Append(Convert.ToString(jsonEvent));

            int rc_not_used = ARCAInitAsyncEvent(hCon, inData, STRDIM_LITTLE, outData);

            return reply;
        }

        /// <summary>
        /// Connectio to NOVUS
        /// </summary>
        /// <returns></returns>
        public int Connect(Novus_Json.CinDataConnect inDataConnect)
        {
            int reply = 0;

            novus.OutDataConnect = new Novus_Json.CoutReplyCode();

            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(inDataConnect));
            StringBuilder outData = new StringBuilder(STRDIM_LITTLE);

            reply = ARCAConnect(inData, null, ref hCon, STRDIM_LITTLE, outData);
            JObject jConnect = JObject.Parse(outData.ToString());
            var obj = (JObject)jConnect;
            var userObj = JObject.Parse(outData.ToString());

            novus.OutDataConnect = new Novus_Json.CoutReplyCode();
            foreach (var prop in obj.Properties())
            {
                switch (prop.Name)
                {
                    case "reply":
                        Console.WriteLine((string)jConnect["reply"]["replyCode"]);
                        Console.WriteLine((string)jConnect["reply"]["replyCodeDescription"]);
                        break;
                }
            }
            novus.OutDataConnect = JsonConvert.DeserializeObject<Novus_Json.CoutReplyCode>(outData.ToString());
            return reply;
        }

        public int DeviceStatus()
        {
            int reply;
            novus.OutDataStatus = new Novus_Json.CoutDataStatus();
            novus.InDataStatus = new Novus_Json.CinDataStatus();
            novus.InDataStatus.statusType = novConst.STATUSTYPE_EXTENDED;
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataStatus));
            StringBuilder outData = new StringBuilder(STRDIM_MID);
            reply = ARCADeviceStatus(hCon, inData, STRDIM_MID, outData);
            novus.OutDataStatus = JsonConvert.DeserializeObject<Novus_Json.CoutDataStatus>(outData.ToString());
            return reply;
        }

        /// <summary>
        /// Get the device status
        /// </summary>
        /// <returns></returns>
        public int Recovery()
        {
            int reply;
            novus.OutDataExeDeviceReset = new Novus_Json.CoutReplyCode();
            novus.InDataExeDeviceReset  = new Novus_Json.CinDataExeDeviceReset();
            novus.InDataExeDeviceReset.mode = novConst.DEVICE_RESET_MODE_FORCED;
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataExeDeviceReset));
            StringBuilder outData = new StringBuilder(256);
            reply = ARCAexeDeviceReset (hCon, inData, 256, outData);
            novus.OutDataExeDeviceReset = JsonConvert.DeserializeObject<Novus_Json.CoutReplyCode>(outData.ToString());
            return reply;
        }
        /// <summary>
        /// Activate FW SUITE
        /// </summary>
        /// <param name="filename">Suite file name to activate</param>
        /// <returns></returns>
        public int UpdateVersion(string filename)
        {
            int reply;
            novus.OutDataUpdateVersion = new Novus_Json.CoutReplyCode();
            novus.InDataUpdateVersion = new Novus_Json.CinDataUpdateVersion();
            novus.InDataUpdateVersion.FileName = filename;
            novus.InDataUpdateVersion.cmdMode = novConst.DLINK_CMDMODE_SYNC;
            novus.InDataUpdateVersion.updateMode = novConst.UPDATEVERSION_MODE_ACTIVATE;
            novus.InDataUpdateVersion.updateType = novConst.UPDATEVERSION_TYPE_SUITE;
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataUpdateVersion));
            StringBuilder outData = new StringBuilder(STRDIM_MID);
            reply = ARCAUpdateVersion(hCon, inData, STRDIM_MID, outData);
            novus.OutDataUpdateVersion = JsonConvert.DeserializeObject<Novus_Json.CoutReplyCode>(outData.ToString());
            return reply;
        }

        /// <summary>
        /// Download SUITE zip file
        /// </summary>
        /// <param name="filename">Zip file to download</param>
        /// <param name="filePath">path of zip file</param>
        /// <returns></returns>
        public int UpdateVersion(string filename, string filePath)
        {
            int reply;
            string fullFileName = filePath + "\\" + filename;
            System.IO.FileInfo myFile = new System.IO.FileInfo(fullFileName);
            novus.OutDataUpdateVersion = new Novus_Json.CoutReplyCode();
            novus.InDataUpdateVersion = new Novus_Json.CinDataUpdateVersion();
            novus.InDataUpdateVersion.FileName = filename;
            novus.InDataUpdateVersion.FilePath = fullFileName;
            novus.InDataUpdateVersion.FileSize = myFile.Length.ToString();
            novus.InDataUpdateVersion.cmdMode = novConst.DLINK_CMDMODE_SYNC;
            novus.InDataUpdateVersion.updateMode = novConst.UPDATEVERSION_MODE_INIT;
            novus.InDataUpdateVersion.updateType = novConst.UPDATEVERSION_TYPE_SUITE;
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataUpdateVersion));
            StringBuilder outData = new StringBuilder(STRDIM_MID);
            reply = ARCAUpdateVersion(hCon, inData, STRDIM_MID, outData);
            novus.OutDataUpdateVersion = JsonConvert.DeserializeObject<Novus_Json.CoutReplyCode>(outData.ToString());
            return reply;
        }

        /// <summary>
        /// Disconnect from Device
        /// </summary>
        /// <returns></returns>
        public int Disconnect()
        {
            int reply;
            //StringBuilder outData = new StringBuilder(STRDIM_LITTLE);
            //novus.inDataDisconnect = new Novus_Json.CinDisconnect();
            //StringBuilder prova = new StringBuilder(JsonConvert.SerializeObject(novus.inDataDisconnect));
            ////prova = new StringBuilder();
            //string indata="";

            var inData = new StringBuilder();
            int llOutData = 1024;
            var outData = new StringBuilder(llOutData);
            reply = ARCADisconnect(hCon, inData, llOutData, outData);
            novus.OutDataDisconnect = JsonConvert.DeserializeObject<Novus_Json.CoutReplyCode>(outData.ToString());
            return reply;
        }

        /// <summary>
        /// Open command
        /// </summary>
        /// <param name="opSide">operator side (A-T)</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public int Open(string opSide, string password)
        {
            int reply;

            novus.InDataOpen = new Novus_Json.CinDataOpen();
            novus.InDataOpen.opSide = opSide;
            novus.InDataOpen.password = password;

            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataOpen));
            StringBuilder outData = new StringBuilder(STRDIM_LITTLE);
            reply = ARCAOpen(hCon, inData, STRDIM_LITTLE, outData);
            novus.OutDataOpen = JsonConvert.DeserializeObject<Novus_Json.CoutReplyCode>(outData.ToString());
            return reply;
        }

        /// <summary>
        /// Close command
        /// </summary>
        /// <param name="opSide">operator Side</param>
        /// <returns></returns>
        public int Close(string opSide)
        {
            int reply;

            novus.InDataClose = new Novus_Json.CinDataClose();
            novus.InDataClose.opSide = opSide;

            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataClose));
            StringBuilder outData = new StringBuilder(STRDIM_LITTLE);
            reply = ARCAClose(hCon, inData, STRDIM_LITTLE, outData);
            novus.OutDataClose = JsonConvert.DeserializeObject<Novus_Json.CoutReplyCode>(outData.ToString());
            return reply;
        }

        public int GetCashData(string noteType = "")
        {
            int reply;
            novus.InDataCashData = new Novus_Json.CinDataCashData();
            StringBuilder inData = new StringBuilder();
            if (noteType != "")
            {
                novus.InDataCashData.noteType = noteType;
                novus.InDataCashData.opSide = novConst.OPSIDE_LEFT;
                novus.InDataCashData.password = "123456";
                inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataCashData));
            }
            StringBuilder outData = new StringBuilder(STRDIM_MID);
            reply = ARCAgetCashData(hCon, inData, STRDIM_MID, outData);
            novus.OutDataCashDataStd = JsonConvert.DeserializeObject<Novus_Json.CoutDataCashDataStd>(outData.ToString());
            return reply;
        }

        public int DownloadServiceInfo(string filename)
        {
            int reply=-1;
            novus.inDataTraceTransfer = new Novus_Json.CinDataeExeTraceTransfer();
            novus.inDataTraceTransfer.TransferType = "Download";
            novus.inDataTraceTransfer.TransferMode = "Service";
            novus.inDataTraceTransfer.TransferFile = "Log";
            novus.inDataTraceTransfer.ZipfileName =  filename;
            StringBuilder inData = new StringBuilder();
            inData = new StringBuilder(JsonConvert.SerializeObject(novus.inDataTraceTransfer));
            StringBuilder outData = new StringBuilder(STRDIM_MID);
            reply = ARCAexeTraceTransfer(hCon, inData, STRDIM_MID, outData);
            return reply;
        }

        public int Deposit()
        {
            int reply;
            novus.InDataDeposit = new Novus_Json.CinDataDeposit();
            novus.InDataDeposit.cmdMode = novConst.DLINK_CMDMODE_SYNC;
            novus.InDataDeposit.cassetteDetails = "true";
            //novus.InDataDeposit.mode = "";
            StringBuilder inData = new StringBuilder(JsonConvert.SerializeObject(novus.InDataDeposit));
            StringBuilder outData = new StringBuilder(STRDIM_LITTLE);
            reply = ARCADeposit(hCon, inData, STRDIM_LITTLE, outData);
            novus.OutDataDeposit = JsonConvert.DeserializeObject<Novus_Json.CoutDataDeposit>(outData.ToString());

            return reply;
        }

        public int GetDateTime()
        {
            int reply;
            StringBuilder outData = new StringBuilder(STRDIM_LITTLE);
            reply = ARCAgetDeviceDateTime(hCon, null, STRDIM_LITTLE, outData);
            return reply;
        }

        public int AbilitaSSH()
        {
            int reply;
            string send = "T,1,6,3,1";
            reply = ARCADevSvcSingleCommand(hCon, send);
            return reply;
        }
    }
}
