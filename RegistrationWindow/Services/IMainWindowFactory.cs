using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RegistrationWindow.VM;

namespace RegistrationWindow.Services
{
    public interface IMainWindowFactory
    {
        MainWindow Create();
    }

    public class MainWindowFactory : IMainWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MainWindow Create()
        {
            var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

            var mainWindow = new MainWindow(viewModel);

            return mainWindow;
        }
    }
}
