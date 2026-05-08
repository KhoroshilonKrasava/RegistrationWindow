using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RegistrationWindow.Services
{
    public interface IUIService 
    {
        Task InvokeOnUIThread(Action action);
    }

    public class UIService : IUIService
    {
        public async Task InvokeOnUIThread(Action action)
        {
            await Application.Current.Dispatcher.InvokeAsync(action);
        }
    }
    public interface IPasswordFieldService
    {
        Task AddConfirmPasswordField();
    }

    public class PasswordFieldService : IPasswordFieldService
    {
        private readonly IUIService _uiService;
        private int count = 0;

        public PasswordFieldService(IUIService uiService)
        {
            _uiService = uiService;
        }

        public async Task AddConfirmPasswordField()
        {
            await _uiService.InvokeOnUIThread(() =>
            {
                if (count is not >= 1)
                {
                    var dynamicTextBox = new TextBox();
                    count++;

                    var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (mainWindow != null)
                    {
                        dynamicTextBox.Style = (Style)mainWindow.Resources["TextBox"];
                        dynamicTextBox.SetBinding(TextBox.TextProperty, new Binding("ConfirmPassword") { Mode = BindingMode.TwoWay }); 
                        int index = mainWindow.MainPanel.Children.IndexOf(mainWindow.targetElement);
                        mainWindow.MainPanel.Children.Insert(index + 1, dynamicTextBox);
                    }
                }
            });
        }
    }

}
