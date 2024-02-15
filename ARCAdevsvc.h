// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the ARCADEVSRV_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// ARCADEVSRV_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef _WIN32
#ifdef ARCADEVSRV_EXPORTS
#define ARCADEVSRV_API __declspec(dllexport)
#else
#define ARCADEVSRV_API __declspec(dllimport)
#endif
#endif
#ifndef ARCADEVSRV_H
#define ARCADEVSRV_H     1

#ifdef _WIN32
// This class is exported from the ARCADevSrv.dll
class ARCADEVSRV_API CARCADevSrv {
public:
CARCADevSrv(void);
// TODO: add your methods here.
};

extern ARCADEVSRV_API int nARCADevSrv;
#else
 #include "win_to_linux.h"
 #define ARCADEVSRV_API
#endif

// ------------------------------------------------------------------------
//                      DEFINES
// ------------------------------------------------------------------------
#define COMMAND_DIM_MIN_BUFFER_OUTPUT 1024

#define NO_TRANSFER						    -1
#define REFERENCE_FILE_TRANSFER             12
#define SSLTLS_CERTIFICATE_FILE_TRANSFER	16
#define SSLTLS_PRIVATE_KEY_FILE_TRANSFER	17
#define SUITE_FILE_TRANSFER					18  
#define IPSEC_CERTIFICATE_FILE_TRANSFER     20
#define IPSEC_CA_FILE_TRANSFER				21


//GENERAL
#define OPERATION_TYPE "operationType"
#define DEVSVC_OPERATION_CONFIG_TYPE "configType"
#define OPERATION_OUTPUT_MESSAGE "message"
#define OPERATION_ENABLE	"enable"
#define OPERATION_DISABLE	"disable"

 //--------------------------- Defines for ARCAsetDeviceIdentification  -----------------------------
#define OPERATION_CONN "connectivity"

//defines for ARCAUpdateVersion-------------------------------------------
#define OPERATION_MODE_UPDATE "updateMode"
#define OPERATION_MODE_UPDATE_TYPE "updateType"
#define OPERATION_MODE_UPDATE_INIT "init"
#define OPERATION_MODE_UPDATE_ACTIVATE "activate"
#define OPERATION_MODE_UPDATE_WAIT "check"
#define OPERATION_DOWNLOAD_FILE_NAME "FileName"
#define OPERATION_DOWNLOAD_FILE_SIZE "FileSize"
#define OPERATION_DOWNLOAD_FILE_PATH "FilePath"

#define OPERATION_UPDATE_FILE_NAME_SUITE "Suite"
#define OPERATION_UPDATE_FILE_NAME_REFERENCE "Reference"
#define OPERATION_UPDATE_FILE_NAME_SSL_CERTIFICATE  "SSLcertificate"
#define OPERATION_UPDATE_FILE_NAME_SSL_PRIVATE_KEY "SSLprivatekey"
#define OPERATION_UPDATE_FILE_NAME_IPSEC_CERTIFICATE "IpsecCertificate"
#define OPERATION_UPDATE_FILE_NAME_IPSEC_CA_KEY "IpsecCa"

//defines for ARCAexeDeviceErrorLog-------------------------------------------
#define OPERATION_INIT_MODE_LOG_INIT "Init"

//defines for ARCAgetDevicePhoto----------------------------------------------
#define PHOTO_TARGET_MODULE					"targetModule"

#define PHOTO_TARGET_MODULE_UPPER			"upper"				//2
#define PHOTO_TARGET_MODULE_ALL				"all"				//t= 0 , 10
#define PHOTO_TARGET_MODULE_LOWER			"lower"				//11
#define PHOTO_TARGET_MODULE_BINS			"bins"				//cm18 compatibility..
#define PHOTO_TARGET_MODULE_SINGLE_PHOTO	"singlePhoto"				

#define PHOTO_TARGET_MODULE_BINS_PHASE		"binModulePhase"
#define PHOTO_TARGET_MODULE_BINS_PHASE1		"phase1"				
#define PHOTO_TARGET_MODULE_BINS_PHASE2		"phase2"				

