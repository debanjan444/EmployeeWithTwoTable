using Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class EmployeeAddRequest
    {
        [Required(ErrorMessage ="Name can not be empty")]
        
        public string? Name { get; set; }
        [Required(ErrorMessage ="Email can not be empty")]
       
        [EmailAddress]

        public string? Email { get; set; }
        [Required(ErrorMessage = "salary can not be empty")]
        public int? Salary { get; set; }
        [Required(ErrorMessage = "joining date can not be empty")]
        public DateTime? JoiningDate { get; set; }
        [Required(ErrorMessage = "dept can not be empty")]
        public int? DepartmentId { get; set; }

        public Employee ToEmployee()
        {
            return new Employee
            {
                Name = this.Name,
                Email = this.Email,
                Salary = this.Salary,
                JoiningDate = this.JoiningDate,
                DepartmentId = this.DepartmentId
            };
        }
    }
}
