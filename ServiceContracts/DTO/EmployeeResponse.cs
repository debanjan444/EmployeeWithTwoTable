using Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
   

        public string? Name { get; set; }
      
        public string? Email { get; set; }
    
        public int? Salary { get; set; }
    
        public DateTime? JoiningDate { get; set; }
       
        public int? DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

    }
    public static class ExtensionMethod
    {
        public static EmployeeResponse ToEmployeeResponse(this Employee employee)
        {
            return new EmployeeResponse
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                JoiningDate = employee.JoiningDate,
                DepartmentId = employee.DepartmentId,
                //departmentName is not included because it is not part of the Employee entity. You may need to fetch it separately if required.
                DepartmentName = employee.Department?.DepartmentName
            };
        }
    }
}
