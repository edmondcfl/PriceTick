namespace Service
{
    public class RandomPriceFieldDefinition
    {
        public RandomPriceFieldDefinition(string ticker, decimal low, decimal high)
        {
            Ticker = ticker;
            Low = low;
            High = high;
            LowInPenny = (int)(low * 100);
            HighInPenny = (int)(high * 100);
        }

        public string Ticker { get; private set; }
        public decimal Low { get; private set; }
        public decimal High { get; private set; }
        public int LowInPenny { get; }
        public int HighInPenny { get; }
    }
}
