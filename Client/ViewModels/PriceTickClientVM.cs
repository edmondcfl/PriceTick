using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Client.Models;
using Client.Views;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Service;

namespace Client.ViewModels
{
    public class PriceTickClientVM: IPriceTickClientVM
    {
        private IPriceService _priceService;
        private ILogger<PriceTickClientVM> _logger;
        private IPriceHistoryWindow _priceHistoryWindow;

        public PriceTickClientVM(ILogger<PriceTickClientVM> logger, IPriceService priceService, IPriceTickClient client, IPriceHistoryWindow priceHistoryWindow)
        {
            _logger = logger;
            _priceService = priceService;
            _priceService.PriceTick += OnPriceTick;

            Client = client;
            _priceHistoryWindow = priceHistoryWindow;
            PriceHistory = _priceHistoryWindow.PriceHistoryVM.PriceHistory;

            SubscribeCommand = new RelayCommand(Subscribe, CanSubscribe);
            UnsubscribeCommand = new RelayCommand(Unsubscribe, CanUnsubscribe);
            ExitCommand = new RelayCommand(Exit);
            DisplayHistoryCommand = new RelayCommand(DisplayHistory);

            _logger.LogInformation("Application Started.");
        }

        public IPriceTickClient Client { get; private set; }
        public IPriceHistory PriceHistory { get; private set; }

        public ICommand SubscribeCommand { get; private set; }
        public void Subscribe (object parameter)
        {
            var ticker = Client.Ticker;

            Client.PriceTickGridData.Add(new PriceTickGridItem(ticker));
            _priceService.Subscribe(ticker);
            Client.StatusBarText = $"Subcribed {ticker}";
        }
        public bool CanSubscribe (object parameter)
        {
            return !String.IsNullOrWhiteSpace(Client.Ticker)
                    && !Client.PriceTickGridData.Any(x => x.Ticker == Client.Ticker);
        }

        public ICommand UnsubscribeCommand {  get; private set; }
        public void Unsubscribe(object parameter)
        {
            var ticker = Client.Ticker;

            Client.PriceTickGridData.Remove(
                Client.PriceTickGridData.Where(x => x.Ticker == ticker).Single());
            _priceService.Unsubscribe(ticker);
            Client.StatusBarText = $"Unsubcribed {ticker}";
        }
        public bool CanUnsubscribe(object parameter)
        {
            return !String.IsNullOrWhiteSpace(Client.Ticker)
                    && Client.PriceTickGridData.Any(x => x.Ticker == Client.Ticker);
        }

        public ICommand ExitCommand { get; private set; }
        public void Exit(object parameter)
        {
            Application.Current.Shutdown();
        }

        public ICommand DisplayHistoryCommand { get; private set; }
        public void DisplayHistory(object gridDataItem)
        {
            var item = gridDataItem as PriceTickGridItem;
            if (item == null)
                return;

            var ticker = item.Ticker;
            var priceHistory = _priceService.GetPriceHistory(ticker);
            if (priceHistory == null || priceHistory.Count == 0)
            {
                MessageBox.Show($"There is no price history for ticker {ticker}", "Price History");
                return;
            }

            var msg = $"GetPriceHistory({ticker}): Returns {priceHistory.Count} ticks.";
            _logger.LogInformation(msg);
            Client.StatusBarText = msg;

            PriceHistory.Update(ticker, priceHistory);
            _priceHistoryWindow.Show();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            _logger.LogInformation("Application Closing.");
            Application.Current.Shutdown();
        }

        public void OnPriceTick(object sender, PriceTickEventArgs ea)
        {
            var ticker = ea.Ticker;
            var price = ea.Value;
            var item = Client.PriceTickGridData.Where(x => x.Ticker == ticker).FirstOrDefault();
            if (item != null)
            {
                item.Price = price;
            }
        }
    }
}
