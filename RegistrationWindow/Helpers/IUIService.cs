using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace RegistrationWindow.Helpers
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
}
