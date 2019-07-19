using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SummonersWar
{
    public partial class AutoClick : Form
    {
        bool IsAssistantWorking = false;
        WindowClass win = new WindowClass();
        ScreenCapture cap = new ScreenCapture();
        IntPtr AssistantHwnd;

        public AutoClick()
        {
            InitializeComponent();
            CheckBSRunningTimer.Start();
            //this.StartPosition = FormStartPosition.Manual
            //this.Location = new Point(235, 235);
        }

        private void listHwndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hWndList hwnd = new hWndList();
            hwnd.Show();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Reset();
            sw.Start();
            Image img = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ms.jpg");
            Bitmap s = (Bitmap)img;
            for (int i = 0; i < s.Width; i++)
            {
                for (int j = 0; j < s.Height; j++)
                {
                    Color pixel = s.GetPixel(i, j);
                    Console.WriteLine("i = " + i.ToString() + " , j = " + j.ToString() + " , " + pixel.ToArgb()
                        + " , A : " + pixel.A + " R : " + pixel.R + " G : " + pixel.G + " B : " + pixel.B);
                }
            }

            Console.WriteLine("Img Size : " + s.Width + " * " + s.Height + "( wid * hei )");
            sw.Stop();
            Console.WriteLine("done. Time spend : " + sw.Elapsed.TotalMilliseconds.ToString() + "ms .");
        }

        private void summonerWarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsAssistantWorking)
            {

                //IntPtr TargetHwnd = winClass.ToFindWindow("BlueStacks");
                IntPtr TargetHwnd = win.ToFindWindow("未命名 - 記事本");
                WindowClass.RECT rect = win.ToGetWindowRect(TargetHwnd);

                AStatus.Text = "Assistant is Running ...";

                SummonerAssistant ast = new SummonerAssistant(TargetHwnd, rect);
                AssistantHwnd = ast.Handle;
                ast.Show();

                CompareImageTimer.Start();
            }
        }

        private void CheckBSRunningTimer_Tick(object sender, EventArgs e)
        {
            IntPtr ptr = win.ToFindWindow("BlueStacks");
            if (ptr != IntPtr.Zero)
                BStatus.Text = "BlueStacks is Running ... ";
            else
                BStatus.Text = "BlueStacks is Closed.";
        }

        private void CompareImageTimer_Tick(object sender, EventArgs e)
        {            
            Image s = cap.CapturehWndWindow(AssistantHwnd);
            s.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_TestImage.png");
            //Set Timer , CaptureScreen , ToSearch pixel
        }
    }
}
