using System.Windows.Input;

namespace FitnessClub
{
    public static class AppCommands
    {
        // Команда сброса (Ctrl+R)
        public static readonly RoutedUICommand ClearFilters = new RoutedUICommand(
            "Очистить фильтры", "ClearFilters", typeof(AppCommands),
            new InputGestureCollection { new KeyGesture(Key.R, ModifierKeys.Control) }
        );

        // Команда применения (Enter / Ctrl+Enter)
        public static readonly RoutedUICommand ApplyFilters = new RoutedUICommand(
            "Применить фильтры", "ApplyFilters", typeof(AppCommands),
            new InputGestureCollection { new KeyGesture(Key.Enter) }
        );
    }
}