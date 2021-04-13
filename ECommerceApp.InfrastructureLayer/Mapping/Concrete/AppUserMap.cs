using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.InfrastructureLayer.Mapping.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.InfrastructureLayer.Mapping.Concrete
{
    public class AppUserMap : BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id); //=> AppUser ıd'yi primary key olarak belirledik.

            builder.Property(x => x.FullName).IsRequired(true).HasMaxLength(50); // => FullName boş geçilmesin ve maximum karakteri "50" olarak belirledik.
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(40);
            builder.Property(x => x.Email).IsRequired(true);

            builder.HasMany(x => x.AppUserToProducts)
                .WithOne(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.AppRole)
            //     .WithMany(x => x.AppUsers)
            //     .HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);


            base.Configure(builder);
        }
    }
}
