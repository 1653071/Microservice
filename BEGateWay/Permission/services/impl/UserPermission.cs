using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.connection;
using ModelClassLibrary.repo.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.services.impl
{
    public class UserPermission : PermissionRepository<UserPermissions>  , IUserPermission
    {
        public UserPermission(PermissionContext context) : base(context)
        {

        }
        public dynamic getUserInGroup(int groupid)
        {
            var userper = from user in m_context.UsersTable
                          join userpermission in m_context.UserPermissions
                          on user.Id equals userpermission.usid
                          join groups in m_context.Groups.Where(m => m.groupid == groupid)
                          on userpermission.groupid equals groups.groupid
                          select new
                          {
                              user,
                              groups,
                              userpermission
                          };
            return userper;
        }

        public dynamic getUserNotInGroup(int groupid)
        {
            var userper = from user in m_context.UsersTable
                          where !(from gr in m_context.Groups.Where(m => m.groupid == groupid)
                                  join userpermission in m_context.UserPermissions
                                  on gr.groupid equals userpermission.groupid
                                  select userpermission.usid).Contains(user.Id)
                          select new
                          {
                              user
                          };
            return userper;
        }
        public void removeUserFromGroup(UserPermissions up)
        {
            var userper = m_context.UserPermissions.Where(m => m.usid == up.usid && m.groupid == up.groupid).FirstOrDefault();
            m_context.Remove(userper);
            save();
        
        }
        
    }
}
