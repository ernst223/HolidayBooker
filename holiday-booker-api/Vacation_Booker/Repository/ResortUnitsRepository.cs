using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Repository
{
    public class ResortUnitsRepository
    {
        private MyContext dc;
        public ResortUnitsRepository(MyContext T)
        {
            dc = T;
        }
        public bool AddResortUnit(ResortUnits T)
        {
                try
                {
                    dc.ResortUnits.Add(T);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool EditResortUnit(ResortUnits T)
        {
                try
                {
                    var data = dc.ResortUnits.Where(o => o.Id == T.Id).FirstOrDefault();
                    data.ResortId = T.ResortId;
                    data.UnitSizeId = T.UnitSizeId;
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }
        public List<ResortUnits> GetResortUnits()
        {
                try
                {
                    return dc.ResortUnits.ToList();
                }
                catch
                {
                    return null;
                }
            
        }
        public ResortUnits GetResortUnit(int id)
        {
                try
                {
                    return dc.ResortUnits.Where(o => o.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            
        }
        public List<UnitSizes> GetUnitSizesPerResort(int id)
        {
            try
            {
                var data = dc.ResortUnits.Where(o => o.ResortId == id).ToList();
                List<UnitSizes> result = new List<UnitSizes>();
                foreach (var entity in data)
                {
                    result.Add(dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault());
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteResortUnit(int id)
        {
                try
                {
                    dc.ResortUnits.Remove(dc.ResortUnits.Where(o => o.Id == id).FirstOrDefault());
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }
        public bool DeleteUnitSizeAtResortId(int id)
        {
            try
            {
                var data = dc.ResortUnits.Where(o => o.ResortId == id).ToList();
                foreach(var entity in data)
                {
                    dc.ResortUnits.Remove(entity);
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
