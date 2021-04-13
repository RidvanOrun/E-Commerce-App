using ECommerceApp.DomainLayer.Entities.Interface;
using ECommerceApp.DomainLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.DomainLayer.Entities.Concrete
{
    public class AppRole : IdentityRole<int>, IBaseEntity
    {
        public AppRole(string name)
        {
            Name = name.ToString();
        }

        //public virtual List<AppUser> AppUsers { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }


    }
}
