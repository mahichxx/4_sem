using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                Style = (Style)Application.Current.FindResource("ServiceCard"),
                Width = 220,
                Cursor = Cursors.Hand
            };

            var stack = new StackPanel();

            // Фото
            var img = new Image { Height = 120, Stretch = Stretch.UniformToFill, Margin = new Thickness(-14, -14, -14, 10) };
            try
            {
                string path = (s.Images?.Count > 0) ? s.Images[0] : "icon/favicon.ico";
                img.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            }
            catch { }
            stack.Children.Add(img);

            stack.Children.Add(new TextBlock { Text = s.ShortName, FontSize = 16, FontWeight = FontWeights.Bold, TextWrapping = TextWrapping.Wrap });
            stack.Children.Add(new TextBlock { Text = s.Category, Foreground = Brushes.Gray, FontSize = 12, Margin = new Thickness(0, 2, 0, 4) });
            stack.Children.Add(new TextBlock { Text = $"⭐ {s.Rating}", FontSize = 13 });

            var pricePanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 4, 0, 0) };
            pricePanel.Children.Add(new TextBlock { Text = $"{s.Price} р.", FontSize = 15, FontWeight = FontWeights.Bold, Foreground = (Brush)Application.Current.FindResource("PrimaryBrush") });
            if (s.DiscountPercent > 0)
                pricePanel.Children.Add(new TextBlock { Text = $" -{s.DiscountPercent}%", Foreground = Brushes.OrangeRed, FontSize = 12, Margin = new Thickness(5, 0, 0, 0) });
            stack.Children.Add(pricePanel);

            stack.Children.Add(new TextBlock { Text = s.InStock ? "✅ В наличии" : "❌ Нет", FontSize = 11 });

            // КНОПКИ (Заменили иконки на текст, чтобы не было квадратов)
            var btnPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
            btnPanel.SetBinding(UIElement.VisibilityProperty, new System.Windows.Data.Binding("AdminVisibility") { Source = vm });

            var btnEdit = new Button { Content = "Изм.", Width = 45, Margin = new Thickness(0, 0, 5, 0) };
            var btnDelete = new Button { Content = "Уд.", Width = 45, Style = (Style)Application.Current.FindResource("DangerButton") };

            // ГЛАВНОЕ: Останавливаем событие, чтобы не открывалось окно деталей при клике на кнопки
            btnEdit.Click += (sender, e) => { e.Handled = true; vm.SelectedService = s; vm.EditCommand.Execute(null); };
            btnDelete.Click += (sender, e) => { e.Handled = true; vm.SelectedService = s; vm.DeleteCommand.Execute(null); };

            btnPanel.Children.Add(btnEdit);
            btnPanel.Children.Add(btnDelete);
            stack.Children.Add(btnPanel);

            card.Child = stack;

            // Клик по всей карточке открывает детали
            card.MouseLeftButtonUp += (sender, e) => {
                var win = new ServiceDetailWindow(s, vm.Services); // Передаем сам список!
                win.Owner = owner;
                win.ShowDialog();
            };

            return card;
        }
    }
}