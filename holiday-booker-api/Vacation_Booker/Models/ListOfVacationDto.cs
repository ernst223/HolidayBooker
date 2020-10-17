using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Models
{
    public class ListOfVacationDto
    {
        public List<CheckDuplicateDto> myList { get; set; }
    }
}
