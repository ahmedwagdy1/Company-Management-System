namespace Application.BLL.DTOS.Department
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } 
        public DateOnly? CreatedOn { get; set; } 
        public int ModifiedBy { get; set; } 
        public DateOnly? ModifiedOn { get; set; } 
        public bool IsDeleted { get; set; } 
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
    }
}
