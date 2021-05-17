using Client.Models;

namespace Client.ViewModels
{
    public class PriceHistoryVM: IPriceHistoryVM
    {
        public PriceHistoryVM(IPriceHistory priceHistory)
        {
            PriceHistory = priceHistory;
        }

        public IPriceHistory PriceHistory { get; private set; }
    }
}
