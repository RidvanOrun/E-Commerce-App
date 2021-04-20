

1. "ECommerceApp" ismiyle Blank Solution a��l�r.
2. "ECommerceApp.DomainLayer" ismiyle Class Library (.Core) projesi eklenir.
	2.1. "Enums" dosyas� a��l�r.
		2.1.1. "Status" Class � eklenir. 

	2.2. "Entities" dosyas� a��l�r.
		2.2.1. Interface dosyas� a��l�r.
			2.2.1.1. "IBaseEntity" Class� olu�turulur.
		2.2.2. Concrete dosyas� a��l�r.
			2.2.2.1. AppUser.cs
				"IdentityUser<int>, IBaseEntity" class lar�ndan kal�t�m al�n�r.
			2.2.2.2. Product.cs
				"BaseEntity<int>" clas�ndan kal�t�m al�n�r.
			2.2.2.3. AppUserToProduct.cs
				"BaseEntity<int>" clas�ndan kal�t�m al�n�r.
			2.2.2.4. AppRole.cs
				"IdentityRole<int>, IBaseEntity" class lar�ndan kal�t�m al�n�r
			2.2.2.5. Category.cs
				"BaseEntity<int>" clas�ndan kal�t�m al�n�r.
			2.2.2.6. BaseEntity.cs olu�turulur.
				"IBaseEntity" class �ndan kal�t�m al�n�r. 

	2.3. "Repository" dosyas� olu�turulur.
		2.3.1. "BaseRepository" klas�r� a��l�r.
			2.3.1.1. "IBaseRepository" interface i olu�turulur ve repositoryler yaz�lmaya ba�lan�r.
				
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

		2.3.1. "EntityRepository" klas�r� a��l�r.
			2.3.1.1. "IAppUserRepository" interface i a��l�r.
				"IBaseRepository" interface inden "AppUser" tipinde kal�t�m al�n�r.
			2.3.1.2. "ICategoryRepository" interface i a��l�r.
				"IBaseRepository" interface inden "Category" tipinde kal�t�m al�n�r.
			2.3.1.3. "ICategoryRepository" interface i a��l�r.
				"IBaseRepository" interface inden "Category" tipinde kal�t�m al�n�r.

	2.4. "UnitOfWork" dosyas� olu�turulur.
		2.4.1. "IUnitOfWork" Dosyas� Olu�turulur.
		
			public interface IUnitOfWork:IAsyncDisposable
				{
					IAppUserRepository AppUserRepository { get; }

					ICategoryRepository CategoryRepository { get; }

					IProductRepository ProductRepository { get; }

					Task Commit(); //Ba�ar�l� bir i�lemin sonucunda �al��t�r�l�r. ��lemin ba�lamas�ndan itibaren t�m de�i�ikliklerin veri taban�na uygulanmas�n� temin eder.

					Task ExecuteSqlRaw(string sql, params object[] parameters); // Mevcut sql sorgular�m�z� do�rudan veri taban�nda y�r�tmek i�in kullan�lan bir method.
				}


