using System.Windows;
using System;
using System.Windows.Controls;
using System.Data;
using System.Text.RegularExpressions;

namespace Calculator.Views
{
    public partial class StandardView : UserControl
    {
        public StandardView()
        {
            InitializeComponent();
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            InputText.Text = "";
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            MemoryText.Text = "";
            InputText.Text = "";
            OutputText.Text = "";
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputText.Text))
                return;

            if (InputText.Text.EndsWith("d"))
                InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 3);
            else
                InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);

            if (InputText.Text == "0")
                InputText.Text = "";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (InputText.Text == "0")
                InputText.Text = "";
            InputText.Text += button.Content.ToString();
        }

        private void SymbolButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            InputText.Text += button.Content.ToString();
        }

        private void PowButton_Click(object sender, RoutedEventArgs e)
        {
            InputText.Text += '^';
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            MemoryText.Text = InputText.Text;
            InputText.Text = OutputText.Text.Replace(',', '.');
        }

        private void InputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateResult();
        }

        private void UpdateResult()
        {
            string expression = InputText.Text.ToString();
            try
            {
                expression = ProcessPowerOperations(expression);
                expression = ProcessFactorials(expression);
                expression = ReplaceSymbol(expression);
                expression = ProcessPercentOperations(expression);
                expression = expression.Replace("mod", "%");
                CalculateAndDisplayResult(expression);
            }
            catch (Exception)
            {
                OutputText.Text = "Error";
            }
        }

        private string ReplaceSymbol(string oldString)
        {
            string newString;
            newString = oldString.Replace("×", "*");
            newString = newString.Replace("÷", "/");
            return newString;
        }

        private string ProcessPercentOperations(string expression)
        {
            return Regex.Replace(expression, @"(\d+)%", match =>
            {
                double num = double.Parse(match.Groups[1].Value);
                double result = num / 100;
                return result.ToString().Replace(',', '.');
            });
        }

        private string ProcessFactorials(string expression)
        {
            return Regex.Replace(expression, @"(\d+)!", match =>
            {
                int num = int.Parse(match.Groups[1].Value);
                int result = CalculateFactorial(num);
                return result.ToString();
            });
        }
        private int CalculateFactorial(int n)
        {
            if (n == 0)
                return 1;
            return n * CalculateFactorial(n - 1);
        }

        private string ProcessPowerOperations(string expression)
        {
            Match powerMatch = Regex.Match(expression, @"(\d+(\.\d+)?)(\^)(\d+(\.\d+)?)");
            if (powerMatch.Success)
            {
                double baseNumber = double.Parse(powerMatch.Groups[1].Value);
                double exponent = double.Parse(powerMatch.Groups[4].Value);
                double result = Math.Pow(baseNumber, exponent);
                return expression.Replace(baseNumber.ToString() + "^" + exponent.ToString(), result.ToString());
            }
            return expression;
        }

        private void CalculateAndDisplayResult(string expression)
        {
            DataTable table = new DataTable();
            object result = table.Compute(expression, "");
            OutputText.Text = result.ToString();
        }
    }
}