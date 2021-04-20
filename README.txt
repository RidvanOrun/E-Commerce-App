
1. "ECommerceApp" ismiyle Blank Solution açýlýr.
2. "ECommerceApp.DomainLayer" ismiyle Class Library (.Core) projesi eklenir.
	
	Yüklenecek Paketler :	-Microsoft.AspNetCore.Http.Features(5.0.4)
							-Microsoft.EntityFrameworkCore(5.0.4)
							-Microsoft.Extensions.Identity.Stores(5.0.4)
							-Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)

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

		2.3.2. "EntityRepository" klasörü açýlýr.
			2.3.2.1. "IAppUserRepository" interface i açýlýr.
				"IBaseRepository" interface inden "AppUser" tipinde kalýtým alýnýr.
			2.3.2.2. "ICategoryRepository" interface i açýlýr.
				"IBaseRepository" interface inden "Category" tipinde kalýtým alýnýr.
			2.3.2.3. "IProductRepository" interface i açýlýr.
				"IBaseRepository" interface inden "Product" tipinde kalýtým alýnýr.

	2.4. "UnitOfWork" dosyasý oluþturulur.
		2.4.1. "IUnitOfWork" Dosyasý Oluþturulur.
		
			public interface IUnitOfWork:IAsyncDisposable
				{
					IAppUserRepository AppUserRepository { get; }

					ICategoryRepository CategoryRepository { get; }

					IProductRepository ProductRepository { get; }

					Task Commit();

					Task ExecuteSqlRaw(string sql, params object[] parameters);
				}

