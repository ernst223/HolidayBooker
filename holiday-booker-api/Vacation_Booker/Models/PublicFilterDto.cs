using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class PublicFilterDto
    {
        public int? ResortId { get; set; }
        public DateTime? ArrivalIn { get; set; }
        public DateTime? ArrivalOut { get; set; }
        public int? MaxAmount { get; set; }
        public int? MinAmount { get; set; }
        public int? RegionId { get; set; }
        public int? AreaId { get; set; }
        public int? CountryId { get; set; }
        public int? UnitSizeId { get; set; }
    }
}
