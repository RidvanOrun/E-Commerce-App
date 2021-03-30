using AutoMapper;
using ECommerceApp.ApplicationLayer.Model.DTOs;

using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Mapper
{
    /// <summary>
    /// Bu sınıf bizim için Entity ve dto sınıfları arasında mapping tablosu görevi görmektedir. 
    /// Auto mapper buradaki kuralları kontrol ederek veri transferi işlemlerini otomatik yapacak.
    /// </summary>
    public class Mapping : Profile
    {
        //CreateMap metodu ile entity ve dtoları eşleştirip mapliyoruz
        //ReversMap komutu ise bu mappingin iki yönlü olduğunu bildiriyor.

        public Mapping()
        {
            /// <summary>
            /// Bu yapıcı method içerisinde kurallarımızı tanımlıyoruz.
            /// </summary>
            CreateMap<AppUser, EditProfileDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, ProfileDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }

    //Hedef ve kaynak arasında property isimleri farklı ise aşağıdaki gibi eşleştirme yapılır
    //.ForMember(a => a.Name, b => b.MapFrom(c => c.ModelName))

    // Bir propertynin görmezden gelinmesi isteniyor ise aşağıdaki gibi bir tanımlama yapılmalıdır.
    //.ForSourceMember(x => x.Name, opt => opt.Ignore())
}
