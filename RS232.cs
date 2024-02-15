using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;

namespace CM18tool
{
    public class RS232

    {
        public static SerialPort mySerial = new SerialPort();
        Stopwatch myTimer = new Stopwatch();
        public static string answerString;
        public static byte[] answerByte;
        public string commandAnswer = "";
        public string commandSent = "";
        //ComScope frmComScope = new ComScope();
        public bool DTR = false;
        public bool RTS = false;
        public event EventHandler DataReceived;
        public event EventHandler DataSent;
        public RS232()
        {
            //frmComScope.Show();
            ConfigCom("COM4");
            myTimer.Reset();
        }


        /// <summary>
        /// Open the COM port if closed
        /// </summary>
        public bool OpenCom()
        {
            //mySerial.Close();
            if (!mySerial.IsOpen)
            {
                try
                {
                    mySerial.Open();
                }
                catch { return false; }
            }
            return true;
            try
            {
                if (!mySerial.IsOpen)
                    mySerial.Open();
                return true;
            }
            catch
            {
                return false;
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

        public void SetDtrOn (bool enable)
        {
            if (enable == true)
            {
                mySerial.DtrEnable = true;
                DTR = true;
            }
        }

        public void SetDtrOff()
        {
            mySerial.DtrEnable = false;
            DTR = false;
        }

        public void SetRtsOn ()
        {
            mySerial.RtsEnable = true;
            RTS = true;
        }

        public void SetRtsOff()
        {
            mySerial.RtsEnable = false;
            RTS = false;
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
            //mySerial.DtrEnable = DTR;
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
            } while (mySerial.BytesToRead == 0 && commandAnswer == "") ;
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

        public string SendCommand(string command,string FwData, int nByteAnswer = 1, int timeOut = 5000)
        {
            //if (FwData.Substring(0, 1) == ":")
            //    FwData = "3A" + FwData.Substring(1);
            string tmp = "";
            for (int i = 0; i < FwData.Length; i++)
            {
                tmp += Arca.Ascii2Hex(FwData.Substring(i, 1));
            }
            commandSent = command + tmp;
            command += tmp;
            if (mySerial.IsOpen == false)
            {
                try
                {
                    mySerial.Open();
                }
                catch (System.IO.IOException ex)
                {
                    commandAnswer = "";
                    return ex.Message;
                }
            }
            int dim = command.Length % 2;
            if (dim != 0)
                command = "0" + command;

            byte[] commandToSend = new byte[command.Length / 2];
            int index = 0;
            commandAnswer = "";
            //string serialAnswer = "";
            int bytesReceived = 0;

            for (int i = 1; i < command.Length; i += 2)
            {
                commandToSend[index] = Convert.ToByte(command.Substring(i - 1, 2), 16);
                index += 1;
            }
            mySerial.DiscardInBuffer();
            myTimer.Restart();
            //frmComScope.lstComScope.Items.Add("SND: " + command);
            DataSent(command, null);
            mySerial.Write(commandToSend, 0, command.Length / 2);
            Arca.WaitingTime(100);
            if (nByteAnswer > 0)
            {
                //serialRead:
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
                byte[] dataReceived = new byte[bytesReceived];
                mySerial.Read(dataReceived, 0, bytesReceived);
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
            return commandAnswer;
        }

        public string SendCommand(string command, int nByteAnswer=1, int timeOut=5000 )
        {
            commandSent = command;
            if (mySerial.IsOpen == false)
            {
                try
                {
                    mySerial.Open();
                }
                catch (System.IO.IOException ex)
                {
                    commandAnswer = "";
                    return ex.Message;
                }
            }
            int dim = command.Length %2;
            if (dim != 0)
                command = "0" + command;
            
            byte[] commandToSend = new byte[command.Length / 2];
            int index = 0;
            commandAnswer = "";
            //string serialAnswer = "";
            int bytesReceived = 0;

            for (int i = 1; i < command.Length; i += 2)
            {
                commandToSend[index] = Convert.ToByte(command.Substring(i - 1, 2), 16);
                index += 1;
            }
            mySerial.DiscardInBuffer();
            myTimer.Restart();
            //frmComScope.lstComScope.Items.Add("SND: " + command);
            DataSent(command, null);
            mySerial.Write(commandToSend, 0, command.Length/2);
            Arca.WaitingTime(100);
            if (nByteAnswer > 0)
            {
                //serialRead:
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
                byte[] dataReceived = new byte[bytesReceived];
                mySerial.Read(dataReceived, 0, bytesReceived);
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
            return commandAnswer;
        }
        public string SendHexCommand(string command, int nByteAnswer = 1, int timeOut = 5000)
        {
            commandSent = command;
            if (mySerial.IsOpen == false)
            {
                try
                {
                    mySerial.Open();
                }
                catch (System.IO.IOException ex)
                {
                    commandAnswer = "";
                    return ex.Message;
                }
            }
            int dim = command.Length % 2;
            if (dim != 0)
                command = "0" + command;

            byte[] commandToSend = new byte[command.Length / 2];
            int index = 0;
            commandAnswer = "";
            //string serialAnswer = "";
            int bytesReceived = 0;

            for (int i = 1; i < command.Length; i += 2)
            {
                commandToSend[index] = Convert.ToByte(command.Substring(i - 1, 2), 16);
                index += 1;
            }
            mySerial.DiscardInBuffer();
            myTimer.Restart();
            //frmComScope.lstComScope.Items.Add("SND: " + command);
            DataSent(command, null);
            mySerial.Write(commandToSend, 0, command.Length / 2);
            Arca.WaitingTime(100);
            if (nByteAnswer > 0)
            {
                //serialRead:
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
                byte[] dataReceived = new byte[bytesReceived];
                mySerial.Read(dataReceived, 0, bytesReceived);
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
            return commandAnswer;
        }
    }
}
