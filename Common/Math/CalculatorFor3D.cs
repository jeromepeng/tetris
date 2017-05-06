using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Math
{
    public static class CalculatorFor3D
    {
        #region 3D Rotate (With Rodrigues' Rotation Matrix)

        /// <summary>
        /// Calculat the rotate matrix between two vector.
        /// </summary>
        /// <param name="vectorBefore"></param>
        /// <param name="vectorAfter"></param>
        static double[] Calculation(double[] vectorBefore, double[] vectorAfter)
        {
            double[] rotationAxis;
            double rotationAngle;
            rotationAxis = CrossProduct(vectorBefore, vectorAfter);
            rotationAngle = System.Math.Acos(DotProduct(vectorBefore, vectorAfter) / Normalize(vectorBefore) / Normalize(vectorAfter));
            return RotationMatrix(rotationAngle, rotationAxis);
        }

        /// <summary>
        /// Cross produce between two vector.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static private double[] CrossProduct(double[] a, double[] b)
        {
            double[] c = new double[3];

            c[0] = a[1] * b[2] - a[2] * b[1];
            c[1] = a[2] * b[0] - a[0] * b[2];
            c[2] = a[0] * b[1] - a[1] * b[0];

            return c;
        }

        /// <summary>
        /// Dot product between two vector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static private double DotProduct(double[] a, double[] b)
        {
            double result;
            result = a[0] * b[0] + a[1] * b[1] + a[2] * b[2];

            return result;
        }

        /// <summary>
        /// Normalization of one vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        static private double Normalize(double[] v)
        {
            double result;

            result = System.Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);

            return result;
        }

        /// <summary>
        /// Get the rotate matrix.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        static private double[] RotationMatrix(double angle, double[] u)
        {
            double norm = Normalize(u);
            double[] rotatinMatrix = new double[9];

            u[0] = u[0] / norm;
            u[1] = u[1] / norm;
            u[2] = u[2] / norm;

            rotatinMatrix[0] = System.Math.Cos(angle) + u[0] * u[0] * (1 - System.Math.Cos(angle));
            rotatinMatrix[1] = u[0] * u[1] * (1 - System.Math.Cos(angle) - u[2] * System.Math.Sin(angle));
            rotatinMatrix[2] = u[1] * System.Math.Sin(angle) + u[0] * u[2] * (1 - System.Math.Cos(angle));

            rotatinMatrix[3] = u[2] * System.Math.Sin(angle) + u[0] * u[1] * (1 - System.Math.Cos(angle));
            rotatinMatrix[4] = System.Math.Cos(angle) + u[1] * u[1] * (1 - System.Math.Cos(angle));
            rotatinMatrix[5] = -u[0] * System.Math.Sin(angle) + u[1] * u[2] * (1 - System.Math.Cos(angle));

            rotatinMatrix[6] = -u[1] * System.Math.Sin(angle) + u[0] * u[2] * (1 - System.Math.Cos(angle));
            rotatinMatrix[7] = u[0] * System.Math.Sin(angle) + u[1] * u[2] * (1 - System.Math.Cos(angle));
            rotatinMatrix[8] = System.Math.Cos(angle) + u[2] * u[2] * (1 - System.Math.Cos(angle));

            return rotatinMatrix;
        }
        #endregion

        #region Function for transform between coordinate system.

        /// <summary>
        /// Get the cross point which one line cross a plane.
        /// </summary>
        /// <param name="lineVector">The vector of the line.</param>
        /// <param name="linePoint">A point on the line.</param>
        /// <param name="normalVector">The normal vector of the plane.</param>
        /// <param name="planePoint">A point on the plane.</param>
        /// <returns>The cross point between the plane and line.</returns>
        static public double[] GetCrossPointFromPlaneAndALine(double[] lineVector, double[] linePoint, double[] normalVector, double[] planePoint)
        {
            double[] result = new double[] { double.MaxValue, double.MaxValue, double.MaxValue };
            return result;
        }

        /// <summary>
        /// Transform on point from one coordinate to another coordinate.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="rotateMatrix"></param>
        /// <param name="translateMatrix"></param>
        /// <returns></returns>
        static public double[] TransformPoint(double[] point, double[] rotateMatrix, double[] translateMatrix)
        {
            double[] result = new double[] { double.MaxValue, double.MaxValue, double.MaxValue };
            return result;
        }

        /// <summary>
        /// Get the rotate matrix between two coordinate systems.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        static public double[] GetRotateMatrix(double[] vector1, double[] vector2)
        {
            double[] result = new double[] { double.MaxValue, double.MaxValue, double.MaxValue,
                                             double.MaxValue, double.MaxValue, double.MaxValue,
                                             double.MaxValue, double.MaxValue, double.MaxValue };
            return result;
        }

        /// <summary>
        /// Get the translate matrix between two coordinate systems.
        /// </summary>
        /// <param name="oriPoint1"></param>
        /// <param name="oriPoint2"></param>
        /// <returns></returns>
        static public double[] GetTranslateMatrix(double[] oriPoint1, double[] oriPoint2)
        {
            double[] result = new double[] { oriPoint1[0] - oriPoint2[0], oriPoint1[1] - oriPoint2[1], oriPoint1[2] - oriPoint2[2] };
            return result;
        }

        /// <summary>
        /// Get vector between two points.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        static public double[] GetVectorFromTwoPoints(double[] point1, double[] point2)
        {
            return new double[] { point2[0] - point1[0], point2[1] - point1[1], point2[2] - point1[2] };
        }

        /// <summary>
        /// Move a point in the line with a distance.
        /// </summary>
        /// <param name="lineVector"></param>
        /// <param name="linePoint"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        static public double[] MovePointOnLineWithADistance(double[] lineVector, double[] linePoint, double length)
        {
            double[] result = new double[] { double.MaxValue, double.MaxValue, double.MaxValue };
            return result;
        }

        /// <summary>
        /// Transfer a point from the coordinate of space to coordinate of virtual.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        static public double[] TransferPointFromSpace3DToVirtual3D(double[] point)
        {
            double[] result = new double[3] { 0.0, 0.0, 0.0 };
            if (point.Length == 3)
            {
                result[0] = point[0];
                result[1] = point[2];
                result[2] = point[1];
            }
            return result;
        }

        /// <summary>
        /// Transfer a point from virtual 2D space to screen 2D.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        static public double[] TransferPointFromVirtual2DToScreen2D(double[] point, double screenHeight)
        {
            double[] result = new double[2] { 0.0, 0.0 };
            if (point.Length >= 2)
            {
                result[0] = point[0];
                result[1] = screenHeight - point[1];
            }
            return result;
        }

        /// <summary>
        /// Get the relationship between point and plane
        /// </summary>
        /// <param name="point"></param>
        /// <param name="plane"></param>
        /// <returns></returns>
        static public double RelationshipBetweenPointAndPlane(double[] point, List<double[]> plane)
        {
            double result = 0;
            if (plane.Count > 2)
            {
                MatrixAdv calculateMatrix = new MatrixAdv(4, 4, new double[] {
                1, point[0], point[1], point[2],
                1, plane[0][0], plane[0][1], plane[0][2],
                1, plane[1][0], plane[1][1], plane[1][2],
                1, plane[2][0], plane[2][1], plane[2][2]});
                result = MatrixAdv.GetDeterminant(calculateMatrix);
            }
            return result;
        }

        /// <summary>
        /// Get the relationship between point and plane with whose cofactors.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="cofactorsOfPlane"></param>
        /// <param name="plane"></param>
        /// <returns></returns>
        static public double RelationshipBetweenPointAndPlaneWithCofactors(double[] point, double[] cofactorsOfPlane, List<double[]> plane)
        {
            double result = 0;
            if (plane.Count > 2)
            {
                MatrixAdv calculateMatrix = new MatrixAdv(4, 4, new double[] {
                1, point[0], point[1], point[2],
                1, plane[0][0], plane[0][1], plane[0][2],
                1, plane[1][0], plane[1][1], plane[1][2],
                1, plane[2][0], plane[2][1], plane[2][2]});
                result = MatrixAdv.GetDeterminantWithCofactor(calculateMatrix, cofactorsOfPlane);
            }
            return result;
        }

        /// <summary>
        /// Return true if the point1 and point2 are at the same side of one panel.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="plane"></param>
        /// <returns></returns>
        static public bool IsPointsAtTheSameSideOfPlane(double[] point1, double[] point2, List<double[]> plane)
        {
            bool result = true;
            result = (RelationshipBetweenPointAndPlane(point1, plane) > 0 && RelationshipBetweenPointAndPlane(point2, plane) > 0)
                || (RelationshipBetweenPointAndPlane(point1, plane) < 0 && RelationshipBetweenPointAndPlane(point2, plane) < 0);
            return result;
        }

        /// <summary>
        /// Get the transaction point between line and plane
        /// </summary>
        /// <param name="cofactosOfPlane"></param>
        /// <param name="constantOfLine"></param>
        /// <param name="directionVector"></param>
        /// <returns></returns>
        static public double[] TransactionBetweenLineAndPlane(double[] cofactosOfPlane, double[] constantOfLine, double[] directionVector)
        {
            double[] result = new double[3] { 0, 0, 0 };
            if (cofactosOfPlane.Length == 4 && constantOfLine.Length == 3 && directionVector.Length == 3)
            {
                double t = -((cofactosOfPlane[0] + constantOfLine[0] * cofactosOfPlane[1] + constantOfLine[1] * cofactosOfPlane[2] + constantOfLine[2] * cofactosOfPlane[3]) / (cofactosOfPlane[1] * directionVector[0] + cofactosOfPlane[2] * directionVector[1] + cofactosOfPlane[3] * directionVector[2]));
                for (int i = 0; i < 3; i++)
                {
                    result[i] = t * directionVector[i] + constantOfLine[i];
                }
            }
            return result;
        }

        /// <summary>
        /// Return true if number is between number1 and number2.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        static public bool IsNumberBetweenTwoNumber(double number, double number1, double number2)
        {
            bool result = false;
            if (number1 == number2)
            {
                result = number == number1;
            }
            else
            {
                if (number1 > number2)
                {
                    result = number > number2 && number < number1;
                }
                else
                {
                    result = number < number2 && number > number1;
                }
            }
            return result;
        }

        /// <summary>
        /// Return ture if pt is in the area of the plane.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="planeRange"></param>
        /// <param name="cofactorsOfPlane"></param>
        /// <param name="dataOfPlane"></param>
        /// <returns></returns>
        static public bool IsPointInPlane(double[] point, double[] planeRange, double[] cofactorsOfPlane, List<double[]> dataOfPlane, bool isTransaction)
        {
            bool result = false;
            if (point.Length * 2 == planeRange.Length &&
                cofactorsOfPlane.Length - 1 == point.Length &&
                dataOfPlane.Count > 2 &&
                dataOfPlane[0].Length == point.Length)
            {
                result = isTransaction ? true : System.Math.Round(RelationshipBetweenPointAndPlaneWithCofactors(point, cofactorsOfPlane, dataOfPlane), 3) == 0.000;
                for (int i = 0; i < point.Length; i++)
                {
                    result = result && IsNumberBetweenTwoNumber(System.Math.Round(point[i], 3), System.Math.Round(planeRange[i * 2], 3), System.Math.Round(planeRange[i * 2 + 1], 3));
                }
            }
            return result;
        }

        /// <summary>
        /// Rotate a point around one axis with one angle and axis go through (0, 0, 0).
        /// </summary>
        /// <param name="point"></param>
        /// <param name="axisType"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static public double[] RotatePointWithOneAxisOriginal(double[] point, MoveType axisType, double angle)
        {
            double[] result = new double[3];
            MatrixAdv rotateMatrix = GetRotateMatrix(axisType, angle);
            MatrixAdv pointMatrix = new MatrixAdv(1, 3, point);
            result = (pointMatrix * rotateMatrix).Data;
            return result;
        }

        /// <summary>
        /// Rotate a point around one axis with one angle and axis go through (originalCoord[0], originalCoord[1], originalCoord[2]).
        /// </summary>
        /// <param name="point"></param>
        /// <param name="axisType"></param>
        /// <param name="angle"></param>
        /// <param name="originalCoord"></param>
        /// <returns></returns>
        static public double[] RotatePointWithOneAxisAny(double[] point, MoveType axisType, double angle, double[] originalCoord)
        {
            double[] result = new double[3];
            MatrixAdv rotateMatrix = GetRotateMatrix(axisType, angle);
            MatrixAdv original = new MatrixAdv(1, 3, originalCoord);
            MatrixAdv pointMatrix = new MatrixAdv(1, 3, point);
            pointMatrix = pointMatrix - original;
            pointMatrix = pointMatrix * rotateMatrix;
            pointMatrix = pointMatrix + original;
            result = pointMatrix.Data;
            return result;
        }

        /// <summary>
        /// Get the rotate matrix with one angle.
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static public MatrixAdv GetRotateMatrix(MoveType axisType, double angle)
        {
            MatrixAdv result = null;
            double rad = (angle / 360) * 2 * System.Math.PI;
            double[] matrix = new double[9];
            switch (axisType)
            {
                case MoveType.X:
                    {
                        matrix[0] = 1;
                        matrix[1] = 0;
                        matrix[2] = 0;
                        matrix[3] = 0;
                        matrix[4] = System.Math.Cos(rad);
                        matrix[5] = System.Math.Sin(rad);
                        matrix[6] = 0;
                        matrix[7] = -System.Math.Sin(rad);
                        matrix[8] = System.Math.Cos(rad);
                        result = new MatrixAdv(3, 3, matrix);
                        break;
                    }
                case MoveType.Y:
                    {
                        matrix[0] = System.Math.Cos(rad);
                        matrix[1] = 0;
                        matrix[2] = -System.Math.Sin(rad);
                        matrix[3] = 0;
                        matrix[4] = 1;
                        matrix[5] = 0;
                        matrix[6] = System.Math.Sin(rad);
                        matrix[7] = 0;
                        matrix[8] = System.Math.Cos(rad);
                        result = new MatrixAdv(3, 3, matrix);
                        break;
                    }
                case MoveType.Z:
                    {
                        matrix[0] = System.Math.Cos(rad);
                        matrix[1] = System.Math.Sin(rad);
                        matrix[2] = 0;
                        matrix[3] = -System.Math.Sin(rad);
                        matrix[4] = System.Math.Cos(rad);
                        matrix[5] = 0;
                        matrix[6] = 0;
                        matrix[7] = 0;
                        matrix[8] = 1;
                        result = new MatrixAdv(3, 3, matrix);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return result;
        }
        #endregion
    }
}
