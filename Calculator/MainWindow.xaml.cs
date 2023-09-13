using Calculator.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitialInstallationOfTheTheme();

        }
        private int currentThemeIndex;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            WorkingArea.Opacity = 1;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            WorkingArea.Opacity = 0.3;
        }
        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainButton.IsChecked = false;
        }

        private void OverTheWindowsButton_Checked(object sender, RoutedEventArgs e)
        {
            Topmost = true;
        }

        private void OverTheWindowsButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Topmost = false;
        }
        private void InitialInstallationOfTheTheme() //
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string filePath = Path.Combine(folderPath, "Data", "UITheme.xml");
            LoadCurrencyRatesFromXml(filePath);
            AppTheme.ChangeTheme(themes[currentThemeIndex]);
        }

        List<Uri> themes = new List<Uri> //
        {
            new Uri("UIThemes/DarkTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/KiwiTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/LightTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/OrangeTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/PayloadTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/RiverTheme.xaml", UriKind.Relative),
        };

        private int GetSelectedThemeIndex(XmlDocument xmlDoc) //
        {
            XmlNode themeSelectedNode = xmlDoc.SelectSingleNode("/UITheme/ThemeSelected");
            if (themeSelectedNode != null)
            {
                int themeIndex;
                if (int.TryParse(themeSelectedNode.InnerText, out themeIndex))
                {
                    return themeIndex;
                }
            }
            return 0; // Возвращаем индекс темы по умолчанию, если узел не найден или значение некорректно
        }
        private void LoadCurrencyRatesFromXml(string filePath) //
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                currentThemeIndex = GetSelectedThemeIndex(xmlDoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных из XML: " + ex.Message);
            }
        }
    }
}