using Application.BLL.Services.EmailSender;
using Application.DAL.Data.Models.IdentityModule;
using Application.DAL.Data.Models.Shared;
using Application.Presentation.ViewModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManger,
        SignInManager<ApplicationUser> _loginManger,
        IEmailSender _emailSender
        ) : Controller
    {
        // Register, Login, Logout 
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerView)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = new ApplicationUser()
            {
                UserName = registerView.UserName,
                FirstName = registerView.FirstName,
                LastName = registerView.LastName,
                Email = registerView.Email
            };
            var result = _userManger.CreateAsync(user, registerView.Password).Result;
            if (result.Succeeded)
                return RedirectToAction(nameof(Login));
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(registerView);
            }
        }

        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public IActionResult Login(LoginViewModel loginView)
        {
            if(!ModelState.IsValid) return View(loginView);
            // 1. Find user by Email
            // 2. If user is not null
            // 3. check password
            // 4. Sign In
            // 5. Checked is acc is locked or is not allowed
            var user = _userManger.FindByEmailAsync(loginView.Email).Result;
            if(user is not null)
            {
                var flag = _userManger.CheckPasswordAsync(user, loginView.Password).Result;
                if (flag)
                {
                    // user with email is exist and password is correct
                    var result = _loginManger.PasswordSignInAsync(user, loginView.Password, loginView.RememberMe, false).Result;
                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your account is not allowed to login");
                    if(result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your account is locked out");
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login");
            return View(loginView);
        }

        [HttpGet]
        public new IActionResult SignOut()
        {
            _loginManger.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgetPassword() => View();
        [HttpPost]
        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordView)
        {
            if (ModelState.IsValid)
            {
                var user = _userManger.FindByEmailAsync(forgetPasswordView.Email).Result;
                if (user is not null)
                {
                    var token = _userManger.GeneratePasswordResetTokenAsync(user).Result;
                    var url = Url.Action("ResetPassword", "Account", new {email = user.Email, token}, Request.Scheme);
                    // BaseUrl/Account/ResetPassword?Email=wagdy22sdc@gmail.com?token=cvsdsdvcasfvfgvrvgdsavc
                    // To, Subject, Body
                    var email = new Email()
                    {
                        To = forgetPasswordView.Email,
                        Subject = "Reset Your Password",
                        Body = url
                    };
                    // Send Email
                    _emailSender.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
                else
                    ModelState.AddModelError("", "Invalid Operation Please Try Again");
            }
            return View(forgetPasswordView);
        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordView)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = _userManger.FindByEmailAsync(email).Result;
                if (user != null)
                {
                    var result = _userManger.ResetPasswordAsync(user, token, resetPasswordView.NewPassword).Result;
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));
                }
            }
            ModelState.AddModelError("", "Invalid Operations Please Try Again");
            return View(resetPasswordView);
        }

        public IActionResult AccessDenied() => View();
    }
}
