using ModelClassLibrary.Repo.impl;
using ModelClassLibrary.Models;
using SampleApi2.Services;
using System.Linq;
using System.Threading.Tasks;

using System;
using ModelClassLibrary.iterface;
using Microsoft.Extensions.Caching.Memory;
using ModelClassLibrary.connection;
using System.Collections.Generic;

namespace SampleApi2.Services.impl
{
    public class UserImpl : Reponsitory<User>, IUser
    {
        private IHashPass m_hashPass;
       

        public UserImpl(PermissionContext context, IHashPass hashPass) : base(context)
        {
            m_hashPass = hashPass;
            
        }


        public bool checkUserExist(string email)
        {
            List<User> user = getAll().Where(m => m.Username == email).ToList();
            if (user.Count == 0)
            {
                return false;
            }
            return true;
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
