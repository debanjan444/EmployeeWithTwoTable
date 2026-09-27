using Entitites;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _db;
        public EmployeeService(EmployeeDbContext db) { 
        _db = db;
        }
        public async Task<EmployeeResponse?> AddEmployee(EmployeeAddRequest? employeeAddRequest)
        {
           if(employeeAddRequest == null)
            {
                throw new ArgumentNullException(nameof(employeeAddRequest));
            }
            ValidationContext validationContext = new ValidationContext(employeeAddRequest);
            List<ValidationResult> validationResults = new List<ValidationResult>();    
            bool isValid = Validator.TryValidateObject(employeeAddRequest, validationContext, validationResults, true);
            if(!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ArgumentException($"EmployeeAddRequest is not valid: {errorMessages}");
            }
            var deptId = employeeAddRequest.DepartmentId;
           var department =  await _db.Departments.FirstOrDefaultAsync(d => d.DepartmentId == deptId); 
            if(department == null)
            {
                throw new ArgumentException($"Department with ID {deptId} does not exist.");
            }
            Employee employee = employeeAddRequest.ToEmployee();
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            EmployeeResponse employeeResponse = employee.ToEmployeeResponse();
            employeeResponse.DepartmentName = department.DepartmentName;
            return employeeResponse;

        }

        public async  Task<bool> DeleteEmployee(int employeeId)
        {
           Employee? employee =   await _db.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
           if(employee == null)
            {
                return false;
            }
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<EmployeeResponse>?> GetAllEmployees()
        {
           List<Employee> employees = await _db.Employees.Include(e => e.Department).ToListAsync();
            List<EmployeeResponse> employeeResponses = employees.Select(e => e.ToEmployeeResponse()).ToList();
           
            return employeeResponses;
        }

        public async Task<EmployeeResponse?> GetEmployeeById(int employeeId)
        {
           Employee? employee =  await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if(employee == null)
            {
                return null;
            }
            EmployeeResponse employeeResponse = employee.ToEmployeeResponse();
         
            return employeeResponse;
        }

        public async Task<EmployeeResponse?> UpdateEmployee(EmployeeUpdateRequest? employeeUpdateRequest)
        {
            if(employeeUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(employeeUpdateRequest));
            }
            ValidationContext validationContext = new ValidationContext(employeeUpdateRequest);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(employeeUpdateRequest, validationContext, validationResults, true);
            if(!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ArgumentException($"EmployeeUpdateRequest is not valid: {errorMessages}");
            }
            Employee? employee =  await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == employeeUpdateRequest.EmployeeId);
            if(employee == null)
            {
                return null;
            }
            var department = await _db.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == employeeUpdateRequest.DepartmentId);

            if (department == null)
            {
                throw new ArgumentException(
                    $"Department with ID {employeeUpdateRequest.DepartmentId} does not exist.");
            }
            employee.Name = employeeUpdateRequest.Name;
            employee.Email = employeeUpdateRequest.Email;
            employee.Salary = employeeUpdateRequest.Salary;
            employee.JoiningDate = employeeUpdateRequest.JoiningDate;
            employee.DepartmentId = employeeUpdateRequest.DepartmentId;
            employee.Department = department;

            await _db.SaveChangesAsync();
           return  employee.ToEmployeeResponse();
        }
    }
}
