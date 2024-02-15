//
// ARCA Technologies s.r.l.
// Strada Statale Lago di Viverone 17 - 10012 Bollengo (TORINO) Italy
//
// Phone ++39 125 235611
// Telefax ++39 125 235623
//
// www.arca.com		techsupp.it@arca.com
//
//
// All rights reserved
//
// We reserve the right to vary technical specification
//
//  MODULE:   ARCADevInt.h
//
//  PURPOSE:  Novus Include Interface

#ifdef _WIN32

#ifdef ARCADEVINT_EXPORTS
#define ARCADEVINT_API __declspec(dllexport)
#else
#define ARCADEVINT_API __declspec(dllimport)
#endif
#endif
#ifndef ARCADEVINT_H
#define ARCADEVINT_H     1

#define LIBRARY_VERSION_0_1						0.1
#define LIBRARY_VERSION_1_0						1.0
#ifndef _WIN32
	typedef unsigned char UCHAR, BYTE, *LPBYTE;
	typedef char* LPSTR;
	#define CM_OK   0
	#define APIENTRY
    #define ARCADEVINT_API
#endif

// ------------------------------------------------------------------------
//                      DEFINES
// ------------------------------------------------------------------------

//--------------------------- Defines for Asyncronous ------------------------
#define A_EVENT									"event"

#define A_EVENT_TYPE							"type"
#define A_EVENT_DESCRPTION						"description"
	

#define A_EVENT_STATUS_CODE						"statusCode"
#define A_EVENT_STATUS_DESCRIPTION				"statusDescription"

#define A_EVENT_POCKET_REPORT					"pocketsReport"
#define A_EVENT_POCKET_REPORT_OUTPUT			"totalOutput"
#define A_EVENT_POCKET_REPORT_REJECT			"totalReject"

#define A_EVENT_DISABLE							"disableEvent"  
#define A_EVENT_ENABLE							"enableEvent" 
#define A_EVENT_ERRORS							"errors"  
#define A_EVENT_POCKETES						"pocketsEvent"  
#define A_EVENT_SAFE							"safeEvent"  


//general defines
#define OPERATION_CONFIG_TYPE					"configType"
#define OPERATION_PASSWORD						"password"
#define OP_TYPE									"type"
#define OPERATION_ENABLE 						"enable"
#define OPERATION_DISABLE 					    "disable"

// Parameter connectionType
#define CONNECTION_LAN							"LAN"
#define CONNECTION_USB							"USB"
#define CONNECTION_RS232						"RS232"

#define CONNECTION_ENCRIPTION					"encryption"
#define CONNECTION_ENCRIPTION_SSL				"SSL"
#define CONNECTION_ENCRIPTION_TLS				"TLS"

// Parameter protocolSet
#define PROTOCOL_STANDARD						"STANDARD"
#define PROTOCOL_SIMPLIFIED						"SIMPLIFIED"


// Parameter Mode for Deposit, Dispene, Count, EmptyDrawer
#define MODE_COMMAND_MODE						"cmdMode"
#define MODE_COMMAND_ASYNC						"ASYNC"
#define MODE_COMMAND_SYNC						"SYNC"


// Default parameters for serial comunication
#define COM_BAUDERATE							9600
#define COM_PARITY								"NO"
#define COM_DATABITS							8
#define COM_STOPBITS							1


// Parameter Operation
#define DEPOSIT_END_ACCEPTED					"accept"
#define DEPOSIT_END_REJECTED					"undo"
#define DEPOSIT_END_LIST						"list"

// Parameter Operation
#define COUNTING_START							"StartCounting"
#define COUNTING_CONTINUE						"ContinueCounting"
#define COUNTING_FULL							"FullCounting"
#define COUNTING_START_SAMPLING					"startsampling"



//--------------------------- Defines for ARCAInitialize -----------------------------
#define INITILIZE_TRACE_PATH					"pathtrace"
#define REPLY_CONTENT							"reply"
#define REPLY_CODE								"replyCode"
#define REPLY_CODE_DESCRIPTION					"replyCodeDescription"
#define HEADER_CONTENT							"content"

//---------------------------- BUFFER OUTPUT SECTION
#define DEPOSIT_DIM_MIN_BUFFER_OUTPUT			2048
#define COMMAND_DIM_MIN_BUFFER_OUTPUT			1024
#define CASH_DATA_DIM_MIN_BUFFER_OUTPUT			1024
#define DEPOSIT_END_DIM_MIN_BUFFER_OUTPUT		1024
#define DISPENSE_DIM_MIN_BUFFER_OUTPUT			1024
#define CASSETTE_DIM_MIN_BUFFER_OUTPUT			1024
#define DEVICE_DATETIME_DIM_MIN_BUFFER_OUTPUT	1024
#define COUNTING_DIM_MIN_BUFFER_OUTPUT			1024

//--------------------------- Defines for Connect -----------------------------
#define CONNECT_TYPE							"connectionType"
#define CONNECT_PROTOCOL						"protocolSet"
#define CONNECT_IP_ADDRESS						"address"
#define CONNECT_IP_PORT							"port"
#define CONNECT_USB_NUMBER						"USBSerialNumber"
#define CONNECT_COM_NAME						"COMname"
#define CONNECT_COM_BAUDERATE					"baudRate"
#define CONNECT_COM_PARITY						"parity"
#define CONNECT_COM_DATABITS					"dataBits"
#define CONNECT_COM_STOPBITS					"stopBits"
#define CONNECT_PARITY_NO						"None"
#define CONNECT_PARITY_EVEN						"Even"
#define CONNECT_PARITY_ODD						"Odd"

//--------------------------- Defines for Open --------------------------------
#define OPEN_OPSIDE								"opSide"


//--------------------------- Defines for Get Cash Data -----------------------

#define CASH_DATA_HEADER						"UnitCashData"
#define CASH_DATA_SIDE_ENABLED					"side_enabled"
#define CASH_DATA_CASSETTE_ID_STATUS			"cassetteIDstatus"

#define CASH_DATA_TOTAL_NOTES					"totalNotes"
#define CASH_DATA_REPORT_ELEMENTS				"noteReportElements"
#define CASH_DATA_NOTE_REPORT					"noteReport"
#define CASH_DATA_CASSETTE_ID					"cassetteID"
#define CASH_DATA_NOTE_ID						"noteID"
#define CASH_DATA_NOTE_NUMBER					"numberNote"
#define CASH_DATA_CAPACITY						"capacity"
#define CASH_DATA_SWITCH						"switch"
#define CASH_DATA_DETAILS_CASSETTE				"detailsCassette"
#define CASH_DATA_SOILING_TEST					"soilingTest"
#define CASH_DATA_GRID_CHECK					"gridCheck"
#define CASH_DATA_TAPE_CHECK					"tapeCheck"
#define CASH_DATA_UV_DE_INKED_CHECK				"UVDe-InkedCheck"
#define CASH_DATA_DOG_AERS_CORNER_CHECK			"dogEarsCornerCheck"
#define CASH_DATA_FORMAT_CHECK					"formatCheck"
#define CASH_DATA_CONTRAST_CHECK				"contrastCheck"
#define CASH_DATA_CLOSED_TEARS_CHECK			"closedTearsCheck"
#define CASH_DATA_GRAFFITTI_CHECK				"graffittiCheck"
#define CASH_DATA_STAIN_CHECK					"stainCheck"
#define CASH_DATA_DYE_CHECK						"dyeCheck"

