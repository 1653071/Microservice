using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService1.Models
{
    public class ErrorResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }
        public ErrorResponse(string errorMassage) : this(new List<string>() { errorMassage }) { }
        public ErrorResponse(IEnumerable <string> errorMassage)
        {
            ErrorMessages = errorMassage;
           
        }
    }
}
