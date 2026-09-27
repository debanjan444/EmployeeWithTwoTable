using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;

namespace EmployeeWithTwoTable.Controllers
{
 
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(IDepartmentService departmentService,IEmployeeService employeeService ) {
            _departmentService = departmentService;
            _employeeService = employeeService;


        }


        [Route("/")]
        [Route("Index")]
        
        public async Task<IActionResult> Index()
        {
            List<EmployeeResponse>? emplist = await _employeeService.GetAllEmployees();

            return View(emplist);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAllDepartments();

            ViewBag.Departments = departments?
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.DepartmentName
                })
                .ToList();

            return View();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(EmployeeAddRequest? employeeAddRequest)
        {

            if (!ModelState.IsValid)
            {
                string message = "";
              var errosMessages =   ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                message = string.Join("<br/>", errosMessages);
                return BadRequest(message);


            }
           EmployeeResponse? employeeResponse =   await _employeeService.AddEmployee(employeeAddRequest);
            if (employeeResponse == null)
            {
                return BadRequest("Employee not added");
            }
            return RedirectToAction("Index");

        }


    }
}
