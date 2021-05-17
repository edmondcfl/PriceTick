using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Client.Models
{
    public class PriceTickClient: ObservableObject, INotifyPropertyChanged, IPriceTickClient
    {
        private string _ticker;
        private string _statusBarText = "Ready";

        public PriceTickClient()
        {
            PriceTickGridData = new ObservableCollection<PriceTickGridItem>();
        }

        public ObservableCollection<PriceTickGridItem> PriceTickGridData { get; set; }

        public string Ticker
        {
            get
            {
                return _ticker;
            }
            set
            {
                _ticker = value;
                OnPropertyChanged();
            }
        }

        public string StatusBarText
        {
            get
            {
                return _statusBarText;
            }
            set
            {
                _statusBarText = value;
                OnPropertyChanged();
            }
        }
    }
}
