using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.ApplicationLayer.Services.Interface
{
    public interface ICategoryService
    {
        Task Create(CategoryDTO categoryDTO);
        Task Delete(CategoryDTO categoryDTO);
        Task Update(CategoryDTO categoryDTO);

        Task<CategoryDTO> GetById(int id);
        Task<CategoryDTO> GetCategoryName(CategoryDTO categoryDTO);
      
        Task<List<Category>> CategoryList();
    }
}
