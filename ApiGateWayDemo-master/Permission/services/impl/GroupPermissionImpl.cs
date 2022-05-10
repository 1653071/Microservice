using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.connection;
using ModelClassLibrary.repo.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.services.impl
{
    public class GroupPermissionImpl:PermissionRepository<GroupPermissions>, IGroupPermission
    {
        public GroupPermissionImpl(PermissionContext context) : base(context)
        {

        }
        public void deleteGroupPermission(GroupPermissions gr)
        {
            var grpr = m_context.GroupPermissions.FirstOrDefault(m => m.groupid == gr.groupid && m.perid == gr.perid);
            m_context.Remove(grpr);
            save();
        }
    }
}
