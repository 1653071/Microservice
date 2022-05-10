
using AuthService1.Models;
using Microsoft.IdentityModel.Tokens;
using ModelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService1.service
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfig _config;
        public AccessTokenGenerator(AuthenticationConfig configuration)
        {
            _config = configuration;
                    
        }
        public string GeneratorToken(User user) {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.AccessTokenSecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim("role",user.Role.ToString())
                
            };  
            JwtSecurityToken token = new JwtSecurityToken(_config.Issuer, _config.Audience,claims,DateTime.UtcNow,DateTime.UtcNow.AddMinutes(_config.ExpiredMinute),credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
