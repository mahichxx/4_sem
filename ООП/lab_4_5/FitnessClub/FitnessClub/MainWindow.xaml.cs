using System.Windows;
using System.Windows.Input;

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
            RenderCards();

            // Перерисовка карточек, когда в модели вызывается Refresh()
            _vm.ServicesView.CollectionChanged += (s, e) => RenderCards();
        }

        private void RenderCards()
        {
            if (ServicesPanel == null) return;
            ServicesPanel.Children.Clear();
            foreach (var obj in _vm.ServicesView)
            {
                if (obj is Service s) ServicesPanel.Children.Add(CardFactory.CreateCard(s, _vm, this));
            }
        }

        private void NavServices_Click(object sender, MouseButtonEventArgs e) { CardsView.Visibility = Visibility.Visible; TableView.Visibility = Visibility.Collapsed; RenderCards(); }
        private void NavManage_Click(object sender, MouseButtonEventArgs e) { CardsView.Visibility = Visibility.Collapsed; TableView.Visibility = Visibility.Visible; }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) { }
        private void BtnViewCards_Click(object sender, RoutedEventArgs e) => NavServices_Click(null, null);
        private void BtnViewTable_Click(object sender, RoutedEventArgs e) => NavManage_Click(null, null);

        private void TableView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_vm.SelectedService != null)
            {
                var win = new ServiceDetailWindow(_vm.SelectedService, _vm.Services);
                win.Owner = this;
                if (win.ShowDialog() == true) { _vm.SaveServices(); RenderCards(); }
            }
        }
    }
}