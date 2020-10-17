using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class ResortWithRegionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Region { get; set; }
        public int? RegionId { get; set; }
        public string Area { get; set; }
        public string Country { get; set; }
        public string Coordinates { get; set; }
    }
}
