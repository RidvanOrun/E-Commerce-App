using ECommerceApp.DomainLayer.Entities.Interface;
using ECommerceApp.DomainLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.DomainLayer.Entities.Concrete
{
    //IdentityUser => Microsoft'un bize hazrıladığı bir sınıf, user ile ilgli işlmelerde hızlı kullanabilmemiz için bize bir çok yapı sağlayan bir sınıf. User Role, login registrition işlemlerinde hazır yapılar sunmaktadır. Bu sınıfın kendi hazır tabloların "Id" sütunu barındırdığından, alışık olduğunuz gibi IBaseEntity.cs arayüzünden varlıklarımıza Id basmadık. 
    public class AppUser:IdentityUser<int>, IBaseEntity
    {
        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }

        public string FullName { get; set; }
        public string ImagePath { get; set; } = "/images/users/default.jpg";

    }
}
