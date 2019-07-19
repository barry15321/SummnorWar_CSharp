using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class ScreenCapture
{
    public Image CaptureAllScreen()
    {
        return (Image)CopyGraphicsAllScreen();
        //return CaptureWindow(User32.GetDesktopWindow());
    }

    private Bitmap CopyGraphicsAllScreen()
    {
        int screenLeft = SystemInformation.VirtualScreen.Left;
        int screenTop = SystemInformation.VirtualScreen.Top;
        int screenWidth = SystemInformation.VirtualScreen.Width;
        int screenHeight = SystemInformation.VirtualScreen.Height;

        // Create a bitmap of the appropriate size to receive the screenshot.
        Bitmap AllSrc = null;
        Bitmap bmp = new Bitmap(screenWidth, screenHeight);
        Graphics g = Graphics.FromImage(bmp);
        g.CopyFromScreen(screenLeft, screenTop, 0, 0, bmp.Size);

        //bmp.Save(@"C:\Users\ReplacedToy\Desktop\" + "SaveAllScreen.jpg", ImageFormat.Jpeg);
        AllSrc = bmp;
        return AllSrc;
    }

    /// <summary>
    /// Creates an Image object containing a screen shot of a specific window
    /// </summary>
    /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
    /// <returns></returns>
    public Image CaptureWindow(IntPtr handle)
    {
        int TotalWidth = 0, TotalHeight = 0;
        for (int i = 0; i < Screen.AllScreens.Length; i++)
        {
            Screen src = Screen.AllScreens[i];
            TotalWidth += src.Bounds.Width;
            TotalHeight += src.Bounds.Height;
        }

        IntPtr hdcSrc = User32.GetWindowDC(handle);
        User32.RECT windowRect = new User32.RECT();
        User32.GetWindowRect(handle, ref windowRect);
        int width = TotalWidth - Screen.PrimaryScreen.Bounds.Left;
        int height = TotalHeight - Screen.PrimaryScreen.Bounds.Top;
        //int width = windowRect.right - windowRect.left;
        //int height = windowRect.bottom - windowRect.top;
        // create a device context we can copy to
        IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
        // create a bitmap we can copy it to,
        // using GetDeviceCaps to get the width/height
        IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
        // select the bitmap object
        IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
        // bitblt over
        GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
        // restore selection
        GDI32.SelectObject(hdcDest, hOld);
        // clean up 
        GDI32.DeleteDC(hdcDest);
        User32.ReleaseDC(handle, hdcSrc);

        // get a .NET image object for it
        Image img = Image.FromHbitmap(hBitmap);
        // free up the Bitmap object
        GDI32.DeleteObject(hBitmap);

        return img;
    }

    public Image CapturehWndWindow(IntPtr hWnd)
    {
        //https://blog.csdn.net/spiderlily/article/details/8548470
        // User32.dll PrintWindow
        IntPtr hscrdc = User32.GetWindowDC(hWnd);
        User32.RECT windowRect = new User32.RECT();
        User32.GetWindowRect(hWnd, ref windowRect);
        int width = windowRect.right - windowRect.left;
        int height = windowRect.bottom - windowRect.top;
        IntPtr hbitmap = GDI32.CreateCompatibleBitmap(hscrdc, width, height);
        IntPtr hmemdc = GDI32.CreateCompatibleDC(hscrdc);
        GDI32.SelectObject(hmemdc, hbitmap);
        bool re = User32.PrintWindow(hWnd, hmemdc, 0);
        Bitmap bmp = null;
        if (re)
        {
            bmp = Bitmap.FromHbitmap(hbitmap);
        }
        GDI32.DeleteObject(hbitmap);
        GDI32.DeleteDC(hmemdc);
        User32.ReleaseDC(hWnd, hscrdc);
        return bmp;
    }

    //BitBlt , CreateDC , CreateCompatibleBitmap , CreateCompatibleDC , DeleteDC , DeleteObject , SelectObject
    private class GDI32
    {
        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>

        public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(
           string lpszDriver, // driver name
           string lpszDevice, // device name
           string lpszOutput, // not used; should be NULL
           IntPtr lpInitData // optional printer data
        );

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hObjectSource,
            int nXSrc, int nYSrc, int dwRop);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    }

    //PrintWindow , GetDesktopWindow , GetWindowDC , ReleaseDC , GetWindowRect
    private class User32
    {
        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        /// 
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
            IntPtr hwnd, // Window to copy,Handle to the window that will be copied. 
            IntPtr hdcBlt, // HDC to print into,Handle to the device context. 
            UInt32 nFlags // Optional flags,Specifies the drawing options. It can be one of the following values. 
        );

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

    }

    public Point SearchingImagePixel(Image ParentImage, Image ChildImage, int StartSearchingX = -1, int StartSearchingY = -1)
    { 
        Point flag = new Point(-1, -1);

        return flag;
    }
}
