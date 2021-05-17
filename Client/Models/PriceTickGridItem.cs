using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.Models
{
    public class PriceTickGridItem : INotifyPropertyChanged
    {
        private string ticker;
        private decimal? price;
        public event PropertyChangedEventHandler PropertyChanged;

        public PriceTickGridItem(string ticker)
        {
            Ticker = ticker;
        }

        public string Ticker
        {
            get { return ticker; }
            set
            {
                ticker = value;
                OnPropertyChanged();
            }
        }

        public decimal? Price
        {
            get { return price; }
            set
            {
                if (price == null || price == value)
                {
                    Direction = TickDirection.Flat;
                }
                else
                {
                    Direction = value > price ? TickDirection.Up : TickDirection.Down;
                }
                price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Direction));
            }
        }

        public TickDirection Direction { get; private set; }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
