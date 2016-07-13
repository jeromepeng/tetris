using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Math
{
    /// <summary>
    /// Advance class for the operation of matrix.
    /// Replace of the original class "Matrix".
    /// </summary>
    public class MatrixAdv : BaseOperatoin<MatrixAdv>
    {
        #region Private Property

        /// <summary>
        /// Matrix Data
        /// </summary>
        private double[] data;

        /// <summary>
        /// The length of the matrix data.
        /// </summary>
        private int height;
        
        /// <summary>
        /// The and width of the matrix data.
        /// </summary>
        private int width;
        #endregion
     
        #region Construction
        /// <summary>
        /// For interface BaseOperatoin<MatrixAdv>
        /// </summary>
        public MatrixAdv()
        {
 
        }

        /// <summary>
        /// Construction.
        /// </summary>
        /// <param name="lineNum"></param>
        /// <param name="colNum"></param>
        /// <param name="data"></param>
        public MatrixAdv(int lineNum, int colNum, double[] data)
        {
            height = lineNum; 
            width = colNum;
            this.data = new double[lineNum * width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    this.data[i * width + j] = data[i * width + j];
                }
            }
        }

        /// <summary>
        /// Construction just with num of the line and column.
        /// </summary>
        /// <param name="lineNum"></param>
        /// <param name="colNum"></param>
        public MatrixAdv(int lineNum, int colNum)
            : this(lineNum, colNum, new double[lineNum * colNum])
        {
            
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Get the column number of the data.
        /// </summary>
        public int Height
        {
            get { return height; }
        }

        /// <summary>
        /// For BaseOperatoin.IsEqual to make judgement.
        /// </summary>
        public double[] Data
        {
            get
            {
                return data;
            }
        }

        /// <summary>
        /// Get hte line number of the data.
        /// </summary>
        public int Width
        {
            get { return width; }
        }
        #endregion

        #region Public Function

        /// <summary>
        /// Get the value of the data with the line number and colum number
        /// </summary>
        /// <param name="line"></param>
        /// <param name="colum"></param>
        /// <returns></returns>
        public double this[int line, int colum]
        {
            get
            {
                return data[line * width + colum];
            }
            set
            {
                data[line * width + colum] = value;
            }
        }

        /// <summary>
        /// Get sub matrix of this matrix.
        /// </summary>
        /// <param name="line">Start point</param>
        /// <param name="lineEnd">End point</param>
        /// <param name="colum"></param>
        /// <param name="columEnd"></param>
        /// <returns>Sub matrix</returns>
        /// | 1 2 3 4 5 | [1, 4, 1, 4] = | 3 4 5 |
        /// | 2 3 4 5 6 |                | 4 5 6 |
        /// | 3 4 5 6 7 |                | 5 6 7 |
        /// | 4 5 6 7 8 |
        /// | 5 6 7 8 9 |                
        public MatrixAdv this[int line, int lineEnd, int colum, int columEnd]
        {
            get
            {
                MatrixAdv result = null;
                if (lineEnd > line && columEnd > colum &&
                    line >= 0 && colum >= 0 &&
                    lineEnd <= this.height && columEnd <= this.width)
                {
                    result = new MatrixAdv(lineEnd - line, columEnd - colum);
                    for (int i = 0; i < columEnd - colum; i++)
                    {
                        for (int j = 0; j < lineEnd - line; j++)
                        {
                            result[j, i] = this[j + line, i + colum];
                        }
                    }
                }
                return result;
            }
        }


        /// <summary>
        /// The operation of addition between two matrices.
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <returns></returns>
        public static MatrixAdv operator +(MatrixAdv matrixA, MatrixAdv matrixB)
        {
            if (matrixA.Width != matrixB.Width || matrixA.Height != matrixB.Height)
            {
                return null;
            }
            MatrixAdv c = new MatrixAdv(matrixA.Height, matrixA.Width);
            for (int i = 0; i < matrixA.Height; i++)
            {
                for (int j = 0; j < matrixA.Width; j++)
                {
                    c[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }
            return c;
        }

        /// <summary>
        /// The operation of subtraction between two matrices.
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <returns></returns>
        public static MatrixAdv operator -(MatrixAdv matrixA, MatrixAdv matrixB)
        {
            if (matrixA.Width != matrixB.Width || matrixA.Height != matrixB.Height)
            {
                return null;
            }
            MatrixAdv c = new MatrixAdv(matrixA.Height, matrixA.Width);
            for (int i = 0; i < matrixA.Height; i++)
            {
                for (int j = 0; j < matrixA.Width; j++)
                {
                    c[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            }
            return c;
        }

        /// <summary>
        /// The operation of multiplication between two matrices.
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <returns></returns>
        public static MatrixAdv operator *(MatrixAdv matrixA, MatrixAdv matrixB)
        {
            //Console.WriteLine(" a:{0}X{1}  b:{2}X{3}", a.Length, a.Width, b.Length, b.Width);

            if (matrixA.Width != matrixB.Height)
            {
                //  Console.WriteLine("error a:{0}X{1}  b:{2}X{3}", a.Length, a.Width, b.Length, b.Width);
                return null;
            }
            MatrixAdv c = new MatrixAdv(matrixA.Height, matrixB.Width);
            for (int i = 0; i < c.Height; i++)
            {
                for (int j = 0; j < c.Width; j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < matrixA.Width; k++)
                    {
                        c[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }
            return c;
        }

        /// <summary>
        /// The operation of equation between two matrices.
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <returns></returns>
        public static bool operator ==(MatrixAdv matrixA, MatrixAdv matrixB)
        {
            /*bool result = true;
            if (matrixA.Height != matrixB.Height || matrixA.Width != matrixB.Width)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < matrixA.Height; i++)
                {
                    for (int j = 0; j < matrixA.Width; j++)
                    {
                        if (matrixA[i, j] != matrixB[i, j])
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;*/
            return matrixA.IsEqual(matrixB);
        }

        /// <summary>
        /// The operation of noneequation between two matrices.
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <returns></returns>
        public static bool operator !=(MatrixAdv matrixA, MatrixAdv matrixB)
        {
            /*bool result = false;
            if (matrixA.Height != matrixB.Height || matrixA.Width != matrixB.Width)
            {
                result = true;
            }
            else
            {
                for (int i = 0; i < matrixA.Height; i++)
                {
                    for (int j = 0; j < matrixA.Width; j++)
                    {
                        if (matrixA[i, j] != matrixB[i, j])
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;*/
            return !matrixA.IsEqual(matrixB);
        }

        /// <summary>
        /// Get the convolution from two matrix.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static double Convolution(MatrixAdv matrix1, MatrixAdv matrix2)
        {
            double result = double.MinValue;
            if (matrix1.Width == matrix2.Width && matrix1.Height == matrix2.Height)
            {
                result = 0;
                for (int i = 0 ; i < matrix1.Width; i++)
                {
                    for (int j = 0; j < matrix1.Height; j++)
                    {
                        result += matrix1[j, i] * matrix2[j, i];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Return the converse of the matrix
        /// </summary>
        /// <param name="matrixIn"></param>
        /// <returns></returns>
        public static MatrixAdv Converse(MatrixAdv matrixIn)
        {
            if (matrixIn.Height != matrixIn.Width)
            {
                return null;
            }
            //clone
            MatrixAdv a = new MatrixAdv(matrixIn.Height, matrixIn.Width);
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < a.Width; j++)
                {
                    a[i, j] = matrixIn[i, j];
                }
            }
            MatrixAdv c = new MatrixAdv(a.Height, a.Width);
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < a.Width; j++)
                {
                    if (i == j) { c[i, j] = 1; }
                    else { c[i, j] = 0; }
                }
            }

            //i表示第几行，j表示第几列
            for (int j = 0; j < a.Height; j++)
            {
                bool flag = false;
                for (int i = j; i < a.Height; i++)
                {
                    if (a[i, j] != 0)
                    {
                        flag = true;
                        double temp;
                        //交换i,j,两行
                        if (i != j)
                        {
                            for (int k = 0; k < a.Height; k++)
                            {
                                temp = a[j, k];
                                a[j, k] = a[i, k];
                                a[i, k] = temp;

                                temp = c[j, k];
                                c[j, k] = c[i, k];
                                c[i, k] = temp;
                            }
                        }
                        //第j行标准化
                        double d = a[j, j];
                        for (int k = 0; k < a.Height; k++)
                        {
                            a[j, k] = a[j, k] / d;
                            c[j, k] = c[j, k] / d;
                        }
                        //消去其他行的第j列
                        d = a[j, j];
                        for (int k = 0; k < a.Height; k++)
                        {
                            if (k != j)
                            {
                                double t = a[k, j];
                                for (int n = 0; n < a.Height; n++)
                                {
                                    a[k, n] -= (t / d) * a[j, n];
                                    c[k, n] -= (t / d) * c[j, n];
                                }
                            }
                        }
                    }
                }
                if (!flag) return null;
            }
            return c;
        }

        /// <summary>
        /// Get a matrix with a specifically accuracy.
        /// If accuracy = 2 then:
        /// 1.0000001 => 1.00
        /// </summary>
        /// <param name="accuracy"></param>
        /// <param name="thisMatrix"></param>
        /// <returns></returns>
        public static MatrixAdv GetMatrixWithSpecificallyAccuracy(MatrixAdv thisMatrix, int accuracy)
        {
            MatrixAdv result = new MatrixAdv(thisMatrix.Height, thisMatrix.Width);
            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    result[i, j] = System.Math.Round(thisMatrix[i, j], accuracy);
                }
            }
            return result;
        }

        /// <summary>
        /// Get the cofactor of a matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static MatrixAdv GetCofactor(MatrixAdv matrix, int ai, int aj)
        {
            int m = matrix.Width;
            int n = matrix.Height;
            if (m != n)
            {
                Exception myException = new Exception("Height of matrix is not equal to width.");
                throw myException;
            }
            int n2 = n - 1;
            MatrixAdv teampMatirx = new MatrixAdv(n2, n2);

            //left up
            for (int i = 0; i < ai; i++)
            {
                for (int j = 0; j < aj; j++)
                {
                    teampMatirx[i, j] = matrix[i, j];
                }
            }
            //right down
            for (int i = ai; i < n2; i++)
            {
                for (int j = aj; j < n2; j++)
                {
                    teampMatirx[i, j] = matrix[i + 1, j + 1];
                }
            }
            //rigth up
            for (int i = 0; i < ai; i++)
            {
                for (int j = aj; j < n2; j++)
                {
                    teampMatirx[i, j] = matrix[i, j + 1];
                }
            }
            //left down
            for (int i = ai; i < n2; i++)
            {
                for (int j = 0; j < aj; j++)
                {
                    teampMatirx[i, j] = matrix[i + 1, j];
                }
            }
            //signal
            if ((ai + aj) % 2 != 0)
            {
                for (int i = 0; i < n2; i++)
                {
                    teampMatirx[i, 0] = -teampMatirx[i, 0];
                }
            }
            return teampMatirx;
        }

        /// <summary>
        /// Get determinant of matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double GetDeterminant(MatrixAdv matrix)
        {
            double result = 0;
            int m = matrix.Width;
            int n = matrix.Height;
            if (m != n)
            {
                Exception myException = new Exception("Height of matrix is not equal to width.");
                throw myException;
            }
            if (n == 1)
            {
                result = matrix[0, 0];
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    result += matrix[0, i] * GetDeterminant(GetCofactor(matrix, 0, i));
                }
            }
            return result;
        }

        /// <summary>
        /// Get determinant with konwn cofactor.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="cofactor"></param>
        /// <returns></returns>
        public static double GetDeterminantWithCofactor(MatrixAdv matrix, double[] cofactor)
        {
            double result = 0;
            int m = matrix.Width;
            int n = matrix.Height;
            if (m != n || cofactor.Length != m)
            {
                Exception myException = new Exception("Height of matrix is not equal to width.");
                throw myException;
            }
            if (n == 1)
            {
                result = matrix[0, 0];
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    result += matrix[0, i] * cofactor[i];
                }
            }
            return result;
        }

        /// <summary>
        /// Get the cofactors of one line.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="lineNum"></param>
        /// <returns></returns>
        public static double[] GetCofactorOfOneLine(MatrixAdv matrix, int lineNum)
        {
            int m = matrix.Width;
            int n = matrix.Height;
            if (m != n || m == 1)
            {
                Exception myException = new Exception("Height of matrix is not equal to width.");
                throw myException;
            }
            double[] result = new double[m];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetDeterminant(GetCofactor(matrix, lineNum, i));
            }
            return result;
        }

        #endregion

        #region Test function in console.

        /// <summary>
        /// Out put the value of the matrix in console.
        /// </summary>
        public void OutputInConsole()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(this[i, j].ToString("0.00000000") + " ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine();
        }
        #endregion

        #region Override Method
        /// <summary>
        /// Override of Equals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.IsEqual(obj as MatrixAdv);
        }

        /// <summary>
        /// Override of GetHashCode.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Override of ToString.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder toString = new StringBuilder();
            string stringLine;
            for (int i = 0; i < height; i++)
            {
                stringLine = string.Empty;
                for (int j = 0; j < width; j++)
                {
                    stringLine = stringLine + this[i, j].ToString("0.00000000") + " ";
                }
                toString.Append(stringLine);
            }
            return toString.ToString();
        }
        #endregion
    }
}
/*
[a1,b1,c1,d1]|x|=|0|
[a2,b2,c2,d2]|y| |0|
[a3,b3,c3,d3]|z| |0|
             |1|
*/