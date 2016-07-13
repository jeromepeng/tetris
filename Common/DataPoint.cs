using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   
    /// <summary>
    /// Data point.
    /// </summary>
    public class DataPoint : BaseOperatoin<DataPoint>
    {
        public int X {get; set;}
        public int Y {get; set;}
        public int Value {get; set;}
    }
}
