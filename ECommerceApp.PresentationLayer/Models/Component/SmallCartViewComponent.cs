using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.PresentationLayer.Models.Extensions;
using ECommerceApp.PresentationLayer.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Models.Component
{
    public class SmallCartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            SmallCartViewModel smallCartViewModel;

            if (cart == null || cart.Count == 0)
            {
                smallCartViewModel = null;
            }
            else
            {
                smallCartViewModel = new SmallCartViewModel
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }

            return View(smallCartViewModel);
        }
    }
}
