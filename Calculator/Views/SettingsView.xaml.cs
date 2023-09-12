using Calculator.Core;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
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
        }
    }
}