using ECommerceApp.ApplicationLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Models.Component
{
    public class ProductDetail:ViewComponent
    {
        private readonly IProductService _productService;
        public ProductDetail(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id) => View(await _productService.GetById(id));
    }
}