#define PHOTO_TARGET_MODULE_BINS_REJECT     "binReject"		//only novus		
#define PHOTO_TARGET_MODULE_BINS_OUTPUT	    "binOutput"		//only novus 		
#define PHOTO_TARGET_MODULE_BINS_INPUT	    "binInput"		//only novus		

//[OUT]
//#define PHOTO_MODULE				"moduleId"
#define PHOTO_RESULT						"resultCode"
#define PHOTO_RESULT_DESCRIPTION			"resultDescription"
#define PHOTO_THRESHOLD						"threshold"
#define PHOTO_AGC							"agc"
#define PHOTO_CURRENT						"current"
#define PHOTOS_ID							"phId"

#define PHOTOS_ID_THRESHOLD					 "threshold"
#define PHOTOS_ID_AGC					     "agc"
#define PHOTOS_ID_CURRENT					 "current"
#define PHOTOS_ID_POLARITY					 "polarity"
#define PHOTOS_ID_DRIVER_TYPE				 "drivertype"


#define PHOTO_DESCRIPTION_SUCCES	"success"
#define PHOTO_DESCRIPTION_ABSENT	"absent"
#define PHOTO_DESCRIPTION_ND		"nd"
#define PHOTO_DESCRIPTION_ERROR_SYN	"syntaxError"
#define PHOTO_DESCRIPTION_ERROR		"executionError"

#define PHOTO_SYNC_TRV_R_1			"syncTrvR1"
#define PHOTO_SYNC_TRV_R_2			"syncTrvR2"
#define PHOTO_SYNC_TRV_L_1			"syncTrvL1"
#define PHOTO_SYNC_TRV_L_2			"syncTrvL2"
#define PHOTO_CASH_R				"cashR"
#define PHOTO_CASH_L				"cashL"
#define PHOTO_SYNC_OUT_R			"syncOutR"
#define PHOTO_SYNC_OUT_C			"syncOutC"
#define PHOTO_SYNC_OUT_L			"syncOutL"
#define PHOTO_STACK_REJ_R_1			"stackRejR1"
#define PHOTO_STACK_REJ_R_2			"stackRejR2"
#define PHOTO_STACK_REJ_L_1			"stackRejL1"
#define PHOTO_STACK_REJ_L_2			"stackRejL2"
#define PHOTO_TRACK_OPEN			"trackOpen"
#define PHOTO_SORTER_TRV_H			"sorterTrvH"
#define PHOTO_SORTER_TRV_L			"sorterTrvL"
#define PHOTO_SORTER_OUT			"sorterOut"
#define PHOTO_BIN_OUT_R				"binOutR"
#define PHOTO_BIN_OUT_C				"binOutC"
#define PHOTO_BIN_OUT_L				"binOutL"
#define PHOTO_BIN_REJ_R				"binRejR"
#define PHOTO_BIN_REJ_C				"binRejC"
#define PHOTO_BIN_REJ_L				"binRejL"
#define PHOTO_FEED_1				"feed1"
#define PHOTO_FEED_2				"feed2"
#define PHOTO_FEED_3				"feed3"
#define PHOTO_FEED_4				"feed4"
#define PHOTO_REJ_R					"rejR"
#define PHOTO_REJ_C					"rejC"
#define PHOTO_REJ_L					"rejL"
#define PHOTO_STACK_OUT_R_1			"stackOutR1"
#define PHOTO_STACK_OUT_R_2			"stackOutR2"
#define PHOTO_STACK_OUT_L_1			"stackOutL1"
#define PHOTO_STACK_OUT_L_2		    "stackOutL2"
#define PHOTO_BIN_IN				"binIn"

#define PHOTO_FEED_STROBE			"feedStrobe"
#define PHOTO_FEED_OPEN				"feedOpen"

//lower part
#define PHOTO_CASSETTE_DESCRIPTION	"cassetteDescription"
#define PHOTO_OUT_L					"outL"
#define PHOTO_OUT_R					"outR"
#define PHOTO_TRACK					"track"
#define PHOTO_END					"end"
#define PHOTO_SORTER				"sorter"


