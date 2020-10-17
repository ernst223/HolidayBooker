using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class EnquiryDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Dob { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public int StockId { get; set; }
        public int Adults { get; set; }
        public int Under12 { get; set; }
        public string Note { get; set; }
    }
}
