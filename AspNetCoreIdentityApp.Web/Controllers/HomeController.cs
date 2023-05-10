using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.Services;
using AspNetCoreIdentityApp.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public async Task<IActionResult> SignIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel parameters, string? returnURL = null)
        {
            returnURL = returnURL ?? Url.Content("~/"); //If returnURL is null, then returnURL will be equal to "~/". If returnURL is not null, then returnURL will be equal to returnURL.
            var hasUser = await _userManager.FindByEmailAsync(parameters.Email);
            var result = await _signInManager.PasswordSignInAsync(hasUser, parameters.Password, parameters.RememberMe, true); //If the user is not locked out, then the last parameter should be false.
            if (result.Succeeded)
            {
                return Redirect(returnURL);
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your account is locked out.");
                return View();
            }
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "You are not allowed to sign in.");
                return View();
            }
            if (result.RequiresTwoFactor)
            {
                ModelState.AddModelError(string.Empty, "You have to use two factor authentication.");
                return View();
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Cannot sign in for 3 minutes.");
                return View();
            }
            ModelState.AddModelError(string.Empty, $"Invalid login attempt. Attempt = {_userManager.GetAccessFailedCountAsync(hasUser).Result}");
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel parameters)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var identityResult = await _userManager.CreateAsync(new() { UserName = parameters.UserName, PhoneNumber = parameters.PhoneNumber, Email = parameters.Email }, parameters.Password);

            if (identityResult.Succeeded)
            {
                ViewBag.Message = "Sign in process is successful.";
                return View();
            }
            foreach (IdentityError item in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(PasswordResetModel parameter)
        {
            var hasUser = await _userManager.FindByEmailAsync(parameter.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return View();
            }
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser!);
            var passResetLink = Url.Action("ResetPassword", "Home", new { token = passwordResetToken, userID = hasUser!.Id }, Request.Scheme); //reset password page is in HomeController, so we write "Home" for controller name.

            await _emailService.SendResetEmail(passResetLink, hasUser.Email);

            TempData["success"] = "Password reset link has been sent to your e-mail address.";
            return RedirectToAction(nameof(ForgetPassword));
        }

        public IActionResult ResetPassword(string token, string userID)
        {
            TempData["token"] = token;
            TempData["userID"] = userID;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPass parameters)
        {
            string UserID = TempData["userID"].ToString();
            string token = TempData["token"].ToString();
            if (!ModelState.IsValid)
            {
                return View();
            }
            var hasUser = await _userManager.FindByIdAsync(UserID);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return View();
            }
            var result = await _userManager.ResetPasswordAsync(hasUser, token, parameters.Password);
            if (result.Succeeded)
            {
                TempData["success"] = "Password reset is successful.";
                return RedirectToAction(nameof(ResetPassword));
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}