using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class FastFilterDto
    {
        public int CountryId { get; set; }
        public int[] AreaId { get; set; }
        public DateTime? Arrival { get; set; }
        public int Nights { get; set; }
    }
}
