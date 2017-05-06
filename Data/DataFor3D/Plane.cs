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
    /// Class for plane
    /// </summary>
    public class Plane : I3DData
    {
        #region Priavete Member
        /// <summary>
        /// Points of one plane.
        /// </summary>
        private List<Point3D> dataPoint = new List<Point3D>();

        /// <summary>
        /// Cofactors of this plane.
        /// </summary>
        private double[] cofactors;

        /// <summary>
        /// Data of this plane.
        /// </summary>
        private List<double[]> data = new List<double[]>();

        /// <summary>
        /// Range of the plane.
        /// </summary>
        private double[] planeRange = new double[6];

        /// <summary>
        /// Key of this plane.
        /// </summary>
        private string key = string.Empty;

        /// <summary>
        /// Restore the DataStyle.
        /// </summary>
        private DataStyle style;

        /// <summary>
        /// Gravity center of this plane.
        /// </summary>
        private Point3D gravityCenter = new Point3D(new double[] { 0, 0, 0}, string.Empty);
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="points"></param>
        public Plane(List<Point3D> points, string key, DataStyle style)
        {
            this.key = key;
            this.style = style;
            if (points.Count > 2)
            {
                planeRange[0] = planeRange[1] = points[0].Data[0];
                planeRange[2] = planeRange[3] = points[0].Data[1];
                planeRange[4] = planeRange[5] = points[0].Data[2];
                for (int i = 0; i < points.Count; i++)
                {
                    dataPoint.Add(points[i]);
                    data.Add(new double[3] { points[i].Data[0], points[i].Data[1], points[i].Data[2] });
                    planeRange[0] = planeRange[0] < points[i].Data[0] ? points[i].Data[0] : planeRange[0];
                    planeRange[2] = planeRange[2] < points[i].Data[1] ? points[i].Data[1] : planeRange[2];
                    planeRange[4] = planeRange[4] < points[i].Data[2] ? points[i].Data[2] : planeRange[4];
                    planeRange[1] = planeRange[1] > points[i].Data[0] ? points[i].Data[0] : planeRange[1];
                    planeRange[3] = planeRange[3] > points[i].Data[1] ? points[i].Data[1] : planeRange[3];
                    planeRange[5] = planeRange[5] > points[i].Data[2] ? points[i].Data[2] : planeRange[5];
                }
                cofactors = GetCofactors();
                gravityCenter = GetGravityCenter();
            }
            else
            {
                throw new Exception("The number of point should be bigger than 3!");
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get cofactors of this plane.
        /// </summary>
        /// <returns></returns>
        public double[] GetCofactors()
        {
            double[] result = new double[4];
            MatrixAdv calculateMatrix = new MatrixAdv(4, 4, new double[] {
            1, 1, 1, 1,
            1, dataPoint[0].Data[0], dataPoint[0].Data[1], dataPoint[0].Data[2],
            1, dataPoint[1].Data[0], dataPoint[1].Data[1], dataPoint[1].Data[2],
            1, dataPoint[2].Data[0], dataPoint[2].Data[1], dataPoint[2].Data[2]});
            result = MatrixAdv.GetCofactorOfOneLine(calculateMatrix, 0);
            return result;
        }

        /// <summary>
        /// Get the gravity center of this shape.
        /// </summary>
        /// <returns></returns>
        public Point3D GetGravityCenter()
        {
            Point3D result = new Point3D(new double[] { 0, 0, 0 }, string.Empty);
            for (int i = 0; i < dataPoint.Count; i++)
            {
                result.Data[0] += dataPoint[i].Data[0];
                result.Data[1] += dataPoint[i].Data[1];
                result.Data[2] += dataPoint[i].Data[2];
            }
            result.Data[0] = result.Data[0] / dataPoint.Count;
            result.Data[1] = result.Data[1] / dataPoint.Count;
            result.Data[2] = result.Data[2] / dataPoint.Count;
            return result;
        }

        /// <summary>
        /// Return ture if pt is in the area of the plane.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool IsPointInPlane(Point3D pt, bool isTransaction)
        {
            return CalculatorFor3D.IsPointInPlane(pt.Data, planeRange, cofactors, data, isTransaction);
        }

        /// <summary>
        /// Get the transaction point between line and this plane.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public Point3D GetTransactionPointOfALine(Line line)
        {
            double[] transactionPoint = CalculatorFor3D.TransactionBetweenLineAndPlane(cofactors, line.Constant, line.Vector);
            Point3D result = new Point3D(transactionPoint, string.Empty);
            return result;
        }

        /// <summary>
        /// Is the point one of this plane's points.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool IsPointBelongToMe(Point3D pt)
        {
            bool result = false;
            for (int i = 0; i < dataPoint.Count; i++)
            {
                if (dataPoint[i].Equals(pt))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Do something while data has been changed.
        /// </summary>
        public void DataHasBeenUpdated()
        {
            planeRange[0] = planeRange[1] = dataPoint[0].Data[0];
            planeRange[2] = planeRange[3] = dataPoint[0].Data[1];
            planeRange[4] = planeRange[5] = dataPoint[0].Data[2];
            data.Clear();
            for (int i = 0; i < dataPoint.Count; i++)
            {
                data.Add(new double[3] { dataPoint[i].Data[0], dataPoint[i].Data[1], dataPoint[i].Data[2] });
                planeRange[0] = planeRange[0] < dataPoint[i].Data[0] ? dataPoint[i].Data[0] : planeRange[0];
                planeRange[2] = planeRange[2] < dataPoint[i].Data[1] ? dataPoint[i].Data[1] : planeRange[2];
                planeRange[4] = planeRange[4] < dataPoint[i].Data[2] ? dataPoint[i].Data[2] : planeRange[4];
                planeRange[1] = planeRange[1] > dataPoint[i].Data[0] ? dataPoint[i].Data[0] : planeRange[1];
                planeRange[3] = planeRange[3] > dataPoint[i].Data[1] ? dataPoint[i].Data[1] : planeRange[3];
                planeRange[5] = planeRange[5] > dataPoint[i].Data[2] ? dataPoint[i].Data[2] : planeRange[5];
            }
            cofactors = GetCofactors();
        }
        #endregion

        #region Public Member
        /// <summary>
        /// Get cofactors.
        /// </summary>
        public double[] Cofactors
        {
            get
            {
                return cofactors;
            }
        }

        /// <summary>
        /// Get all the points of this plane.
        /// </summary>
        public List<Point3D> DataPoint
        {
            get
            {
                return dataPoint;
            }
        }

        /// <summary>
        /// Gets the style of this plane.
        /// </summary>
        public DataStyle Style
        {
            get
            {
                return style;
            }
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
            return result;
        }

        /// <summary>
        /// Get graphic of this data;
        /// </summary>
        /// <returns></returns>
        public GraphicType DataType()
        {
            return GraphicType.Plane;
        }

        /// <summary>
        /// Get data.
        /// </summary>
        public object GetData()
        {
            return data;
        }

        /// <summary>
        /// The gravity center of this shape.
        /// </summary>
        public I3DData GravityCenter
        {
            get
            {
                return gravityCenter;
            }
        }

        /// <summary>
        /// Move this cuboid.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public void Move(double value, MoveType type)
        {
            //Do nothing.
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
                data[i] = CalculatorFor3D.RotatePointWithOneAxisAny(data[i], type, angle, original);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataPoint[i].Data[j] = data[i][j];
                }
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
        /// <returns></returns>
        public List<I3DData> GetScreenPlaneWithHide(I3DData projectPoint, I3DData screen, double screenHeight, object parameter)
        {
            List<I3DData> result = new List<I3DData>();
            return result;
        }

        /// <summary>
        /// Get all of the planes which can be seen in any screen.
        /// And all the planes which hide behind specific object won't be return.
        /// </summary>
        /// <param name="projectPoint"></param>
        /// <param name="screen"></param>
        /// <param name="screenHeight"></param>
        /// <param name="parameter"></param>
        /// <param name="specificObject"></param>
        /// <returns></returns>
        public List<I3DData> GetScreenPlaneWihtHideOnSpecificObjects(I3DData projectPoint, I3DData screen, double screenHeight, object parameter, I3DData specificObject)
        {
            List<I3DData> result = new List<I3DData>();
            return result;
        }

        /// <summary>
        /// Is the point hide behind this object.
        /// </summary>
        /// <param name="projectPoint"></param>
        /// <param name="screen"></param>
        /// <param name="screenHeight"></param>
        /// <param name="parameter"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsPointHideBehindThisObject(I3DData projectPoint, I3DData screen, double screenHeight, object parameter, I3DData point)
        {
            bool result = false;
            return result;
        }
        #endregion
    }
}
