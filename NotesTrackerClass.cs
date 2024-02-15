using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace NovusTool
{
    class NotesTrackerClass
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;    // Any value the sender chooses. For example, can be used to identify the type of data sent.
            public int cbData;       // Contains the length of the data in lpData.
            public IntPtr lpData;    // Pointer to the data.
        }

        const int WM_COPYDATA = 0x004A;
        //public const int WM_NOTES_TRACKER_MESSAGE = 0x20C5C;
        //public const int WM_NOTES_TRACKER_MESSAGE = 0x00020C5C;

        //For use with WM_COPYDATA and COPYDATASTRUCT
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, IntPtr lParam);

        //maudero - 06Jul2020 - for FindWindow()
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string sClass, string sWindow);

#if false
        public struct NOTESTRACKER_M1
        {
            //HWND theWind;
            //Int32 theWind;
            //char theReader[20];
            public byte[]  theReader;
            //char theReaderName[50];
            public byte[]  theReaderName;
            //char theSetupFileIdentifier[20];
            //public byte[] theSetupFileIdentifier;
        };
#else
        //maudero - 06Jul2020
        public struct NOTESTRACKER_M1
        {
            public Int32 theWind;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public byte[] theReader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)] public byte[] theReaderName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public byte[] theSetupFileIdentifier;
        };

        static string NotesTracker_WinTitle = "NotesTracker";
        static string NotesTracker_Exe = "NotesTracker\\NotesTracker";
        public static int NotesTracker_WinHandle = 0;
        public static int nNoteReport = 0;
