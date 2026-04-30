using Application.BLL.DTOS.Department;
using Application.DAL.Data.Models.DepartmentModul;
using System.Runtime.CompilerServices;

namespace Application.BLL.Factories
{
    internal static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department d)
        {
            return new DepartmentDto()
            {
                DeptId = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                DateOfCreation = d.CreatedOn.HasValue ? DateOnly.FromDateTime(d.CreatedOn.Value) : default
            };
        }
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn.HasValue ? DateOnly.FromDateTime(department.CreatedOn.Value) : default,
                ModifiedBy = department.ModifiedBy,
                ModifiedOn = department.ModifiedOn.HasValue ? DateOnly.FromDateTime(department.ModifiedOn.Value) : default,
                IsDeleted = department.IsDeleted,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description
            };
        }
        public static Department ToEntity(this CreateDepartmentDto dto)
        {
            return new Department()
            {
                Code = dto.Code,
                Description = dto.Description,
                Name = dto.Name,
                CreatedOn = dto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
        public static Department ToEntity(this UpdatedDepartmentDto dto)
        {
            return new Department()
            {
                Id = dto.Id,
                Code = dto.Code,
                Description = dto.Description,
                Name = dto.Name,
                CreatedOn = dto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}
