using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelClassLibrary.area.auth
{
    public class Roles
    {
        [Key]
        public int roleid { get; set; }
        public string rolename { get; set; }
    }
}
