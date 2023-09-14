using Calculator.Core;
using System.Windows;
using System.Windows.Input;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppTheme appTheme = new AppTheme();
            appTheme.InitialInstallationOfTheTheme();
        }

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
    }
}