//CM18x photo...

#define PHOTO_ALL					"ph_all"
#define PHOTO_UPPER_PART			"ph_upper"
#define PHOTO_LOWER_PART			"ph_lower"
#define PHOTO_PH_IN_CENTER			"ph_incenter"
#define PHOTO_PH_IN_LEFT			"ph_inleft"
#define PHOTO_PH_FEED				"ph_feed"
#define PHOTO_PH_C1					"ph_c1"
#define PHOTO_PH_SHIFT				"ph_shift"
#define PHOTO_PH_INQ				"ph_inq"
#define PHOTO_PH_COUNT				"ph_count"
#define PHOTO_PH_OUT				"ph_out"
#define PHOTO_PH_C3					"ph_c3"
#define PHOTO_PH_C4A				"ph_c4a"
#define PHOTO_PH_C4B				"ph_c4b"
#define PHOTO_PH_REJ				"ph_rej"
#define PHOTO_PH_IN_BOX				"ph_in_box"
#define PHOTO_PH_CASH_LEFT			"ph_cashleft"
#define PHOTO_PH_CASH_RIGHT			"ph_cashright"
#define PHOTO_H_MAX					"ph_hmax"

//----------------- Defines for ARCAsetDevicePhoto	-----------------------------
//CM18x photo...
#define OPERATION_DEVICE_PHOTOS_ID "ph_id"
#define OPERATION_DEVICE_PHOTOS_VAL "ph_val"

//new photo bin for Novus
#define PHOTO_TARGET_MODULE_BINS_INPUT_IN			"binInputIn"				
#define PHOTO_TARGET_MODULE_BINS_INPUT_OUT			"binInputOut"	
#define PHOTO_TARGET_MODULE_BINS_OUTPUT_IN			"binOutputIn"				
#define PHOTO_TARGET_MODULE_BINS_OUTPUT_OUT			"binOutputOut"	
#define PHOTO_TARGET_MODULE_BINS_REJECT_IN			"binRejectIn"				
#define PHOTO_TARGET_MODULE_BINS_REJECT_OUT			"binRejectOut"	

//defines for ARCACassette-------------------------------------------------
#define OPERATION_CASSETTE_NUM_CAS "num_cas"
#define OPERATION_CASSETTE_ENABLE "Enable"

//--------------------------- Defines for ARCAsetReaderSettings-----------------------------
//IN
#define READER_SETTING_TYPE "type"
#define READER_SETTING_NBANKS "numBanks"
#define READER_SETTING_ACTIVEBANKS "activeBanks"
#define READER_SETTING_SLOT "slot"
#define READER_SETTING_REFERENCES "references"

#define READER_SETTING_BANK_NUM "bank_num"
#define READER_SETTING_BANK_TO_ACT "bank_to_act"
#define READER_SETTING_REF_1 "enable_ref1"
#define READER_SETTING_REF_2 "enable_ref2"
#define READER_SETTING_REF_3 "enable_ref3"
#define READER_SETTING_REF_4 "enable_ref4"
#define READER_SETTING_BANKID "bankId"






//--------------------------- Defines for ARCAsetCassetteChangeKey-----------------------------
#define OPERATION_SEC_TARGET_CASSETTE "cassette"
#define OPERATION_SEC_NEW_CODE "newCode"
#define OPERATION_SEC_OLD_CODE "oldCode"

//--------------------------- Defines for ARCAgetShiftCenterSettings-----------------------------
#define SHIFT_CENTERING_VALUE "centeringValue"


//--------------------------- Defines for ARCAsetDeviceProtocolType-----------------------------
#define DEVICE_PROTOCOL_TYPE "protocolSet"
#define DEVICE_PROTOCOL_STD "STANDARD"
#define DEVICE_PROTOCOL_SIM "SIMPLIFIED"

//--------------------------- Defines for ARCAsetDeviceSerialNumber-----------------------------
#define CASSETTE_MODULE_SERIAL_NUMBER				"cassetteSerialNumber"
#define UNIT_MODULE_CASSETTE_NUMBER					"cassetteNumber"


