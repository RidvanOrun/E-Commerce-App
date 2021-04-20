
1. "ECommerceApp" ismiyle Blank Solution a��l�r.
2. "ECommerceApp.DomainLayer" ismiyle Class Library (.Core) projesi eklenir.
	
	Y�klenecek Paketler :	-Microsoft.AspNetCore.Http.Features(5.0.4)
							-Microsoft.EntityFrameworkCore(5.0.4)
							-Microsoft.Extensions.Identity.Stores(5.0.4)
							-Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)

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

		2.3.2. "EntityRepository" klas�r� a��l�r.
			2.3.2.1. "IAppUserRepository" interface i a��l�r.
				"IBaseRepository" interface inden "AppUser" tipinde kal�t�m al�n�r.
			2.3.2.2. "ICategoryRepository" interface i a��l�r.
				"IBaseRepository" interface inden "Category" tipinde kal�t�m al�n�r.
			2.3.2.3. "IProductRepository" interface i a��l�r.
				"IBaseRepository" interface inden "Product" tipinde kal�t�m al�n�r.

	2.4. "UnitOfWork" dosyas� olu�turulur.
		2.4.1. "IUnitOfWork" Dosyas� Olu�turulur.
		
			public interface IUnitOfWork:IAsyncDisposable
				{
					IAppUserRepository AppUserRepository { get; }

					ICategoryRepository CategoryRepository { get; }

					IProductRepository ProductRepository { get; }

					Task Commit();

					Task ExecuteSqlRaw(string sql, params object[] parameters);
				}

