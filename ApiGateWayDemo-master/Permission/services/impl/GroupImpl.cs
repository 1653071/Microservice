using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.connection;
using ModelClassLibrary.repo.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.services.impl
{
    public class GroupImpl : PermissionRepository<Groups>, IGroup
    {
        public GroupImpl(PermissionContext context) : base(context)
        {

        }
    }
}
