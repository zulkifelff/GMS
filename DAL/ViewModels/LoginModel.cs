using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
