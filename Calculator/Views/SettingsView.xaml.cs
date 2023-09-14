using Calculator.Core;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Calculator.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            string folderPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string filePath = Path.Combine(folderPath, "Data", "UITheme.xml");
            LoadThemeFromXml(filePath);
            UIThemeController.SelectedIndex = currentThemeIndex;
        }
        private void GmailLinkButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto:bardugovmk@gmail.com");
        }
        private void LinkedinLinkButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("www.linkedin.com/in/mikhailbardziuhou");
        }
        private void TelegramLinkButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://t.me/MikhailBardziuhou");
        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/bardugovmk/Calculator");
        }


        private int currentThemeIndex;

        List<Uri> themes = new List<Uri>
        {
            new Uri("UIThemes/DarkTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/KiwiTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/LightTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/OrangeTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/PayloadTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/RiverTheme.xaml", UriKind.Relative),
        };
        private void UIThemeController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentThemeIndex = UIThemeController.SelectedIndex;
            AppTheme.ChangeTheme(themes[currentThemeIndex]);
            SaveThemeSelectedToXml();
        }
        private void SaveThemeSelectedToXml()
        {
            try
            {
                string folderPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                string filePath = Path.Combine(folderPath, "Data", "UITheme.xml");
                
                XDocument doc = new XDocument(
                    new XElement("UITheme",
                        new XElement("ThemeSelected", currentThemeIndex.ToString(""))
                    )
                );
                
                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении данных о выбранной теме: " + ex.Message);
            }
        }
        private int GetSelectedThemeIndex(XmlDocument xmlDoc)
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
        private void LoadThemeFromXml(string filePath)
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