using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL.IRepositories
{
    interface IDepartment
    {
        IEnumerable<DepartmentList> GetDepartments();
        Department GetDepartmentById(int? departmentId);
        void InsertDepartment(Department department);
        void DeleteDepartment(int? departmentId, int userId);
        void UpdateDepartment(Department department);
        void Save();
    }
}
