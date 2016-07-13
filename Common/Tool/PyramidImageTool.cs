using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Common.Tool
{
    public static class PyramidImageTool
    {
        private static double completedPercentage = 0;

        private static int completedStep = 0;
        public static double CompletedPercentage
        {
            get
            {
                return completedPercentage;
            }
        }

        public static int CompletedStep
        {
            get
            {
                return completedStep;
            }
        }
        public static BitmapImage GeneratePyramidImage(BitmapImage image, int level, int gauseLevel, double delta, int templateSize)
        {
            BitmapImage result = image;
            int lastHeight = 0;
            Common.Math.MatrixAdv gaussTemplate = Common.Math.SIFT.GetGaussTemplate(delta, templateSize);
            if (image != null)
            {
                Bitmap oriBitmap = BasicImageTool.BitmapImageToBitmap(image);
                Bitmap retBitmap = new Bitmap(GetPyramidImgaeWidth(oriBitmap.Width, level, gauseLevel), GetPyramidImageHeight(oriBitmap.Height, level, gauseLevel));
                Common.Math.MatrixAdv oriBitmapComponentsGaussConvolutionR = new Common.Math.MatrixAdv();
                Common.Math.MatrixAdv oriBitmapComponentsGaussConvolutionG = new Common.Math.MatrixAdv();
                Common.Math.MatrixAdv oriBitmapComponentsGaussConvolutionB = new Common.Math.MatrixAdv();
                Common.Math.MatrixAdv oriBitmapComponentsGaussConvolutionA = new Common.Math.MatrixAdv();
                for (int i = 0; i < level; i++)
                {
                    if (i == 0)
                    {
                        oriBitmapComponentsGaussConvolutionR = new Common.Math.MatrixAdv(oriBitmap.Height, oriBitmap.Width, BasicImageTool.GetSingleColorComponentsValueFormBitmap(oriBitmap, ColorComponent.R));
                        oriBitmapComponentsGaussConvolutionG = new Common.Math.MatrixAdv(oriBitmap.Height, oriBitmap.Width, BasicImageTool.GetSingleColorComponentsValueFormBitmap(oriBitmap, ColorComponent.G));
                        oriBitmapComponentsGaussConvolutionB = new Common.Math.MatrixAdv(oriBitmap.Height, oriBitmap.Width, BasicImageTool.GetSingleColorComponentsValueFormBitmap(oriBitmap, ColorComponent.B));
                        oriBitmapComponentsGaussConvolutionA = new Common.Math.MatrixAdv(oriBitmap.Height, oriBitmap.Width, BasicImageTool.GetSingleColorComponentsValueFormBitmap(oriBitmap, ColorComponent.A));
                        completedPercentage = 20;
                    }
                    else
                    {
                        lastHeight += oriBitmapComponentsGaussConvolutionA.Height;
                        oriBitmapComponentsGaussConvolutionR = BasicImageTool.SampleNIn1(oriBitmapComponentsGaussConvolutionR, 2);
                        oriBitmapComponentsGaussConvolutionG = BasicImageTool.SampleNIn1(oriBitmapComponentsGaussConvolutionG, 2);
                        oriBitmapComponentsGaussConvolutionB = BasicImageTool.SampleNIn1(oriBitmapComponentsGaussConvolutionB, 2);
                        oriBitmapComponentsGaussConvolutionA = BasicImageTool.SampleNIn1(oriBitmapComponentsGaussConvolutionA, 2);
                        completedPercentage = 20;
                    }
                    for (int j = 0; j < gauseLevel; j++)
                    {
                        oriBitmapComponentsGaussConvolutionR = Common.Math.SIFT.GaussConvolutionResult(oriBitmapComponentsGaussConvolutionR, gaussTemplate);
                        completedPercentage = 30;
                        oriBitmapComponentsGaussConvolutionG = Common.Math.SIFT.GaussConvolutionResult(oriBitmapComponentsGaussConvolutionG, gaussTemplate);
                        completedPercentage = 40;
                        oriBitmapComponentsGaussConvolutionB = Common.Math.SIFT.GaussConvolutionResult(oriBitmapComponentsGaussConvolutionB, gaussTemplate);
                        completedPercentage = 50;
                        BasicImageTool.InsertColorComponentsIntoBitMapPixel(j * oriBitmapComponentsGaussConvolutionR.Width, lastHeight, oriBitmapComponentsGaussConvolutionR, oriBitmapComponentsGaussConvolutionG, oriBitmapComponentsGaussConvolutionB, oriBitmapComponentsGaussConvolutionA, ref retBitmap);
                        completedPercentage = 100;
                    }
                    completedStep = i;
                }
                result = BasicImageTool.BitmapToBitmapImage(retBitmap);
            }
            return result;
        }

        private static int GetPyramidImgaeWidth(int inputWidth, int level, int gaussLevel)
        {
            return inputWidth * gaussLevel;

        }

        private static int GetPyramidImageHeight(int inputHeight, int level, int gaussLevel)
        {
            int result = inputHeight;
            for (int i = 1; i < gaussLevel; i++)
            {
                result += (int)(inputHeight / (System.Math.Pow(2, i))) + 1;
            }
            return result;
        }
    }
}
