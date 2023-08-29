using System.Windows;
using System;
using System.Windows.Controls;

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
            InputText.Text = "0";
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            InputText.Text = "0";
            OutputText.Text = "0";
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputText.Text.Length > 0 && InputText.Text != "0" && InputText.Text[InputText.Text.Length-1] != 'd')
                InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);
            else if (InputText.Text[InputText.Text.Length - 1] == 'd')
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

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == "±" && InputText.Text[0] != '-')
                InputText.Text = "-" + InputText.Text;
            else
                InputText.Text = InputText.Text.Remove(0, 1);
        }

        private void PowButton_Click(object sender, RoutedEventArgs e)
        {
            InputText.Text += '^';
        }

        private void InputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputText != null && OutputText != null)
            {
                double num;
                try
                {
                    num = Convert.ToDouble(InputText.Text);
                }
                catch (FormatException)
                {
                    num = 0;
                }
                OutputText.Text = Calculation(num);
            }
        }
        private string Calculation(double input)
        {
            return "0";
        }
    }
}