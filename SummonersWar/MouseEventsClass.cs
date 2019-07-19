using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public class MouseEventsClass
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

    public void MouseMove(int x , int y)
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
        WindowClass fw = new WindowClass();
        MessageHandlingClass ms = new MessageHandlingClass();
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

