using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace StudentCard_Borodulia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnLoadPhoto.Click += BtnLoadPhoto_Click;
        }

        private void BtnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.png)|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Заполните обязательные поля!");
                return;
            }

            string gender = rbMale.IsChecked == true ? "Мужской" : "Женский";
            string message = $"Студент: {txtFirstName.Text} {txtLastName.Text}\n" +
            $"Возраст: {txtAge.Text}\nПол: {gender}\n" +
            $"Email: {txtEmail.Text}\nТелефон: {txtPhone.Text}\n" +
            $"Курс: {(cmbCourse.SelectedItem as ComboBoxItem)?.Content}\n" +
            $"Специализация: {(cmbSpecialization.SelectedItem as ComboBoxItem)?.Content}";

            MessageBox.Show(message, "Данные сохранены");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sldPerformance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //текущее значение слайдера
            txtPerformanceValue.Text = ((int)e.NewValue).ToString();
        }

    }
}
