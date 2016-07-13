using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Math
{
    /// <summary>
    /// Matrix related function.
    /// </summary>
    static public class Matrix
    {
        #region public function
        /// <summary>
        /// Mutiple two matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="matrix1Colum"></param>
        /// <param name="matrix2Colum"></param>
        /// <returns></returns>
        static public MatrixData MutiMatrix(MatrixData matrix1, MatrixData matrix2)
        {
            MatrixData result = new MatrixData();
            {
                if (matrix1.ColNum == matrix2.LineNum)
                {
                    result.Data = new double[matrix1.LineNum * matrix1.ColNum];
                    for (int i = 0 ; i < matrix2.ColNum; i++)
                    {
                        for (int j = 0 ; j < matrix1.LineNum; j++)
                        {
                            result.Data[j * matrix2.ColNum + i] = 0;
                            for (int k = 0 ; k < matrix1.ColNum; k++)
                            {
                                result.Data[j * matrix2.ColNum + i] += matrix1.Data[j * matrix1.ColNum + k] * matrix2.Data[k * matrix2.ColNum + i];
                            }
                        }
                    }
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        #endregion
    }
}
