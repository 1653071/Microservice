using ModelClassLibrary.Models;
using ModelClassLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi1.Services
{
    public interface IUser: IReponsitory<User>
    {
        public Boolean createFirstUser(User user);
        public dynamic getByEmail(string email);
    }
}
