using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BookOrder.Views;

namespace BookOrder;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ShowPage(new StartseitePage(this));
    }

    public void ShowPage(UserControl page)
    {
        PageHost.Content = page;
    }
}

