using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RegistrationWindow.DataBase.Models;

namespace RegistrationWindow.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users => Set<UserModel>();
        public ApplicationContext() => Database.EnsureCreated();

        static readonly string connectionString = "Server=localhost; User ID=root; Password=123456; Database=MonitorDataBase";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
