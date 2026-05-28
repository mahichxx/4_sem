using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FitnessClub
{
    public partial class ServiceEditWindow : Window
    {
        // Свойство для передачи результата обратно в MainViewModel
        public Service Result { get; private set; }
        private Service _originalService;

        // Конструктор принимает объект услуги и список всех услуг (для категорий)
        public ServiceEditWindow(Service service, IEnumerable<Service> allServices)
        {
            InitializeComponent();
            _originalService = service;

            // ИСПРАВЛЕНИЕ: Гарантированно заполняем категории
            var cats = allServices.Select(s => s.Category).Where(c => !string.IsNullOrEmpty(c)).Distinct().ToList();
            if (!cats.Contains("Групповые")) cats.Add("Групповые"); // Добавь сюда категории, которые точно должны быть
            CmbCategory.ItemsSource = cats;

            if (service != null)
            {
                TxtShortName.Text = service.ShortName;
                TxtFullName.Text = service.FullName;
                TxtDescription.Text = service.Description;
                CmbCategory.SelectedItem = service.Category;
                TxtPrice.Text = service.Price.ToString();
                TxtDiscount.Text = service.DiscountPercent.ToString();
                TxtRating.Text = service.Rating.ToString();

                // ИСПРАВЛЕНИЕ: Было PurchaseCount, а база работает с Quantity!
                TxtQuantity.Text = service.Quantity.ToString();

                ChkInStock.IsChecked = service.InStock;
                TxtImagePath.Text = (service.Images != null && service.Images.Count > 0) ? service.Images[0] : "";
            }
            else
            {
                TxtPrice.Text = "0";
                TxtDiscount.Text = "0";
                TxtRating.Text = "5";
                TxtQuantity.Text = "0";
            }
        }

        // Метод выбора изображения (тот самый SelectImage_Click из ошибки)
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Изображения|*.jpg;*.png;*.jpeg;*.webp";
            if (dlg.ShowDialog() == true)
            {
                TxtImagePath.Text = dlg.FileName;
            }
        }

        // Сохранение данных (Save_Click)
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем новый объект или обновляем существующий
                var service = _originalService ?? new Service();

                service.ShortName = TxtShortName.Text;
                service.FullName = TxtFullName.Text;
                service.Description = TxtDescription.Text;
                service.Category = CmbCategory.SelectedItem?.ToString() ?? "Общее";
                service.Price = decimal.Parse(TxtPrice.Text);
                service.DiscountPercent = int.Parse(TxtDiscount.Text);
                service.Rating = double.Parse(TxtRating.Text);
                // Было: service.PurchaseCount = int.Parse(TxtQuantity.Text);
                service.Quantity = int.Parse(TxtQuantity.Text); // Теперь сохраняем в базу правильно                service.InStock = ChkInStock.IsChecked ?? false;

                // Внутри Save_Click перед Result = service;
                if (!string.IsNullOrEmpty(TxtImagePath.Text))
                {
                    service.Images = new List<string> { TxtImagePath.Text };
                }

                Result = service;
                this.DialogResult = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        // Отмена (Cancel_Click)
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}