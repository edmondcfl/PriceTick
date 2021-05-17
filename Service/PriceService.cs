using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Service
{
    public class PriceService : IPriceService
    {
        private readonly ILogger<PriceService> _logger;
        private readonly List<RandomPriceFieldDefinition> _priceFields;
        private readonly int _tickPeriodInMilliSecond;
        private readonly Random _rand;
        private Dictionary<string, Dictionary<DateTime, decimal>> _priceHistory;
        private HashSet<string> _subscribedTickers;
        private static object lockObj = new object();
        public event EventHandler<PriceTickEventArgs> PriceTick;

        public PriceService(ILogger<PriceService> logger)
        {
            _logger = logger;

            lock (lockObj)
            {
                _tickPeriodInMilliSecond = 1000;
                _rand = new Random();

                _priceFields = new List<RandomPriceFieldDefinition>();
                _priceFields.Add(new RandomPriceFieldDefinition("Stock1", 240, 270));
                _priceFields.Add(new RandomPriceFieldDefinition("Stock2", 180, 210));

                _priceHistory = new Dictionary<string, Dictionary<DateTime, decimal>>();
                foreach (var field in _priceFields)
                {
                    _priceHistory[field.Ticker] = new Dictionary<DateTime, decimal>();
                }

                _subscribedTickers = new HashSet<string>();
            }
        }

        public void Start()
        {
            while (true)
            {
                var task = Task.Run(async delegate
                {
                    await Task.Delay(_tickPeriodInMilliSecond);
                    FeedPrices();
                });
                task.Wait();
            }
        }

        public void Subscribe(string ticker)
        {
            lock (lockObj)
            {
                if (!_subscribedTickers.Contains(ticker))
                {
                    _subscribedTickers.Add(ticker);
                }
            }

            var className = this.GetType().Name;
            var methodName = MethodBase.GetCurrentMethod().Name;
            _logger.LogInformation($"{className}.{methodName}: Subscribed for {ticker}.");
        }
        public void Unsubscribe(string ticker)
        {
            lock (lockObj)
            {
                if (_subscribedTickers.Contains(ticker))
                {
                    _subscribedTickers.Remove(ticker);
                }
            }

            var className = this.GetType().Name;
            var methodName = MethodBase.GetCurrentMethod().Name;
            _logger.LogInformation($"{className}.{methodName}: Unsubscribed {ticker}.");
        }

        protected void FeedPrices()
        {
            foreach (var field in _priceFields)
            {
                var timestamp = DateTime.Now;
                var nextTick = _rand.Next(field.LowInPenny, field.HighInPenny + 1) / 100m;
                lock (lockObj)
                {
                    _priceHistory[field.Ticker][timestamp] = nextTick;
                }
                var className = this.GetType().Name;
                var methodName = MethodBase.GetCurrentMethod().Name;
                _logger.LogDebug($"{className}.{methodName}: ticker={field.Ticker}, price={nextTick}");

                if (_subscribedTickers.Contains(field.Ticker))
                {
                    PriceTick?.Invoke(this, new PriceTickEventArgs(field.Ticker, nextTick));
                }
            }
        }

        public Dictionary<DateTime, decimal> GetPriceHistory(string ticker)
        {
            lock (lockObj)
            {
                if (!_priceHistory.ContainsKey(ticker))
                    return null;

                return _priceHistory[ticker];
            }
        }

    }
}
