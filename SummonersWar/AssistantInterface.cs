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
using SummonersWar.Model;

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

        List<Script_data> data = new List<Script_data>();
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
                    Point ptr = SearchImage(data[Index].img, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);
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

                    Point btmap = Search.SearchPixelBitmap((Bitmap)source.Clone(), (Bitmap)data[Index].img, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);
                    Point lockmap = Search.SearchLockBitmap((Bitmap)source.Clone(), (Bitmap)data[Index].img, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);

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
            Bitmap src = (Bitmap)Source.Clone(), compare = (Bitmap)data[Index].img.Clone();
            //Point BitResult = Search.SearchPixelBitmap(src, compare, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);
            Point Result = Search.SearchLockBitmap((Bitmap)Source.Clone(), compare, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);

            if (Result.X == -1 && Result.Y == -1)
            {
                this.Text = "AutoClick Searching Failure , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";

                if (data[Index].IsForced)
                {
                    SendClickEvents(Index);
                    Index = (data.Count - 1 == Index) ? 0 : Index + 1;
                }
            }
            else
            {
                this.Text = "AutoClick Searching Success , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";

                Thread.Sleep(100);
                SendClickEvents(Index);
                Thread.Sleep(data[Index].delaytime);

                Index = (data.Count - 1 == Index) ? 0 : Index + 1;

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
                    Reload_Imagedata();
                    
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
        
        private void SendClickEvents(int KeyOptions)
        {
            int KeyValue = (int)Keys.A + KeyOptions;

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

        private void Reload_Imagedata()
        {
            List<Image_directory> list = new List<Image_directory>();
            data = new List<Script_data>();
            using (StreamReader sr = new StreamReader(SetupFilePath))
            {
                string json = sr.ReadToEnd();
                
                //ClipSearchPoints = new List<Point>();
                //delayTimes = new List<int>();
                if (json != string.Empty)
                {
                    try
                    {
                        list = JsonConvert.DeserializeObject<List<Image_directory>>(json);
                        for (int i = 0; i < list.Count; i++)
                        {
                            data.Add(new Script_data() { img = Image.FromFile(list[i].path), delaytime = list[i].delaytime, IsForced = list[i].IsForceClick, SearchPoints = new Point(-1, -1) });
                            //Imagelist.Add(Image.FromFile(list[i].path));
                            //delayTimes.Add(list[i].delaytime);
                            //ClipSearchPoints.Add(new Point(-1, -1));
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
