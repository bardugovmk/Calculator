using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Views
{
    public partial class AreaView : UserControl
    {
        public AreaView()
        {
            InitializeComponent();
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            InputText.Text = "0";
        }
        
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputText.Text.Length > 0)
                InputText.Text = InputText.Text.Substring(0, InputText.Text.Length - 1);
        }
        
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (InputText.Text == "0")
                InputText.Text = "";
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
                int selectedItem1 = AreaUnitsBeforeConverting.SelectedIndex;
                int selectedItem2 = AreaUnitsAfterConverting.SelectedIndex;
                OutputText.Text = ConvertElement(num, selectedItem1, selectedItem2);
            }
        }
        
        private void AreaUnitsConverting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double num;
            if (double.TryParse(InputText.Text, out num))
            {
                int selectedItem1 = AreaUnitsBeforeConverting.SelectedIndex;
                int selectedItem2 = AreaUnitsAfterConverting.SelectedIndex;
                OutputText.Text = ConvertElement(num, selectedItem1, selectedItem2);
            }
        }
        private string ConvertElement(double input, int selectedItem1, int selectedItem2)
        {
            if (selectedItem1 == selectedItem2)
            {
                return input.ToString();
            }
            else
            {
                double result = 0;
                if (selectedItem1 == 0)
                {
                    switch (selectedItem2)
                    {
                        case 1:
                            result = input / 100;
                            break;
                        case 2:
                            result = input / 1000000.0;
                            break;
                        case 3:
                            result = input / 10000000000.0;
                            break;
                        case 4:
                            result = input / 1000000000000;
                            break;
                        case 5:
                            result = input / 645.16;
                            break;
                        case 6:
                            result = input / 92903.04;
                            break;
                        case 7:
                            result = input / 836127.36;
                            break;
                        case 8:
                            result = input / 40468564.224;
                            break;
                        case 9:
                            result = input / 2589988110.336;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 1)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input / 0.01;
                            break;
                        case 2:
                            result = input / 10000;
                            break;
                        case 3:
                            result = input / 100000000;
                            break;
                        case 4:
                            result = input / 10000000000;
                            break;
                        case 5:
                            result = input / 6.4516;
                            break;
                        case 6:
                            result = input / 929.0304;
                            break;
                        case 7:
                            result = input / 8361.2736;
                            break;
                        case 8:
                            result = input / 4046.8564224;
                            break;
                        case 9:
                            result = input / 258998.8110336;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 2)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input / 0.000001;
                            break;
                        case 1:
                            result = input / 0.0001;
                            break;
                        case 3:
                            result = input / 10000;
                            break;
                        case 4:
                            result = input / 1000000;
                            break;
                        case 5:
                            result = input * 1550.0031;
                            break;
                        case 6:
                            result = input * 10.7639;
                            break;
                        case 7:
                            result = input * 1.19599;
                            break;
                        case 8:
                            result = input * 0.000247105;
                            break;
                        case 9:
                            result = input * 0.000000386102159;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 3)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input / 0.000000000001;
                            break;
                        case 1:
                            result = input / 0.0000000001;
                            break;
                        case 2:
                            result = input / 0.000001;
                            break;
                        case 4:
                            result = input / 100;
                            break;
                        case 5:
                            result = input * 15500031;
                            break;
                        case 6:
                            result = input * 107639.1041671;
                            break;
                        case 7:
                            result = input * 11959.9;
                            break;
                        case 8:
                            result = input * 2.4710538147;
                            break;
                        case 9:
                            result = input * 0.0038610216;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 4)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input / 0.000000000001;
                            break;
                        case 1:
                            result = input / 0.0000000001;
                            break;
                        case 2:
                            result = input / 0.000001;
                            break;
                        case 3:
                            result = input / 0.001;
                            break;
                        case 5:
                            result = input * 1550003100.0062;
                            break;
                        case 6:
                            result = input * 10763910.41671;
                            break;
                        case 7:
                            result = input * 1195990;
                            break;
                        case 8:
                            result = input * 247.10538147;
                            break;
                        case 9:
                            result = input * 0.386102159;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 5)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input * 645.16;
                            break;
                        case 1:
                            result = input * 6.4516;
                            break;
                        case 2:
                            result = input * 0.00064516;
                            break;
                        case 3:
                            result = input * 0.000000064516;
                            break;
                        case 4:
                            result = input * 0.00000000064516;
                            break;
                        case 6:
                            result = input * 0.00694444;
                            break;
                        case 7:
                            result = input * 0.00077161791;
                            break;
                        case 8:
                            result = input * 0.0000001594225;
                            break;
                        case 9:
                            result = input * 0.000000000249098;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 6)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input / 0.0000107639;
                            break;
                        case 1:
                            result = input / 0.00107639;
                            break;
                        case 2:
                            result = input * 0.09290304;
                            break;
                        case 3:
                            result = input * 0.0000009290304;
                            break;
                        case 4:
                            result = input * 0.000000009290304;
                            break;
                        case 5:
                            result = input * 144;
                            break;
                        case 7:
                            result = input * 11.959900463;
                            break;
                        case 8:
                            result = input * 0.0000229568;
                            break;
                        case 9:
                            result = input * 0.000000035870064;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 7)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input / 0.000119599;
                            break;
                        case 1:
                            result = input / 0.0119599;
                            break;
                        case 2:
                            result = input / 0.83612736;
                            break;
                        case 3:
                            result = input / 0.000000083612736;
                            break;
                        case 4:
                            result = input / 0.00000000083612736;
                            break;
                        case 5:
                            result = input / 1550.0031;
                            break;
                        case 6:
                            result = input / 9;
                            break;
                        case 8:
                            result = input / 0.0002066116;
                            break;
                        case 9:
                            result = input * 0.0000002066116;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 8)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input / 0.0002471054;
                            break;
                        case 1:
                            result = input / 0.02471054;
                            break;
                        case 2:
                            result = input / 4046.8564224;
                            break;
                        case 3:
                            result = input / 0.0000404686;
                            break;
                        case 4:
                            result = input / 0.00000040468564;
                            break;
                        case 5:
                            result = input / 6272640;
                            break;
                        case 6:
                            result = input / 43560;
                            break;
                        case 7:
                            result = input / 4840;
                            break;
                        case 9:
                            result = input / 0.0015625;
                            break;
                        default:
                            return "ERROR";
                    }
                }
                else if (selectedItem1 == 9)
                {
                    switch (selectedItem2)
                    {
                        case 0:
                            result = input / 2589988110.336;
                            break;
                        case 1:
                            result = input / 25899881.10336;
                            break;
                        case 2:
                            result = input / 2589988.110336;
                            break;
                        case 3:
                            result = input / 258.9988110336;
                            break;
                        case 4:
                            result = input / 2.589988110336;
                            break;
                        case 5:
                            result = input / 4014489599.9992;
                            break;
                        case 6:
                            result = input / 27878400;
                            break;
                        case 7:
                            result = input / 3097600;
                            break;
                        case 8:
                            result = input / 640;
                            break;
                        default:
                            return "ERROR";

                    }
                }
                return result.ToString("0.##################");
            }
        }

    }
}