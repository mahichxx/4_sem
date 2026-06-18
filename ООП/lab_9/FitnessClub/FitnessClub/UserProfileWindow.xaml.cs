using System;
using System.Linq;
using System.Windows;

namespace FitnessClub
{
    public partial class UserProfileWindow : Window
    {
        // КОНСТРУКТОР (Обязательно должен быть!)
        public UserProfileWindow()
        {
            InitializeComponent(); // Без этой строки окно будет белым!
        }

        // Метод для Светлой темы
        private void SetLightTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Themes/LightTheme.xaml");
        }

        // Метод для Темной темы
        private void SetDarkTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Themes/DarkTheme.xaml");
        }

        // Метод для Розовой темы
        private void SetPinkTheme_Click(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Themes/PinkTheme.xaml");
        }

        // Вспомогательный метод для смены темы
        private void ApplyTheme(string path)
        {
            try
            {
                var merged = Application.Current.Resources.MergedDictionaries;
                var oldTheme = merged.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme"));

                if (oldTheme != null) merged.Remove(oldTheme);

                merged.Add(new ResourceDictionary { Source = new Uri(path, UriKind.Relative) });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при смене темы: {ex.Message}");
            }
        }

        // Метод закрытия окна
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}