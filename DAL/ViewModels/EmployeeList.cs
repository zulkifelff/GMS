using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class EmployeeList
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string FatherName { get; set; }
        //public Nullable<int> Nationality { get; set; }
        //public Nullable<bool> Gender { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string EmployeeNumber { get; set; }
        public string CardNumber { get; set; }
        public string Password { get; set; }
        //public Nullable<int> UserTypeId { get; set; }
        public string UserType { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        //public int? DepartmentId { get; set; }
        //public int? DesignationId { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string ProfilePicture { get; set; }
    }
}