#define CASH_DATA_NOTE_TYPE						"noteType"
#define CASH_DATA_NOTE_TYPE_UNFIT				"unfitSafeLastDeposit"
#define CASH_DATA_NOTE_TYPE_UNFIT_OUTPUT		"unfitOutputLastDeposit"
#define CASH_DATA_NOTE_TYPE_NOT_AUTHENTICATED	"notAuthenticated"
#define CASH_DATA_NOTE_TYPE_SUSPECT				"suspect"
#define CASH_DATA_NOTE_TYPE_SUSPECT_NOT_AUTH	"suspectNotAuthenticated"
#define CASH_DATA_NOTE_TYPE_UNFIT_DETAILS		"unfitLastDeposit"
#define CASH_DATA_NOTE_UNFIT_CLASSIF_DETAILS	"unfitClassificationDetails"
#define CASH_DATA_ADVANCED						"advanced"

//--------------------------- Defines for Deposit -----------------------------
#define DEPOSIT_HEADER							"Deposit"

#define DEPOSIT_CMD_MODE						"cmdMode"
#define DEPOSIT_DENOM_ID						"denomID"
#define DEPOSIT_MODE_TYPE						"mode"

#define DEPOSIT_MODE_TYPE_NATION				"nation"
#define DEPOSIT_MODE_TYPE_FORCED				"forced"
#define DEPOSIT_FORCED_MODE						"forcedMode" 
#define DEPOSIT_MODE_TYPE_CONTINUOUS			"continuousFeeding"
#define DEPOSIT_MODE_TYPE_CONTINUOUS_TIME		"continuousFeedingTime"
#define DEPOSIT_MODE_TYPE_CONTINUOUS_ACTION		"continuousFeedingAction"

#define DEPOSIT_REPORT							"depositReport"
#define DEPOSIT_TOTAL_NOTE						"totalNoteDep"
#define DEPOSIT_REFUSED							"refused"
#define DEPOSIT_UNRECOGNIZED					"unrecognized"
#define DEPOSIT_NOTE_REPORT_ELEMENTS			"noteReportElements"
#define DEPOSIT_NOTE_REPORT						"noteReport"
#define DEPOSIT_NOTE_TYPE						"noteType"
#define DEPOSIT_NUMBER_NOTE						"numberNote"
#define DEPOSIT_NOTE_SAFE_CASSETTES				"safeNumberOfCassettes"
#define DEPOSIT_SAFE_REPORT						"safeReport"
#define DEPOSIT_CASSETTE_ID						"storageID"
#define DEPOSIT_CASSETTE_CONF					"storageConf"
#define DEPOSIT_NOTE_QTY						"noteQty"
#define DEPOSIT_CASSETTE_DETAILS				"cassetteDetails"
#define DEPOSIT_DETAILS_BEFORE					"detailsBefore"
#define DEPOSIT_DETAILS_AFTER					"detailsAfter"


#define DEPOSIT_END_HEADER						"DepositEnd"

#define DEPOSIT_END_OPERATION					"reviewType"
#define DEPOSIT_END_TOTAL_NOTES					"totalNotesProcessed"
#define DEPOSIT_END_NOTE_REPORT					"noteReport"
#define DEPOSIT_END_NOTE_TYPE					"noteType"
#define DEPOSIT_END_NUMBER_NOTE					"numberNote"

#define DEPOSIT_MANAGE_LIGHT					"manageLights"
#define DEPOSIT_MANAGE_LIGHT_TRUE				"true"


//--------------------------- Defines for Count -------------------------------
#define COUNTING_MODE_TYPE						"mode"
#define COUNTING_MODE_TYPE_STD_COUNT			"standardCount"     //Standard counting
#define COUNTING_MODE_TYPE_FIT					"fit"
#define COUNTING_MODE_TYPE_SAMPLING				"sampling"
#define COUNTING_MODE_TYPE_MODE_BLOCK_COUNT     "modeBlockCount"    //counting mode block      
#define COUNTING_MODE_SINGLE_EMISSION			"singleEmis"        
#define COUNTING_MODE_MULTI_EMISSION			"multiEmis"         
//#define COUNTING_MODE_SINGLE_EMISSION_FIT		"singleEmisFit"     // wip
//#define COUNTING_MODE_MULTI_EMISSION_FIT		"multiEmisFit"      // wip

#define COUNTING_CMD_MODE						"cmdMode"
#define COUNTING_BOUNDLE_SIZE					"boundleSize"
#define COUNTING_BOUNDLE_COUNTING_MODE			"boundleCountingMode"
#define COUNTING_CLASS                          "class"
#define COUNTING_WAY							"way"
#define COUNTING_SESSION						"session"
#define COUNTING_CAT						    "cat"
#define COUNTING_FIT						    "fit"
#define COUNTING_NOTE						    "note"


#define COUNTING_REPORT							"countReport"
#define COUNTING_TOTAL_NOTE						"totalNoteCounted"
#define COUNTING_REFUSED						"refused"
#define COUNTING_UNRECOGNIZED					"unrecognized"
#define COUNTING_NOTE_REPORT_ELEMENTS			"noteReportElements"
#define COUNTING_NOTE_REPORT					"noteReport"
#define COUNTING_NOTE_TYPE						"noteType"
#define COUNTING_NUMBER_NOTE					"numberNote"
#define COUNTING_DENOM_ID						"denomID"

//--------------------------- Defines for ARCASampling -------------------------------
#define SAMPLING_INIT							"init"
#define SAMPLING_UNDO_LN						"undoLastNote"
#define SAMPLING_UNDO_B							"undoLastBatch"
#define SAMPLING_UNDO_SESSION					"undoSession"
#define SAMPLING_ACCEPT							"accept"
#define SAMPLING_LIST							"list"

#define SAMPLING_MODE							"mode"
#define SAMPLING_SESSION						"session"

#define CAT2									"cat2"
#define CAT3									"cat3"
#define CAT4									"cat4"

#define FIT										"fit"
#define UNFIT									"unfit"
//--------------------------- Defines for Dispense ----------------------------

#define DISPENSE_HEADER							"Dispense"
#define DISPENSE_CMD_MODE						"cmdMode"
#define DISPENSE_NOTE_DISPENSE					"noteDispense"
#define DISPENSE_DENOMINATION_ID				"denominationID"
#define DISPENSE_CASSETTE_ID					"cassetteID"
#define DISPENSE_NUMBER_NOTE					"numberNote"
#define DISPENSE_MODE							"alternateDispense"
#define DISPENSE_MODE_ALTERNATE					"timeDispense"

#define DISPENSE_TOTAL							"totalNotesDispensed"
#define DISPENSE_NOTE_REPORT_ELEMENTS			"noteReportElements"
#define DISPENSE_NOTE_REPORT					"noteReport"

//--------------------------- Defines for Empty Cassette ------------------------
#define EMPTY_CASSETTE_DIM_MIN_BUFFER_OUTPUT	1024
#define EMPTY_CASSETTE_HEADER					"EmptyCassette"

#define EMPTY_CASSETTE_CMD_MODE					"cmdMode"
#define EMPTY_CASSETTE_ID						"cassetteID"
//#define EMPTY_CASSETTE_PASSWORD					"password"
#define EMPTY_CASSETTE_BOUNDLE_SIZE				"outBoundleSize"

#define EMPTY_CASSETTE_OPERATION_CODE			"operationCode"
#define EMPTY_CASSETTE_START_NOTES				"startingNotes"
#define EMPTY_CASSETTE_OUTPUT_NOTES				"outputNotes"

//--------------------------- Defines for Device Date Time ------------------------
#define DEVICE_DATETIME_HEADER					"DeviceDateTime"

