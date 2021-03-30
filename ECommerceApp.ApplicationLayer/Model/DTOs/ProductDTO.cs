using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; } = "/images/product/default.jpg";
        public IFormFile Image { get; set; }
        public IFormFile ImageTwo { get; set; }
        public IFormFile ImageTheree { get; set; }
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }

    }
}
