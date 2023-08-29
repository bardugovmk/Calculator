using System.Windows;
using System.Windows.Input;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            WorkingArea.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            WorkingArea.Opacity = 0.3;
        }
        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainBtn.IsChecked = false;
            
        }

        private void OverTheWindowsBtn_Checked(object sender, RoutedEventArgs e)
        {
            Topmost = true;
        }

        private void OverTheWindowsBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            Topmost = false;
        }
    }
}