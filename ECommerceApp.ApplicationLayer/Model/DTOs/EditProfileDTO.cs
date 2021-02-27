using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class EditProfileDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped] // => Eşlenmemiş 
        public IFormFile Image { get; set; }
    }
}
