using ECommerceApp.ApplicationLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Models.Component
{
    public class ProductsViewComponent:ViewComponent
    {
        private readonly IProductService _productService;

        public ProductsViewComponent(IProductService productService)
        {
            this._productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _productService.GetOrderByList());
        }
    }
}
