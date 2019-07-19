using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public class MessageHandlingClass
{
    [DllImport("User32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("User32.dll")]
    private static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    //message
    public int ToSendMessage(IntPtr Hwnd, int Msg, int wparam, int lparam)
    {
        return SendMessage(Hwnd, Msg, wparam, lparam);
    }

    public int ToPostMessage(IntPtr Hwnd, int Msg, int wparam, int lparam)
    {
        return PostMessage(Hwnd, Msg, wparam, lparam);
    }

}
