using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;



namespace NOVUS_SVC
{
    public class Novus_Class_svc
    {


        public class ArcaReply_svc
        {
            // ------------------------------------------------------------------------
            //                      REPLY-CODE
            // ------------------------------------------------------------------------

            

            // ------------------------------------------------------------------------
            //                      ERRORS
            // ------------------------------------------------------------------------
            

        }

        //[DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int ARCADevSvcDebug(int hConnect, string inData);

        #region COIN
        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCACoinDispense(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCACoinInventory(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCoinConfiguration(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCoinCashData(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);
        #endregion

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAInitializeSrv(float version, StringBuilder inData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        
        public static extern int ARCADevSvcSingleCommand(int hConnect, string inData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeCassetteInit(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDevicePhoto(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDevicePhoto(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetReaderSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAUpdateVersion(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetOperatingHours(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

       
        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCACassetteSettingProtocol(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeCassetteAssign(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAexeFileTransfer(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetOutputSetting(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDispenseSecurityLimit(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDelays(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetInputSetting(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetInputSetting(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetPocketOutput(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetPocketOutput(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCassette(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCassetteDenomination(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceIdentification(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceProtocolType(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCommunicationLAN(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCommunicationLAN(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetShiftCenterSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDeviceLAN(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceLAN(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetOperatingHours(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetShiftCenterSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetReaderThreshold(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDeviceSerialNumber(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetAudioSettings(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetTimeZone(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetTimeZone(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCommunicationUsb(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCommunicationUsb(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetDevicePhotoNovus(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetDevicePhotoNovus(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAsetCommunicationSerial(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);

        [DllImport("ARCAdevsvc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAgetCommunicationSerial(int hConnect, StringBuilder inData, int lloutData, StringBuilder outData);


    };
};
