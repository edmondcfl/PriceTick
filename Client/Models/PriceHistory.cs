using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Client.Models
{
    public class PriceHistory : ObservableObject, INotifyPropertyChanged, IPriceHistory
    {
        private string _priceHistoryTitleText = "";

        public PriceHistory()
        {
            PriceHistoryGridData = new ObservableCollection<PriceHistoryGridItem>();
        }

        public ObservableCollection<PriceHistoryGridItem> PriceHistoryGridData { get; set; }

        public string PriceHistoryTitleText
        {
            get
            {
                return _priceHistoryTitleText;
            }
            set
            {
                _priceHistoryTitleText = value;
                OnPropertyChanged();
            }
        }

        public void Update(string ticker, Dictionary<DateTime, decimal> priceHistory)
        {
            PriceHistoryTitleText = $"Price History of {ticker}";
            InitPriceHistoryGridData(priceHistory);
        }

        private void InitPriceHistoryGridData(Dictionary<DateTime, decimal> priceHistory)
        {
            PriceHistoryGridData.Clear();
            foreach (var kvp in priceHistory.OrderBy(x => x.Key))
            {
                PriceHistoryGridData.Add(new PriceHistoryGridItem(kvp.Key, kvp.Value));
            }
            OnPropertyChanged(nameof(PriceHistoryGridData));
        }
    }
}
