using ECommerceApp.DomainLayer.Entities.Interface;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.DomainLayer.Repository.BaseRepository
{
    // Repository: Temel olarak veritabanı sorgulama işlemlerinin bir merkezden yapılmasını sağlayarak iş katmamına bu işlererin taşınmasını önler bu şekilde sorgu ve kod tekrarını engelleriz
    public interface IBaseRepository<T> where T: IBaseEntity
    {       
        Task<List<T>> Get(Expression<Func<T, bool>> expression);
        Task<T> GetById(int id);      
        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                                         Expression<Func<T, bool>> expression = null,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> inculude = null,
                                                         bool disableTracking = true);

        Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
                                                     Expression<Func<T, bool>> expression = null,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                     Func<IQueryable<T>, IIncludableQueryable<T, object>> inculude = null,
                                                     bool disableTracking = true);
    }
}
