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
                    x.Password.RequiredLength = 3; // => password e girilen karakterin minimum 3 olmas�n� sa�lad�k. Varsay�lan de�er 6 d�r.
                    x.Password.RequiredUniqueChars = 0;
                    x.Password.RequireLowercase = false; // =>�zelli�i; �ifre i�erisinde en az 1 adet k���k harf zorunlulu�u olmas� �zelli�ini false yapt�k.
                    x.Password.RequireUppercase = false; // => �zelli�i; �ifre i�erisinde en az 1 adet b�y�k harf zorunlulu�u olmas�n� false yapt�k.
                    x.Password.RequireNonAlphanumeric = false; // =>  �zelli�i; �ifre i�erisinde en az 1 adet alfan�merik karakter zorunlulu�u olmas� �zelli�i false.
                })
                .AddEntityFrameworkStores<ApplicationDbContext>() // => AddEntityFrameworkStores<ApplicationDbContext>() metodu da; dahil etti�imiz Identity ara katman�ndaki kullan�c� bilgilerini y�netirken hangi DbContext s�n�f�n�n kullan�lmas� gerekti�ini belirtmektedir
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

            app.UseAuthentication(); //  metodu; web uygulamam�z�n, ekledi�imiz Identity ara katman�n� yetkilendirme i�in kullanmas�n� sa�lamaktad�r.


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
