using RegistrationWindow.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

        public Task<AuthResult> RegisterAsync(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
