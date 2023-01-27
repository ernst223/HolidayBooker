using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;

namespace Vacation_Booker.Repository
{
    public class PublicRepository
    {
        private MyContext dc;
        public PublicRepository(MyContext T)
        {
            dc = T;
        }

        public List<PublicModelDto> GetAllStock()
        {
            try
            {
                var currentUser = dc.Users.Where(a => a.Email == "info@holidaybooker.co.za").FirstOrDefault();
                var vacationData = dc.Vacations.Take(100).ToList();
                List<PublicModelDto> data = new List<PublicModelDto>();
                Resort theResort;
                Area theArea;
                Region theRegion;
                Country theCountry;
                foreach (var entity in vacationData)
                {
                    //gathering the resort, region, area and country and if null do not add to the list
                    theResort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault();
                    if (theResort != null)
                    {
                        theRegion = dc.Regions.Where(o => o.Id == theResort.RegionId).FirstOrDefault();
                        if (theRegion != null)
                        {
                            theArea = dc.Areas.Where(o => o.Id == theRegion.AreaId).FirstOrDefault();
                            if (theArea != null)
                            {
                                theCountry = dc.Countries.Where(o => o.Id == theArea.CountryId).FirstOrDefault();
                                if (theCountry != null)
                                {
                                    if (currentUser.Id == entity.UserId)
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.Price2Pay,
                                            Sold = entity.Sold,
                                            Hold = entity.Hold
                                        });
                                    } else
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.PartnersPrice,
                                            Sold = entity.Sold,
                                            Hold = entity.Hold
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
        public List<PublicModelDto> GetFastFilteredStock(FastFilterDto T)
        {
            try
            {
                CleanVacations();
                var currentUser = dc.Users.Where(a => a.Email == "info@holidaybooker.co.za").FirstOrDefault();
                var vacationData = dc.Vacations.ToList();
                List<PublicModelDto> data = new List<PublicModelDto>();
                Resort theResort;
                Area theArea;
                Region theRegion;
                Country theCountry;
                foreach (var entity in vacationData)
                {
                    //gathering the resort, region, area and country and if null do not add to the list
                    theResort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault();
                    if (theResort != null)
                    {
                        theRegion = dc.Regions.Where(o => o.Id == theResort.RegionId).FirstOrDefault();
                        if (theRegion != null)
                        {
                            theArea = dc.Areas.Where(o => o.Id == theRegion.AreaId).FirstOrDefault();
                            if (theArea != null)
                            {
                                theCountry = dc.Countries.Where(o => o.Id == theArea.CountryId).FirstOrDefault();
                                if (theCountry != null)
                                {
                                    if (currentUser.Id == entity.UserId)
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.Price2Pay,
                                            Sold = entity.Sold
                                        });
                                    } else
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.PartnersPrice,
                                            Sold = entity.Sold
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                //Starting the filtering of all the stock
                if (T.CountryId != 0)
                {
                    data = data.Where(o => o.CountryId == T.CountryId).ToList();
                }
                
                if(T.AreaId.Count() != 0)
                {
                    var tempList = new List<PublicModelDto>();
                    foreach (var entity in T.AreaId)
                    {
                        foreach(var subentity in data.Where(o => o.AreaId == entity).ToList())
                        {
                            tempList.Add(subentity);
                        }
                    }
                    data = tempList;
                }

                if (T.Nights != 0)
                {
                    //data = data.Where(o => o.Nights == T.Nights).ToList();
                    data = data.OrderByDescending(o => o.Nights == T.Nights).ToList();
                }

                if (T.Arrival != null)
                {
                    var tempDate = (DateTime)T.Arrival;
                    tempDate = tempDate.Date.AddDays(1);
                    var tempFirst5Dates = data.Where(o => o.Arrival <= tempDate && o.Arrival >= tempDate.AddDays(-5)).ToList();
                    var second5Dates = data.Where(o => o.Arrival > tempDate && o.Arrival <= tempDate.AddDays(5)).ToList();
                    List<PublicModelDto> first5Dates = new List<PublicModelDto>();
                    foreach(var entity in second5Dates)
                    {
                        tempFirst5Dates.Add(entity);
                    }
                    //var smallestEntity = tempFirst5Dates.Select(o => o.Arrival).Min();
                    data = tempFirst5Dates.OrderBy(o => o.Arrival).ToList();
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
        public List<PublicModelDto> GetFilteredStock(PublicFilterDto T)
        {
            //This method is to first delete all the outdated stock
            CleanVacations();

            try
            {
                var currentUser = dc.Users.Where(a => a.Email == "info@holidaybooker.co.za").FirstOrDefault();
                var vacationData = dc.Vacations.Where(o => o.Sold == false).ToList();
                List<PublicModelDto> data = new List<PublicModelDto>();
                Resort theResort;
                Area theArea;
                Region theRegion;
                Country theCountry;
                foreach (var entity in vacationData)
                {
                    //gathering the resort, region, area and country and if null do not add to the list
                    theResort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault();
                    if(theResort != null)
                    {
                        theRegion = dc.Regions.Where(o => o.Id == theResort.RegionId).FirstOrDefault();
                        if(theRegion != null)
                        {
                            theArea = dc.Areas.Where(o => o.Id == theRegion.AreaId).FirstOrDefault();
                            if(theArea != null)
                            {
                                theCountry = dc.Countries.Where(o => o.Id == theArea.CountryId).FirstOrDefault();
                                if(theCountry != null)
                                {
                                    if (currentUser.Id == entity.UserId)
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.Price2Pay,
                                            Sold = entity.Sold
                                        });
                                    } else
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.PartnersPrice,
                                            Sold = entity.Sold
                                        });
                                    }
                                    
                                }
                            }
                        }
                    }     
                }

                //Start filltering the data according to the model
                if(T.ResortId != null)
                {
                    data = data.Where(o => o.ResortId == T.ResortId).ToList();
                }
                if(T.ArrivalIn != null)
                {
                    data = data.Where(o => o.Arrival > T.ArrivalIn).ToList();
                }
                if (T.ArrivalOut != null)
                {
                    data = data.Where(o => o.Arrival < T.ArrivalIn).ToList();
                }
                if(T.MaxAmount != null)
                {
                    data = data.Where(o => o.Price2Pay < T.MaxAmount).ToList();
                }
                if (T.MinAmount != null)
                {
                    data = data.Where(o => o.Price2Pay > T.MinAmount).ToList();
                }
                if(T.RegionId != null)
                {
                    data = data.Where(o => o.RegionId == T.RegionId).ToList();
                }
                if(T.AreaId != null)
                {
                    data = data.Where(o => o.AreaId == T.AreaId).ToList();
                }
                if(T.CountryId != null)
                {
                    data = data.Where(o => o.CountryId == T.CountryId).ToList();
                }
                if(T.UnitSizeId != null)
                {
                    data = data.Where(o => o.UnitSizeId == T.UnitSizeId).ToList();
                }

                //return the filtered data
                return data;
            }
            catch
            {
                return null;
            }
        }

        //This method is to first delete all the outdated stock
        public void CleanVacations()
        {
            try
            {
                var data = dc.Vacations.Where(a => a.Arrival.AddDays(a.Nights) < DateTime.Now).ToList();
                foreach (var entity in data)
                {
                    delVacAccordingToPar(entity);
                }
            }
            catch
            {
                Console.WriteLine("Could not Clean Vacation Table. Something went wrong at CleanVacations() Method");
            }
        }
        public void delVacAccordingToPar(Vacation T)
        {
            try
            {
                dc.Vacations.Remove(T);
                dc.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Failed to delete expired date stock entity. delVacAccordingToPar() method failed");
            }
        }

        public List<PublicModelDto> GetAllStockPartner()
        {
            try
            {
                CleanVacations();
                var currentUser = dc.Users.Where(a => a.UserName == "EZTTravel").FirstOrDefault();
                var vacationData = dc.Vacations.Take(100).ToList();
                List<PublicModelDto> data = new List<PublicModelDto>();
                Resort theResort;
                Area theArea;
                Region theRegion;
                Country theCountry;
                foreach (var entity in vacationData)
                {
                    //gathering the resort, region, area and country and if null do not add to the list
                    theResort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault();
                    if (theResort != null)
                    {
                        theRegion = dc.Regions.Where(o => o.Id == theResort.RegionId).FirstOrDefault();
                        if (theRegion != null)
                        {
                            theArea = dc.Areas.Where(o => o.Id == theRegion.AreaId).FirstOrDefault();
                            if (theArea != null)
                            {
                                theCountry = dc.Countries.Where(o => o.Id == theArea.CountryId).FirstOrDefault();
                                if (theCountry != null)
                                {
                                    if (currentUser.Id == entity.UserId)
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.Price2Pay,
                                            Sold = entity.Sold,
                                            Hold = entity.Hold
                                        });
                                    }
                                    else
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.PartnersPrice,
                                            Sold = entity.Sold,
                                            Hold = entity.Hold
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
        public List<PublicModelDto> GetFastFilteredStockPartner(FastFilterDto T)
        {
            try
            {
                CleanVacations();
                var currentUser = dc.Users.Where(a => a.UserName == "EZTTravel").FirstOrDefault();
                var vacationData = dc.Vacations.ToList();
                List<PublicModelDto> data = new List<PublicModelDto>();
                Resort theResort;
                Area theArea;
                Region theRegion;
                Country theCountry;
                foreach (var entity in vacationData)
                {
                    //gathering the resort, region, area and country and if null do not add to the list
                    theResort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault();
                    if (theResort != null)
                    {
                        theRegion = dc.Regions.Where(o => o.Id == theResort.RegionId).FirstOrDefault();
                        if (theRegion != null)
                        {
                            theArea = dc.Areas.Where(o => o.Id == theRegion.AreaId).FirstOrDefault();
                            if (theArea != null)
                            {
                                theCountry = dc.Countries.Where(o => o.Id == theArea.CountryId).FirstOrDefault();
                                if (theCountry != null)
                                {
                                    if (currentUser.Id == entity.UserId)
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.Price2Pay,
                                            Sold = entity.Sold
                                        });
                                    }
                                    else
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.PartnersPrice,
                                            Sold = entity.Sold
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                //Starting the filtering of all the stock
                if (T.CountryId != 0)
                {
                    data = data.Where(o => o.CountryId == T.CountryId).ToList();
                }

                if (T.AreaId.Count() != 0)
                {
                    var tempList = new List<PublicModelDto>();
                    foreach (var entity in T.AreaId)
                    {
                        foreach (var subentity in data.Where(o => o.AreaId == entity).ToList())
                        {
                            tempList.Add(subentity);
                        }
                    }
                    data = tempList;
                }

                if (T.Nights != 0)
                {
                    //data = data.Where(o => o.Nights == T.Nights).ToList();
                    data = data.OrderByDescending(o => o.Nights == T.Nights).ToList();
                }

                if (T.Arrival != null)
                {
                    var tempDate = (DateTime)T.Arrival;
                    tempDate = tempDate.Date.AddDays(1);
                    var tempFirst5Dates = data.Where(o => o.Arrival <= tempDate && o.Arrival >= tempDate.AddDays(-5)).ToList();
                    var second5Dates = data.Where(o => o.Arrival > tempDate && o.Arrival <= tempDate.AddDays(5)).ToList();
                    List<PublicModelDto> first5Dates = new List<PublicModelDto>();
                    foreach (var entity in second5Dates)
                    {
                        tempFirst5Dates.Add(entity);
                    }
                    //var smallestEntity = tempFirst5Dates.Select(o => o.Arrival).Min();
                    data = tempFirst5Dates.OrderBy(o => o.Arrival).ToList();
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
        public List<PublicModelDto> GetFilteredStockPartner(PublicFilterDto T)
        {
            //This method is to first delete all the outdated stock
            CleanVacations();

            try
            {
                var currentUser = dc.Users.Where(a => a.UserName == "EZTTravel").FirstOrDefault();
                var vacationData = dc.Vacations.Where(o => o.Sold == false).ToList();
                List<PublicModelDto> data = new List<PublicModelDto>();
                Resort theResort;
                Area theArea;
                Region theRegion;
                Country theCountry;
                foreach (var entity in vacationData)
                {
                    //gathering the resort, region, area and country and if null do not add to the list
                    theResort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault();
                    if (theResort != null)
                    {
                        theRegion = dc.Regions.Where(o => o.Id == theResort.RegionId).FirstOrDefault();
                        if (theRegion != null)
                        {
                            theArea = dc.Areas.Where(o => o.Id == theRegion.AreaId).FirstOrDefault();
                            if (theArea != null)
                            {
                                theCountry = dc.Countries.Where(o => o.Id == theArea.CountryId).FirstOrDefault();
                                if (theCountry != null)
                                {
                                    if (currentUser.Id == entity.UserId)
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.Price2Pay,
                                            Sold = entity.Sold
                                        });
                                    }
                                    else
                                    {
                                        data.Add(new PublicModelDto()
                                        {
                                            Id = entity.Id,
                                            Resort = theResort.Description,
                                            ResortId = entity.ResortId,
                                            Link = theResort.Link,
                                            RegionId = theRegion.Id,
                                            Region = theRegion.Description,
                                            AreaId = theArea.Id,
                                            Area = theArea.Description,
                                            CountryId = theCountry.Id,
                                            Country = theCountry.Description,
                                            UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                                            UnitSizeId = entity.UnitSizeId,
                                            Arrival = entity.Arrival,
                                            Nights = entity.Nights,
                                            Price2Pay = entity.PartnersPrice,
                                            Sold = entity.Sold
                                        });
                                    }

                                }
                            }
                        }
                    }
                }

                //Start filltering the data according to the model
                if (T.ResortId != null)
                {
                    data = data.Where(o => o.ResortId == T.ResortId).ToList();
                }
                if (T.ArrivalIn != null)
                {
                    data = data.Where(o => o.Arrival > T.ArrivalIn).ToList();
                }
                if (T.ArrivalOut != null)
                {
                    data = data.Where(o => o.Arrival < T.ArrivalIn).ToList();
                }
                if (T.MaxAmount != null)
                {
                    data = data.Where(o => o.Price2Pay < T.MaxAmount).ToList();
                }
                if (T.MinAmount != null)
                {
                    data = data.Where(o => o.Price2Pay > T.MinAmount).ToList();
                }
                if (T.RegionId != null)
                {
                    data = data.Where(o => o.RegionId == T.RegionId).ToList();
                }
                if (T.AreaId != null)
                {
                    data = data.Where(o => o.AreaId == T.AreaId).ToList();
                }
                if (T.CountryId != null)
                {
                    data = data.Where(o => o.CountryId == T.CountryId).ToList();
                }
                if (T.UnitSizeId != null)
                {
                    data = data.Where(o => o.UnitSizeId == T.UnitSizeId).ToList();
                }

                //return the filtered data
                return data;
            }
            catch
            {
                return null;
            }
        }
    }
}
