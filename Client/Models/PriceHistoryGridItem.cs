using System;
using System.ComponentModel;

namespace Client.Models
{
    public class PriceHistoryGridItem : ObservableObject, INotifyPropertyChanged
    {
        private DateTime timestamp;
        private decimal price;

        public PriceHistoryGridItem(DateTime timestamp, decimal price)
        {
            Timestamp = timestamp;
            Price = price;
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
            set
            {
                timestamp = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
    }
}
