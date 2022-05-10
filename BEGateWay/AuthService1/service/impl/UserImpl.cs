using ModelClassLibrary.connection;
using ModelClassLibrary.iterface;
using ModelClassLibrary.Models;
using ModelClassLibrary.Repo.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService1.service.impl
{
    public class UserImpl : Reponsitory<User>, IUser
    {
        private IHashPass m_hashPass;


        public UserImpl(PermissionContext context, IHashPass hashPass) : base(context)
        {
            m_hashPass = hashPass;

        }



        public Boolean createFirstUser(User user)
        {
            user.Password = m_hashPass.hashPass(user.Password);
            insert(user);
            return true;

        }
        public dynamic getByEmail(string username)
        {
            return getAll().Where(m => m.Username == username).FirstOrDefault();
        }
        public bool checkUserExist(string username)
        {
            List<User> user = getAll().Where(m => m.Username == username).ToList();
            if (user.Count == 0)
            {
                return false;
            }
            return true;
        }
    }

}
