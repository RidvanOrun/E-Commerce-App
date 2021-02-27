using ECommerceApp.DomainLayer.Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.DomainLayer.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IAppUserRepository AppUserRepository { get; }


        Task Commit(); //Başarılı bir işlemin sonucunda çalıştırılır. İşlemin başlamasından itibaren tüm değişikliklerin veri tabanına uygulanmasını temin eder.

        Task ExecuteSqlRaw(string sql, params object[] parameters); // Mevcut sql sorgularımızı doğrudan veri tabanında yürütmek için kullanılan bir method.
    }
}