#define DEVICE_DATETIME_TIME_SECONDS			"seconds"
#define DEVICE_DATETIME_TIME_MINUTES			"minutes"
#define DEVICE_DATETIME_TIME_HOURS				"hours"
#define DEVICE_DATETIME_WEEK_DAY				"weekDay"
#define DEVICE_DATETIME_WEEK_DAY_MONDAY			"Monday"
#define DEVICE_DATETIME_WEEK_DAY_TUESDAY		"Tuesday"
#define DEVICE_DATETIME_WEEK_DAY_WEDNESDAY		"Wednesday"
#define DEVICE_DATETIME_WEEK_DAY_THURSDAY		"Thursday"
#define DEVICE_DATETIME_WEEK_DAY_FRIDAY			"Friday"
#define DEVICE_DATETIME_WEEK_DAY_SATURDAY		"Saturday"
#define DEVICE_DATETIME_WEEK_DAY_SUNDAY			"Sunday"
#define DEVICE_DATETIME_DATE_DAY				"day"
#define DEVICE_DATETIME_DATE_MONTH				"month"
#define DEVICE_DATETIME_DATE_YEAR				"year"

//--------------------------- Defines for Unit Status -------------------------
#define UNIT_STATUS_HEADER						"UnitStatus"

#define UNIT_STATUS_STATUS_TYPE					"statusType" 
#define UNIT_STATUS_STATUS_POCKET				"pocketsAndCover" 
#define UNIT_STATUS_STATUS_EXTEND				"extended" 

#define UNIT_STATUS_FEEDER						"feeder"
#define UNIT_STATUS_RTC							"RTC"
#define UNIT_STATUS_NOTE_READER					"noteReader"
#define UNIT_STATUS_SAFE_CONTROLLER				"safeController"
#define UNIT_CASSETTE_CODE						"cassetteCode"
#define UNIT_STATUS_CODE						"statusCode"
#define UNIT_STATUS_DESCRIPTION					"description"

#define SLOT_STATUS_OK							"Status OK"
#define SLOT_STATUS_HEADER						"slotStatus"
#define SLOT_STATUS_SAFE_DOOR					"safeDoor"
#define SLOT_STATUS_UPPER_COVER					"cover"
#define SLOT_STATUS_FEEDER						"feeder"
#define SLOT_STATUS_INPUT_SLOT					"inputSlot"
#define SLOT_STATUS_REJECT_SLOT					"rejectSlot"
#define SLOT_STATUS_LEFT_SLOT					"leftSlot"
#define SLOT_STATUS_RIGHT_SLOT					"rightSlot"
#define SLOT_STATUS_LEFT_LIGHT					"leftexternbutton"
#define SLOT_STATUS_RIGHT_LIGHT					"rightexternalbutton"
#define SLOT_STATUS_FKP_POCKET					"Cat2Boxpresent"
#define SLOT_STATUS_FKS_POCKET					"Cat2Boxstatus"
#define SLOT_STATUS_CLOSED						"closed"
#define SLOT_STATUS_OPEN						"open"
#define SLOT_STATUS_EMPTY						"empty"
#define SLOT_STATUS_NOT_EMPTY					"not empty"
#define SLOT_STATUS_LIGHT_OFF					"light off"
#define SLOT_STATUS_LIGHT_ON					"ligth on"
#define SLOT_STATUS_PRESENT						"present"
#define SLOT_STATUS_NOT_PRESENT					"not present"

//--------------------------- Defines for Unit Info Configuration -------------
#define UNIT_CONFIG_HEADER						"UnitInfoConfiguration"

#define UNIT_CONFIG_MODEL						"model"
#define UNIT_CONFIG_SERIAL_NUMBER				"serialNumber"

//--------------------------- Defines for ARCAgetDeviceOutputNotes -------------
#define OUTPUT_NOTE_REPORT						"NoteReport"
#define OUTPUT_NOTE_REPORT_ELEMENTS				"NoteReportElements"
#define OUTPUT_NOTE_TYPE						"noteType"
#define OUTPUT_NOTE_NR_NOTES					"numberNote"
#define OUTPUT_TYPE_UNFIT						"unfit"


//--------------------------- Defines for ARCAgetCassetteSettings -------------
#define READER_GET_CONFIG_BANK					"bank"
#define READER_GET_CONFIG_BANKNAME				"bankname"
#define READER_GET_CONFIG_NUM_BANK				"numbank"
#define READER_GET_CONFIG_NUM_ENA				"enabled"
#define READER_GET_CONFIG_HEADER				"ReaderConfig"
#define READER_GET_CONFIG_MESSAGE				"Message"
#define READER_GET_CONFIG_HEADER_NID			"Denom_Nid"
#define READER_GET_CONFIG_HEADER_VV				"Denom_Height"
#define READER_GET_CONFIG_HEADER_DG				"Denom_Version"
#define READER_GET_CONFIG_HEADER_EN				"Denom_Enable"
#define READER_GET_CONFIG_HEADER_CASSETE		"Cassette_Enable"
#define READER_GET_CONFIG_HEADER				"ReaderConfig"
#define READER_GET_CONFIG_ACTIVE_BANK			"activeBank"
#define READER_DENOM_GET_CONFIG_HEADER			"Denom"

//--------------------------- Defines for ARCAexeDeviceReset -------------
#define OPERATION_RESET_TYPE					"mode"
#define OPERATION_RESET_TYPE_FORCED				"forced"

//--------------------------- Defines for ARCAgetReaderVersion -------------
#define OPERATION_READER_VERSION_TYPE			"VersionType"
#define OPERATION_READER_VERSION_TYPE_ECB		"ECB"
#define OPERATION_READER_VERSION_TYPE_ECB_H		"ECB_header"

//--------------------------- Defines for ARCAgetDeviceType -------------
#define	OPERATION_REF							"References"
#define OPERATION_REF_BANK_ID					"bankId"
#define OPERATION_REF_BANK_VER					"ver"
#define OPERATION_REF_BANK_NAME					"ref_name"



				
//--------------------------- Defines for ARCAgetReaderStatistics -------------
#define SLOT_STATISTIC_DESCRIPTION				"description"
#define SLOT_STATISTIC_COUNT					"count"

#define SLOT_STATISTIC_NPROC					"nProc"
#define SLOT_STATISTIC_NACCEPT					"nAccept"
#define SLOT_STATISTIC_NREFUSED					"nRefused"
#define SLOT_STATISTIC_NSUSP					"nSusp"
#define SLOT_STATISTIC_NOT_AUTH					"nNotAuth"
#define SLOT_STATISTIC_NCLSF					"nClsf"
#define SLOT_STATISTIC_NRID						"nIrd"
#define SLOT_STATISTIC_NMAG						"nMag"
#define SLOT_STATISTIC_NFORMAT					"nFormat"
#define SLOT_STATISTIC_NUNFIT					"nUnfit"
#define SLOT_STATISTIC_NOVERRUN					"nOverrun"
#define SLOT_STATISTIC_NDISABLECHAIN			"nDisableChain"
#define SLOT_STATISTIC_NUV						"nUv"
#define SLOT_STATISTIC_NEXTREMEFIT				"nExtremeFit"
#define SLOT_STATISTIC_NGENERROR				"nGenericError"
#define SLOT_STATISTIC_NOCRCHECK				"nOcrCheck"

