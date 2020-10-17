using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Entities
{
    public class Vacation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ResortId")]
        public Resort Resort { get; set; }
        public int ResortId { get; set; }

        [ForeignKey("UnitSizeId")]
        public UnitSizes UnitSize { get; set; }
        public int UnitSizeId { get; set; }

        public int SupplierId { get; set; }

        public DateTime Arrival { get; set; }
        public int Nights { get; set; }
        public int Price2Pay { get; set; }
        public int BuyingPrice { get; set; }
        public int AdminFee { get; set; }
        public bool Sold { get; set; }
        public bool Hold { get; set; }
    }
}
