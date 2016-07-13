using Common;
using Common.Interface;
using Common.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataFor3D
{
    /// <summary>
    /// Class for cuboid, get the screen point of every point.
    /// </summary>
    public class Cuboid : I3DData
    {
        #region Private Member
        /// <summary>
        /// Restore the point of a cuboid.
        /// </summary>
        private List<Point3D> data = new List<Point3D>();

        /// <summary>
        /// All six planes.
        /// </summary>
        private List<Plane> planes = new List<Plane>();

        private string key = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pointData"></param>
        public Cuboid(List<Point3D> pointData, List<int[]> planePointIndex, string key)
        {
            this.key = key;
            if (pointData.Count == 8)
            {
                for (int i = 0; i < pointData.Count; i++)
                {
                    data.Add(new Point3D(new double[3] { pointData[i].Data[0], pointData[i].Data[1], pointData[i].Data[2] }, string.Empty));
                }
            }
            else
            {
                throw new Exception("The number of point should be 8!");
            }
            if (planePointIndex.Count == 6)
            {
                for (int i = 0; i < planePointIndex.Count; i++)
                {
                    List<Point3D> pointsOfPlane = new List<Point3D>();
                    for (int j = 0; j < planePointIndex[i].Length; j++)
                    {
                        pointsOfPlane.Add(data[planePointIndex[i][j]]);
                    }
                    planes.Add(new Plane(pointsOfPlane, string.Empty));
                }
            }
            else
            {
                throw new Exception("The number of plane should be 6!");
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Get all the point which should be shown in 3D.
        /// </summary>
        /// <param name="projectPoint"></param>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        private void GetPointWithHide(Point3D projectPoint)
        {
            for (int i = 0; i < data.Count; i++)
            {
                List<Point3D> transactionPoints = new List<Point3D>();
                Line projectLine = new Line(projectPoint, data[i], string.Empty);
                bool showPoint = true;
                for (int j = 0; j < planes.Count; j++)
                {
                    Point3D tempPt = planes[j].GetTransactionPointOfALine(projectLine);
                    if (planes[j].IsPointInPlane(tempPt, true))
                    {
                        transactionPoints.Add(tempPt);
                    }
                }
                for (int j = 0; j < transactionPoints.Count; j++)
                {
                    if (projectLine.IsPointInLine(transactionPoints[j]))
                    {
                        showPoint = false;
                        break;
                    }
                }
                data[i].Visiable = showPoint;
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get all the point in screen coordinate and no point will be hiden.
        /// </summary>
        /// <param name="projectPoint">The point of projection.</param>
        /// <param name="screenHeight">The height of screen.</param>
        /// <returns></returns>
        public List<Point3D> GetScreenPointWithoutHide(Point3D projectPoint, double screenHeight)
        {
            List<Point3D> result = new List<Point3D>();
            for (int i = 0; i < data.Count; i++)
            {
                Point3D point2D = new Point3D(PointTransfer.TransferPointFrom3DToScreen2D(data[i].Data, projectPoint.Data, screenHeight), string.Empty);
                result.Add(point2D);
            }
            return result;
        }
        /// <summary>
        /// Get all the point which are not hiden.
        /// </summary>
        /// <param name="projectPoint">The point of projection.</param>
        /// <param name="screenHeight">The height of screen.</param>
        /// <returns></returns>
        public List<Point3D> GetScreenPointWithHide(Point3D projectPoint, double screenHeight)
        {
            List<Point3D> result = new List<Point3D>();
            GetPointWithHide(projectPoint);
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Visiable)
                {
                    Point3D point2D = new Point3D(PointTransfer.TransferPointFrom3DToScreen2D(data[i].Data, projectPoint.Data, screenHeight), string.Empty);
                    result.Add(point2D);
                }
            }
            return result;
        }

        /// <summary>
        /// Get all the planes which can be seen.
        /// </summary>
        /// <param name="projectPoint"></param>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        public List<Plane> GetScreenPlaneWithHide(Point3D projectPoint, double screenHeight)
        {
            List<Plane> result = new List<Plane>();
            for (int i = 0; i < planes.Count; i++)
            {
                bool show = true;
                for (int j = 0; j < planes[i].DataPoint.Count; j++)
                {
                    show = show && planes[i].DataPoint[j].Visiable;
                }
                if (show)
                {
                    List<Point3D> points2D = new List<Point3D>();
                    for (int j = 0; j < planes[i].DataPoint.Count; j++)
                    {
                        points2D.Add(new Point3D(PointTransfer.TransferPointFrom3DToScreen2D(planes[i].DataPoint[j].Data, projectPoint.Data, screenHeight), string.Empty));
                    }
                    Plane plane2D = new Plane(points2D, string.Empty);
                    result.Add(plane2D);
                }
            }
            return result;
        }

        

        
        #endregion

        #region Interface Method
        /// <summary>
        /// Get info string.
        /// </summary>
        /// <returns></returns>
        public string InfoString()
        {
            string result = string.Empty;
            StringBuilder strB = new StringBuilder();
            for (int i = 0; i < data.Count; i++)
            {
                strB.AppendLine(data[i].InfoString());
            }
            result = strB.ToString();
            return result;
        }

        /// <summary>
        /// Get graphic of this data;
        /// </summary>
        /// <returns></returns>
        public GraphicType DataType()
        {
            return GraphicType.Cuboid;
        }

        /// <summary>
        /// Get data.
        /// </summary>
        public object GetData()
        {
            return data.ToArray();
        }

        /// <summary>
        /// Move this cuboid.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public void Move(double value, MoveType type)
        {
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Data[(int)type] += value;
            }
            for (int i = 0; i < planes.Count; i++)
            {
                planes[i].DataHasBeenUpdated();
            }
        }

        /// <summary>
        /// Rotate a data around the original.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="type"></param>
        /// <param name="?"></param>
        public void Rotate(double angle, MoveType type, double[] original)
        {
            for (int i = 0; i < data.Count; i++)
            {
                double[] rotatedData = CalculatorFor3D.RotatePointWithOneAxisAny(data[i].Data, type, angle, original);
                for (int j = 0; j < rotatedData.Length; j++)
                {
                    data[i].Data[j] = rotatedData[j];
                }
            }
            for (int i = 0; i < planes.Count; i++)
            {
                planes[i].DataHasBeenUpdated();
            }
        }

        /// <summary>
        /// Key of this object
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            return key;
        }

        /// <summary>
        /// Get all of the planes which can be seen in any screen.
        /// </summary>
        /// <param name="projectPoint"></param>
        /// <param name="screen"></param>
        /// <param name="screenHeight"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<I3DData> GetScreenPlaneWithHide(I3DData projectPoint, I3DData screen, double screenHeight, object parameter)
        {
            List<I3DData> result = new List<I3DData>();
            int leftBottomPointIndex = (int)parameter;
            if (projectPoint.DataType() == GraphicType.Point && screen.DataType() == GraphicType.Plane)
            {
                Plane screenPlane = screen as Plane;
                Point3D projectPoint3D = projectPoint as Point3D;
                GetPointWithHide(projectPoint3D);
                for (int i = 0; i < planes.Count; i++)
                {
                    bool show = true;
                    for (int j = 0; j < planes[i].DataPoint.Count; j++)
                    {
                        show = show && planes[i].DataPoint[j].Visiable;
                    }
                    if (show)
                    {
                        List<Point3D> points2D = new List<Point3D>();
                        for (int j = 0; j < planes[i].DataPoint.Count; j++)
                        {
                            Line projectLine = new Line(projectPoint3D, planes[i].DataPoint[j], string.Empty);
                            double[] transactionPoint = CalculatorFor3D.TransactionBetweenLineAndPlane(screenPlane.Cofactors, projectLine.Constant, projectLine.Vector);
                            points2D.Add(new Point3D(PointTransfer.TransferPointFrom3DToScreen2DWithScreenOfAnyAngle(transactionPoint, screenPlane.DataPoint[leftBottomPointIndex].Data, screenHeight), string.Empty));
                        }
                        Plane plane2D = new Plane(points2D, string.Empty);
                        result.Add(plane2D);
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