#define SLOT_STATISTIC_NSKEW					"nSkew"
#define SLOT_STATISTIC_NDOUBLE					"nDouble"
#define SLOT_STATISTIC_NDIMENSION				"nDimension"
#define SLOT_STATISTIC_NABORT					"nAbort"
#define SLOT_STATISTIC_NLOCALIZZ				"nLocalizz"
#define SLOT_STATISTIC_NOTHER					"nOther"
#define SLOT_STATISTIC_NACCFIT					"nAccFit"
#define SLOT_STATISTIC_NACCUFIT					"nAccUnFit"
#define SLOT_STATISTIC_NRECJBYRTC				"nRejRtc"
#define SLOT_STATISTIC_TOO_LONG					"nTooLong"
#define SLOT_STATISTIC_NROUTPUTBYRTC			"nOutRtc"
//--------------------------- Defines for ARCAgetCassetteSettings -------------
//IN
#define GET_CASSETTE_NUMBER						"cassetteNumber"
#define PROTOCOL_CASSETTE_NR					"protocolCassetteNumber"
#define PROTOCOL_CASSETTE_NR_SERVICE			"serviceProtocolCassetteNumber"
#define CASSETTE_NUMBER							"numCassette"
#define PROTOCOL_DENOMINATION					"denominations"
#define PROTOCOL_REFERENCES						"references"
//OUT
#define CAS_ID_MULTI_DENOM						"cassetteID"

//--------------------------- Defines for ARCAsetCassetteSettings -------------
#define OPERATION_MODULE_CASSETTE				"cassette"
#define OPERATION_MODULE_CASSETTE_INIT			"cassetteInit"
#define OPERATION_MODULE_CASSETTE_CASID			"cas_id"
#define OPERATION_MODULE_CASSETTE_CASSN			"serial_num"
#define OPERATION_MODULE_CASSETTE_CASCODE		"code"
#define OPERATION_MODULE_CASSETTE_INIT_TARGET	"TargetToinitialize"
#define OPERATION_MODULE_CASSETTE_INIT_KEY		"code"
#define TARGET_CASSETTE_ID						"cassetteID"
#define TARGET_CASSETTE_DENOMINATION			"Denomination"
#define OPERATION_CONFIG_TYPE_SWITCH			"switch"
#define OPERATION_CONFIG_TYPE_CONFIG			"config"
#define CONFIG_CASSETTE_STATUS					"cassetteStatus"
#define CONFIG_CASSETTE_ENABLED 				"enable"
#define CONFIG_CASSETTE_DISABLED 				"disable"

//--------------------------- Defines for ARCAgetDeviceVersion -------------
//IN
#define DEVICE_VERSION_TYPE						"versionType"
#define DEVICE_VERSION_TYPE_UNIT_ID				"UnitIdentification"
#define DEVICE_VERSION_TYPE_OPTIONS				"Options"

#define DEVICE_VERSION_TYPE_RTC					"rtc"
#define DEVICE_VERSION_TYPE_IDENTIFIER			"reader"
#define DEVICE_VERSION_TYPE_SAFE				"safe"
#define DEVICE_VERSION_TYPE_FPGA				"fpga"
#define DEVICE_VERSION_TYPE_OSC					"osc"   //only for Novus
#define DEVICE_VERSION_TYPE_SHUTTER				"Shutter"
#define DEVICE_VERSION_TYPE_CASSETTE_A			"cassetteA"
#define DEVICE_VERSION_TYPE_CASSETTE_B			"cassetteB"
#define DEVICE_VERSION_TYPE_CASSETTE_C			"cassetteC"	
#define DEVICE_VERSION_TYPE_CASSETTE_D			"cassetteD"	
#define DEVICE_VERSION_TYPE_CASSETTE_E			"cassetteE"	
#define DEVICE_VERSION_TYPE_CASSETTE_F			"cassetteF"	
#define DEVICE_VERSION_TYPE_CASSETTE_G			"cassetteG"	
#define DEVICE_VERSION_TYPE_CASSETTE_H			"cassetteH"	
#define DEVICE_VERSION_TYPE_CASSETTE_I			"cassetteI"	
#define DEVICE_VERSION_TYPE_CASSETTE_J			"cassetteJ"	
#define DEVICE_VERSION_TYPE_CASSETTE_K			"cassetteK"	
#define DEVICE_VERSION_TYPE_CASSETTE_L			"cassetteL"	

#define DEVICE_VERSION_TYPE_CASSETTE_A_SB			"cassetteSensorBoardA"
#define DEVICE_VERSION_TYPE_CASSETTE_B_SB			"cassetteSensorBoardB"
#define DEVICE_VERSION_TYPE_CASSETTE_C_SB			"cassetteSensorBoardC"	
#define DEVICE_VERSION_TYPE_CASSETTE_D_SB			"cassetteSensorBoardD"	
#define DEVICE_VERSION_TYPE_CASSETTE_E_SB			"cassetteSensorBoardE"	
#define DEVICE_VERSION_TYPE_CASSETTE_F_SB			"cassetteSensorBoardF"	
#define DEVICE_VERSION_TYPE_CASSETTE_G_SB			"cassetteSensorBoardG"	
#define DEVICE_VERSION_TYPE_CASSETTE_H_SB			"cassetteSensorBoardH"	
#define DEVICE_VERSION_TYPE_CASSETTE_I_SB			"cassetteSensorBoardI"	
#define DEVICE_VERSION_TYPE_CASSETTE_J_SB			"cassetteSensorBoardJ"	
#define DEVICE_VERSION_TYPE_CASSETTE_K_SB			"cassetteSensorBoardK"	
#define DEVICE_VERSION_TYPE_CASSETTE_L_SB			"cassetteSensorBoardL"	

#define DEVICE_VERSION_TYPE_BOOT_V				"bootLoader"	
#define DEVICE_VERSION_TYPE_WCE_V				"WCE"	
#define DEVICE_VERSION_TYPE_DLL_V				"xdll"
#define DEVICE_VERSION_TYPE_XAPP_V				"xapp"

#define DEVICE_VERSION_TYPE_READER_HOST_V		"Host_reader"
#define DEVICE_VERSION_TYPE_READER_DPS_V		"Dsp_reader"
#define DEVICE_VERSION_TYPE_READER_FPGA_V		"Fpga_reader"
#define DEVICE_VERSION_TYPE_READER_TAPE_V		"Tape_reader"
#define DEVICE_VERSION_TYPE_READER_MAGNETIC_V	"Magnetic_reader"
#define DEVICE_VERSION_NAME_CONTROLLER			"controller_name"
#define DEVICE_VERSION_SUITE					"suite_name"
#define DEVICE_VERSION_PROTOCOL_INTERFACE		"protocolInterface"
#define DEVICE_VERSION_UI						"userInterface"
#define DEVICE_VERSION_DISPLAY					"displayVersion"
#define DEVICE_VERSION							"version"
#define DEVICE_VERSION_ALARM					"alarm"
#define DEVICE_VERSION_PH01						"ph01"
#define DEVICE_VERSION_PH02						"ph02"
#define DEVICE_VERSION_USB_1					"usbModuleA"
#define DEVICE_VERSION_USB_2					"usbModuleB"
#define DEVICE_VERSION_DIGITAL_DISTRIBUTION		"digitalDistribution"
#define DEVICE_VERSION_OS						"osVersion"
#define DEVICE_VERSION_ASSET					"assetVersion"
#define DEVICE_VERSION_CDF						"cdfName"

//OUT
#define DEVICE_VERSION_NAME						"name"
#define DEVICE_VERSION_COMP_SW					"ver_sw"
#define DEVICE_VERSION_COMP_HW					"ver_hw"
#define DEVICE_VERSION_SN						"serial_num"
#define DEVICE_MODULE							"module"
#define DEVICE_MODULE_NAME						"name"
#define DEVICE_MODULE_VERSION					"ver"
#define DEVICE_MODULE_RELEASE					"rel"
#define DEVICE_VERSION_CONTROLLER				"controller"
#define DEVICE_VERSION_FEEDER					"feeder"
#define DEVICE_VERSION_RTC						"rtc"
#define DEVICE_VERSION_IDENTIFIER				"identifier"
#define DEVICE_VERSION_SAFE						"safe"
#define DEVICE_VERSION_FPGA						"fpga"
#define DEVICE_VERSION_XDLL						"xDll"
#define DEVICE_VERSION_USB_A					"usbA"
#define DEVICE_VERSION_USB_B					"usbB"
#define DEVICE_MODULE_COMP_VERSION				"ddVersion" //only for Novus


