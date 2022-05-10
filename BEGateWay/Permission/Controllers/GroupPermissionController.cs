using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.respond;
using Permission.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.Controllers
{
    [Authorize(Policy = "admin")]
    [ApiController]
    [Route("/grouppermission")]
    public class GroupPermissionController :ControllerBase
    {
        private IGroupPermission m_group_permission;
        public GroupPermissionController( IGroupPermission igp ) {
            m_group_permission = igp;
        }
        [HttpGet]
        public DataRespond getAll()
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "Success";
                data.data = m_group_permission.getAll();

            }
            catch (Exception e)
            {
                data.success = false;
                data.message = "fail";
                data.error = e;
            }
            return data;
        }
        [HttpPost]
        public DataRespond createGroupPermission([FromBody] GroupPermissions g)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "Success";
                g.createday = DateTime.Now;
                data.data = g;
                m_group_permission.insert(g);

            }
            catch (Exception e)
            {
                data.success = false;
                data.message = "fail";
                data.error = e;
            }
            return data;

        }
        [HttpPost("removeGroupPermission")]
        public DataRespond remove([FromBody] GroupPermissions g)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                
                m_group_permission.deleteGroupPermission(g);
                

            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
    }
}
