using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.DomainLayer.Repository.EntityRepository;
using ECommerceApp.InfrastructureLayer.Context;
using ECommerceApp.InfrastructureLayer.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.InfrastructureLayer.Repository.EntityTypeRepository
{
    public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    }
}
