using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Math
{
    /// <summary>
    /// To rotate one point.
    /// </summary>
    public static class Rotate
    {
        #region private property
        
        #endregion

        #region public function
        /// <summary>
        /// Rotate one point with the angle(degree)
        /// </summary>
        /// <param name="inputPoint"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static public DataPoint RotatePoint(DataPoint inputPoint, double angle)
        {
            DataPoint result = new DataPoint();
            MatrixData oriPoint = new MatrixData();
            oriPoint.ColNum = 2;
            oriPoint.LineNum = 1;
            oriPoint.Data = new double[] {inputPoint.X, inputPoint.Y };
            MatrixData rotatePoint = Matrix.MutiMatrix(oriPoint, CreateRotateMatrix(90, 2));
            if (rotatePoint != null)
            {
                result = inputPoint.Clone();
                result.X = DoubleToInt(rotatePoint.Data[0]);
                result.Y = DoubleToInt(rotatePoint.Data[1]);
            }
            return result;
        }

        /// <summary>
        /// Create the roate matrix with the angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="dimension">2 = 2D, 3 = 3D</param>
        static public MatrixData CreateRotateMatrix(double angle, int dimension)
        {
            MatrixData result = new MatrixData();
            switch (dimension)
            {
                case 2:
                    {
                        result.ColNum = 2;
                        result.LineNum = 2;
                        result.Data = new double[] {System.Math.Cos(AngleToRad(angle)), -System.Math.Sin(AngleToRad(angle)), System.Math.Sin(AngleToRad(angle)), System.Math.Cos(AngleToRad(angle)) };
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return result;
        }

        /// <summary>
        /// Convert the angle to rad.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        static public double AngleToRad(double angle)
        {
            return (angle / 360.0) * 2 * Constant.PI;
        }

        /// <summary>
        /// Convert double to int.
        /// 0.99999999 -> 1;
        /// -0.9999999 -> -1;
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public int DoubleToInt(double input)
        {
            return input > 0 ? (int)(input + 0.5) : (int)(input - 0.5);
        }
        #endregion

        #region Private functions

        

        #endregion
    }
}
