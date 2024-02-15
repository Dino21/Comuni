// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the ARCALE_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// ARCALE_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef ARCALE_EXPORTS
#define ARCALE_API __declspec(dllexport)
#else
#define ARCALE_API __declspec(dllimport)
#endif


#define ARCALIC_OK								0
#define ARCALIC_ERROR_CREATING_FILE_LICENSE		2


ARCALE_API int ARCAgenOTP(char *snMachine, char *date, char *hour, char *outPwd);
