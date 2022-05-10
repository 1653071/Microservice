using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.services
{
    public interface IPermission : IReponsitory<Permissions>
    {
        dynamic getCategoryByUser(int usid);
        dynamic getPerMissionByGroupId(int groupid);
        dynamic getPerMissionNotInGroup(int groupid);
        dynamic getAllLanguage();

    }
}
