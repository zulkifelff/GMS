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
    public class PackageRepository : IPackage
    {
        private GMSEntities _context;
        public PackageRepository(GMSEntities context)
        {
            this._context = context;
        }
        public IEnumerable<PackageList> GetPackages()
        {
            return _context.Packages.Where(x => x.IsActive == true)
                .Select(x => new PackageList
                {
                    PackageId = x.PackageId,
                    Title = x.Title
                }).ToList();
        }
        public Package GetPackageById(int? id)
        {
            return _context.Packages.Find(id);
        }
        public void InsertPackage(Package package)
        {
            _context.Packages.Add(package);
        }
        public void DeletePackage(int? packageId, int userId)
        {
            //Package package = _context.Packages.Find(packageId);
            //_context.Packages.Remove(package);
            Package package = _context.Packages.Find(packageId);
            if (package != null)
            {
                package.IsActive = false;
                package.UpdatedBy = userId;
                package.UpdatedOn = DateTime.Now;
                Save();
            }
        }

        public void UpdatePackage(Package package)
        {
            //_context.Entry(package).State = EntityState.Modified;
            Package currPackage = GetPackageById(package.PackageId);
            if(currPackage !=null)
            {
                currPackage.Title = package.Title;
                currPackage.UpdatedBy = package.UpdatedBy;
                currPackage.UpdatedOn = package.UpdatedOn;
                currPackage.Amount = package.Amount;
                currPackage.Description = package.Description;
                currPackage.DurationDays = package.DurationDays;
                Save();
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
