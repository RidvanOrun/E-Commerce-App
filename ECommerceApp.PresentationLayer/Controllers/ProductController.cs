﻿



using ECommerceApp.ApplicationLayer.Grids;
using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.PresentationLayer.Models.VMs;
using ECommerceApp.PresentationLayer.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
   
        public ProductController(IProductService productService,
                                ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var model = await _productService.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.ProductName.Contains(search) || x.Description.Contains(search)).ToList();
            }

            return View(model);
        }     

        public IActionResult Details(int id)
        {
            ViewBag.productId = id;

            return View();
        }


        public async Task<IActionResult> GetList(CategoryDTO categoryDTO)
        {
            
            return View(await _productService.GetList(categoryDTO.Id));
        }

        public async Task<IActionResult> GetProductList(Category category)
        {
            List<Product> products = await _productService.GetList(category.Id);

            return View(products);
        }

    }
}
