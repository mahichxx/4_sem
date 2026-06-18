using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FitnessClub
{
    public static class CardFactory
    {
        public static Border CreateCard(Service s, MainViewModel vm, Window owner)
        {
            var card = new Border
            {
                Width = 220,
                // Безопасный поиск стиля: если не найдем "ServiceCard", будет просто рамка
                Style = Application.Current.TryFindResource("ServiceCard") as Style,
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10)
            };

            var stack = new StackPanel();

            // Фото с защитой от ошибок пути
            var img = new Image { Height = 120, Stretch = Stretch.UniformToFill, Margin = new Thickness(0, 0, 0, 10) };
            try
            {
                string path = (s.Images?.Count > 0) ? s.Images[0] : "pack://application:,,,/icon/favicon.ico";
                if (!string.IsNullOrEmpty(path))
                {
                    // Проверяем, существует ли файл, если это локальный путь
                    if (!path.StartsWith("http") && !path.StartsWith("pack") && !System.IO.File.Exists(path))
                        path = "pack://application:,,,/icon/favicon.ico";

                    img.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                }
            }
            catch { /* Если фото не загрузилось, просто идем дальше */ }
            stack.Children.Add(img);

            stack.Children.Add(new TextBlock { Text = s.ShortName, FontSize = 16, FontWeight = FontWeights.Bold, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(5, 0, 5, 0) });
            stack.Children.Add(new TextBlock { Text = s.Category, Foreground = Brushes.Gray, FontSize = 12, Margin = new Thickness(5, 2, 5, 4) });
            stack.Children.Add(new TextBlock { Text = $"⭐ {s.Rating}", FontSize = 13, Margin = new Thickness(5, 0, 5, 0) });

            var pricePanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5, 4, 5, 10) };
            pricePanel.Children.Add(new TextBlock
            {
                Text = $"{s.Price} р.",
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)Application.Current.TryFindResource("PrimaryBrush") ?? Brushes.Green
            });
            stack.Children.Add(pricePanel);

            // Кнопки управления (только для админа)
            var btnPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 5, 0, 5) };
            btnPanel.SetBinding(UIElement.VisibilityProperty, new System.Windows.Data.Binding("AdminVisibility") { Source = vm });

            var btnEdit = new Button { Content = "✏️", Width = 40, Margin = new Thickness(5) };
            var btnDelete = new Button
            {
                Content = "🗑️",
                Width = 40,
                Margin = new Thickness(5),
                Style = Application.Current.TryFindResource("DangerButton") as Style
            };

            btnEdit.Click += (sender, e) => { e.Handled = true; vm.SelectedService = s; vm.EditCommand.Execute(null); };
            btnDelete.Click += (sender, e) => { e.Handled = true; vm.SelectedService = s; vm.DeleteCommand.Execute(null); };

            btnPanel.Children.Add(btnEdit);
            btnPanel.Children.Add(btnDelete);
            stack.Children.Add(btnPanel);

            card.Child = stack;

            // Клик по всей карточке для деталей
            card.MouseLeftButtonUp += (sender, e) => {
                var win = new ServiceDetailWindow(s, vm.Services);
                win.Owner = owner;
                win.ShowDialog();
            };

            return card;
        }
    }
}