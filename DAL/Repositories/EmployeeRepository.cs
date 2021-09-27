using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.IRepositories;
using System.Data.Entity;
using DAL.ViewModels;
using Common;
namespace DAL.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private GMSEntities _context;
        public EmployeeRepository(GMSEntities context)
        {
            this._context = context;
        }
        public IEnumerable<LookupModel> GetEmployees()
        {
            return _context.Employees.Where(x => x.IsActive == true)
                .Select(x => new LookupModel
                {
                    Id = x.EmployeeId,
                    Name = x.Name
                }).ToList();
        }
        public IEnumerable<EmployeeList> GetEmployeesDetailData()
        {
            return _context.Employees.Where(x => x.IsActive == true)
                .Select(x => new EmployeeList
                {
                    EmployeeCode = x.EmployeeCode,
                    EmployeeId = x.EmployeeId,
                    Name = x.Name,
                    Address = x.Address,
                    CardNumber = x.CardNumber,
                    CNIC = x.CNIC,
                    Phone=x.Phone,
                    Email = x.Email,
                    DateOfEmployment = x.DateOfEmployment,
                    Department = _context.Departments.Where(y => y.DepartmentId == x.DepartmentId).Select(z => z.Title).FirstOrDefault(),
                    Designation = _context.Designations.Where(y => y.DesignationId == x.DesignationId).Select(z => z.Title).FirstOrDefault(),
                    DOB = x.DOB,
                    EmployeeNumber = x.EmployeeNumber,
                    FatherName = x.FatherName,
                    Gender = x.Gender == true ? "Male" : "Female",
                    Mobile = x.Mobile,
                    Nationality = _context.Countries.Where(y => y.CountryId == x.Nationality).Select(z => z.Title).FirstOrDefault(),
                    Password = x.Password,
                    ProfilePicture = x.ProfilePicture == null ? ApplicationConstants.DefaultUserImagePath : x.ProfilePicture,
                    UserType = _context.UserTypes.Where(y => y.UserTypeId == x.UserTypeId).Select(z => z.Title).FirstOrDefault(),

                }).ToList();
        }
        public Employee GetEmployeeById(int? id)
        {
            return _context.Employees.Find(id);
        }
        public void InsertEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
        }
        public void DeleteEmployee(int? employeeId, int userId)
        {
            //Employee employee = _context.Employees.Find(employeeId);
            //_context.Employees.Remove(employee);
            Employee employee = _context.Employees.Find(employeeId);
            if (employee != null)
            {
                employee.IsActive = false;
                employee.UpdatedBy = userId;
                employee.UpdatedOn = DateTime.Now;
                Save();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee currEmployee = GetEmployeeById(employee.EmployeeId);
            if (currEmployee != null)
            {
                currEmployee.UpdatedBy = employee.UpdatedBy;
                currEmployee.UpdatedOn = employee.UpdatedOn;
                currEmployee.Name = employee.Name;
                currEmployee.Address = employee.Address;
                currEmployee.CardNumber = employee.CardNumber;
                currEmployee.CNIC = employee.CNIC;
                currEmployee.DateOfEmployment = employee.DateOfEmployment;
                currEmployee.DepartmentId = employee.DepartmentId;
                currEmployee.DesignationId = employee.DesignationId;
                currEmployee.DOB = employee.DOB;
                //currEmployee.EmployeeNumber = employee.EmployeeNumber;
                currEmployee.FatherName = employee.FatherName;
                currEmployee.Gender = employee.Gender;
                currEmployee.Mobile = employee.Mobile;
                currEmployee.Nationality = employee.Nationality;
                currEmployee.Phone = employee.Phone;
                currEmployee.ProfilePicture = employee.ProfilePicture;
                currEmployee.UserTypeId = employee.UserTypeId;
                currEmployee.Email = employee.Email;
                Save();
            }
            //_context.Entry(employee).State = EntityState.Modified;
        }
        public void ModifyEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }
        public void UpdateCredentials(Employee employee)
        {
            Employee currEmployee = GetEmployeeById(employee.EmployeeId);
            if (currEmployee != null)
            {
                currEmployee.UpdatedBy = employee.UpdatedBy;
                currEmployee.UpdatedOn = employee.UpdatedOn;
                currEmployee.EmployeeNumber = employee.EmployeeNumber;
                currEmployee.Password = employee.Password;
                Save();
            }
            //_context.Entry(employee).State = EntityState.Modified;
        }

        public bool IsUniqueEmployeeNumber(string employeeNumber, int employeeId)
        {
            Employee employee = _context.Employees.Where(x => x.EmployeeNumber == employeeNumber && x.EmployeeId != employeeId).FirstOrDefault();
            if (employee != null)
                return false;

            return true;
        }

        public bool IsUniqueEmail(string email, int employeeId)
        {
            Employee employee = _context.Employees.Where(x => x.Email == email && x.EmployeeId != employeeId).FirstOrDefault();
            if (employee != null)
                return false;

            return true;
        }
        public bool IsUniqueMobile(string mobile, int employeeId)
        {
            Employee employee = _context.Employees.Where(x => x.Mobile == mobile && x.EmployeeId != employeeId).FirstOrDefault();
            if (employee != null)
                return false;

            return true;
        }
        public bool IsUniqueCnic(string cnic, int employeeId)
        {
            Employee employee = _context.Employees.Where(x => x.CNIC == cnic && x.EmployeeId != employeeId).FirstOrDefault();
            if (employee != null)
                return false;

            return true;
        }
        public Employee Validate(string employeeNumber, string password)
        {
            return _context.Employees.Where(x => x.IsActive == true && x.EmployeeNumber == employeeNumber && x.Password == password)
                .FirstOrDefault();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
