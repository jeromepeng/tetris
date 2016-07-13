using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    // Define some base type used by this system;

    /// <summary>
    /// Color
    /// </summary>
    public class BColor
    {
        public int A;
        public int R;
        public int G;
        public int B;

        /// <summary>
        /// Spliter for the values.
        /// </summary>
        const char valueSpliter = ',';
        
        /// <summary>
        /// Convert the string to BColor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public BColor ToBColor(string data)
        {
            BColor result = null;
            string[] values = data.Split(valueSpliter);
            if (values.Length == 4)
            {
                result = new BColor() 
                { 
                    A = System.Convert.ToInt32(values[0]), 
                    R = System.Convert.ToInt32(values[1]), 
                    G = System.Convert.ToInt32(values[2]), 
                    B = System.Convert.ToInt32(values[3]) 
                };
            }
            else if (values.Length == 3)
            {
                result = new BColor()
                {
                    A = 255,
                    R = System.Convert.ToInt32(values[0]),
                    G = System.Convert.ToInt32(values[1]),
                    B = System.Convert.ToInt32(values[2])
                };
            }
            return result;
        }
    }

    /// <summary>
    /// Define const property.
    /// </summary>
    public class ConstProperty
    {
        /// <summary>
        /// Spliter for every data.
        /// </summary>
        public const char SpliterForData = '!';

        /// <summary>
        /// Spliter for key and value.
        /// </summary>
        public const char SpliterForKeyAndValue = ':';
    }

    /// <summary>
    /// Type of game.
    /// </summary>
    public enum GameType
    {
        Tetris,
        Tank
    }

    /// <summary>
    /// Action type.
    /// </summary>
    public enum ActionType
    {
        Down,
        Up,
        Left,
        Right,
        Fire
    }

    /// <summary>
    /// Define my game data.
    /// </summary>
    public class DataDefine : IDataDefine
    {
        #region Private Property
        /// <summary>
        /// X of the data's center.
        /// </summary>
        private int centX;

        /// <summary>
        /// Y of the data's center
        /// </summary>
        private int centY;

        /// <summary>
        /// Length of the data.
        /// </summary>
        private int length;


        /// <summary>
        /// Height of the data.
        /// </summary>
        private int height;

        /// <summary>
        /// All the data value in this data. 
        /// </summary>
        private DataPoint[] datas;

        /// <summary>
        /// Current X.
        /// </summary>
        private int currentX;

        /// <summary>
        /// Current Y.
        /// </summary>
        private int currentY;

        /// <summary>
        /// Additional data.
        /// </summary>
        private string additionalData;

        /// <summary>
        /// The additional datas which have been transformed.
        /// </summary>
        private Dictionary<string, object> transformedAdditionalData = new Dictionary<string, object>();
        #endregion

        #region Public Property
        /// <summary>
        /// Gets X of the data's center.
        /// </summary>
        public int CentX
        {
            get
            {
                return centX;
            }
        }

        /// <summary>
        /// Gets Y of the data's center
        /// </summary>
        public int CentY
        {
            get
            {
                return centY;
            }
        }

        /// <summary>
        /// Gets the length of the data.
        /// </summary>
        public int Length
        {
            get
            {
                return length;
            }
        }

        /// <summary>
        /// Gets the height of the data.
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
        }

        /// <summary>
        /// Gets the data value in this data. 
        /// </summary>
        public DataPoint[] Datas
        {
            get
            {
                return datas;
            }
        }

        /// <summary>
        /// Current X.
        /// </summary>
        public int CurrentX
        {
            get
            {
                return currentX;
            }
            set
            {
                currentX = value;
            }
        }

        /// <summary>
        /// Current Y.
        /// </summary>
        public int CurrentY
        {
            get
            {
                return currentY;
            }
            set
            {
                currentY = value;
            }
        }
        #endregion

        #region Construcotr
        /// <summary>
        /// Constructor for datadefine.
        /// </summary>
        /// <param name="centX"></param>
        /// <param name="centY"></param>
        /// <param name="height"></param>
        /// <param name="length"></param>
        /// <param name="dataPoints"></param>
        /// <param name="additionalData"></param>
        public DataDefine(int centX, int centY, int height, int length, DataPoint[] dataPoints, string additionalData)
        {
            this.centX = centX;
            this.centY = centY;
            this.datas = dataPoints;
            this.length = length;
            this.height = height;
            this.additionalData = additionalData;
            transformedAdditionalData = TransformedData(this.additionalData);
        }
        #endregion

        #region Interface member
        /// <summary>
        /// Get additional data.
        /// </summary>
        /// <param name="dataTypes"></param>
        /// <param name="dataObject"></param>
        public object GetAdditionalData(string name)
        {
            object result = transformedAdditionalData.ContainsKey(name) ? transformedAdditionalData[name] : null;
            return result;
        }
        #endregion

        #region Private function
        /// <summary>
        /// Transforme the addtional data string to object.
        /// </summary>
        /// <param name="additionalDataString"></param>
        /// <returns></returns>
        private Dictionary<string, object> TransformedData(string additionalDataString)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(additionalData))
            {
                string[] oriData = additionalData.Split(ConstProperty.SpliterForData);
                for (int i = 0; i < oriData.Length; i++)
                {
                    string[] nameAndTypeAndValue = oriData[i].Split(ConstProperty.SpliterForKeyAndValue);
                    if (nameAndTypeAndValue.Length == 3)
                    {
                        switch (nameAndTypeAndValue[1])
                        {
                            case "BColor":
                                {
                                    result.Add(nameAndTypeAndValue[0], BColor.ToBColor(nameAndTypeAndValue[2]));
                                    break;
                                }
                            case "bool":
                                {
                                    result.Add(nameAndTypeAndValue[0], System.Convert.ToBoolean(nameAndTypeAndValue[2]));
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                }
            }
            return result;
        }
        #endregion
    }

    /// <summary>
    /// Data define for matrix.
    /// </summary>
    public class MatrixData
    {
        #region public property
        public double[] Data;
        public int LineNum;
        public int ColNum;
        #endregion
    }

    /// <summary>
    /// Enum for move type.
    /// </summary>
    public enum MoveType
    {
        X = 0,
        Y = 1,
        Z = 2
    }

    /// <summary>
    /// Type of graphic.
    /// </summary>
    public enum GraphicType
    {
        Point = 0,
        Line = 1,
        Plane = 2,
        Cuboid = 3
    }
}
