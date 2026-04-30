using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.BLL.DTOS.EmployeeDTOS
{
    public class EmployeeDetalisDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public string Gender { get; set; }
        [Display(Name ="Employee Type")]
        public string EmployeeType { get; set; }
        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }
        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }
        [Display(Name = "Modified By")]
        public int ModifiedBy { get; set; }
        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }
        public string? Department { get; set; }
        public int? DepartmentId { get; set; }
        public string? Image { get; set; }

    }
}
