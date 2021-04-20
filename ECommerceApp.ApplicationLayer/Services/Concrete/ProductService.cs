using AutoMapper;
using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.ApplicationLayer.Services.Interface;
using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.DomainLayer.Enums;
using ECommerceApp.DomainLayer.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.ApplicationLayer.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IWebHostEnvironment webHostEnvironment)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task Create(ProductDTO productDTO)
        {
            if (productDTO != null)
            {
                string imageName = "noimage.png";
                if (productDTO.Image != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/product");
                    string newName = productDTO.Description.Trim().Replace(" ", string.Empty).Substring(0, 7);
                    imageName = newName + "_" + productDTO.Image.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await productDTO.Image.CopyToAsync(fileStream);
                    fileStream.Close();
                }
                productDTO.ImagePath = imageName;
                Product product = _mapper.Map<ProductDTO, Product>(productDTO);
                await _unitOfWork.ProductRepository.Add(product);
                await _unitOfWork.Commit();
            }
        }

        public async Task<List<Product>> GetAll()
        {
            List<Product> products = await _unitOfWork.ProductRepository.Get(x => x.Status != Status.Passive);
            return products;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);

            return _mapper.Map<ProductDTO>(product);
        }     

        public async Task<List<Product>> GetList(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);

            List<Product> products = await _unitOfWork.ProductRepository.Get(x => x.CategoryId == category.Id);

            return products;
        }      

        public async Task<List<Product>> GetOrderByList()
        {
            var productList = await _unitOfWork.ProductRepository.GetFilteredList(
                selector: x => new Product
                {
                    ProductName = x.ProductName,
                    Description = x.Description,
                    DescText = x.DescText,
                    Image = x.Image,
                    ImagePath = x.ImagePath,
                    UnitPrice = x.UnitPrice
                },
                orderby: x => x.OrderByDescending(x => x.CreateDate)
                );
            return productList;
        }

        public async Task<List<Category>> GetCategory()
        {
            var categoryList = await _unitOfWork.CategoryRepository.Get(x => x.Status != Status.Passive);
            return categoryList;
        }

        public async Task Update(ProductDTO productDTO)
        {
            var products = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.Id == productDTO.Id);
        
            if (productDTO != null)
            {
                if (productDTO.Image != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/product");
                    if (!string.Equals(products.Image, "noimage.png"))
                    {
                        string oldPath = Path.Combine(uploadDir, products.ImagePath);
                        if (File.Exists(oldPath))
                        {
                            File.Delete(oldPath);
                        }
                    }

                    string imageName = productDTO.ProductName + "_" + productDTO.Image.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await productDTO.Image.CopyToAsync(fileStream);
                    fileStream.Close();
                    products.ImagePath = imageName;
                }
                if (productDTO.Description != null)
                {
                    products.Description = productDTO.Description;
                }
               
                _unitOfWork.ProductRepository.Update(products);
                await _unitOfWork.Commit();

            }
            
        }
     
    }
}
