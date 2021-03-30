using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.DomainLayer.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.DomainLayer.Repository.EntityRepository
{
    public interface IProductRepository:IBaseRepository<Product>
    {
    }
}