3. "ECommerceApp.InfastructureLayer" ismiyle Class Library (.Core) projesi eklenir.
	
	Y�klenecek Paketler :	-Microsoft.AspNetCore.Identity.EntityFrameworkCore(5.0.4)
							-Microsoft.EntityFrameworkCore(5.0.4)
							-Microsoft.EntityFrameworkCore.SqlServer(5.0.4)
							-Microsoft.EntityFrameworkCore.Tools(5.0.4)

	Referans Proje		:	-"ECommerceApp.DomainLayer"

	3.1. "Mapping" klas�r a��l�r. 
		3.1.1. "Abstract" klas�r� a��l�r.
			3.1.1.1. "BaseMap" class � a��l�r. 

				public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
					{
						public virtual void Configure(EntityTypeBuilder<T> builder)
						{
							builder.Property(x => x.CreateDate).IsRequired(true);
							builder.Property(x => x.ModifiedDate).IsRequired(false);
							builder.Property(x => x.DeleteDate).IsRequired(false);
							builder.Property(x => x.Status).IsRequired(true);
						}
					}

		3.1.2. "Concrete" klas�r� a��l�r.
			3.1.2.1. "AppRole" class � a��l�r.
				"BaseMap" abstract class �ndan "AppRole" tipinde kal�t�m al�n�r.
			3.1.2.2. "AppUserMap" class � a��l�r.
				"BaseMap" abstract class �ndan "AppUser" tipinde kal�t�m al�n�r.
			3.1.2.3. "AppUserToProductMap" class � a��l�r.
				"BaseMap" abstract class �ndan "AppUserToProduct" tipinde kal�t�m al�n�r.				
			3.1.2.4. "ProductMap" class � a��l�r.
				"BaseMap" abstract class �ndan "Product" tipinde kal�t�m al�n�r.
			3.1.2.5. "CategoryMap" class � a��l�r.
				"BaseMap" abstract class �ndan "Category" tipinde kal�t�m al�n�r.
	3.2. "Repository" klas�r� a��l�r.
		3.2.1. "BaseRepository" folder� a��l�r
			3.2.1.1. "BaseRepository" abstract class � olu�turulur. 
				"IBaseRepository" interface inden T Type olarak kal�t�m al�r. Ve interfacein i�erisinde tan�mlanm�� methodlar g�vdelendirilir.
		3.2.2. "EntityTypeRepositor" fodler � a��l�r
			3.2.2.1. "AppUserRepository" interface i a��l�r.			
				"BaseRepository"den "AppUser" tipinde ve IAppUserRepository dan kal�t�m al�n�r.
				Icerisine Db ba�lant�s� tan�mlan�r.

				public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
					{
						public AppUserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { } 
					}

			3.2.2.2. "CategoryRepository" interface i a��l�r.
				"BaseRepository"den "Category" tipinde ve ICategoryRepository den kal�t�m al�n�r.
				Icerisine Db ba�lant�s� tan�mlan�r.

				public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
					{
						public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

					}

			3.2.2.3. "ProductRepository" interface i a��l�r.
				"BaseRepository"den "Product" tipinde ve IProductRepository den kal�t�m al�n�r.
				Icerisine Db ba�lant�s� tan�mlan�r.

				public class ProductRepository:BaseRepository<Product>,IProductRepository
					{
						public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { } // => Database ba�lant�s� yap�ld�
					}

	3.3 "UnitOfWork" klas�r� a��l�r.
		3.1. "UnitOfWork" class� a��l�r. UnitOfWork i�in gerekli i�lemler yap�l�r.
			"IUnitOfWork" interface implement edilir. 
			
			public class UnitOfWork : IUnitOfWork
				{
					private readonly ApplicationDbContext _db;
					public UnitOfWork(ApplicationDbContext db)
					{
						this._db = db ?? throw new ArgumentNullException("Database Bo� Olamaz..!"); // => ?? Karar mekanizmas�n� ba�latt�k.Bu karar mekanizmas� ya bize db ba�lant�s�n� d�necek ya da ArgumentNullException ile hata mesaj� verecek.
					}


					private IAppUserRepository _appUserRepository;
					public IAppUserRepository AppUserRepository
					{
						get
						{
							if (_appUserRepository == null) _appUserRepository = new AppUserRepository(_db); // =Z _appUserRepository bo� gelirse databse ba�lant�s�n� �ret.
							return _appUserRepository; // => e�er dolu gelirse _appUserRepository d�nd�r .
						}
					}
        
					private ICategoryRepository _categoryRepository;
					public ICategoryRepository CategoryRepository
					{
						get
						{
							if (_categoryRepository == null) _categoryRepository = new CategoryRepository(_db);

							return _categoryRepository; 
						}
					}       
     

					private IProductRepository _productRepository;
					public IProductRepository ProductRepository
					{
						get
						{
							if (_productRepository == null) _productRepository = new ProductRepository(_db);

							return _productRepository;
						}
					}

					public async Task Commit() => await _db.SaveChangesAsync();

					private bool isDisposing = false;
					public async ValueTask DisposeAsync()
					{
						if (!isDisposing)
						{
							isDisposing = true;
							await DisposeAsync(true);
							GC.SuppressFinalize(this); // => Nesnemizi tamam�yla temizlenmesini sa�layacak.
						}
					}
					private async Task DisposeAsync(bool disposing)
					{
						if (disposing) await _db.DisposeAsync(); // => �retilen db nesnemizi dispose ettik.
					}

					public Task ExecuteSqlRaw(string sql, params object[] parameters)// => Bu method i�erisine sql sorgusu alacak execute edececek ve paramters g�nderecek
					{
						throw new NotImplementedException();
					}
				}

	3.3. "Context" folder� a��l�r.
		3.3.1. "ApplicationDbContext.cs" class� olu�turulur. Db ba�lant�s� i�in gerekli i�lemler yap�l�r.

			public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,int>
				{
					public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } 

					public DbSet<Category> Categories { get; set; }
					public DbSet<Product> Products { get; set; }
					public DbSet<AppUserToProduct> AppUserToProducts { get; set; }
					public DbSet<AppRole> AppRoles { get; set; }
					public DbSet<AppUser> AppUsers { get; set; }  

					protected override void OnModelCreating(ModelBuilder builder)
					{
						builder.ApplyConfiguration(new AppUserMap());
						builder.ApplyConfiguration(new AppRoleMap());
						builder.ApplyConfiguration(new AppUserToProductMap());
						builder.ApplyConfiguration(new CategoryMap());
						builder.ApplyConfiguration(new ProductMap());           

						base.OnModelCreating(builder);
					}
				}
