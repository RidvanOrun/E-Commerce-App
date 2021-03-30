using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class ProfileDTO
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public int ProductCount { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
    }
}
