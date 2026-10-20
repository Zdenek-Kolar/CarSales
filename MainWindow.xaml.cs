using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using CarSales.Models;
using CarSales.ViewModels;
using Microsoft.Win32;


namespace CarSales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = "xml";
                openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                bool? result = openFileDialog.ShowDialog();

                if (result is true)
                {
                    // display the selected file name
                    FileNameTextBox.Text = openFileDialog.FileName;

                    List<Sale> sales = new List<Sale>();

                    XDocument xml = XDocument.Load(openFileDialog.FileName);
                    IEnumerable<XElement> xmlSales = xml.Descendants("carsale");
                    foreach (XElement xmlSale in xmlSales)
                    {
                        Sale sale = new Sale()
                        {
                            // depending on the business logic, we could consider providing some default value here if they are not correctly filled in:
                            // Tax = double.TryParse(xmlSale.Descendants("tax").Single().Value, out double parsedTax) ? parsedTax : 20,   // <-- default 20 % tax
                            // but probably safer to just not parse incorrect data

                            Name = xmlSale.Descendants("name").Single().Value,
                            Date = DateTime.Parse(xmlSale.Descendants("date").Single().Value),
                            Price = double.Parse(xmlSale.Descendants("price").Single().Value),
                            Tax = double.Parse(xmlSale.Descendants("tax").Single().Value)
                        };
                        sales.Add(sale);
                    }

                    // fill in table
                    salesDataGrid.ItemsSource = sales;

                    // calculate and show weekend results
                    IEnumerable<SeriesSale> weekendSales = sales.Where(s => s.Date.DayOfWeek == DayOfWeek.Saturday || s.Date.DayOfWeek == DayOfWeek.Sunday)
                                                                .GroupBy(s => s.Name)
                                                                .Select(group => new SeriesSale
                                                                {
                                                                    Name = group.Key,
                                                                    TotalPrice = group.Sum(s => s.Price),
                                                                    TotalPriceWithTax = group.Sum(s => s.PriceWithTax)
                                                                });

                    weekendTotalSalesDataGrid.ItemsSource = weekendSales;

                    ErrorMessageTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // todo: log exception?
                ErrorMessageTextBox.Text = string.Format("Došlo k chybě: {0}", ex.Message);
                salesDataGrid.ItemsSource = null;
                weekendTotalSalesDataGrid.ItemsSource = null;
            }
        }
    }
}
