using Application.DAL.Data.Models.IdentityModule;
using Application.Presentation.ViewModel;
using Application.Presentation.ViewModel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController(RoleManager<IdentityRole> _roleManager, IWebHostEnvironment _env, UserManager<ApplicationUser> _userManager) : Controller
    {
        [HttpGet]
        public IActionResult Index(string SearchValue)
        {
            var rolesQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
                rolesQuery = rolesQuery.Where(r => r.Name.ToLower().Contains(SearchValue.ToLower()));
            var roles = rolesQuery.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            return View(roles);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleView)
        {
            if(!ModelState.IsValid) return View(roleView);
            string message = "";
            try
            {
                var role = new IdentityRole()
                {
                    Id = roleView.Id,
                    Name = roleView.Name
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Role Can Not Be Created";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Role Can Not Be Updated Becuase Problem Happen";
            }
            ModelState.AddModelError("", message);
            return View(roleView);
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if(role is null) return NotFound();
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            });
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            var users = await _userManager.Users.ToListAsync();
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                User = users.Select(u => new UserRoleViewModel()
                {
                    Id = u.Id,
                    Name = u.UserName,
                    IsSelected = _userManager.IsInRoleAsync(u, role.Name).Result
                }).ToList()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel roleView, string id)
        {
            if (!ModelState.IsValid) return View(roleView);
            if (roleView.Id != id) return BadRequest();
            string message = "";
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return NotFound();
                role.Name = roleView.Name;
                var result = await _roleManager.UpdateAsync(role);
                foreach (var item in roleView.User)
                {
                    var user = await _userManager.FindByIdAsync(item.Id);
                    if(user is not null)
                    {
                        if (item.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                            await _userManager.AddToRoleAsync(user, role.Name);
                        else if(!item.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                        
                }
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Role Can Not Be Updated";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Role Can Not Be Updated Because Probem Happen";
            }
            ModelState.AddModelError("", message);
            return View(roleView);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            string message = "";
            try
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Role Can Not Be Deleted";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Role Can Not Be Updated Because Problem Happen";
            }
            ModelState.AddModelError("", message);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
