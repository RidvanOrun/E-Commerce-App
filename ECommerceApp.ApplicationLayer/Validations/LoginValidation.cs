using ECommerceApp.ApplicationLayer.Model.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Validations
{
    public class LoginValidation:AbstractValidator<LoginDTO>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Boş Bırakılmaz!");
            RuleFor(x => x.Password).MinimumLength(3).NotEmpty().WithMessage("Geçerli bir şifre giriniz...");
        }
    }
}
