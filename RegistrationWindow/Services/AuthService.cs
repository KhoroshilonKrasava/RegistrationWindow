using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using RegistrationWindow.Data.Models;

namespace RegistrationWindow.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
       




        public AuthService(IUserRepository userRepository, IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
           
        }
        public async Task<AuthResult> LoginAsync(string login, string password)
        {
            try
            {
                var user = await _userRepository.GetByLoginAsync(login);
                if (user is null)
                {

                    return new AuthResult
                    {
                        IsSuccess = false,
                        Message = "Login no found"
                    };
                }
                if (!_passwordHashService.VerifyPassword(password, user.PasswordHash))
                {
                    return new AuthResult
                    {
                        IsSuccess = false,
                        Message = "Неверный пароль"
                    };
                }
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
                return new AuthResult
                {
                    IsSuccess = true,
                    Message = "Вход выполнен успешно",
                    User = user
                };
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    IsSuccess = false,
                    Message = $"Ошибка при входе: {ex.Message}"
                };
            }



        }

        public async Task<AuthResult> RegisterAsync(string login, string password)
        {
            try
            {

                if (await _userRepository.IsLoginExistsAsync(login))
                {
                    return new AuthResult
                    {
                        IsSuccess = false,
                        Message = "Login already used",

                    };
                }
                if (string.IsNullOrEmpty(password) || password.Length < 6)
                {
                    return new AuthResult
                    {
                        IsSuccess = false,
                        Message = "Password is wrong"
                    };
                }

                var user = new User
                {
                    Login = login,
                    PasswordHash = _passwordHashService.HashPassword(password),
                };
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
                return new AuthResult
                {
                    IsSuccess = true,
                    Message = "Reggistration access",
                    User = user
                };

            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    IsSuccess = false,
                    Message = $"Registration is wrong: {ex.Message}"
                };
            }
        }
        //private int count = 0;

        //private TextBox dynamicTextBox = null;
        //public async Task AddConfirmPasswordFild()
        //{
        //    if (count is not >= 1)
        //    {
        //        dynamicTextBox = new TextBox();
        //        count++;
        //        dynamicTextBox.Style = (Style)_mainWindow.Resources["TextBox"];
        //        int index = _mainWindow.MainPanel.Children.IndexOf(_mainWindow.targetElement);
        //        _mainWindow.MainPanel.Children.Insert(index + 1, dynamicTextBox);
        //    }

        //}


    }
   
}
