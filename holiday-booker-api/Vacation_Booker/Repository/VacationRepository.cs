using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;

namespace Vacation_Booker.Repository
{
    public class VacationRepository
    {
        private MyContext dc;
        public VacationRepository(MyContext T)
        {
            dc = T;
        }
        public bool AddVacation(Vacation T)
        {
                try
                {
                //Need to add one day because for some random reason the arrival date is set to one day back at 22:00
                T.Arrival = T.Arrival.Date.AddDays(1);
                    dc.Vacations.Add(T);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }

        public List<StockWithDetails> getStockWithDetails()
        {
            try
            {
                var allStock = dc.Vacations.ToList();
                List<StockWithDetails> result = new List<StockWithDetails>();
                foreach (var entity in allStock)
                {
                    result.Add(new StockWithDetails()
                    {
                        Id = entity.Id,
                        Arrival = entity.Arrival,
                        AdminFee = entity.AdminFee,
                        BuyingPrice = entity.BuyingPrice,
                        Hold = entity.Hold,
                        Nights = entity.Nights,
                        Price2Pay = entity.Price2Pay,
                        Resort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault(),
                        Sold = entity.Sold,
                        SupplierId = entity.SupplierId,
                        UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault()
                    });
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        public List<displayDuplicatesDto> getDisplayDuplicates(List<Vacation> T)
        {
            List<displayDuplicatesDto> result = new List<displayDuplicatesDto>();
            foreach(var entity in T)
            {
                result.Add(new displayDuplicatesDto()
                {
                    Id = entity.Id,
                    Arrival = Convert.ToString(entity.Arrival.Date),
                    Nights = entity.Nights,
                    Provider = dc.Suppliers.Where(o => o.Id == entity.SupplierId).First().Code,
                    Resort = dc.Resorts.Where(o => o.Id == entity.ResortId).First().Description
                });
            }
            return result;
        }

        public List<Vacation> checkforduplicates(List<CheckDuplicateDto> T)
        {
            List<Vacation> result = new List<Vacation>();
            DateTime datetoTest;
            Vacation temp;
            foreach(var entity in T)
            {
                datetoTest = entity.Arrival.Date.AddDays(1);
                temp = dc.Vacations.Where(o => o.SupplierId == entity.ProviderId && o.ResortId == entity.ResortId && o.UnitSizeId == entity.UnitSizeId && o.Nights == entity.Nights && o.Arrival == datetoTest).FirstOrDefault();
                if(temp != null)
                {
                    result.Add(temp);
                }
            }
            return result;
        }
        public bool AddVacationList(List<Vacation> T)
        {
            try
            {
                //Need to add one day because for some random reason the arrival date is set to one day back at 22:00
                foreach(var entity in T)
                {
                    entity.Arrival = entity.Arrival.Date.AddDays(1);
                    dc.Vacations.Add(entity);
                }
                
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool EditVacation(Vacation T)
        {
                try
                {
                    var data = dc.Vacations.Where(o => o.Id == T.Id).FirstOrDefault();
                    data.Id = T.Id;
                    data.ResortId = T.ResortId;
                    data.SupplierId = T.SupplierId;
                    data.UnitSizeId = T.UnitSizeId;
                    // data.Arrival = T.Arrival;
                    //For some random reason gives me previous day at 22:00

                    data.Arrival = T.Arrival.Date.AddDays(1);
                    data.Nights = T.Nights;
                    data.Price2Pay = T.Price2Pay;
                    data.BuyingPrice = T.BuyingPrice;
                    data.AdminFee = T.AdminFee;
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
        public bool EditVacationSold(Vacation T)
        {
            try
            {
                var data = dc.Vacations.Where(o => o.Id == T.Id).FirstOrDefault();
                data.Id = T.Id;
                data.Sold = T.Sold;
                if (data.Sold == true)
                {
                    data.Hold = false;
                }
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool EditVacationHold(Vacation T)
        {
            try
            {
                var data = dc.Vacations.Where(o => o.Id == T.Id).FirstOrDefault();
                data.Id = T.Id;
                data.Hold = T.Hold;
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public List<Vacation> GetVacations()
        {
            CleanVacations();
                try
                {
                    return dc.Vacations.OrderByDescending(o => o.Id).ToList();
                }
                catch
                {
                    return null;
                }
        }
        public List<VacationForDisplayDto> getFilterByState(int filter)
        {
            var vacationData = dc.Vacations.OrderByDescending(o => o.Id).ToList();
            if(filter == 1)
            {
                vacationData = vacationData.Where(o => o.Hold == true).ToList();
            } else if(filter == 2)
            {
                vacationData = vacationData.Where(o => o.Sold == true).ToList();
            }
            List<VacationForDisplayDto> result = new List<VacationForDisplayDto>();
            foreach (var entity in vacationData)
            {
                result.Add(new VacationForDisplayDto()
                {
                    Id = entity.Id,
                    Provider = dc.Suppliers.Where(o => o.Id == entity.SupplierId).FirstOrDefault().Description,
                    ProviderId = entity.SupplierId,
                    Resort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault().Description,
                    ResortId = entity.ResortId,
                    UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                    UnitSizeId = entity.UnitSizeId,
                    Arrival = entity.Arrival.ToString(),
                    Nights = entity.Nights,
                    BuyingPrice = entity.BuyingPrice,
                    Price2Pay = entity.Price2Pay,
                    AdminFee = entity.AdminFee,
                    Sold = entity.Sold,
                    Hold = entity.Hold
                });
            }

            return result;
        }
        public List<VacationForDisplayDto> getFilteredVacation(FilterStock T)
        {
            var vacationData = dc.Vacations.OrderByDescending(o => o.Id).ToList();
            if (T.ResortId != 0)
            {
                vacationData = vacationData.Where(o => o.ResortId == T.ResortId).ToList();
            }
            if (T.SupplierId != 0)
            {
                vacationData = vacationData.Where(o => o.SupplierId == T.SupplierId).ToList();
            }
            if (T.TheDate != null)
            {
                var tempDate = (DateTime)T.TheDate;
                tempDate = tempDate.Date.AddDays(1);
                vacationData = vacationData.Where(o => o.Arrival >= tempDate).ToList();
                vacationData = vacationData.OrderBy(o => o.Arrival).ToList();
            }
           
            List<VacationForDisplayDto> result = new List<VacationForDisplayDto>();
            foreach (var entity in vacationData)
            {
                result.Add(new VacationForDisplayDto()
                {
                    Id = entity.Id,
                    Provider = dc.Suppliers.Where(o => o.Id == entity.SupplierId).FirstOrDefault().Description,
                    ProviderId = entity.SupplierId,
                    Resort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault().Description,
                    ResortId = entity.ResortId,
                    UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                    UnitSizeId = entity.UnitSizeId,
                    Arrival = entity.Arrival.ToString(),
                    Nights = entity.Nights,
                    BuyingPrice = entity.BuyingPrice,
                    Price2Pay = entity.Price2Pay,
                    AdminFee = entity.AdminFee,
                    Sold = entity.Sold,
                    Hold = entity.Hold
                });
            }

            return result;
        }

        public List<VacationForDisplayDto> GetVacationDisplay()
        {
            CleanVacations();
            
                var vacationData = dc.Vacations.ToList();
                List<VacationForDisplayDto> result = new List<VacationForDisplayDto>();
                foreach(var entity in vacationData)
                {
                    result.Add(new VacationForDisplayDto()
                    {
                        Id = entity.Id,
                        Provider = dc.Suppliers.Where(o => o.Id == entity.SupplierId).FirstOrDefault().Description,
                        ProviderId = entity.SupplierId,
                        Resort = dc.Resorts.Where(o => o.Id == entity.ResortId).FirstOrDefault().Description,
                        ResortId = entity.ResortId,
                        UnitSize = dc.UnitSizes.Where(o => o.Id == entity.UnitSizeId).FirstOrDefault().Description,
                        UnitSizeId = entity.UnitSizeId,
                        //Arrival = entity.Arrival.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                        Arrival = entity.Arrival.ToString(),
                        Nights = entity.Nights,
                        BuyingPrice = entity.BuyingPrice,
                        Price2Pay = entity.Price2Pay,
                        AdminFee = entity.AdminFee,
                        Sold = entity.Sold,
                        Hold = entity.Hold
                    });
                }
                result = result.OrderByDescending(o => o.Id).ToList();
                return result.OrderByDescending(o => o.Id).ToList();
            
        }
        public void CleanVacations()
        {
            try
            {
                var data = dc.Vacations.ToList();
                DateTime theDate;
                foreach(var entity in data)
                {
                    theDate = entity.Arrival.AddDays(entity.Nights);
                    if(theDate < DateTime.Now)
                    {
                        delVacAccordingToPar(entity);
                    }
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
        public Vacation GetVacation(int id)
        {
                try
                {
                    return dc.Vacations.Where(o => o.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            
        }
        public bool DeleteVacationdeleteVacationByProviderId(int id)
        {
            var entries = dc.Vacations.Where(o => o.SupplierId == id).ToList();
            foreach(var entity in entries)
            {
                dc.Vacations.Remove(entity);
            }
            dc.SaveChanges();
            return true;
        }
        public bool DeleteVacation(int id)
        {
                try
                {
                    var entity = dc.Vacations.Where(o => o.Id == id).FirstOrDefault();
                    dc.Vacations.Remove(entity);
                    dc.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            
        }

        public List<string> uploadVactionCSV(IFormFile theFile, string supplierName, int adminFee) 
        {
            List<string> result = new List<string>();
            int LineCount = 0;
            int commaCount = 0;
            int commaPointCount = 0;
            List<Vacation> tempEntries;
            List<Vacation> vacationsToBeAdded = new List<Vacation>();
            string line;
            string[] values;
            using (var ms = new MemoryStream())
            {
                theFile.CopyTo(ms);
                ms.Position = 0;
                using (var reader = new StreamReader(ms))
                {
                    while (!reader.EndOfStream)
                    {
                        LineCount++;
                        line = reader.ReadLine();
                        if (LineCount != 1)
                        {
                            commaCount = line.Count(f => f == ',');
                            commaPointCount = line.Count(f => f == ';');
                            if (commaCount > commaPointCount)
                            {
                                values = line.Split(',');
                            }
                            else
                            {
                                values = line.Split(';');
                            }
                            tempEntries = getProcessedEntry(values, supplierName, adminFee);
                            if (tempEntries == null)
                            {
                                result.Add(getFailedEntry(values, LineCount));
                            }
                            else
                            {
                                foreach (var entity in tempEntries)
                                {
                                    if (!isDuplicate(entity))
                                    {
                                        vacationsToBeAdded.Add(entity);
                                    }
                                }
                            }
                        }
                    }
                    if(vacationsToBeAdded.Count > 0)
                    {
                        foreach (var entity in vacationsToBeAdded)
                        {
                            entity.Id = 0;
                            dc.Vacations.Add(new Vacation() 
                            { 
                                AdminFee = entity.AdminFee,
                                Arrival = entity.Arrival,
                                BuyingPrice = entity.BuyingPrice,
                                Hold = entity.Hold,
                                Nights = entity.Nights,
                                Price2Pay = entity.Price2Pay,
                                ResortId = entity.ResortId,
                                Sold = entity.Sold,
                                SupplierId = entity.SupplierId,
                                UnitSizeId = entity.UnitSizeId
                            });
                        }
                        dc.SaveChanges();
                    }
                }
            }
            return result;
        }
        public bool isDuplicate(Vacation vacation)
        {
            if(dc.Vacations.Where(a => a.Nights == vacation.Nights && a.Price2Pay == vacation.Price2Pay && a.ResortId == vacation.ResortId
            && a.SupplierId == vacation.SupplierId && a.UnitSizeId == vacation.UnitSizeId && a.Arrival == vacation.Arrival 
            && a.BuyingPrice == vacation.BuyingPrice).FirstOrDefault() == null)
            {
                return false;
            } else
            {
                return true;
            }
        }
        public List<Vacation> getProcessedEntry(string[] entry, string supplierName, int adminFee)
        {
            List<Vacation> result = new List<Vacation>();
            Vacation tempVacation;
            Resort tempResort = getResortFromString(entry[2]);
            UnitSizes tempUnitSizes = getUnitSizesFromString(entry[6]);
            Supplier tempSupplier = GetSupplierFromString(supplierName);
            if(tempResort == null || tempUnitSizes == null || tempSupplier == null)
            {
                return null;
            }
            try
            {
                // Full week
                if(Convert.ToInt32(entry[7]) != 0)
                {
                    tempVacation = new Vacation();
                    tempVacation.ResortId = tempResort.Id;
                    tempVacation.UnitSizeId = tempUnitSizes.Id;
                    tempVacation.SupplierId = tempSupplier.Id;
                    tempVacation.Arrival = Convert.ToDateTime(entry[3]);
                    //tempVacation.Nights = 7;
                    tempVacation.Nights = Convert.ToInt32(entry[13]);
                    tempVacation.Price2Pay = Convert.ToInt32(entry[10]);
                    tempVacation.BuyingPrice = CalculateBuyingPriceFW(Convert.ToDouble(entry[7]));
                    tempVacation.AdminFee = adminFee;
                    tempVacation.Sold = false;
                    tempVacation.Hold = false;
                    result.Add(tempVacation);
                }

                // Weekend
                if (Convert.ToInt32(entry[8]) != 0)
                {
                    tempVacation = new Vacation();
                    tempVacation.ResortId = tempResort.Id;
                    tempVacation.UnitSizeId = tempUnitSizes.Id;
                    tempVacation.SupplierId = tempSupplier.Id;
                    tempVacation.Arrival = Convert.ToDateTime(entry[3]);
                    //tempVacation.Nights = 3;
                    tempVacation.Nights = Convert.ToInt32(entry[14]);
                    tempVacation.Price2Pay = Convert.ToInt32(entry[11]);
                    tempVacation.BuyingPrice = CalculateBuyingPriceWE(Convert.ToDouble(entry[8]));
                    tempVacation.AdminFee = adminFee;
                    tempVacation.Sold = false;
                    tempVacation.Hold = false;
                    result.Add(tempVacation);
                }

                // Mid Week
                if (Convert.ToInt32(entry[9]) != 0)
                {
                    tempVacation = new Vacation();
                    tempVacation.ResortId = tempResort.Id;
                    tempVacation.UnitSizeId = tempUnitSizes.Id;
                    tempVacation.SupplierId = tempSupplier.Id;
                    tempVacation.Arrival = Convert.ToDateTime(entry[3]).AddDays(3);
                    //tempVacation.Nights = 5;
                    tempVacation.Nights = Convert.ToInt32(entry[15]);
                    tempVacation.Price2Pay = Convert.ToInt32(entry[12]);
                    tempVacation.BuyingPrice = CalculateBuyingPriceMW(Convert.ToDouble(entry[9]));
                    tempVacation.AdminFee = adminFee;
                    tempVacation.Sold = false;
                    tempVacation.Hold = false;
                    result.Add(tempVacation);
                }
                if(result.Count == 0)
                {
                    return null;
                }
                return result;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public int CalculateBuyingPriceFW(double points)
        {
            double result = (points*1.56)+1045;
            return Convert.ToInt32(Math.Round(result));
        }
        public int CalculateBuyingPriceWE(double points)
        {
            double result = (points*1.56)+860;
            return Convert.ToInt32(Math.Round(result));
        }
        public int CalculateBuyingPriceMW(double points)
        {
            double result = (points * 1.56) + 860;
            return Convert.ToInt32(Math.Round(result));
        }
        public Resort getResortFromString(string word)
        {
            return dc.Resorts.Where(a => a.Description == word).FirstOrDefault();
        }
        public UnitSizes getUnitSizesFromString(string word)
        {
            return dc.UnitSizes.Where(a => a.Description == word).FirstOrDefault();
        }
        public Supplier GetSupplierFromString(string word)
        {
            return dc.Suppliers.Where(a => a.Description == word).FirstOrDefault();
        }
        public string getFailedEntry(string[] entry, int lineCount)
        {
            string result = Convert.ToString(lineCount);
            foreach (var entity in entry)
            {
                result += "," + entity;
            }
            return result;
        }
    }
}
