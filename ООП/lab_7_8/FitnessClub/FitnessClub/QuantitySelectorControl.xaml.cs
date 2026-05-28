using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitnessClub
{
    public partial class QuantitySelectorControl : UserControl
    {
        public static readonly DependencyProperty QuantityValueProperty =
            DependencyProperty.Register("QuantityValue", typeof(int), typeof(QuantitySelectorControl),
                new PropertyMetadata(1, null, CoerceQuantity), ValidateQuantity);

        public int QuantityValue
        {
            get => (int)GetValue(QuantityValueProperty);
            set => SetValue(QuantityValueProperty, value);
        }

        // Валидация (ValidateValueCallback)
        private static bool ValidateQuantity(object value)
        {
            return (int)value >= 0; // Значение не может быть отрицательным
        }

        // Корректировка (CoerceValueCallback)
        private static object CoerceQuantity(DependencyObject d, object baseValue)
        {
            int val = (int)baseValue;
            if (val > 100) return 100; // Максимальное ограничение - 100
            return val;
        }


        // 1. Direct (Прямое)
        public static readonly RoutedEvent ValueChangedDirectEvent = EventManager.RegisterRoutedEvent(
            "ValueChangedDirect", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(QuantitySelectorControl));

        // 2. Tunneling (Туннельное - идет сверху вниз, начинается с Preview)
        public static readonly RoutedEvent PreviewQuantityChangedEvent = EventManager.RegisterRoutedEvent(
            "PreviewQuantityChanged", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(QuantitySelectorControl));

        // 3. Bubbling (Пузырьковое - идет снизу вверх)
        public static readonly RoutedEvent QuantityChangedEvent = EventManager.RegisterRoutedEvent(
            "QuantityChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(QuantitySelectorControl));

        public event RoutedEventHandler QuantityChanged
        {
            add => AddHandler(QuantityChangedEvent, value);
            remove => RemoveHandler(QuantityChangedEvent, value);
        }

        public static readonly RoutedUICommand IncrementCommand = new RoutedUICommand("Increment", "Increment", typeof(QuantitySelectorControl));
        public static readonly RoutedUICommand DecrementCommand = new RoutedUICommand("Decrement", "Decrement", typeof(QuantitySelectorControl));

        public QuantitySelectorControl()
        {
            InitializeComponent();

            // Привязка логики к командам
            CommandBindings.Add(new CommandBinding(IncrementCommand, (s, e) =>
            {
                RaiseEvent(new RoutedEventArgs(PreviewQuantityChangedEvent)); // Запуск туннельного события
                QuantityValue++;
                RaiseEvent(new RoutedEventArgs(ValueChangedDirectEvent));     // Запуск прямого события
                RaiseEvent(new RoutedEventArgs(QuantityChangedEvent));        // Запуск пузырькового события
            }));

            CommandBindings.Add(new CommandBinding(DecrementCommand, (s, e) =>
            {
                if (QuantityValue > 0)
                {
                    QuantityValue--;
                    RaiseEvent(new RoutedEventArgs(QuantityChangedEvent));
                }
            }));
        }
    }
}