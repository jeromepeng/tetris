using System.Windows;
using SIFTTest.ViewModel;
using SIFTTest.Util;

namespace SIFTTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ViewBUS viewBUS;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            viewBUS = ServiceLocator.Instance.GetInstance<ViewBUS>();
            var mainWindow = new MainWindow { DataContext = App.ViewBUSInstance.ResolveViewModel("Main") };
            mainWindow.Show();
            Current.MainWindow = mainWindow;
        }

        public static ViewBUS ViewBUSInstance
        {
            get
            {
                return viewBUS;
            }
        }
    }
}
