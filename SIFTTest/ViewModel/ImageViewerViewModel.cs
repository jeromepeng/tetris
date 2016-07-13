using SIFTTest.Interface;
using SIFTTest.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SIFTTest.ViewModel
{
    [Export(typeof(IMainViewModel))]
    [ExportMetadata("ViewName", "ImageViewer")]
    public class ImageViewerViewModel : NotificationObject, IMainViewModel
    {
        #region Private Member
        private object tag;
        private BitmapImage image = null;
        private BitmapImage imageForMathcing = null;
        #endregion

        #region Public Member
        public string Header
        {
            get
            {
                return "Show Image.";
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

        public BitmapImage Image
        {
            get
            {
                if (image == null)
                {
                    InitialImage();
                }
                return image;
            }
            set
            {
                if (value == image)
                {
                    return;
                }
                image = value;
                OnPropertyChanged(() => this.Image);
            }
        }

        public BitmapImage ImageForMatching
        {
            get
            {
                if (imageForMathcing == null)
                {
                    InitialImage();
                }
                return imageForMathcing;
            }
            set
            {
                if (value == imageForMathcing)
                {
                    return;
                }
                imageForMathcing = value;
                OnPropertyChanged(() => this.ImageForMatching);
            }
        }

        public DelegateCommand GenerateCommand
        {
            get
            {
                return new DelegateCommand(GeneratePyramidImage);
            }
        }
        #endregion

        #region Private Function
        private void GeneratePyramidImage()
        {
            App.ViewBUSInstance.ResolveViewModelMessage("PyramidImageViewer", new BitmapImage[2] { Image, ImageForMatching }, "CaculatePyramidImage");
            App.ViewBUSInstance.ResolveViewModelMessage("Main", this, "PyramidImageViewer");
        }

        private void InitialImage()
        {
            string path = (string)App.ViewBUSInstance.ResolveViewModel("SelectImage").Tag;
            if (!string.IsNullOrEmpty(path))
            {
                string[] paths = path.Split(';');
                if (paths.Length == 2)
                {
                    Image = new BitmapImage(new Uri(paths[0], UriKind.Absolute));
                    
                    ImageForMatching = new BitmapImage(new Uri(paths[1], UriKind.Absolute));
                }
            }
        }
        #endregion

        #region Interface Function
        public void ResolveMessage(object parameter, string info)
        {

        }
        #endregion
    }
}
