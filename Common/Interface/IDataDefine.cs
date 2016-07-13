using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{

    public interface IDataDefine
    {
        /// <summary>
        /// Get additional data.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetAdditionalData(string name);
    }
}
