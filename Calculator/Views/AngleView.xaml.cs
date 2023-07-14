using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Views
{
    public partial class AngleView : UserControl
    {
        public AngleView()
        {
            InitializeComponent();
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            InputText.Text = "";
        }
        
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputText.Text.Length > 0)
                InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);
        }
        
        private void NumberButton_Click(object sender, RoutedEventArgs e)
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
                InputText.Text = InputText.Text.Remove(0,1);
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (!InputText.Text.Contains(","))
                InputText.Text += button.Content.ToString();
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
                int selectedItem1 = AngleUnitsBeforeConverting.SelectedIndex;
                int selectedItem2 = AngleUnitsAfterConverting.SelectedIndex;
                OutputText.Text = ConvertElement(num, selectedItem1, selectedItem2).ToString();
            }
        }
        private void AngleUnitsConverting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double num;
            if (double.TryParse(InputText.Text, out num))
            {
                int selectedItem1 = AngleUnitsBeforeConverting.SelectedIndex;
                int selectedItem2 = AngleUnitsAfterConverting.SelectedIndex;
                OutputText.Text = ConvertElement(num, selectedItem1, selectedItem2).ToString();
            }
        }
         
        private string ConvertElement(double input, int selectedItem1, int selectedItem2)
        {
            if ((selectedItem1 == selectedItem2) ||
                (selectedItem1 == selectedItem2) ||
                (selectedItem1 == selectedItem2))
                return input.ToString();

            else if (selectedItem1 == 0 && selectedItem2 == 1)
                return (input * (180.0 / Math.PI)).ToString();

            else if (selectedItem1 == 0 && selectedItem2 == 2)
                return (input * (200.0 / Math.PI)).ToString();

            else if (selectedItem1 == 1 && selectedItem2 == 0)
                return (input * (Math.PI / 180.0)).ToString();

            else if (selectedItem1 == 1 && selectedItem2 == 2)
                return ((input * 200.0) / 180.0).ToString();

            else if (selectedItem1 == 2 && selectedItem2 == 0)
                return (input * (Math.PI / 200.0)).ToString();

            else if (selectedItem1 == 2 && selectedItem2 == 1)
                return ((input * 180.0) / 200.0).ToString();

            return "ERROR"; 
        }
    }
}