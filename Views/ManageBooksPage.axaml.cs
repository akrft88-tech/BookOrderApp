using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using System.Linq;
using BookOrder.Models;
using BookOrder.Services;

namespace BookOrder.Views;

public partial class ManageBooksPage : UserControl
{
    private readonly MainWindow _mainWindow;
    private readonly BookService _bookService = new();
    private ObservableCollection<Book> _books = new();

    public ManageBooksPage(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        BtnBack.Click += (_, _) => _mainWindow.ShowPage(new StartseitePage(_mainWindow));
        BtnRefresh.Click += (_, _) => LoadBooks();
        BtnSearch.Click += BtnSearch_Click;
        BtnAdd.Click += BtnAdd_Click;

        LoadBooks();
    }

    private void LoadBooks()
    {
        _books = new ObservableCollection<Book>(_bookService.GetAll().OrderBy(b => b.Title));
        BooksListBox.ItemsSource = _books;
    }

    private void BtnSearch_Click(object? sender, RoutedEventArgs e)
    {
        string query = SearchBox.Text?.Trim().ToLower() ?? "";
        var results = string.IsNullOrEmpty(query)
            ? _bookService.GetAll()
            : _bookService.GetAll()
                .Where(b => b.Title.ToLower().Contains(query) || b.Author.ToLower().Contains(query));

        _books = new ObservableCollection<Book>(results);
        BooksListBox.ItemsSource = _books;
    }

    private void BtnAdd_Click(object? sender, RoutedEventArgs e)
    {
        string title = TitleBox.Text?.Trim() ?? "";
        string author = AuthorBox.Text?.Trim() ?? "";
        string code = CodeBox.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(code))
            return;

        var book = new Book { Title = title, Author = author, BookCode = code };
        _bookService.Add(book);

        TitleBox.Text = "";
        AuthorBox.Text = "";
        CodeBox.Text = "";

        LoadBooks();
    }

    private void BtnDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is Book book)
        {
            _bookService.Delete(book.Id);
            LoadBooks();
        }
    }
}
