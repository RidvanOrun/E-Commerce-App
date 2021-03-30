using AutoMapper;
using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.DomainLayer.Enums;
using ECommerceApp.DomainLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.ApplicationLayer.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
       
  

        public CategoryService(IUnitOfWork unitOfWork,
                              IMapper mapper
                              )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            
        }

        public async Task<List<Category>> CategoryList()
        {
            List<Category> categories = await _unitOfWork.CategoryRepository.GetFilteredList(
               selector: x => new Category
               {
                   Id = x.Id,
                   CategoryName = x.CategoryName,

               },
               expression: y => y.Status != DomainLayer.Enums.Status.Passive);

            //List<Category> categories = await _unitOfWork.CategoryRepository.Get(x => x.Status != Status.Passive);


            return categories;
        }

        public async Task Create(CategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.FirstOrDefault(x => x.CategoryName == categoryDTO.CategoryName); // eğer böyle bir category yoksa ekle

            if (category==null)
            {
                var newCategory = _mapper.Map<CategoryDTO, Category>(categoryDTO);
                await _unitOfWork.CategoryRepository.Add(newCategory);
                await _unitOfWork.Commit();
            }
            
        }

        public async Task Delete(CategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(categoryDTO.Id);
            if (category != null)
            {
                _unitOfWork.CategoryRepository.Delete(category);
                await _unitOfWork.Commit();
            }

        }

        public Task<Category> GetActiveToCategory(int id)
        {
            //var category = await _unitOfWork.CategoryRepository.GetById(id);

            //if (category != null)
            //{
            //    category.Status = Status.Modified;
            //    await _unitOfWork.Commit();
            //}
            //return category; -----------------> ihtiyaç olursa açıcam...

            throw new NotImplementedException();

        }

        public Task<List<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> GetCategoryName(CategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefault(
               selector: y => new CategoryDTO
               {
                   CategoryName = categoryDTO.CategoryName
               },
               expression: x => x.Id == categoryDTO.Id
               );
            return category;
        }

        public async Task<List<Category>> GetToPassive()
        {
            List<Category> categories = await _unitOfWork.CategoryRepository.Get(x => x.Status != Status.Passive);

            return categories;
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.FirstOrDefault(x => x.CategoryName == categoryDTO.CategoryName);

            if (category!=null)
            {
                category.CategoryName = categoryDTO.CategoryName;
            }

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Commit();
        }
    }
}
