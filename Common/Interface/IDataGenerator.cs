using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IDataGenerator
    {
        /// <summary>
        /// Initials game data.
        /// </summary>
        /// <param name="dataFiles"></param>
        void InitialDatas(string dataFiles);

        /// <summary>
        /// Gets one data.
        /// </summary>
        /// <returns></returns>
        DataDefine GetOneData();

        /// <summary>
        /// Gets datas.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        DataDefine[] GetDatas(int num);

        /// <summary>
        /// Gets specified data.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        DataDefine GetSpecifiedData(int index);

        /// <summary>
        /// Gets all datas.
        /// </summary>
        /// <returns></returns>
        DataDefine[] GetAllDatas();
    }
}
