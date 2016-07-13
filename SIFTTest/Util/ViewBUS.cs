using SIFTTest.Interface;
using SIFTTest.Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFTTest.Util
{
    [Export]
    public class ViewBUS
    {
        #region Private Member
        private IMainViewProvider viewProvider;
        private ViewBUS instance;
        #endregion

        #region  Constructor
        [ImportingConstructor]
        public ViewBUS(IMainViewProvider mainViewProvider)
        {
            viewProvider = mainViewProvider;
        }
        #endregion

        #region Public Memeber
        
        #endregion

        #region Public Method
        public IMainViewModel ResolveViewModel(string viewModel)
        {
            return viewProvider.GetViewModel(viewModel);
        }

        public void ResolveViewModelMessage(string viewModelName, object parameter, string info)
        {
            viewProvider.GetViewModel(viewModelName).ResolveMessage(parameter, info);
        }
        #endregion
    }
}
