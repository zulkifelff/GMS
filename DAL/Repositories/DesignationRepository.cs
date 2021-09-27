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
    public class DesignationRepository : IDesignation
    {
        private GMSEntities _context;
        public DesignationRepository(GMSEntities context)
        {
            this._context = context;
        }
        public IEnumerable<DesignationList> GetDesignations()
        {
            return _context.Designations.Where(x => x.IsActive == true)
                .Select(x => new DesignationList
                {
                    DesignationId = x.DesignationId,
                    Title = x.Title
                }).ToList();
        }
        public Designation GetDesignationById(int? id)
        {
            return _context.Designations.Find(id);
        }
        public void InsertDesignation(Designation designation)
        {
            _context.Designations.Add(designation);
        }
        public void DeleteDesignation(int? designationId, int userId)
        {
            //Designation designation = _context.Designations.Find(designationId);
            //_context.Designations.Remove(designation);
            Designation designation = _context.Designations.Find(designationId);
            if (designation != null)
            {
                designation.IsActive = false;
                designation.UpdatedBy = userId;
                designation.UpdatedOn = DateTime.Now;
                Save();
            }
        }

        public void UpdateDesignation(Designation designation)
        {
            Designation currdesignation = GetDesignationById(designation.DesignationId);
            if(currdesignation !=null)
            {
                currdesignation.UpdatedBy = designation.UpdatedBy;
                currdesignation.UpdatedOn = designation.UpdatedOn;
                currdesignation.Title = designation.Title;
                Save();
            }
            //_context.Entry(designation).State = EntityState.Modified;
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
