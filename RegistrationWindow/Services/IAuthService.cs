using RegistrationWindow.Data.Models;

namespace RegistrationWindow.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string login, string password);
        Task<AuthResult> RegisterAsync(string login, string password);
        //Task AddConfirmPasswordFild();
    }

    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }
}