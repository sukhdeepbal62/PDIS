using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PDIS.Models
{
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }

        [Required]
        [StringLength(100)]
        public string SalaryMonth { get; set; }

        [Required]
        public int SalaryYear { get; set; }

        public float TotalSalary { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("Salaries")]
        public Employee Employee { get; set; }

        public ICollection<SalaryDetail> SalaryDetails { get; set; }
    }
}
