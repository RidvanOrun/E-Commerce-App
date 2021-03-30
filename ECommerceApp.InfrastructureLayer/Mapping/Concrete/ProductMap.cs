using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.InfrastructureLayer.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.InfrastructureLayer.Mapping.Concrete
{
    public class ProductMap:BaseMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
        }
    }
}
