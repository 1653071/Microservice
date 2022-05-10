using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.respond;
using Permission.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.Controllers
{
    
    [Route("/permission")]
    [ApiController]
    
    public class PermissionController : ControllerBase
    {
        private IPermission m_permistion;

     

        public PermissionController(IPermission permission)
        {
            m_permistion = permission;
        }
        [Authorize(Policy = "admin")]
        [HttpGet]
        public DataRespond getAllPermission() {
            DataRespond dataRespond = new DataRespond();
            try {
                dataRespond.success = true;
                dataRespond.data = m_permistion.getAll();

            } catch(Exception e ) {
                dataRespond.success = false;
                dataRespond.message = e.Message;
                dataRespond.error = e;
            }
            return dataRespond;
        }
        [Authorize(Policy = "admin")]
        [HttpPost]
        public DataRespond addNewPermission([FromBody] Permissions p) {

            DataRespond data = new DataRespond();
            try {
                data.success = true;
                data.message = "add success";
                m_permistion.insert(p);
            }
            catch (Exception e) {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            
            }
            return data;
        
        


        }
        [Authorize(Policy = "admin")]
        [HttpGet("getPerMissionByGroupId/{groupid:int}")]
        public DataRespond getPerMissionByGroupId(int groupid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_permistion.getPerMissionByGroupId(groupid);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }

            return data;
        }
        [Authorize(Policy = "admin")]
        [HttpGet("getPerMissionNotInGroup/{groupid:int}")]
        public DataRespond getPerMissionNotInGroup(int groupid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_permistion.getPerMissionNotInGroup(groupid);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }

            return data;
        }
        [HttpGet("getAllPermissionByUser/{usid:int}")]
        public DataRespond getPermissionByUser(int usid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
               
                data.data = m_permistion.getCategoryByUser(usid);
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.success = false;
                data.error = e;
            }
            return data;
        }

    }
}
