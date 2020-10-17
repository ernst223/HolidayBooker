using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Repository
{
    public class SupplierRepository
    {
        private MyContext dc;
        public SupplierRepository(MyContext T)
        {
            dc = T;
        }
        public bool AddSupplier(Supplier T)
        {
                try
                {
                    dc.Suppliers.Add(T);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }
        public bool EditSupplier(Supplier T)
        {
                try
                {
                    var data = dc.Suppliers.Where(o => o.Id == T.Id).FirstOrDefault();
                    data.Description = T.Description;
                    data.Code = T.Code;
                    data.Email = T.Email;
                    data.Contact = T.Contact;
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }
        public List<Supplier> GetSuppliers()
        {
                try
                {
                    return dc.Suppliers.OrderByDescending(o => o.Id).ToList();
                }
                catch
                {
                    return null;
                }
            
        }
        public Supplier GetSupplier(int id)
        {
                try
                {
                    return dc.Suppliers.Where(o => o.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            
        }
        public bool deleteSupplier(int id)
        {
                try
                {
                    var entity = dc.Suppliers.Where(o => o.Id == id).FirstOrDefault();
                    dc.Suppliers.Remove(entity);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }
    }
}
