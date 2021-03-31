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
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; } /*= "/images/product/default.jpg";*/

        [NotMapped] // => NotMapped attribute’ü uygulanarak belli bir alanın tablo’da oluşturulması engellenir.
        public IFormFile Image { get; set; }
        [NotMapped]
        public IFormFile ImageTwo { get; set; }
        [NotMapped]
        public IFormFile ImageTheree { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual List<AppUserToProduct> AppUserToProducts { get; set; }
    }
}
