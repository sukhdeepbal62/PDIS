using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PDIS.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        [StringLength(50)]
        public string ExtName { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        public float BasicSalary { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        [InverseProperty("Employees")]
        public Department Department { get; set; }

        [Required]
        public int PostID { get; set; }

        [ForeignKey("PostID")]
        [InverseProperty("PostEmployees")]
        public Post Post { get; set; }

        [NotMapped]
        public PhotoUpload FileUpload { get; set; }

        public ICollection<Salary> Salaries { get; set; }
    }

    public class PhotoUpload
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
