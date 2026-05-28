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

            // Извлекаем уникальные категории для выпадающего списка
            CmbCategory.ItemsSource = allServices.Select(s => s.Category).Distinct().ToList();

            if (service != null)
            {
                // Если мы редактируем — заполняем поля данными
                TxtShortName.Text = service.ShortName;
                TxtFullName.Text = service.FullName;
                TxtDescription.Text = service.Description;
                CmbCategory.SelectedItem = service.Category;
                TxtPrice.Text = service.Price.ToString();
                TxtDiscount.Text = service.DiscountPercent.ToString();
                TxtRating.Text = service.Rating.ToString();
                TxtQuantity.Text = service.PurchaseCount.ToString();
                ChkInStock.IsChecked = service.InStock;
                TxtImagePath.Text = (service.Images != null && service.Images.Count > 0) ? service.Images[0] : "";
            }
            else
            {
                // Если создаем новую — ставим значения по умолчанию
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
                service.PurchaseCount = int.Parse(TxtQuantity.Text);
                service.InStock = ChkInStock.IsChecked ?? false;

                if (!string.IsNullOrEmpty(TxtImagePath.Text))
                {
                    if (service.Images == null) service.Images = new List<string>();
                    service.Images.Clear();
                    service.Images.Add(TxtImagePath.Text);
                }

                Result = service;
                this.DialogResult = true; // Закрывает окно и возвращает успех
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в данных! Проверьте числа и цены.\n" + ex.Message);
            }
        }

        // Отмена (Cancel_Click)
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}