//--------------------------- Defines for ARCAsetConfigReader -------------
#define SET_CONFIG_DENOMINATION					"denomination"
#define SET_CONFIG_DENOMINATION_STATUS			"denominationStatus"
#define SET_CONFIG_ENABLE_TARGET				"Target"
#define SET_CONFIG_MESSAGE						"Message"

//--------------------------- Defines for ARCAgetDeviceHistory -------------
#define GETLOG_CONFIG_TYPE						"type"
#define GETLOG_CONFIG_TYPE_ERROR_LOG			"errorlog"
#define GETLOG_RECORD_NUMBER					"lineNumber"

#define GET_LOG_HEADER							"logHeader"
#define GETLOG_RECORD_LIFE						"life_num"
#define GETLOG_RECORD_USER						"user"
#define GETLOG_RECORD_T_OPERATION				"typeofoperation"
#define GETLOG_RECORD_HLEVEL_RC					"highlevelRc"
#define GETLOG_RECORD_FEEDER_MODULE				"feederStatus"
#define GETLOG_RECORD_READER_MODULE				"ReaderStatus"
#define GETLOG_RECORD_SAFE_MODULE				"safeStatus"
#define GETLOG_RECORD_CASSETTE_A_MODULE			"cassetteAStatus"
#define GETLOG_RECORD_CASSETTE_B_MODULE			"cassetteBStatus"
#define GETLOG_RECORD_CASSETTE_C_MODULE			"cassetteCStatus"
#define GETLOG_RECORD_CASSETTE_D_MODULE			"cassetteDStatus"
#define GETLOG_RECORD_CASSETTE_E_MODULE			"cassetteEStatus"
#define GETLOG_RECORD_CASSETTE_F_MODULE			"cassetteFStatus"
#define GETLOG_RECORD_CASSETTE_G_MODULE			"cassetteGStatus"
#define GETLOG_RECORD_CASSETTE_H_MODULE			"cassetteHStatus"
#define GETLOG_RECORD_CASSETTE_I_MODULE			"cassetteIStatus"
#define GETLOG_RECORD_CASSETTE_L_MODULE			"cassetteLStatus"
#define GETLOG_RECORD_CASSETTE_M_MODULE			"cassetteMStatus"
#define GETLOG_RECORD_CASSETTE_N_MODULE			"cassetteNStatus"

//--------------------------- Defines for ARCAgetMacAddress -------------

#define MAC_ADDRESS_SN							"MacAddress"

//--------------------------- Defines for ARCAexeSecurity -------------
#define SECURITY_AREA							"area"
#define SECURITY_UPPER							"upperTransport"
#define SECURITY_OPERATION						"operation"
#define SECURITY_OPERATION_LOCK					"lock"
#define SECURITY_OPERATION_UNLOCK				"unlock"




//--------------------------- Defines for ARCAgetDeviceSettings -------------
#define HEADER_CONFIG							"Config"
#define OPTION_DATE_DISPLAY						"viewDateTimeOnDisplay"
#define OPTION_BALANCED_CASSETTE				"balancedCassetteHandling"
#define OPTION_ALARM_1							"alarm1Handling"
#define OPTION_DELAY							"delayClassDispensing"
#define OPTION_DATE_DISPLAY_AM_PM				"viewDateTimeOnDisplayInFormatAMandPM"
#define OPTION_UNFIT_SAFE						"useUnfitinSafe"
#define OPTION_JOURNAL_LOG						"enableJournallog"

#define HEADER_OPTIONCONFIG						"OptionConfig"
#define OPTION_SPECIAL_CLEAN					"specialCleanWithoutControl"
#define OPTION_SPECIAL_ALARM					"specialAlarmMode"
#define OPTION_SPECIAL_PROTOCOL					"simplifiedProtocol"
#define OPTION_SPECIAL_CLOSE					"executeCloseCommandAlsoInJam"


#define HEADER_OPTION_ONE_CONFIG				"OptionOneConfig"
#define OPTION_FORCE_RECOVERY					"forceRecoveryWithTestCommand"
#define OPTION_JAM_SPECIAL_CLEAN				"donotremovecassettejamifspecialcleanON"
#define OPTION_JAM_SPECIAL_CLEAN_SP				"doNotRemoveCassetteJamIfSpecialCleanON"
#define OPTION_LAN_TIME_OUT						"lanTimeout"
#define OPTION_TCDA								"timeControlDispenseamount"
#define OPTION_BOOKING_EXTERNAL					"enableBookingExternalButton"

#define HEADER_OPTION_TWO_CONFIG				"OptionTwoConfig"
#define OPTION_SAFE_40MM						"safeCen40Bit"
#define OPTION_BVU_HANDLING						"bvuSynchronousHandling"
#define OPTION_FACE_ORIENTATION					"faceOrientationHandling"
#define OPTION_SPECIAL_CLEAN_DISPLAY			"specialCleanUsingDisplay"
#define OPTION_CAT2BOX_HANDLING					"cat2BoxHandling"
#define OPTION_EVOLED_HANDLING					"evoLedHandling"
#define OPTION_SEPARATE							"separateCat2andCat3"
#define OPTION_PUSH_BN							"feedBanknoteswithInputBinFree"
#define OPTION_DIFFERENT_CENTER					"differentCenterAUDbn"
#define OPTION_DISABLE_MESSAGE					"disableMessageonDisplay"
#define OPTION_ALARM2							"alarm2OutputHandling"
#define OPTION_REMAP_JAM						"remapFeederJamtoMissfeeding"
#define OPTION_VIRTUAL_D_ENV					"virtualDestkopEnvironment"
#define OPTION_NEW_LED_SIDE						"handlingNewLedSide"
#define OPTION_DISABLE_LED						"disableCM18evoLedPulse"
#define OPTION_RTC_COMPATIBILITY				"RtcModuleNameCompatibility"

#define OPERATION_TYPE							"operationType"
#define OPERATION_NUMBER						"number"
#define OPERATION_CONFIGURATION					"configuration"

//--------------------------- Defines for ARCAgetReaderThreshold -------------
//IN
#define FIT_CONFIGURATION						"fitConfiguration"
#define FIT_THRESHOLD							"fitThreshold"
#define FIT_VERSION								"fitVersion"

//OUT
#define THRESHOLD_FIT_FLAG						"fitnessFlag"
#define THRESHOLD_FIT_THRESHOLD					"fitnessThreshold"
#define THRESHOLD_FITNESS_V						"fitnessVer"

//--------------------------- Defines for ARCAgetReaderCurrency-------------
#define READER_GET_SLOT							"slot"

#define THRESHOLD_DSP_0							"dps_0"
#define THRESHOLD_DSP_1							"dps_1"
#define THRESHOLD_DSP_2							"dps_2"
#define THRESHOLD_DSP_3							"dps_3"




//--------------------------- Defines for ARCAsetAlarm -------------
#define OPERATION_MODULE_ALARM					"alarmID"
#define OPERATION_MODULE_ALARM_STATUS			"alarmStatus"

#define	OPERATION_MODULE_ALARM_1				"alarm1"
#define	OPERATION_MODULE_ALARM_2				"alarm2"
#define	OPERATION_MODULE_ALARM_SILENT			"alarmSilent"


