using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interface;
using Common;

namespace Data.DataContainer.TetrisDataGenerator
{
    /// <summary>
    /// Generator of tetris data.
    /// </summary>
    public class TetrisDataGenerator : IDataGenerator
    {
        #region Const Fields
        /// <summary>
        /// The length of infos.
        /// </summary>
        private const int InfoLength = 4;

        /// <summary>
        /// The flag of data begin.
        /// </summary>
        private const string BeginTag = "TETRIS";

        /// <summary>
        /// The flag of data end.
        /// </summary>
        private const string EndTag = "ENDTETRIS";

        /// <summary>
        /// The flag of additional data begin.
        /// </summary>
        private const string AdditionalParameterTagBegin = "<";

        /// <summary>
        /// The flag of additional data end.
        /// </summary>
        private const string AdditionalParameterTagEnd = ">";
        #endregion

        #region Private Property
        /// <summary>
        /// All tetris data.
        /// </summary>
        private DataDefine[] tetrisData;
        #endregion

        #region Public Method
        /// <summary>
        /// Initials game data.
        /// </summary>
        /// <param name="dataFiles"></param>
        public void InitialDatas(string dataFiles)
        {
            StreamReader sr = new StreamReader(dataFiles);
            string stringLine;
            bool fileBegin = false;
            int index = 0;
            try
            {
                while ((stringLine = sr.ReadLine()) != null)
                {
                    if (fileBegin)
                    {
                        if (!stringLine.ToUpper().Equals(EndTag))
                        {
                            tetrisData[index] = GetDataFromString(stringLine);
                            index++;
                        }
                        else
                        {
                            fileBegin = false;
                        }
                    }
                    if (!fileBegin && stringLine.ToUpper().Equals(BeginTag))
                    {
                        tetrisData = new DataDefine[System.Convert.ToInt32(sr.ReadLine())];
                        fileBegin = true && tetrisData.Length > 0;
                    }
                }
            }
            catch 
            {
                sr.Close();
                throw new Exception("Data is broken!");
            }
            sr.Close();
        }

        /// <summary>
        /// Gets one data.
        /// </summary>
        /// <returns></returns>
        public DataDefine GetOneData()
        {
            return null;
        }

        /// <summary>
        /// Gets datas.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataDefine[] GetDatas(int num)
        {
            return null;
        }

        /// <summary>
        /// Gets specified data.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataDefine GetSpecifiedData(int index)
        {
            DataDefine result = null;
            if (tetrisData != null && tetrisData.Length > index)
            {
                result = tetrisData[index];
            }
            return result;
        }

        /// <summary>
        /// Gets all datas.
        /// </summary>
        /// <returns></returns>
        public DataDefine[] GetAllDatas()
        {
            return tetrisData;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Gets data from one string.
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        private DataDefine GetDataFromString(string dataString)
        {
            DataDefine result;
            string[] dataFields = dataString.Split(';');
            if (dataFields.Length >= InfoLength)
            {
                int length = System.Convert.ToInt32(dataFields[0]);
                int height = System.Convert.ToInt32(dataFields[1]);
                int centX = System.Convert.ToInt32(dataFields[2]);
                int centY = System.Convert.ToInt32(dataFields[3]);
                DataPoint[] dataPoints = null;
                string additionalData = string.Empty;
                if (dataFields.Length >= InfoLength + length * height)
                {
                    dataPoints = new DataPoint[length * height];
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            int index = i * length + j;
                            dataPoints[index].X = j - centX;
                            dataPoints[index].Y = i - centY;
                            dataPoints[index].Value = System.Convert.ToInt32(dataFields[index + InfoLength]);
                        }
                    }
                }
                else
                {
                    result = null;
                }
                additionalData = dataFields[dataFields.Length - 1];
                if (string.IsNullOrEmpty(additionalData) || additionalData.Substring(0, 1) != AdditionalParameterTagBegin || additionalData.Substring(additionalData.Length - 1, 1) != AdditionalParameterTagEnd)
                {
                    additionalData = string.Empty;
                }
                else
                {
                    additionalData.Substring(1, additionalData.Length - 2);
                }
                result = new DataDefine(centX, centY, length, height, dataPoints, additionalData);
            }
            else
            {
                result = null;
            }
            return result;
        }
        #endregion
    }
}
