using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NOVUS;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NOVUS_SVC;

namespace NovusTool
{
    class GlobalArcaFunction
    {
        public const string FILE_PWD = "license.arcacer";
        public const string CKEY = "ArcaCKeyTest";
        public const string SALT = "Qazwsxe1"; //lo stesso del programma license generation ...

        [DllImport("Arcald.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCAldDecrypt(StringBuilder FileCrypt, StringBuilder ckeyUser, StringBuilder ivecUser, int lloutData, StringBuilder JsonOutData);

        [DllImport("Arcald.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ARCApasswordHash(StringBuilder password, StringBuilder salt, StringBuilder hashString, int llhashString);

        public const string TRUE = "true";
        public const string FUNCTIONS = "Functions";
        public const string OPERATIONS = "fc_operations";
        public const string STATUS = "fc_status";
        public const string SETTINGS = "fc_settings";
        public const string UPDATES = "fc_updates";
        public const string ACQUISITION = "fc_acquistion";
        public const string ANALYSIS = "fc_analysis";


        //public const int LICENSE_INVALID_DATE = 1000;
        //public const int LICENSE_DATE_EXPIRED = 2000;
        //public const int LICENSE_INVALID_USERNAME = 3000;
        //public const int LICENSE_INVALID_PWD = 4000;

    
        public static int ARCAGetCashDataUpdate(ref string [] capacity,ref  string[] drawerID, ref string[] noteID, ref string[] s_switch,ref string [] numberNote,ref int index)
        {
            int llData = 4999;
            StringBuilder inData;
            inData = new StringBuilder();
            var outData = new StringBuilder(llData);
            int Rc_CashData = Novus_Class.ArcaReply.AR_OKAY;

            JObject JCashData = new JObject(
                new JProperty(JsonFunctioncs.DEPOSIT_CASSETTE_DETAIL, "false"));

            inData.Append(JCashData);

            Rc_CashData = Novus_Class.ARCAgetCashData(FormNovus.hCon, inData, llData, outData);

           
            if (outData.Length != 0 &&  (Rc_CashData != Novus_Class.ArcaReply.AR_OPEN_NOT_DONE))
            {
                string totalNotes = "";
                JsonFunctioncs.JsonCashData(outData.ToString(), ref drawerID, ref noteID, ref numberNote, ref capacity, ref s_switch, ref index, ref totalNotes);
            }

            //return FormNovus.Rc_Dll; 
            return Rc_CashData;

        }

        public static int ARCAsetDateTimeSync()
        {
            // Get the current date.
            DateTime thisDay = DateTime.Now;
            int lloutdatatimetransfer = 9999;
            var outData = new StringBuilder(lloutdatatimetransfer);
            //FormNovus myForm = (FormNovus)this.ParentForm;
            int rc = 0; 

            //F,n,4,[cc],ss,pp,hh,ww,dd,mm,yy
            int dayNumberOfWeek = (int)thisDay.DayOfWeek;

            JObject JDevice = new JObject
                {
                    { "seconds", thisDay.Second.ToString().PadLeft(2,'0')},
                    { "minutes", thisDay.Minute.ToString().PadLeft(2,'0') },
                    { "hours", thisDay.Hour.ToString().PadLeft(2,'0') },
                    { "weekDay",  thisDay.DayOfWeek.ToString() },
                    { "day", thisDay.Day.ToString().PadLeft(2,'0') },
                    { "month", thisDay.Month.ToString().PadLeft(2,'0') },
                    { "year", thisDay.Year.ToString() }
                };


            StringBuilder inData;
            inData = new StringBuilder();
            inData.Append(Convert.ToString(JDevice));


            rc = Novus_Class.ARCAsetDeviceDateTime(FormNovus.hCon, inData, lloutdatatimetransfer, outData);
            //if (FormNovus.Rc_Dll == Novus_Class.ArcaReply.AR_OKAY)
            //{
            //    //aggiorno la videata...
            //    get();
            //    Function_Gen.DisplayExecutionCorrect("ARCAsetDeviceDateTime", myForm);
            //}
            //else
            //{
            //    Function_Gen.DisplayError("ARCAsetDeviceDateTime", myForm);
            //}

            return rc ; 

        }

        public static int ARCADecodeLicenseFile(FormNovus myform , StringBuilder outData,string UserNameInput , string Pwdinput)
        {
            JObject jOutput;
            bool fLoginOk = false;


            jOutput = JObject.Parse(outData.ToString());

            string UserName_O = (string)jOutput["UserName"];
            string password_O = (string)jOutput["password"];
            string Internal_O = (string)jOutput["internal"];
            string Expired_Date_O = (string)jOutput["ExpiredDate"];
            string CreationDate_O = (string)jOutput["CreationDate"];

             if ((UserNameInput == UserName_O && Pwdinput == password_O) || UserNameInput == "" )
            {
                CultureInfo cultureIt = new CultureInfo("it-IT");
                Thread.CurrentThread.CurrentCulture = cultureIt;
                String format = "dd/MM/yyyy";

                DateTime dt = DateTime.Now;
                String str = dt.ToString(format);
                DateTime dtToday = DateTime.ParseExact(str, format, cultureIt.DateTimeFormat);

               // DateTime dtExpired = DateTime.Parse(Expired_Date_O);
                DateTime dtExpired = DateTime.ParseExact(Expired_Date_O, format, cultureIt.DateTimeFormat);
                //DateTime dtCreation = DateTime.Parse(CreationDate_O);
                DateTime dtCreation = DateTime.ParseExact(CreationDate_O, format, cultureIt.DateTimeFormat);

                //DateTime dtToday = DateTime.Now;



                if (dtToday.Date < dtCreation.Date)
                {
                    return Novus_Class.ArcaReply.AR_APP_WRONG_DATES; 
                }
                else
                {
                    if (dtExpired.Date < dtToday.Date)
                        return Novus_Class.ArcaReply.AR_APP_LIC_DATE_EXPIRED; 
                    else
                    {
                        fLoginOk = true;
                        myform.bt_main_Device.Enabled = true;

                        if (fLoginOk == true)
                        {
                            //passo ad analizzare il file con le opzioni da abilitare nell'applicativo 
                            string jTemp = "";
                            jTemp = (string)jOutput[FUNCTIONS][OPERATIONS]["enable"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                            {
                                FormNovus.foperations_enable = true;
                                //myform.bt_main_Operation.Enabled = true;
                            }


                            jTemp = (string)jOutput[FUNCTIONS][OPERATIONS]["count"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                                FormNovus.foperations_fCount = true;

                            jTemp = (string)jOutput[FUNCTIONS][OPERATIONS]["deposit"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                                FormNovus.foperations_fdeposit = true;

                            jTemp = (string)jOutput[FUNCTIONS][OPERATIONS]["dispense"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                                FormNovus.foperations_fdispense = true;

                            jTemp = (string)jOutput[FUNCTIONS][OPERATIONS]["empty"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                                FormNovus.foperations_fempty = true;

                            jTemp = (string)jOutput[FUNCTIONS][STATUS]["enable"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                            {
                                FormNovus.fstatus_enable = true;
                                //myform.bt_main_Status.Enabled = true;
                            }


                            jTemp = (string)jOutput[FUNCTIONS][STATUS]["cashdata"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                                FormNovus.fstatus_cashdata = true;

                            jTemp = (string)jOutput[FUNCTIONS][SETTINGS]["enable"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                            {
                                FormNovus.fsettings_enable = true;
                                //myform.bt_main_Settings.Enabled = true;
                            }


                            jTemp = (string)jOutput[FUNCTIONS][UPDATES]["enable"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                            {
                                FormNovus.fupdates_enable = true;
                                //myform.bt_main_Version.Enabled = true;
                            }


                            jTemp = (string)jOutput[FUNCTIONS][ACQUISITION]["enable"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                            {
                                FormNovus.facquistion_enable = true;
                                //myform.bt_main_Acquisition.Enabled = true;
                            }


                            jTemp = (string)jOutput[FUNCTIONS][ANALYSIS]["enable"];
                            if (jTemp != null && jTemp.ToLower() == TRUE)
                            {
                                FormNovus.fanalysis_enable = true;
                               // myform.bt_main_Analysis.Enabled = true;
                            }
                        }
                    }
                }
            }
            else
            {
                if (UserNameInput != UserName_O)
                    return Novus_Class.ArcaReply.AR_APP_WRONG_USERNAME; 


                if (Pwdinput != password_O)
                    return Novus_Class.ArcaReply.AR_APP_WRONG_PWD; 
            }
     
            return Novus_Class.ArcaReply.AR_OKAY;
        }

        public static int ArcaDisconnectbyPort(int hConnect)
        {
            var inData = new StringBuilder();
            int llOutData = 1024; 
            var outData = new StringBuilder(llOutData);
            int rc_temp;

            rc_temp = Novus_Class.ARCADisconnect(hConnect, inData, llOutData, outData);

            return rc_temp;
        }

        public static int GlobalArcaClose()
        {
            var inDataC = new StringBuilder();
            int llDataClose = 1024;
            var outDataC = new StringBuilder(llDataClose);

            if (FormNovus.fOpenDone == true)
                FormNovus.Rc_Dll = Novus_Class.ARCAClose(FormNovus.hCon, inDataC, llDataClose, outDataC);

            Thread.Sleep(200);

            return FormNovus.Rc_Dll;
        }

        public static int GlobalARCAOpen()
        {
            int llData = 256;

            JObject JOpen = new JObject(
                new JProperty("opSide", FormNovus.UserSideOpen),
                new JProperty("password", FormNovus.UserSidePwd));

            StringBuilder inData;
            inData = new StringBuilder();
            inData.Append(Convert.ToString(JOpen));

            var outData = new StringBuilder(llData);

            FormNovus.Rc_Dll = Novus_Class.ARCAOpen(FormNovus.hCon, inData, llData, outData);

            return FormNovus.Rc_Dll;


        }

        public static void checkCurrentDay(int day, ref string dayofWeek)
        {
            switch (day)
            {
                case 1:
                    dayofWeek = JsonFunctioncs.MONDAY;
                    break;
                case 2:
                    dayofWeek = JsonFunctioncs.TUESDAY;
                    break;
                case 3:
                    dayofWeek = JsonFunctioncs.WEDNESDAY;
                    break;
                case 4:
                    dayofWeek = JsonFunctioncs.THURSDAY;
                    break;
                case 5:
                    dayofWeek = JsonFunctioncs.FRIDAY;
                    break;
                case 6:
                    dayofWeek = JsonFunctioncs.SATURDAY;
                    break;
                case 7:
                    dayofWeek = JsonFunctioncs.SUNDAY;
                    break;
                default:
                    break;
            }
        }

        public static int GlobalARCANrCassette(FormNovus myform)
        {
            int llData = 1024;
            StringBuilder inData;
            var outData = new StringBuilder(llData);

           

            string  nCassette;
            JObject getCassetteSetting = new JObject(
                  new JProperty(JsonFunctioncs.CONFIG_TYPE, JsonFunctioncs.GET_CASSETTE_NUMBER));
            inData = new StringBuilder();
            inData.Append(Convert.ToString(getCassetteSetting));


            FormNovus.Rc_Dll = Novus_Class.ARCAgetProtocolSettings(FormNovus.hCon, inData, llData, outData);
            if (FormNovus.Rc_Dll == Novus_Class.ArcaReply.AR_OKAY)
            {
                Function_Gen.DisplayExecutionCorrect("ARCAgetProtocolSettings", myform);
                JObject rss = JObject.Parse(outData.ToString());
                nCassette  = (string)rss[JsonFunctioncs.HEADER_CONTENT][JsonFunctioncs.CASSETTE_NUMBER];
                FormNovus.NCassette = Convert.ToInt32(nCassette);
            }
            else
                Function_Gen.DisplayError("ARCAgetProtocolSettings", myform);


            return FormNovus.NCassette;
        }

        public static DataGridView SetGridHeightWidth(DataGridView grd, int maxHeight, int maxWidth)
        {
            var height = 40;
            foreach (DataGridViewRow row in grd.Rows)
            {
                if (row.Visible)
                    height += row.Height;
            }

            if (height > maxHeight)
                height = maxHeight;

            grd.Height = height;

            var width = 60;
            foreach (DataGridViewColumn col in grd.Columns)
            {
                if (col.Visible)
                    width += col.Width;
            }

            if (width > maxWidth)
                width = maxWidth;

            grd.Width = width;

            return grd;
        }

        public static string convertIndexToCassette(int index, int index_row ,DataGridView dt)
        {
            string NameDrawer = "";
            Color color = System.Drawing.ColorTranslator.FromHtml("#b6f2c6");

            if (index == 0)
            {
                NameDrawer = "DrawerA";
                color = System.Drawing.ColorTranslator.FromHtml("#b6f2c6");
            }
                
            if (index == 5)
            {
                NameDrawer = "DrawerB";
                color = System.Drawing.ColorTranslator.FromHtml("#b6f2ef");
            }
                
            if (index == 10)
            {
                NameDrawer = "DrawerC";
                color = System.Drawing.ColorTranslator.FromHtml("#b6d7f2");
            }
               
            if (index == 15)
            {
                NameDrawer = "DrawerD";
                color = System.Drawing.ColorTranslator.FromHtml("#b6cbf2");
            }
               
            if (index == 20)
            {
                NameDrawer = "DrawerE";
                color = System.Drawing.ColorTranslator.FromHtml("#b6bdf2");
            }
               
            if (index == 25)
            {
                NameDrawer = "DrawerF";
                color = System.Drawing.ColorTranslator.FromHtml("#d0b6f2");
            }
                
            if (index == 30)
            {
                NameDrawer = "DrawerG";
                color = System.Drawing.ColorTranslator.FromHtml("#b6f2ba");
            }
              
            if (index == 35)
            {
                NameDrawer = "DrawerH";
                color = System.Drawing.ColorTranslator.FromHtml("#eaf2b6");
            }

            if (index == 40)
            {
                NameDrawer = "DrawerI";
                color = System.Drawing.ColorTranslator.FromHtml("#b6bef2");
            }

            if (index == 45)
            {
                NameDrawer = "DrawerJ";
                color = System.Drawing.ColorTranslator.FromHtml("#deb6f2");
            }

            if (index == 50)
            {
                NameDrawer = "Drawerk";
                color = System.Drawing.ColorTranslator.FromHtml("#f2b6c3");
            }

            if (index == 55)
            {
                NameDrawer = "DrawerL";
                color = System.Drawing.ColorTranslator.FromHtml("#f2dfb6");
            }


            for (int ii = index_row; ii <=index_row + 5; ii++)
                dt.Rows[ii].DefaultCellStyle.BackColor = color;

            return NameDrawer;
        }
        public static  void ReadPhotoValueNovus(string target, string photoId, DataGridView dt, int NrCassette,   FormNovus myForm /*, Button bt*/)
        {
            StringBuilder inData;
            int llData = 9999;
            int index = 0;
            int index_row = 0;
            inData = new StringBuilder();
            var outData = new StringBuilder(llData);

            Wait F_Wait = new Wait();


            //bt.Enabled = false;
            //leggo il valore dei photo ...
            JObject JDevicePhoto = new JObject(
                new JProperty(JsonFunctioncs.PHOTO_TARGET_MODULE, target),
                new JProperty(JsonFunctioncs.PHOTOS_ID, photoId)
                );

            inData.Append(Convert.ToString(JDevicePhoto));

            Function_Gen.ExecutionCommand(F_Wait, true, myForm);

            FormNovus.Rc_Dll = Novus_Class_svc.ARCAgetDevicePhotoNovus(FormNovus.hCon, inData, llData, outData);

            Function_Gen.ExecutionCommand(F_Wait, false, myForm);

            if (FormNovus.Rc_Dll == Novus_Class.ArcaReply.AR_OKAY)
                Function_Gen.DisplayExecutionCorrect("ARCAgetDevicePhotoNovus", myForm);
            else
                Function_Gen.DisplayError("ARCAgetDevicePhotoNovus", myForm);

            if (outData.Length != 0)
            {
                string[] moduleId = new string[FormNovus.MAX_PHOTO_ELEMENT];
                string[] ph_id = new string[FormNovus.MAX_PHOTO_ELEMENT];
                string[] result = new string[FormNovus.MAX_PHOTO_ELEMENT];
                string[] agc = new string[FormNovus.MAX_PHOTO_ELEMENT];
                string[] current = new string[FormNovus.MAX_PHOTO_ELEMENT];
                string[] threshold = new string[FormNovus.MAX_PHOTO_ELEMENT];


                JsonFunctioncs.JsonPhotoSensorNovus(outData.ToString(), ref index, ref moduleId, ref ph_id, ref result, ref threshold, ref agc, ref current);


                if (index != 0)
                {
                    if (photoId == JsonFunctioncs.PHOTO_TARGET_MODULE_LOWER)
                    {
                        dt.RowCount = index + NrCassette;
                        for (int ii = 0; ii < index; ii++)
                        {
                            if (ii % 5 == 0)
                            {
                                dt.Rows[index_row].Cells[1].Value = convertIndexToCassette(ii,index_row,dt);
                                dt.Rows[index_row].Cells[2].Value = "--";
                                dt.Rows[index_row].Cells[3].Value = "--";
                                dt.Rows[index_row].Cells[4].Value = "--";
                                dt.Rows[index_row++].Cells[5].Value = "--";
                            }
                            dt.Rows[index_row].Cells[1].Value = moduleId[ii];
                            dt.Rows[index_row].Cells[2].Value = result[ii];
                            dt.Rows[index_row].Cells[3].Value = threshold[ii];
                            dt.Rows[index_row].Cells[4].Value = agc[ii];
                            dt.Rows[index_row++].Cells[5].Value = current[ii];

                        }
                    }
                    else
                    {
                        dt.RowCount = index;
                        for (int ii = 0; ii < dt.RowCount; ii++)
                        {
                            if (moduleId[ii] != null)
                            {
                                dt.Rows[ii].Cells[1].Value = moduleId[ii];
                                dt.Rows[ii].Cells[2].Value = result[ii];
                                dt.Rows[ii].Cells[3].Value = threshold[ii];
                                dt.Rows[ii].Cells[4].Value = agc[ii];
                                dt.Rows[ii].Cells[5].Value = current[ii];
                            }
                        }
                    }

                }
               // bt.Enabled = true;
              
            }
        }//ReadPhotoValueNovus


        public static void WritePhotoValueNovus(string target, string photoId, DataGridView dt, int NrCassette, FormNovus myForm /*, Button bt*/)
        {
            StringBuilder inData;
            int llData = 9999;
            inData = new StringBuilder();
            var outData = new StringBuilder(llData);

           
            Wait F_Wait = new Wait();


            //bt.Enabled = false;
            //scrivo  il valore dei photo ...
            //JObject JDevicePhoto = new JObject(
            //    new JProperty(JsonFunctioncs.PHOTO_TARGET_MODULE, target),
            //    new JProperty(JsonFunctioncs.PHOTOS_ID, photoId)
            //    );

            //inData.Append(Convert.ToString(JDevicePhoto));

            //Function_Gen.ExecutionCommand(F_Wait, true, myForm);

            //FormNovus.Rc_Dll = Novus_Class_svc.ARCAsetDevicePhotoNovus(FormNovus.hCon, inData, llData, outData);

            //Function_Gen.ExecutionCommand(F_Wait, false, myForm);

            //if (FormNovus.Rc_Dll == Novus_Class.ArcaReply.AR_OKAY)
            //    Function_Gen.DisplayExecutionCorrect("ARCAsetDevicePhotoNovus", myForm);
            //else
            //    Function_Gen.DisplayError("ARCAsetDevicePhotoNovus", myForm);


            JObject JSetPhotoCalibrations = new JObject
            {
                new JProperty("ph_id", photoId.ToString())
            };

           



            inData = new StringBuilder();
            inData.Append(Convert.ToString(JSetPhotoCalibrations));

            Function_Gen.ExecutionCommand(F_Wait, true, myForm);

            FormNovus.Rc_Dll = Novus_Class_svc.ARCAsetDevicePhoto(FormNovus.hCon, inData, llData, outData);

            Function_Gen.ExecutionCommand(F_Wait, false, myForm);

            if (FormNovus.Rc_Dll == Novus_Class.ArcaReply.AR_OKAY)
                Function_Gen.DisplayExecutionCorrect("ARCAsetDevicePhoto", myForm);
            else
                Function_Gen.DisplayError("ARCAsetDevicePhoto", myForm);

            // bt.Enabled = true;

        }//WritePhotoValueNovus




    }
}
