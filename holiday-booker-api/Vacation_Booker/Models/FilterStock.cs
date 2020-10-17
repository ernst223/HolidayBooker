using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class FilterStock
    {
        public int SupplierId { get; set; }
        public int ResortId { get; set; }
        public DateTime? TheDate { get; set; }
    }
}