//--------------------------- Defines for ARCAgetDelays -------------
//OUT
#define OPERATION_DELAY_VALUE					"delay"
#define OPERATION_DELAY_AMOUNT      			"amount"

//--------------------------- Defines for ARCAgetReaderIdentifier() -------------
#define READER_VERSION							"version"
#define READER_CDFNAME							"cdfName"

//--------------------------- Defines for ARCAexeAlarm() -------------
#define ALARM_PWD_LOCK							"passwordLock"
#define ALARM_TEMP_LOCK							"temporaryLock"
#define LOCK_MODE								"lockMode"
#define LOCK_ON									"lockOn"
#define LOCK_OFF								"lockOff"
#define TEMPORARY_LOCK_TIME						"lockTime"

//--------------------------- Defines for ARCAsetDelays()-----------------------------
#define OPERATION_DELAY_AMOUNT					"amount"
#define OPERATION_DELAY							"delay"

//--------------------------- Defines for ARCAexeTraceTransfer()-----------------------------
#define FILE_TRANSFER_TYPE						"TransferType"
#define FILE_TRANSFER_MODE						"TransferMode"
#define FILE_TRANSFER_FILE						"TransferFile"
#define FILE_TRANSFER_DOWNLOAD					"Download"
#define FILE_TRANSFER_UPLOAD					"Upload"
#define FILE_TRANSFER_DOWNLOAD_INIT				"Init"
#define FILE_TRANSFER_DOWNLOAD_TRANSFER			"Transfer"
#define FILE_TRANSFER_DOWNLOAD_END				"End"
#define FILE_TRANSFER_DOWNLOAD_ABORT			"Abort"
#define FILE_TRANSFER_DOWNLOAD_SERVICE			"Service"
#define FILE_TRANSFER_FILE_LOG					"Log"
#define FILE_TRANSFER_FILE_SUITE				"Suite"
#define FILE_TRANSFER_FILE_MODULE				"Module"
#define FILE_TRANSFER_FILE_LOG_NAME				"ZipfileName"

//--------------------------- Defines for ARCAgetPocketOutput()-----------------------------
#define BUNDLE_SIZE								"bundleSize"

//--------------------------- Defines for ARCAsetPocketOutput()-----------------------------

#define OPERATION_SET_OUTPUT_SETTING_BUNDLE		"withdrawalBundleSize"
#define OPERATION_INPUT_BIN_VALUE				"numnotes"

//--------------------------- Defines for ARCAsetLogServiceRecord()-----------------------------
#define OPERATION_DEVICE_LOG					"log"
#define OPERATION_DEVICE_LOG_VALUE				"ServiceText"
#define OPERATION_DEVICE_LOG_INIT				"IntiJournal"
#define OPERATION_DEVICE_LOG_ERASE				"EraseAllData"
#define OPERATION_DEVICE_SERVICE_1				"nn1"
#define OPERATION_DEVICE_SERVICE_2				"nn2"
#define OPERATION_DEVICE_SERVICE_3				"nn3"
#define OPERATION_DEVICE_SERVICE_4				"nn4"
#define OPERATION_DEVICE_SERVICE_5				"nn5"
#define OPERATION_DEVICE_SERVICE_6				"nn6"
#define OPERATION_DEVICE_SERVICE_7				"nn7"
#define OPERATION_DEVICE_SERVICE_8				"nn8"
#define OPERATION_DEVICE_SERVICE_9				"nn9"
#define OPERATION_DEVICE_SERVICE_A				"nna"
#define OPERATION_DEVICE_SERVICE_B				"nnb"
#define OPERATION_DEVICE_SERVICE_C				"nnc"
#define OPERATION_DEVICE_SERVICE_D				"nnd"
#define OPERATION_DEVICE_SERVICE_E				"nne"
#define OPERATION_DEVICE_SERVICE_F				"nnf"
#define OPERATION_DEVICE_SERVICE_G				"nng"
#define OPERATION_DEVICE_SERVICE_H				"nnh"
#define OPERATION_DEVICE_SERVICE_I				"nni"
#define OPERATION_DEVICE_SERVICE_J				"nnj"
#define OPERATION_DEVICE_SERVICE_K				"nnk"

////--------------------------- Defines for ARCAgetDeviceType-------------------------------------------------
//IN
#define OPERATION_TYPE_CM					"value"
#define OPERATION_TYPE_CMD_TYPE				"type"
#define OPERATION_TYPE_VERSION				"version"
#define OPERATION_TYPE_MACHINE				"machine" 
#define OPERATION_TYPE_MACHINE_EXT			"machineExtended" 
#define OPERATION_TYPE_CM18   				"CM18"
#define OPERATION_TYPE_CM18T				"CM18T"
#define OPERATION_TYPE_CM18E				"CM18E"
#define OPERATION_TYPE_EVO					"EVO"
#define OPERATION_TYPE_STD					"STD"

//OUT
#define OPERATION_TYPE_PROTOCOL				"protocolInterface" 
#define OPERATION_TYPE_NAME					"name" 
#define OPERATION_TYPE_TYPE					"type" 
#define OPERATION_TYPE_MUM_PROT_CAS			"numProtoCassette" 
#define OPERATION_TYPE_MUM_BAG				"numBag" 
#define OPERATION_TYPE_MUM_PROTO_BAG		"numProtoBag" 
#define OPERATION_TYPE_MUM_CD80				"numCD80"
#define OPERATION_TYPE_MODE					"mode"
#define OPERATION_TYPE_LED					"led"

//--------------------------- Defines for ARCAgetModuleSerialNumber -------------
//IN
#define UNIT_MODULE_NAME						"moduleName"
//OUT
#define UNIT_MODULE_SERIAL_NUMBER				"serialNumber"

//--------------------------- Defines for ARCAexeCoinDispense -------------
//IN
#define IN_COIN_AMOUNT "coinAmount"
#define IN_COIN_DENOMINATION "coinDenomination"

#define COIN_DISPENSE_DENOMINATION_ID "denominationID"
#define COIN_DISPENSE_ID_VALUE "value"
//OUT
#define COIN_STATUS_BYTE "statusByte"
#define COIN_AMOUNT "amount"
#define COIN_DENOMINATION "denomination"
//--------------------------- Defines for ARCACoinInventory -------------
//IN
#define COIN_CLEAR_INVENTORY "clear"
#define COIN_LOAD_INVENTORY "load"
//--------------------------- Defines for ARCAgetCoinConfiguration -------------
//IN
#define IN_COIN_PARAM "coinParamId"
//OUT
#define IN_COIN_PARAM_VALUE "coinParamValue"

//--------------------------- Defines for ARCAgetCoinCashData -------------
#define COIN_AVAILABLE_AMOUNT "availableAmount"
#define COIN_AVAILABLE_DISPENSE "dispensedAmount"
#define COIN_CLEAR_CASH_BALANCE "clearCashBalance"

//--------------------------- Defines for ARCAgetCassettePhysicalPosition -------------
//OUT
#define CASSETTE_POSITION      "position"

//--------------------------- Defines for ARCAgetBanknoteSerialNumber -------------
//IN
#define BANKNOTE_NUMBER						"numberBkn"
#define BANKNOTE_DENOM						"denomBkn"

//OUT
#define TYPE_CAT							"typeCat"
#define TYPE_OCR							"typeOcr"
#define TYPE_SN								"serialNumber"

//--------------------------- Defines for ARCAgetAudioSettings -------------
#define TEST_AUDIO								"testAudio"
#define GET_AUDIO								"getAudioValue"

//OUT
#define AUDIO_VALUE								"audioValue"


//--------------------------- Defines for ARCAexeDeviceBell -------------
#define NUM_BEEP								"numBeep"

