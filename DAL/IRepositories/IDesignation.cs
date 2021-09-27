using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL.IRepositories
{
    interface IDesignation
    {
        IEnumerable<DesignationList> GetDesignations();
        Designation GetDesignationById(int? designationId);
        void InsertDesignation(Designation designation);
        void DeleteDesignation(int? designationId, int userId);
        void UpdateDesignation(Designation designation);
        void Save();
    }
}
