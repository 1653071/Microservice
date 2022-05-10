using ModelClassLibrary.area.auth.roles;

using ModelClassLibrary.Repo.impl;
using ModelClassLibrary.connection;
using ModelClassLibrary.repo.impl;
using System.Linq;

namespace Permission.services.impl
{
    public class PermissionImpl: PermissionRepository<Permissions>, IPermission
    {
        public PermissionImpl(PermissionContext context) : base(context)
        {

        }
        public dynamic getAllLanguage()
        {
            var listlang = (from tran in m_context.PermissionTranslations
                            join lang in m_context.Languages
                            on tran.languages equals lang.languages
                            select lang).GroupBy(
                                m => m.langname,
                                m => m.languages,
                                (key, g) => new
                                {
                                    key,
                                    lan = g.FirstOrDefault()
                                }
                           );
            return listlang;
        }

        public dynamic getCategoryByUser(int usid)
        {
           
            var menu =
                (from userper in m_context.UserPermissions.Where(m => m.usid == usid)
                 join groups in m_context.Groups
                 on userper.groupid equals groups.groupid
                 join groupper in m_context.GroupPermissions
                 on groups.groupid equals groupper.groupid
                 join perm in m_context.Permissions
                 on groupper.perid equals perm.perid
                
              
                 select new
                 {
                     perm.link,
                     perm.action,
               
                     perm.perid,
                     perm.parent_id,
                     perm.position
                 });
            //var user = m_context.UsersTable.FirstOrDefault(m => m.Id == usid);
            //if (user.Email == "admin")
            //{
            //    var x = from per in m_context.Permissions.Where(n => n.link == "admincontrolcomponent")
            //            select new
            //            {
            //                per.link,
            //                per.pername,
            //                per.action,
                         
            //                per.perid,
            //                per.parent_id,
            //                per.position
            //            };
            //    return x.Union(menu).Distinct().OrderBy(m => m.position);

            //}
            return menu.Distinct().OrderBy(m => m.position);
        }

        public dynamic getPerMissionByGroupId(int groupid)
        {
            var permission = from per in m_context.Permissions.Where(m => m.active == true)
                             join grper in m_context.GroupPermissions
                             on per.perid equals grper.perid
                             join gr in m_context.Groups.Where(m => m.groupid == groupid)
                             on grper.groupid equals gr.groupid
                             select new
                             {
                                 per,
                                 gr
                             };
            return permission;
        }
        public dynamic getPerMissionNotInGroup(int groupid)
        {
            var permission = from per in m_context.Permissions.Where(m => m.active == true)
                             where !(from gr in m_context.Groups.Where(m => m.groupid == groupid)
                                     join grper in m_context.GroupPermissions
                                     on gr.groupid equals grper.groupid
                                     select grper.perid).Contains(per.perid)
                             select new
                             {
                                 per
                             };
            return permission;
        }
    }
}
