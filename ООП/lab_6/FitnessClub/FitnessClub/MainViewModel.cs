using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Newtonsoft.Json;

namespace FitnessClub
{
    public class MainViewModel : BaseViewModel
    {
        private readonly string _filePath = "data/services.json";

        public ObservableCollection<Service> Services { get; set; } = new();
        public ICollectionView ServicesView { get; set; }

        // Класс для хранения пары действий (Прямое и Обратное)
        private class UndoRedoAction
        {
            public Action Forward { get; set; }
            public Action Backward { get; set; }
        }

        // ЗАДАНИЕ 8: Стеки для Undo/Redo
        private Stack<UndoRedoAction> _undoStack = new Stack<UndoRedoAction>();
        private Stack<UndoRedoAction> _redoStack = new Stack<UndoRedoAction>();

        public Service? SelectedService { get; set; }
        public string SearchText { get; set; } = "";
        public string SelectedCategory { get; set; } = "Все категории";
        public string PriceFrom { get; set; } = "";
        public string PriceTo { get; set; } = "";
        public ObservableCollection<string> Categories { get; set; } = new();

        private bool _isAdminMode = true;
        public bool IsAdminMode
        {
            get => _isAdminMode;
            set { SetProperty(ref _isAdminMode, value); OnPropertyChanged(nameof(AdminVisibility)); }
        }
        public Visibility AdminVisibility => IsAdminMode ? Visibility.Visible : Visibility.Collapsed;

        // Команды
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public ICommand SwitchThemeCommand { get; }
        public ICommand SwitchLanguageCommand { get; }

        public MainViewModel()
        {
            LoadServices();
            ServicesView = CollectionViewSource.GetDefaultView(Services);
            ServicesView.Filter = FilterLogic;

            ApplyFilterCommand = new RelayCommand(_ => ServicesView.Refresh());

            // Инициализация Undo/Redo с проверкой возможности нажатия
            UndoCommand = new RelayCommand(_ => Undo(), _ => _undoStack.Count > 0);
            RedoCommand = new RelayCommand(_ => Redo(), _ => _redoStack.Count > 0);

            AddCommand = new RelayCommand(_ => AddService());
            EditCommand = new RelayCommand(obj => EditService(obj as Service));
            DeleteCommand = new RelayCommand(obj => DeleteService(obj as Service));

            SwitchThemeCommand = new RelayCommand(_ => SwitchTheme());
            SwitchLanguageCommand = new RelayCommand(_ => SwitchLanguage());
        }

        // Вспомогательный метод для выполнения действия
        private void ExecuteAction(Action forward, Action backward)
        {
            forward();
            _undoStack.Push(new UndoRedoAction { Forward = forward, Backward = backward });
            _redoStack.Clear(); // Новое действие очищает историю "вперед"
            SaveServices();
            ServicesView.Refresh();
        }

        // ОТМЕНА (Назад)
        private void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var action = _undoStack.Pop();
                action.Backward(); // Выполняем откат
                _redoStack.Push(action); // Сохраняем в Redo
                SaveServices();
                ServicesView.Refresh();
            }
        }

        // ПОВТОР (Вперед)
        private void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var action = _redoStack.Pop();
                action.Forward(); // Повторяем действие
                _undoStack.Push(action); // Возвращаем в Undo
                SaveServices();
                ServicesView.Refresh();
            }
        }

        private void DeleteService(Service? s)
        {
            var target = s ?? SelectedService;
            if (target == null || !IsAdminMode) return;

            if (MessageBox.Show("Удалить выбранную услугу?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var item = target;
                ExecuteAction(
                    () => Services.Remove(item),
                    () => Services.Add(item)
                );
            }
        }

        private void EditService(Service? s)
        {
            var target = s ?? SelectedService;
            if (target == null || !IsAdminMode) return;

            // Клонируем объект ДО изменений для Undo
            var oldState = JsonConvert.DeserializeObject<Service>(JsonConvert.SerializeObject(target));

            var win = new ServiceEditWindow(target, Services);
            if (win.ShowDialog() == true && win.Result != null)
            {
                var newState = JsonConvert.DeserializeObject<Service>(JsonConvert.SerializeObject(win.Result));
                var index = Services.IndexOf(target);

                if (index != -1)
                {
                    ExecuteAction(
                        () => { Services[index] = newState; ServicesView.Refresh(); },
                        () => { Services[index] = oldState; ServicesView.Refresh(); }
                    );
                }
            }
        }

        private void AddService()
        {
            var win = new ServiceEditWindow(null, Services);
            if (win.ShowDialog() == true && win.Result != null)
            {
                var newItem = win.Result;
                ExecuteAction(
                    () => Services.Add(newItem),
                    () => Services.Remove(newItem)
                );
            }
        }

        private bool FilterLogic(object obj)
        {
            if (obj is not Service s) return false;
            if (!string.IsNullOrWhiteSpace(SearchText) && !s.ShortName.ToLower().Contains(SearchText.ToLower())) return false;
            if (SelectedCategory != "Все категории" && s.Category != SelectedCategory) return false;

            var culture = System.Globalization.CultureInfo.InvariantCulture;
            if (decimal.TryParse(PriceFrom.Replace(',', '.'), System.Globalization.NumberStyles.Any, culture, out decimal pMin))
                if (s.Price < pMin) return false;

            if (decimal.TryParse(PriceTo.Replace(',', '.'), System.Globalization.NumberStyles.Any, culture, out decimal pMax))
                if (s.Price > pMax) return false;

            return true;
        }

        private void LoadServices()
        {
            Services.Clear(); Categories.Clear(); Categories.Add("Все категории");
            string dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);

            if (File.Exists(_filePath))
            {
                try
                {
                    var loaded = JsonConvert.DeserializeObject<List<Service>>(File.ReadAllText(_filePath));
                    if (loaded != null)
                        foreach (var s in loaded) { Services.Add(s); if (!Categories.Contains(s.Category)) Categories.Add(s.Category); }
                }
                catch { }
            }
        }

        public void SaveServices() => File.WriteAllText(_filePath, JsonConvert.SerializeObject(Services.ToList(), Formatting.Indented));

        private void SwitchTheme()
        {
            var merged = Application.Current.Resources.MergedDictionaries;
            var oldTheme = merged.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme"));
            if (oldTheme != null)
            {
                string newPath = oldTheme.Source.OriginalString.Contains("Light") ? "Themes/DarkTheme.xaml" : "Themes/LightTheme.xaml";
                merged.Remove(oldTheme);
                merged.Add(new ResourceDictionary { Source = new Uri(newPath, UriKind.Relative) });
            }
        }

        private void SwitchLanguage()
        {
            var merged = Application.Current.Resources.MergedDictionaries;
            var oldDict = merged.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Strings"));
            if (oldDict != null)
            {
                string newPath = oldDict.Source.OriginalString.Contains("ru") ? "Strings.en.xaml" : "Strings.ru.xaml";
                merged.Remove(oldDict);
                merged.Add(new ResourceDictionary { Source = new Uri(newPath, UriKind.Relative) });
            }
        }

        private List<Service> GetDefaultServices() => new List<Service> { new Service { Id = 1, ShortName = "Йога", Category = "Групповые", Price = 1500 } };
    }
}