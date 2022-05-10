using ModelClassLibrary.Repo.impl;
using ModelClassLibrary.Models;
using SampleApi1.Services;
using System.Linq;
using System.Threading.Tasks;
using ModelClassLibrary.connection;
using System;
using ModelClassLibrary.iterface;
using Microsoft.Extensions.Caching.Memory;

namespace SampleApi1.Services.impl
{
    public class UserImpl : Reponsitory<User>, IUser
    {
        private IHashPass m_hashPass;
       

        public UserImpl(DataContext context, IHashPass hashPass) : base(context)
        {
            m_hashPass = hashPass;
            
        }

        

        public Boolean createFirstUser(User user) {
            user.Password = m_hashPass.hashPass(user.Password);
            insert(user);
            return true;

        }
        public dynamic getByEmail(string email) {
            return getAll().Where(m => m.Email == email).ToList();
        }
    }
}