4. "ECommerceApp.ApplicationLayer" ismiyle Class Library (.Core) projesi eklenir.
	
	Y�klenecek Paketler :	-Autofac(6.1.0)
							-AutoMapper(10.1.1)
							-AutoMapper.Extensions.Microsoft.DependencyInjection(8.1.1)
							-FluentValidation.AspNetCore(9.5.3)
							-Microsoft.AspNetCore.Identity(2.2.0)
							-SixLabors.ImageSharp(1.0.3)							

	Referans Proje		:	-"ECommerceApp.DomainLayer"
							-"ECommerceApp.InfrastructureLayer"

	4.1. "Model" Folder� a��l�r.
		4.1.1. DTOs Fodler� a��l�r. I� odakl� olarak kullan�m ihtiyac�na g�re DTO lar olu�tu�rulur.
	4.2. "Mapper" Folder� a��l�r.
		4.2.1. "Mapping" class � a��l�r. ve DTO lar�n olu�turuldu�u varl�klar� ile maplama i�lemi yap�l�r. AutoMApper Paketinden yararlanmak i�in "Profile" dan kal�t�m al�n�r.
			
			 public class Mapping : Profile
				{

					public Mapping()
					{
           
						CreateMap<AppUser, EditProfileDTO>().ReverseMap();
						CreateMap<AppUser, LoginDTO>().ReverseMap();
						CreateMap<AppUser, ProfileDTO>().ReverseMap();
						CreateMap<AppUser, RegisterDTO>().ReverseMap();

						CreateMap<Category, CategoryDTO>().ReverseMap();
						CreateMap<CategoryDTO, Category>().ReverseMap();

						CreateMap<Product, ProductDTO>().ReverseMap();
						CreateMap<ProductDTO, Product>().ReverseMap();
					}
				}
	4.3. "Services" Folder� a��l�r. 
		4.3.1. "Interface" Folder� a��l�r.
			4.3.1.1. "IAppUserService" interface i a��l�r ve i�ine "AppUserService" de g�vdelendirilecek olan ve "AppUser" ile ilgili kullan�lmak istenilen Methodlar tan�mlan�r.
			
			public interface IAppUserService
				{
					Task DeleteUser(int id);
					Task EditUser(EditProfileDTO editProfileDTO);

					Task<IdentityResult> Register(RegisterDTO registerDto);
					Task<SignInResult> LogIn(LoginDTO loginDTO);
					Task LogOut();

					Task<int> GetUserIdFromName(string userName);

					Task<EditProfileDTO> GetById(int id);
					Task<LoginDTO> GetLoginById(int id);
					Task<ProfileDTO> GetByUserName(string userName);
				}

			4.3.1.2. "ICategoryService" interface i a��l�r ve i�ine "CategoryService" de g�vdelendirilecek olan ve "Category" ile ilgili kullan�lmak istenilen Methodlar tan�mlan�r.

			public interface ICategoryService
				{
					Task Create(CategoryDTO categoryDTO);
					Task Delete(CategoryDTO categoryDTO);
					Task Update(CategoryDTO categoryDTO);

					Task<CategoryDTO> GetById(int id);
					Task<CategoryDTO> GetCategoryName(CategoryDTO categoryDTO);
      
					Task<List<Category>> CategoryList();
				}

			4.3.1.3. "IProductService" interface i a��l�r ve i�ine "ProductService" de g�vdelendirilecek olan ve "Product" ile ilgili kullan�lmak istenilen Methodlar tan�mlan�r.

			public interface IProductService
				{
					Task Create(ProductDTO productDTO);
					Task Update(ProductDTO productDTO);

					Task<ProductDTO> GetById(int id);      

					Task<List<Product>> GetAll();
     
					Task<List<Product>> GetOrderByList();

					Task<List<Category>> GetCategory(); 
       
					Task<List<Product>> GetList(int id);
				}

		4.3.2. "Concrete" Folder� a��l�r.
			4.3.2.1. "AppUserService" class � a��l�r. "IAppUserService" interface i implement edilir. ve Methodlar g�vdelendirilir. 

			public class AppUserService : IAppUserService
				{
					private readonly IUnitOfWork _unitOfWork;
					private readonly IMapper _mapper;
					private readonly SignInManager<AppUser> _signInManager;
					private readonly UserManager<AppUser> _userManager;

					public AppUserService(IUnitOfWork unitOfWork,
										  IMapper mapper,
										  SignInManager<AppUser> signInManager,
										  UserManager<AppUser> userManager)
					{
						this._unitOfWork = unitOfWork;
						this._mapper = mapper;
						this._signInManager = signInManager;
						this._userManager = userManager;
					}

					public async Task DeleteUser(int id)
					{
						AppUser user = await _unitOfWork.AppUserRepository.GetById(id);

						_unitOfWork.AppUserRepository.Delete(user);
           
					}

					public async Task EditUser(EditProfileDTO editProfileDTO)
					{
						var user = await _unitOfWork.AppUserRepository.GetById(editProfileDTO.Id);
						if (user != null)					

						{
							if (editProfileDTO.Image != null)
							{
								using var image = Image.Load(editProfileDTO.Image.OpenReadStream());
								//image.Mutate(x => x.Resize(256, 256));
								image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
								user.ImagePath = ("/images/users/" + user.UserName + ".jpg");
								_unitOfWork.AppUserRepository.Update(user);
								await _unitOfWork.Commit();
							}
							if (editProfileDTO.UserName != user.UserName)
							{
								var newUserName = await _userManager.FindByNameAsync(editProfileDTO.UserName);

								if (newUserName == null)
								{
									await _userManager.SetUserNameAsync(user, editProfileDTO.UserName);
									//user.UserName = editProfileDTO.UserName;
									await _signInManager.SignInAsync(user, isPersistent: true);
								}
							}
							if (editProfileDTO.FullName != user.FullName)
							{
								user.FullName = editProfileDTO.FullName;
							}
							if (editProfileDTO.Email != user.Email)
							{
								var isnewEmail = await _userManager.FindByEmailAsync(editProfileDTO.Email);
								if (isnewEmail == null)
								{
									await _userManager.SetEmailAsync(user, editProfileDTO.Email);
								}
							}
							if (editProfileDTO.Password != null)
							{
								user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, editProfileDTO.Password);
							}

							_unitOfWork.AppUserRepository.Update(user);
							await _unitOfWork.Commit();
						}
					}

					public async Task<EditProfileDTO> GetById(int id)
					{
						var user = await _unitOfWork.AppUserRepository.GetById(id);

						return _mapper.Map<EditProfileDTO>(user);
					}
					public async Task<LoginDTO> GetLoginById(int id)
					{
						var user = await _unitOfWork.AppUserRepository.GetById(id);

						return _mapper.Map<LoginDTO>(user);
					}

					public async Task<ProfileDTO> GetByUserName(string userName)
					{
						var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(
							selector: x => new ProfileDTO {
								ImagePath = x.ImagePath,
								UserName = x.UserName
							},
							expression: y=>y.UserName==userName                
							);


						return user;
					}

					public async Task<int> GetUserIdFromName(string userName)
					{
						var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(
							selector: x => x.Id,
							expression: x => x.UserName == userName);

						return Convert.ToInt32(user); // NOT => Burada int tipinde id d�nmesi gerekiyor kontrol
					}

					public async Task<SignInResult> LogIn(LoginDTO loginDTO)
					{
						var user = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, loginDTO.RememberMe, false);

       
						return user;
					}

					public async Task LogOut()
					{
						await _signInManager.SignOutAsync();
					}

					public async Task<IdentityResult> Register(RegisterDTO registerDTO)
					{
						var user = _mapper.Map<AppUser>(registerDTO);

						var result = await _userManager.CreateAsync(user, registerDTO.ConfirmPassword);

						if (result.Succeeded)
						{
							await _signInManager.SignInAsync(user, isPersistent: false); //isPersistent => Taray�c� kullan�c� giri� bilgilerini haf�za da tutmsun mu diye sorar.
						}
						return result;
					}

				}
			
			4.3.2.2. "CategoryService" class � a��l�r. "ICategoryService" interface i implement edilir. ve Methodlar g�vdelendirilir. 

				public class CategoryService : ICategoryService
					{
						private readonly IUnitOfWork _unitOfWork;
						private readonly IMapper _mapper;
  

						public CategoryService(IUnitOfWork unitOfWork,
											  IMapper mapper
											  )
						{
							this._unitOfWork = unitOfWork;
							this._mapper = mapper;
            
						}

						public async Task<List<Category>> CategoryList()
						{
							List<Category> categories = await _unitOfWork.CategoryRepository.GetFilteredList(
							   selector: x => new Category
							   {
								   Id = x.Id,
								   CategoryName = x.CategoryName,

							   },
							   expression: y => y.Status != DomainLayer.Enums.Status.Passive);

							//List<Category> categories = await _unitOfWork.CategoryRepository.Get(x => x.Status != Status.Passive);


							return categories;
						}

						public async Task Create(CategoryDTO categoryDTO)
						{
							var category = await _unitOfWork.CategoryRepository.FirstOrDefault(x => x.CategoryName == categoryDTO.CategoryName); // e�er b�yle bir category yoksa ekle

							if (category==null)
							{
								var newCategory = _mapper.Map<CategoryDTO, Category>(categoryDTO);
								await _unitOfWork.CategoryRepository.Add(newCategory);
								await _unitOfWork.Commit();
							}
            
						}

						public async Task Delete(CategoryDTO categoryDTO)
						{
							var category = await _unitOfWork.CategoryRepository.GetById(categoryDTO.Id);
							if (category != null)
							{
								_unitOfWork.CategoryRepository.Delete(category);
								await _unitOfWork.Commit();
							}

						}


						public async Task<CategoryDTO> GetById(int id)
						{
							var category = await _unitOfWork.CategoryRepository.GetById(id);

							return _mapper.Map<CategoryDTO>(category);
						}

						public async Task<CategoryDTO> GetCategoryName(CategoryDTO categoryDTO)
						{
							var category = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefault(
							   selector: y => new CategoryDTO
							   {
								   CategoryName = categoryDTO.CategoryName
							   },
							   expression: x => Convert.ToInt32(x.Id) == categoryDTO.Id
							   );
							return category;
						}

						public async Task Update(CategoryDTO categoryDTO)
						{
							var category = await _unitOfWork.CategoryRepository.FirstOrDefault(x => x.CategoryName == categoryDTO.CategoryName);

							if (category!=null)
							{
								category.CategoryName = categoryDTO.CategoryName;
							}

							_unitOfWork.CategoryRepository.Update(category);
							await _unitOfWork.Commit();
						}
					}

				4.3.2.2. "ProductService" class � a��l�r. "IProductService" interface i implement edilir. ve Methodlar g�vdelendirilir. 

					public class ProductService : IProductService
						{
							private readonly IUnitOfWork _unitOfWork;
							private readonly IMapper _mapper;
							private readonly IWebHostEnvironment _webHostEnvironment;

							public ProductService(IUnitOfWork unitOfWork,
												  IMapper mapper,
												  IWebHostEnvironment webHostEnvironment)
							{
								this._unitOfWork = unitOfWork;
								this._mapper = mapper;
								this._webHostEnvironment = webHostEnvironment;
							}

							public async Task Create(ProductDTO productDTO)
							{
								if (productDTO != null)
								{
									string imageName = "noimage.png";
									if (productDTO.Image != null)
									{
										string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/product");
										string newName = productDTO.Description.Trim().Replace(" ", string.Empty).Substring(0, 7);
										imageName = newName + "_" + productDTO.Image.FileName;
										string filePath = Path.Combine(uploadDir, imageName);
										FileStream fileStream = new FileStream(filePath, FileMode.Create);
										await productDTO.Image.CopyToAsync(fileStream);
										fileStream.Close();
									}
									productDTO.ImagePath = imageName;
									Product product = _mapper.Map<ProductDTO, Product>(productDTO);
									await _unitOfWork.ProductRepository.Add(product);
									await _unitOfWork.Commit();
								}
							}

							public async Task<List<Product>> GetAll()
							{
								List<Product> products = await _unitOfWork.ProductRepository.Get(x => x.Status != Status.Passive);
								return products;
							}

							public async Task<ProductDTO> GetById(int id)
							{
								var product = await _unitOfWork.ProductRepository.GetById(id);

								return _mapper.Map<ProductDTO>(product);
							}     

							public async Task<List<Product>> GetList(int id)
							{
								var category = await _unitOfWork.CategoryRepository.GetById(id);

								List<Product> products = await _unitOfWork.ProductRepository.Get(x => x.CategoryId == category.Id);

								return products;
							}      

							public async Task<List<Product>> GetOrderByList()
							{
								var productList = await _unitOfWork.ProductRepository.GetFilteredList(
									selector: x => new Product
									{
										ProductName = x.ProductName,
										Description = x.Description,
										DescText = x.DescText,
										Image = x.Image,
										ImagePath = x.ImagePath,
										UnitPrice = x.UnitPrice
									},
									orderby: x => x.OrderByDescending(x => x.CreateDate)
									);
								return productList;
							}

							public async Task<List<Category>> GetCategory()
							{
								var categoryList = await _unitOfWork.CategoryRepository.Get(x => x.Status != Status.Passive);
								return categoryList;
							}

							public async Task Update(ProductDTO productDTO)
							{
								var products = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.Id == productDTO.Id);
        
								if (productDTO != null)
								{
									if (productDTO.Image != null)
									{
										string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/product");
										if (!string.Equals(products.Image, "noimage.png"))
										{
											string oldPath = Path.Combine(uploadDir, products.ImagePath);
											if (File.Exists(oldPath))
											{
												File.Delete(oldPath);
											}
										}

										string imageName = productDTO.ProductName + "_" + productDTO.Image.FileName;
										string filePath = Path.Combine(uploadDir, imageName);
										FileStream fileStream = new FileStream(filePath, FileMode.Create);
										await productDTO.Image.CopyToAsync(fileStream);
										fileStream.Close();
										products.ImagePath = imageName;
									}
									if (productDTO.Description != null)
									{
										products.Description = productDTO.Description;
									}
               
									_unitOfWork.ProductRepository.Update(products);
									await _unitOfWork.Commit();

								}
            
							}
     
						}

	4.4. "IOC" folder� a��l�r. 	Third PArt IOC container kullanmak istedi�im i�in normalde Startup class�nda yap�lmas� gereken IOC ocntainer i�lemini burada "AutoFact Container ile ger�ekle�tiricem.
		4.4.1. "AutoFactContainer" class� a��l�r. 
			
			public class AutoFactContainer:Module
				{
					protected override void Load(ContainerBuilder builder)
					{
						builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
						builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
						builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();

						builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
					}
				}
