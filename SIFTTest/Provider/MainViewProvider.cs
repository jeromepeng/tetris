using SIFTTest.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFTTest.Provider
{
    public interface IMainViewProvider
    {
        IMainViewModel GetViewModel(string viewName);
    }

    [Export(typeof(IMainViewProvider))]
    public class MainViewProvider : IMainViewProvider
    {
        [ImportMany(typeof(IMainViewModel))]
        private IEnumerable<Lazy<IMainViewModel, IViewModelData>> mainViewModels;

        public IMainViewModel GetViewModel(string viewName)
        {
            var vm = mainViewModels.FirstOrDefault(p => p.Metadata.ViewName == viewName);

            if (null != vm)
            {
                return vm.Value;
            }

            return null;
        }
    }
}
