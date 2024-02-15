using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NOVUS;
using NovusTool;
using NovusTool.Views.Acquisition;
using NovusTool.Views.Authenticate;
using NovusTool.Views.Operation;

namespace NovusTool
{
    public class Function_Gen
    {
        //public const string SINGLE_DENOMINATION_MULTITYPE_SPECIFIC = "#";//EUC#
        //public const string MULTI_DENOM_SPECIFIC = "*#";  //EU*#
        //public const string MULTIUNF = "*"; //EU*-
        public const string SINGLE_DENOMINATION_MULTITYPE = "*"; //EUC*
        public const string SINGLE_DENOMINATION_UNFIT = "-"; //EUC-


        public const string UNFIT = "--";
        public const string MULTIOVERUNF = "##";  //EU##;
        public const string MULTIUNF_UNFIT = "*-"; //EU*-
        public const string MULTIDENOMINATION = "**"; //EU**
        public const string MULTI_FALSE = "??";
        public const string OVERFLOW = "++";  //EU++ 

        public const string STR_UNFIT = "Unfit";
        public const string STR_MULTIOVERUNF = "MultiOver"; 
        public const string STR_MULTIUNF_UNFIT = "MultiUnfit"; 
        public const string STR_MULTIDENOMINATION = "MultiDenomination";
        public const string STR_MULTI_FALSE = "False";
        public const string STR_OVERFLOW = "Overflow";

        public const string MULTI_NATION_DENOM_SPECIFIC = "####";
        public const string MULTINATION = "****";

        public const string UNIT_UNDO = "undo";
        public const string LASTDEPOSIT_OUTPUT_UNFIT = "Last deposit Output Unfit notes";//T,n,033
        public const string LASTDEPOSIT_SAFE_UNFIT = "Last deposit Safe Unfit notes"; //T,n,0,34
        public const string LASTDEPOSIT_READER_STATISTICS = "Transaction statistics";
        public const string LASTDEPOSIT_UNFIT_NOTES = "Last deposit Unfit note detail"; //T,n,0,36
        public const string LASTDEPOSIT_SUSPECT_NOTAUTHENTICATED = "Last deposit suspect and not authenticated (CAT.2 + CAT.3)"; //T,n,0,37
        public const string LASTDEPOSIT_NOTAUTHENTICATED = "Last deposit not authenticated (CAT.3)";//T,n,0,38  NOT AUTHENTICATED
        public const string LASTDEPOSIT_SUSPECT = "Last deposit suspect (CAT.2)";//T,n,0,86  false o sospette di falso
        public const string LASTDEPOSIT_UNFIT_NOTE_DETAILS = "Last deposit unfit single note details";//T,n,3,6  

        public const string SIDE_L = "L";
        public const string SIDE_R = "R";

