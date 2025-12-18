using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            string[,] buttons = {
                { "C", "<-", "*", "/" },
                { "7", "8", "9", "-" },
                { "4", "5", "6", "+" },
                { "1", "2", "3", "=" },
                { "0", ",", "(", ")" }
            };

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    string content = buttons[i, j];
                    if (string.IsNullOrEmpty(content)) continue;

                    var btn = new Button
                    {
                        Content = content,
                        FontSize = 18,
                        Margin = new Thickness(2),
                        MinWidth = 50,
                        MinHeight = 40
                    };
                    btn.Click += Button_Click;
                    ButtonGrid.Children.Add(btn);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string value = (string)((Button)sender).Content;

            switch (value)
            {
                case "C":
                    InputBox.Text = "";
                    ResultText.Text = "";
                    return;

                case "<-":
                    if (!string.IsNullOrEmpty(InputBox.Text))
                        InputBox.Text = InputBox.Text.Substring(0, InputBox.Text.Length - 1);
                    return;

                case "=":
                    ComputeExpression();
                    return;

                case ",":
                    AppendDecimalSeparator();
                    return;

                case "+":
                case "-":
                case "*":
                case "/":
                    HandleOperator(value);
                    return;

                default:
                    InputBox.Text += value;
                    return;
            }
        }

        private void HandleOperator(string op)
        {
            if (string.IsNullOrEmpty(InputBox.Text))
                return;

            char last = InputBox.Text[InputBox.Text.Length - 1];

            if (last == '+' || last == '-' || last == '*' || last == '/')
            {
                InputBox.Text = InputBox.Text.Substring(0, InputBox.Text.Length - 1) + op;
            }
            else
            {
                InputBox.Text += op;
            }
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComputeExpression();
        }

        private void InputBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ComputeExpression();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                InputBox.Text = "";
                ResultText.Text = "";
                e.Handled = true;
            }
        }

        private void AppendDecimalSeparator()
        {
            string text = InputBox.Text;
            int i = text.Length - 1;
            while (i >= 0 && (char.IsDigit(text[i]) || text[i] == ',' || text[i] == '.')) i--;

            string currentNumber = text.Substring(i + 1);
            if (!currentNumber.Contains(",") && !currentNumber.Contains("."))
            {
                InputBox.Text += ",";
            }
        }

        private void ComputeExpression()
        {
            string expr = InputBox.Text;
            if (string.IsNullOrWhiteSpace(expr))
            {
                ResultText.Text = "";
                return;
            }

            if (TryCompute(expr, out decimal result))
                ResultText.Text = $"{expr} = {result.ToString("0.##", CultureInfo.CurrentCulture)}";
            else
                ResultText.Text = "";
        }

        private bool TryCompute(string expr, out decimal result)
        {
            result = 0m;
            try
            {
                string normalized = NormalizeExpression(expr);

                // Автозакрытие скобок
                int openCount = 0, closeCount = 0;
                foreach (char c in normalized)
                {
                    if (c == '(') openCount++;
                    else if (c == ')') closeCount++;
                }
                while (closeCount < openCount)
                {
                    normalized += ")";
                    closeCount++;
                }

                var dt = new DataTable();
                var obj = dt.Compute(normalized, "");

                if (obj is double d)
                    result = Convert.ToDecimal(d, CultureInfo.InvariantCulture);
                else if (obj is decimal m)
                    result = m;
                else if (obj is int ii)
                    result = ii;
                else
                    result = Convert.ToDecimal(obj, CultureInfo.InvariantCulture);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string NormalizeExpression(string expr)
        {
            var sb = new StringBuilder(expr.Length);
            char prev = '\0';

            foreach (char c in expr)
            {
                char cc = (c == ',') ? '.' : c;

                bool prevIsNumOrDot = char.IsDigit(prev) || prev == '.';
                bool prevIsClose = prev == ')';
                bool currIsOpen = cc == '(';
                bool currIsNumOrDot = char.IsDigit(cc) || cc == '.';

                if (currIsOpen && (prevIsNumOrDot || prevIsClose))
                    sb.Append('*');

                if (currIsNumOrDot && prevIsClose)
                    sb.Append('*');

                sb.Append(cc);
                prev = cc;
            }

            return sb.ToString().Trim();
        }
    }
}
