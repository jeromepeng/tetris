using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using Common.Math;
using System.Collections.Generic;
using Data.DataFor3D;

namespace UnitTestProjectForRotate
{
    #region Testing class defeined
    class IsEqualSubClass : Common.Interface.BaseOperatoin<IsEqualClass>
    {
        public Int64 testSubValue1 { get; set; }
        public byte testSubValue2 { get; set; }
        public float testSubValue3 { get; set; }
    }
    class IsEqualClass : Common.Interface.BaseOperatoin<IsEqualClass>
    {
        public int testValue1 { get; set; }
        public string testValue2 { get; set; }
        public double testValue3 { get; set; }

        public IsEqualSubClass testValue4 { get; set; }
    }
    #endregion

    [TestClass]
    public class UnitTestForRotatePoint
    {
        [TestMethod]
        public void TestForRotatePointIn2D()
        {
            DataPoint testPoint = new DataPoint();
            testPoint.X = 1;
            testPoint.Y = 2;
            DataPoint resultPoint = new DataPoint();
            resultPoint.X = 2;
            resultPoint.Y = -1;
            DataPoint actualResult = new DataPoint();
            actualResult = Common.Math.Rotate.RotatePoint(testPoint, 90);
            Assert.AreEqual(resultPoint.IsEqual(actualResult), true);
        }

