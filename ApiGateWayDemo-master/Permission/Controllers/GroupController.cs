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
    [Route("/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IGroup m_group;
        public GroupController(IGroup group) {
            m_group = group;
        }
        [HttpGet]
        public DataRespond getAll() {
            DataRespond data = new DataRespond();
            try {
                data.success = true;
                data.message = "Success";
                data.data= m_group.getAll();
            
            } catch(Exception e) {
                data.success = false;
                data.message = "fail";
                data.error = e;
            }
            return data;
        }
        [HttpPost]
        public DataRespond createGroup([FromBody] Groups g) {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "Success";
              
                data.data = g;
                m_group.insert(g);

            }
            catch (Exception e)
            {
                data.success = false;
                data.message = "fail";
                data.error = e;
            }
            return data;
        
        }
    }
}
