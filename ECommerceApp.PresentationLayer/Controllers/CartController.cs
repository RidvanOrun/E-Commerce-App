
using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.InfrastructureLayer.Context;
using ECommerceApp.PresentationLayer.Models.Extensions;
using ECommerceApp.PresentationLayer.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CartController(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public IActionResult Index()
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartViewModel = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };

            return View(cartViewModel);
        }

        public async Task<IActionResult> Add(int id)
        {
            Product product = await _applicationDbContext.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();//çart bilgilerini session'da tutma karararı almıştık. Mantıksız bir hareket değildir. Çoğu e-ticaret sitesi bu yaklaşımı kullanmaktadır (günümüz için check edin). Bunun için uygulam içerisinde bulunan varlıkları "json" dönüştürmmemiz gerekmekteydi bunun için extension method yazdık. burada bu eztension methodun Deserialize Object kısmını kullandık. Neden? Çünkü Session üzerinden alıyorum cart'ın bilgilerini. 

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
                 // sepete eklenmek istenilen ürün yok ise yani ürün sepete ilk kez eklenecekse
            }
            else
            {
                cartItem.Quantity += 1;//sepette var ise increase ediyoruz
                
            }

            HttpContext.Session.SetJson("Cart", cart);

            if (HttpContext.Request.Headers["X-Request-With"] != "XMLHttpRequest")
            {
                return RedirectToAction("Index", "Product");//"actionName, controllerName"
            }

            return RedirectToAction("Index"/*, "Product"*/);
        }

        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
                --cartItem.Quantity;
            else
                cart.RemoveAll(x => x.ProductId == id);


            if (cart.Count == 0)
                HttpContext.Session.Remove("Cart");
            else
                HttpContext.Session.SetJson("Cart", cart);


            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.ProductId == id);

            if (cart.Count == 0)
                HttpContext.Session.Remove("Cart");
            else
                HttpContext.Session.SetJson("Cart", cart);

            return RedirectToAction("Index");
        }


        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Product");//"actionName, controllerName"
        }

        public IActionResult Payment()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Product");//"actionName, controllerName"
        }

        public IActionResult PaymentView()
        {
            HttpContext.Session.Clear();
            return View();
        }
    }
}
