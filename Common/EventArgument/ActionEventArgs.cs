using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventArgument
{
    public class ActionEventArgs : EventArgs
    {
        /// <summary>
        /// Adding parameters for this action arg.
        /// </summary>
        public string Parameter1 { get; set; }

        /// <summary>
        /// Adding parameters for this action arg.
        /// </summary>
        public object Parameter2 { get; set; }

        /// <summary>
        /// Adding parameters for this action arg.
        /// </summary>
        public int Parameter3 { get; set; }
    }
}