// ------------------------------------------------------------------------
//                      REPLY-CODE
// ------------------------------------------------------------------------
#define AR_OKAY										0
// ------------------------------------------------------------------------
//                      ERRORS - WARNING
// ------------------------------------------------------------------------
#define AR_BOOT_MODE								100
#define AR_UNIT_BUSY								102
#define AR_COVER_OPEN								103
#define AR_SAFE_OPEN								104
#define	AR_FEEDER_OPEN								105
#define AR_OUT_OF_SERVICE							106
#define AR_DEPOSIT_OUT_OF_SERVICE					107
#define AR_SAFE_OUT_OF_SERVICE						108
#define	AR_LOCKED									109
#define AR_CASSETTE_UNLOCK							110
#define AR_SW_SYNTAX_ERROR							201
#define AR_SW_ERROR									202
#define AR_WRONG_FW									203
#define AR_NOTE_IN_RIGHT_OUTPUT						204
#define AR_NOTE_IN_LEFT_OUTPUT						205
#define AR_NOTE_IN_REJECT_POCKET					206
#define AR_INPUT_FEEDER_EMPTY						207
#define AR_NOTE_ON_FEEDER_INPUT						208
#define AR_CASSETTE_FULL						    209
#define AR_DENOMINATION_EMPTY						210
#define AR_WRONG_KEY								211
#define AR_NO_CASSETTE_ENABLE						212
#define AR_WRONG_SIDE								213
#define AR_REQ_OVER_LIMIT							214
#define AR_CASSETTE_NOT_PRESENT						215
#define AR_CASSETTE_NOT_EMPTY						216
#define AR_NID_NOT_PRESENT							217
#define AR_NO_JOURNAL								218
#define AR_RECORD_NOT_PRESENT						219
#define AR_STARTING_REBOOT							220
#define AR_UNEXPECTED_CMD							221
#define AR_BOOKING_NOT_ACTIVE						222
#define AR_COIN_ERROR								223
#define AR_CAT_2_BOX_FULL							230
#define AR_CAT_2_BOX_NOT_PRESENT					231
#define AR_CALIBRATE_SENSOR_ERROR					238

#define AR_FEEDER_JAM								301
#define AR_IDENTIFY_JAM								302
#define AR_UPPER_TRACK_JAM							303
#define AR_CROSS_POINT_JAM							304
#define AR_LOWER_TRACK_JAM							305
#define AR_OUTPUT_TRACK_JAM							306
#define AR_CASSETTE_A_JAM							307
#define AR_CASSETTE_B_JAM							308
#define AR_CASSETTE_C_JAM							309
#define AR_CASSETTE_D_JAM							310
#define AR_CASSETTE_E_JAM							311
#define AR_CASSETTE_F_JAM							312
#define AR_CASSETTE_G_JAM							313
#define AR_CASSETTE_H_JAM							314
#define AR_CASSETTE_I_JAM							315
#define AR_CASSETTE_J_JAM							316
#define AR_MISSING_NOTE_CASSETTE_A					319
#define AR_MISSING_NOTE_CASSETTE_B					320
#define AR_MISSING_NOTE_CASSETTE_C					321
#define AR_MISSING_NOTE_CASSETTE_D					322
#define AR_MISSING_NOTE_CASSETTE_E					323
#define AR_MISSING_NOTE_CASSETTE_F					324
#define AR_MISSING_NOTE_CASSETTE_G					325
#define AR_MISSING_NOTE_CASSETTE_H					326
#define AR_MISSING_NOTE_CASSETTE_I					327
#define AR_MISSING_NOTE_CASSETTE_J					328
#define AR_LOWER_TRACK_JAM_CD80						330
#define AR_CASSETTE_K_JAM							331
#define AR_CASSETTE_L_JAM							332
#define AR_CASSETTE_M_JAM							333
#define AR_CASSETTE_N_JAM							334
#define AR_CASSETTE_O_JAM							335
#define AR_CASSETTE_P_JAM							336
#define AR_MISSING_NOTE_CASSETTE_K					341
#define AR_MISSING_NOTE_CASSETTE_L					342
#define AR_MISSING_NOTE_CASSETTE_M					343
#define AR_MISSING_NOTE_CASSETTE_N					344
#define AR_MISSING_NOTE_CASSETTE_O					345
#define AR_MISSING_NOTE_CASSETTE_P					346
#define AR_MISFEEDING								401
#define AR_CASSETTE_A_OUT_OF_SERVICE				402
#define AR_CASSETTE_B_OUT_OF_SERVICE				403
#define AR_CASSETTE_C_OUT_OF_SERVICE				404
#define AR_CASSETTE_D_OUT_OF_SERVICE				405
#define AR_CASSETTE_E_OUT_OF_SERVICE				406
#define AR_CASSETTE_F_OUT_OF_SERVICE				407
#define AR_CASSETTE_G_OUT_OF_SERVICE				408
#define AR_CASSETTE_H_OUT_OF_SERVICE				409
#define AR_CASSETTE_I_OUT_OF_SERVICE				410
#define AR_CASSETTE_J_OUT_OF_SERVICE				411
#define AR_NOTE_OVERFLOW							415
#define AR_COUNT_ERROR								416
#define AR_FEEDER_WRONG_STATUS						417
#define AR_READER_COMM_ERROR						418
#define AR_SAFE_COMM_ERROR							419
#define AR_SAFE_FAILURE								420
#define AR_CASSETTE_K_OUT_OF_SERVICE				421
#define AR_CASSETTE_L_OUT_OF_SERVICE				422
#define AR_CASSETTE_M_OUT_OF_SERVICE				423
#define AR_CASSETTE_N_OUT_OF_SERVICE				424
#define AR_CASSETTE_O_OUT_OF_SERVICE				425
#define AR_CASSETTE_P_OUT_OF_SERVICE				426
#define AR_SHUTTER_FAILURE							429
#define AR_UPPER_LOCK_FAILURE						430

#define AR_BCC_ERROR								921
// ------------------------------------------------------------------------
//                      ERRORS - LIBRARY
// ------------------------------------------------------------------------
#define AR_LINK_SYSTEM_ERR							1050	//"Function communication device error"
#define AR_LINK_CONNECTION_ERR						1051	//"Invalid handle"
#define AR_LINK_ALREADY_CONNECTED_ERR				1052	//"Connection already connected"
#define AR_LINK_CONNECT_PARAMETERS_ERR				1053	//"Syntax error on connect operation"
#define AR_LINK_WRITE_ERR							1054	//"WriteFile Function on device has failed"
#define AR_LINK_READ_ERR							1055	//"ReadFile Function on device has failed"
#define AR_LINK_LAN_OPEN_ERR						1056	//"Error on TCP/IP socket connection routines"
#define AR_LINK_LAN_WRITE_ERR						1057	//"Write on LAN has failed"
#define AR_LINK_LAN_READ_ERR						1058	//"Read on LAN has failed"
#define AR_LINK_TIMEOUT_ERR							1059	//"Time command has expired"
#define AR_LINK_OVERFLOW_ERR						1060	//"Buffer of reply command has been exeed"
#define AR_LINK_BCC_ERR								1061	//"Trasmission error, wrong BCC/char"
#define AR_LINK_PROTOCOL_ERR						1062	//"Trasmission error, wrong protocol "
#define AR_LINK_CDM_IN_PROGRESS						1063	//"Command in progress"
#define AR_LINK_TERMINATE_EMULATOR					1064	//"Command in progress"
#define AR_LINK_SEQUENCE_ERR    					1065	//"Command in progress"
#define AR_LINK_LAN_CONNECTION_REFUSED  			1066	//"LAN Connection refused 10061"
#define AR_LINK_SYNTAX_ERR              			1067	//"CMLINK Syntax Error"
#define AR_LINK_LAN_CONNECTION_RESET				1068	//"LAN 10054 - Connection reset by peer"
#define AR_LINK_LANSERVER_CLIENT_CLOSE  			1069	//"layer Close session by client in LanServer"
#define AR_LINK_LIBUSB_NOT_FOUND					1070	//"LibUsb not found"
#define AR_LINK_LIBUSB_ERROR						1071	//"LibUsb Error generic"

