using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Models
{
    public class ResortDisplayDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Region { get; set; }
    }
}
