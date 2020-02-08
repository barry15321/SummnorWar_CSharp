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

        int Index = 0;
        bool IsAssistantWorking = false;
        IntPtr AssistantHwnd = IntPtr.Zero;

        GlobalKeyboardHook hk;
        List<Image> Clip = new List<Image>();
        List<Point> ClipSearchPoints = new List<Point>();
        List<IntPtr> AssistantWindowHwndList = new List<IntPtr>();
        // Bluestack 1280 * 720
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

        public AutoClick()
        {
            InitializeComponent();
            InitializeAutoClickFunction();
        }

        private void InitializeAutoClickFunction()
        {
            hk = new GlobalKeyboardHook();
            hk.KeyDown += new KeyEventHandler(gHook_KeyDown);
            hk.KeyUp += new KeyEventHandler(gHook_KeyUp);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                hk.HookedKeys.Add(key);

            hk.hook();
            //Full Area keyboardhooking

            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Crystal.png")); ClipSearchPoints.Add(new Point(1165, 446));
            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\SellButton.png")); ClipSearchPoints.Add(new Point(678, 671));
            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\ConfirmButton.png")); ClipSearchPoints.Add(new Point(782, 699));
            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\AgainButton.png")); ClipSearchPoints.Add(new Point(375, 459));
            Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\FireMountain\\Map.png")); ClipSearchPoints.Add(new Point(1078, 675));

            //Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\ClipMax\\467447_Step1.png")); ClipSearchPoints.Add(new Point(467, 447));
            //Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\ClipMax\\678671_Step2_1.png")); ClipSearchPoints.Add(new Point(678, 671));
            //Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\ClipMax\\612352_unknownbook_Step2_2.png")); ClipSearchPoints.Add(new Point(612, 352));
            //Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\ClipMax\\602305_Step2_3.png")); ClipSearchPoints.Add(new Point(602, 305));
            //Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\ClipMax\\1078676_Step3.png")); ClipSearchPoints.Add(new Point(1078, 676));
            //Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\ClipMax\\772647_notsure_book.png")); ClipSearchPoints.Add(new Point(772, 647));
            //Clip.Add(Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\ClipMax\\775691_monsterck.png")); ClipSearchPoints.Add(new Point(775, 691));

            // Capture Image , Points add into List
        }

        private void listHwndToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            hWndList hwnd = new hWndList();
            AssistantHwnd = hwnd.Handle;
            hwnd.Show();
        }

        private void summonerWarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsAssistantWorking)
            {
                IntPtr TargetHwnd = BasicWindow.ToFindWindow("BlueStacks");
                BlueStackHwnd = TargetHwnd;

                WindowClass.RECT rect = BasicWindow.ToGetWindowRect(TargetHwnd);

                if (TargetHwnd != IntPtr.Zero)
                {
                    AStatus.Text = "Assistant is Working ...";
                    SummonerAssistant Assistant = new SummonerAssistant(TargetHwnd, rect);
                    AssistantHwnd = Assistant.Handle;
                    Assistant.Show();

                    AssistantWindowHwndList = EnumWindowsList(TargetHwnd);
                    //for (int i = 0; i < AssistantWindowHwndList.Count; i++)
                    //    Console.WriteLine(AssistantWindowHwndList[i]);
                }
                else
                {
                    AStatus.Text = "Assistant failed working ...";
                }
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            //help me get this problem
            

            List<Color> wrongpixel = new List<Color>();
            List<Point> wrongpoint = new List<Point>();

            List<Color> imgpx = new List<Color>();
            List<Color> lockpx = new List<Color>();
            Image src_img = Image.FromFile(Environment.CurrentDirectory + "\\SourceMax\\2019-11-17-08-27-28_Image.png");
            Image clip_img = Image.FromFile(Environment.CurrentDirectory + "\\ClipMax\\772647_notsure_book.png");
            Bitmap lock_img = (Bitmap)clip_img.Clone();

            Point pr = Search.SearchLockBitmap((Bitmap)src_img.Clone() , (Bitmap)clip_img.Clone());
            Point ptr = Search.SearchPixelBitmap((Bitmap)src_img.Clone(), (Bitmap)clip_img.Clone());

            Console.WriteLine(pr.X + " , " + pr.Y);
            Console.WriteLine(ptr.X + " , " + ptr.Y);
            //int x = 785;
            //int y = 665;
            //LockBitmap lockbmp = new LockBitmap(lock_img);
            //lockbmp.LockBits();
            //for (int i = 0; i < clip_img.Width; i++)
            //{
            //    for (int j = 0; j < clip_img.Height; j++)
            //    {
            //        if (((Bitmap)src_img).GetPixel(i + x, j + y) != lockbmp.GetPixel(i, j))
            //        {
            //            wrongpoint.Add(new Point(i, j));
            //        }
            //    }
            //}

            //lockbmp.UnlockBits();

            //Console.WriteLine("wrong pixel counts : " + wrongpoint.Count);
            //for (int i = 0; i < wrongpoint.Count; i++)
            //{
            //    Console.WriteLine(wrongpoint[i]);
            //}
        }
               
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

                    //Bitmap bmp = (Bitmap)source.Clone();
                    //LockBitmap lockbit = new LockBitmap((Bitmap)source.Clone());
                    //lockbit.LockBits();

                    ////676,674 => 30*20
                    //for (int i = 676; i < 706; i++)
                    //{
                    //    for (int j = 674; j < 694; j++)
                    //    {
                    //        Color cr = bmp.GetPixel(i, j);
                    //        Color crr = lockbit.GetPixel(i, j);

                    //        if (cr != crr)
                    //        {
                    //            Console.WriteLine("i = " + i + " , j = " + j + " :: " + cr + " , " + crr);
                    //        }
                    //    }
                    //}

                    //lockbit.UnlockBits();
                    //break;
            }
        }

        private void gHook_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {

            }
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

        private Point SearchImage(Image SearchImg , int x , int y)
        {
            ///Point pt = Search.SearchBitmap((Bitmap)pic, (Bitmap)src); Memory Locked.
            Image pic = CaptureScreen.CapturehWndWindow(AssistantHwnd);

            Point pt = Search.SearchLockBitmap((Bitmap)pic.Clone(), (Bitmap)SearchImg.Clone(), x, y);
            Point bpt = Search.SearchPixelBitmap((Bitmap)pic, (Bitmap)SearchImg, x, y);

            Console.WriteLine("Recieve Point : " + x + " , " + y + " . Result : " + pt.X + " , " + pt.Y);
            Console.WriteLine("Recieve Bitmap Point : " + x + " , " + y + " . Result : " + bpt.X + " , " + bpt.Y);
            Console.WriteLine();
            
            pic.Dispose();
            return pt;
        }
        
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

            IntPtr ptr = BlueStackHwnd;
            for (int i = 0; i < 3; i++)
            {
                Message.ToPostMessage(ptr, (int)KeyBoardEventsFlag.WM_KEYDOWN, KeyValue, 0);
                Message.ToPostMessage(ptr, (int)KeyBoardEventsFlag.WM_KEYUP, KeyValue, 0);
                ptr = BasicWindow.ToFindWindowEx(ptr, IntPtr.Zero, null, null);
            }

            //int KeyValue = (int)Keys.A + KeyOptions;
            //foreach (IntPtr p in AssistantWindowHwndList)
            //{
            //    Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYDOWN, KeyValue, 0);
            //    Message.ToPostMessage(p, (int)KeyBoardEventsFlag.WM_KEYUP, KeyValue, 0);
            //}
            Console.WriteLine("SendClick " + (Char)KeyValue + " pressed.");
        }

        private void SimulateClickTimer_Tick(object sender, EventArgs e)
        {
            Image Source = CaptureScreen.CapturehWndWindow(AssistantHwnd);
            Bitmap src = (Bitmap)Source.Clone(), compare = (Bitmap)Clip[Index].Clone();
            Point Result = Search.SearchPixelBitmap(src, compare, ClipSearchPoints[Index].X, ClipSearchPoints[Index].Y);
            //Point Result = Search.SearchLockBitmap((Bitmap)Source.Clone(), (Bitmap)Clip[Index].Clone(), ClipSearchPoints[Index].X, ClipSearchPoints[Index].Y);            
            // Memory problem.

            if (Result.X == -1 && Result.Y == -1)
                this.Text = "AutoClick Searching Failure , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";
            else
            {
                this.Text = "AutoClick Searching Success , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";

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

                        Index = 4;
                        break;
                    case 1:
                    case 2:
                    case 3:
                        Index = 4;
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
            IntPtr ptr = BasicWindow.ToFindWindow("BlueStacks");
            if (ptr != IntPtr.Zero)
                BStatus.Text = "BlueStacks is Running ... ";
            else
                BStatus.Text = "BlueStacks is Closed.";
        }

        private void AutoClick_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AssistantHwnd != IntPtr.Zero)
            {
                Process[] process = Process.GetProcessesByName("Bluestacks");

                process[0].Kill();

                process = Process.GetProcessesByName("HD-Agent");

                process[0].Kill();

                process = Process.GetProcessesByName("HD-Player");

                process[0].Kill();

                process = Process.GetProcessesByName("BstkSVC");

                process[0].Kill();

                //this.Close();
            }
        }
    }
}
