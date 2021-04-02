﻿using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.ApplicationLayer.Services.Interface
{
    public interface IProductService
    {
        Task Create(ProductDTO productDTO);
        Task Update(ProductDTO productDTO);
        Task Delete(ProductDTO productDTO);

        Task<ProductDTO> GetById(int id);
        Task<ProductDTO> GetByName(string productName);

        Task<List<Product>> GetAll();

        Task<List<Product>> GetOrderByList();

        Task<List<Category>> GetCategory(); 
        
        //????????????? List AppuserToProduct Aynısından appuserdada olması gerekiyor mu aceeebaaaa
        Task<List<Product>> GetList(int id);

        //Task<List<Product>> ProductByCategory(CategoryDTO categoryDTO);

    }
}
