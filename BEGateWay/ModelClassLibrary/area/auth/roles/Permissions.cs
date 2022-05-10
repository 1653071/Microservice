using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelClassLibrary.area.auth.roles
{
    public class Permissions
    {
        [Key]
        public int perid { get; set; }
        public string pername { get; set; }
        public Boolean active { get; set; }
        public string policy { get; set; }
        public string link { get; set; }
        public string action { get; set; }
        public string note { get; set; }
        public int parent_id { get; set; }
        public int position { get; set; }
    }
}
