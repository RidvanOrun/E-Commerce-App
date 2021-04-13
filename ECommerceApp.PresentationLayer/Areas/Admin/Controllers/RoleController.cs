using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.DomainLayer.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(UserManager<AppUser> userManager,
                             RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize]
        public IActionResult Index() => View(_roleManager.Roles);

        [Authorize]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([MinLength(2, ErrorMessage ="Minimum lebght is 2"),
                                                Required(ErrorMessage ="Must to into role name")] string name)
        {
            if (ModelState.IsValid)
            {
                name = name.Trim().Replace(" ", string.Empty);
                IdentityResult ıdentityResult = await _roleManager.CreateAsync(new AppRole(name));
                if (ıdentityResult.Succeeded)
                {
                    TempData["Success"] = "The role has been created..!";
                    return RedirectToAction("Index");
                }
                else foreach (IdentityError error in ıdentityResult.Errors) ModelState.AddModelError("", error.Description);
            }

            TempData["Error"] = "The role hasm't been created..!";
            return View(name);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNotRole = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNotRole;
                list.Add(user);

            }

            return View(new RoleEditDTO { Role = role, HasRole = hasRole, HasNotRole = hasNotRole });
        }

        [Authorize]
        [HttpPost]
       
        public async Task<IActionResult> Edit(RoleEditDTO roleEdit)
        {
            IdentityResult result;

            foreach (var userId in roleEdit.AddIds ?? new string[] { })
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                result = await _userManager.AddToRoleAsync(appUser, roleEdit.RoleName);
            }

            foreach (var userId in roleEdit.DeleteIds ?? new string[] { })
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(appUser, roleEdit.RoleName);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Remove(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }

}
