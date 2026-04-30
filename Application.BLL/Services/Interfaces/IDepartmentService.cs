using Application.BLL.DTOS.Department;

namespace Application.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<int> AddDpartmentAsync(CreateDepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentAsync(string? DepartmentSearchName);
        Task<DepartmentDetailsDto> GetByIdAsync(int id);
        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto);
    }
}