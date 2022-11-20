﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Repository
{
    public class UnitSizeRepository
    {
        private MyContext dc;
        public UnitSizeRepository(MyContext T)
        {
            dc = T;
        }
        public bool AddUnitSize(UnitSizes T, string userId)
        {
                try
                {
                    T.UserId = userId;
                    dc.UnitSizes.Add(T);
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
        public List<UnitSizes> GetUnitSizes(string userId)
        {
                try
                {
                    return dc.UnitSizes.Where(a => a.UserId == userId).OrderByDescending(o => o.Id).ToList();
                }
                catch
                {
                    return null;
                }
            
        }
        public UnitSizes GetUnitSize(int id)
        {
                try
                {
                    return dc.UnitSizes.Where(o => o.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            
        }
        public bool DeleteUnitSize(int id)
        {
                try
                {
                    dc.UnitSizes.Remove(dc.UnitSizes.Where(o => o.Id == id).FirstOrDefault());
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
