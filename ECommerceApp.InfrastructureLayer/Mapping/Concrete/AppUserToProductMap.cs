using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.InfrastructureLayer.Mapping.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.InfrastructureLayer.Mapping.Concrete
{
    public class AppUserToProductMap:BaseMap<AppUserToProduct>
    {
        public override void Configure(EntityTypeBuilder<AppUserToProduct> builder)
        {
            builder.HasOne(x => x.Product)
                .WithMany(x => x.AppUserToProducts)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.AppUserToProducts)
                .HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
