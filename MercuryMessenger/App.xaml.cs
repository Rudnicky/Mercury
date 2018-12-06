using MercuryMessenger.Interfaces;
using MercuryMessenger.Utils;
using MercuryMessenger.ViewModels;
using System.Windows;
using Unity;

namespace MercuryMessenger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            UnityContainerSetup();
        }

        private void UnityContainerSetup()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISubscriber, Subscriber>();

            var mainWindowViewModel = container.Resolve<MainWindowViewModel>();
            var window = new MainWindow { DataContext = mainWindowViewModel };
            window.Show();
        }
    }
}
