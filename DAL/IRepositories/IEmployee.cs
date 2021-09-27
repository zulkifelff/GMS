using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL.IRepositories
{
    interface IEmployee
    {
        IEnumerable<LookupModel> GetEmployees();
        Employee GetEmployeeById(int? employeeId);
        void InsertEmployee(Employee employee);
        void DeleteEmployee(int? employeeId, int userId);
        void UpdateEmployee(Employee employee);
        void Save();
    }
}
