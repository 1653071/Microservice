using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelClassLibrary.connection;
using ModelClassLibrary.iterface;
using ModelClassLibrary.Models;
using ModelClassLibrary.DataRespond;
using SampleApi1.context;
using SampleApi1.Models;
using SampleApi1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelClassLibrary.respond;

namespace SampleApi2.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepository;


        private IList<User> _userModels;
        private IHashPass m_hashpass;
        public UserController(IUser userRepository, IHashPass hashpass)
        {
          
            _userRepository = userRepository;
            m_hashpass = hashpass;


        }
        [Authorize]
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
  
        [HttpPost("insert")]
        public DataRespond  insertUser([FromBody] User us)
        {
            DataRespond data = new DataRespond();
            us.Password = m_hashpass.hashPass(us.Password);
            
          
            
            try
            {
                
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
                data.error = e.Message;
            }
            return data;
            
        }
        [HttpDelete("delete")]
        public dynamic deleteUser([FromBody] User us)
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
        [HttpPut("update")]
        public DataRespond updateUser([FromBody] User rq)
        {
            DataRespond data = new DataRespond();
            try
            {
               
                data.success = true;
                data.message = "update success";
                User users = _userRepository.getById(rq.Id);

                users.Name = rq.Name;
                users.Email = rq.Email;
                

                
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


        private void CreateDemoData()
        {   
            _userModels = new List<User>();

            for (int i = 0; i < 5; i++)
            {
                var index = i + 1;
                _userModels.Add(new User
                {
                  
                  
                    Email = $"quahasyd{index}@gmail.com",
                    Id = i,
                    Name = $"123{index}"
                });
            }
        }
    }
}
