using Application.BLL.DTOS;
using Application.BLL.DTOS.Department;
using Application.BLL.Services.Interfaces;
using Application.Presentation.Models;
using Application.Presentation.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Presentation.Controllers
{
    //[AllowAnonymous] by defult
    [Authorize] // anyone authernticated can access the actions (authorize)
    public class DepartmentController(
        IDepartmentService _departmentService,
        IWebHostEnvironment _environment,
        ILogger<DepartmentController> _logger
        ) : Controller
    {
        // BaseUrl/Department/Index
        // ViewData , ViewBag , ViewStorage ==> Same Storage
        // Extra Info (Extra Data)
        // 1. Send Controller ==> View 
        // 2. Send View ==> PartialView
        // 3. Send View ==> Layout
        public async Task<IActionResult> Index(string? DepartmentSearchName)
        {
            ViewData["Message"] = "Welcome To View Data";
            ViewBag.Message02 = "Welcome To View Bag";
            var departments = await _departmentService.GetAllDepartmentAsync(DepartmentSearchName);
            return View(departments);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    int result = await _departmentService.AddDpartmentAsync(new CreateDepartmentDto()
                    {
                        //Id = Id.Value,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.CreatedOn
                    });
                    string message;
                    if (result > 0)
                        message = "Department Created Successfully";
                    else
                        message = "Department Can Not Be Created";

                    TempData["Message"] = message;
                    // Redirect To Index Page
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Development ==> Action , Log Error In Console
                    if (_environment.IsDevelopment())
                    {
                        _logger.LogError($"Department Can Not Be Created Because {ex.Message}");
                    }
                    else
                    {
                        _logger.LogError($"Department Can Not Be Created Because {ex.Message}");
                        return View("ErrorView");
                    }
                    // Deployment ==> Action , Log Error In File, Db, Return View (Error)
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = await _departmentService.GetByIdAsync(id.Value);
            if (department is null) return NotFound();
            return View(department);
        } 
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return BadRequest(); // 400
            var department = await _departmentService.GetByIdAsync(id.Value);
            if (department is null) return NotFound(); // 404
            var departmentVM = new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description!,
                CreatedOn = department.CreatedOn.HasValue ? department.CreatedOn.Value : default
            };
            return View(departmentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? Id, DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    if (Id is null) return BadRequest();
                    int result = await _departmentService.UpdateDepartmentAsync(new UpdatedDepartmentDto()
                    {
                        Id = Id.Value,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.CreatedOn
                    });
                    string message;
                    if (result > 0)
                        message = "Department Updated Successfully";
                    else
                        message = "Department Can Not Be Updated";

                    TempData["Message"] = message;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Development ==> Action , Log Error In Console
                    if (_environment.IsDevelopment())
                    {
                        _logger.LogError($"Department Can Not Be Updated Because {ex.Message}");
                    }
                    else
                    {
                        _logger.LogError($"Department Can Not Be Updated Because {ex.Message}");
                        return View("ErrorView");
                    }
                    // Deployment ==> Action , Log Error In File, Db, Return View (Error)
                }
            }
            return View(viewModel);
        } 
        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool IsDeleted = await _departmentService.DeleteDepartmentAsync(id);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can Not Be Deleted");
                }

            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    _logger.LogError($"Department Can Not Be Delete Because {ex.Message}");
                }
                else
                {
                    _logger.LogError($"Department Can Not Be Delete Because {ex.Message}");
                    return View("ErrorView");
                }
            }
            return RedirectToAction(nameof(Delete), id);

        } 
        #endregion

    }
}
