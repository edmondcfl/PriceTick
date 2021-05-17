using System.Collections.ObjectModel;

namespace Client.Models
{
    public interface IPriceTickClient
    {
        string Ticker { get; set; }

        string StatusBarText { get; set; }
        
        ObservableCollection<PriceTickGridItem> PriceTickGridData { get; set; }
    }
}
