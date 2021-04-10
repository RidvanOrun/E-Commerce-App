using ECommerceApp.ApplicationLayer.Extensions;
using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUser;

        public AccountController(IAppUserService appUser)
        {
            _appUser = appUser;
        }
        #region Register
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUser.Register(model);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                foreach (var item in result.Errors) ModelState.AddModelError(string.Empty, item.Description);
            }
            return View(model);
        }
        #endregion

        #region LogIn
        public IActionResult Login()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Home","Admin");//"actionName, controllerName"
            }

            else if (User.IsInRole("seller"))
            {
                return RedirectToAction("Index", "Home", "Seller");
            }
            else if (User.IsInRole("member"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            

            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction(nameof(HomeController.Index), "Home");
            //}

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUser.LogIn(model);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home"); // Eğer giriş başarılı olursa HomeController'daki Home Action'a yönlendir.
                }
                ModelState.AddModelError(String.Empty, "Geçersiz giriş denemesi..!");
            }
            return View();
        }
        #endregion

        #region LogOut
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _appUser.LogOut();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion

        public async Task<IActionResult> EditProfile(string userName)
        {
            if (userName == User.Identity.Name)
            {
                var user = await _appUser.GetById(User.GetUserId());

                if (user == null) return NotFound();

                return View(user);
            }
            else
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileDTO model)
        {
            await _appUser.EditUser(model);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Details (ProfileDTO model)
        {            
            var user = await _appUser.GetByUserName(model.UserName);
            return View(user);
        }
    }
}
