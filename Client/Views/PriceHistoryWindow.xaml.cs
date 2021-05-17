using System.Windows;
using System.ComponentModel;
using Client.ViewModels;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for PriceHistoryWindow.xaml
    /// </summary>
    public partial class PriceHistoryWindow : Window, IPriceHistoryWindow
    {
        public PriceHistoryWindow(IPriceHistoryVM priceHistoryVM)
        {
            InitializeComponent();
            DataContext = PriceHistoryVM = priceHistoryVM;
        }

        public IPriceHistoryVM PriceHistoryVM { get; private set; }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void PriceHistoryWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
