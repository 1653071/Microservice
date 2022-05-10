using ModelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelClassLibrary.respond
{
    public class AuthRespond
    {
        public User user { get; set; }
        public string token { get; set; }
    }
}
