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
    public class GeneralRepository
    {
        private GMSEntities _context;
        public GeneralRepository(GMSEntities context)
        {
            this._context = context;
        }
        public void InsertInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
