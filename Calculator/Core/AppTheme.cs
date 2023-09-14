using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace Calculator.Core
{
    internal class AppTheme
    {
        private int currentThemeIndex;
        public static void ChangeTheme(Uri themeuri)
        {
            ResourceDictionary Theme = new ResourceDictionary()
            {
                Source = themeuri
            };
            App.Current.Resources.MergedDictionaries.Add(Theme);
        }
        List<Uri> themes = new List<Uri>
        {
            new Uri("UIThemes/DarkTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/KiwiTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/LightTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/OrangeTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/PayloadTheme.xaml", UriKind.Relative),
            new Uri("UIThemes/RiverTheme.xaml", UriKind.Relative),
        };
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



        public void InitialInstallationOfTheTheme() 
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string filePath = Path.Combine(folderPath, "Data", "UITheme.xml");

            LoadThemeFromXml(filePath);
            ChangeTheme(themes[currentThemeIndex]);
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
            return 0;
        }

    }
}