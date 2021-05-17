using System;
using System.Collections.Generic;

namespace Service
{
    public interface IPriceService
    {
        void Subscribe(string ticker);

        void Unsubscribe(string ticket);

        Dictionary<DateTime, decimal> GetPriceHistory(string ticker);


        event EventHandler<PriceTickEventArgs> PriceTick;

        void Start();
    }
}
