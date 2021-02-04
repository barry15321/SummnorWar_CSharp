using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SearchImage 
{
    public Point SearchLockBitmap(Bitmap ParentBitmap, Bitmap ChildBitmap, int ImageLocationX = -1, int ImageLocationY = -1)
    {
        Point pt = new Point(-1, -1);

        LockBitmap ParentMap = new LockBitmap(ParentBitmap);
        LockBitmap ChildMap = new LockBitmap(ChildBitmap);

        ParentMap.LockBits();
        ChildMap.LockBits();

        //List<Color> misspixel = new List<Color>();
        
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
                                Color left = ParentMap.GetPixel(i + i2, j + j2), right = ChildMap.GetPixel(i2, j2);
                                bool Judgement = (left.R == right.R) && (left.G == right.G) && (left.B == right.B);
                                if (!Judgement)
                                //if (ParentMap.GetPixel(i + i2, j + j2) != ChildMap.GetPixel(i2, j2))
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
                        Color left = ParentMap.GetPixel(ImageLocationX + i, ImageLocationY + j), right = ChildMap.GetPixel(i, j);
                        bool Judgement = (left.R == right.R) && (left.G == right.G) && (left.B == right.B);
                        if (!Judgement)
                        //if (ParentMap.GetPixel(ImageLocationX + i, ImageLocationY + j) != ChildMap.GetPixel(i, j))
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
        ParentBitmap.Dispose();
        ChildBitmap.Dispose();
        

        return pt;
    }

    public Point SearchPixelBitmap(Bitmap Source, Bitmap Target, int LocationX = -1, int LocationY = -1)
    {
        Point pt = new Point(-1, -1);
        
        if (LocationX == -1 && LocationY == -1)
        {
            bool EndSearch = false;
            for (int i = 0; i < Source.Width; i++)
            {
                for (int j = 0; j < Source.Height; j++)
                {
                    if (i + Target.Width < Source.Width && j + Target.Height < Source.Height && Source.GetPixel(i, j) == Target.GetPixel(0, 0))
                    { 
                        bool IsMatch = true;
                        for (int i2 = 1; i2 < Target.Width; i2++)
                        {
                            for (int j2 = 1; j2 < Target.Height; j2++)
                            {
                                if (Source.GetPixel(i + i2, j + j2) != Target.GetPixel(i2, j2))
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
            if (LocationX + Target.Width <= Source.Width && LocationY + Target.Height <= Source.Height)
            {
                bool IsMatch = true;
                for (int i = 0; i < Target.Width; i++)
                {
                    for (int j = 0; j < Target.Height; j++)
                    {
                        if (Source.GetPixel(LocationX + i, LocationY + j) != Target.GetPixel(i, j))
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
                    return new Point(LocationX, LocationY);
                }
            }

        }

        return pt;
    }
}

