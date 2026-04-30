using Application.BLL.DTOS.EmployeeDTOS;

namespace Application.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        // GET ALL
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string? EmployeeSearchName, bool withTracking = false);

        // GET BY ID
        Task<EmployeeDetalisDto> GetByIdAsync(int id);

        // ADD, DELETE
        Task<int> AddEmployeeAsync(CreateEmployeeDto employeeDto);

        // DELETE
        Task<bool> DeleteEmployeeAsync(int id);

        // UPDATE
        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto);
    }
}
