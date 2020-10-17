using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class CheckDuplicateDto
    {
        public int ProviderId { get; set; }
        public int ResortId { get; set; }
        public int UnitSizeId { get; set; }
        public DateTime Arrival { get; set; }
        public int Nights { get; set; }
    }
}
