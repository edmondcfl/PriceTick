using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Service;
using Client.Models;
using Client.ViewModels;
using Client.Views;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _host;
        private IPriceService _priceService;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .UseSerilog((host, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .WriteTo.File("Client.log", rollingInterval: RollingInterval.Day)
                        .WriteTo.Debug()
                        .MinimumLevel.Debug();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<IPriceTickClientVM, PriceTickClientVM>();
                    services.AddSingleton<IPriceTickClient, PriceTickClient>();
                    services.AddSingleton<IPriceHistoryWindow, PriceHistoryWindow>();
                    services.AddSingleton<IPriceHistoryVM, PriceHistoryVM>();
                    services.AddSingleton<IPriceHistory, PriceHistory>();
                    services.AddSingleton<IPriceService, PriceService>();
                })
                .Build();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            _priceService = _host.Services.GetRequiredService<IPriceService>();
            Task task = Task.Run(_priceService.Start);

            base.OnStartup(e);
        }
    }

}
