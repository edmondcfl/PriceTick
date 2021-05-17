using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Client.Models
{
    public interface IPriceHistory
    {
        string PriceHistoryTitleText { get; set; }

        void Update(string ticker, Dictionary<DateTime, decimal> priceHistory);

        ObservableCollection<PriceHistoryGridItem> PriceHistoryGridData { get; set; }

    }
}
