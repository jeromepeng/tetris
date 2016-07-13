using SIFTTest.Interface;
using SIFTTest.Provider;
using SIFTTest.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIFTTest.ViewModel
{
    [Export(typeof(IMainViewModel))]
    [ExportMetadata("ViewName", "Main")]
    public class MainWindowViewModel : NotificationObject, IMainViewModel
    {
        private IMainViewModel currentView;
        private object tag;

        public MainWindowViewModel()
        {
            CurrentView = App.ViewBUSInstance.ResolveViewModel("SelectImage");
        }

        public IMainViewModel CurrentView
        {
            get
            {
                return currentView;
            }
            set
            {
                if (Equals(value, currentView))
                {
                    return;
                }
                currentView = value;
                OnPropertyChanged(() => this.CurrentView);
            }
        }

        public string Name
        {
            get
            {
                return "MainView";
            }
        }

        public object Tag
        {
            get
            {
                return tag;
            }
        }

        public string Header
        {
            get
            {
                return "Main Windows";
            }
        }

        /*private void DatabaseConnected(object sender, EventArgs eventArgs)
        {
            CurrentView = App.ViewBUS.ResolveViewModel("SelectImage");
        }*/

        #region Interface Function
        public void ResolveMessage(object parameter, string info)
        {
            switch (info)
            {
                case "NewViewModel":
                    {
                        CurrentView = (IMainViewModel)parameter;
                        break;
                    }
                case "PyramidImageViewer":
                    {
                        CurrentView = App.ViewBUSInstance.ResolveViewModel("PyramidImageViewer");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion
    }
}
