using AutoMapper;
using ECommerceApp.ApplicationLayer.Mapper;
using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.InfrastructureLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddHttpClient(); //buna bak
            services.AddMemoryCache(); //buda
            services.AddSession(); //xx

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Mapping));

            services.AddIdentity<AppUser, AppRole>(
                x =>
                {
                    x.SignIn.RequireConfirmedAccount = false;
                    x.SignIn.RequireConfirmedEmail = false;
                    x.SignIn.RequireConfirmedPhoneNumber = false;
                    x.User.RequireUniqueEmail = false;
                    x.Password.RequiredLength = 3; // => password e girilen karakterin minimum 3 olmasýný saðladýk. Varsayýlan deðer 6 dýr.
                    x.Password.RequiredUniqueChars = 0;
                    x.Password.RequireLowercase = false; // =>özelliði; þifre içerisinde en az 1 adet küçük harf zorunluluðu olmasý özelliðini false yaptýk.
                    x.Password.RequireUppercase = false; // => özelliði; þifre içerisinde en az 1 adet büyük harf zorunluluðu olmasýný false yaptýk.
                    x.Password.RequireNonAlphanumeric = false; // =>  özelliði; þifre içerisinde en az 1 adet alfanümerik karakter zorunluluðu olmasý özelliði false.
                })
                .AddEntityFrameworkStores<ApplicationDbContext>() // => AddEntityFrameworkStores<ApplicationDbContext>() metodu da; dahil ettiðimiz Identity ara katmanýndaki kullanýcý bilgilerini yönetirken hangi DbContext sýnýfýnýn kullanýlmasý gerektiðini belirtmektedir
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication(); //  metodu; web uygulamamýzýn, eklediðimiz Identity ara katmanýný yetkilendirme için kullanmasýný saðlamaktadýr.


            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
              );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
