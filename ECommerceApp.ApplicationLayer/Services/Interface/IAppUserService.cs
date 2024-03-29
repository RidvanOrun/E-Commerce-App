﻿using ECommerceApp.ApplicationLayer.Model.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.ApplicationLayer.Services.Interface
{
    public interface IAppUserService
    {
        Task DeleteUser(int id);
        Task EditUser(EditProfileDTO editProfileDTO);

        Task<IdentityResult> Register(RegisterDTO registerDto);
        Task<SignInResult> LogIn(LoginDTO loginDTO);
        Task LogOut();

        Task<int> GetUserIdFromName(string userName);

        Task<EditProfileDTO> GetById(int id);
        Task<LoginDTO> GetLoginById(int id);
        Task<ProfileDTO> GetByUserName(string userName);
    }
}
