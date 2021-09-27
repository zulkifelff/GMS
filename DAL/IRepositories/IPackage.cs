using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL.IRepositories
{
    interface IPackage
    {
        IEnumerable<PackageList> GetPackages();
        Package GetPackageById(int? packageId);
        void InsertPackage(Package package);
        void DeletePackage(int? packageId, int userId);
        void UpdatePackage(Package package);
        void Save();
    }
}
