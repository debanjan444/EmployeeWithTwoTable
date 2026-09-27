using Entitites;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeDbContext _db;
        public DepartmentService(EmployeeDbContext db)
        {
            _db = db;
        }

        public async Task<DepartmentResponse> AddDepartment(DepartmentAddRequest? departmentAddRequest)
        {
            if(departmentAddRequest == null)
            {
                throw new ArgumentNullException(nameof(departmentAddRequest));
            }
            ValidationContext validationContext = new ValidationContext(departmentAddRequest);
            List<ValidationResult> validationResults = new List<ValidationResult>();
          bool isValid =   Validator.TryValidateObject(departmentAddRequest, validationContext, validationResults, true);
            if (!isValid) {
                throw new ArgumentException("Invalid department details");

            }

           Department department =  departmentAddRequest.ToDepartment();
            _db.Departments.Add(department);
            await _db.SaveChangesAsync();
            DepartmentResponse deptRes = department.ToDepartmentResponse();
            return deptRes;

        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            Department? department = await _db.Departments.FirstOrDefaultAsync(dept => dept.DepartmentId == departmentId);
            if(department == null)
            {
                return false;
            }
             _db.Departments.Remove(department);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<DepartmentResponse>?> GetAllDepartments()
        {
            List<Department> departments = await  _db.Departments.ToListAsync();
            List<DepartmentResponse> departmentResponses =  departments.Select(dept => dept.ToDepartmentResponse()).ToList();
            return departmentResponses;
        }

        public async Task<DepartmentResponse?> GetDepartmentById(int departmentId)
        {
           Department? department = await   _db.Departments.FirstOrDefaultAsync(dept => dept.DepartmentId == departmentId);
            if(department == null)
            {
                return null;
            }
            DepartmentResponse departmentResponse = department.ToDepartmentResponse();
            return departmentResponse;
        }

        public async  Task<DepartmentResponse?> UpdateDepartment(DepartmentUpdateRequest? departmentUpdateRequest)
        {
            if(departmentUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(departmentUpdateRequest));
            }
            ValidationContext validationContext = new ValidationContext(departmentUpdateRequest);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(departmentUpdateRequest, validationContext, validationResults, true);
            if (!isValid)
            {
                throw new ArgumentException("Invalid department details");
            }
            Department? department = await  _db.Departments.FirstOrDefaultAsync(dept => dept.DepartmentId == departmentUpdateRequest.DepartmentId);
            if(department == null)
            {
                return null;
            }
            department.DepartmentName = departmentUpdateRequest.DepartmentName;
            await _db.SaveChangesAsync();
            DepartmentResponse departmentResponse = department.ToDepartmentResponse();
            return departmentResponse;


        }
    }
}
