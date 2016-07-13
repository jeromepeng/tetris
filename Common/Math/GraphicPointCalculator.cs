using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Math
{
    /// <summary>
    /// Calculator for point in graphic space.
    /// </summary>
    static public class GraphicPointCalculator
    {
        /// <summary>
        /// Get the project point on screen of the target poing.
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <param name="screenAngle"></param>
        /// <param name="targetPoint"></param>
        /// <param name="focusLength"></param>
        /// <returns></returns>
        static public double[] GetScreenPoint(double[] screenPosition, double[] screenAngle, double[] targetPoint, double focusLength)
        {
            double[] result = new double[] { double.MaxValue, double.MaxValue };
            double[] screenPoint = CalculatorFor3D.GetCrossPointFromPlaneAndALine(
                CalculatorFor3D.GetVectorFromTwoPoints(CalculatorFor3D.MovePointOnLineWithADistance(screenAngle, screenPosition, focusLength), targetPoint), 
                targetPoint, 
                screenAngle, 
                screenPosition);
            double[] rotateMatrix = CalculatorFor3D.GetRotateMatrix(new double[] { 1, 0, 0 }, screenAngle);
            double[] translateMatrix = CalculatorFor3D.GetTranslateMatrix(screenPosition, new double[] { 0, 0, 0 });
            screenPoint = CalculatorFor3D.TransformPoint(screenPoint, rotateMatrix, translateMatrix);
            result[0] = screenPoint[0];
            result[1] = screenPoint[2];
            return result;
        }
    }
}
