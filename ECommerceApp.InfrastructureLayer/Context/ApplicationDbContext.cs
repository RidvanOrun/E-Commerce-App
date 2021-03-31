using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.InfrastructureLayer.Mapping.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.InfrastructureLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int> // => AppUser Identity'den besleneceği için ekstra bir daha DbSet yapmaya gerek yok.
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } // Asp.Net.Core Db bağlantısı için oluşturulmuştur.

        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUserToProduct> AppUserToProducts { get; set; }       



        protected override void OnModelCreating(ModelBuilder builder) // => Mapping olarak oluşturduğumuz kuralları burada override ederek Db'ye gönderiyoruz.
        {
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new AppRoleMap());
            builder.ApplyConfiguration(new AppUserToProductMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new ProductMap());           

            base.OnModelCreating(builder);
        }

        // Uygulamada lazy loading açmak için kullanılır.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
