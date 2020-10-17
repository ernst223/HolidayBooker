using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class AreasDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string Country { get; set; }
    }
}
