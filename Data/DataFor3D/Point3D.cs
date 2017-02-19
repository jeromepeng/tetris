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
    /// Class of Point
    /// </summary>
    public class Point3D : I3DData
    {
        #region Private Member
        /// <summary>
        /// Data of one point.
        /// </summary>
        private List<double[]> data = new List<double[]>();

        /// <summary>
        /// Key of this point.
        /// </summary>
        private string key = string.Empty;

        /// <summary>
        /// Is the poing visiable.
        /// </summary>
        private bool visiable = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of one point.
        /// </summary>
        /// <param name="xyz"></param>
        public Point3D(double[] xyz, string key)
        {
            if (xyz.Length == 3 || xyz.Length == 2)
            {
                data.Add(new double[3]);
                this.key = key;
                for (int i = 0; i < xyz.Length; i++)
                {
                    data[0][i] = xyz[i];
                }
            }
        }
        #endregion

        #region Override Method
        /// <summary>
        /// Override of equasl.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Point3D temp = obj as Point3D;
            bool result = false;
            result = temp.Data[0] == this.data[0][0] &&
                   temp.Data[1] == this.data[0][1] &&
                   temp.Data[2] == this.data[0][2];
            return result;
        }
        #endregion

        #region Public Member
        /// <summary>
        /// Get data.
        /// </summary>
        public double[] Data
        {
            get
            {
                return data[0];
            }
        }

        /// <summary>
        /// Get and set the visiable;
        /// </summary>
        public bool Visiable
        {
            set
            {
                this.visiable = value;
            }
            get
            {
                return visiable;
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
            StringBuilder strB = new StringBuilder();
            for (int i = 0 ; i < data[0].Length; i++)
            {
                strB.Append(System.Math.Round(data[0][i], 15).ToString("#0.000000000000000"));
                strB.Append(" : ");
            }
            strB.Append(visiable);
            result = strB.ToString();
            return result;
        }

        /// <summary>
        /// Get graphic of this data;
        /// </summary>
        /// <returns></returns>
        public GraphicType DataType()
        {
            return GraphicType.Point;
        }

        /// <summary>
        /// Get data.
        /// </summary>
        public object GetData() 
        { 
            return data[0];
        }

        /// <summary>
        /// Move a data.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public void Move(double value, MoveType type)
        {
            data[0][(int)type] += value;
        }

        /// <summary>
        /// Rotate a data around the original.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="type"></param>
        /// <param name="?"></param>
        public void Rotate(double angle, MoveType type, double[] original)
        {
            if (original[0] != data[0][0] || original[1] != data[0][1] || original[2] != data[0][2])
            {
                data[0] = CalculatorFor3D.RotatePointWithOneAxisAny(data[0], type, angle, original);
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
