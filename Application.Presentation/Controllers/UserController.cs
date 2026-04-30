using Application.DAL.Data.Models.IdentityModule;
using Application.Presentation.ViewModel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Presentation.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager, IWebHostEnvironment _env) : Controller
    {
        [Authorize]
        // Index , Detials, Update, Delete
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchValue)
        {
            var usersQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue))
                usersQuery = usersQuery.Where(u => u.Email.ToLower().Contains(SearchValue.ToLower()));
            var users = usersQuery.Select(u => new UserViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                //Roles = ,
            }).ToList();
            foreach (var item in users)
                item.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(item.Id));
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if(user is null) return NotFound();
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View(userVM);
        }

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await _userManager.GetRolesAsync(user)
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel userVM, string id)
        {
            if(!ModelState.IsValid) return View(userVM);
            if(userVM.Id != id) return BadRequest();
            string message = "";
            try
            {
                var user = await _userManager.FindByIdAsync(userVM.Id);
                if (user is null) return NotFound();
                user.FirstName = userVM.FirstName;
                user.LastName = userVM.LastName;
                user.Email = userVM.Email;
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded) 
                    return RedirectToAction(nameof(Index));
                else
                    message = "User Can Not Be Updated";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "User Can Not Be Updated Because Problem Happen";
            }
            ModelState.AddModelError("", message);
            return View(userVM);
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if(id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            string message = "";
            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "User Can Not Be Deleted";
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "User Can Not Be Deleted Because Problem Happen";
            }
            ModelState.AddModelError("", message);
            return RedirectToAction(nameof(Index));
        }
    }
}
