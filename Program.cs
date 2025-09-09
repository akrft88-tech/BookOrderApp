using Avalonia;

namespace BookOrder
{
    internal class Program
    {
        [STAThread] 
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()    // Используем твой App.xaml.cs
                         .UsePlatformDetect() // Автоматический выбор платформы (Windows, Linux, macOS)
                         .LogToTrace();       // Логирование
    }
}
