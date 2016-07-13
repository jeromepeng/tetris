using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Common.Tool
{
    public enum ColorComponent
    {
        R,
        G,
        B,
        A
    }

    public static class BasicImageTool
    {
        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        /*[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);*/
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            /*Bitmap bitmapSource = new Bitmap(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color pixelColor = bitmap.GetPixel(i, j);
                    Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    bitmapSource.SetPixel(i, j, newColor);
                }
            }*/
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(ms.ToArray());
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
        }

        public static double[] GetSingleColorComponentsValueFormBitmap(Bitmap inputBitmap, ColorComponent components)
        {
            double[] result = null;
            if (inputBitmap != null)
            {
                result = new double[inputBitmap.Width * inputBitmap.Height];
                switch (components)
                {
                    case ColorComponent.R:
                        {
                            for (int i = 0; i < inputBitmap.Width; i++)
                            {
                                for (int j = 0; j < inputBitmap.Height; j++)
                                {
                                    result[j * inputBitmap.Width + i] = inputBitmap.GetPixel(i, j).R;
                                }
                            }
                            break;
                        }
                    case ColorComponent.G:
                        {
                            for (int i = 0; i < inputBitmap.Width; i++)
                            {
                                for (int j = 0; j < inputBitmap.Height; j++)
                                {
                                    result[j * inputBitmap.Width + i] = inputBitmap.GetPixel(i, j).G;
                                }
                            }
                            break;
                        }
                    case ColorComponent.B:
                        {
                            for (int i = 0; i < inputBitmap.Width; i++)
                            {
                                for (int j = 0; j < inputBitmap.Height; j++)
                                {
                                    result[j * inputBitmap.Width + i] = inputBitmap.GetPixel(i, j).B;
                                }
                            }
                            break;
                        }
                    case ColorComponent.A:
                        {
                            for (int i = 0; i < inputBitmap.Width; i++)
                            {
                                for (int j = 0; j < inputBitmap.Height; j++)
                                {
                                    result[j * inputBitmap.Width + i] = inputBitmap.GetPixel(i, j).A;
                                }
                            }
                            break;
                        }
                }
            }
            return result;
        }

        public static void InsertColorComponentsIntoBitMapPixel(int startX, int startY, Common.Math.MatrixAdv r, Common.Math.MatrixAdv g, Common.Math.MatrixAdv b, Common.Math.MatrixAdv a, ref Bitmap bitMap)
        {
            if (CanConvert(startX, startY, r, g, b, a, bitMap))
            {
                for (int i = 0; i < r.Width; i++)
                {
                    for (int j = 0; j < r.Height; j++)
                    {
                        bitMap.SetPixel(i + startX, j + startY, Color.FromArgb((int)a[i, j], (int)r[i, j], (int)g[i, j], (int)b[i, j]));
                    }
                }
            }
        }

        public static bool CanConvert(int startX, int startY, Common.Math.MatrixAdv r, Common.Math.MatrixAdv g, Common.Math.MatrixAdv b, Common.Math.MatrixAdv a, Bitmap bitMap)
        {
            return (r.Width == g.Width && g.Width == b.Width && b.Width == a.Width)
                && (r.Height == g.Height && g.Height == b.Height && b.Height == a.Height)
                && (startX + r.Width <= bitMap.Width)
                && (startY + r.Height <= bitMap.Height);
        }

        public static Common.Math.MatrixAdv SampleNIn1(Common.Math.MatrixAdv data, int n)
        {
            Common.Math.MatrixAdv result = new Common.Math.MatrixAdv(data.Height / n, data.Width / n);
            for (int i = 0; i < result.Width; i++)
            {
                for (int j = 0; j < result.Height; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        for (int l = 0; l < n; l++)
                        {
                            result[i, j] += data[i * n + k, j * n + l];
                        }
                    }
                    result[i, j] /= (n * n);
                }
            }
            return result;
        }
    }
}
