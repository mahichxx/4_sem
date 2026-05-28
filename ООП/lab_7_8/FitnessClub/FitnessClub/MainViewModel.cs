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

        // ВОЗВРАЩАЕМ СВОЙСТВА ДЛЯ ЦЕН (Убирает ошибки CS1061)
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

        public event Action RequestRender;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ApplyFilterCommand { get; }

        public MainViewModel()
        {
            // УДАЛИ ИЛИ ЗАКОММЕНТИРУЙ ЭТУ СТРОКУ:
            // CheckDatabase(); 

            //LoadFromDb();
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

            // Логика фильтрации по цене
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            if (!string.IsNullOrWhiteSpace(PriceFrom) && decimal.TryParse(PriceFrom.Replace(',', '.'), System.Globalization.NumberStyles.Any, culture, out decimal pMin))
                if (s.Price < pMin) return false;
            if (!string.IsNullOrWhiteSpace(PriceTo) && decimal.TryParse(PriceTo.Replace(',', '.'), System.Globalization.NumberStyles.Any, culture, out decimal pMax))
                if (s.Price > pMax) return false;

            return true;
        }

        public async void LoadFromDb()
        {
            var data = await DatabaseService.GetAllServicesAsync();
            Application.Current.Dispatcher.Invoke(() => {
                Services.Clear();
                foreach (var item in data) Services.Add(item);
                RequestRender?.Invoke();
            });
        }

        private void DeleteService(Service? s)
        {
            var target = s ?? SelectedService;
            if (target == null) return;
            if (MessageBox.Show($"Удалить '{target.ShortName}'?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseService.DeleteService(target.Id);
                Services.Remove(target);
                RequestRender?.Invoke();
            }
        }

        private void EditService(Service? s)
        {
            var target = s ?? SelectedService;
            if (target == null) return;
            var win = new ServiceEditWindow(target, Services);
            if (win.ShowDialog() == true)
            {
                DatabaseService.SaveServiceWithTransaction(target, false);
                LoadFromDb();
            }
        }

        private void AddService()
        {
            var win = new ServiceEditWindow(null, Services);
            if (win.ShowDialog() == true && win.Result != null)
            {
                DatabaseService.SaveServiceWithTransaction(win.Result, true);
                LoadFromDb();
            }
        }
    }
}