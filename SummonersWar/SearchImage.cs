using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SearchImage 
{
    public Point SearchBitmap(Bitmap ParentBitmap, Bitmap ChildBitmap, int ImageLocationX = -1, int ImageLocationY = -1)
    {
        Point pt = new Point(-1, -1);

        LockBitmap ParentMap = new LockBitmap(ParentBitmap);
        LockBitmap ChildMap = new LockBitmap(ChildBitmap);

        ParentMap.LockBits();
        ChildMap.LockBits();

        if (ImageLocationX == -1 && ImageLocationY == -1)
        {
            bool EndSearch = false;
            for (int i = 0; i < ParentMap.Width; i++)
            {
                for (int j = 0; j < ParentMap.Height; j++)
                {
                    if (i + ChildMap.Width <= ParentMap.Width && j + ChildMap.Height <= ParentMap.Height)
                    {
                        bool IsMatch = true;
                        for (int i2 = 0; i2 < ChildMap.Width; i2++)
                        {
                            for (int j2 = 0; j2 < ChildMap.Height; j2++)
                            {
                                if (ParentMap.GetPixel(i + i2, j + j2) != ChildMap.GetPixel(i2, j2))
                                {
                                    IsMatch = false;
                                    break;
                                }
                            }
                            if (!IsMatch)
                                break;
                        }

                        if (IsMatch)
                        {
                            EndSearch = true;
                            pt = new Point(i, j);
                            break;
                            //return new Point(i, j);
                        }
                    }
                }

                if (EndSearch)
                    break;
            }
        }
        else
        {
            if (ImageLocationX + ChildBitmap.Width <= ParentBitmap.Width && ImageLocationY + ChildBitmap.Height <= ParentBitmap.Height)
            {
                bool IsMatch = true;
                for (int i = 0; i < ChildBitmap.Width; i++)
                {
                    for (int j = 0; j < ChildBitmap.Height; j++)
                    {
                        if (ParentMap.GetPixel(ImageLocationX + i, ImageLocationY + j) != ChildMap.GetPixel(i, j))
                        {
                            IsMatch = false;
                            break;
                        }
                    }

                    if (!IsMatch)
                        break;
                }

                if (IsMatch)
                {
                    return new Point(ImageLocationX, ImageLocationY);
                }
            }

        }

        ParentMap.UnlockBits();
        ChildMap.UnlockBits();

        return pt;
    }
}

