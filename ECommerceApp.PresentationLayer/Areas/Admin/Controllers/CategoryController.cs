using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.CategoryList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Create(categoryDTO);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id) => View(await _categoryService.GetById(id));


        [HttpPost]
        public async Task<IActionResult> Update(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryService.GetCategoryName(model);
                if (category != null)
                {
                    await _categoryService.Update(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is already a category..!");
                    TempData["Warning"] = "The page  is already exsist..!";
                    return View(category);
                }
            }
            else
            {
                TempData["Error"] = "The category hasn't been edited..!";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetById(id);

            await _categoryService.Delete(category);
            return View("Index");

        }


    }
}
