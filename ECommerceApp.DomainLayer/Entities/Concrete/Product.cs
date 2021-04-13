using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceApp.DomainLayer.Entities.Concrete
{
    public class Product:BaseEntity<int>
    {
        public Product()
        {
            AppUserToProducts = new List<AppUserToProduct>();
        }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string DescText { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; } = "/images/product/default.jpg";

        [NotMapped]
        public IFormFile Image { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; } // filtreleme için string  tutuldu
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual List<AppUserToProduct> AppUserToProducts { get; set; }
    }
}
