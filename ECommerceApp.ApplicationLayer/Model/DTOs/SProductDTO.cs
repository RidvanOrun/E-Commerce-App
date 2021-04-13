using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class SProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public Dictionary<int, string> AppUsers { get; set; }

        public void Load(Product product)
        {
            Id = product.Id;
            ProductName = product.ProductName;
            UnitPrice= product.UnitPrice;
            AppUsers = new Dictionary<int, string>();

            foreach (AppUserToProduct ap in product.AppUserToProducts) AppUsers.Add(ap.AppUserId, ap.AppUser.UserName);
        }
    
    }
}
