using Client.ViewModels;

namespace Client.Views
{
    public interface IPriceHistoryWindow
    {
        IPriceHistoryVM PriceHistoryVM { get; }

        void Show();
    }
}
