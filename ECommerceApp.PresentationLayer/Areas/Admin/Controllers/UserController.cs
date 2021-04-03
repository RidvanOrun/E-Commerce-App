using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using ECommerceApp.DomainLayer.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;

        public UserController(UserManager<AppUser> userManager, IAppUserService appUserService)
        {
            _userManager = userManager;
            _appUserService = appUserService;
        }

        public IActionResult Index() => View(_userManager.Users);

        public async Task<IActionResult> Update(int id)
        {
            var user = await _appUserService.GetById(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.EditUser(model);
                TempData["Success"] = "The product edited..!";
                return RedirectToAction("Index");
            }
            else
                TempData["Error"] = "The product hasn't been edited..!";
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {           
            await _appUserService.DeleteUser(id);
            return RedirectToAction("Index");
        }

    }
}
