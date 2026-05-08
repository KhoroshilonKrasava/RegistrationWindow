using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using RegistrationWindow.Commands;
using RegistrationWindow.Helpers;
using RegistrationWindow.Services;

namespace RegistrationWindow.VM
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly IPasswordFieldService _passwordField;

        private string _login;
        private string _password;
        private string _confirmPassword;
        private bool _isLoading;
        private bool _registrationFlag = false;
        private string _statusMessage;
        IMessageService _messageService;
        // Commands
        public ICommand EnterButton { get; }
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand AddConfirmPasswordFild { get; }



        public MainWindowViewModel(IAuthService authService, IMessageService messageService, IPasswordFieldService passwordField)
        {
            _authService = authService;
            _messageService = messageService;
            _passwordField = passwordField;

            EnterButton = new Commands.AsyncRelayCommand(async _ => await ExecuteEnterButton(), CanExecuteEnterButton);
            RegisterCommand = new Commands.AsyncRelayCommand(async _ => await ExecuteRegisterAsync(), CanExecuteRegister);
            LoginCommand = new Commands.AsyncRelayCommand(async _ => await ExecuteLoginAsync(), CanExecuteLogin);
            AddConfirmPasswordFild = new Commands.AsyncRelayCommand(async async_ => await AddConfirmPasswordFildCommand(), CanExecuteAddConfirmFildCommand);

        }
        #region
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private async Task ExecuteEnterButton()
        {
            await (_registrationFlag ? ExecuteRegisterAsync() : ExecuteLoginAsync());
        }
        private async Task ExecuteLoginAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Выполняется авторизация...";

                var result = await _authService.LoginAsync(Login, Password);

                if (result.IsSuccess)
                {
                    StatusMessage = "Авторизация успешна! Вход.";
                    _messageService.ShowStatusMessage(StatusMessage, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    ExecuteClear();
                }
                else
                {
                    StatusMessage = result.Message;
                    _messageService.ShowStatusMessage(result.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Критическая ошибка: {ex.Message}";
                _messageService.ShowStatusMessage(StatusMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                IsLoading = false;
            }
        }
        private async Task ExecuteRegisterAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Выполняется регистрация...";

                var result = await _authService.RegisterAsync(Login, Password);

                if (result.IsSuccess)
                {
                    StatusMessage = "Регистрация успешна! Теперь вы можете войти.";
                    _messageService.ShowStatusMessage(StatusMessage, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    ExecuteClear();
                }
                else
                {
                    StatusMessage = result.Message;
                    _messageService.ShowStatusMessage(result.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Критическая ошибка: {ex.Message}";
                _messageService.ShowStatusMessage(StatusMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                IsLoading = false;
            }
        }
        private async Task AddConfirmPasswordFildCommand()
        {
            _registrationFlag = true;
            await _passwordField.AddConfirmPasswordField();
        }
        private bool CanExecuteAddConfirmFildCommand(object parametr)
        {
            return true;
        }
        private bool CanExecuteEnterButton(object parameter)
        {
            if (_registrationFlag)
            {
                return !string.IsNullOrWhiteSpace(Login) &&
                       !string.IsNullOrWhiteSpace(Password) &&
                       !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                       Password.Length >= 6;
            }
            else
            {
                return
                  !string.IsNullOrWhiteSpace(Login) &&
                  !string.IsNullOrWhiteSpace(Password) &&
                  Password.Length >= 6;
            }
        }
        private bool CanExecuteRegister(object parameter)
        {
            return !IsLoading &&
                   !string.IsNullOrWhiteSpace(Login) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                   Password.Length >= 6 &&
                   Password == ConfirmPassword;
        }
        private bool CanExecuteLogin(object parameter)
        {
            return !IsLoading &&
                   !string.IsNullOrWhiteSpace(Login) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password.Length >= 6;
        }

        private void ExecuteClear()
        {
            Login = string.Empty;
            Password = string.Empty;
            StatusMessage = "Готов к работе";
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

