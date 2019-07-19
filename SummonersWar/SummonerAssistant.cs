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
    public partial class SummonerAssistant : Form
    {
        IntPtr TargetHwnd;
        WindowClass.RECT TargetRect;

        public SummonerAssistant(IntPtr hWnd , WindowClass.RECT hWndRect)
        {            
            InitializeComponent();

            TargetHwnd = hWnd;
            TargetRect = hWndRect;
            InitialFunction();
        }

        private void InitialFunction()
        {
            WindowClass win = new WindowClass();

            Console.WriteLine("TargetHwnd : " + TargetHwnd.ToString());
            Console.WriteLine("TargetRect : " + TargetRect.Left + " , " + TargetRect.Right + " , " + TargetRect.Top + " , " + TargetRect.Bottom);
            
            this.Size = new Size((TargetRect.Right - TargetRect.Left + 16), (TargetRect.Bottom - TargetRect.Top + 38));
            win.ToMoveWindow(TargetHwnd, 0, 0, TargetRect.Right - TargetRect.Left, TargetRect.Bottom - TargetRect.Top);

            win.ToSetParentWindow(TargetHwnd, this.pictureBox1.Handle);
        }

        private void CaptureWindowScreen()
        {
            ScreenCapture cap = new ScreenCapture();

            //Image img = cap.CaptureWindow(this.Handle);
            //img.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Testfile.png");
        }
    }
}
