using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Repository
{
    public class CountryRepository
    {
        private MyContext dc;
        public CountryRepository(MyContext T)
        {
            dc = T;
        }
        public bool AddCountry(Country T)
        {
                try
                {
                    dc.Countries.Add(T);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool EditCountry(Country T)
        {
                try
                {
                    var data = dc.Countries.Where(o => o.Id == T.Id).FirstOrDefault();
                    data.Description = T.Description;
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public List<Country> GetCountries()
        {
                try
                {
                    var result = dc.Countries.ToList();
                    return result;
                }
                catch
                {
                    return null;
                }
        }
        public Country GetCountry(int id)
        {
                try
                {
                    return dc.Countries.Where(o => o.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
        }
        public bool DeleteCountry(int id)
        {
                try
                {
                    dc.Countries.Remove(dc.Countries.Where(o => o.Id == id).FirstOrDefault());
                    dc.SaveChanges();

                //Now deleting all traces of the country 
                var data = dc.Areas.Where(o => o.CountryId == id).ToList();
                foreach(var entity in data)
                {
                    entity.CountryId = null;
                }
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
