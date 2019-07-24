﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
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

            test();
            //Image img = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Source.png");
            //Bitmap s = (Bitmap)img;

            //for (int i = 0; i < s.Width; i++)
            //{
            //    for (int j = 0; j < s.Height; j++)
            //    {
            //        Color pixel = s.GetPixel(i, j);
            //        //Console.WriteLine("i = " + i.ToString() + " , j = " + j.ToString() + " , " + pixel.ToArgb()
            //        //    + " , A : " + pixel.A + " R : " + pixel.R + " G : " + pixel.G + " B : " + pixel.B);
            //    }
            //}

            //Console.WriteLine("Img Size : " + s.Width + " * " + s.Height + " ( wid * hei )");
            //sw.Stop();
            //Console.WriteLine("done. Time spend : " + sw.Elapsed.TotalMilliseconds.ToString() + "ms .");
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

        private void test()
        {
            Bitmap bmpGray = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Source.png");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < bmpGray.Height; i++)
            {
                for (int j = 0; j < bmpGray.Width; j++)
                {
                    Color color = bmpGray.GetPixel(j, i);
                    //int aver = (color.R + color.G + color.B) / 3;
                    int aver = (color.R * 38 + color.G * 75 + color.B * 15) >> 7;
                    bmpGray.SetPixel(j, i, Color.FromArgb(aver, aver, aver));
                }
            }
            bmpGray.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\WhatDoesImageDo.png");
            stopwatch.Stop();

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();

            bmpGray = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Source.png");
            Rectangle rect = new Rectangle(1, 1, bmpGray.Width - 2, bmpGray.Height - 2);
            BitmapData bmpData = bmpGray.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr ptr = bmpData.Scan0;
            for (int i = 0; i < rect.Height; i++)
            {
                for (int j = 0; j < rect.Width; j++)
                {
                    int aver = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        aver += Marshal.ReadByte(ptr, i * bmpData.Stride + j * 3 + k);
                        //Marshal.ReadByte(ptr, i * bmpData.Stride + j * 3 + k); = pixel

                    }
                    aver /= 3;
                    for (int k = 0; k < 3; k++)
                    {
                        Marshal.WriteByte(ptr, i * bmpData.Stride + j * 3 + k, (byte)aver);
                    }
                }
            }
            bmpGray.UnlockBits(bmpData);
            bmpGray.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\WhatDoesImageDo2.png");
            stopwatch2.Stop();

            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            bmpGray = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Source.png");

            //Rectangle rect2 = new Rectangle(1, 1, bmpGray.Width - 2, bmpGray.Height - 2);
            Rectangle rect2 = new Rectangle(0, 0, bmpGray.Width - 1 , bmpGray.Height - 1);
            BitmapData bmpData2 = bmpGray.LockBits(rect2, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr ptr2 = bmpData2.Scan0;
            byte[] rgbValues = new byte[rect2.Height * rect2.Width * 3];
            Marshal.Copy(ptr2, rgbValues, 0, rgbValues.Length);
            for (int i = 0; i < rgbValues.Length; i += 3)
            {
                int colorTemp = rgbValues[i] + rgbValues[i + 1] + rgbValues[i + 2];
                colorTemp /= 3;
                rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = (byte)colorTemp;
            }
            Marshal.Copy(rgbValues, 0, ptr2, rgbValues.Length);
            bmpGray.UnlockBits(bmpData);
            bmpGray.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\WhatDoesImageDo3.png");
            stopwatch3.Stop();

            Stopwatch stopwatch4 = new Stopwatch();
            stopwatch4.Start();

            bmpGray = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Source.png");
            Rectangle rect4 = new Rectangle(1, 1, bmpGray.Width - 2, bmpGray.Height - 2);
            BitmapData bmpData4 = bmpGray.LockBits(rect4, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr4 = (byte*)(bmpData4.Scan0);
                for (int i = 0; i < rect4.Height; i++)
                {
                    for (int j = 0; j < rect4.Width; j++)
                    {
                        int aver = 0;
                        for (int k = 0; k < 3; k++)
                        {
                            aver += ptr4[i * bmpData4.Stride + j * 3 + k];
                        }
                        aver /= 3;
                        for (int k = 0; k < 3; k++)
                        {
                            ptr4[i * bmpData4.Stride + j * 3 + k] = (byte)aver;
                        }
                    }
                }
            }
            bmpGray.UnlockBits(bmpData4);
            bmpGray.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\WhatDoesImageDo4.png");
            stopwatch4.Stop();

            Console.WriteLine("StopWatch : " + stopwatch.Elapsed.TotalMilliseconds.ToString() + "ms .");
            Console.WriteLine("StopWatch2 : " + stopwatch2.Elapsed.TotalMilliseconds.ToString() + "ms .");
            Console.WriteLine("StopWatch3 : " + stopwatch3.Elapsed.TotalMilliseconds.ToString() + "ms .");
            Console.WriteLine("StopWatch4 : " + stopwatch4.Elapsed.TotalMilliseconds.ToString() + "ms .");
        }
    }
}
