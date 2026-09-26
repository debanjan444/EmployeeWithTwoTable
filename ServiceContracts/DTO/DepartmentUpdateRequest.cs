using Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public  class DepartmentUpdateRequest
    {
        [Required]

        public int DepartmentId { get; set; }
        [Required]
        public String? DepartmentName { get; set; }
        public Department ToDepartment()
        {
            return new Department
            {
                DepartmentId = this.DepartmentId,
                DepartmentName = this.DepartmentName
            };
        }
    }
}
