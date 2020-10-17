using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;

namespace Vacation_Booker.Models
{
    public class StockWithDetails
    {
        public int Id { get; set; }
        public Resort Resort { get; set; }
        public UnitSizes UnitSize { get; set; }
        public int SupplierId { get; set; } //Ek stier nie die supplier details nie
        public DateTime Arrival { get; set; }
        public int Nights { get; set; }
        public int Price2Pay { get; set; }
        public int BuyingPrice { get; set; }
        public int AdminFee { get; set; }
        public bool Sold { get; set; }
        public bool Hold { get; set; }
    }
}
