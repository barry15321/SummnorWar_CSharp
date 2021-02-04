using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class WindowsAPI
{
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public struct POINT
    {
        public int x;
        public int y;
    }

    public static int nCount;
    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    //public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
    public static extern IntPtr FindWindowExA(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("user32.dll")]
    //private static extern int GetWindowRect(IntPtr hwnd, out lpRect);
    public static extern Boolean GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    internal static extern Boolean GetClientRect(IntPtr hwnd, ref RECT lpRect);

    [DllImport("user32.dll")]
    //static extern int GetWindowText(IntPtr hwnd, string lpString, int cch);
    static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int StringMaximun);

    [DllImport("user32.dll")]
    static extern int GetWindowTextLength(IntPtr hwnd); // equals GetWindowText return int

    [DllImport("user32.dll")]
    private static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll")]
    static extern int SetForegroundWindow(IntPtr hwnd);


    [DllImport("user32.dll")]
    static extern IntPtr EnumWindows(CallbackDef callback, int lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

    //public delegate bool CallBack(IntPtr hwnd, int lParam);
    delegate bool CallbackDef(IntPtr hWnd, int lParam);

    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    public int ToFocusWindow(IntPtr hwnd)
    {
        return SetForegroundWindow(hwnd);
    }

    public IntPtr ToFindWindow(string WindowName)
    {
        return FindWindow(null, WindowName);
    }

    public IntPtr ToFindWindowEx(IntPtr ParentHwnd, IntPtr Childhwnd, string lpszClassStr, string lpszWindowStr)
    {
        return FindWindowExA(ParentHwnd, Childhwnd, lpszClassStr, lpszWindowStr);
    }

    public string ToGetWindowTitleText(IntPtr hwnd)
    {
        StringBuilder txt = new StringBuilder(255);
        GetWindowText(hwnd, txt, 255); //int = Length
        return txt.ToString();
    }

    public RECT ToGetWindowRect(IntPtr hwnd)
    {
        RECT WindowRect = new RECT();
        GetWindowRect(hwnd, out WindowRect);
        return WindowRect;
    }

    public int ToSetParentWindow(IntPtr Child, IntPtr Parent)
    {
        return SetParent(Child, Parent);
    }

    public int ToMoveWindow(IntPtr hwnd, int x, int y, int wid, int hei)
    {
        return MoveWindow(hwnd, x, y, wid, hei, true);
    }
    //set parent window

    public bool ToShowWindowAsync(IntPtr hwnd, int nCmdShow)
    {
        return ShowWindowAsync(hwnd, nCmdShow);
    }

    //
    private static List<string> HwndNameList = new List<string>();
    private static List<string> DexCodeList = new List<string>();
    private static List<IntPtr> HwndPtrList = new List<IntPtr>();

    public List<string> ToGetHwndNameList()
    {
        return HwndNameList;
    }

    public List<string> ToGetDexCodeList()
    {
        return DexCodeList;
    }

    public List<IntPtr> ToGetHwndList()
    {
        return HwndPtrList;
    }

    public void GetEnums()
    {
        HwndPtrList.Clear();
        HwndNameList.Clear();
        DexCodeList.Clear();

        CallbackDef callback = new CallbackDef(EnumWindowFunction);
        EnumWindows(callback, 0);
    }

    private static bool EnumWindowFunction(IntPtr h, int lParam)
    {
        string mystring;

        StringBuilder text = new StringBuilder(255);
        GetWindowText(h, text, 255);
        mystring = text.ToString();

        HwndNameList.Add(text.ToString());
        DexCodeList.Add(Convert.ToString((int)h, 16).ToUpper());
        HwndPtrList.Add(h);
        //s.Add();  List<string> => only static , because CallBackFunction doesnt belong to FindWindowClass
        return true;
    }
    //
}

public class Messages
{
    [DllImport("User32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [DllImport("User32.dll")]
    private static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    public int ToSendMessage(IntPtr Hwnd, int Msg, int wparam, int lparam)
    {
        return SendMessage(Hwnd, Msg, wparam, lparam);
    }

    public int ToPostMessage(IntPtr Hwnd, int Msg, int wparam, int lparam)
    {
        return PostMessage(Hwnd, Msg, wparam, lparam);
    }

}

public class Mouse
{
    [DllImport("user32.dll")]
    public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    public extern static void SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    public extern static bool GetCursorPos(out Point p);
    //mouse

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

    public void MouseMove(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public void MouseClick()
    {
        mouse_event((int)(MouseEventsFlag.LEFTDOWN), 0, 0, 0, IntPtr.Zero);
        mouse_event((int)(MouseEventsFlag.LEFTUP), 0, 0, 0, IntPtr.Zero);
    }

    public Point ToGetCursorPosistion()
    {
        Point Cursor = new Point();
        GetCursorPos(out Cursor);
        return Cursor;
    }

    public void Calling()
    {
        
        WindowsAPI fw = new WindowsAPI();
        Messages ms = new Messages();
        IntPtr ptr = fw.ToFindWindow("未命名 - 記事本");
        IntPtr ch1 = fw.ToFindWindowEx(ptr, IntPtr.Zero, null, null);

        if (ptr != IntPtr.Zero)
        {
            Console.WriteLine("ptr = " + Convert.ToString((int)ptr, 16).ToUpper());
            Console.WriteLine("ch1 = " + Convert.ToString((int)ch1, 16).ToUpper());

            ms.ToPostMessage(ch1, (int)KeyBoardEventsFlag.WM_CHAR, (int)Keys.A, 0);
            Console.WriteLine("try to press A");
        }
        else
            Console.WriteLine("Window not found . ");

    }
}
