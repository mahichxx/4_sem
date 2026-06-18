using System;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace FitnessClub
{
    public partial class MainWindow : Window
    {
        private MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;

            // 1. Отрисовка при запуске
            RenderCards();

            // 2. АВТО-ОБНОВЛЕНИЕ КАРТОЧЕК при поиске или фильтрации
            // Мы подписываемся на событие обновления представления
            _vm.ServicesView.CollectionChanged += (s, e) => RenderCards();
        }

        // МЕТОД ОТРИСОВКИ КАРТОЧЕК (Задание 7)
        private void RenderCards()
        {
            // Проверка, существует ли панель (чтобы не было ошибок при инициализации)
            if (ServicesPanel == null) return;

            ServicesPanel.Children.Clear();

            // Проходим по отфильтрованному списку из ViewModel
            foreach (var item in _vm.ServicesView)
            {
                if (item is Service s)
                {
                    // Создаем ваш новый UserControl
                    var card = new ServiceCardControl
                    {
                        DataContext = s, // Передаем данные услуги в карточку
                        Margin = new Thickness(10)
                    };

                    // Добавляем карточку на экран
                    ServicesPanel.Children.Add(card);
                }
            }
        }

        // ИСПРАВЛЕНИЕ: Открытие окна личного кабинета (Задание 5)
        private void OpenProfile_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр окна
            var profileWin = new UserProfileWindow();

            profileWin.Owner = this; // Указываем владельца
            profileWin.DataContext = this.DataContext; // Передаем ViewModel для работы темы/языка

            // Используем ShowDialog() вместо Show()
            // Это делает окно МОДАЛЬНЫМ (нельзя кликнуть на главное окно, пока это открыто)
            profileWin.ShowDialog();
        }

        // Переключение видов (Карточки / Таблица)
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
        }

        private void BtnViewCards_Click(object sender, RoutedEventArgs e) => NavServices_Click(null, null);
        private void BtnViewTable_Click(object sender, RoutedEventArgs e) => NavManage_Click(null, null);

        // Обработка двойного клика в таблице (для редактирования)
        private void TableView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_vm.SelectedService != null && _vm.IsAdminMode)
            {
                _vm.EditCommand.Execute(_vm.SelectedService);
                RenderCards();
            }
        }
    }
}