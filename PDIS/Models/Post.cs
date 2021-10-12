using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PDIS.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [Required]
        [StringLength(100)]
        public string PostName { get; set; }

        [Required]
        public float Salary { get; set; }

        public ICollection<Employee> PostEmployees { get; set; }
    }
}
