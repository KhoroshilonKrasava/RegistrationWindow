using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationWindow.DataBase.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

    }
}
