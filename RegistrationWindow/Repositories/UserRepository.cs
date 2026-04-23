using Microsoft.EntityFrameworkCore;
using RegistrationWindow.Data;
using RegistrationWindow.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationWindow.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;
        public UserRepository(UsersContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }
       
        public async Task<bool> IsLoginExistsAsync(string login)
        {
            return await _context.Users.AnyAsync(u => u.Login == login);
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await Task.CompletedTask;
        }
    }
}
