using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Humanizer;

namespace RegistrationWindow.Helpers
{
    public interface IMessageService
    {
        void ShowStatusMessage(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
    public class MessageService : IMessageService
    {
        public void ShowStatusMessage(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
