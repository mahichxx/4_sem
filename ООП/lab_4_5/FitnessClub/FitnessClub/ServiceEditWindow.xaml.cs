using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Globalization;

namespace FitnessClub
{
    public partial class ServiceEditWindow : Window
    {
        public Service? Result { get; private set; }
        private Service? _original;
        private IList<Service> _allServices;
        private string _imagePath = "";

        public ServiceEditWindow(Service? service, IList<Service> allServices)
        {
            InitializeComponent();
            _original = service;
            _allServices = allServices;

            if (service != null) FillForm(service);
            else ChkInStock.IsChecked = true;
        }

        private void FillForm(Service s)
        {
            TxtShortName.Text = s.ShortName;
            TxtFullName.Text = s.FullName;
            TxtDescription.Text = s.Description;
            CmbCategory.Text = s.Category;
            TxtPrice.Text = s.Price.ToString();
            TxtDiscount.Text = s.DiscountPercent.ToString();
            TxtRating.Text = s.Rating.ToString();
            TxtQuantity.Text = s.Quantity.ToString();
            ChkInStock.IsChecked = s.InStock;

            if (s.Images?.Count > 0)
            {
                _imagePath = s.Images[0];
                TxtImagePath.Text = Path.GetFileName(_imagePath);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtShortName.Text))
            {
                MessageBox.Show("Введите название!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var culture = CultureInfo.CurrentCulture;

            Result = new Service
            {
                Id = _original?.Id ?? (_allServices.Count + 1),
                ShortName = TxtShortName.Text.Trim(),
                FullName = TxtFullName.Text.Trim(),
                Description = TxtDescription.Text.Trim(),
                Category = CmbCategory.Text.Trim(),
                Price = decimal.TryParse(TxtPrice.Text.Replace('.', ','), NumberStyles.Any, culture, out var p) ? p : 0,
                DiscountPercent = int.TryParse(TxtDiscount.Text, out var d) ? d : 0,
                Rating = double.TryParse(TxtRating.Text.Replace('.', ','), NumberStyles.Any, culture, out var r) ? r : 0,
                Quantity = int.TryParse(TxtQuantity.Text, out var q) ? q : 0,
                InStock = ChkInStock.IsChecked == true,

                // Эти поля мы не используем в фитнес-клубе, оставляем пустыми
                Country = "",
                Manufacturer = "",
                Color = "",
                Size = "",

                PurchaseCount = _original?.PurchaseCount ?? 0,
                Images = !string.IsNullOrEmpty(_imagePath) ? new List<string> { _imagePath } : (_original?.Images ?? new List<string>())
            };

            DialogResult = true;
            Close();
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Изображения|*.jpg;*.png;*.jpeg;*.bmp" };
            if (dialog.ShowDialog() == true)
            {
                _imagePath = dialog.FileName;
                TxtImagePath.Text = Path.GetFileName(_imagePath);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();

        private void DropZone_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
            e.Handled = true;
        }

        private void DropZone_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    _imagePath = files[0];
                    TxtImagePath.Text = Path.GetFileName(_imagePath);
                }
            }
        }
    }
}