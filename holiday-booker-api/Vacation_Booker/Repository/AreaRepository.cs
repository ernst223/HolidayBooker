using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;

namespace Vacation_Booker.Repository
{
    public class AreaRepository
    {
        private MyContext dc;
        public AreaRepository(MyContext T)
        {
            dc = T;
        }
        public bool addArea(Area T)
        {
            try
            {
                dc.Areas.Add(T);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Area> getAreaPerCountry(int id)
        {
            try
            {
                return dc.Areas.Where(o => o.CountryId == id).ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool updateArea(Area T)
        {
            try
            {
                var data = dc.Areas.Where(o => o.Id == T.Id).FirstOrDefault();
                data.Description = T.Description;
                data.CountryId = T.CountryId;
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Area> GetAreas()
        {
            try
            {
                return dc.Areas.OrderByDescending(o => o.Id).ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<AreasDto> GetAreasDto()
        {
            try
            {
                var data = dc.Areas.ToList();
                Country tempValue = null;
                List<AreasDto> result = new List<AreasDto>();
                foreach(var entity in data)
                {
                tempValue = dc.Countries.Where(o => o.Id == entity.CountryId).FirstOrDefault();
                if(tempValue == null)
                {
                    result.Add(new AreasDto()
                    {
                        Id = entity.Id,
                        Description = entity.Description,
                        CountryId = entity.CountryId,
                        Country = null
                    });
                }
                else
                {
                    result.Add(new AreasDto()
                    {
                        Id = entity.Id,
                        Description = entity.Description,
                        CountryId = entity.CountryId,
                        Country = tempValue.Description
                    });
                }
                    
                }
                return result.OrderByDescending(o => o.Id).ToList();
            }
            catch
            {
                return null;
            }
        }
        public Area GetArea(int id)
        {
            try
            {
                return dc.Areas.Where(o => o.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteArea(int id)
        {
            try
            {
                dc.Areas.Remove(dc.Areas.Where(o => o.Id == id).FirstOrDefault());
                dc.SaveChanges();

                //Now deleting all traces of the Area
                var data = dc.Regions.Where(o => o.AreaId == id).ToList();
                foreach(var entity in data)
                {
                    entity.AreaId = null;
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
