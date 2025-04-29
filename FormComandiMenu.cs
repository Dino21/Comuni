using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTM_Testing
{
    public partial class FormComandiMenu : Form
    {
        public FormComandiMenu()
        {
            InitializeComponent();
        }
        public FormComandiMenu (string modalità)
        {
            InitializeComponent();
            switch(modalità)
            {
                case "status":
                    lblMessage.Text = "GET STATUS";
                    grpStatusModule.Visible = false;
                    grpMachineStatus.Visible = true;
                    lblRtcDec.Text = Form1.machineStatus.rtc.dec.ToString();
                    lblRtcHex.Text = Form1.machineStatus.rtc.hex;
                    lblRtcName.Text = Form1.machineStatus.rtc.name;
                    lblRtcDescription.Text = Form1.machineStatus.rtc.description;

                    lblPho1Dec.Text = Form1.machineStatus.pho1.dec.ToString();
                    lblPho1Hex.Text = Form1.machineStatus.pho1.hex;
                    lblPho1Name.Text = Form1.machineStatus.pho1.name;
                    lblPho1Description.Text = Form1.machineStatus.pho1.description;

                    lblPho2Dec.Text = Form1.machineStatus.pho2.dec.ToString();
                    lblPho2Hex.Text = Form1.machineStatus.pho2.hex;
                    lblPho2Name.Text = Form1.machineStatus.pho2.name;
                    lblPho2Description.Text = Form1.machineStatus.pho2.description;
                    break;
                case "preset":
                    lblMessage.Text = "PRESET";
                    grpStatusModule.Visible = true;
                    grpMachineStatus.Visible = false;
                    lblStHex.Text = Form1.modStatus.hex;
                    lblStDec.Text = Form1.modStatus.dec.ToString();
                    lblStDescription.Text = Form1.modStatus.description;
                    lblStName.Text = Form1.modStatus.name;
                    break;
            }
            
            
        }


    }
}
