using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using System.Net.NetworkInformation;

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
            
            Loaded += PageLoaded;
            
            string folderPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string filePath = Path.Combine(folderPath, "Data", "CurrencyRates.xml");
            LoadCurrencyRatesFromXml(filePath);
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
            if (InputText.Text != "")
            {
                Button button = (Button)sender;
                if (button.Content.ToString() == "±" && InputText.Text[0] != '-')
                    InputText.Text = "-" + InputText.Text;
                else
                    InputText.Text = InputText.Text.Remove(0, 1);
            }
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

        private void UpdateCourse_Click(object sender, RoutedEventArgs e)
        {
            if (IsInternetAvailable())
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

                        SaveCurrencyRatesToXml();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Отсутствует подключение к интернету. Пожалуйста, проверьте ваше соединение и повторите попытку.");
            }
        }

        private bool IsInternetAvailable()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                return true;
            }
            return false;
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
        private void SaveCurrencyRatesToXml()
        {
            try
            {
                string folderPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                string filePath = Path.Combine(folderPath, "Data", "CurrencyRates.xml");

                XDocument doc = new XDocument(
                    new XElement("CurrencyRates",
                        new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd")),
                        new XElement("Currency",
                            new XElement("Code", "USD"),
                            new XElement("Rate", currencyData_USDollar.ToString("0.##################"))),
                        new XElement("Currency",
                            new XElement("Code", "BYN"),
                            new XElement("Rate", currencyData_BelarusianRuble.ToString("0.##################"))),
                        new XElement("Currency",
                            new XElement("Code", "CNY"),
                            new XElement("Rate", currencyData_ChineseYuan.ToString("0.##################"))),
                        new XElement("Currency",
                            new XElement("Code", "EUR"),
                            new XElement("Rate", currencyData_Euro.ToString("0.##################")))
                    )
                );

                doc.Save(filePath);
                MessageBox.Show("Данные о курсах валют успешно сохранены в XML-файл.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении данных: " + ex.Message);
            }
        }

        private void LoadCurrencyRatesFromXml(string filePath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                currencyData_BelarusianRuble = GetCurrencyRate(xmlDoc, "BYN");
                currencyData_ChineseYuan = GetCurrencyRate(xmlDoc, "CNY");
                currencyData_Euro = GetCurrencyRate(xmlDoc, "EUR");
                currencyData_USDollar = GetCurrencyRate(xmlDoc, "USD");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных из XML: " + ex.Message);
            }
        }

        private double GetCurrencyRate(XmlDocument xmlDoc, string currencyCode)
        {
            XmlNodeList currencyNodes = xmlDoc.SelectNodes("/CurrencyRates/Currency");
            foreach (XmlNode currencyNode in currencyNodes)
            {
                XmlNode codeNode = currencyNode.SelectSingleNode("Code");
                XmlNode rateNode = currencyNode.SelectSingleNode("Rate");

                if (codeNode != null && rateNode != null && codeNode.InnerText == currencyCode)
                {
                    string rateStr = rateNode.InnerText.Replace(",", ".");
                    double rate;
                    if (double.TryParse(rateStr, NumberStyles.Any, CultureInfo.InvariantCulture, out rate))
                    {
                        return rate;
                    }
                }
            }
            return 1.0;
        }

        private void CourseUpdaDteate_TextChanged(object sender, TextChangedEventArgs e)
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string filePath = Path.Combine(folderPath, "Data", "CurrencyRates.xml");

            try
            {
                XDocument doc = XDocument.Load(filePath);

                string lastUpdateDate = ExtractLastUpdateDate(doc);

                CourseUpdaDteate.Text = lastUpdateDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private string ExtractLastUpdateDate(XDocument doc)
        {
            XElement dateElement = doc.Root.Element("Date");
            if (dateElement != null)
            {
                return dateElement.Value;
            }
            return "Дата не найдена";
        }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            CourseUpdaDteate_TextChanged(null, null); 
        }
    }
}