using System.Windows;
using Client.ViewModels;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IPriceTickClientVM priceTickClientVM)
        {
            InitializeComponent();
            DataContext = priceTickClientVM;
            Closing += priceTickClientVM.OnWindowClosing;
        }
    }
}
