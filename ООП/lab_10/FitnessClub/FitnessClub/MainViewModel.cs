using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace FitnessClub
{
    public class MainViewModel : BaseViewModel
    {
        // 10. ПАТТЕРН Unit of Work (Единица работы)
        // Теперь это наш единственный "пульт управления" данными
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public ObservableCollection<Service> Services { get; set; } = new();
        public ICollectionView ServicesView { get; set; }
        public ObservableCollection<string> Categories { get; set; } = new();

        private Service? _selectedService;
        public Service? SelectedService
        {
            get => _selectedService;
            set => SetProperty(ref _selectedService, value);
        }

        private string _searchText = "";
        public string SearchText { get => _searchText; set { SetProperty(ref _searchText, value); ServicesView.Refresh(); } }

        private string _priceFrom = "";
        public string PriceFrom { get => _priceFrom; set { SetProperty(ref _priceFrom, value); ServicesView.Refresh(); } }

        private string _priceTo = "";
        public string PriceTo { get => _priceTo; set { SetProperty(ref _priceTo, value); ServicesView.Refresh(); } }

        private bool _isAdminMode = true;
        public bool IsAdminMode
        {
            get => _isAdminMode;
            set
            {
                SetProperty(ref _isAdminMode, value);
                OnPropertyChanged(nameof(AdminVisibility));
                RequestRender?.Invoke();
            }
        }

        public Visibility AdminVisibility => IsAdminMode ? Visibility.Visible : Visibility.Collapsed;

        public event Action? RequestRender;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ApplyFilterCommand { get; }

        public MainViewModel()
        {
            ServicesView = CollectionViewSource.GetDefaultView(Services);
            ServicesView.Filter = FilterLogic;

            AddCommand = new RelayCommand(_ => AddService());
            EditCommand = new RelayCommand(obj => EditService(obj as Service));
            DeleteCommand = new RelayCommand(obj => DeleteService(obj as Service));
            ApplyFilterCommand = new RelayCommand(_ => ServicesView.Refresh());
        }

        private bool FilterLogic(object obj)
        {
            if (obj is not Service s) return false;
            if (!string.IsNullOrWhiteSpace(SearchText) && !s.ShortName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) return false;

            var culture = System.Globalization.CultureInfo.InvariantCulture;
            if (!string.IsNullOrWhiteSpace(PriceFrom) && decimal.TryParse(PriceFrom.Replace(',', '.'), System.Globalization.NumberStyles.Any, culture, out decimal pMin))
                if (s.Price < pMin) return false;
            if (!string.IsNullOrWhiteSpace(PriceTo) && decimal.TryParse(PriceTo.Replace(',', '.'), System.Globalization.NumberStyles.Any, culture, out decimal pMax))
                if (s.Price > pMax) return false;

            return true;
        }

        // МЕТОД ЗАГРУЗКИ (Использует Репозиторий)
        public void LoadFromDb()
        {
            try
            {
                // 10. Использование паттерна Repository через UnitOfWork
                // Мы просто просим репозиторий Services дать нам все данные
                var data = _unitOfWork.Services.GetAll().ToList();

                Application.Current.Dispatcher.Invoke(() => {
                    Services.Clear();
                    foreach (var item in data) Services.Add(item);

                    Categories.Clear();
                    Categories.Add("Все категории");

                    // Загружаем категории из репозитория категорий
                    var allCategories = _unitOfWork.Categories.GetAll();
                    foreach (var cat in allCategories)
                    {
                        if (!Categories.Contains(cat.Name))
                            Categories.Add(cat.Name);
                    }

                    RequestRender?.Invoke();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки (Лаба 10): " + ex.Message);
            }
        }

        // CRUD: Удаление
        private void DeleteService(Service? s)
        {
            var target = s ?? SelectedService;
            if (target == null) return;

            if (MessageBox.Show($"Удалить '{target.ShortName}'?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // 10. Удаляем через Репозиторий и сохраняем через UnitOfWork
                _unitOfWork.Services.Delete(target.Id);
                _unitOfWork.Save(); // Фиксируем изменения в базе

                Services.Remove(target);
                RequestRender?.Invoke();
            }
        }

        // CRUD: Редактирование
        private void EditService(Service? s)
        {
            var target = s ?? SelectedService;
            if (target == null) return;

            var win = new ServiceEditWindow(target, Services);
            if (win.ShowDialog() == true)
            {
                // 10. Обновляем объект в репозитории
                _unitOfWork.Services.Update(target);
                _unitOfWork.Save();

                LoadFromDb();
            }
        }

        // CRUD: Добавление
        private void AddService()
        {
            var win = new ServiceEditWindow(null, Services);
            if (win.ShowDialog() == true && win.Result != null)
            {
                // 10. Создаем новую запись через репозиторий
                _unitOfWork.Services.Create(win.Result);
                _unitOfWork.Save();

                LoadFromDb();
            }
        }
    }
}