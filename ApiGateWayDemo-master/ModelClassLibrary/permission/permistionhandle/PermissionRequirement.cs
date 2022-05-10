using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
namespace ModelClassLibrary.permission.permistionhandle
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(List<string> permission)
        {
            Permission = permission;
        }

        public List<string> Permission { get; }
        public enum PermissionEnum
        {
            User,
            Permistion
        }
    }
}
