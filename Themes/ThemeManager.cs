using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfAppFinanse.Themes
{
    public static class ThemeManager
    {
        private static string _currentTheme;

        public static void ApplyTheme(string theme)
        {
            if (_currentTheme == theme) return; // Если тема не изменилась, ничего не делаем

            _currentTheme = theme;

            string themeFileName = theme == "Dark" ? "DarkTheme.xaml" : "LightTheme.xaml";
            string themePath = $"/Themes/{themeFileName}";
            var themeUri = new Uri(themePath, UriKind.Relative);
            var newThemeDict = new ResourceDictionary { Source = themeUri };

            // Удаляем старый словарь темы и добавляем новый
            var appResources = Application.Current.Resources;
            var oldThemeDict = appResources.MergedDictionaries.FirstOrDefault(d => d.Source?.ToString().Contains("Theme.xaml") == true);
            if (oldThemeDict != null)
            {
                appResources.MergedDictionaries.Remove(oldThemeDict);
            }
            appResources.MergedDictionaries.Add(newThemeDict);
        }
    }
}

