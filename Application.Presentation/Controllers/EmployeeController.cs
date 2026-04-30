using Application.BLL.DTOS.EmployeeDTOS;
using Application.BLL.Services.Interfaces;
using Application.DAL.Data.Models.EmployeeModul;
using Application.DAL.Data.Models.Shared;
using Application.Presentation.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Presentation.Controllers
{
    [Authorize] // anyone authernticated can access the actions (authorize)
    public class EmployeeController(
        IEmployeeService _employeeService,
        IWebHostEnvironment _environment,
        ILogger<EmployeeController> _logger
        ) : Controller
    {
        public async Task<IActionResult> Index(string? EmployeeSearchName)
        {
            var employeeDto = await _employeeService.GetAllEmployeesAsync(EmployeeSearchName);
            return View(employeeDto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _employeeService.AddEmployeeAsync(new CreateEmployeeDto()
                    {
                        Address = employeeViewModel.Address,
                        Age = employeeViewModel.Age,
                        Email = employeeViewModel.Email,
                        EmployeeTypes = employeeViewModel.EmployeeTypes,
                        Gender = employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        Name = employeeViewModel.Name,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        Salary = employeeViewModel.Salary,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image
                    });
                    //if(result > 0)
                    //{
                    //    return RedirectToAction(nameof(Index));
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError(string.Empty, "Employee Can Not Be Created");
                    //    return View(employeeViewModel);
                    //}
                    string message;
                    if (result > 0)
                        message = "Employee Created Successfully";
                    else
                        message = "Employee Can Not Be Created";

                    TempData["Message"] = message;
                    // Redirect To Index Page
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        _logger.LogError($"Employee Can Not Be Created Because {ex.Message}");
                    }
                    else
                    {
                        _logger.LogError($"Employee Can Not Be Created Because {ex.Message}");
                        return View("ErrorView");
                    }
                }
            }
            return View(employeeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var result = await _employeeService.GetByIdAsync(id.Value);
            if (result is null) return NotFound();
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id) // <--------------
        {
            if (!id.HasValue) return BadRequest();
            var result = await _employeeService.GetByIdAsync(id.Value);
            if (result is null) return NotFound();
            var resultVM = new EmployeeViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Age = result.Age,
                Address = result.Address,
                Salary = result.Salary,
                IsActive = result.IsActive,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                HiringDate = result.HiringDate,
                EmployeeTypes = Enum.Parse<EmployeeTypes>(result.EmployeeType),
                Gender = Enum.Parse< Gender>(result.Gender),
                DepartmentId = result.DepartmentId,
                //Image = result.Image
            };
            return View(resultVM);
        }  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if(employeeViewModel is null || id != employeeViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return BadRequest();
           
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(new UpdatedEmployeeDto
                {
                    Id = employeeViewModel.Id,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age,
                    Email = employeeViewModel.Email,
                    EmployeeTypes = employeeViewModel.EmployeeTypes,
                    Gender = employeeViewModel.Gender,
                    HiringDate = employeeViewModel.HiringDate,
                    IsActive = employeeViewModel.IsActive,
                    Name = employeeViewModel.Name,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    Salary = employeeViewModel.Salary,
                    DepartmentId = employeeViewModel.DepartmentId,
                    //Image = employeeViewModel.Image
                });
                string message;
                if (result > 0)
                    message = "Employee Update Successfully";
                else
                    message = "Employee Can Not Be Updated";

                TempData["Message"] = message;
                // Redirect To Index Page
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    _logger.LogError($"Employee Can Not Be Updated Because {ex.Message}");
                }
                else
                {
                    _logger.LogError($"Employee Can Not Be Updated Because {ex.Message}");
                    return View("ErrorView");
                }
            }
            
            return View(employeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            try
            {
                var isDeleted = await _employeeService.DeleteEmployeeAsync(id.Value);
                if(isDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can Not Be Deleted");
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    _logger.LogError($"Employee Can Not Be Delete Because {ex.Message}");
                }
                else
                {
                    _logger.LogError($"Employee Can Not Be Delete Because {ex.Message}");
                    return View("ErrorView");
                }
            }
            return View(nameof(Index), id);
        }
    }
}
