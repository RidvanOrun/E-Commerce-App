using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.InfrastructureLayer.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.InfrastructureLayer.Mapping.Concrete
{
    public class CategoryMap:BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
        }
    }
}
