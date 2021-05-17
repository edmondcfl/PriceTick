using System;

namespace Service
{
    public class PriceTickEventArgs : EventArgs
    {
        public PriceTickEventArgs(string ticker, decimal value)
        {
            Ticker = ticker;
            Value = value;
        }
        public string Ticker { get; set; }
        public decimal Value { get; set; }
    }
}
