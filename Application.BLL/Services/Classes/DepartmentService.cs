using Application.BLL.DTOS.Department;
using Application.BLL.Factories;
using Application.BLL.Services.Interfaces;
using Application.DAL.Data.Models.DepartmentModul;
using Application.DAL.Data.Repositories.Interfaces;

namespace Application.BLL.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {

        // GetAll ==> Id, Name, Code, Description, DateOfCreation
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentAsync(string? DepartmentSearchName)
        {
            IEnumerable<Department> departments;
            if (!string.IsNullOrWhiteSpace(DepartmentSearchName))
                departments = await _unitOfWork.departmentRepository.GetAllAsync(e => e.Name.ToLower().Contains(DepartmentSearchName.ToLower()));
            else
                departments = await _unitOfWork.departmentRepository.GetAllAsync();

            return departments.Select(d => d.ToDepartmentDto());  // Use Extension Method Mapping
        }

        // GetById
        public async Task<DepartmentDetailsDto> GetByIdAsync(int id)
        {
            var department = await _unitOfWork.departmentRepository.GetByIdAsync(id);
            // Mapping Department to DepartmentDetailsDto

            // Mapping Types:
            // 1.AutoMapping ==> Package: AutoMapper

            // 2.Extension Method Mapping
            return department?.ToDepartmentDetailsDto();

            // 3.Constructor Mapping

            // 4.Manual Mapping
            //return new DepartmentDetailsDto()
            //{
            //    Id = department.Id,
            //    CreatedBy = department.CreatedBy,
            //    CreatedOn = department.CreatedOn.HasValue ? DateOnly.FromDateTime(department.CreatedOn.Value) : default,
            //    ModifiedBy = department.ModifiedBy,
            //    ModifiedOn = department.ModifiedOn.HasValue ? DateOnly.FromDateTime(department.ModifiedOn.Value) : default,
            //    IsDeleted = department.IsDeleted,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description
            //};
        }

        // Add
        public async Task<int> AddDpartmentAsync(CreateDepartmentDto departmentDto)
        {
            _unitOfWork.departmentRepository.Add(departmentDto.ToEntity());
            return await _unitOfWork.SaveChangesAsync();
        }

        // Update
        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.departmentRepository.Update(departmentDto.ToEntity());
            return await _unitOfWork.SaveChangesAsync();
        }

        // Delete
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.departmentRepository.GetByIdAsync(id);
            if (department is null) return false;
            _unitOfWork.departmentRepository.Delete(department);
            return await _unitOfWork.SaveChangesAsync() > 0 ? true : false;
        }

    }
}
