using Client.Models;
using System.Windows.Input;
using System.ComponentModel;

namespace Client.ViewModels
{
    public interface IPriceTickClientVM
    {
        IPriceTickClient Client { get;  }

        IPriceHistory PriceHistory { get;  }

        ICommand SubscribeCommand { get; }

        ICommand UnsubscribeCommand { get; }

        ICommand ExitCommand { get; }

        ICommand DisplayHistoryCommand { get; }

        void OnWindowClosing(object sender, CancelEventArgs e);
    }
}
