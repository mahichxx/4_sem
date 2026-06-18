using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace FitnessClub
{
    public partial class ServiceDetailWindow : Window
    {
        private Service _service;
        private System.Collections.Generic.IList<Service> _allServices;

        public ServiceDetailWindow(Service service, System.Collections.Generic.IList<Service> allServices)
        {
            InitializeComponent();
            _service = service;
            _allServices = allServices;
            FillData();
        }

        private void FillData()
        {
            TxtFullName.Text = _service.FullName;
            TxtCategory.Text = _service.Category;
            TxtRating.Text = _service.Rating.ToString();
            TxtPrice.Text = $"{_service.Price} руб.";
            TxtDiscount.Text = _service.DiscountPercent > 0 ? $"{_service.DiscountPercent}%" : "";
            TxtStock.Text = _service.InStock ? "✅" : "❌";
            TxtDescription.Text = _service.Description;
            TxtCountry.Text = _service.Country;
            TxtManufacturer.Text = _service.Manufacturer;
            TxtPurchaseCount.Text = _service.PurchaseCount.ToString();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var win = new ServiceEditWindow(_service, _allServices);
            win.Owner = this;
            if (win.ShowDialog() == true && win.Result != null)
            {
                var index = _allServices.IndexOf(_service);
                if (index != -1) _allServices[index] = win.Result;

                _service = win.Result;
                FillData();
                DialogResult = true;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var msg = Application.Current.Resources["MsgDeleteConfirm"]?.ToString() ?? "Удалить?";
            var title = Application.Current.Resources["MsgConfirmTitle"]?.ToString() ?? "Подтверждение";
            if (MessageBox.Show(msg, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _allServices.Remove(_service);
                DialogResult = true;
                Close();
            }
        }
    }
}