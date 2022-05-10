using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService1.service
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
