using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelClassLibrary.connection;
using ModelClassLibrary.iterface;
using ModelClassLibrary.Models;
using ModelClassLibrary.DataRespond;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelClassLibrary.respond;
using SampleApi2.Services;
using ModelClassLibrary.permission.services;

namespace SampleApi1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepository;
        private IUserPermission user_permission;
        private IAuthorizationHandler auth_handler;



        private IHashPass m_hashpass;
        public UserController(IUser userRepository, IHashPass hashpass,IUserPermission userPermission,IAuthorizationHandler authHandler)
        {
           
            _userRepository = userRepository;
            m_hashpass = hashpass;
            user_permission = userPermission;
            auth_handler = authHandler;

        }
        [Authorize(Policy = "admin")]
        [HttpGet("list")]
        public DataRespond GetList()
        {

            DataRespond data = new DataRespond();




            try
            {

                //string requestedWith = HttpContext.Request.Headers["Authorization"];
                data.success = true;
                data.data = _userRepository.getAll();
                data.message = "success";

            }
            catch (Exception e)
            {
                data.success = false;
                data.error = e;
                data.error = e.Message;
            }
            return data;
        }


        [HttpGet("getByEmail")]
        public dynamic getByEmail(string email)
        {
            return _userRepository.getByEmail(email);
        }
        [HttpGet("getByID")]
        public dynamic getByID(int id)
        {
            return _userRepository.getById(id);
        }
   
        [Authorize(Policy = "admin")]
        
        [HttpPost("insert")]
        public DataRespond insertUser([FromBody] User us)
        {
            DataRespond data = new DataRespond();
            us.Password = m_hashpass.hashPass(us.Password);



            try
            {
                if (_userRepository.checkUserExist(us.Username))
                {
                    data.success = false;
                    data.message = "Username is exist!";
                    return data;
                }

                //string requestedWith = HttpContext.Request.Headers["Authorization"];
                data.success = true;
                data.data = us;
                data.message = "success";
                _userRepository.insert(us);
            }
            catch (Exception e)
            {   
                data.success = false;
                data.error = e;
                data.message = e.Message;
            }
            return data;

        }
        [Authorize(Policy = "admin")]
        [HttpDelete("delete")]
        public DataRespond deleteUser([FromBody] User us)
        {

            DataRespond data = new DataRespond();
            
            try
            {
                

                //string requestedWith = HttpContext.Request.Headers["Authorization"];
                data.success = true;

                data.message = "delete success";
                _userRepository.delete(us.Id);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = "delete fail";
                data.error = e;
                data.error = e.Message;
            }
            return data;


        }

        //[HttpGet("info/{accountName}")]
        //public UserModel GetUserInfo(string accountName)
        //{
        //    return _userModels.FirstOrDefault(c => c.AccountName == accountName);
        //}
        [Authorize(Policy = "admin")]
        [HttpPut("update")]
        public DataRespond updateUser([FromBody] User rq)
        {
            DataRespond data = new DataRespond();
            try
            {

                data.success = true;
                data.message = "update success";
                User users = _userRepository.getById(rq.Id);
                users.Phone = rq.Phone;
                users.Address = rq.Address;
                users.Avatar = rq.Avatar;
                



                _userRepository.update(users);
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

