using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SummonersWar
{
    public partial class AssistantInterface : Form
    {
        WindowsAPI window = new WindowsAPI(); 
        Messages Message = new Messages();

        ScreenCapture CaptureScreen = new ScreenCapture();
        SearchImage Search = new SearchImage();
        ImageStorage isg = new ImageStorage();

        int Index = 0;
        bool IsAssistantWorking = false;
        string SetupFilePath = System.IO.Directory.GetCurrentDirectory() + "\\ImgSetup.json";

        IntPtr AssistantHwnd = IntPtr.Zero;

        GlobalKeyboardHook hk;

        List<Image> Imagelist = new List<Image>();
        List<Image> Clip = new List<Image>();
        List<Point> ClipSearchPoints = new List<Point>();
        List<IntPtr> AssistantWindowHwndList = new List<IntPtr>();
       
        IntPtr BlueStackHwnd = IntPtr.Zero;
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

        public AssistantInterface()
        {
            InitializeComponent();
            InitializeAutoClickFunction();
        }

        private void InitializeAutoClickFunction()
        {
            IntPtr target = window.ToFindWindow("BlueStacks");
            List<IntPtr> target_list = EnumWindowsList(target);

            hk = new GlobalKeyboardHook();
            hk.KeyDown += new KeyEventHandler(gHook_KeyDown);
            hk.KeyUp += new KeyEventHandler(gHook_KeyUp);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                hk.HookedKeys.Add(key);

            hk.hook();
            //Full Area keyboardhooking

            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Crystal.png")); ClipSearchPoints.Add(new Point(1379, 579));
            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Sell.png")); ClipSearchPoints.Add(new Point(730, 867));
            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Confirm.png")); ClipSearchPoints.Add(new Point(-1, -1));
            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Map.png")); ClipSearchPoints.Add(new Point(1257, 878));

            // Capture Image , Points add into List
        }

        #region Key Events

        private void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            switch ((int)e.KeyValue)
            {
                /// Buttom 'F1'
                /// 
                case 112:
                    SimulateClickTimer.Start();
                    this.Text = "AutoClick Start.";
                    break;
                /// Buttom 'F2'
                /// Search Assistant image with Clip[Index]
                case 113:
                    SimulateClickTimer.Stop();
                    this.Text = "AutoClick Stop.";
                    break;
                /// Buttom 'F3'
                /// Search Image
                case 114:
                    //Point pt = SearchImage(Clip[Index], -1, -1);
                    Point ptr = SearchImage(Clip[Index], ClipSearchPoints[Index].X, ClipSearchPoints[Index].Y);
                    this.Text = "Search Result : x :  " + ptr.X + " , y : " + ptr.Y; 
                    break;
                /// Buttom 'F4'
                /// Capture current window image
                case 115:
                    Image Picture = CaptureScreen.CapturehWndWindow(AssistantHwnd);
                    Picture.Save(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_Image.png");
                    Console.WriteLine("Image has been saved at : " + Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_Image.png");
                    this.Text = "Image has been saved at : " + Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss").ToString() + "_Image.png";

                    break;
                /// Buttom 'F5' , Set IndexCounter => 0
                /// 
                case 116:
                    Index = 0;
                    this.Text = "Index = " + Index.ToString();
                    break;
                /// Buttom 'F6' , Index ++
                /// 
                case 117:
                    Index++;
                    this.Text = "Add index , Index = " + Index.ToString();
                    break;
                /// Buttom 'F7'  
                /// Send ClickEvents ('A' + Index) to Assistant
                case 118:
                    this.Text = "Index = " + Index.ToString() + " , Send Click Events. ";
                    SendClickEvents(Index);
                    break;
                /// Buttom 'F8'  
                /// 
                case 119:
                    Image source = CaptureScreen.CapturehWndWindow(AssistantHwnd);

                    Point btmap = Search.SearchPixelBitmap((Bitmap)source.Clone(), (Bitmap)Clip[Index], ClipSearchPoints[Index].X, ClipSearchPoints[Index].Y);
                    Point lockmap = Search.SearchLockBitmap((Bitmap)source.Clone(), (Bitmap)Clip[Index], ClipSearchPoints[Index].X, ClipSearchPoints[Index].Y);

                    Console.WriteLine("btmap : " + btmap);
                    Console.WriteLine("lockmap : " + lockmap);
                    break;
            }
        }
        private void gHook_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {

            }
        }

        #endregion

        #region Timer Events

        private void SimulateClickTimer_Tick(object sender, EventArgs e)
        {
            Image Source = CaptureScreen.CapturehWndWindow(AssistantHwnd);
            Bitmap src = (Bitmap)Source.Clone(), compare = (Bitmap)Imagelist[Index].Clone();
            Point BitResult = Search.SearchPixelBitmap(src, compare, ClipSearchPoints[Index].X, ClipSearchPoints[Index].Y);
            Point Result = Search.SearchLockBitmap((Bitmap)Source.Clone(), compare, ClipSearchPoints[Index].X, ClipSearchPoints[Index].Y);            
            // Memory problem.

            if (Result.X == -1 && Result.Y == -1)
                this.Text = "AutoClick Searching Failure , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";
            else
            {
                this.Text = "AutoClick Searching Success , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";

                if (ClipSearchPoints[Index].X == -1 && ClipSearchPoints[Index].Y == -1)
                    ClipSearchPoints[Index] = new Point(Result.X, Result.Y);
                Thread.Sleep(500);
                SendClickEvents(Index);
                switch (Index)
                {
                    case 0:
                        Thread.Sleep(200);
                        SendClickEvents(Index);
                        Thread.Sleep(500);
                        SendClickEvents(Index);
                        Thread.Sleep(800);
                        SendClickEvents(Index);

                        Thread.Sleep(1000); //Wait ME

                        Thread.Sleep(800);
                        SendClickEvents(1);
                        Thread.Sleep(800);
                        SendClickEvents(2);

                        Index = 3;
                        break;
                    case 1:
                    case 2:
                    case 3:
                        //Index = 3;
                        Index = 0;
                        break;
                    case 4:
                        Index = 0;
                        break;
                }
                Thread.Sleep(200);
            }

            Source.Dispose();
            src.Dispose();
            compare.Dispose();
        }

        private void BSRunningTimer_Tick(object sender, EventArgs e)
        {
            IntPtr ptr = window.ToFindWindow("BlueStacks");
            if (ptr != IntPtr.Zero)
                BStatus.Text = "BlueStacks is Running ... ";
            else
                BStatus.Text = "BlueStacks is Closed.";
        }


        #endregion

        #region StripMenu Event

        private void listHwndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hWndList hwnd = new hWndList();
            AssistantHwnd = hwnd.Handle;
            hwnd.Show();
        }

        private void summonerWarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(window.ToShowWindowAsync(target, 3));//3 max 2 min 1 normal

            if (!IsAssistantWorking)
            {
                IntPtr TargetHwnd = window.ToFindWindow("BlueStacks");
                BlueStackHwnd = TargetHwnd;

                window.ToShowWindowAsync(TargetHwnd, 3);
                Thread.Sleep(500);
                WindowsAPI.RECT rect = window.ToGetWindowRect(TargetHwnd);

                if (TargetHwnd != IntPtr.Zero)
                {
                    AssistantWindowHwndList = EnumWindowsList(TargetHwnd);

                    AStatus.Text = "Assistant is Working ...";
                    SummonerAssistant Assistant = new SummonerAssistant(TargetHwnd, rect);
                    AssistantHwnd = Assistant.Handle;
                    Assistant.Show();

                    window.ToShowWindowAsync(TargetHwnd, 3);
                    // reload image list and SearchPoints
                    Reload_Imagelist();
                    
                }
                else
                {
                    AStatus.Text = "Assistant failed working ...";
                }
            }
        }

        private void imageReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageStorage isg = new ImageStorage();
            isg.Show();

        }
        #endregion

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private List<IntPtr> EnumWindowsList(IntPtr ptr)
        {
            List<IntPtr> list = new List<IntPtr>();
            IntPtr pre = IntPtr.Zero;
                        
            while (ptr != IntPtr.Zero)
            {
                list.Add(ptr);
                pre = ptr;
                ptr = window.ToFindWindowEx(ptr, IntPtr.Zero, null, null);
            }

            return list;
        }

        private Point SearchImage(Image SearchImg , int x , int y)
        {
            ///Point pt = Search.SearchBitmap((Bitmap)pic, (Bitmap)src); Memory Locked.
            Image pic = CaptureScreen.CapturehWndWindow(AssistantHwnd);

            Point pt = Search.SearchLockBitmap((Bitmap)pic.Clone(), (Bitmap)SearchImg.Clone(), x, y);
            //Point bpt = Search.SearchPixelBitmap((Bitmap)pic, (Bitmap)SearchImg, x, y);

            Console.WriteLine("Recieve Point : " + x + " , " + y + " . Result : " + pt.X + " , " + pt.Y);
            Console.WriteLine();
            
            pic.Dispose();
            return pt;
        }
        
        int wparam = 0;
        private void SendClickEvents(int KeyOptions)
        {
            int KeyValue = (int)Keys.A + KeyOptions;

            switch (KeyOptions)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    KeyValue = (int)Keys.A + KeyOptions;
                    break;
                case 4:
                    KeyValue = (int)Keys.A + KeyOptions - 1;
                    break;
            }

            //IntPtr ptr = BlueStackHwnd;
            //for (int i = 0; i < 3; i++)
            //{
            //    Message.ToPostMessage(ptr, (int)KeyBoardEventsFlag.WM_KEYDOWN, KeyValue, 0);
            //    Message.ToPostMessage(ptr, (int)KeyBoardEventsFlag.WM_KEYUP, KeyValue, 0);
            //    ptr = window.ToFindWindowEx(ptr, IntPtr.Zero, null, null);
            //}

            foreach (IntPtr p in AssistantWindowHwndList)
            {
                //window.ToFocusWindow(p);
                //int lparam = MouseX & 0xFFFF | (MouseY & 0xFFFF) << 16;
                
                Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYDOWN, KeyValue, 0);
                Thread.Sleep(10);
                Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYUP, KeyValue, 0);

                Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYDOWN, KeyValue, 0);
                Thread.Sleep(10);
                Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYUP, KeyValue, 0);

                Console.WriteLine("SendClick " + (Char)KeyValue + " pressed on " + p.ToString() + ".");
            }
        }

        private void AutoClick_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (AssistantHwnd != IntPtr.Zero)
            //{
            //    Process[] process = Process.GetProcessesByName("Bluestacks");

            //    process[0].Kill();

            //    process = Process.GetProcessesByName("HD-Agent");

            //    process[0].Kill();

            //    process = Process.GetProcessesByName("HD-Player");

            //    process[0].Kill();

            //    process = Process.GetProcessesByName("BstkSVC");

            //    process[0].Kill();

            //    //this.Close();
            //}
        }

        private void Reload_Imagelist()
        {
            List<Image_directory> list = new List<Image_directory>();
            using (StreamReader sr = new StreamReader(SetupFilePath))
            {
                string json = sr.ReadToEnd();
                ClipSearchPoints = new List<Point>();

                if (json != string.Empty)
                {
                    try
                    {
                        list = JsonConvert.DeserializeObject<List<Image_directory>>(json);
                        for (int i = 0; i < list.Count; i++)
                        { 
                            Imagelist.Add(Image.FromFile(list[i].path));
                            ClipSearchPoints.Add(new Point(-1, -1));
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            //JsonConvert
        }
    }

}