//--------------------------- Defines for ARCASetAudioSettings -------------
//INT
#define SET_AUDIO_VALUE "audioValue"

//--------------------------- Defines for ARCAget/setOperatingHours -------------
//IN
#define OPERATING_HOURS_TYPE "operatingHoursType"
#define OPERATING_HOURS_TYPE_AP "aperiodic"
#define OPERATING_HOURS_TYPE_P "periodic"

#define APERIODC_DAY "day"
#define APERIODC_MONTH "month"
#define APERIODC_YEAR "year"
#define OPERATING_HOURS_DAY "weekDay"
#define OPERATING_HOURS_SWTICH_ON "switchOn"
#define OPERATING_HOURS_SWTICH_OFF "switchOff"
#define OPERATING_HOURS_LUNCH_BREAK_ON "lunchBreakOn"
#define OPERATING_HOURS_LUNCH_BREAK_OFF "lunchBreakOff"

//--------------------------- Defines for ARCAsetReaderThreshold -------------
//IN
#define DEVSVC_FIT_CONFIGURATION "fitConfiguration"
#define DEVSVC_FIT_THRESHOLD "fitThreshold"
#define DEVSVC_THRESHOLD_FIT_FLAG "fitFlag"
#define DEVSVC_THRESHOLD_FIT_T_FITNESS "fitThresholdFitness"


//--------------------------- Defines for ARCAgetCommunicationLAN -------------
//IN
#define LAN_PARAM "lanParam"
#define LAN_PARAM_PORT "lanPort"
//OUT
#define LAN_PARAM_VALUE "lanParamValue"

#define LAN_PORT_VALUE  "lanPortValue"
#define LAN_PORT_STATUS "lanPortStatus"

//--------------------------- Defines for ARCAsetCommunicationLAN -------------
//IN
#define IN_LAN_PARAM_WHITELIST "whitelist"
#define IN_LAN_OP_TYPE "opType"
#define LAN_ADDR_TYPE "addressType"
#define LAN_ADDR "address"

//--------------------------- Defines for ARCAgetDeviceLAN -------------
#define LAN_COMM_PROTOCOL "lanEncryption"
 


//--------------------------- Defines for ARCAgetTimeZone -------------
#define  TIME_ZONE_INDEX           "timeZoneIndex"
#define	 TIME_ZONE_UTC				"timeZoneUTC"
#define	 TIME_ZONE_LABEL			"timeZoneLabel"

//--------------------------- Defines for ARCAgetUsbPortStatus -------------
#define PORT_USB_STATUS				"statusUsb"
#define PORT_USB					"portUsb"

//--------------------------- Defines for  ARCAsetCommunicationSerial-------------
#define PORT_SERIAL_STATUS			"statusSerial"
#define PORT_SERIAL					"portSerial"

#define OPERATION_OFF				"off"
#define OPERATION_PERIPHERAL		"peripheral"
#define OPERATION_HOST				"host"

//--------------------------- Defines for  ARCAsetDeviceVersion-------------
#define DISPLAY_VERSION				"displayVersion"
#define DEVINT_VERSION				"ARCAdevintVersion"
#define DEVSVC_VERSION				"ARCAdevsvcVersion"
#define DEVLINK_VERSION				"ARCAdevlnkVersion"
#define LIB_SPDLOG_VERSION				"spdlogVersion"
#define LD_VERSION					"ARCAldVersion"


//--------------------------- Defines for  ARCAgetDeviceLife-------------
#define VALUE_LIFE					"totalNotesLifeProcessed"

//--------------------------- Defines for  ARCAexeInject-------------
#define INPUT_VALUE					"InputValue"
#define OUTPUT_VALUE				"OutputValue"
#define TIMEOUT_VALUE				"TimeOut"

