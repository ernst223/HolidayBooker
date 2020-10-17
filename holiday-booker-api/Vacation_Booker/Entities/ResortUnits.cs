using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Entities
{
    public class ResortUnits
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UnitSizeId")]
        public UnitSizes UnitSize { get; set; }
        public int UnitSizeId { get; set; }

        [ForeignKey("ResortId")]
        public Resort Resort { get; set; }
        public int ResortId { get; set; }
    }
}
