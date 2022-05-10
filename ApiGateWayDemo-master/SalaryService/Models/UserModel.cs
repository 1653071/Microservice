using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryService.Models
{
    public class UserModel

    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Salary { get; set; }
        public string AccountName { get; set; }
    }
}
