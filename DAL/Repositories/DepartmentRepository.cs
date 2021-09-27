using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.IRepositories;
using System.Data.Entity;
using DAL.ViewModels;
namespace DAL.Repositories
{
    public class DepartmentRepository :IDepartment
    {
        private GMSEntities _context;
        public DepartmentRepository(GMSEntities context)
        {
            this._context = context;
        }
        public IEnumerable<DepartmentList> GetDepartments()
        {
            return _context.Departments.Where(x => x.IsActive == true)
                .Select(x => new DepartmentList
                {
                    DepartmentId = x.DepartmentId,
                    Title = x.Title
                }).ToList();
        }
        public Department GetDepartmentById(int? id)
        {
            return _context.Departments.Find(id);
        }
        public void InsertDepartment(Department department)
        {
            _context.Departments.Add(department);
        }
        public void DeleteDepartment(int? departmentId, int userId)
        {
            //Department department = _context.Departments.Find(departmentId);
            //_context.Departments.Remove(department);
            Department department = _context.Departments.Find(departmentId);
            if (department != null)
            {
                department.IsActive = false;
                department.UpdatedBy = userId;
                department.UpdatedOn = DateTime.Now;
                Save();
            }
        }

        public void UpdateDepartment(Department department)
        {
            //_context.Entry(department).State = EntityState.Modified;
            Department currdepartment = GetDepartmentById(department.DepartmentId);
            if (currdepartment != null)
            {
                currdepartment.UpdatedBy = department.UpdatedBy;
                currdepartment.UpdatedOn = department.UpdatedOn;
                currdepartment.Title = department.Title;
                Save();
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
