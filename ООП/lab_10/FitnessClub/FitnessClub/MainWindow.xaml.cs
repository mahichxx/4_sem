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

            // Подписка на событие отрисовки
            _vm.RequestRender += RenderCards;

            // Безопасный запуск базы данных
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Инициализация БД (Лаба 9 - Code First)
                await Task.Run(() => {
                    using (var db = new FitnessDbContext())
                    {
                        db.Database.Initialize(force: false);
                    }
                });

                _vm.LoadFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке базы: {ex.Message}");
            }
        }

        private void RenderCards()
        {
            if (ServicesPanel == null || _vm == null) return;

            Dispatcher.Invoke(() => {
                ServicesPanel.Children.Clear();
                foreach (var s in _vm.Services)
                {
                    var card = new ServiceCardControl();
                    card.ServiceTitle = s.ShortName;
                    card.PriceValue = s.Price;
                    card.RatingValue = (int)s.Rating;
                    card.IsAdmin = _vm.IsAdminMode;

                    // ИСПРАВЛЕННАЯ ЛОГИКА КАРТИНКИ:
                    if (!string.IsNullOrEmpty(s.ImagePath))
                    {
                        try
                        {
                            // Теперь мы ищем папку Img прямо в папке с запущенной программой
                            string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, s.ImagePath);

                            // Если файл существует - ставим его в карточку
                            if (System.IO.File.Exists(fullPath))
                            {
                                card.ServiceImage = fullPath;
                            }
                        }
                        catch { /* Ошибка пути - просто не покажет фото */ }
                    }

                    card.DataContext = s;
                    card.Margin = new Thickness(10);
                    card.MouseDown += (snd, ev) => _vm.SelectedService = (snd as ServiceCardControl).DataContext as Service;
                    ServicesPanel.Children.Add(card);
                }
            });
        }

        // ВАЖНО: Тот самый метод, из-за которого была ошибка (Лаба 7)
        private void OnServiceCardSelected(object sender, RoutedEventArgs e)
        {
            // Этот метод срабатывает благодаря Bubbling-эффекту (Лаба 7, пункт 3)
            if (e.OriginalSource is ServiceCardControl card && card.DataContext is Service s)
            {
                MessageBox.Show($"Выбрана услуга: {s.ShortName}\nЦена: {s.Price} руб.", "Информация");
            }
        }

        // Метод для счетчика (тоже нужен для XAML)
        private void OnQuantityChangedUserFriendly(object sender, RoutedEventArgs e)
        {
            RenderCards();
        }

        // Остальные методы навигации
        private void OpenProfile_Click(object sender, RoutedEventArgs e) { MessageBox.Show("Профиль пользователя"); }
        private void NavServices_Click(object sender, MouseButtonEventArgs e) { CardsView.Visibility = Visibility.Visible; TableView.Visibility = Visibility.Collapsed; RenderCards(); }
        private void NavManage_Click(object sender, MouseButtonEventArgs e) { CardsView.Visibility = Visibility.Collapsed; TableView.Visibility = Visibility.Visible; TableView.ItemsSource = _vm.Services; }
        private void ApplyFilters_Executed(object sender, ExecutedRoutedEventArgs e) { _vm.ApplyFilterCommand.Execute(null); RenderCards(); }
        private void ClearFilters_Executed(object sender, ExecutedRoutedEventArgs e) { _vm.SearchText = ""; _vm.ApplyFilterCommand.Execute(null); RenderCards(); }
    }
}