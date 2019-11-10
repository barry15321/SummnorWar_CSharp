using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SummonersWar
{
    public partial class AutoClick : Form
    {
        WindowClass BasicWindow = new WindowClass();
        ScreenCapture CaptureScreen = new ScreenCapture();
        SearchImage Search = new SearchImage();
        MessageHandlingClass Message = new MessageHandlingClass();

        List<IntPtr> AssistantUnderPtr = new List<IntPtr>();
        bool IsAssistantWorking = false;
        int IndexCounter = 0;
        IntPtr AssistantHwnd = IntPtr.Zero;

        GlobalKeyboardHook hk;
        List<Image> ClipImage = new List<Image>();
        List<Point> ClipSearchPoints = new List<Point>();
        List<IntPtr> AssistantWindowHwndList = new List<IntPtr>();

        // Bluestack 1280 * 720

        public enum KeyBoardEventsFlag
        {
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WM_CHAR = 0x102,
            WM_SYSTEMCHAR = 0x106,
            WM_SYSTEMKEYUP = 0x105,
            WM_SYSTEMKEYDOWN = 0x104,
            WM_MOUSE_MOVE = 0x200,
            WM_LBUTTON_DOWN = 0x201,
            WM_LBUTTON_UP = 0x202
        }

        public enum MouseEventsFlag
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        //private const int WM_SETTEXT = 0x000c;        
        //private const int WM_MOUSE_MOVE = 0x0200;
        //private const int WM_LBUTTON_DOWN = 0x0201; //'0x C# 16x = &H
        //private const int WM_LBUTTON_UP = 0x0202;
        //private const int WM_KEYDOWN = 0x0100;
        //private const int WM_KEYUP = 0x0101;
        //private const int WM_CHAR = 0x0102;

        public AutoClick()
        {
            InitializeComponent();
            keyboardhooking();
            ClipImageLoadFunction();
        }

        /// <summary>
        /// Summoner's War Assistant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listHwndToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            hWndList hwnd = new hWndList();
            AssistantHwnd = hwnd.Handle;
            hwnd.Show();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            //test();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Reset();
            sw.Start();

            Point pt = new Point(-1, -1);

            Image Source = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CaptureScreen.png");
            Image SubImg = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\cyrstal.png");
                        
            //pt = SearchBitmap((Bitmap)Source, (Bitmap)SubImg, 1109, 86);
            pt = Search.SearchBitmap((Bitmap)Source, (Bitmap)SubImg, -1, -1);

            sw.Stop();

            Console.WriteLine("StopWatch : " + sw.Elapsed.TotalMilliseconds.ToString() + "ms .");
            Console.WriteLine("Result : (" + pt.X + " , " + pt.Y + ")");

        }
                
        private void summonerWarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsAssistantWorking)
            {
                IntPtr TargetHwnd = BasicWindow.ToFindWindow("BlueStacks");

                WindowClass.RECT rect = BasicWindow.ToGetWindowRect(TargetHwnd);

                if (TargetHwnd != IntPtr.Zero)
                {
                    AStatus.Text = "Assistant is Working ...";
                    SummonerAssistant Assistant = new SummonerAssistant(TargetHwnd, rect);
                    AssistantHwnd = Assistant.Handle;
                    Assistant.Show();
                    
                    AssistantWindowHwndList = EnumWindowsList(AssistantHwnd);
                    foreach (IntPtr p in AssistantWindowHwndList)
                    {
                        Console.WriteLine("ptr : " + p.ToString());
                        Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYDOWN, (int)Keys.C, 0);
                    }
                }
                else
                {
                    AStatus.Text = "Assistant failed working ...";
                }
            }
        }

        private void BSRunningTimer_Tick(object sender, EventArgs e)
        {
            IntPtr ptr = BasicWindow.ToFindWindow("BlueStacks");
            if (ptr != IntPtr.Zero)
                BStatus.Text = "BlueStacks is Running ... ";
            else
                BStatus.Text = "BlueStacks is Closed.";
        }
        
        private void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            switch ((int)e.KeyValue)
            {
                case 112: //F1
                    this.Text = "Index = " + IndexCounter.ToString() + " , Send Click Events. ";
                    SendClickEvents(IndexCounter);
                    break;
                case 113: //F2
                    Point pt = SearchImage(ClipImage[IndexCounter], -1, -1);
                    Point ptr = SearchImage(ClipImage[IndexCounter], ClipSearchPoints[IndexCounter].X, ClipSearchPoints[IndexCounter].Y);
                    this.Text = "Index = " + IndexCounter.ToString() + " , Point Console : " + pt.X.ToString() + " , " + pt.Y.ToString();
                    Console.WriteLine("Index = " + IndexCounter.ToString() + " , Point -1 Console : " + pt.X.ToString() + " , " + pt.Y.ToString());

                    if (pt.X != -1 && pt.Y != -1)
                        SendClickEvents(IndexCounter);                    
                    break;
                case 114: //F3
                    SaveImageFunction();
                    this.Text = "Image Saved , Location : " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss").ToString() + "_TestImage.png";
                    break;
                case 115: //F4
                    IndexCounter = 0;
                    this.Text = "Index = " + IndexCounter.ToString();
                    break;
                case 116: //F5
                    IndexCounter++;
                    this.Text = "Add index , Index = " + IndexCounter.ToString();
                    break;
                //
                case 117: //F6
                    break;
                //    
                case 118: //F7
                    SimulateClickTimer.Start();
                    this.Text = "AutoClick Start.";
                    break;
                case 119: //F8
                    SimulateClickTimer.Stop();
                    this.Text = "AutoClick Stop.";
                    break;
            }
        }

        private void gHook_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private List<IntPtr> EnumWindowsList(IntPtr ptr)
        {
            List<IntPtr> list = new List<IntPtr>();
            IntPtr pre = IntPtr.Zero;
                        
            while (ptr != IntPtr.Zero)
            {
                list.Add(ptr);
                pre = ptr;
                ptr = BasicWindow.ToFindWindowEx(ptr, IntPtr.Zero, null, null);
            }

            return list;
        }
        
        /// <summary>
        /// HookedKeys Initital function
        /// </summary>
        private void keyboardhooking()
        {
            hk = new GlobalKeyboardHook();
            hk.KeyDown += new KeyEventHandler(gHook_KeyDown);
            hk.KeyUp += new KeyEventHandler(gHook_KeyUp);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                hk.HookedKeys.Add(key);
            
            hk.hook();
        }

        private int MakeParam(int key , int lparam)
        {
            return key + (lparam << 16);
        }

        private void SaveImageFunction()
        {
            Image pic = CaptureScreen.CapturehWndWindow(AssistantHwnd);
            pic.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_TestImage.png");

            Console.WriteLine("Image Saved at : " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_TestImage.png");
        }

        private Point SearchImage(Image SearchImg , int x , int y)
        {
            Image pic = CaptureScreen.CapturehWndWindow(AssistantHwnd);
            Console.WriteLine("Recieve Point : " + x + " , " + y);

            ///Point pt = Search.SearchBitmap((Bitmap)pic, (Bitmap)src); Memory Locked.
            Point pt = Search.SearchBitmap((Bitmap)pic.Clone(), (Bitmap)SearchImg.Clone(), x, y);
            
            Console.WriteLine("pt : " + pt.X + " , " + pt.Y);
            Console.WriteLine();

            return pt;
        }
        
        private void ClipImageLoadFunction()
        {
            ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\315404_BlueCrystal.png")); // blue
            ClipSearchPoints.Add(new Point(315, 404));
            ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\638413_Flash.png")); // flash
            ClipSearchPoints.Add(new Point(638, 413));
            ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\946410_RedCrystal.png")); // crystal
            ClipSearchPoints.Add(new Point(946, 410));
            ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\472635_sellblue.png"));
            ClipSearchPoints.Add(new Point(472, 635));
            ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\598594_confirm.png"));
            ClipSearchPoints.Add(new Point(598, 594));
            ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\311420_again.png"));
            ClipSearchPoints.Add(new Point(311, 420));

            //ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\315404_BlueCrystal.png")); // blue
            //ClipSearchPoints.Add(new Point(-1, -1));
            //ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\638413_Flash.png")); // flash
            //ClipSearchPoints.Add(new Point(638, 413));
            //ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\946410_RedCrystal.png")); // crystal
            //ClipSearchPoints.Add(new Point(-1, -1));
            //ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\472635_sellblue_copy.png"));
            //ClipSearchPoints.Add(new Point(-1, -1));
            //ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\598594_confirm_copy.png"));
            //ClipSearchPoints.Add(new Point(-1, -1));
            //ClipImage.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Clip\\311420_again_copy.png"));
            //ClipSearchPoints.Add(new Point(-1, -1));

            Console.WriteLine("Image loaded.");
        }

        private void SendClickEvents(int KeyOptions)
        {
            int KeyValue = (int)Keys.A + KeyOptions;
            foreach (IntPtr p in AssistantWindowHwndList)
            {
                Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYDOWN, KeyValue, 0);
                Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYUP, KeyValue, 0);
            }
            Console.WriteLine("Assistance loop press.");
        }

        private void SimulateClickTimer_Tick(object sender, EventArgs e)
        {
            Image Source = CaptureScreen.CapturehWndWindow(AssistantHwnd);

            Point Result = Search.SearchBitmap((Bitmap)Source, (Bitmap)ClipImage[IndexCounter], -1, -1);

            if (Result.X == -1 && Result.Y == -1)
                this.Text = "AutoClick Searching Failure , Index = " + IndexCounter.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";
            else
            {
                this.Text = "AutoClick Searching Success , Index = " + IndexCounter.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";

                SendClickEvents(IndexCounter);
                switch (IndexCounter)
                {
                    case 0:
                        Thread.Sleep(200);
                        SendClickEvents(IndexCounter);
                        Thread.Sleep(400);
                        SendClickEvents(IndexCounter);
                        Thread.Sleep(600);
                        SendClickEvents(IndexCounter);

                        //Image SubScreen = CaptureScreen.CapturehWndWindow(AssistantHwnd);
                        //Point SubResult = Search.SearchBitmap((Bitmap)SubScreen, (Bitmap)ClipImage[1], -1, -1);
                        //if (SubResult.X == -1 && SubResult.Y == -1) // check
                        //{

                        //}
                        //else // equal to sell
                        //{

                        //}                        
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }
        }
        
    }
}
