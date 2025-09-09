using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using BookOrder.Models;
using BookOrder.Services;
using System.Linq;

namespace BookOrder.Views;

public partial class OrderDetailWindow : Window
{
    private readonly OrderService _orderService = new();
    private Order _order;

    private ObservableCollection<OrderBook> _books = new();

    public OrderDetailWindow(Order order)
    {
        InitializeComponent();
        _order = order;

        // Daten setzen
        NameBox.Text = order.ReaderName;
        AddressBox.Text = order.ReaderAddress;
        _books = new ObservableCollection<OrderBook>(order.OrderBooks);
        BooksListBox.ItemsSource = _books;

        // Events
        BtnSave.Click += BtnSave_Click;
        BtnDelete.Click += BtnDelete_Click;
        BtnClose.Click += BtnClose_Click;
    }

    private void BtnSave_Click(object? sender, RoutedEventArgs e)
    {
        _order.ReaderName = NameBox.Text?.Trim() ?? "";
        _order.ReaderAddress = AddressBox.Text?.Trim() ?? "";
        _order.OrderBooks = _books.ToList();

        _orderService.Update(_order); // Update im Service implementieren
        this.Close();
    }

    private void BtnDelete_Click(object? sender, RoutedEventArgs e)
    {
        _orderService.Delete(_order.Id); // Delete im Service implementieren
        this.Close();
    }

    private void BtnClose_Click(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
