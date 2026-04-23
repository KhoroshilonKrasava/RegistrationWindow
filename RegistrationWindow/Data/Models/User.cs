using System;
using System.Collections.Generic;

namespace RegistrationWindow.Data.Models;

public  class User
{
    public uint Id { get; set; }

    public string Login { get; set; } = null!;
    
    public string PasswordHash { get; set; } = null!;
}
