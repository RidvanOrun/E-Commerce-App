using ECommerceApp.DomainLayer.Entities.Interface;
using ECommerceApp.DomainLayer.Enums;
using ECommerceApp.DomainLayer.Repository.BaseRepository;
using ECommerceApp.InfrastructureLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.InfrastructureLayer.Repository.BaseRepository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> _table; 

        public BaseRepository(ApplicationDbContext context)
        {
            this._context = context; // => dışarıdan gelen context bağlantısını _context ile eşitledik.
            this._table = _context.Set<T>(); // Tablolarımızı her defasında yazmamak için bu consructor method ile _context ile tanımlandı.
        }
        public async Task Add(T entity) => await _table.AddAsync(entity);// => Gelen Entity AddAsync methoduyla ekledik.

        public async Task<bool> Any(Expression<Func<T, bool>> expression) => await _table.AnyAsync(expression); // => geri dönüş tipi bool olarak belirledik ve yazılan expression a göre True False dönecek

        public void Delete(T entity)
        {
            entity.Status = Status.Passive; // => Status passsive haline getirdik.
            entity.DeleteDate = DateTime.Now; // passsive alınma tarihini o anlık tarih yaptık.
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression) => await _table.Where(expression).FirstOrDefaultAsync();

        public async Task<List<T>> Get(Expression<Func<T, bool>> expression) => await _table.Where(expression).ToListAsync();
        public async Task<T> GetById(int id) => await _table.FindAsync(id);
        public async Task<List<T>> GettAll() => await _table.ToListAsync();
        public async Task<T> GetByUserName(string userName) => await _table.FindAsync(userName);

        public void Update(T entity)
        {
            //entity.ModifiedDate = DateTime.Now;
            _context.Entry<T>(entity).State = EntityState.Modified; // => Entry .NetCore ile gelen hazır bir methoddur. 
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,// => ilk paramatre Entity tipince olacak ikinci aldığı parametre ise dönüş tipide TResult olacak.
                                                                Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, // ilk parametredeki verinin bool tipinde dönmesini sağlayacaktır.
                                                                IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, // öğeleri bir düzene göre sıralar.
                                                                IIncludableQueryable<T, object>> inculude = null,// Tanımlanan öğeyi içerip içermediğini kontrol eder.
                                                                bool disableTracking = true)
        {
            IQueryable<T> query = _table; //=> Sorgu geldikçe Db den ye gidip gelecek.

            if (disableTracking) query = query.AsNoTracking(); // => disableTracking varlık üzerinde ki değişiklikleri kontrol edip Save'e gönderiyoru. Biz filtreleme yaptığımızdan filtreleyip gönderiyoruz. Burada disableTracking'e gerek olmadığı için kapattık.
            if (inculude != null) query = inculude(query); // => include edilen nesneleri query'e attık.
            if (expression != null) query = query.Where(expression); // => expression ile gelenleri linq to sorgusu yazılması için Where sorgusunu query'e attık.
            if (orderby != null) return await orderby(query).Select(selector).FirstOrDefaultAsync(); // => Gelen orderby sorgusu dolu ise bu şart çalışacak.
            else return await query.Select(selector).FirstOrDefaultAsync(); // => şayet Null geliyorsa da bu satır çalışsın.
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
                                                            Expression<Func<T, bool>> expression = null,
                                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                            Func<IQueryable<T>, IIncludableQueryable<T, object>> inculude = null,
                                                            bool disableTracking = true)
        {
            IQueryable<T> query = _table;
            if (disableTracking) query = query.AsNoTracking();
            if (inculude != null) query = inculude(query);
            if (expression != null) query = query.Where(expression);
            if (orderby != null) return await orderby(query).Select(selector).ToListAsync();
            else return await query.Select(selector).ToListAsync();
        }

    }
}
