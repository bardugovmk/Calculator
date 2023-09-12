using System.Windows;
using System;
using System.Windows.Controls;
using System.Data;
using System.Text.RegularExpressions;

namespace Calculator.Views
{
    public partial class StandardView : UserControl
    {
        private DataTable table; 
        public StandardView()
        {
            table = new DataTable();
            InitializeComponent();
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            InputText.Text = "0";
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            MemoryText.Text = "0";
            InputText.Text = "0";
            OutputText.Text = "0";
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputText.Text.Length > 0 && InputText.Text != "0" && InputText.Text[InputText.Text.Length-1] != 'd' && InputText.Text!="")
                InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);
            else if (InputText.Text.Length!=0 && InputText.Text[InputText.Text.Length - 1] == 'd')
                InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 3);
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

        private string ReplaceSymbol(string oldString)
        {
            string newString;
            newString = oldString.Replace("×", "*");
            newString = newString.Replace("÷", "/");
            newString = newString.Replace("mod", "%");

            return newString;
        }

        private void UpdateResult()
        {
            string expression = InputText.Text.ToString();
            try
            {
                expression = ReplaceSymbol(expression);

                // Заменяем операции с процентами на соответствующие выражения
                expression = Regex.Replace(expression, @"(\d+)%", match =>
                {
                    double num = double.Parse(match.Groups[1].Value);
                    double result = num / 100;
                    return result.ToString().Replace(',','.');
                });

                expression = Regex.Replace(expression, @"(\d+)!", match =>
                {
                    int num = int.Parse(match.Groups[1].Value);
                    int result = CalculateFactorial(num);
                    return result.ToString();
                });

                Match powerMatch = Regex.Match(expression, @"(\d+(\.\d+)?)(\^)(\d+(\.\d+)?)");
                if (powerMatch.Success)
                {
                    double baseNumber = double.Parse(powerMatch.Groups[1].Value);
                    double exponent = double.Parse(powerMatch.Groups[4].Value);
                    double result = Math.Pow(baseNumber, exponent);
                    expression = expression.Replace(baseNumber.ToString() + "^" + exponent.ToString(), result.ToString());
                    OutputText.Text = table.Compute(expression, "").ToString();
                }
                else
                {
                    object result = table.Compute(expression, "");
                    OutputText.Text = result.ToString();
                }
            }
            catch (Exception)
            {
                OutputText.Text = "Error";
            }
        }

        private int CalculateFactorial(int n)
        {
            if (n == 0)
                return 1;
            return n * CalculateFactorial(n - 1);
        }
    }
}