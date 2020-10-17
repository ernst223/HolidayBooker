using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class displayDuplicatesDto
    {
        public int Id { get; set; }
        public string Resort { get; set; }
        public string Provider { get; set; }
        public string Arrival { get; set; }
        public int Nights { get; set; }
    }
}
