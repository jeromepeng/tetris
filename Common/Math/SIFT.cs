using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Math
{
    /// <summary>
    /// Class for SIFT
    /// </summary>
    public class SIFT
    {
        #region Private Method
        /// <summary>
        /// The Gauss convolution core.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        private static double GaussConvolutionCore(double x, double y, double delta)
        {
            return System.Math.Pow(Constant.e, -(x * x + y * y) / (2 * delta * delta)) / (2 * Constant.PI * delta * delta);
        }

        /// <summary>
        /// Get gauss template.
        /// </summary>
        /// <param name="delta"></param>
        /// <param name="templateSize"></param>
        /// <returns></returns>
        public static MatrixAdv GetGaussTemplate(double delta, int templateSize)
        {
            int center = templateSize / 2;
            MatrixAdv result = new MatrixAdv(templateSize, templateSize);
            for (int i = 0; i <= center; i++)
            {
                for (int j = 0; j <= center; j++)
                {
                    double value = GaussConvolutionCore(i - center, j - center, delta);
                    result[i, j] = result[templateSize - i - 1, templateSize - j - 1] = result[i, templateSize - j - 1] = result[templateSize - i - 1, j] = value;
                }
            }
            return result;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Do gauss convolution with input data.
        /// </summary>
        /// <param name="delta"></param>
        /// <param name="templateSize"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static MatrixAdv GaussConvolutionResult(MatrixAdv input, MatrixAdv gaussTemplate)
        {
            MatrixAdv result = null;
            int halfTemplateSize = gaussTemplate.Width / 2;
            if (input.Width > gaussTemplate.Width && input.Height > gaussTemplate.Width)
            {
                result = new MatrixAdv(input.Height, input.Width);
                for (int i = halfTemplateSize; i < input.Width - halfTemplateSize; i++)
                {
                    for (int j = halfTemplateSize; j < input.Height - halfTemplateSize; j++)
                    {
                        result[i, j] = MatrixAdv.Convolution(gaussTemplate, input[j - halfTemplateSize, j + halfTemplateSize + 1, i - halfTemplateSize, i + halfTemplateSize + 1]);
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
