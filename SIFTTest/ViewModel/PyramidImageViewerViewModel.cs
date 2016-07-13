using Common.Tool;
using SIFTTest.Interface;
using SIFTTest.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SIFTTest.ViewModel
{
    [Export(typeof(IMainViewModel))]
    [ExportMetadata("ViewName", "PyramidImageViewer")]
    public class PyramidImageViewerViewModel : NotificationObject, IMainViewModel
    {
        #region Private Member
        private object tag;
        private BitmapImage image = null;
        private BitmapImage imageForMathcing = null;
        private double completedPercentage = 0;
        private bool stopProgressBar = false;
        #endregion

        #region Public Member
        public string Name
        {
            get
            {
                return "PyramidImageViewer";
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
                return "Pyramid Image Viewer";
            }
        }

        public BitmapImage Image
        {
            get
            {
                if (image == null)
                {
                    //InitialImage();
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

        public double CompletedPercentage
        {
            get
            {
                return completedPercentage;
            }
            set
            {
                if (value == completedPercentage)
                {
                    return;
                }
                completedPercentage = value;
                OnPropertyChanged(() => this.CompletedPercentage);
            }
        }

        public BitmapImage ImageForMatching
        {
            get
            {
                if (imageForMathcing == null)
                {
                    //InitialImage();
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

        
        #endregion

        #region Private Method
        private void ProcessImage(object parameter)
        {
            BitmapImage[] bitmaps = parameter as BitmapImage[];
            if (bitmaps.Length == 2)
            {
                Image = PyramidImageTool.GeneratePyramidImage(bitmaps[0], 4, 4, 1.5, 7);
                ImageForMatching = PyramidImageTool.GeneratePyramidImage(bitmaps[1], 4, 4, 1.5, 7);
                stopProgressBar = true;
            }
        }

        private void ProgressBar()
        {
            while (!stopProgressBar)
            {
                CompletedPercentage = PyramidImageTool.CompletedPercentage;
            }
        }
        
        #endregion

        #region Public Method
        public void ResolveMessage(object parameter, string info)
        {
            switch (info)
            {
                case "CaculatePyramidImage":
                    {
                        Thread processThread = new Thread(new ParameterizedThreadStart(ProcessImage));
                        processThread.Start(parameter);
                        Thread progressBar = new Thread(new ThreadStart(ProgressBar));
                        progressBar.Start();
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
