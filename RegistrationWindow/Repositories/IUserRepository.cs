using RegistrationWindow.Data.Models;

public interface IUserRepository
{
    Task<User> GetByLoginAsync(string login);
    Task<bool> IsLoginExistsAsync(string login);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task SaveChangesAsync();
}