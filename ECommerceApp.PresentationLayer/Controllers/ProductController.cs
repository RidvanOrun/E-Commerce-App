

using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using ECommerceApp.DomainLayer.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Controllers
{
    //[Authorize(Roles = "member")]
    [Authorize(Roles = "admin")]
    [Authorize(Roles = "seller")]
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

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAll());
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


        //public async Task<IActionResult> ProductByCategory(CategoryDTO categoryDTO)
        //{
        //    Category category = await _categoryService.



        //Category category = await _categoryRepository.FirstOrDefault(x => x.Slug == categorySlug);

        //    //if (category == null) return RedirectToAction("Index");

        //    //ViewBag.CategoryName = category.Name;
        //    //ViewBag.CategorySlug = category.Slug;
        //    //List<Product> products = await _productRepository.Get(x => x.CategoryId == category.Id);

        //    return View(products);
        //}



    }
}
