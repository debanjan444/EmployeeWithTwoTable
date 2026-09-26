using Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class DepartmentResponse
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }


    }
    public static class ExtensionMetod
    {
        public static DepartmentResponse ToDepartmentResponse(this Department department)
        {
            return new DepartmentResponse
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
        }
    };
}
