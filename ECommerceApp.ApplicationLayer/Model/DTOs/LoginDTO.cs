using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public AppRole AppRole { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
