using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PDIS.Models
{
    public class Allowance
    {
        [Key]
        public int AllowanceID { get; set; }

        [Required]
        [StringLength(100)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string AllowanceName { get; set; }

        [Required]
        public float Percentage { get; set; }
    }
}