        [TestMethod]
        public void TestForConversMatrixRightResult()
        {
            Common.Math.MatrixAdv test = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             -1, 3, -7, 10 , 
             -7, -3, 5, 10 , 
             3, 1, -1, 2 , 
             1, 1, -1, 2  });
            Common.Math.MatrixAdv testResult = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             1.00, 0, 0, 0 , 
             0, 1.00, 0, 0 , 
             0, 0, 1.00, 0 , 
             0, 0, 0, 1.00  });
            Assert.AreEqual(testResult == Common.Math.MatrixAdv.GetMatrixWithSpecificallyAccuracy((Common.Math.MatrixAdv.Converse(test) * test), 2), true);
        }

        [TestMethod]
        public void TestForConversMatrixWrongResult()
        {
            Common.Math.MatrixAdv test = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             -1, 3, -7, 10 , 
             -7, -3, 5, 10 , 
             3, 1, -1, 2 , 
             1, 1, -1, 2  });
            Common.Math.MatrixAdv testResult = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             1.00, 0, 0, 0 , 
             0, 1.00, 0, 0 , 
             0, 0, 1.00, 0 , 
             0, 0, 0, 0.00  });
            Assert.AreEqual(testResult == Common.Math.MatrixAdv.GetMatrixWithSpecificallyAccuracy((Common.Math.MatrixAdv.Converse(test) * test), 2), false);
        }

        [TestMethod]
        public void TestForIsEqual()
        {
            IsEqualClass test1 = new IsEqualClass() { testValue1 = 1, testValue2 = "2", testValue3 = 3.1, testValue4 = new IsEqualSubClass() { testSubValue1 = 4, testSubValue2 = 5, testSubValue3 = 6.0f } };
            IsEqualClass test2 = new IsEqualClass() { testValue1 = 1, testValue2 = "2", testValue3 = 3.1, testValue4 = new IsEqualSubClass() { testSubValue1 = 4, testSubValue2 = 5, testSubValue3 = 6.0f } };
            Assert.AreEqual(test1.IsEqual(test2), true);
        }

        [TestMethod]
        public void TestForIsNotEqualForString()
        {
            IsEqualClass test1 = new IsEqualClass() { testValue1 = 1, testValue2 = "2", testValue3 = 3.1, testValue4 = new IsEqualSubClass() { testSubValue1 = 4, testSubValue2 = 5, testSubValue3 = 6.0f } };
            IsEqualClass test2 = new IsEqualClass() { testValue1 = 1, testValue2 = "2 ", testValue3 = 3.1, testValue4 = new IsEqualSubClass() { testSubValue1 = 4, testSubValue2 = 5, testSubValue3 = 6.0f } };
            Assert.AreEqual(test1.IsEqual(test2), false);
        }

        [TestMethod]
        public void TestForIsNotEqualForSubClass()
        {
            IsEqualClass test1 = new IsEqualClass() { testValue1 = 1, testValue2 = "2", testValue3 = 3.1, testValue4 = new IsEqualSubClass() { testSubValue1 = 4, testSubValue2 = 5, testSubValue3 = 6.0f } };
            IsEqualClass test2 = new IsEqualClass() { testValue1 = 1, testValue2 = "2", testValue3 = 3.1, testValue4 = new IsEqualSubClass() { testSubValue1 = 4, testSubValue2 = 5, testSubValue3 = 6.1f } };
            Assert.AreEqual(test1.IsEqual(test2), false);
        }

        [TestMethod]
        public void TestForCopyForSubClass()
        {
            IsEqualClass test1 = new IsEqualClass() { testValue1 = 1, testValue2 = "2", testValue3 = 3.1, testValue4 = new IsEqualSubClass() { testSubValue1 = 4, testSubValue2 = 5, testSubValue3 = 6.0f } };
            IsEqualClass test2 = test1.Clone();
            Assert.AreEqual(test1.IsEqual(test2), true);
        }

        [TestMethod]
        public void TestForMatrixNotEqualWithWidthAndHeight()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(5, 5, new double[25] { 
             1, 2, 3, 4, 5 , 
             2, 3, 4, 5, 6 , 
             3, 4, 5, 6, 7 , 
             4, 5, 6, 7, 8 ,
             5, 6, 7, 8, 9});
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 3, new double[9] { 
             3, 4, 5 , 
             4, 5, 6 , 
             5, 6, 7 });
            Assert.AreEqual(test1.IsEqual(test2), false);
        }

        [TestMethod]
        public void TestForMatrixNotEqualWithElement()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(5, 5, new double[25] { 
             1, 2, 3, 4, 5 , 
             2, 3, 4, 5, 6 , 
             3, 4, 5, 6, 7 , 
             4, 5, 6, 7, 8 ,
             5, 6, 7, 8, 9});
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(5, 5, new double[25] { 
             1, 2, 3, 4, 5 , 
             2, 3, 4, 5, 6 , 
             3, 4, 5, 6, 7 , 
             4, 5, 6, 7, 8 ,
             5, 6, 7, 8, 8});
            Assert.AreEqual(test1.IsEqual(test2), false);
        }

        [TestMethod]
        public void TestForMatrixEqual()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(5, 5, new double[25] { 
             1, 2, 3, 4, 5 , 
             2, 3, 4, 5, 6 , 
             3, 4, 5, 6, 7 , 
             4, 5, 6, 7, 8 ,
             5, 6, 7, 8, 9});
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(5, 5, new double[25] { 
             1, 2, 3, 4, 5 , 
             2, 3, 4, 5, 6 , 
             3, 4, 5, 6, 7 , 
             4, 5, 6, 7, 8 ,
             5, 6, 7, 8, 9});
            Assert.AreEqual(test1.IsEqual(test2), true);
        }

        [TestMethod]
        public void ConvolutionTest()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             1, 2, 3, 2 , 
             2, 3, 4, 3 , 
             3, 4, 5, 4 , 
             2, 3, 4, 3  });
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             1, 1, 1, 1 , 
             1, 1, 1, 1 , 
             1, 1, 2, 3 , 
             1, 1, 1, 3  });
            Assert.AreEqual(Common.Math.MatrixAdv.Convolution(test1, test2), 67);
        }

        [TestMethod]
        public void SubMatrixTestForSquare()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(5, 5, new double[25] { 
             1, 2, 3, 4, 5 , 
             2, 3, 4, 5, 6 , 
             3, 4, 5, 6, 7 , 
             4, 5, 6, 7, 8 ,
             5, 6, 7, 8, 9});
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 3, new double[9] { 
             3, 4, 5 , 
             4, 5, 6 , 
             5, 6, 7 });
            Assert.AreEqual(test1[1, 4, 1, 4].IsEqual(test2), true);
        }

        [TestMethod]
        public void SubMatrixTestForRectVertical()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(5, 5, new double[25] { 
             1, 2, 3, 4, 5 , 
             2, 3, 4, 5, 6 , 
             3, 4, 5, 6, 7 , 
             4, 5, 6, 7, 8 ,
             5, 6, 7, 8, 9});
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 2, new double[6] { 
             3, 4, 4 , 
             5, 5, 6 });
            Assert.AreEqual(test1[1, 4, 1, 3].IsEqual(test2), true);
        }

        [TestMethod]
        public void MatrixTestFotToString()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 2, new double[6] { 
             3, 4, 
             4 ,5, 
             5, 6 });
            Assert.AreEqual(test2.ToString(), "3.00000000 4.00000000 4.00000000 5.00000000 5.00000000 6.00000000 ");
        }

        [TestMethod]
        public void SubMatrixTestForRectHorizone()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(5, 5, new double[25] { 
             1, 2, 3, 4, 5 , 
             2, 3, 4, 5, 6 , 
             3, 4, 5, 6, 7 , 
             4, 5, 6, 7, 8 ,
             5, 6, 7, 8, 9});
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(2, 3, new double[6] { 
             3, 4, 5, 
             4 ,5, 6 });
            Assert.AreEqual(test1[1, 3, 1, 4].IsEqual(test2), true);
        }

        [TestMethod]
        public void GaussConvolutionTestFor8X8AllOneMatrix()
        {
            Common.Math.MatrixAdv gaussTest = SIFT.GetGaussTemplate(0.84089642, 7);
            Common.Math.MatrixAdv input = new Common.Math.MatrixAdv(8, 8, new double[] 
            {1,1,1,1,1,1,1,1,
             1,1,1,1,1,1,1,1,
             1,1,1,1,1,1,1,1,
             1,1,1,1,1,1,1,1,
             1,1,1,1,1,1,1,1,
             1,1,1,1,1,1,1,1,
             1,1,1,1,1,1,1,1,
             1,1,1,1,1,1,1,1}
            );
            Common.Math.MatrixAdv result = SIFT.GaussConvolutionResult(input, gaussTest);
            Assert.AreEqual(
            System.Math.Abs(result[3, 3] - 1) < 0.0001 &&
            System.Math.Abs(result[3, 4] - 1) < 0.0001 &&
            System.Math.Abs(result[4, 3] - 1) < 0.0001 &&
            System.Math.Abs(result[4, 4] - 1) < 0.0001, true);
        }

        [TestMethod]
        public void TestForMutipleNoneSqureMatrixAndResultIsSqure()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(2, 3, new double[6] { 
             3, 4, 5,
             4, 5, 6});
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 2, new double[6] { 
             3, 4, 
             4, 5, 
             5, 6 });
            Common.Math.MatrixAdv result = new Common.Math.MatrixAdv(2, 2, new double[4] { 
             50, 62, 
             62 ,77});
            Assert.AreEqual(test1 * test2 == result, true);
        }

        [TestMethod]
        public void TestForMutipleNoneSqureMatrixAndResultIsNotSqure()
        {
            Common.Math.MatrixAdv test1 = new Common.Math.MatrixAdv(1, 3, new double[3] { 
             3, 4, 5});
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 2, new double[6] { 
             3, 4, 
             4, 5, 
             5, 6 });
            Common.Math.MatrixAdv result = new Common.Math.MatrixAdv(1, 2, new double[2] { 
             50, 62});
            Assert.AreEqual(test1 * test2 == result, true);
        }

        [TestMethod]
        public void TestForGetDeterminantIn3X3MatrixAndResultIsZero()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 3, new double[9] { 
             3, 4, 5 , 
             4, 5, 6 , 
             5, 6, 7 });
            Assert.AreEqual(MatrixAdv.GetDeterminant(test2) == 0, true);
        }

        [TestMethod]
        public void TestForGetDeterminantIn3X3UpTriangleMatrix()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 3, new double[9] { 
             1, 1, 1 , 
             0, 1, 1 , 
             0, 0, 2 });
            Assert.AreEqual(MatrixAdv.GetDeterminant(test2) == 2, true);
        }

        [TestMethod]
        public void TestForGetDeterminantIn3X3NormalMatrixAndResultIsNotZero()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 3, new double[9] { 
             2, 1, 4 , 
             6, 3, 1 , 
             1, 2, 5 });
            Assert.AreEqual(MatrixAdv.GetDeterminant(test2) == 33, true);
        }

        [TestMethod]
        public void TestForGetDeterminantIn4X4MatrixAndResultIsZero()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             3, 4, 5, 6 , 
             4, 5, 6, 7 , 
             5, 6, 7, 8 ,
             6, 7, 8, 9});
            Assert.AreEqual(MatrixAdv.GetDeterminant(test2) == 0, true);
        }

        [TestMethod]
        public void TestForGetDeterminantIn4X4UpTriangleMatrix()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             1, 1, 1, 1 , 
             0, 1, 1, 1 , 
             0, 0, 2, 1 ,
             0, 0, 0, 3});
            Assert.AreEqual(MatrixAdv.GetDeterminant(test2) == 6, true);
        }

        [TestMethod]
        public void TestForPointsAtTheSameSideOfOnePanel()
        {
            double[] point1 = new double[3] { 0, 0, 0 };
            double[] point2 = new double[3] { 0.28, 0.25, 0.12 };
            List<double[]> panel = new List<double[]>();
            panel.Add(new double[3] { 0, 0, 1 });
            panel.Add(new double[3] { 0, 1, 0 });
            panel.Add(new double[3] { 1, 0, 0 });
            Assert.AreEqual(CalculatorFor3D.IsPointsAtTheSameSideOfPlane(point1, point2, panel), true);
        }

        [TestMethod]
        public void TestForPointsAtTheDifferentSideOfOnePanel()
        {
            double[] point1 = new double[3] { 0, 0, 0 };
            double[] point2 = new double[3] { 1, 1, 1 };
            List<double[]> panel = new List<double[]>();
            panel.Add(new double[3] { 0, 0, 1 });
            panel.Add(new double[3] { 0, 1, 0 });
            panel.Add(new double[3] { 1, 0, 0 });
            Assert.AreEqual(CalculatorFor3D.IsPointsAtTheSameSideOfPlane(point1, point2, panel), false);
        }

        [TestMethod]
        public void TestForGetDeterminantWithCofactorIn3X3NormalMatrixAndResultIsNotZero()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 3, new double[9] { 
             2, 1, 4 , 
             6, 3, 1 , 
             1, 2, 5 });
            double[] cofactors = new double[3] {13, -29, 9};
            Assert.AreEqual(MatrixAdv.GetDeterminantWithCofactor(test2, cofactors) == 33, true);
        }

        [TestMethod]
        public void TestForGetCofactorOfOneLine()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(3, 3, new double[9] { 
             2, 1, 4 , 
             6, 3, 1 , 
             1, 2, 5 });
            double[] result = MatrixAdv.GetCofactorOfOneLine(test2, 0);
            Assert.AreEqual(result[0] == 13 && result[1] == -29 && result[2] == 9, true);
        }

        [TestMethod]
        public void TestForFindTransactionBetweenOneLineAndAPlane()
        {
            Common.Math.MatrixAdv test2 = new Common.Math.MatrixAdv(4, 4, new double[16] { 
             1, 1, 1, 1 ,
             1, 1, 0, 0 , 
             1, 0, 1, 0 , 
             1, 0, 0, 1 });
            double[] cofactors = MatrixAdv.GetCofactorOfOneLine(test2, 0);
            double[] constant = new double[3] { 0, 0, 0 };
            double[] direction = new double[3] { 2, 2, 2 };
            double[] result = Common.Math.CalculatorFor3D.TransactionBetweenLineAndPlane(cofactors, constant, direction);
            Assert.AreEqual(System.Math.Round(result[0], 3) == 0.333 && System.Math.Round(result[1], 3) == 0.333 && System.Math.Round(result[2], 3) == 0.333, true);
        }

        [TestMethod]
        public void TestForPointInPlane()
        {
            double[] point = new double[3] { 0.333333, 0.333333, 0.333333 };
            double[] cofactors = new double[4] { 1, -1, -1, -1 };
            double[] planerange = new double[6] { 1, 0, 1, 0, 1, 0 };
            List<double[]> plane = new List<double[]>();
            plane.Add(new double[3] { 0, 0, 1 });
            plane.Add(new double[3] { 0, 1, 0 });
            plane.Add(new double[3] { 1, 0, 0 });
            Assert.AreEqual(CalculatorFor3D.IsPointInPlane(point, planerange, cofactors, plane, true), true);
        }

        [TestMethod]
        public void TestForPointNotInPlaneBecauseOutOfRange()
        {
            double[] point = new double[3] { 0.333333, 0.333333, 0.333333 };
            double[] cofactors = new double[4] { 1, -1, -1, -1 };
            double[] planerange = new double[6] { 1, 0, 1, 0, 1, 0 };
            List<double[]> plane = new List<double[]>();
            plane.Add(new double[3] { 0, 0, 1 });
            plane.Add(new double[3] { 0, 1, 0 });
            plane.Add(new double[3] { 1, 0, 0 });
            Assert.AreEqual(CalculatorFor3D.IsPointInPlane(point, planerange, cofactors, plane, true), true);
        }

        [TestMethod]
        public void GetTheVectorOfALine()
        {
            double[] pt1Data = new double[3] {0, 0, 0};
            double[] pt2Data = new double[3] {1, 1, 1};
            Line test = new Line(new Point3D(pt1Data, string.Empty), new Point3D(pt2Data, string.Empty), string.Empty);
            Assert.AreEqual(test.Vector[0] == -1 && test.Vector[1] == -1 && test.Vector[2] == -1, true);
        }

        [TestMethod]
        public void TestForTransferPointFrom3DToScreen2DWithScreenOfAnyAngle()
        {
            double[] pt1 = new double[] { 0.5, 0.5, 0.6 };
            double[] ptLD = new double[] { 0, 1, 0 };
            double screenHeight = 1;
            double[] result = new double[] {0.707, 0.400 };
            double[] calResult = PointTransfer.TransferPointFrom3DToScreen2DWithScreenOfAnyAngle(pt1, ptLD, screenHeight);
            Assert.AreEqual(System.Math.Round(calResult[0], 3) == result[0] && System.Math.Round(calResult[1], 3) == result[1], true);
        }

        [TestMethod]
        public void TestForRotatePointWithOneAxisZ()
        {
            double[] pt1 = new double[] { 0.707, 0.707, 0 };
            double[] result = new double[] { 0.5, 0.866, 0 };
            double[] calResult = CalculatorFor3D.RotatePointWithOneAxisOriginal(pt1, MoveType.Z, 15);
            Assert.AreEqual(System.Math.Round(calResult[0], 3) == result[0] && System.Math.Round(calResult[1], 3) == result[1] && System.Math.Round(calResult[2], 3) == result[2], true);
        }

        [TestMethod]
        public void TestForRotatePointWithOneAxisY()
        {
            double[] pt1 = new double[] { 0.707, 0, 0.707 };
            double[] result = new double[] { 0.866, 0, 0.5 };
            double[] calResult = CalculatorFor3D.RotatePointWithOneAxisOriginal(pt1, MoveType.Y, 15);
            Assert.AreEqual(System.Math.Round(calResult[0], 3) == result[0] && System.Math.Round(calResult[1], 3) == result[1] && System.Math.Round(calResult[2], 3) == result[2], true);
        }

        [TestMethod]
        public void TestForRotatePointWithOneAxisX()
        {
            double[] pt1 = new double[] { 0, 0.707, 0.707 };
            double[] result = new double[] { 0, 0.5, 0.866 };
            double[] calResult = CalculatorFor3D.RotatePointWithOneAxisOriginal(pt1, MoveType.X, 15);
            Assert.AreEqual(System.Math.Round(calResult[0], 3) == result[0] && System.Math.Round(calResult[1], 3) == result[1] && System.Math.Round(calResult[2], 3) == result[2], true);
        }

        [TestMethod]
        public void TestForRotatePointWithOneAxisAny()
        {
            double[] pt1 = new double[] { 1.707, 1.707, 0 };
            double[] result = new double[] { 1.5, 1.866, 0 };
            double[] original = new double[] {1, 1, 1};
            double[] calResult = CalculatorFor3D.RotatePointWithOneAxisAny(pt1, MoveType.Z, 15, original);
            Assert.AreEqual(System.Math.Round(calResult[0], 3) == result[0] && System.Math.Round(calResult[1], 3) == result[1] && System.Math.Round(calResult[2], 3) == result[2], true);
        }
    }
}
