using AutoMapper;
using ECommerceApp.ApplicationLayer.Model.DTOs;

using ECommerceApp.ApplicationLayer.Services.Interface;
using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.DomainLayer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using System;
using System.Threading.Tasks;

namespace ECommerceApp.ApplicationLayer.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AppUserService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              SignInManager<AppUser> signInManager,
                              UserManager<AppUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public async Task DeleteUser(int id)
        {
            AppUser user = await _unitOfWork.AppUserRepository.GetById(id);

            _unitOfWork.AppUserRepository.Delete(user);
           
        }

        public async Task EditUser(EditProfileDTO editProfileDTO)
        {
            var user = await _unitOfWork.AppUserRepository.GetById(editProfileDTO.Id);
            if (user != null)
            {
                if (editProfileDTO.Image != null)
                {
                    using var image = Image.Load(editProfileDTO.Image.OpenReadStream());
                    //image.Mutate(x => x.Resize(256, 256));
                    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    user.ImagePath = ("/images/users/" + user.UserName + ".jpg");
                    _unitOfWork.AppUserRepository.Update(user);
                    await _unitOfWork.Commit();
                }
                if (editProfileDTO.UserName != user.UserName)
                {
                    var newUserName = await _userManager.FindByNameAsync(editProfileDTO.UserName);

                    if (newUserName == null)
                    {
                        await _userManager.SetUserNameAsync(user, editProfileDTO.UserName);
                        //user.UserName = editProfileDTO.UserName;
                        await _signInManager.SignInAsync(user, isPersistent: true);
                    }
                }
                if (editProfileDTO.FullName != user.FullName)
                {
                    user.FullName = editProfileDTO.FullName;
                }
                if (editProfileDTO.Email != user.Email)
                {
                    var isnewEmail = await _userManager.FindByEmailAsync(editProfileDTO.Email);
                    if (isnewEmail == null)
                    {
                        await _userManager.SetEmailAsync(user, editProfileDTO.Email);
                    }
                }
                if (editProfileDTO.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, editProfileDTO.Password);
                }

                _unitOfWork.AppUserRepository.Update(user);
                await _unitOfWork.Commit();
            }
        }

        public async Task<EditProfileDTO> GetById(int id)
        {
            var user = await _unitOfWork.AppUserRepository.GetById(id);

            return _mapper.Map<EditProfileDTO>(user);
        }
        public async Task<LoginDTO> GetLoginById(int id)
        {
            var user = await _unitOfWork.AppUserRepository.GetById(id);

            return _mapper.Map<LoginDTO>(user);
        }

        public async Task<ProfileDTO> GetByUserName(string userName)
        {
            var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(
                selector: x => new ProfileDTO {
                    ImagePath = x.ImagePath,
                    UserName = x.UserName
                },
                expression: y=>y.UserName==userName                
                );


            return user;
        }

        public async Task<int> GetUserIdFromName(string userName)
        {
            var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(
                selector: x => x.Id,
                expression: x => x.UserName == userName);

            return Convert.ToInt32(user); // NOT => Burada int tipinde id dönmesi gerekiyor kontrol
        }

        public async Task<SignInResult> LogIn(LoginDTO loginDTO)
        {
            var user = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, loginDTO.RememberMe, false);

       
            return user;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
            var user = _mapper.Map<AppUser>(registerDTO);

            var result = await _userManager.CreateAsync(user, registerDTO.ConfirmPassword);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false); //isPersistent => Tarayıcı kullanıcı giriş bilgilerini hafıza da tutmsun mu diye sorar.
            }
            return result;
        }

    }
}
