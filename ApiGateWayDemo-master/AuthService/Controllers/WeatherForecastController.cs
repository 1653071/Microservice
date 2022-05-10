using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase

    {
        private readonly UserManager<User> _userRepository;
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest) {
            if(!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            if (registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse("Password does not match confirm password."));
            }

            User registrationUser = new User()
            {
                Email = registerRequest.Email
            };

            IdentityResult result = await _userRepository.CreateAsync(registrationUser, registerRequest.Password);
            if(!result.Succeeded)
            {
                IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();
                IdentityError primaryError = result.Errors.FirstOrDefault();
                
                if(primaryError.Code == nameof(errorDescriber.DuplicateEmail))
                {
                    return Conflict(new ErrorResponse("Email already exists."));
                }
                
            }

            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest) { 

        }
    }
    
}
