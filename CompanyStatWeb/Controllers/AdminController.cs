using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyStatWeb.Models;
using CompanyStatWeb.ViewModels;

namespace CompanyStatWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // --- Settings Section ---
        [HttpGet]
        public IActionResult Settings()
        {
            // In a real app, these would come from a database or configuration file.
            // For now, we return a default model or one with some static values.
            var model = new SiteSettingsViewModel
            {
                SiteName = "CompanyStat Pro",
                ContactEmail = "admin@company.com",
                ItemsPerPage = 20,
                MaintenanceMode = false
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings(SiteSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Here we would save the settings to DB or Config.
                // Simulating a save:
                TempData["SuccessMessage"] = "Settings saved successfully!";
                return RedirectToAction(nameof(Settings));
            }
            return View(model);
        }

        // --- User Management Section ---
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles,
                    IsAdmin = roles.Contains("Admin")
                });
            }

            return View(userViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // Prevent deleting the initial admin for safety
                if (user.Email == "admin@company.com")
                {
                    TempData["ErrorMessage"] = "Cannot delete the super admin account.";
                    return RedirectToAction(nameof(Users));
                }

                await _userManager.DeleteAsync(user);
                TempData["SuccessMessage"] = "User deleted successfully.";
            }
            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleAdmin(string id)
        {
             var user = await _userManager.FindByIdAsync(id);
             if (user != null)
             {
                 if (await _userManager.IsInRoleAsync(user, "Admin"))
                 {
                     // Downgrade to User
                     if (user.Email == "admin@company.com")
                     {
                         TempData["ErrorMessage"] = "Cannot remove Admin role from super admin.";
                         return RedirectToAction(nameof(Users));
                     }
                     await _userManager.RemoveFromRoleAsync(user, "Admin");
                     await _userManager.AddToRoleAsync(user, "User");
                 }
                 else
                 {
                     // Upgrade to Admin
                     await _userManager.RemoveFromRoleAsync(user, "User");
                     await _userManager.AddToRoleAsync(user, "Admin");
                 }
                 TempData["SuccessMessage"] = "User permissions updated.";
             }
             return RedirectToAction(nameof(Users));
        }
    }
}
