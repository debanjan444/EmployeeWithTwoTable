using System.ComponentModel.DataAnnotations;

namespace Entitites
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(20)]
        public string? DepartmentName { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }

    }
}
