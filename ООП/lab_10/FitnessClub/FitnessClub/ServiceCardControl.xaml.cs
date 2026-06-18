using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitnessClub
{
    public partial class ServiceCardControl : UserControl
    {
        // Новое свойство: Режим админа (скрывает/показывает кнопки)
        public static readonly DependencyProperty IsAdminProperty =
            DependencyProperty.Register("IsAdmin", typeof(bool), typeof(ServiceCardControl), new PropertyMetadata(false));

        public bool IsAdmin
        {
            get => (bool)GetValue(IsAdminProperty);
            set => SetValue(IsAdminProperty, value);
        }

        public static readonly DependencyProperty ServiceTitleProperty =
            DependencyProperty.Register("ServiceTitle", typeof(string), typeof(ServiceCardControl), new PropertyMetadata("Название услуги"));

        public string ServiceTitle
        {
            get => (string)GetValue(ServiceTitleProperty);
            set => SetValue(ServiceTitleProperty, value);
        }

        public static readonly DependencyProperty PriceValueProperty =
            DependencyProperty.Register("PriceValue", typeof(decimal), typeof(ServiceCardControl),
                new PropertyMetadata(0m), value => (decimal)value >= 0);

        public decimal PriceValue
        {
            get => (decimal)GetValue(PriceValueProperty);
            set => SetValue(PriceValueProperty, value);
        }

        public static readonly DependencyProperty CategoryNameProperty =
            DependencyProperty.Register("CategoryName", typeof(string), typeof(ServiceCardControl), new PropertyMetadata("Общее"));

        public string CategoryName
        {
            get => (string)GetValue(CategoryNameProperty);
            set => SetValue(CategoryNameProperty, value);
        }

        public static readonly DependencyProperty ServiceImageProperty =
            DependencyProperty.Register("ServiceImage", typeof(string), typeof(ServiceCardControl), new PropertyMetadata(null));

        public string ServiceImage
        {
            get => (string)GetValue(ServiceImageProperty);
            set => SetValue(ServiceImageProperty, value);
        }

        public static readonly DependencyProperty RatingValueProperty =
            DependencyProperty.Register("RatingValue", typeof(int), typeof(ServiceCardControl),
                new PropertyMetadata(0, null, (d, baseVal) => (int)baseVal > 5 ? 5 : baseVal));

        public int RatingValue
        {
            get => (int)GetValue(RatingValueProperty);
            set => SetValue(RatingValueProperty, value);
        }

        // СОБЫТИЯ
        public static readonly RoutedEvent CardSelectedEvent = EventManager.RegisterRoutedEvent(
            "CardSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ServiceCardControl));

        public event RoutedEventHandler CardSelected
        {
            add => AddHandler(CardSelectedEvent, value);
            remove => RemoveHandler(CardSelectedEvent, value);
        }

        // КОМАНДА
        public static readonly RoutedUICommand OpenDetailsCommand = new RoutedUICommand("OpenDetails", "OpenDetails", typeof(ServiceCardControl));

        public ServiceCardControl()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(OpenDetailsCommand, (s, e) =>
            {
                MessageBox.Show($"Детали услуги: {ServiceTitle}\nЦена: {PriceValue} руб.");
            }));
        }

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CardSelectedEvent));
        }
    }
}