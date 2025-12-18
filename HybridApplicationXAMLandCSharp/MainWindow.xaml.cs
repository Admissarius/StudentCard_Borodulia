using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace HybridApplicationXAMLandCSharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAllDynamicControls();
        } // mw()


        /// <summary>
        /// метод инициализации всех динамических элементов управления
        /// </summary>

        private void InitializeAllDynamicControls()
        {
            DynamicContentPanel.Children.Clear();

            CreateFormControls();        // Форма ввода
            CreateInteractiveControls();    // Интерактивные элементы
            CreateDataControls();        // Таблицы данных
            CreateUserInteractionControls();   // Динамическое добавление

            // Кнопка обновления
            Button refreshButton = new Button()
            {
                Content = "Обновить все элементы",
                Width = 150,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0)
            };

            refreshButton.Click += (s, e) => InitializeAllDynamicControls();

            DynamicContentPanel.Children.Add(refreshButton);
        }

        /// <summary>
        /// Пример 1: Динамическая форма ввода 
        /// </summary>
        private void CreateFormControls()
        {
            // Группа для формы
            GroupBox formGroup = new GroupBox();
            formGroup.Header = "Динамическая форма";
            formGroup.Margin = new Thickness(0, 0, 0, 15);

            Grid formGrid = new Grid();
            formGrid.Margin = new Thickness(10);

            // Создание столбцов
            formGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            formGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // Создание строк
            for (int i = 0; i < 4; i++)
            {
                formGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
            }

            // Поле имени
            Label nameLabel = new Label() { Content = "Имя:" };
            Grid.SetRow(nameLabel, 0); Grid.SetColumn(nameLabel, 0);

            TextBox nameTextBox = new TextBox() { Margin = new Thickness(5) };
            Grid.SetRow(nameTextBox, 0); Grid.SetColumn(nameTextBox, 1);

            // Поле email
            Label emailLabel = new Label() { Content = "Email:" };
            Grid.SetRow(emailLabel, 1); Grid.SetColumn(emailLabel, 0);

            TextBox emailTextBox = new TextBox() { Margin = new Thickness(5) };
            Grid.SetRow(emailTextBox, 1); Grid.SetColumn(emailTextBox, 1);

            // Поле телефона
            Label phoneLabel = new Label() { Content = "Телефон:" };
            Grid.SetRow(phoneLabel, 2); Grid.SetColumn(phoneLabel, 0);

            TextBox phoneTextBox = new TextBox() { Margin = new Thickness(5) };
            Grid.SetRow(phoneTextBox, 2); Grid.SetColumn(phoneTextBox, 1);

            // Кнопка отправки
            Button submitButton = new Button()
            {
                Content = "Отправить",
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5)
            };
            submitButton.Click += SubmitButton_Click;
            Grid.SetRow(submitButton, 3); Grid.SetColumn(submitButton, 1);

            // Добавление элементов в форму
            formGrid.Children.Add(nameLabel);
            formGrid.Children.Add(nameTextBox);
            formGrid.Children.Add(emailLabel);
            formGrid.Children.Add(emailTextBox);
            formGrid.Children.Add(phoneLabel);
            formGrid.Children.Add(phoneTextBox);
            formGrid.Children.Add(submitButton);

            formGroup.Content = formGrid;
            DynamicContentPanel.Children.Add(formGroup);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Grid formGrid = (Grid)button.Parent;

            // Получение значений из текстовых полей
            string name = ((TextBox)formGrid.Children[1]).Text;
            string email = ((TextBox)formGrid.Children[3]).Text;
            string phone = ((TextBox)formGrid.Children[5]).Text;

            MessageBox.Show($"Данные отправлены!\nИмя: {name}\nEmail: {email}\nТелефон: {phone}", "Результат");
        }

        /// <summary>
        /// Пример 2: Интерактивные элементы управления
        /// </summary>
        private void CreateInteractiveControls()
        {
            // Группа для интерактивных элементов
            GroupBox interactiveGroup = new GroupBox();
            interactiveGroup.Header = "Интерактивные элементы";
            interactiveGroup.Margin = new Thickness(0, 0, 0, 15);

            StackPanel interactivePanel = new StackPanel();
            interactivePanel.Margin = new Thickness(10);

            // CheckBox
            CheckBox checkBox = new CheckBox()
            {
                Content = "Согласен с условиями",
                IsChecked = true,
                Margin = new Thickness(0, 0, 0, 10)
            };
            checkBox.Checked += (s, e) => { /* Логика при выборе */ };
            checkBox.Unchecked += (s, e) => { /* Логика при снятии выбора */ };

            // RadioButtons
            StackPanel radioPanel = new StackPanel() { Orientation = Orientation.Vertical };
            TextBlock radioLabel = new TextBlock() { Text = "Выберите вариант:", Margin = new Thickness(0, 0, 0, 5) };

            RadioButton radio1 = new RadioButton() { Content = "Вариант 1", IsChecked = true };
            RadioButton radio2 = new RadioButton() { Content = "Вариант 2" };
            RadioButton radio3 = new RadioButton() { Content = "Вариант 3" };

            radioPanel.Children.Add(radioLabel);
            radioPanel.Children.Add(radio1);
            radioPanel.Children.Add(radio2);
            radioPanel.Children.Add(radio3);

            // ComboBox
            ComboBox comboBox = new ComboBox()
            {
                Margin = new Thickness(0, 10, 0, 10),
                Width = 200
            };
            comboBox.Items.Add("Элемент 1");
            comboBox.Items.Add("Элемент 2");
            comboBox.Items.Add("Элемент 3");
            comboBox.SelectedIndex = 0;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;

            // Slider
            StackPanel sliderPanel = new StackPanel();
            TextBlock sliderValue = new TextBlock() { Text = "Значение: 50" };
            Slider slider = new Slider()
            {
                Width = 200,
                Minimum = 0,
                Maximum = 100,
                Value = 50
            };
            slider.ValueChanged += (s, e) => {
                sliderValue.Text = $"Значение: {slider.Value:F0}";
            };

            sliderPanel.Children.Add(sliderValue);
            sliderPanel.Children.Add(slider);

            // Добавление всех элементов
            interactivePanel.Children.Add(checkBox);
            interactivePanel.Children.Add(radioPanel);
            interactivePanel.Children.Add(comboBox);
            interactivePanel.Children.Add(sliderPanel);

            interactiveGroup.Content = interactivePanel;
            DynamicContentPanel.Children.Add(interactiveGroup);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedItem = comboBox.SelectedItem?.ToString();

            // Динамическое создание сообщения
            TextBlock message = new TextBlock()
            {
                Text = $"Выбран: {selectedItem}",
                Foreground = Brushes.Blue,
                Margin = new Thickness(0, 5, 0, 0)
            };

            // Добавляем сообщение в панель
            StackPanel parentPanel = (StackPanel)comboBox.Parent;
            parentPanel.Children.Add(message);

            // Автоматическое удаление через 3 секунды
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (s, args) => {
                parentPanel.Children.Remove(message);
                timer.Stop();
            };
            timer.Start();
        }
        private void CreateDataControls()
        {
            // Группа для данных
            GroupBox dataGroup = new GroupBox();
            dataGroup.Header = "Динамическая таблица";
            dataGroup.Margin = new Thickness(0, 0, 0, 15);

            StackPanel dataPanel = new StackPanel();
            dataPanel.Margin = new Thickness(10);

            // Кнопка для создания таблицы
            Button createTableButton = new Button()
            {
                Content = "Создать таблицу",
                Width = 120,
                Margin = new Thickness(0, 0, 0, 10)
            };
            createTableButton.Click += CreateTableButton_Click;

            // Контейнер для таблицы
            Border tableContainer = new Border()
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(5)
            };

            dataPanel.Children.Add(createTableButton);
            dataPanel.Children.Add(tableContainer);

            dataGroup.Content = dataPanel;
            DynamicContentPanel.Children.Add(dataGroup);
        }

        /// <summary>
        /// Пример 3: Динамическая таблица данных 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel parentPanel = (StackPanel)button.Parent;
            Border tableContainer = (Border)parentPanel.Children[1];

            // Очистка предыдущей таблицы
            tableContainer.Child = null;

            // Создание новой таблицы
            Grid dynamicGrid = new Grid();
            dynamicGrid.ShowGridLines = true;

            // Создание столбцов и строк
            for (int i = 0; i < 4; i++)
            {
                dynamicGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 6; i++)
            {
                dynamicGrid.RowDefinitions.Add(new RowDefinition());
            }

            // Заголовки столбцов
            string[] headers = { "ID", "Наименование", "Цена", "Количество" };
            for (int col = 0; col < 4; col++)
            {
                TextBlock header = new TextBlock()
                {
                    Text = headers[col],
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5)
                };
                Grid.SetRow(header, 0);
                Grid.SetColumn(header, col);
                dynamicGrid.Children.Add(header);
            }

            // Заполнение данными
            Random random = new Random();
            for (int row = 1; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    FrameworkElement element;

                    if (col == 0) // ID
                    {
                        element = new TextBlock()
                        {
                            Text = row.ToString(),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(5)
                        };
                    }
                    else if (col == 1) // Наименование
                    {
                        element = new TextBox()
                        {
                            Text = $"Товар {row}",
                            Margin = new Thickness(2)
                        };
                    }
                    else if (col == 2) // Цена
                    {
                        element = new TextBox()
                        {
                            Text = (random.Next(100, 1000)).ToString(),
                            Margin = new Thickness(2)
                        };
                    }
                    else // Количество
                    {
                        element = new ComboBox()
                        {
                            Margin = new Thickness(2)
                        };
                        for (int i = 1; i <= 10; i++)
                        {
                            ((ComboBox)element).Items.Add(i);
                        }
                     ((ComboBox)element).SelectedIndex = 0;
                    }

                    Grid.SetRow(element, row);
                    Grid.SetColumn(element, col);
                    dynamicGrid.Children.Add(element);
                }
            }

            tableContainer.Child = dynamicGrid;
        }


        private void CreateUserInteractionControls()
        {
            GroupBox interactionGroup = new GroupBox();
            interactionGroup.Header = "Динамическое добавление";
            interactionGroup.Margin = new Thickness(0, 0, 0, 15);

            StackPanel interactionPanel = new StackPanel();
            interactionPanel.Margin = new Thickness(10);

            // Поле для ввода имени нового элемента
            StackPanel inputPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            TextBox newItemTextBox = new TextBox()
            {
                Width = 200,
                Margin = new Thickness(0, 0, 10, 0),
                ToolTip = "Введите название элемента"
            };

            Button addButton = new Button()
            {
                Content = "Добавить",
                Width = 80
            };

            inputPanel.Children.Add(newItemTextBox);
            inputPanel.Children.Add(addButton);

            // Контейнер для динамически добавляемых элементов
            StackPanel itemsContainer = new StackPanel()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };

            // Обработчик добавления
            addButton.Click += (s, e) =>
            {
                string itemName = newItemTextBox.Text;
                if (!string.IsNullOrWhiteSpace(itemName))
                {
                    AddDynamicItem(itemsContainer, itemName);
                    newItemTextBox.Text = string.Empty;
                    newItemTextBox.Focus();
                }
            };

            // Обработчик нажатия Enter в TextBox
            newItemTextBox.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    addButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            };

            interactionPanel.Children.Add(inputPanel);
            interactionPanel.Children.Add(itemsContainer);
            interactionGroup.Content = interactionPanel;
            DynamicContentPanel.Children.Add(interactionGroup);
        }

        /// <summary>
        /// Пример 4: Динамическое добавление элементов по действию пользователя 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="itemName"></param>

        private void AddDynamicItem(StackPanel container, string itemName)
        {
            StackPanel itemPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            itemPanel.Margin = new Thickness(0, 2, 0, 2);

            CheckBox checkBox = new CheckBox() { VerticalAlignment = VerticalAlignment.Center };
            TextBlock textBlock = new TextBlock()
            {
                Text = itemName,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5, 0, 10, 0)
            };

            Button deleteButton = new Button()
            {
                Content = "Удалить",
                Width = 60,
                Tag = itemPanel // Сохраняем ссылку на родительский контейнер
            };

            deleteButton.Click += (s, e) => {
                Button btn = (Button)s;
                StackPanel parent = (StackPanel)btn.Tag;
                container.Children.Remove(parent);
            };

            itemPanel.Children.Add(checkBox);
            itemPanel.Children.Add(textBlock);
            itemPanel.Children.Add(deleteButton);

            container.Children.Add(itemPanel);
        }

    }
}
