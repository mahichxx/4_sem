using System;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub
{
    public partial class MainWindow : Window
    {
        private MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            this.DataContext = _vm;

            // Подписываемся на обновление карточек
            _vm.RequestRender += RenderCards;

            // Загружаем базу после открытия окна
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(() => DatabaseService.InitializeDatabase());
                _vm.LoadFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка БД: {ex.Message}");
            }
        }

        private void RenderCards()
        {
            if (ServicesPanel == null || _vm == null) return;

            // Используем Dispatcher, чтобы обновление UI шло в правильном потоке
            Dispatcher.Invoke(() => {
                ServicesPanel.Children.Clear();
                foreach (var item in _vm.ServicesView)
                {
                    if (item is Service s)
                    {
                        var card = new ServiceCardControl();
                        card.ServiceTitle = s.ShortName;
                        card.PriceValue = s.Price;
                        card.RatingValue = (int)s.Rating;
                        card.ServiceImage = (s.Images != null && s.Images.Count > 0) ? s.Images[0] : null;
                        card.IsAdmin = _vm.IsAdminMode;
                        card.DataContext = s;
                        card.Margin = new Thickness(10);

                        // При клике на карточку — она становится "Выбранной"
                        card.MouseDown += (snd, ev) => _vm.SelectedService = (snd as ServiceCardControl).DataContext as Service;

                        ServicesPanel.Children.Add(card);
                    }
                }
            });
        }

        // --- ИСПРАВЛЕНИЕ: МЕТОДЫ, КОТОРЫЕ ИЩЕТ XAML ---

        // 1. Метод для счетчика (тот самый, из-за которого ошибка)
        private void OnQuantityChangedUserFriendly(object sender, RoutedEventArgs e)
        {
            // Просто обновляем список при изменении счетчика
            _vm?.ApplyFilterCommand?.Execute(null);
            RenderCards();
        }

        // 2. Метод для открытия профиля
        private void OpenProfile_Click(object sender, RoutedEventArgs e)
        {
            var profileWin = new UserProfileWindow { Owner = this, DataContext = this.DataContext };
            profileWin.ShowDialog();
        }

        // 3. Метод при клике на карточку (если он прописан в XAML)
        private void OnServiceCardSelected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is ServiceCardControl card && card.DataContext is Service s)
            {
                MessageBox.Show($"Услуга: {s.ShortName}\nЦена: {s.Price} руб.");
            }
        }

        //  Навигация и фильтры 
        private void NavServices_Click(object sender, MouseButtonEventArgs e)
        {
            CardsView.Visibility = Visibility.Visible;
            TableView.Visibility = Visibility.Collapsed;
            RenderCards();
        }

        private void NavManage_Click(object sender, MouseButtonEventArgs e)
        {
            CardsView.Visibility = Visibility.Collapsed;
            TableView.Visibility = Visibility.Visible;
            TableView.ItemsSource = _vm.Services;
        }

        private void ApplyFilters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.ApplyFilterCommand.Execute(null);
            RenderCards();
        }

        private void ClearFilters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.SearchText = "";
            _vm.PriceFrom = "";
            _vm.PriceTo = "";
            _vm.ApplyFilterCommand.Execute(null);
            RenderCards();
        }
    }
}