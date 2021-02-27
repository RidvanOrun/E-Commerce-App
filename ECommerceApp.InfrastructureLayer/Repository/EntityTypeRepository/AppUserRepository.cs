using ECommerceApp.DomainLayer.Entities.Concrete;
using ECommerceApp.DomainLayer.Repository.EntityRepository;
using ECommerceApp.InfrastructureLayer.Context;
using ECommerceApp.InfrastructureLayer.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.InfrastructureLayer.Repository.EntityTypeRepository
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository// => BaseRepository<AppUser> tipinde kalıtım aldık. Daha sonra inject edeceğimiz IAppUserRepository tanımladık. Bunu yapmamızın amacı DIP prensibine uymamız. Sınıfları olabildiğince birbirinden bağımsız hale getirmek.
    {
        public AppUserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { } // => Database bağlantısı yapıldı.
    }
}
}
