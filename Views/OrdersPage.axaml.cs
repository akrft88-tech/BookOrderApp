using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using BookOrder.Models;
using BookOrder.Services;

namespace BookOrder.Views;

public partial class OrdersPage : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly OrderService _service = new();
    private readonly ObservableCollection<Order> _orders = new();

    public OrdersPage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        OrdersListBox.ItemsSource = _orders;

        BtnBack.Click += BtnBack_Click;
        BtnSearch.Click += BtnSearch_Click;
        BtnRefresh.Click += BtnRefresh_Click;

        OrdersListBox.DoubleTapped += OrdersListBox_DoubleTapped;

        LoadOrders();
    }

    private void BtnBack_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.ShowPage(new StartseitePage(_mainWindow));
    }

    private void BtnSearch_Click(object? sender, RoutedEventArgs e)
    {
        string query = SearchBox.Text?.Trim() ?? "";

        var results = string.IsNullOrEmpty(query)
            ? _service.GetAll()
            : _service.Search(query);

        _orders.Clear();
        foreach (var order in results)
            _orders.Add(order);
    }

    private async void OrdersListBox_DoubleTapped(object? sender, RoutedEventArgs e)
    {
        if (OrdersListBox.SelectedItem is Order order)
        {
            var window = new OrderDetailWindow(order);
            await window.ShowDialog(_mainWindow);
            LoadOrders(); // Nach Schließen neu laden
        }
    }

    private void BtnRefresh_Click(object? sender, RoutedEventArgs e)
    {
        LoadOrders();
    }

    private void LoadOrders()
    {
        var list = _service.GetAll();
        _orders.Clear();
        foreach (var order in list)
            _orders.Add(order);
    }
}
