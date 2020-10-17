using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Booker.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(256)]
        [Required]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Extention { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool Disabled { get; set; }
        [MaxLength(256)]
        public string ResetCode { get; set; }
        public DateTime? ResetExpiryDate { get; set; }
    }
}