#endif

        public struct NOTESTRACKER_M2
        {
            //HWND theWind;
            //int Notes;
            //int NotesBaseIndex;
            //bool batch;
            public Int32 theWind;
            public Int32 Notes;
            public Int32 NotesBaseIndex;
            public bool  batch;
        };

        // Allocate a pointer to an arbitrary structure on the global heap.
        public static IntPtr IntPtrAlloc<T>(T param)
        {
            IntPtr retval = Marshal.AllocHGlobal(Marshal.SizeOf(param));
            Marshal.StructureToPtr(param, retval, false);
            return retval;
        }

        // Free a pointer to an arbitrary structure from the global heap.
        public static void IntPtrFree(ref IntPtr preAllocated)
        {
            if (IntPtr.Zero == preAllocated)
                throw (new NullReferenceException("Go Home"));
            Marshal.FreeHGlobal(preAllocated);
        }



        public static void NotesTrackerSendMessage(IntPtr hwnd, int nMessage)
        {
            COPYDATASTRUCT cds = new COPYDATASTRUCT();

            switch (nMessage)
            {
                case 0:
                    NotesTracker_WinHandle = FindWindow(null, NotesTracker_WinTitle);
                    if (NotesTracker_WinHandle == 0)
                    {
                        try
                        {
                           var  proc = Process.Start(NotesTracker_Exe);
                            proc.WaitForInputIdle();
                            //proc.WaitForInputIdle(3000);
                            for (int xx = 0; xx < 50; xx++)
                            {
                                if (string.IsNullOrEmpty(proc.MainWindowTitle))
                                {
                                    System.Threading.Thread.Sleep(100);
                                    proc.Refresh();
                                    Application.DoEvents();
                                }
                                else
                                {
                                    //System.Threading.Thread.Sleep(5000);
                                    //proc.Refresh();
                                    //Application.DoEvents();
                                    break;
                                }
                            }
                            NotesTracker_WinHandle = FindWindow(null, NotesTracker_WinTitle);
                        }
                        catch(Exception ex )
                        {
                            Console.WriteLine(ex.ToString());
                            MessageBox.Show("Copy the soruce folder of NotesTracker under NovusTool directory and restart the appplication");

                        }
                    }
                    break;
                case 1:
                    //connect
#if false
                    NOTESTRACKER_M1 Message1 = new NOTESTRACKER_M1();
                    Message1.theReader = new byte[4];
                    Message1.theReader[0] = Convert.ToByte('R');
                    Message1.theReader[1] = Convert.ToByte('L');
                    Message1.theReader[2] = Convert.ToByte('4');
                    Message1.theReader[3] = Convert.ToByte('0');

                    Message1.theReaderName = new byte[4];
                    Message1.theReaderName[0] = Convert.ToByte('R');
                    Message1.theReaderName[1] = Convert.ToByte('L');
                    Message1.theReaderName[2] = Convert.ToByte('4');
                    Message1.theReaderName[3] = Convert.ToByte('0');

                    cds.dwData = (IntPtr)nMessage;
                    IntPtr buffer = IntPtrAlloc(Message1);
                    cds.lpData = buffer;
                    cds.cbData = Marshal.SizeOf(Message1);
                    IntPtr copyDataBuff = IntPtrAlloc(cds);
                    //SendMessage(hwnd.ToInt32(), WM_COPYDATA, 0,  copyDataBuff);
                    SendMessage(WM_NOTES_TRACKER_MESSAGE, WM_COPYDATA, 0, copyDataBuff);
                    IntPtrFree(ref copyDataBuff);
                    IntPtrFree(ref buffer);
#else
                    //maudero - 06Jul2020
                    NOTESTRACKER_M1 Message1 = new NOTESTRACKER_M1();
                    IntPtr ptrCopyData1 = IntPtr.Zero;
                    try
                    {
                        NotesTracker_WinHandle = FindWindow(null, NotesTracker_WinTitle);
                        if (NotesTracker_WinHandle == 0)
                        {
                            // run "NotesTracker"...
                            var proc = Process.Start(NotesTracker_WinTitle);
                            proc.WaitForInputIdle();
                            //proc.WaitForInputIdle(3000);
                            for ( int xx = 0; xx < 50; xx++ )
                            {
                                if ( string.IsNullOrEmpty(proc.MainWindowTitle) )
                                {
                                    System.Threading.Thread.Sleep(100);
                                    proc.Refresh();
                                    Application.DoEvents();
                                }
                                else
                                {
                                    //System.Threading.Thread.Sleep(5000);
                                    //proc.Refresh();
                                    //Application.DoEvents();
                                    break;
                                }
                            }
                            NotesTracker_WinHandle = FindWindow(null, NotesTracker_WinTitle);
                        }
                        if (NotesTracker_WinHandle != 0)
                        {
                            Message1.theWind = hwnd.ToInt32();
                            Message1.theReader = new byte[20];
                            Message1.theReader[0] = Convert.ToByte('R');
                            Message1.theReader[1] = Convert.ToByte('L');
                            Message1.theReader[2] = Convert.ToByte('4');
                            Message1.theReader[3] = Convert.ToByte('0');
                            //Message1.theReader[0] = Convert.ToByte('R');
                            //Message1.theReader[1] = Convert.ToByte('S');
                            //Message1.theReader[2] = Convert.ToByte('3');
                            //Message1.theReader[3] = Convert.ToByte('2');
                            Message1.theReader[4] = Convert.ToByte('\0');
                            Message1.theReaderName = new byte[50];
                            Message1.theReaderName[0] = Convert.ToByte('R');
                            Message1.theReaderName[1] = Convert.ToByte('L');
                            Message1.theReaderName[2] = Convert.ToByte('4');
                            Message1.theReaderName[3] = Convert.ToByte('0');
                            //Message1.theReaderName[0] = Convert.ToByte('R');
                            //Message1.theReaderName[1] = Convert.ToByte('S');
                            //Message1.theReaderName[2] = Convert.ToByte('3');
                            //Message1.theReaderName[3] = Convert.ToByte('2');
                            Message1.theReaderName[4] = Convert.ToByte('\0');
                            Message1.theSetupFileIdentifier = new byte[20];
                            Message1.theSetupFileIdentifier[0] = Convert.ToByte('Z'); // prova
                            Message1.theSetupFileIdentifier[1] = Convert.ToByte('\0');

                            cds.dwData = (IntPtr)nMessage; // Just a number to identify the data type
                            cds.cbData = Marshal.SizeOf(Message1); // 94
                            IntPtr buffer = IntPtrAlloc(Message1);
                            cds.lpData = buffer;

                            ptrCopyData1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(cds));
                            Marshal.StructureToPtr(cds, ptrCopyData1, false);

                            // Send the message
                            SendMessage(NotesTracker_WinHandle, WM_COPYDATA, 0, ptrCopyData1);

                            //IntPtrFree(ref ptrCopyData);
                            IntPtrFree(ref buffer);
                        }
                        else
                        {
                            MessageBox.Show(String.Format("No window found with the title {0}.", NotesTracker_WinTitle),
                                "NovusTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "NovusTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Free the allocated memory after the control has been returned
                        if (ptrCopyData1 != IntPtr.Zero)
                            Marshal.FreeCoTaskMem(ptrCopyData1);
                    }
#endif
                    break;

                case 2:
                    //work - Deposit Count
                    //maudero - 06Jul2020
                    NOTESTRACKER_M2 Message2 = new NOTESTRACKER_M2();
                    IntPtr ptrCopyData2 = IntPtr.Zero;
                    try
                    {
                        if (NotesTracker_WinHandle != 0)
                        {
                            Message2.theWind = hwnd.ToInt32();
                            Message2.Notes = nNoteReport; // rss["noteReportElements"]
                            Message2.NotesBaseIndex = 0;
                            Message2.batch = true;

                            cds.dwData = (IntPtr)nMessage; // Just a number to identify the data type
                            cds.cbData = Marshal.SizeOf(Message2);
                            IntPtr buffer = IntPtrAlloc(Message2);
                            cds.lpData = buffer;

                            ptrCopyData2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(cds));
                            Marshal.StructureToPtr(cds, ptrCopyData2, false);

                            // Send the message
                            SendMessage(NotesTracker_WinHandle, WM_COPYDATA, 0, ptrCopyData2);

                            //IntPtrFree(ref ptrCopyData2);
                            IntPtrFree(ref buffer);
                        }
                        else
                        {
                            MessageBox.Show(String.Format("No window found with the title {0}.", NotesTracker_WinTitle),
                                "NovusTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "NovusTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Free the allocated memory after the control has been returned
                        if (ptrCopyData2 != IntPtr.Zero)
                            Marshal.FreeCoTaskMem(ptrCopyData2);
                    }
                    break;

                case 3:
                    //disconnect (just like <connect> but all items are empty)
                    //maudero - 06Jul2020
                    NOTESTRACKER_M1 Message3 = new NOTESTRACKER_M1();
                    IntPtr ptrCopyData3 = IntPtr.Zero;
                    try
                    {
                        if (NotesTracker_WinHandle != 0)
                        {
                            Message3.theWind = hwnd.ToInt32();
                            Message3.theReader = new byte[20];
                            Message3.theReader[0] = Convert.ToByte('\0');
                            Message3.theReaderName = new byte[50];
                            Message3.theReaderName[0] = Convert.ToByte('\0');
                            Message3.theSetupFileIdentifier = new byte[20];
                            Message3.theSetupFileIdentifier[0] = Convert.ToByte('\0');

                            cds.dwData = (IntPtr)nMessage; // Just a number to identify the data type
                            cds.cbData = Marshal.SizeOf(Message3);
                            IntPtr buffer = IntPtrAlloc(Message3);
                            cds.lpData = buffer;

                            ptrCopyData3 = Marshal.AllocCoTaskMem(Marshal.SizeOf(cds));
                            Marshal.StructureToPtr(cds, ptrCopyData3, false);

                            // Send the message
                            SendMessage(NotesTracker_WinHandle, WM_COPYDATA, 0, ptrCopyData3);

                            //IntPtrFree(ref ptrCopyData3);
                            IntPtrFree(ref buffer);
                        }
                        else
                        {
                            MessageBox.Show(String.Format("No window found with the title {0}.", NotesTracker_WinTitle),
                                "NovusTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "NovusTool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Free the allocated memory after the control has been returned
                        if (ptrCopyData3 != IntPtr.Zero)
                            Marshal.FreeCoTaskMem(ptrCopyData3);
                    }
                    break;

                case 4:
                    //CLOSE SESSION: READY TO COLLECT STATISTICS
                    //cds.cbData = sizeof(theApp.NOTESTRACKER_Message4);
                    //cds.lpData = &theApp.NOTESTRACKER_Message4;
                    break;

                default:
                    // code block
                    break;
            }//switch (nMessage)
        }
    }
}
