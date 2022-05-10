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
    [Route("/userpermission")]
    [ApiController]
    
    public class UserPermissionController : ControllerBase
    {
        private IUserPermission m_permistion;



        public UserPermissionController(IUserPermission permission)
        {
            m_permistion = permission;
        }
        [Authorize(Policy = "admin")]
        [HttpGet("getUserNotInGroup/{groupid:int}")]
        public DataRespond getUserNotInGroup(int groupid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                data.data = m_permistion.getUserNotInGroup(groupid);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [HttpGet("getUserInGroup/{groupid:int}")]
        public DataRespond getUserInGroup123(int groupid)
            {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                data.data = m_permistion.getUserInGroup(groupid);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [HttpGet]
        public DataRespond getAllPermission()
        {
            DataRespond dataRespond = new DataRespond();
            try
            {
                dataRespond.success = true;
                dataRespond.data = m_permistion.getAll();

            }
            catch (Exception e)
            {
                dataRespond.success = false;
                dataRespond.message = e.Message;
                dataRespond.error = e;
            }
            return dataRespond;
        }
        [HttpPost("insert")]
        public DataRespond addUserToGroup([FromBody] UserPermissions p)
        {

            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "add success";
                p.createday = DateTime.Now;
                m_permistion.insert(p);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;

            }
            return data;


        }
        [HttpPost("remove")]
        public DataRespond deleteUserFromGroup([FromBody] UserPermissions p)
        {

            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "remove success";
               
                m_permistion.removeUserFromGroup(p);
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
