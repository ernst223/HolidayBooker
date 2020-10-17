using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