3. "ECommerceApp.InfastructureLayer" ismiyle Class Library (.Core) projesi eklenir.
	
	Yüklenecek Paketler :	-Microsoft.AspNetCore.Identity.EntityFrameworkCore(5.0.4)
							-Microsoft.EntityFrameworkCore(5.0.4)
							-Microsoft.EntityFrameworkCore.SqlServer(5.0.4)
							-Microsoft.EntityFrameworkCore.Tools(5.0.4)

	Referans Proje		:	-"ECommerceApp.DomainLayer"

	3.1. "Mapping" klasör açýlýr. 
		3.1.1. "Abstract" klasörü açýlýr.
			3.1.1.1. "BaseMap" class ý açýlýr. 

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

		3.1.2. "Concrete" klasörü açýlýr.
			3.1.2.1. "AppRole" class ý açýlýr.
				"BaseMap" abstract class ýndan "AppRole" tipinde kalýtým alýnýr.
			3.1.2.2. "AppUserMap" class ý açýlýr.
				"BaseMap" abstract class ýndan "AppUser" tipinde kalýtým alýnýr.
			3.1.2.3. "AppUserToProductMap" class ý açýlýr.
				"BaseMap" abstract class ýndan "AppUserToProduct" tipinde kalýtým alýnýr.				
			3.1.2.4. "ProductMap" class ý açýlýr.
				"BaseMap" abstract class ýndan "Product" tipinde kalýtým alýnýr.
			3.1.2.5. "CategoryMap" class ý açýlýr.
				"BaseMap" abstract class ýndan "Category" tipinde kalýtým alýnýr.
	3.2. "Repository" klasörü açýlýr.
		3.2.1. "BaseRepository" folderý açýlýr
			3.2.1.1. "BaseRepository" abstract class ý oluþturulur. 
				"IBaseRepository" interface inden T Type olarak kalýtým alýr. Ve interfacein içerisinde tanýmlanmýþ methodlar gövdelendirilir.
		3.2.2. "EntityTypeRepositor" fodler ý açýlýr
			3.2.2.1. "AppUserRepository" interface i açýlýr.			
				"BaseRepository"den "AppUser" tipinde ve IAppUserRepository dan kalýtým alýnýr.
				Icerisine Db baðlantýsý tanýmlanýr.

				public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
					{
						public AppUserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { } 
					}

			3.2.2.2. "CategoryRepository" interface i açýlýr.
				"BaseRepository"den "Category" tipinde ve ICategoryRepository den kalýtým alýnýr.
				Icerisine Db baðlantýsý tanýmlanýr.

				public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
					{
						public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

					}

			3.2.2.3. "ProductRepository" interface i açýlýr.
				"BaseRepository"den "Product" tipinde ve IProductRepository den kalýtým alýnýr.
				Icerisine Db baðlantýsý tanýmlanýr.

				public class ProductRepository:BaseRepository<Product>,IProductRepository
					{
						public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { } // => Database baðlantýsý yapýldý
					}

	3.3 "UnitOfWork" klasörü açýlýr.
		3.1. "UnitOfWork" classý açýlýr. UnitOfWork için gerekli iþlemler yapýlýr.
			"IUnitOfWork" interface implement edilir. 
			
			public class UnitOfWork : IUnitOfWork
				{
					private readonly ApplicationDbContext _db;
					public UnitOfWork(ApplicationDbContext db)
					{
						this._db = db ?? throw new ArgumentNullException("Database Boþ Olamaz..!"); // => ?? Karar mekanizmasýný baþlattýk.Bu karar mekanizmasý ya bize db baðlantýsýný dönecek ya da ArgumentNullException ile hata mesajý verecek.
					}


					private IAppUserRepository _appUserRepository;
					public IAppUserRepository AppUserRepository
					{
						get
						{
							if (_appUserRepository == null) _appUserRepository = new AppUserRepository(_db); // =Z _appUserRepository boþ gelirse databse baðlantýsýný üret.
							return _appUserRepository; // => eðer dolu gelirse _appUserRepository döndür .
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
							GC.SuppressFinalize(this); // => Nesnemizi tamamýyla temizlenmesini saðlayacak.
						}
					}
					private async Task DisposeAsync(bool disposing)
					{
						if (disposing) await _db.DisposeAsync(); // => Üretilen db nesnemizi dispose ettik.
					}

					public Task ExecuteSqlRaw(string sql, params object[] parameters)// => Bu method içerisine sql sorgusu alacak execute edececek ve paramters gönderecek
					{
						throw new NotImplementedException();
					}
				}

	3.3. "Context" folderý açýlýr.
		3.3.1. "ApplicationDbContext.cs" classý oluþturulur. Db baðlantýsý için gerekli iþlemler yapýlýr.

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
	
	Yüklenecek Paketler :	-Autofac(6.1.0)
							-AutoMapper(10.1.1)
							-AutoMapper.Extensions.Microsoft.DependencyInjection(8.1.1)
							-FluentValidation.AspNetCore(9.5.3)
							-Microsoft.AspNetCore.Identity(2.2.0)
							-SixLabors.ImageSharp(1.0.3)							

	Referans Proje		:	-"ECommerceApp.DomainLayer"
							-"ECommerceApp.InfrastructureLayer"

	4.1. "Model" Folderý açýlýr.
		4.1.1. DTOs Fodlerý açýlýr. Iþ odaklý olarak kullaným ihtiyacýna göre DTO lar oluþtuýrulur.
	4.2. "Mapper" Folderý açýlýr.
		4.2.1. "Mapping" class ý açýlýr. ve DTO larýn oluþturulduðu varlýklarý ile maplama iþlemi yapýlýr. AutoMApper Paketinden yararlanmak için "Profile" dan kalýtým alýnýr.
			
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
	4.3. "Services" Folderý açýlýr. 
		4.3.1. "Interface" Folderý açýlýr.
			4.3.1.1. "IAppUserService" interface i açýlýr ve içine "AppUserService" de gövdelendirilecek olan ve "AppUser" ile ilgili kullanýlmak istenilen Methodlar tanýmlanýr.
			
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

			4.3.1.2. "ICategoryService" interface i açýlýr ve içine "CategoryService" de gövdelendirilecek olan ve "Category" ile ilgili kullanýlmak istenilen Methodlar tanýmlanýr.

			public interface ICategoryService
				{
					Task Create(CategoryDTO categoryDTO);
					Task Delete(CategoryDTO categoryDTO);
					Task Update(CategoryDTO categoryDTO);

					Task<CategoryDTO> GetById(int id);
					Task<CategoryDTO> GetCategoryName(CategoryDTO categoryDTO);
      
					Task<List<Category>> CategoryList();
				}

			4.3.1.3. "IProductService" interface i açýlýr ve içine "ProductService" de gövdelendirilecek olan ve "Product" ile ilgili kullanýlmak istenilen Methodlar tanýmlanýr.

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

		4.3.2. "Concrete" Folderý açýlýr.
			4.3.2.1. "AppUserService" class ý açýlýr. "IAppUserService" interface i implement edilir. ve Methodlar gövdelendirilir. 

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

						return Convert.ToInt32(user); // NOT => Burada int tipinde id dönmesi gerekiyor kontrol
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
							await _signInManager.SignInAsync(user, isPersistent: false); //isPersistent => Tarayýcý kullanýcý giriþ bilgilerini hafýza da tutmsun mu diye sorar.
						}
						return result;
					}

				}
			
			4.3.2.2. "CategoryService" class ý açýlýr. "ICategoryService" interface i implement edilir. ve Methodlar gövdelendirilir. 

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
							var category = await _unitOfWork.CategoryRepository.FirstOrDefault(x => x.CategoryName == categoryDTO.CategoryName); // eðer böyle bir category yoksa ekle

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

				4.3.2.2. "ProductService" class ý açýlýr. "IProductService" interface i implement edilir. ve Methodlar gövdelendirilir. 

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

	4.4. "IOC" folderý açýlýr. 	Third PArt IOC container kullanmak istediðim için normalde Startup classýnda yapýlmasý gereken IOC ocntainer iþlemini burada "AutoFact Container ile gerçekleþtiricem.
		4.4.1. "AutoFactContainer" classý açýlýr. 
			
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
	
	Yüklenecek Paketler :	-Autofac.Extensions.DependencyInjection(7.1.0)
							-Microsoft.AspNetCore.Identity.EntityFrameworkCore(5.0.4)
							-Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation(3.1.0)
							-Microsoft.EntityFrameworkCore(5.0.4)
							-Microsoft.EntityFrameworkCore.Design(5.0.4)	
							-Microsoft.EntityFrameworkCore.SqlServer(5.0.4)
							-Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)								

	Referans Proje		:	-"ECommerceApp.ApplicationLayer"
							-"ECommerceApp.InfrastructureLayer"
	
	5.1. "Startup.cs" class ýnýn içerisi düzenlenir.
		
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

	5.2 "appsetting.json" içerisine Db için gerekli iþlem yazýlýr.
		
		 "AllowedHosts": "*",
		  "ConnectionStrings": {
			"DefaultConnection": "Server=RIDVANORUN;Database=ECommerceApp;Trusted_Connection=True;MultipleActiveResultSets=True;"    
			  
		  }

	5.3. "Program.cs" içerisine third part ýoc container için gerkli iþlem yazýlýr.
		
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

	5.4. "Migration" iþlemi yapýlýr. Database Güncellenir.

	5.5. Controllers Folderý içerise Controllerlar eklenir ve içleri düzenlenir.
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
									return RedirectToAction(nameof(HomeController.Index), "Home"); // Eðer giriþ baþarýlý olursa HomeController'daki Home Index'a yönlendir.
								}
								ModelState.AddModelError(String.Empty, "Geçersiz giriþ denemesi..!");
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

						List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();//çart bilgilerini session'da tutma karararý almýþtýk. Mantýksýz bir hareket deðildir. Çoðu e-ticaret sitesi bu yaklaþýmý kullanmaktadýr (günümüz için check edin). Bunun için uygulam içerisinde bulunan varlýklarý "json" dönüþtürmmemiz gerekmekteydi bunun için extension method yazdýk. burada bu eztension methodun Deserialize Object kýsmýný kullandýk. Neden? Çünkü Session üzerinden alýyorum cart'ýn bilgilerini. 

						CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

						if (cartItem == null)
						{
							cart.Add(new CartItem(product));
							 // sepete eklenmek istenilen ürün yok ise yani ürün sepete ilk kez eklenecekse
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

		5.5.5. Oluþturulan controller lar için gerekli viewlar oluþturtulur ve düzenlemeleri yapýlýr.

	5.6. Areas folderi açýlýr ve içerisi düzenlenir.
		5.6.1. Admin areasi Açýlýr.
			5.6.1.1. Controllers folderý içerisine ihtiysç duyulan controllerlar eklenir ve düzenlenir. bununla birlikte viewlarý oluþturulur ve düzenlenir.
		5.6.2. Seller Areasi açýlýr.
			5.6.2.1. Controllers folderý içerisine ihtiysç duyulan controllerlar eklenir ve düzenlenir. bununla birlikte viewlarý oluþturulur ve düzenlenir
			