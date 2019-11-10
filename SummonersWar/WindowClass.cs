using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class WindowClass
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

    private static List<string> HwndNameList = new List<string>();
    private static List<string> DexCodeList = new List<string>();
    private static List<IntPtr> HwndPtrList = new List<IntPtr>();

    [DllImport("user32.dll")]
    static extern IntPtr EnumWindows(CallbackDef callback, int lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

    //public delegate bool CallBack(IntPtr hwnd, int lParam);
    delegate bool CallbackDef(IntPtr hWnd, int lParam);
    
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

    public int ToFocusWindow(IntPtr hwnd)
    {
        return SetForegroundWindow(hwnd);
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

    public IntPtr ToFindWindow(string WindowName)
    {
        return FindWindow(null, WindowName);
    }

    public IntPtr ToFindWindowEx(IntPtr ParentHwnd , IntPtr Childhwnd , string lpszClassStr , string lpszWindowStr)
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

    public int ToMoveWindow(IntPtr hwnd , int x , int y , int wid , int hei)
    {
        return MoveWindow(hwnd, x, y, wid, hei, true);
    }
    //set parent window
}

