using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL.SceneGraph.Assets;

namespace Opengl
{
    static class Con
    {
        public static Bitmap ToBitmap(this List<ushort> pixels, int width, int height, double black = 0, double white = 1)
        {
            int index = 0;
            Bitmap bitmap = new Bitmap(width, height);
            int max = pixels.Max();
            int min = pixels.Min();
            int range = max - min;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int nColor = (int)(((double)(pixels[index] - range * black) / (range * white - range * black)) * max);
                    nColor = (int)(((double)nColor / range) * 255);
                    nColor = (nColor < 0) ? 0 : nColor;
                    nColor = (nColor > 255) ? 255 : nColor;
                    bitmap.SetPixel(i, j, Color.FromArgb(nColor, nColor, nColor));
                    index++;
                }
            }
            return bitmap;
        }

        public static Bitmap Conv(this Bitmap bitmap, int[,] filterMask)
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    newBitmap.SetPixel(i, j, GetConvColor(i, j, bitmap, filterMask));
                }
            }
            return newBitmap;
        }

        public static Bitmap Conv(this Bitmap bitmap, int[,] filterMask, int coeff)
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    newBitmap.SetPixel(i, j, GetConvColor(i, j, bitmap, filterMask, coeff));
                }
            }
            return newBitmap;
        }

        private static Color GetConvColor(int x, int y, Bitmap bitmap, int[,] filterMask)
        {
            int m = filterMask.GetLength(0) / 2;
            int col = 0;
            for (int i = -m; i <= m; i++)
            {
                for (int j = -m; j < m; j++)
                {
                    if ((x-i) >= 0 && (y-j) >=0 && (x-i) < bitmap.Width && (y-j) < bitmap.Height )
                    {
                        col += bitmap.GetPixel(x - i, y - j).R * filterMask[i + m, j + m]; 
                    }                    
                }
            }
            if (col < 0) col = 0;
            if (col > 255) col = 255;

            return Color.FromArgb(col, col, col);
        }

        private static Color GetConvColor(int x, int y, Bitmap bitmap, int[,] filterMask, int coeff)
        {
            int m = filterMask.GetLength(0) / 2;
            int col = 0;
            for (int i = -m; i <= m; i++)
            {
                for (int j = -m; j < m; j++)
                {
                    if ((x - i) >= 0 && (y - j) >= 0 && (x - i) < bitmap.Width && (y - j) < bitmap.Height)
                    {
                        col += bitmap.GetPixel(x - i, y - j).R * filterMask[i + m, j + m];
                    }
                }
            }
            col /= coeff;
            if (col < 0) col = 0;
            if (col > 255) col = 255;

            return Color.FromArgb(col, col, col);
        }

    }
}
