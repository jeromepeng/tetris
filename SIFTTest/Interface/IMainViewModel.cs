using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFTTest.Interface
{
    public interface IMainViewModel
    {
        string Header { get; }

        object Tag { get; }

        string Name { get; }

        void ResolveMessage(object parameter, string info);
    }
}
