using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;

namespace Vacation_Booker.Repository
{
    public class ResortRepository
    {
        private MyContext dc;
        public ResortRepository(MyContext T)
        {
            dc = T;
        }
        public bool AddResortWithUnits(Resort resort, List<UnitSizes> unitSizes)
        {
                try
                {
                    //Firt adding the unitSizes
                    foreach(var entity in unitSizes)
                    {
                        dc.UnitSizes.Add(entity);
                    }

                    //Now adding the resort
                    dc.Resorts.Add(resort);
                    dc.SaveChanges();

                    //Retreiving the values from the database for there id's
                    var theResort = dc.Resorts.Last();
                    //Returning the last units inserted into the database that are the unitSizes (need there Id's)
                    var theUnitSizes = dc.Resorts.Skip(Math.Max(0, dc.Resorts.Count() - unitSizes.Count())).ToList();

                    //making a new ResortUnit entities for the relationships by using the Id's gathered
                    foreach(var entity in theUnitSizes)
                    {
                        dc.ResortUnits.Add(new ResortUnits()
                        {
                            UnitSizeId = entity.Id,
                            ResortId = theResort.Id,
                        });
                    }
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool EditResort(Resort resort)
        {
                try
                {
                    var data = dc.Resorts.Where(o => o.Id == resort.Id).FirstOrDefault();
                    data.Description = resort.Description;
                    data.Link = resort.Link;
                    data.RegionId = resort.RegionId;
                    data.Coordinates = resort.Coordinates;
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool EditUnitSize(UnitSizes T)
        {
                try
                {
                    var data = dc.UnitSizes.Where(o => o.Id == T.Id).FirstOrDefault();
                    data.Description = T.Description;
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool DeleteUnitSize(int id)
        {
                try
                {
                    dc.UnitSizes.Remove(dc.UnitSizes.Where(o => o.Id == id).FirstOrDefault());
                    dc.ResortUnits.Remove(dc.ResortUnits.Where(o => o.UnitSizeId == id).FirstOrDefault());
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool DeleteResort(int id)
        {
            try
            {
                //First Delete all the Foreign key entities. 
                var unitSizesForResort = dc.ResortUnits.Where(a => a.ResortId == id);
                foreach(var entity in unitSizesForResort)
                {
                  dc.ResortUnits.Remove(entity);
                }
                dc.SaveChanges();

                //Now deleting the Resort Self
                    dc.Resorts.Remove(dc.Resorts.Where(o => o.Id == id).FirstOrDefault());
                    dc.SaveChanges();
                    return true;
             }
                catch
            {
                return false;
            }
        }

        public bool addResort(Resort T)
        {
                try
                {
                    dc.Resorts.Add(T);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public Resort lastResort()
        {
            try
            {
                return dc.Resorts.LastOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public List<ResortWithRegionDto> GetResortWithRegion()
        {
            try
            {
                var data = dc.Resorts.ToList();
                List<ResortWithRegionDto> Result = new List<ResortWithRegionDto>();
                Area theArea = null;
                Country theCountry = null;
                Region theRegion = null;
                foreach(var entity in data)
                {
                    if(entity.RegionId != 0)
                    {
                        theRegion = dc.Regions.Where(o => o.Id == entity.RegionId).FirstOrDefault();
                        theArea = dc.Areas.Where(o => o.Id == theRegion.AreaId).FirstOrDefault();
                        if (theArea == null)
                        {
                            Result.Add(new ResortWithRegionDto()
                            {
                                Id = entity.Id,
                                Description = entity.Description,
                                Link = entity.Link,
                                RegionId = entity.RegionId,
                                Region = theRegion.Description,
                                Area = null,
                                Country = null,
                                Coordinates = entity.Coordinates
                            });
                        }
                        else
                        {
                            theCountry = dc.Countries.Where(o => o.Id == theArea.CountryId).FirstOrDefault();
                            if (theCountry == null)
                            {
                                Result.Add(new ResortWithRegionDto()
                                {
                                    Id = entity.Id,
                                    Description = entity.Description,
                                    Link = entity.Link,
                                    RegionId = entity.RegionId,
                                    Region = theRegion.Description,
                                    Area = theArea.Description,
                                    Country = null,
                                    Coordinates = entity.Coordinates
                                });
                            }
                            else
                            {
                                Result.Add(new ResortWithRegionDto()
                                {
                                    Id = entity.Id,
                                    Description = entity.Description,
                                    Link = entity.Link,
                                    RegionId = entity.RegionId,
                                    Region = theRegion.Description,
                                    Area = theArea.Description,
                                    Country = theCountry.Description,
                                    Coordinates = entity.Coordinates
                                });
                            }
                        }
                    }
                    else
                    {
                        Result.Add(new ResortWithRegionDto()
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            Link = entity.Link,
                            RegionId = null,
                            Region = null,
                            Area = null,
                            Country = null,
                            Coordinates = entity.Coordinates
                        });
                    }
                    
                }
                return Result.OrderByDescending(o => o.Id).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<ResortDisplayDto> GetResortsForDisplay()
        {
                try
                {
                    List<ResortDisplayDto> Result = new List<ResortDisplayDto>();
                    var Resort = dc.Resorts.ToList();
                    foreach(var entity in Resort)
                    {
                        Result.Add(new ResortDisplayDto()
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            Region = dc.Regions.Where(o => o.Id == entity.RegionId).FirstOrDefault().Description
                        });
                    }
                    return Result.OrderByDescending(o => o.Id).ToList();
                }
                catch
                {
                    return null;
                }
        }
        public Resort GetResort(int id)
        {
                try
                {
                    return dc.Resorts.Where(o => o.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
        }
        public List<Resort> GetResorts()
        {
                try
                {
                    return dc.Resorts.OrderByDescending(o => o.Id).ToList();
                }
                catch
                {
                    return null;
                }
        }
    }
}
