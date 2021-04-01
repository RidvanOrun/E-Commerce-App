using ECommerceApp.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }

       
    }
}
