using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using System.Text.Json;
using System.Net.Http;
using System.Windows;
using Lab4.Windows;
using Lab4.Models;
using System.Linq;
using System;


namespace Lab4.Windows
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private Response response;
        private MainWindow mainWindow;
        private HttpClient _httpClient; // Объявляем _httpClient здесь

        public ObservableCollection<PaymentRecordModel> myCollection { get; set; }

        public Main(Response response, MainWindow mainWindow)
        {
            InitializeComponent();
            _httpClient = new HttpClient(); // Инициализируем _httpClient здесь
            this.response = response;
            this.mainWindow = mainWindow;
            myCollection = new ObservableCollection<PaymentRecordModel>();
        }
        private async void showMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = NameOrNumber.Text;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7252/debtor-statement?search={searchQuery}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var debtor = JsonSerializer.Deserialize<PaymentRecordModel>(jsonResponse);

                    myListBox.Items.Clear();

                    if (debtor != null)
                    {
                        myCollection.Add(debtor);
                        myListBox.ItemsSource = myCollection;
                    }
                    else
                    {
                        MessageBox.Show("Должники не найдены.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при выполнении запроса.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске должников: {ex.Message}");
            }
        }

        private async void deleteExpiredButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7252/debtors");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var DebtOrOverpayment = JsonSerializer.Deserialize<PaymentRecordModel>(jsonResponse);

                    myListBox.Items.Clear();

                    foreach (var debtor in jsonResponse)
                    {
                        myCollection.Add(DebtOrOverpayment);
                    }

                    myListBox.ItemsSource = myCollection;
                }
                else
                {
                    MessageBox.Show("Ошибка при выполнении запроса.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении списка должников: {ex.Message}");
            }
        }

        private void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void medicineCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}