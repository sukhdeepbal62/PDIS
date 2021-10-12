using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PDIS.Models
{
    public class SalaryDetail
    {
        [Key]
        public int DetailsID { get; set; }

        [Required]
        [StringLength(100)]
        public string AllowanceName { get; set; }

        [Required]        
        public float Amount { get; set; }

        [Required]
        public int SalaryID { get; set; }

        [ForeignKey("SalaryID")]
        [InverseProperty("SalaryDetails")]
        public Salary Salary { get; set; }
    }
}
