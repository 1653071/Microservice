using AuthService1.Models;
using ModelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService1.service
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> Create(User user);
    }
}
