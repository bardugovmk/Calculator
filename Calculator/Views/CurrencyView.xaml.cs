using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Calculator.Views
{
    public partial class CurrencyView : UserControl
    {
        double currencyData_BelarusianRuble; 
        double currencyData_ChineseYuan;
        double currencyData_Euro;
        double currencyData_USDollar;

        public CurrencyView()
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
                int selectedItem2 = CurrencyUnitsAfterConverting.SelectedIndex;
                OutputText.Text = ConvertElement(num, selectedItem2);
            }
        }

        private void CurrencyUnitsConverting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double num;
            if (double.TryParse(InputText.Text, out num))
            {
                int selectedItem2 = CurrencyUnitsAfterConverting.SelectedIndex;
                OutputText.Text = ConvertElement(num, selectedItem2);
            }
        }
        private string ConvertElement(double input, int selectedItem1)
        {
            double result = 0;
            switch (selectedItem1)
            {
                case 0:
                    result = input / currencyData_USDollar;
                    break;
                case 1:
                    result = input / currencyData_BelarusianRuble;
                    break;
                case 2:
                    result = input / currencyData_ChineseYuan;
                    break;
                case 3:
                    result = input / currencyData_Euro;
                    break;
                default:
                    return "ERROR";
            }
            return result.ToString("0.##################");
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    string xml = web.DownloadString("https://www.cbr-xml-daily.ru/daily.xml");
                    XDocument doc = XDocument.Parse(xml);

                    UpdateCurrencyValue(doc, ref currencyData_BelarusianRuble, "Белорусский рубль");
                    UpdateCurrencyValue(doc, ref currencyData_ChineseYuan, "Китайский юань");
                    UpdateCurrencyValue(doc, ref currencyData_Euro, "Евро");
                    UpdateCurrencyValue(doc, ref currencyData_USDollar, "Доллар США");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        private void UpdateCurrencyValue(XDocument doc, ref double currencyValue, string currencyName)
        {
            XElement currencyElement = doc.Root.Elements("Valute").FirstOrDefault(el => el.Element("Name")?.Value == currencyName);

            if (currencyElement != null)
            {
                string valueStr = currencyElement.Element("Value")?.Value.Replace(",", ".");
                double.TryParse(valueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out currencyValue);
            }
            else
            {
                MessageBox.Show($"Информация о {currencyName} не найдена.");
            }
        }
    }
}