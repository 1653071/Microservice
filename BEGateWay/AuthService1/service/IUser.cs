using ModelClassLibrary.Models;
using ModelClassLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService1.service
{
    public interface IUser:IReponsitory<User>
    {
        public Boolean createFirstUser(User user);
        public dynamic getByEmail(string email);
        Boolean checkUserExist(string username);
    }
}
