using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationWindow.Data;
using RegistrationWindow.Repositories;
using RegistrationWindow.Services;
using RegistrationWindow.VM;
using Microsoft.Extensions.Hosting;
using RegistrationWindow.Helpers;

namespace RegistrationWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        private IConfiguration _configuration;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            var services = new ServiceCollection();

            services.AddDbContext<UsersContext>(options =>
                  options.UseMySql(
                      _configuration.GetConnectionString("DefaultConnection"),
                      new MySqlServerVersion(new Version(8, 0, 0)) 
                  )
              );
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHashService, PasswordHashService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindow.Show();
        }
    }

}