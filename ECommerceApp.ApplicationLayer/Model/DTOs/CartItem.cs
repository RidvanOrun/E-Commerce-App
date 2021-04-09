using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get => Quantity * Price; }
        public string Image { get; set; }

        public CartItem() { }

        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.ProductName;
            Price = product.UnitPrice;
            Quantity = 1;
            Image = product.ImagePath;
        }
    }
}