//--------------------------- Defines for  ARCAexeTestMotorRTC-------------
#define MOTOR_ID					"motorId"
//CM18 MOTOR
#define MOTOR_MSO					"mso"	
#define MOTOR_MSV_INPUT				"msvInput"	
#define MOTOR_MSO_OUTPUT			"msvOutput"	
#define MOTOR_PRESS_SFO			    "presFo"
#define MOTOR_SFO					"sfo"
#define MOTOR_ALIGN					"align"
#define MOTOR_ELM_SHIFT				"elmShift"
#define MOTOR_MDE					"mde"
#define MOTOR_PRESSOR				"pressor"
#define MOTOR_PUSHER				"pusher"
#define MOTOR_CAT2					"cat2BoxSorter"

//NOVUS MOTOR
#define MOTOR_FEEDER_INPUT					"feederInput"	
#define MOTOR_FEEDER_OUTPUT					"feederOutput"	
#define MOTOR_TRASP_H						"traspH"	
#define MOTOR_ENTER_INPUT					"enterInput"	
#define MOTOR_ENTER_OUTPUT					"enterOutput"	
#define MOTOR_TRASP_V_INPUT					"traspVInput"	
#define MOTOR_TRASP_V_OUTPUT				"traspVOutput"	

//--------------------------- Defines for  ARCAexeTestMotorSafe-------------
#define MOTOR_MIV_INPUT					"mivInput"	
#define MOTOR_MIV_OUTPUT				"mivOutput"	
#define MOTOR_ELM						"elmDoor"	
#define MOTOR_DOOR						"doorSolenoid"
#define MOTOR_CASSETTE_A_SORTER			"cassetteASorter"
#define MOTOR_CASSETTE_B_SORTER			"cassetteBSorter"
#define MOTOR_CASSETTE_C_SORTER			"cassetteCSorter"
#define MOTOR_CASSETTE_D_SORTER			"cassetteDSorter"
#define MOTOR_CASSETTE_E_SORTER			"cassetteESorter"
#define MOTOR_CASSETTE_F_SORTER			"cassetteFSorter"
#define MOTOR_CASSETTE_G_SORTER			"cassetteGSorter"
#define MOTOR_CASSETTE_H_SORTER			"cassetteHSorter"
#define MOTOR_CASSETTE_I_SORTER			"cassetteISorter"
#define MOTOR_CASSETTE_J_SORTER			"cassetteJSorter"
#define MOTOR_CASSETTE_K_SORTER			"cassetteKSorter"
#define MOTOR_CASSETTE_L_SORTER			"cassetteLSorter"
#define MOTOR_TRIVIO_SAFE				"safeTrv"

//--------------------------- Defines for  ARCAexeTestMotorCassette-------------
#define CASSETTE_ID						"cassetteId"
#define MOTOR_PINCH_ROLLER				"pinchRoller"
#define MOTOR_PRESET					"preset"
#define MOTOR_SORTER					"sorter" //only Novus

//--------------------------- Defines for  ARCAexePhotoSensorCassette-------------
//IN
#define OPERATION_READ					"read"
#define OPERATION_WRITE					"write"
//OUT
#define PH_ID							"phId"
#define PH_FOTIN						"phFotin"
#define PH_FOTOUT						"phFotout"

//--------------------------- Defines for  ARCAexeRtcSensorStatus-------------
#define PHOTO_DESCRIPTION				"phDescription"
#define PHOTO_VALUE						"phValue"

#define PHOTO_PH_COVER					"ph_cover"
#define PHOTO_PH_COVLEF					"ph_coverLeft"
#define PHOTO_PH_FPOCKET				"ph_fPocket"
#define PHOTO_PH_FPRESS					"ph_fPress"
#define PHOTO_PH_INPOCKET				"ph_inPocket"
#define PHOTO_SHAKE						"ph_shake"
#define PHOTO_PH_PUSH					"ph_push"
#define PHOTO_PH_PRESS					"ph_press"

#define PHOTO_FREE						"free"
#define PHOTO_COVERED					"covered"
#define PHOTO_NOT_RESPONSE				"notResponse"

#define PHOTO_OPEN						"open"
#define PHOTO_CLOSE						"close"

//--------------------------- Defines for  ARCAexeSafeSensorStatus-------------
#define PHOTO_PH_ALARM3					"ph_alarm3"
#define PHOTO_PH_ALARM4					"ph_alarm4"
#define PHOTO_PH_SAFE_OPEN				"ph_safeOpen"