5. "ECommerceApp.PresentationLayer" ismiyle  ASP.NET Core Web App(Model-View-Controller) projesi eklenir.
	
	Y�klenecek Paketler :	-Autofac.Extensions.DependencyInjection(7.1.0)
							-Microsoft.AspNetCore.Identity.EntityFrameworkCore(5.0.4)
							-Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation(3.1.0)
							-Microsoft.EntityFrameworkCore(5.0.4)
							-Microsoft.EntityFrameworkCore.Design(5.0.4)	
							-Microsoft.EntityFrameworkCore.SqlServer(5.0.4)
							-Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)								

	Referans Proje		:	-"ECommerceApp.ApplicationLayer"
							-"ECommerceApp.InfrastructureLayer"
	
	5.1. "Startup.cs" class �n�n i�erisi d�zenlenir.
		
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
					services.AddScoped<AppRole>();
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

	5.2 "appsetting.json" i�erisine Db i�in gerekli i�lem yaz�l�r.
		
		 "AllowedHosts": "*",
		  "ConnectionStrings": {
			"DefaultConnection": "Server=RIDVANORUN;Database=ECommerceApp;Trusted_Connection=True;MultipleActiveResultSets=True;"    
			  
		  }

	5.3. "Program.cs" i�erisine third part �oc container i�in gerkli i�lem yaz�l�r.
		
		public class Program
			{
				public static void Main(string[] args)
				{
					CreateHostBuilder(args).Build().Run();
				}

				public static IHostBuilder CreateHostBuilder(string[] args) =>
				   Host.CreateDefaultBuilder(args)
				   .UseServiceProviderFactory(new AutofacServiceProviderFactory())
				   .ConfigureContainer<ContainerBuilder>(builder =>
				   {
					   builder.RegisterModule(new AutoFactContainer());
				   })
					   .ConfigureWebHostDefaults(webBuilder =>
					   {
						   webBuilder.UseStartup<Startup>();
					   });
			}

	5.4. "Migration" i�lemi yap�l�r. Database G�ncellenir.

	5.5. Controllers Folder� i�erise Controllerlar eklenir ve i�leri d�zenlenir.
		5.5.1. AccountController.cs 

			public class AccountController : Controller
				{
					private readonly IAppUserService _appUser;

					public AccountController(IAppUserService appUser)
					{
						_appUser = appUser;
					}
					#region Register
					public IActionResult Register()
					{
						if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
						return View();
					}

					[HttpPost]
					public async Task<IActionResult> Register(RegisterDTO model)
					{
						if (ModelState.IsValid)
						{
							var result = await _appUser.Register(model);
							if (result.Succeeded)
								return RedirectToAction("Index", "Home");
							foreach (var item in result.Errors) ModelState.AddModelError(string.Empty, item.Description);
						}
						return View(model);
					}
					#endregion

					#region LogIn
					public IActionResult Login()
					{
						return View();
					}

					[HttpPost]
					public async Task<IActionResult> Login(LoginDTO model)
					{
						if (model!=null)
						{
							if (ModelState.IsValid)
							{
								var userId = await _appUser.GetUserIdFromName(model.UserName);
								var user = await _appUser.GetLoginById(userId);


								var result = await _appUser.LogIn(model);

								if (result.Succeeded)
								{
									return RedirectToAction(nameof(HomeController.Index), "Home"); // E�er giri� ba�ar�l� olursa HomeController'daki Home Index'a y�nlendir.
								}
								ModelState.AddModelError(String.Empty, "Ge�ersiz giri� denemesi..!");
							}
							return View();
						}
						return View();
           
					}
					#endregion

					#region LogOut
					[HttpPost]
					public async Task<IActionResult> LogOut()
					{
						await _appUser.LogOut();

						return RedirectToAction(nameof(HomeController.Index), "Home");
					}
					#endregion

					public async Task<IActionResult> EditProfile(string userName)
					{
						if (userName == User.Identity.Name)
						{
							var user = await _appUser.GetById(User.GetUserId());

							if (user == null) return NotFound();

							return View(user);
						}
						else
						return RedirectToAction(nameof(HomeController.Index), "Home");
					}

					[HttpPost]
					public async Task<IActionResult> EditProfile(EditProfileDTO model)
					{
						await _appUser.EditUser(model);
						return RedirectToAction(nameof(HomeController.Index), "Home");
					}

					public async Task<IActionResult> Details (ProfileDTO model)
					{            
						var user = await _appUser.GetByUserName(model.UserName);
						return View(user);
					}
				}
				
		5.5.2. CartController.cs 

			public class CartController : Controller
				{
					private readonly ApplicationDbContext _applicationDbContext;

					public CartController(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

					public IActionResult Index()
					{

						List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

						CartViewModel cartViewModel = new CartViewModel
						{
							CartItems = cart,
							GrandTotal = cart.Sum(x => x.Price * x.Quantity)
						};

						return View(cartViewModel);
					}

					public async Task<IActionResult> Add(int id)
					{
						Product product = await _applicationDbContext.Products.FindAsync(id);

						List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();//�art bilgilerini session'da tutma kararar� alm��t�k. Mant�ks�z bir hareket de�ildir. �o�u e-ticaret sitesi bu yakla��m� kullanmaktad�r (g�n�m�z i�in check edin). Bunun i�in uygulam i�erisinde bulunan varl�klar� "json" d�n��t�rmmemiz gerekmekteydi bunun i�in extension method yazd�k. burada bu eztension methodun Deserialize Object k�sm�n� kulland�k. Neden? ��nk� Session �zerinden al�yorum cart'�n bilgilerini. 

						CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

						if (cartItem == null)
						{
							cart.Add(new CartItem(product));
							 // sepete eklenmek istenilen �r�n yok ise yani �r�n sepete ilk kez eklenecekse
						}
						else
						{
							cartItem.Quantity += 1;//sepette var ise increase ediyoruz
                
						}

						HttpContext.Session.SetJson("Cart", cart);

						if (HttpContext.Request.Headers["X-Request-With"] != "XMLHttpRequest")
						{
							return RedirectToAction("Index", "Product");//"actionName, controllerName"
						}

						return RedirectToAction("Index"/*, "Product"*/);
					}

					public IActionResult Decrease(int id)
					{
						List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

						CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

						if (cartItem.Quantity > 1)
							--cartItem.Quantity;
						else
							cart.RemoveAll(x => x.ProductId == id);


						if (cart.Count == 0)
							HttpContext.Session.Remove("Cart");
						else
							HttpContext.Session.SetJson("Cart", cart);


						return RedirectToAction("Index");
					}

					public IActionResult Remove(int id)
					{
						List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

						cart.RemoveAll(x => x.ProductId == id);

						if (cart.Count == 0)
							HttpContext.Session.Remove("Cart");
						else
							HttpContext.Session.SetJson("Cart", cart);

						return RedirectToAction("Index");
					}


					public IActionResult Clear()
					{
						HttpContext.Session.Remove("Cart");
						return RedirectToAction("Index", "Product");//"actionName, controllerName"
					}

					public IActionResult Payment()
					{
						HttpContext.Session.Clear();
						return RedirectToAction("Index","Product");//"actionName, controllerName"
					}

					public IActionResult PaymentView()
					{
						HttpContext.Session.Clear();
						return View();
					}
				}

		5.5.3. HomeController.cs 	

			public class HomeController : Controller
				{
					private readonly ILogger<HomeController> _logger;

					public HomeController(ILogger<HomeController> logger)
					{
						_logger = logger;
					}

					public IActionResult Index()
					{
						return View();
					}

					public IActionResult Privacy()
					{
						return View();
					}

					[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
					public IActionResult Error()
					{
						return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
					}
				}

		5.5.4. ProductController.cs 

			public class ProductController : Controller
				{
					private readonly IProductService _productService;
					private readonly ICategoryService _categoryService;
   
					public ProductController(IProductService productService,
											ICategoryService categoryService)
					{
						_productService = productService;
						_categoryService = categoryService;
					}

					public async Task<IActionResult> Index(string search)
					{
						var model = await _productService.GetAll();
						if (!string.IsNullOrEmpty(search))
						{
							model = model.Where(x => x.ProductName.Contains(search) || x.Description.Contains(search)).ToList();
						}

						return View(model);
					}     

					public IActionResult Details(int id)
					{
						ViewBag.productId = id;

						return View();
					}


					public async Task<IActionResult> GetList(CategoryDTO categoryDTO)
					{
            
						return View(await _productService.GetList(categoryDTO.Id));
					}

					public async Task<IActionResult> GetProductList(Category category)
					{
						List<Product> products = await _productService.GetList(category.Id);

						return View(products);
					}

				}

		5.5.5. Olu�turulan controller lar i�in gerekli viewlar olu�turtulur ve d�zenlemeleri yap�l�r.

	5.6. Areas folderi a��l�r ve i�erisi d�zenlenir.
		5.6.1. Admin areasi A��l�r.
			5.6.1.1. Controllers folder� i�erisine ihtiys� duyulan controllerlar eklenir ve d�zenlenir. bununla birlikte viewlar� olu�turulur ve d�zenlenir.
		5.6.2. Seller Areasi a��l�r.
			5.6.2.1. Controllers folder� i�erisine ihtiys� duyulan controllerlar eklenir ve d�zenlenir. bununla birlikte viewlar� olu�turulur ve d�zenlenir
			