using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService1.Models
{
    public class AuthenticationConfig
    {
        public string AccessTokenSecret { get; set; }
        public int ExpiredMinute { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
