using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RegistrationWindow.UserControls
{
    public class PlaceHolderUserControl : TextBox
    {
        public string Placeholder { get; set; } = "Login";
        public PlaceHolderUserControl()
        {
            GotFocus += OnGotFocus;
            LostFocus += OnLostFocus;
            Text += Placeholder;
            Foreground = Brushes.Gray;
        }
        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (Text == Placeholder)
            {
                Text = "";
                Foreground = Brushes.Black;
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                Text = Placeholder;
                Foreground = Brushes.Gray;
            }
        }
    }
}
