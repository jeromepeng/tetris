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
    /// Class of line.
    /// </summary>
    public class Line : I3DData
    {
        #region Private member
        /// <summary>
        /// First point of the line.
        /// </summary>
        private Point3D point1;

        /// <summary>
        /// Second point of the line.
        /// </summary>
        private Point3D point2;

        /// <summary>
        /// The direction vector of this line.
        /// </summary>
        private double[] vector = new double[3] {0, 0, 0};

        /// <summary>
        /// Key of this line.
        /// </summary>
        private string key = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of a line.
        /// </summary>
        public Line(Point3D pointData1, Point3D pointData2, string key)
        {
            this.key = key;
            point1 = new Point3D(pointData1.Data, string.Empty);
            point2 = new Point3D(pointData2.Data, string.Empty);
            for (int i = 0; i < 3; i++)
            {
                vector[i] = point1.Data[i] - point2.Data[i];
            }
        }
        #endregion

        #region Public Member
        /// <summary>
        /// Get the direction vector of a line.
        /// </summary>
        public double[] Vector
        {
            get
            {
                return vector;
            }
        }

        /// <summary>
        /// Get the constatn of a line.
        /// </summary>
        public double[] Constant
        {
            get
            {
                return point2.Data;
            }
        }
        #endregion

        #region Public Functoin
        /// <summary>
        /// If one point is in the line.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool IsPointInLine(Point3D pt)
        {
            return CalculatorFor3D.IsNumberBetweenTwoNumber(pt.Data[0], point1.Data[0], point2.Data[0]) &&
                CalculatorFor3D.IsNumberBetweenTwoNumber(pt.Data[1], point1.Data[1], point2.Data[1]) &&
                CalculatorFor3D.IsNumberBetweenTwoNumber(pt.Data[2], point1.Data[2], point2.Data[2]);
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
            return GraphicType.Line;
        }

        /// <summary>
        /// Get data.
        /// </summary>
        public object GetData()
        {
            return null;
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
            //Do nothing.
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
        #endregion
    }
}
