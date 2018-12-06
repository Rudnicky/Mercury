using MercuryMessenger.ViewModels;
using System.Windows.Controls;

namespace MercuryMessenger.Views
{
    /// <summary>
    /// Interaction logic for PrimaryUserControl.xaml
    /// </summary>
    public partial class PrimaryUserControl : UserControl
    {
        public PrimaryUserControl()
        {
            InitializeComponent();

            this.DataContext = new PrimaryViewModel();
        }
    }
}
