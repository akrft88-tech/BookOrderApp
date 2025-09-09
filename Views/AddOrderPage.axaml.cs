using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using System.Linq;
using BookOrder.Models;
using BookOrder.Services;
using System.Collections.Generic;

namespace BookOrder.Views;

public partial class AddOrderPage : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly OrderService _orderService = new();
    private readonly BookService _bookService = new();

    private ObservableCollection<Book> _books = new();
    private ObservableCollection<OrderBook> _currentOrder = new();

    public AddOrderPage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        // Klick-Events
        BtnAddBook.Click += BtnAddBook_Click;
        BtnSave.Click += BtnSave_Click;
        BtnBack.Click += BtnBack_Click;

        // NEU: Events für Synchronisation
        BooksList.SelectionChanged += BooksList_SelectionChanged;
        BookCodeBox.LostFocus += BookCodeBox_LostFocus; // oder alternativ TextChanged

        LoadBooks();
        CurrentOrderList.ItemsSource = _currentOrder;
    }

    // Bücher laden
    private void LoadBooks()
    {
        _books = new ObservableCollection<Book>(_bookService.GetAll().OrderBy(b => b.Title));
        BooksList.ItemsSource = _books;
        BooksList.SelectedIndex = _books.Any() ? 0 : -1;
    }

    // ========== NEU ==========
    // Wenn man ein Buch in der Liste auswählt → Code ins Textfeld schreiben
    private void BooksList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (BooksList.SelectedItem is Book selectedBook)
        {
            BookCodeBox.Text = selectedBook.BookCode; // Code anzeigen
        }
    }

    // ========== NEU ==========
    // Wenn man einen Code eingibt und Textfeld verlässt → das Buch in der Liste auswählen
    private void BookCodeBox_LostFocus(object? sender, RoutedEventArgs e)
    {
        var code = BookCodeBox.Text?.Trim();
        if (!string.IsNullOrEmpty(code))
        {
            var found = _books.FirstOrDefault(b => b.BookCode == code);
            if (found != null)
            {
                BooksList.SelectedItem = found; // Buch in Liste markieren
            }
        }
    }

    // Buch zur aktuellen Bestellung hinzufügen
    private void BtnAddBook_Click(object? sender, RoutedEventArgs e)
    {
        Book? selectedBook = null;

        var code = BookCodeBox.Text?.Trim();
        if (!string.IsNullOrEmpty(code))
        {
            selectedBook = _books.FirstOrDefault(b => b.BookCode == code);
            if (selectedBook != null)
            {
                BooksList.SelectedItem = selectedBook; // auch Liste updaten
            }
        }

        if (selectedBook == null)
            selectedBook = BooksList.SelectedItem as Book;

        if (selectedBook == null)
            return;

        int quantity = (int)(QuantityBox.Value ?? 1);

        var existing = _currentOrder.FirstOrDefault(ob => ob.BookId == selectedBook.Id);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _currentOrder.Add(new OrderBook
            {
                BookId = selectedBook.Id,
                Book = selectedBook,
                Quantity = quantity
            });
        }

        // Eingaben zurücksetzen
        BookCodeBox.Text = "";
        QuantityBox.Value = 1;

        // Refresh der Liste
        CurrentOrderList.ItemsSource = null;
        CurrentOrderList.ItemsSource = _currentOrder;
    }

    // Bestellung speichern
    private void BtnSave_Click(object? sender, RoutedEventArgs e)
    {
        string name = NameBox.Text?.Trim() ?? "";
        string address = AddressBox.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(address) ||
            !_currentOrder.Any())
            return;

        _orderService.Add(name, address, _currentOrder.Select(ob => (ob.BookId, ob.Quantity)).ToList());

        // Eingaben zurücksetzen
        NameBox.Text = "";
        AddressBox.Text = "";
        _currentOrder.Clear();
        CurrentOrderList.ItemsSource = null;
        CurrentOrderList.ItemsSource = _currentOrder;
    }

    private void BtnBack_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindow.ShowPage(new StartseitePage(_mainWindow));
    }
}
