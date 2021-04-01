using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class RoleEditDTO
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> HasRole { get; set; }
        public IEnumerable<AppUser> HasNotRole { get; set; }
        public string RoleName { get; set; }

        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }

    }
}
