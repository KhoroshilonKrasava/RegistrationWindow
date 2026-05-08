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
        private string _placeholder = "Login";

        public string Placeholder
        {
            get => _placeholder;
            set
            {
                _placeholder = value;
                if (string.IsNullOrWhiteSpace(Text) || Text == _placeholder)
                {
                    Text = _placeholder;
                }
            }
        }

        public PlaceHolderUserControl()
        {
            Loaded += OnLoaded;  // Используем Loaded вместо конструктора
            GotFocus += OnGotFocus;
            LostFocus += OnLostFocus;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                SetPlaceholder();
            }
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
                SetPlaceholder();
            }
        }

        private void SetPlaceholder()
        {
            Text = Placeholder;
            Foreground = Brushes.Gray;
        }
    }
}
