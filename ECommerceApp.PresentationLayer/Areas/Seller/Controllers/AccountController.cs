using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class AccountController:Controller
    {
        private readonly IAppUserService _appUser;

        public AccountController(IAppUserService appUser)
        {
            _appUser = appUser;
        }

        public async Task<IActionResult> Details(ProfileDTO model)
        {
            var user = await _appUser.GetByUserName(model.UserName);
            return View(user);
        }
    }
}
