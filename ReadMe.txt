

1. "ECommerceApp" ismiyle Blank Solution açýlýr.
2. "ECommerceApp.DomainLayer" ismiyle Class Library (.Core) projesi eklenir.
	2.1. "Enums" dosyasý açýlýr.
		2.1.1. "Status" Class ý eklenir. 

	2.2. "Entities" dosyasý açýlýr.
		2.2.1. Interface dosyasý açýlýr.
			2.2.1.1. "IBaseEntity" Classý oluþturulur.
		2.2.2. Concrete dosyasý açýlýr.
			2.2.2.1. AppUser.cs
				"IdentityUser<int>, IBaseEntity" class larýndan kalýtým alýnýr.
			2.2.2.2. Product.cs
				"BaseEntity<int>" clasýndan kalýtým alýnýr.
			2.2.2.3. AppUserToProduct.cs
				"BaseEntity<int>" clasýndan kalýtým alýnýr.
			2.2.2.4. AppRole.cs
				"IdentityRole<int>, IBaseEntity" class larýndan kalýtým alýnýr
			2.2.2.5. Category.cs
				"BaseEntity<int>" clasýndan kalýtým alýnýr.
			2.2.2.6. BaseEntity.cs oluþturulur.
				"IBaseEntity" class ýndan kalýtým alýnýr. 

	2.3. "Repository" dosyasý oluþturulur.
		2.3.1. "BaseRepository" klasörü açýlýr.
			2.3.1.1. "IBaseRepository" interface i oluþturulur ve repositoryler yazýlmaya baþlanýr.
				
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

		2.3.1. "EntityRepository" klasörü açýlýr.
			2.3.1.1. "IAppUserRepository" interface i açýlýr.
				"IBaseRepository" interface inden "AppUser" tipinde kalýtým alýnýr.
			2.3.1.2. "ICategoryRepository" interface i açýlýr.
				"IBaseRepository" interface inden "Category" tipinde kalýtým alýnýr.
			2.3.1.3. "ICategoryRepository" interface i açýlýr.
				"IBaseRepository" interface inden "Category" tipinde kalýtým alýnýr.

	2.4. "UnitOfWork" dosyasý oluþturulur.
		2.4.1. "IUnitOfWork" Dosyasý Oluþturulur.
		
			public interface IUnitOfWork:IAsyncDisposable
				{
					IAppUserRepository AppUserRepository { get; }

					ICategoryRepository CategoryRepository { get; }

					IProductRepository ProductRepository { get; }

					Task Commit(); //Baþarýlý bir iþlemin sonucunda çalýþtýrýlýr. Ýþlemin baþlamasýndan itibaren tüm deðiþikliklerin veri tabanýna uygulanmasýný temin eder.

					Task ExecuteSqlRaw(string sql, params object[] parameters); // Mevcut sql sorgularýmýzý doðrudan veri tabanýnda yürütmek için kullanýlan bir method.
				}


