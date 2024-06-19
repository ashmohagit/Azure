using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> Products { get; set; }
        private CollectionViewSource ViewSource { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            SetupCollectionViewSource();
            ProductsDataGrid.ItemsSource = ViewSource.View;
        }
        private void LoadData()
        {
            Products = new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Product A", Price = 10.0 },
                new Product { Id = 2, Name = "Product B", Price = 20.0 },
                new Product { Id = 3, Name = "Product C", Price = 30.0 },
                new Product { Id = 4, Name = "Product D", Price = 40.0 }
            };
        }

        private void SetupCollectionViewSource()
        {
            ViewSource = new CollectionViewSource { Source = Products };
        }

        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            ViewSource.Filter -= FilterProducts;
            ViewSource.Filter += FilterProducts;
        }

        private void FilterProducts(object sender, FilterEventArgs e)
        {
            if (e.Item is Product product)
            {
                bool isNameMatch = string.IsNullOrEmpty(NameFilterTextBox.Text) ||
                                   product.Name.ToLower().Contains(NameFilterTextBox.Text.ToLower());

                bool isPriceMatch = double.TryParse(PriceFilterTextBox.Text, out double price) ?
                                    product.Price <= price : true;

                e.Accepted = isNameMatch && isPriceMatch;
            }
        }
    }
}
