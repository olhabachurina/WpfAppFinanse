using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MaterialDesignThemes.Wpf;
using WpfAppFinanse;
using WpfAppFinanse.Themes;
using WpfAppFinanse.Views;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
namespace WpfAppFinanse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty ThemeTextProperty = DependencyProperty.Register(
   "ThemeText", typeof(string), typeof(MainWindow), new PropertyMetadata(default(string)));

        public string ThemeText
        {
            get => (string)GetValue(ThemeTextProperty);
            set => SetValue(ThemeTextProperty, value);
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this; // Установила DataContext для привязок
            UpdateThemeText((Application.Current as App).IsLightTheme); //
        }

        private void ThemeToggle_Checked(object sender, RoutedEventArgs e)
        {
            // Изменение на темную тему
            ChangeTheme("Dark");
            UpdateThemeText(false); // Обновите текст при изменении темы
        }
        private void UpdateThemeText(bool isLightTheme)
        {
            ThemeText = isLightTheme ? "Светлая тема" : "Темная тема";
        }
        private void ThemeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeTheme("Light");
            UpdateThemeText(true);
        }
        private void UpdateButtonStyles(ResourceDictionary themeDict)
        {
            var buttons = FindVisualChildren<Button>(this); // Ищем все кнопки в текущем окне
            foreach (var button in buttons)
            {
                // Обновляем цвета каждой кнопки, используя значения из текущего словаря темы
                button.Foreground = (SolidColorBrush)themeDict["PrimaryForeground"];
                button.Background = (SolidColorBrush)themeDict["PrimaryBackground"];
            }
        }

        // Вспомогательный метод для поиска элементов управления определенного типа
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void ChangeTheme(string theme)
        {
            try
            {
                // Путь к файлу ресурсов зависит от темы
                string themeFileName = theme == "Dark" ? "DarkTheme.xaml" : "LightTheme.xaml";
                string themePath = $"/Themes/{themeFileName}"; // Предполагаем, что файлы темы находятся в папке "Themes"
                var themeUri = new Uri(themePath, UriKind.Relative);

                // Загрузка словаря ресурсов темы
                ResourceDictionary themeDict = Application.LoadComponent(themeUri) as ResourceDictionary;

                // Полная очистка существующих глобальных ресурсов и применение новой темы
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(themeDict);

                // Обновление стилей всех кнопок в приложении
                UpdateButtonStyles(themeDict);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при смене темы: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OpenDataEntryWindow(object sender, RoutedEventArgs e)
        {
            DataEntryWindow entryWindow = new DataEntryWindow();
            entryWindow.Show();
        }
        private void OpenStatementWindow_Click(object sender, RoutedEventArgs e)
        {
            // Создание экземпляра окна StatementWindow и открытие его
            StatementWindow statementWindow = new StatementWindow();
            statementWindow.Show(); // ShowDialog() если нужно открыть окно модально
        }
        private void UpdateButtonStyles()//обновление стили всех кнопок в приложении согласно текущей теме, используя глобальные ресурсы
        {
            foreach (Window window in Application.Current.Windows)
            {
                var buttons = FindVisualChildren<Button>(window);
                foreach (var button in buttons)
                {
                    button.Foreground = (SolidColorBrush)Application.Current.Resources["PrimaryForeground"];
                    button.Background = (SolidColorBrush)Application.Current.Resources["PrimaryBackground"];
                }
            }
        }
    }
}