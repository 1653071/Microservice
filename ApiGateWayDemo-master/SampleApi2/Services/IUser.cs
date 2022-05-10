using ModelClassLibrary.Models;
using ModelClassLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi2.Services
{
    public interface IUser: IReponsitory<User>
    {
        public Boolean createFirstUser(User user);
        public dynamic getByEmail(string email);
        public Boolean checkUserExist(string email);
    }
}
