using AspNetCoreIdentityApp.Web.Areas.Admin.Models;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Momenttum Admin")]
        public async Task<IActionResult> Index()
        {
            List<RoleListViewModel> model = await _roleManager.Roles.Select(role => new RoleListViewModel() { Id = role.Id, Name = role.Name }).ToListAsync();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateViewModel parameters)
        {
            var result = await _roleManager.CreateAsync(new Role
            {
                Name = parameters.RoleName
            });

            if (result.Succeeded)
            {
                TempData["Success"] = "Role created successfully";
                return RedirectToAction(nameof(RoleController.Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error while creating role");
            }
            return View();
        }

        public async Task<IActionResult> Update(string id) { 
            var roletoupdate = await _roleManager.FindByIdAsync(id);
            if (roletoupdate != null)
            {
                RoleUpdateViewModel model = new()
                {
                    Name = roletoupdate.Name!,
                    Id = roletoupdate.Id
                };
                return View(model);
            }
            else
            {
                TempData["Success"] = "Role not found";
                return RedirectToAction(nameof(RoleController.Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoleUpdateViewModel parameters)
        {
            Role role = await _roleManager.FindByIdAsync(parameters.Id);
            if (role != null)
            {
                role.Name = parameters.Name;
                await _roleManager.UpdateAsync(role);
                TempData["Success"] = "Role has been updated";
                return RedirectToAction(nameof(RoleController.Index));
            }
            else
            {
                TempData["Success"] = "Role not found";
                return RedirectToAction(nameof(RoleController.Index));
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Role has been deleted";                    
                }
                else
                {
                    TempData["Error"] = "Something went wrong while deleting role";
                }                
            }
            else
            {
                TempData["Success"] = "Role not found";                
            }
            return RedirectToAction(nameof(RoleController.Index));
        }
        
        public async Task<IActionResult> RoleAssignment(string Id)
        {
            var currentUser = await _userManager.FindByIdAsync(Id);
            ViewBag.UserId = Id;
            var roles = await _roleManager.Roles.ToListAsync();

            List<RoleAssignmentViewModel> model = new();

            roles.ForEach(role =>
            {
                var roleAssignmentViewModel = new RoleAssignmentViewModel()
                {
                    Id = role.Id,
                    Name = role.Name!,
                    IsAssigned = _userManager.IsInRoleAsync(currentUser!, role.Name!).Result
                };
                model.Add(roleAssignmentViewModel);
            });

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssignment(List<RoleAssignmentViewModel> parameters, string userid)
        {
            var currentUser = await _userManager.FindByIdAsync(userid);
            foreach (var role in parameters)
            {
                if (role.IsAssigned)
                {
                    await _userManager.AddToRoleAsync(currentUser!, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(currentUser!, role.Name);
                }
            }
            TempData["Success"] = "Role assignment has been updated";
            return RedirectToAction(nameof(HomeController.UserList), "Home");
        }
    }
}
