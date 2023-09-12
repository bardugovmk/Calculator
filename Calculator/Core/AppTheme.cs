using System;
using System.Windows;

namespace Calculator.Core
{
    internal class AppTheme
    {
        public static void ChangeTheme(Uri themeuri)
        {
            ResourceDictionary Theme = new ResourceDictionary()
            {
                Source = themeuri
            };

            //App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(Theme);
        }
    }
}