        public const string LAST_TYPE_DEPOSIT_STANDARD  = "std";
        public const string LAST_TYPE_DEPOSIT_NATION    = "nation";
        public const string LAST_TYPE_DEPOSIT_FORCED    = "forced";





        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        //Lettura File Ini 
        public static string IniReadValue(string Section, string Key, string filetoRead)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, filetoRead);
            return temp.ToString();

        }

        public static void IniWriteValue(string Section, string Key, string Value, string filetoRead)
        {
            WritePrivateProfileString(Section, Key, Value, filetoRead);
        }

        public static bool IsValidIP(string addr)
        {
            //create our match pattern
            string pattern = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }

        public static string getValuebyDenom(string Denom, ref string Currency , ref string Value)
        {
            if (Denom != null)
            {
                if (Denom.Length == 1)
                {
                    if (Denom == "A")
                        Value = "1";
                    else if (Denom == "B")
                        Value = "2";
                    else if (Denom == "C")
                        Value = "5";
                    else if (Denom == "D")
                        Value = "10";
                    else if (Denom == "E")
                        Value = "20";
                    else if (Denom == "F")
                        Value = "25";
                    else if (Denom == "G")
                        Value = "50";
                    else if (Denom == "H")
                        Value = "100";
                    else if (Denom == "I")
                        Value = "200";
                    else if (Denom == "J")
                        Value = "500";
                    else if (Denom == "K")
                        Value = "1000";
                    else if (Denom == "L")
                        Value = "2000";
                    else if (Denom == "M")
                        Value = "5000";
                    else if (Denom == "N")
                        Value = "10000";

                    Currency = "";
                }
                else if (Denom.Length == 4)
                {
                    if (Denom.Substring(2, 1) == "A")
                        Value = "1";
                    else if (Denom.Substring(2, 1) == "B")
                        Value = "2";
                    else if (Denom.Substring(2, 1) == "C")
                        Value = "5";
                    else if (Denom.Substring(2, 1) == "D")
                        Value = "10";
                    else if (Denom.Substring(2, 1) == "E")
                        Value = "20";
                    else if (Denom.Substring(2, 1) == "F")
                        Value = "25";
                    else if (Denom.Substring(2, 1) == "G")
                        Value = "50";
                    else if (Denom.Substring(2, 1) == "H")
                        Value = "100";
                    else if (Denom.Substring(2, 1) == "I")
                        Value = "200";
                    else if (Denom.Substring(2, 1) == "J")
                        Value = "500";
                    else if (Denom.Substring(2, 1) == "K")
                        Value = "1000";
                    else if (Denom.Substring(2, 1) == "L")
                        Value = "2000";
                    else if (Denom.Substring(2, 1) == "M")
                        Value = "5000";
                    else if (Denom.Substring(2, 1) == "N")
                        Value = "10000";

                    Currency = Denom.Substring(0, 2);
                }
            }
            else
                Value = ""; 

            //if (Denom.Substring(0,2) == "EU")
            



            //else
            //{
            //altre valute
            //    Value = "NOT FOUND";
            //}

           
            return Value;

        }

        public static void DisplayError(string FunctionName, FormNovus myForm)
        {
            JsonFunctioncs.ReadJsonError(FormNovus.Language_Type, FormNovus.Rc_Dll.ToString(), ref FormNovus.ErrorCodeDescription);
            if (FormNovus.Rc_Dll == Novus_Class.ArcaReply.AR_CASSETTE_FULL || FormNovus.Rc_Dll == Novus_Class.ArcaReply.AR_DENOMINATION_EMPTY)
            {
                myForm.lblResFunction.Text = FunctionName + " Reply = " + FormNovus.Rc_Dll.ToString() + "   " + FormNovus.ErrorCodeDescription;
                myForm.lblResFunction.ForeColor = Color.OrangeRed;
            }    
            else
            {
                //myForm.lblResFunction.Text = FunctionName + " Error = " + FormNovus.Rc_Dll.ToString() + "   " + FormNovus.ErrorCodeDescription;
                myForm.lblResFunction.Text = FunctionName + " Reply = " + FormNovus.Rc_Dll.ToString() + "   " + FormNovus.ErrorCodeDescription;
                myForm.lblResFunction.ForeColor = Color.Red;
            }
                

            myForm.lblResFunction.Visible = true;
            myForm.Refresh();
        }

        public static void DisplaMessageError(string FunctionName, FormNovus myForm)
        {
            myForm.lblResFunction.Text = FunctionName;
            myForm.lblResFunction.ForeColor = Color.OrangeRed;
            
            myForm.lblResFunction.Visible = true;
            myForm.Refresh();
        }
        public static void DisplayExecutionCorrect(string FunctionName, FormNovus myForm)
        {
            if (FormNovus.Rc_Dll == Novus_Class.ArcaReply.AR_OKAY)
                JsonFunctioncs.ReadJsonError(FormNovus.Language_Type, FormNovus.Rc_Dll.ToString(), ref FormNovus.ErrorCodeDescription);
            else
                FormNovus.ErrorCodeDescription = ""; 
            myForm.lblResFunction.Text = FunctionName + " Reply = " + FormNovus.Rc_Dll.ToString() + "   " + FormNovus.ErrorCodeDescription; 
            myForm.lblResFunction.ForeColor = Color.Green;
            myForm.lblResFunction.Visible = true;
            myForm.Refresh();
        }

        public static void ResetError(FormNovus myForm)
        {
            if (myForm != null )
            {
                myForm.lblResFunction.Text = "";
                myForm.lblResFunction.ForeColor = Color.Black;
                myForm.lblResFunction.Visible = true;
                myForm.Refresh();
            }
           
        }

        public static void export2File(ListView lv)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "SaveFileDialog ComScope";
            sfd.Filter = "Text File (.txt) | *.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName.ToString();
                if (filename != "")
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        foreach (ListViewItem item in lv.Items)
                        {
                            sw.WriteLine(item.Text);
                        }
                    }
                }
            }
        }

        public static void ExecutionCommand(Form fWait, bool Hide, Form FormNovus)
        {

            if (Hide == true)
            {
                //FormNovus.Enabled = false; 
                //Method 2. The manual way
                fWait.StartPosition = FormStartPosition.Manual;
                //fWait.Top = (Screen.PrimaryScreen.Bounds.Height - FormNovus.Height) / 2;
                //fWait.Left = (Screen.PrimaryScreen.Bounds.Width - FormNovus.Width) / 2;
                fWait.Top = ((FormNovus.Top) + (FormNovus.Height / 2) - 100);
                fWait.Left = (FormNovus.Left + (FormNovus.Width / 2) - 100);
                //fWait.Left = 1200; 
                fWait.Show();
                //Cursor.Hide();
            }
            else
            {
                fWait.Hide();
                //FormNovus.Enabled = true; 
                //Cursor.Show();
            }

        }

        public static void CenterForm(Form yuorForm)
        {

            //Method 2. The manual way
            yuorForm.StartPosition = FormStartPosition.Manual;
            yuorForm.Top = (Screen.PrimaryScreen.Bounds.Height - yuorForm.Height) / 2;
            yuorForm.Left = (Screen.PrimaryScreen.Bounds.Width - yuorForm.Width) / 2;
        }

        public static void CentreControlInGroupBox(GroupBox theGroupBox, Control theControl)
        {
            // Find the centre point of the Group Box
            int groupBoxCentreWidth = theGroupBox.Width / 2;
            int groupBoxCentreHeight = theGroupBox.Height / 2;

            // Find the centre point of the Control to be positioned/added
            int controlCentreWidth = theControl.Width / 2;
            int controlCentreHeight = theControl.Height / 2;

            // Set the Control to be at the centre of the Group Box by
            // off-setting the Controls Left/Top from the Group Box centre
            theControl.Left = groupBoxCentreWidth - controlCentreWidth;
            theControl.Top = groupBoxCentreHeight - controlCentreHeight;

            // Set the Anchor to be None to make sure the control remains
            // centred after re-sizing of the form
            theControl.Anchor = AnchorStyles.None;

            // Add the control to the GroupBox's Controls collection to maintain 
            // the correct Parent/Child relationship
            theGroupBox.Controls.Add(theControl);
        }

        public static void FillLanguageObjects(FormNovus myForm, string Language_Type, string NameUserControlActive)
        {
            string jsonfileMain;
            string jsonfileActive;

            //il main sempre ...
            jsonfileMain = Application.StartupPath + "\\Language\\" + "MainPage.json";
            if (File.Exists(jsonfileMain))
            {
                myForm.bt_main_Authenticate.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Authenticate");
                myForm.bt_main_Device.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Device");
                myForm.bt_main_Operation.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Operation");
                myForm.bt_main_Status.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Status");
                myForm.bt_main_Settings.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Settings");
                myForm.bt_main_Version.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Updates");
                myForm.bt_main_Acquisition.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Acquisition");
                //myForm.bt_main_Analysis.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Analysis");
                myForm.bt_main_Disconnect.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "bt_main_Disconnect");

                myForm.lbl_inject_command.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "lbl_iniect_command");
                myForm.menuComScope.Items[0].Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "menuComScope");
                foreach (ToolStripMenuItem menuItem in myForm.menuComScope.Items)
                {
                    foreach (ToolStripMenuItem subitem in menuItem.DropDownItems)
                    {
                        menuItem.DropDownItems[0].Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "cleanToolStripMenuItem");
                        menuItem.DropDownItems[1].Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileMain, Language_Type, "saveToolStripMenuItem");
                        break;
                    }
                }      
            }

            jsonfileActive = Application.StartupPath + "\\Language\\" + NameUserControlActive + ".json";

            if (NameUserControlActive == "UserControlLogin" && File.Exists(jsonfileActive))
            {
                myForm.usrLogin.lbl_login_username.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_login_username");
                myForm.usrLogin.lbl_login_pwd.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_login_pwd");
            }
            else if ((NameUserControlActive == "UserControlOpen" && File.Exists(jsonfileActive)))
            {
                FormNovus.usrcontrolopen.lblSide.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lblSide");
                FormNovus.usrcontrolopen.lblSide.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lblSide");
                FormNovus.usrcontrolopen.lbl_HeaderOpen.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_HeaderCount");
                FormNovus.usrcontrolopen.btOpen.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btOpen");
                FormNovus.usrcontrolopen.btBack.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btBack");
                FormNovus.usrcontrolopen.lblpwd.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lblpwd");
            }
            else if ((NameUserControlActive == "UserControlConnect" && File.Exists(jsonfileActive)))
            {
                myForm.usrConnect.lbl_Remote_IpAddress.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_Remote_IpAddress");
                myForm.usrConnect.lbl_Hostname.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_Hostname");
                myForm.usrConnect.lbl_Port.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_Port");
                myForm.usrConnect.lbl_protocol.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_protocol");
                myForm.usrConnect.lbl_connectionType.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_connectionType");
                myForm.usrConnect.lbl_comNumber.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_comNumber");
                myForm.usrConnect.lbl_baudRate.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_baudRate");
                myForm.usrConnect.lbl_parity.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_parity");
                myForm.usrConnect.lbl_dataBits.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_dataBits");
                myForm.usrConnect.lbl_stopBits.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_stopBits");
                myForm.usrConnect.lbl_USBserialnumber.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_USBserialnumber");
                myForm.usrConnect.btArcaConnect.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btArcaConnect");
            }
            else if ((NameUserControlActive == "UserControlOperatrionMain" && File.Exists(jsonfileActive)))
            {
                myForm.usrOperationMain.lblHeaderOperationMain.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lblHeaderMain");
                myForm.usrOperationMain.btOpen.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btOpen");
                myForm.usrOperationMain.btCount.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btCount");
                myForm.usrOperationMain.btDeposit.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btDeposit");
                myForm.usrOperationMain.btDispense.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btDispense");
                myForm.usrOperationMain.btEmpty.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btEmpty");
                myForm.usrOperationMain.btClose.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btClose");
            }
            else if ((NameUserControlActive == "UserControlSettingsMain" && File.Exists(jsonfileActive)))
            {
                myForm.usrSettingsnMain.lblSettingsHeaderMain.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lblSettingsHeaderMain");
                myForm.usrSettingsnMain.btSettingsUpper.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btSettingsReader");
                myForm.usrSettingsnMain.btSettingssafe.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btSettingssafe");
            }
            else if ((NameUserControlActive == "UserControlCount" && File.Exists(jsonfileActive)))
            {
                FormNovus.usrcount.lbl_HeaderCount.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_HeaderCount");
                FormNovus.usrcount.lbl_BundleSize.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_BundleSize");
                FormNovus.usrcount.rbStart.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "rbStart");
                FormNovus.usrcount.rbContinue.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "rbContinue");
                FormNovus.usrcount.rbfullCountig.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "rbfullCountig");
                FormNovus.usrcount.gpNotesDetail.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gpNotesDetail");
                FormNovus.usrcount.lbl_totalBank.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_totalBank");
                FormNovus.usrcount.lbl_refused.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_refused");
                FormNovus.usrcount.lbl_Rejected.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_Rejected");
                FormNovus.usrcount.btCount.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btCount");
                FormNovus.usrcount.btBack.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btBack");
                //
                FormNovus.usrcount.gridResultOperation.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultOperation_header_0");
                FormNovus.usrcount.gridResultOperation.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultOperation_header_1");
                FormNovus.usrcount.gridResultOperation.Columns[2].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultOperation_header_2");
                FormNovus.usrcount.gridResultOperation.Columns[3].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultOperation_header_3");


            }
            else if ((NameUserControlActive == "UserControlDeposit" && File.Exists(jsonfileActive)))
            {
                FormNovus.usrdeposit.lbl_HeaderDeposit.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_HeaderCount");
                FormNovus.usrdeposit.lbl_totalBank.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_totalBank");
                FormNovus.usrdeposit.lbl_refused.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_refused");
                FormNovus.usrdeposit.lbl_Rejected.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_Rejected");
                FormNovus.usrdeposit.btArcaDeposit.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btArcaDeposit");
                FormNovus.usrdeposit.btBack.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btBack");
                FormNovus.usrdeposit.gridResultOperation.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultOperation_header_0");
                FormNovus.usrdeposit.gridResultOperation.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultOperation_header_1");
                FormNovus.usrdeposit.gridResultOperation.Columns[2].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultOperation_header_2");
                FormNovus.usrdeposit.gridResultSafe.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultSafe_header_0");
                FormNovus.usrdeposit.gridResultSafe.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultSafe_header_1");
                FormNovus.usrdeposit.gridResultSafe.Columns[2].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridResultSafe_header_2");
            }
            else if ((NameUserControlActive == "UserControlDispense" && File.Exists(jsonfileActive)))
            {
                FormNovus.usrdispense.lbl_HeaderDispense.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_HeaderCount");
                FormNovus.usrdispense.btdispense.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btdispense");
                FormNovus.usrdispense.btBack.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btBack");
                FormNovus.usrdispense.grpInputData.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "grpInputData");
                //FormNovus.usrcontroldispense.grddispense.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "Griddispense_header_0");
                FormNovus.usrdispense.grddispensebytarget.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "Griddispense_header_1");
                FormNovus.usrdispense.grddispensebytarget.Columns[2].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "Griddispense_header_2");
                FormNovus.usrdispense.grddispensebytarget.Columns[3].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "Griddispense_header_3");
                FormNovus.usrdispense.grddispensebytarget.Columns[4].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "Griddispense_header_4");
                //FormNovus.usrcontroldispense.grdInputQty.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "grdInputQty_header_0");
                FormNovus.usrdispense.grpResult.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "grpResult");
                FormNovus.usrdispense.grdResultOutput.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "grdResultOutput_header_0");
                FormNovus.usrdispense.grdResultOutput.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "grdResultOutput_header_1");
            }
            else if ((NameUserControlActive == "UserControlEmptyCassette" && File.Exists(jsonfileActive)))
            {
                FormNovus.usremptycassette.lbl_HeaderCount.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_HeaderCount");
                FormNovus.usremptycassette.lbl_information.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_information");
                FormNovus.usremptycassette.lbl_pwd.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_pwd");
                FormNovus.usremptycassette.lbl_Bundle.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_Bundle");
                FormNovus.usremptycassette.chkCheckAll.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "chkCheckAll");
                FormNovus.usremptycassette.gridDrawer.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridDrawer_header_0");
                FormNovus.usremptycassette.gridDrawer.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridDrawer_header_1");
                FormNovus.usremptycassette.gridDrawer.Columns[2].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridDrawer_header_2");
                FormNovus.usremptycassette.gridDrawer.Columns[3].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridDrawer_header_3");
                FormNovus.usremptycassette.gridDrawer.Columns[4].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridDrawer_header_4");
                FormNovus.usremptycassette.btEmpty.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btEmpty");
                FormNovus.usremptycassette.btBack.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btBack");
            }
            else if ((NameUserControlActive == "UserControlSettingsInitCassette" && File.Exists(jsonfileActive)))
            {
                FormNovus.usrcontrolinitcassette.lbl_HeaderInitCassette.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_HeaderCount");
                FormNovus.usrcontrolinitcassette.lbl_information.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_information");
                FormNovus.usrcontrolinitcassette.lbl_pwd.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_pwd");
                FormNovus.usrcontrolinitcassette.rbTarget.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "rbTarget");
                FormNovus.usrcontrolinitcassette.rbDenomination.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "rbDenomination");
                FormNovus.usrcontrolinitcassette.btBack.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btInit");
                FormNovus.usrcontrolinitcassette.gridCassette.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCassette_header_0");
                FormNovus.usrcontrolinitcassette.gridCassette.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCassette_header_1");
                FormNovus.usrcontrolinitcassette.gridCassette.Columns[2].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCassette_header_2");
                FormNovus.usrcontrolinitcassette.gridCassette.Columns[3].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCassette_header_3");
            }
            else if ((NameUserControlActive == "UserControlCashData" && File.Exists(jsonfileActive)))
            {
                FormNovus.userControlCashData.lbl_HeaderCashData.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_HeaderCount");
                FormNovus.userControlCashData.btBack.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "btBack");
                FormNovus.userControlCashData.lbl_information.Text = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "lbl_information");
                FormNovus.userControlCashData.gridCashData.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCashData_header_0");
                FormNovus.userControlCashData.gridCashData.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCashData_header_1");
                FormNovus.userControlCashData.gridCashData.Columns[2].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCashData_header_2");
                FormNovus.userControlCashData.gridCashData.Columns[3].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCashData_header_3");
                FormNovus.userControlCashData.gridCashData.Columns[4].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridCashData_header_4");
                FormNovus.userControlCashData.gridDetailCashData.Columns[0].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridDetailCashData_header_0");
                FormNovus.userControlCashData.gridDetailCashData.Columns[1].HeaderCell.Value = JsonFunctioncs.ReadJsonTextObjet(jsonfileActive, Language_Type, "gridDetailCashData_header_1");

            }

                


        }

        public static  List<Control> GetAllControls(Control container, List<Control> list)
        {
            foreach (Control c in container.Controls)
            {

                if (c.Controls.Count > 0)
                    list = GetAllControls(c, list);
                else
                    list.Add(c);
            }

            return list;
        }
        public static  List<Control> GetAllControls(Control container)
        {
            return GetAllControls(container, new List<Control>());
        }

        public static string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

        public static void TraceFunction(string elementoTrace)
        {
          
            //if (string.IsNullOrWhiteSpace(path))
            //    throw new ArgumentOutOfRangeException(nameof(path), path, "Was null or whitepsace.");

            //if (!File.Exists(path))
            //    throw new FileNotFoundException("File not found.", nameof(path));

            //using (var file = File.Open(path, FileMode.Append, FileAccess.Write))
            //using (var writer = new StreamWriter(file))
            //{
            //    await writer.WriteLineAsync(line);
            //    await writer.FlushAsync();
            //}
        }

        public static void BottoneBack(FormNovus myForm,UserControl uc)
        {
            myForm.pnContent.Controls.Clear();
            myForm.pnContent.Visible = true;
            uc.Dock = DockStyle.Fill;
            uc.Show();
            myForm.pnContent.Controls.Add(uc);
        }

        public static void BottoneNext(FormNovus myForm, UserControl uc)
        {
            myForm.pnContent.Controls.Clear();
            myForm.pnContent.Visible = true;
            uc.Dock = DockStyle.Fill;
            FormNovus.NameUserControlActive = uc.Name;
            uc.Show();
            myForm.pnContent.Controls.Add(uc);
        }


        
       

      




    }
        
};
