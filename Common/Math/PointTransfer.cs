using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Math
{
    /// <summary>
    /// Some method to transfer point (etc. from 3D to 2D)
    /// </summary>
    public static class PointTransfer
    {
        /// <summary>
        /// Transfer the point from 3D to Screen 2D (screen was parallel with z-o-x).
        /// </summary>
        /// <returns></returns>
        public static double[] TransferPointFrom3DToScreen2D(double[] pointOf3D, double[] projectPoint, double screenHeight)
        {
            double[] result = new double[2]{0.0, 0.0};
            if (pointOf3D.Length == 3 && pointOf3D[1] > 0 && projectPoint.Length == 3 && projectPoint[1] < 0)
            {
                double[] tempResult = CalculatorFor3D.TransferPointFromSpace3DToVirtual3D(pointOf3D);
                double[] screenProjectPoint = CalculatorFor3D.TransferPointFromSpace3DToVirtual3D(projectPoint);
                MatrixAdv oriPt = new MatrixAdv(1, 2, new double[2] { tempResult[0] - screenProjectPoint[0], tempResult[1] - screenProjectPoint[1] });
                double transferFactor = screenProjectPoint[2] / (screenProjectPoint[2] - tempResult[2]);
                MatrixAdv transfer = new MatrixAdv(2, 2, new double[4] {transferFactor, 0, 0, transferFactor});
                MatrixAdv constant = new MatrixAdv(1, 2, new double[2] { screenProjectPoint[0], screenProjectPoint[1] });
                result = ((oriPt * transfer) + constant).Data;
                result = CalculatorFor3D.TransferPointFromVirtual2DToScreen2D(result, screenHeight);
            }
            return result;
        }

        /// <summary>
        /// Transfer the point from 3D to Screen 2D (transacation from screen and project line has been known and bottom line of the screen is parallel with x-o-y).
        /// </summary>
        /// <param name="pointOf3D"></param>
        /// <param name="leftBottomPoint"></param>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        public static double[] TransferPointFrom3DToScreen2DWithScreenOfAnyAngle(double[] pointOf3D, double[] leftBottomPoint, double screenHeight)
        {
            double[] result = new double[3] { 0.0, 0.0, 0.0 };
            double[] temp2DResult = new double[2] { 0.0, 0.0 };
            if (pointOf3D.Length == 3 && leftBottomPoint.Length == 3)
            {
                double[] tempResult = CalculatorFor3D.TransferPointFromSpace3DToVirtual3D(pointOf3D);
                double[] bottomLinePoint1 = CalculatorFor3D.TransferPointFromSpace3DToVirtual3D(leftBottomPoint);
                result[1] = tempResult[1] - bottomLinePoint1[1];
                result[0] = System.Math.Sqrt(System.Math.Pow(tempResult[0] - bottomLinePoint1[0], 2) + System.Math.Pow(tempResult[2] - bottomLinePoint1[2], 2));
                temp2DResult = CalculatorFor3D.TransferPointFromVirtual2DToScreen2D(result, screenHeight);
                //Add deepth information for every plan.
                result[0] = temp2DResult[0];
                result[1] = temp2DResult[1];
                result[2] = pointOf3D[0];
            }
            return result;
        }
    }
}
