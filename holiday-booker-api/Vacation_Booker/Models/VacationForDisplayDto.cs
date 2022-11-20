using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Models
{
    public class VacationForDisplayDto
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public int ProviderId { get; set; }
        public string Resort { get; set; }
        public int ResortId { get; set; }
        public string UnitSize { get; set; }
        public int UnitSizeId { get; set; }
        public string Arrival { get; set; }
        public int Nights { get; set; }
        public int BuyingPrice { get; set; }
        public int Price2Pay { get; set; }
        public int AdminFee { get; set; }
        public bool Sold { get; set; }
        public bool Hold { get; set; }
        public int PartnerPrice { get; set; }
    }
}
