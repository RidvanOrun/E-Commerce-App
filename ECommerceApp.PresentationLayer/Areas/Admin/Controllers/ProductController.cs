using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Areas.Admin.Controllers
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

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAll());
        }

        public IActionResult Create() ///?????
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                if (productDTO != null)
                {
                    await _productService.Create(productDTO);
                    return RedirectToAction("Index");
                }
                else return View(productDTO);
            }
            else return View(productDTO);
        }

        public async Task<IActionResult> Update(int id) ////?????
        {
            var product = await _productService.GetById(id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(productDTO);
                TempData["Success"] = "The product edited..!";
                return RedirectToAction("Index");
            }
            else
                TempData["Error"] = "The product hasn't been edited..!";
            return View(productDTO);
        }

        public IActionResult Details(int id)
        {
            ViewBag.productId = id;
            return View();
        }

    }
}
