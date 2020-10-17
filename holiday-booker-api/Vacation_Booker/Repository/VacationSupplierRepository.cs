using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Repository
{
    public class VacationSupplierRepository
    {
        private MyContext dc;
        public VacationSupplierRepository(MyContext T)
        {
            dc = T;
        }
        public bool AddVacationSupplier(VacationSuppliers T)
        {
                try
                {
                    dc.VacationSuppliers.Add(T);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool EditVacationSupplier(VacationSuppliers T)
        {
                try
                {
                    var data = dc.VacationSuppliers.Where(o => o.Id == T.Id).FirstOrDefault();
                    data.SupplierId = T.SupplierId;
                    data.VacationId = T.VacationId;
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }
        public List<VacationSuppliers> GetVacationSuppliers()
        {
                try
                {
                    return dc.VacationSuppliers.ToList();
                }
                catch
                {
                    return null;
                }
            
        }
        public VacationSuppliers GetVacationSupplier(int id)
        {
                try
                {
                    return dc.VacationSuppliers.Where(o => o.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
        }
        public bool DeleteVacationSupplier(int id)
        {
                try
                {
                    dc.VacationSuppliers.Remove(dc.VacationSuppliers.Where(o => o.Id == id).FirstOrDefault());
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
