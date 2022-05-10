using AuthService1.service;
using Microsoft.AspNetCore.Mvc;

using ModelClassLibrary.iterface;
using ModelClassLibrary.Models;
using ModelClassLibrary.respond;
using System;


namespace AuthService1.Controllers
{
    [ApiController]
    [Route("v{v:apiversion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUser _userRepository;
        private IHashPass m_hashpass;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        public WeatherForecastController(IUser userRepository, IHashPass hashpass, AccessTokenGenerator accessTokenGenerator)
        {
            _userRepository = userRepository;
            m_hashpass = hashpass;
            _accessTokenGenerator = accessTokenGenerator;

        }

        [HttpPost("register")]
        public DataRespond Register([FromBody] User us)

        {
            DataRespond data = new DataRespond();
            if (_userRepository.checkUserExist(us.Username))
            {
                data.success = false;
                data.message = "Username exist in database";

            }
            else {
                try
                {
                    us.Password = m_hashpass.hashPass(us.Password);

                    //string requestedWith = HttpContext.Request.Headers["Authorization"];
                    data.success = true;
                    data.data = us;
                    data.message = "success register";
                    _userRepository.insert(us);
                }
                catch (Exception e)
                {
                    data.success = false;
                    data.error = e;
                    data.message = e.Message;
                }
            }
            return data;
        }
        [HttpPost("login")]
        public DataRespond Login([FromBody] User user)

        {
            AuthRespond authRespond = new AuthRespond();

            DataRespond dataRespond = new DataRespond();
            if (!_userRepository.checkUserExist(user.Username))
            {
                dataRespond.success = false;
                dataRespond.message = "Username not in database";

            }
                    
            else
            {
                User existingUser = _userRepository.getByEmail(user.Username);
                if (!m_hashpass.checkPass(existingUser.Password, user.Password))
                {
                    dataRespond.success = false;
                    dataRespond.message = "Pass fail";
                }
                else
                {
                    string accessToken = _accessTokenGenerator.GeneratorToken(existingUser);
                    dataRespond.success = true;
                    authRespond.user = existingUser;
                    authRespond.token = accessToken;
                    dataRespond.data = authRespond;
                    dataRespond.message = "Login success";
                }
            }

            return dataRespond;
        }
    }
}