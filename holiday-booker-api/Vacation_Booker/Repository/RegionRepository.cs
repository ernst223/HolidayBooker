using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;

namespace Vacation_Booker.Repository
{
    public class RegionRepository
    {
        private MyContext dc;
        public RegionRepository(MyContext T)
        {
            dc = T;
        }
        public bool AddRegion(Region T, string userId)
        {
                try
                {
                    T.UserId = userId;
                    dc.Regions.Add(T);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool EditRegion(Region T)
        {
                try
                {
                    var data = dc.Regions.Where(o => o.Id == T.Id).FirstOrDefault();
                    data.Description = T.Description;
                    data.AreaId = T.AreaId;
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public List<Region> GetRegions(string userId)
        {
                try
                {
                    return dc.Regions.OrderByDescending(o => o.Id).ToList();
                }
                catch
                {
                    return null;
                }
        }
        public List<RegionDto> GetRegionsDto(string userId)
        {
            try
            {
                var data = dc.Regions.ToList();
                List<RegionDto> result = new List<RegionDto>();
                Area tempValue = null;
                foreach (var entity in data)
                {
                    tempValue = dc.Areas.Where(o => o.Id == entity.AreaId).FirstOrDefault();
                    if(tempValue == null)
                    {
                        result.Add(new RegionDto()
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            AreaId = entity.AreaId,
                            Area = null
                        });
                    }
                    else
                    {
                        result.Add(new RegionDto()
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            AreaId = entity.AreaId,
                            Area = tempValue.Description
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
        public Region GetRegion(int id)
        {
                try
                {
                    return dc.Regions.Where(o => o.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
        }
        public bool DeleteRegion(int id)
        {
                try
                {
                //First make the Resorts RegionId foreign Key null to prevent errors
                var region = dc.Regions.Where(o => o.Id == id).FirstOrDefault();
                var resorts = dc.Resorts.Where(o => o.RegionId == region.Id);
                foreach(var entity in resorts)
                {
                    entity.RegionId = 0;
                }
                dc.SaveChanges();

                    dc.Regions.Remove(region);
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
