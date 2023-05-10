using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public MemberController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var userviewmodel = new UserViewModel
            {
                UserName = currentUser!.UserName,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                PictureUrl = currentUser.Photo
            };
            return View(userviewmodel);
        }

        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel parameters)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
                if (currentUser != null)
                {
                    var checkoldpass = await _userManager.CheckPasswordAsync(currentUser, parameters.PasswordOld);

                    if (!checkoldpass)
                    {
                        ModelState.AddModelError(string.Empty, "Old password is incorrect");
                        return View(parameters);
                    }
                    var result = await _userManager.ChangePasswordAsync(currentUser, parameters.PasswordOld, parameters.PasswordNew);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(currentUser);
                        await _signInManager.SignOutAsync();
                        TempData["success"] = "Password changed successfully";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password change error");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                }
            }
            return View(parameters);
        }

        public async Task<IActionResult> UserUpdate()
        {
            ViewBag.gender = new SelectList(Enum.GetNames(typeof(Gender)));

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var UIModel = new UserEditViewModel()
            {
                UserName = currentUser!.UserName!,
                Email = currentUser.Email!,
                PhoneNumber = currentUser.PhoneNumber!,
                BirthDate = currentUser.Birthday,
                Gender = currentUser.Gender
            };

            return View(UIModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserEditViewModel parameters)
        {
            if (!ModelState.IsValid)
            {
                return View(parameters);
            }
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            currentUser!.UserName = parameters.UserName;
            currentUser.Email = parameters.Email;
            currentUser.PhoneNumber = parameters.PhoneNumber;
            currentUser.City = parameters.City;
            currentUser.Gender = parameters.Gender;
            currentUser.Birthday = parameters.BirthDate;



            if (parameters.Photo != null && parameters.Photo.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(parameters.Photo.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\userPhoto", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await parameters.Photo.CopyToAsync(stream);
                
                currentUser.Photo = fileName;
            }

            var updateResult = await _userManager.UpdateAsync(currentUser);

            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot update user");
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);

            TempData["success"] = "User updated successfully";
            return RedirectToAction("Index");
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
