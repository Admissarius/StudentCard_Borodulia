using System.Windows;
using System.Windows.Controls;

namespace CurrencyConverter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            // Проверка корректности ввода
            if (!decimal.TryParse(InputValue.Text, out decimal amount))
            {
                ResultText.Text = "Ошибка: введите число.";
                return;
            }

            string selectedConversion = ((ComboBoxItem)ConversionSelector.SelectedItem)?.Content.ToString();
            decimal result = 0;

            switch (selectedConversion)
            {
                case "BYN → USD": result = amount * 0.30m; break;
                case "USD → BYN": result = amount * 3.33m; break;
                case "BYN → EUR": result = amount * 0.28m; break;
                case "EUR → BYN": result = amount * 3.57m; break;
                case "BYN → RUB": result = amount * 30.5m; break;
                default:
                    ResultText.Text = "Выберите направление конвертации.";
                    return;
            }

            ResultText.Text = $"Результат: {result:F2}";
        }
    }
}