using Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class EmployeeUpdateRequest
    {
        public int EmployeeId { get; set; }
        [Required]

        public string? Name { get; set; }
        [Required]

        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public int? Salary { get; set; }
        [Required]
        public DateTime? JoiningDate { get; set; }
        [Required]
        public int? DepartmentId { get; set; }

        public Employee ToEmployee()
        {
            return new Employee
            {
                EmployeeId = this.EmployeeId,
                Name = this.Name,
                Email = this.Email,
                Salary = this.Salary,
                JoiningDate = this.JoiningDate,
                DepartmentId = this.DepartmentId
            };
        }
    }
}
