using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> AreaId { get; set; }
        public string Area { get; set; }
    }
}
