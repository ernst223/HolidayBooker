using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class PublicModelDto
    {
        public int Id { get; set; }
        public string Resort { get; set; }
        public int ResortId { get; set; }
        public string Link { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string Area { get; set; }
        public int AreaId { get; set; }
        public string Region { get; set; }
        public int RegionId { get; set; }
        public string UnitSize { get; set; }
        public int UnitSizeId { get; set; }
        public DateTime Arrival { get; set; }
        public int Nights { get; set; }
        public int Price2Pay { get; set; }
        public bool Sold { get; set; }
        public bool Hold { get; set; }
    }
}
