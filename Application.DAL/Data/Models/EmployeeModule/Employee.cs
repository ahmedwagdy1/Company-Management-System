namespace Application.DAL.Data.Models.EmployeeModul
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        // Gender ==> Enum [Female , Male]
        public Gender Gender { get; set; }
        // EmployeeType ==> PartTimeEmployee , FullTimeEmployee
        public EmployeeTypes EmployeeTypes { get; set; }
        public virtual Department Department { get; set; }
        public int? DepartmentId { get; set; }
        public string? Image { get; set; }
    }
}
