using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IDepartmentService
    {
        Task<DepartmentResponse> AddDepartment(DepartmentAddRequest? departmentAddRequest);
        Task<List<DepartmentResponse>?> GetAllDepartments();
        Task<DepartmentResponse?> GetDepartmentById(int departmentId);
        Task<DepartmentResponse?> UpdateDepartment(DepartmentUpdateRequest? departmentUpdateRequest);

        Task<bool> DeleteDepartment(int departmentId);

    }
}
