using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public class MessageClass
{
    
    [DllImport("user32.dll")]
    public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    public extern static void SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    public extern static bool GetCursorPos(out Point p);
    //mouse
    [DllImport("User32.dll", EntryPoint = "SendMessage")]
    private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("User32.dll", EntryPoint = "PostMessage")]
    private static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    //message
    [DllImport("user32.dll", EntryPoint = "SetParent")]
    private static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
    private static extern void SetForegroundWindow(int hwnd);
    
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

    public int ToSendMessage(IntPtr Hwnd , int Msg , int wparam , int lparam)
    {
        return SendMessage(Hwnd, Msg, wparam, lparam);
    }

    public int ToPostMessage(IntPtr Hwnd, int Msg, int wparam, int lparam)
    {
        return PostMessage(Hwnd, Msg, wparam, lparam);
    }

    public void Calling()
    {
        FindWindowClass fw = new FindWindowClass();
        IntPtr ptr = fw.ToFindWindow("未命名 - 記事本");
        IntPtr ch1 = fw.ToFindWindowEx(ptr, IntPtr.Zero, null, null);
        IntPtr ch2 = fw.ToFindWindowEx(ch1, IntPtr.Zero, null, null);

        if (ptr != IntPtr.Zero)
        {
            Console.WriteLine("ptr = " + Convert.ToString((int)ptr, 16).ToUpper());
            Console.WriteLine("ch1 = " + Convert.ToString((int)ch1, 16).ToUpper());
            Console.WriteLine("ch2 = " + Convert.ToString((int)ch2, 16).ToUpper());

            PostMessage(ptr, 256, (int)Keys.C, 0);
            PostMessage(ch1, 256, (int)Keys.C, 0);

        }
        else
            Console.WriteLine("Window not found . ");
    }
}

