using Avalonia.Controls;
using Avalonia.Interactivity;
namespace BookOrder.Views;

public partial class StartseitePage : UserControl
{
    private readonly MainWindow _mainWindow;

    public StartseitePage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        BtnOrders.Click += BtnOrders_Click;
        BtnAdd.Click += BtnAdd_Click;
        BtnBooks.Click += BtnBooks_Click;
    }

    private void BtnOrders_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.ShowPage(new OrdersPage(_mainWindow));
    }

    private void BtnAdd_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.ShowPage(new AddOrderPage(_mainWindow));
    }

    private void BtnBooks_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.ShowPage(new ManageBooksPage(_mainWindow));
    }
}
