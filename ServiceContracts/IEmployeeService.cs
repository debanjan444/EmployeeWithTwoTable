using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IEmployeeService
    {
        Task<EmployeeResponse?> AddEmployee(EmployeeAddRequest? employeeAddRequest);
        Task<List<EmployeeResponse>?> GetAllEmployees();
        Task<EmployeeResponse?> GetEmployeeById(int employeeId);
        Task<EmployeeResponse?> UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest);
        Task<bool> DeleteEmployee(int employeeId);
    }
}
