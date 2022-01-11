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
        WindowsAPI wapi = new WindowsAPI(); 
        Messages msg_api = new Messages();

        ScreenCapture CaptureScreen = new ScreenCapture();
        SearchImage Search = new SearchImage();

        int Index = 0;
        bool IsAssistantWorking = false;
        string SetupFilePath = System.IO.Directory.GetCurrentDirectory() + "\\ImgSetup.json";

        IntPtr AssistantHwnd = IntPtr.Zero;

        GlobalKeyboardHook hk;

        List<Script_data> data = new List<Script_data>();
        List<IntPtr> AssistantWindowHwndList = new List<IntPtr>();

        IntPtr BlueStackHwnd = IntPtr.Zero, target = IntPtr.Zero;
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

        public AssistantInterface()
        {
            InitializeComponent();
            init_function();
        }

        private void init_function()
        {
            BlueStackHwnd = wapi.ToFindWindow("BlueStacks");
            target = wapi.ToFindWindowEx(BlueStackHwnd, IntPtr.Zero, null, null); 
            //IntPtr target = window.ToFindWindow("BlueStacks");
            //List<IntPtr> target_list = EnumWindowsList(target);
            //AssistantWindowHwndList = target_list;

            hk = new GlobalKeyboardHook();
            hk.KeyDown += new KeyEventHandler(gHook_KeyDown);
            hk.KeyUp += new KeyEventHandler(gHook_KeyUp);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                hk.HookedKeys.Add(key);

            hk.hook();
            //Full Area keyboard hook
        }

        #region Key Events

        private void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    SimulateClickTimer.Start();
                    label1.Text = "AutoClick Start.";
                    log_light.Start();
                    break;
                case Keys.F2:
                    SimulateClickTimer.Stop();
                    label1.Text = "AutoClick Stop.";
                    log_light.Start();
                    break;
                case Keys.F3:
                    Point ptr = SearchImage(data[Index].img, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);
                    label1.Text = "Search Result : x :  " + ptr.X + " , y : " + ptr.Y;
                    log_light.Start();
                    break;
                case Keys.F4:
                    Image Picture = CaptureScreen.CapturehWndWindow(AssistantHwnd);
                    Picture.Save(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_Image.png");
                    Console.WriteLine("Image has been saved at : " + Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_Image.png");
                    label1.Text = "Image has been saved as : " + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss").ToString() + "_Image.png";
                    log_light.Start();
                    break;
                case Keys.F5:
                    Index = 0;
                    label1.Text = "Index = " + Index.ToString();
                    log_light.Start();
                    break;
                case Keys.F6:
                    Index++;
                    label1.Text = "Add index , Index = " + Index.ToString();
                    log_light.Start();
                    break;
                case Keys.F7:
                    label1.Text = "Index = " + Index.ToString() + " , Send Click Events. ";
                    SendClickEvents(Index);
                    log_light.Start();
                    break;
                case Keys.F8:
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
            log_light.Start();
            Image Source = CaptureScreen.CapturehWndWindow(AssistantHwnd);
            //Bitmap src = (Bitmap)Source.Clone();
            Bitmap compare = (Bitmap)data[Index].img.Clone();
            //Point BitResult = Search.SearchPixelBitmap(src, compare, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);
            Point Result = Search.SearchLockBitmap((Bitmap)Source.Clone(), compare, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);

            if (Result.X == -1 && Result.Y == -1)
            {
                label2.Text = "faliure , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";
            }
            else
            {
                Thread.Sleep(300);
                SendClickEvents(Index);
                switch (Index)
                {
                    case 0:
                        Thread.Sleep(350);
                        SendClickEvents(Index);
                        Thread.Sleep(350);
                        SendClickEvents(Index);
                        
                        Thread.Sleep(1000);

                        SendClickEvents(1);
                        Thread.Sleep(500);
                        SendClickEvents(2);
                        Thread.Sleep(500);
                        SendClickEvents(2);
                        Index = 4;
                        break;
                    case 1:
                    case 2:
                    case 3:
                        //Thread.Sleep(150);
                        //Index++;
                        break;
                    case 4:
                        Index = 0;
                        break;
                }

                Thread.Sleep(100);
                label2.Text = "Success , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";
            }

            //if (Result.X == -1 && Result.Y == -1)
            //{
            //    this.Text = "AutoClick Searching Failure , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";

            //    if (data[Index].IsForced)
            //    {
            //        SendClickEvents(Index);
            //        Index = (data.Count - 1 == Index) ? 0 : Index + 1;
            //    }
            //}
            //else
            //{
            //    this.Text = "AutoClick Searching Success , Index = " + Index.ToString() + " : Point = (" + Result.X + " , " + Result.Y + ")";

            //    Thread.Sleep(100);
            //    SendClickEvents(Index);
            //    Index = (data.Count - 1 == Index) ? 0 : Index + 1;

            //}

            Source.Dispose();
            //src.Dispose();
            compare.Dispose();
        }

        private void BSRunningTimer_Tick(object sender, EventArgs e)
        {
            IntPtr ptr = wapi.ToFindWindow("BlueStacks");
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
                IntPtr TargetHwnd = wapi.ToFindWindow("BlueStacks");
                BlueStackHwnd = TargetHwnd;

                wapi.ToShowWindowAsync(TargetHwnd, 3);
                Thread.Sleep(500);
                WindowsAPI.RECT rect = wapi.ToGetWindowRect(TargetHwnd);

                if (TargetHwnd != IntPtr.Zero)
                {
                    AssistantWindowHwndList = EnumWindowsList(TargetHwnd);

                    AStatus.Text = "Assistant is Working ...";
                    SummonerAssistant Assistant = new SummonerAssistant(TargetHwnd, rect);
                    AssistantHwnd = Assistant.Handle;
                    Assistant.Show();

                    wapi.ToShowWindowAsync(TargetHwnd, 3);
                    // reload image list and SearchPoints
                    //Reload_Imagedata();
                    data = new List<Script_data>();
                    data.Add(new Script_data()
                    {
                        img = Image.FromFile("D:\\Side Projects\\SummnorWar_CSharp\\SummonersWar\\bin\\Debug\\Clip\\victory_mark.png"),
                        delaytime = 500,
                        IsForced = true,
                        SearchPoints = new Point(862, 531)
                    });

                    data.Add(new Script_data()
                    {
                        img = Image.FromFile("D:\\Side Projects\\SummnorWar_CSharp\\SummonersWar\\bin\\Debug\\Clip\\Sell_mark.png"),
                        delaytime = 500,
                        IsForced = true,
                        SearchPoints = new Point(693, 808)
                    });

                    data.Add(new Script_data()
                    {
                        img = Image.FromFile("D:\\Side Projects\\SummnorWar_CSharp\\SummonersWar\\bin\\Debug\\Clip\\mys_confirm.png"),
                        delaytime = 500,
                        IsForced = true,
                        SearchPoints = new Point(828, 794)
                    });

                    data.Add(new Script_data()
                    {
                        img = Image.FromFile("D:\\Side Projects\\SummnorWar_CSharp\\SummonersWar\\bin\\Debug\\Clip\\mon_confirm.png"),
                        delaytime = 500,
                        IsForced = true,
                        SearchPoints = new Point(828, 844)
                    });

                    data.Add(new Script_data()
                    {
                        img = Image.FromFile("D:\\Side Projects\\SummnorWar_CSharp\\SummonersWar\\bin\\Debug\\Clip\\again_mark.png"),
                        delaytime = 500,
                        IsForced = true,
                        SearchPoints = new Point(1190, 818)
                    });

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

        private void captureScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image Picture = CaptureScreen.CapturehWndWindow(AssistantHwnd);
            Picture.Save(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_Image.png");
            Console.WriteLine("Image has been saved as : " + Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_Image.png");
            label1.Text = "Image has been saved as : " + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss").ToString() + "_Image.png";
            label1.Visible = true;
            log_light.Start();
        }

        private void scriptStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimulateClickTimer.Start();
            label1.Text = "AutoClick Start.";
            label1.Visible = true;
            log_light.Start();
            label2.Visible = true;
        }

        private void scriptStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimulateClickTimer.Stop();
            label1.Text = "AutoClick Stop.";
            label1.Visible = true;
            log_light.Start();
            label2.Visible = false;
        }

        private void index0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Index = 0;
            label1.Text = "Index = " + Index.ToString();
            label1.Visible = true;
            log_light.Start();
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Index += 1;
            label1.Text = "Index = " + Index.ToString();
            label1.Visible = true;
            log_light.Start();
        }

        private void searchImgindexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point ptr = SearchImage(data[Index].img, data[Index].SearchPoints.X, data[Index].SearchPoints.Y);
            label1.Text = "Search Result : x :  " + ptr.X + " , y : " + ptr.Y;
            label1.Visible = true;
            log_light.Start();
        }

        private void clickIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "Index = " + Index.ToString() + " , Send Click Events. ";
            SendClickEvents(Index);
            label1.Visible = true;
            log_light.Start();
        }

        private void log_light_Tick(object sender, EventArgs e)
        {
            label1.Visible = false;           
            log_light.Stop();
        }

        private void monitorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                ptr = wapi.ToFindWindowEx(ptr, IntPtr.Zero, null, null);
            }

            return list;
        }

        private Point SearchImage(Image SearchImg, int x, int y)
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

            msg_api.ToPostMessage(target, (int)KeyBoardEventsFlag.WM_KEYDOWN, KeyValue, 0);
            Thread.Sleep(10);
            msg_api.ToPostMessage(target, (int)KeyBoardEventsFlag.WM_KEYUP, KeyValue, 0);
            Thread.Sleep(10);

            msg_api.ToPostMessage(target, (int)KeyBoardEventsFlag.WM_KEYDOWN, KeyValue, 0);
            Thread.Sleep(10);
            msg_api.ToPostMessage(target, (int)KeyBoardEventsFlag.WM_KEYUP, KeyValue, 0);
            Thread.Sleep(10);
        }

        private void AutoClick_FormClosing(object sender, FormClosingEventArgs e)
        {

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
                            data.Add(new Script_data()
                            {
                                img = Image.FromFile(list[i].path),
                                delaytime = list[i].delaytime,
                                IsForced = list[i].IsForceClick,
                                SearchPoints = new Point(-1, -1)
                            });
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