#define PHOTO_ALARM_OPEN				"alarmOpen"
#define PHOTO_ALARM_CLOSE				"alarmClose"

//--------------------------- Defines for  ARCAexeSensorStatusAudit-------------
#define PHOTO_STATUS					"phStatus"
#define PHOTO_PERCENTILE				"phPercentile"
#define PHOTO_ID						"phId"


//--------------------------- Defines for  ARCAexeSensorStatus-------------
//IN
#define PHOTO_TARGET_MODULE_SWITCH      "switch"
//OUT
#define SWITCH_UPPER					"switchUpper"
#define SWITCH_DOOR						"switchDoor"
#define SWITCH_CAGE						"switchCage"
#define PHOTO_NOT_OPEN_NOT_CLOSE		"not_open_not_close"

// ------------------------------------------------------------------------
//                      ERRORS - WARNING
// ------------------------------------------------------------------------
#define AR_KO -1



#define AR_JSON_MISSING_UPDATE_MODE 6010
#define AR_JSON_INVALID_FILE_TRANSFER 6011

#define AR_JSON_INVALID_DELAY_ELEMENT 6013

#define AR_INPUT_VALUE_NOT_FOUND 6014

#define AR_TRANSFER_LAN_BUFFER_KO 6016
#define AR_TRANSFER_LAN_LENFILE_ERROR 6017
#define AR_TRANSFER_BUFFER_KO 6018
#define AR_TRANSFER_LENFILE_ERROR    6019
#define AR_TRANSFER_LENFILE_NULL    6020

#define AR_OPEN_FILE_ERR 6023
#define AR_INVALID_TYPE_DOWNLOAD 6024
#define AR_INVALID_LAN_PARAMETER 6025
#define AR_INVALID_BANK_ENABLE_TYPE	6026






     
#define AR_WAIT_DOWNLOAD_TIME_ELAPSED 6100

#ifdef __cplusplus
extern "C" {
#endif
// ------------------------------------------------------------------------
//                      EXPORTED FUNCTIONS
// ------------------------------------------------------------------------
//
ARCADEVSRV_API int ARCAInitializeSrv(float versioning, LPSTR inData);

ARCADEVSRV_API int ARCAdevsvcLibraryVersion(std::string &);

ARCADEVSRV_API int ARCADevSvcSingleCommand(int hConnect, char *inData);

ARCADEVSRV_API int ARCADevSvcDebug(int hConnect, char *inData);

ARCADEVSRV_API int ARCAexeCassetteAssign(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAUpdateVersion(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeDeviceErrorLog(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeRecoverySuspend(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeRecoveryContinue(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeJournalInit(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetDevicePhoto(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeFileTransfer(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetDevicePhoto(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetDispenseSecurityLimit(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCACassette(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetCassetteChangeKey(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetReaderSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetCassetteDenomination(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetDeviceIdentification(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetCassette(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetDeviceSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetShiftCenterSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetDeviceProtocolType(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetDeviceSerialNumber(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetAudioSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetOperatingHours(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetReaderThreshold(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetCommunicationLAN(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetCommunicationLAN(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetOperatingHours(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetDeviceLAN(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetDeviceLAN(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetShiftCenterSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetTimeZone(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetTimeZone(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetCommunicationUsb(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetCommunicationUsb(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetDevicePhotoNovus(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetDevicePhotoNovus(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetCommunicationSerial(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetCommunicationSerial(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetDeviceVersion(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAgetDeviceLife(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeInject(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetModuleSerialNumber(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAsetMacAddress(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeCassetteAcceptAssign(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeTestMotorRTC(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeTestMotorSafe(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeTestMotorCassette(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexePhotoSensorCassette(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeRtcSensorStatus(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeSafeSensorStatus(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeSensorStatus(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVSRV_API int ARCAexeSensorStatusAudit(int hConnect, LPSTR inData, int lloutData, LPSTR outData);


#ifdef __cplusplus
}
#endif


#endif
