using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace RegistrationWindow.UserControls
{

    public class PasswordPlaceHolderControl : TextBox
    {
        private string _realPassword = string.Empty;
        private bool _isPlaceholderActive = true;
        private static string _placeHolderString;

        // DependencyProperty для плейсхолдера
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string),
            typeof(PasswordPlaceHolderControl), new PropertyMetadata(_placeHolderString));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string),
            typeof(PasswordPlaceHolderControl), new FrameworkPropertyMetadata(string.Empty,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged));

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }
        public string PlaceHolderString { get; set; }

        public PasswordPlaceHolderControl()
        {
            _placeHolderString = PlaceHolderString;
            Loaded += OnLoaded;
            GotFocus += OnGotFocus;
            LostFocus += OnLostFocus;
            TextChanged += OnTextChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Password))
            {
                ShowPlaceholder();
            }
            else
            {
                ShowRealPassword();
            }
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (_isPlaceholderActive)
            {
                ClearAndShowRealPassword();
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_realPassword))
            {
                ShowPlaceholder();
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isPlaceholderActive) return;


            var cursorPosition = CaretIndex;
            var newText = Text;

            if (newText.Length > _realPassword.Length)
            {
                var addedChar = newText[cursorPosition - 1];
                _realPassword = _realPassword.Insert(cursorPosition - 1, addedChar.ToString());
            }
            else if (newText.Length < _realPassword.Length)
            {
                if (cursorPosition < _realPassword.Length)
                {
                    _realPassword = _realPassword.Remove(cursorPosition, 1);
                }
                else if (cursorPosition > 0 && cursorPosition <= _realPassword.Length)
                {
                    _realPassword = _realPassword.Remove(cursorPosition - 1, 1);
                }
            }


            UpdateDisplay();
            Password = _realPassword;
            CaretIndex = Math.Min(cursorPosition, Text.Length);
        }

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PasswordPlaceHolderControl)d;
            var newPassword = e.NewValue as string ?? string.Empty;

            if (!control._isPlaceholderActive && control._realPassword != newPassword)
            {
                control._realPassword = newPassword;
                control.UpdateDisplay();
            }
        }
        private void UpdateDisplay()
        {
            if (_isPlaceholderActive) return;
            Text = new string('*', _realPassword.Length);
        }

        private void ShowPlaceholder()
        {
            _isPlaceholderActive = true;
            _realPassword = string.Empty;
            Text = Placeholder;
            Foreground = Brushes.Gray;
            Password = string.Empty;
        }

        private void ShowRealPassword()
        {
            _isPlaceholderActive = false;
            _realPassword = Password ?? string.Empty;
            UpdateDisplay();
            Foreground = Brushes.Black;
        }

        private void ClearAndShowRealPassword()
        {
            _isPlaceholderActive = false;
            Text = string.Empty;
            Foreground = Brushes.Black;
            UpdateDisplay();
        }
    }
}