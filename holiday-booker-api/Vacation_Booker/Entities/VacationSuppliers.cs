﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Entities
{
    public class VacationSuppliers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("VacationId")]
        public Vacation Vacation { get; set; }
        public int VacationId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
    }
}
