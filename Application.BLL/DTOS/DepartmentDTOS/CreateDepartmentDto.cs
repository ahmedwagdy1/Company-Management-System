namespace Application.BLL.DTOS.Department
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; }
    }
}