#define AR_DEVICE_UNKNOWN_REPLY						1699	//used by SVKApp
#define AR_LIBRARY_NOT_INITIALIZED					1700
#define AR_OPEN_NOT_DONE							1701

#define AR_INVALID_LIBRARY_VERSIONING				1710
#define AR_JSON_MISSING_CONNECTIONTYPE				1711
#define AR_JSON_INVALID_CONNECTIONTYPE				1712
#define AR_JSON_MISSING_PROTOCOL_SET				1713
#define AR_JSON_INVALID_PROTOCOL_SET				1714
#define AR_JSON_MISSING_IP_ADDRESS					1715
#define AR_JSON_MISSING_IP_PORT						1716
#define AR_JSON_MISSING_USB_SERIALNUMBER			1717
#define AR_JSON_MISSING_COM_NAME					1718
#define AR_JSON_MISSING_COM_BAUDRATE				1719
#define AR_JSON_MISSING_COM_PARITY					1720
#define AR_JSON_MISSING_COM_DATABITS				1721
#define AR_JSON_MISSING_COM_STOPBITS				1722
#define AR_JSON_MISSING_OPSIDE						1723
#define AR_STRING_TRUNCATED							1724
#define AR_JSON_MISSING_CMD_MODE					1725
#define AR_JSON_INVALID_CMD_MODE					1726
#define AR_JSON_INVALID_PARITY						1727
#define AR_JSON_MISSING_NR_ELEMENTS					1728
#define AR_JSON_INVALID_DISPENSE_ELEMENT			1729
#define AR_JSON_MISSING_CASSETTE_ID					1730
#define AR_JSON_INVALID_CASSETTE_ID					1731
#define AR_JSON_MISSING_PASSWORD					1732
#define AR_JSON_MISSING_END_OPERATION				1733
#define AR_JSON_INVALID_END_OPERATION				1734
#define AR_JSON_MISSING_BOUNDLE_COUNTING_MODE		1735
#define AR_JSON_DEVICE_DATETIME_SECONDS				1736
#define AR_JSON_DEVICE_DATETIME_MINUTES				1737
#define AR_JSON_DEVICE_DATETIME_HOURS				1738
#define AR_JSON_DEVICE_DATETIME_WEEK_DAY			1739
#define AR_JSON_DEVICE_DATETIME_DAY					1740
#define AR_JSON_DEVICE_DATETIME_MONTH				1741
#define AR_JSON_DEVICE_DATETIME_YEAR				1742
#define AR_BUFFER_DIMENSION_ERR						1743
#define AR_JSON_READER_DENOMINATION_MISSING			1744
#define AR_INVALID_CONFIG_TYPE						1745
#define AR_INVALID_STATUS_PARAMETER					1746
#define AR_JSON_MISSING_BOUNDLE_COUNTING_SIZE		1747
#define AR_JSON_INVALID_FORCED_MODE					1748
#define	AR_JSON_INVALID_ALARM_MODE					1749
#define	AR_INVALID_INDATA							1750

#define AR_REPLY_IN_OUTDATA_TRUNCATED				1788
#define AR_INSUFFICIENT_SIZE_outData_BUFFER			1789
#define AR_INVALID_HCONNECT							1790
#define AR_INVALID_INDATA_JSON						1791
#define AR_INVALID_PARAMETER						1792
#define AR_NO_ARCA_DEVICE_CONNECTED					1793

// ------------------------------------------------------------------------
//                      ERRORS - SVKapp
// ------------------------------------------------------------------------
#define AR_APP_GENERIC_ERROR                        3001
#define AR_APP_UNAUTHORIZED                         3002
#define AR_APP_LIC_DATES_MISS                       3003
#define AR_APP_WRONG_DATES                          3004
#define AR_APP_LIC_USERNAME_ERR                     3005
#define AR_APP_WRONG_USERNAME                       3006
#define AR_APP_LIC_PASSW_ERR                        3007
#define AR_APP_WRONG_PWD                            3008
#define AR_APP_ARCALD_BASE                          3020
// From 3020 to 3040 are reserved for ArcaLd errors (3020 + error)

// ------------------------------------------------------------------------
//                      ERRORS - liblog
// ------------------------------------------------------------------------
#define AR_ARCALOG_SYSTEM_ERR							5001	
#define AR_ARCALOG_MODULENAME_ERR						5002
#define AR_ARCALOG_PATH_ERR							    5003	
#define AR_ARCALOG_INVALID_HWIN						    5004	
#define AR_ARCALOG_SESSION_NOT_FOUND					5005 
#define AR_ARCALOG_SESSION_ALREADY_USED				    5006 


// ------------------------------------------------------------------------
//                      others ERRORS 
// ------------------------------------------------------------------------
#define AR_FILE_CREATION_ERR                           6009
#define AR_FILE_WRITE_ERR                              6012
#define AR_JSON_COIN_INVALID_DENOM_ID                  6050

// ------------------------------------------------------------------------
//                      EXPORTED FUNCTIONS
// ------------------------------------------------------------------------
#ifdef __cplusplus
extern "C" {
#endif

ARCADEVINT_API int ARCAInitialize(float versioning, LPSTR inData);

//ARCADEVINT_API int ARCALibraryVersion(char *LibraryVersion, short lenLibraryVersion);
ARCADEVINT_API int ARCALibraryVersion(std::string &);

ARCADEVINT_API int ARCAConnect(LPSTR inData, int(*userFunc)(LPSTR eventInfo), int *hConnect, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCADisconnect(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAOpen(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAClose(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetCashData(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCADepositStart(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCADeposit(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

//ARCADEVINT_API int ARCADepositReport(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCADepositEnd(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCADispense(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

//ARCADEVINT_API int ARCADispenseReport(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCACount(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

//ARCADEVINT_API int ARCACountReport(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAexeCassetteEmpty(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

//ARCADEVINT_API int ARCAexeCassetteEmptyReport(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCADeviceStatus(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCADeviceInfoConfiguration(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetDeviceDateTime(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetDeviceDateTime(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAexeDeviceReset(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetDeviceVersion(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetCassetteSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetReaderCurrency(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetReaderBanks(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetReaderVersion(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetDeviceType(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetReaderIdentifier(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetReaderStatistics(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetCassetteSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetDeviceHistory(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetConfigReader(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAexeSecurity(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetMacAddress(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetReaderThreshold(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetDeviceSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetAlarm(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetDelays(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetProtocolSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetAudioSettings(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCACountMultitype(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetUpperTransportLock(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAexeDeviceShutdown(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAexeAlarm(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetReaderStatisticsEx(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCASampling(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAInitAsyncEvent(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAInjet(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetDelays(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAexeTraceTransfer(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAexeCassetteInit(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetPocketOutput(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetPocketOutput(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetPocketInput(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetPocketInput(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAsetLogServiceRecord(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetModuleSerialNumber(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCACoinDispense(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCACoinInventory(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetCoinConfiguration(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetCoinCashData(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetCassettePhysicalPosition(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAgetBanknoteSerialNumber(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

ARCADEVINT_API int ARCAexeDeviceBell(int hConnect, LPSTR inData, int lloutData, LPSTR outData);

#ifdef __cplusplus
}
#endif

#endif
