using Application.BLL.DTOS.EmployeeDTOS;
using Application.BLL.Services.Attachment_Service;
using Application.BLL.Services.Interfaces;
using Application.DAL.Data.Models.EmployeeModul;
using Application.DAL.Data.Repositories.Interfaces;
using AutoMapper;

namespace Application.BLL.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper, IAttachmentService _attachmentService) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string? EmployeeSearchName, bool withTracking = false)
        {
            IEnumerable<Employee> employees;
            if(!string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = await _unitOfWork.employeeRepository.GetAllAsync(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            else
                employees = await _unitOfWork.employeeRepository.GetAllAsync(withTracking);


            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDetalisDto?> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.employeeRepository.GetByIdAsync(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetalisDto>(employee);
        }

        public async Task<int> AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreateEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
                var image = await _attachmentService.UploadAsync(employeeDto.Image, "images");
                employee.Image = image;
            }
            _unitOfWork.employeeRepository.Add(employee);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
        {
            _unitOfWork.employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            // soft deleted ==> update isDeleted to true
            var employee = await _unitOfWork.employeeRepository.GetByIdAsync(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.employeeRepository.Update(employee);
                return await _unitOfWork.SaveChangesAsync() > 0 ? true : false;
            }
        }

    }
}
