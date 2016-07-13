using Microsoft.Win32;
using SIFTTest.Interface;
using SIFTTest.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIFTTest.ViewModel
{
    [Export(typeof(IMainViewModel))]
    [ExportMetadata("ViewName", "SelectImage")]
    public class SelectImageViewModel : NotificationObject, IMainViewModel
    {
        #region Private Property
        private string imagePath;
        private string imagePathForMatching;
        private object tag;
        private string infoTips;
        #endregion

        #region Public Property
        public string Header
        {
            get
            {
                return "Select one image";
            }
        }

        public string Name
        {
            get
            {
                return "SelectImage";
            }
        }

        public object Tag
        {
            get
            {
                return tag;
            }
        }

        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                if (value == imagePath)
                {
                    return;
                }
                imagePath = value;
                OnPropertyChanged(() => this.ImagePath);
            }
        }

        public string ImagePathForMatching
        {
            get
            {
                return imagePathForMatching;
            }
            set
            {
                if (value == imagePathForMatching)
                {
                    return;
                }
                imagePathForMatching = value;
                OnPropertyChanged(() => this.ImagePathForMatching);
            }
        }

        public string InfoTips
        {
            get
            {
                return infoTips;
            }
            set
            {
                if (value == infoTips)
                {
                    return;
                }
                infoTips = value;
                OnPropertyChanged(() => this.InfoTips);
            }
        }

        public DelegateCommand<object> OpenCommand
        {
            get
            {
                return new DelegateCommand<object>(Open);
            }
        }

        public DelegateCommand<object> BrowseCommand
        {
            get
            {
                return new DelegateCommand<object>(Browse);
            }
        }

        public DelegateCommand<object> BrowseForMatchingCommand
        {
            get
            {
                return new DelegateCommand<object>(BrowseForMatching);
            }
        }
        #endregion

        #region Private Function
        private void Open(object pathBox)
        {
            InfoTips = string.Empty;
            tag = ImagePath + ";" + ImagePathForMatching;
            App.ViewBUSInstance.ResolveViewModelMessage("Main", App.ViewBUSInstance.ResolveViewModel("ImageViewer"), "NewViewModel");
        }

        private void Browse(object pathBox)
        {
            InfoTips = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image File|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            ofd.RestoreDirectory = true;
            if (File.Exists(((TextBox)pathBox).Text))
            {
                ofd.InitialDirectory = ((TextBox)pathBox).Text.Substring(0, ((TextBox)pathBox).Text.LastIndexOf("\\"));
            }
            else if (Directory.Exists(((TextBox)pathBox).Text))
            {
                ofd.InitialDirectory = ((TextBox)pathBox).Text.Trim('\\');
            }
            try
            {
                bool? dialogResult = ofd.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value)
                {
                    ImagePath = ofd.FileName;
                }
            }
            catch
            {
                InfoTips = "Path is wrong.";
            } 
        }

        private void BrowseForMatching(object pathForMatchingBox)
        {
            InfoTips = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image File|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            ofd.RestoreDirectory = true;
            if (File.Exists(((TextBox)pathForMatchingBox).Text))
            {
                ofd.InitialDirectory = ((TextBox)pathForMatchingBox).Text.Substring(0, ((TextBox)pathForMatchingBox).Text.LastIndexOf("\\"));
            }
            else if (Directory.Exists(((TextBox)pathForMatchingBox).Text))
            {
                ofd.InitialDirectory = ((TextBox)pathForMatchingBox).Text.Trim('\\');
            }
            try
            {
                bool? dialogResult = ofd.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value)
                {
                    ImagePathForMatching = ofd.FileName;
                }
            }
            catch
            {
                InfoTips = "Path for matching is wrong.";
            } 
        }
        #endregion

        #region Interface Function
        public void ResolveMessage(object parameter, string info)
        {
            //ToDo:
        }
        #endregion
    }
}
