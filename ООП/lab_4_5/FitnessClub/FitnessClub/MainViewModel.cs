using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace FitnessClub
{
    public class MainViewModel : BaseViewModel
    {
        private readonly string _filePath = "data/services.json";
        public ObservableCollection<Service> Services { get; set; } = new();
        public ICollectionView ServicesView { get; set; }

        public Service? SelectedService { get; set; }

        // Свойства для фильтров (просто хранят текст)
        public string SearchText { get; set; } = "";
        public string SelectedCategory { get; set; } = "Все категории";
        public string PriceFrom { get; set; } = "";
        public string PriceTo { get; set; } = "";
        public string RatingFrom { get; set; } = "";
        public string RatingTo { get; set; } = "";

        public ObservableCollection<string> Categories { get; set; } = new();

        private bool _isAdminMode = true;
        public bool IsAdminMode { get => _isAdminMode; set { SetProperty(ref _isAdminMode, value); OnPropertyChanged(nameof(AdminVisibility)); } }
        public Visibility AdminVisibility => IsAdminMode ? Visibility.Visible : Visibility.Collapsed;

        // Команды
        public ICommand ApplyFilterCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SwitchLanguageCommand { get; }

        public MainViewModel()
        {
            LoadServices();
            ServicesView = CollectionViewSource.GetDefaultView(Services);
            ServicesView.Filter = FilterLogic;

            // КНОПКА ПРИМЕНИТЬ
            ApplyFilterCommand = new RelayCommand(_ => ServicesView.Refresh());

            AddCommand = new RelayCommand(_ => AddService());
            EditCommand = new RelayCommand(_ => EditService(), _ => SelectedService != null);
            DeleteCommand = new RelayCommand(_ => DeleteService(), _ => SelectedService != null);
            SwitchLanguageCommand = new RelayCommand(_ => SwitchLanguage());
        }

        private bool FilterLogic(object obj)
        {
            if (obj is not Service s) return false;

            // 1. Поиск по названию
            if (!string.IsNullOrWhiteSpace(SearchText) && !s.ShortName.ToLower().Contains(SearchText.ToLower())) return false;

            // 2. Категория
            if (SelectedCategory != "Все категории" && s.Category != SelectedCategory) return false;

            // 3. Цена (Безопасный парсинг)
            if (double.TryParse(PriceFrom.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double pMin) && (double)s.Price < pMin) return false;
            if (double.TryParse(PriceTo.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double pMax) && (double)s.Price > pMax) return false;

            // 4. Рейтинг
            if (double.TryParse(RatingFrom.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double rMin) && s.Rating < rMin) return false;
            if (double.TryParse(RatingTo.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double rMax) && s.Rating > rMax) return false;

            return true;
        }

        private void LoadServices()
        {
            Services.Clear(); Categories.Clear(); Categories.Add("Все категории");
            if (File.Exists(_filePath))
            {
                var loaded = JsonConvert.DeserializeObject<List<Service>>(File.ReadAllText(_filePath));
                if (loaded != null) foreach (var s in loaded) { Services.Add(s); if (!Categories.Contains(s.Category)) Categories.Add(s.Category); }
            }
            if (Services.Count == 0) foreach (var s in GetDefaultServices()) { Services.Add(s); if (!Categories.Contains(s.Category)) Categories.Add(s.Category); }
        }

        public void SaveServices() => File.WriteAllText(_filePath, JsonConvert.SerializeObject(Services.ToList(), Formatting.Indented));
        private void AddService() { var win = new ServiceEditWindow(null, Services); if (win.ShowDialog() == true && win.Result != null) { Services.Add(win.Result); SaveServices(); } }
        private void EditService()
        {
            if (SelectedService == null) return;
            var win = new ServiceEditWindow(SelectedService, Services);
            if (win.ShowDialog() == true && win.Result != null)
            {
                var idx = Services.IndexOf(SelectedService);
                if (idx != -1) Services[idx] = win.Result;
                SaveServices(); ServicesView.Refresh();
            }
        }
        private void DeleteService()
        {
            if (SelectedService == null) return;
            if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes) { Services.Remove(SelectedService); SaveServices(); }
        }
        private void SwitchLanguage()
        {
            var merged = Application.Current.Resources.MergedDictionaries;

            // 1. Находим текущий словарь строк (в названии которого есть "Strings")
            var oldDict = merged.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Strings"));

            if (oldDict != null)
            {
                // 2. Определяем путь к новому словарю
                string newPath = oldDict.Source.OriginalString.Contains("ru") ? "Strings.en.xaml" : "Strings.ru.xaml";

                // 3. Удаляем старый и добавляем новый
                merged.Remove(oldDict);
                merged.Add(new ResourceDictionary { Source = new Uri(newPath, UriKind.Relative) });
            }
        }
        private List<Service> GetDefaultServices() => new List<Service> {
            new Service { Id=1, ShortName="Йога", Category="Групповые", Price=1500, Rating=4.8, InStock=true },
            new Service { Id=2, ShortName="Бассейн", Category="Водные", Price=2000, Rating=4.9, InStock=true }
        };
    }
}