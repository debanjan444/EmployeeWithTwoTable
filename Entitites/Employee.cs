using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(20)]
        public string? Name { get; set; }
        [Required]
        [StringLength(20)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public int? Salary { get; set; }
        [Required]
        public DateTime? JoiningDate { get; set; }
        [Required]
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]

        public virtual Department? Department { get; set; }

    }
}
