# DataBaseReadWriteSeparation
读写分离
	
	http method get 请求使用read数据库
    http method post put delete 请求使用write数据库
	也可以使用特性指定使用read 还是write 数据库

ConfigureServices
          
    services.AddControllers(a=> {
        a.Filters.Add<DatabaseChooseFilter>();
    });

    string writeConnectionString = Configuration.GetConnectionString("writeDB");
    string readConnectionString = Configuration.GetConnectionString("readDB");

    services.AddDbContext<TestDbcontext>();

    services.AddDatabaseChoose(a=> {
        a.WriteConnectionString = writeConnectionString;
        a.ReadConnectionString = readConnectionString;
    });

Dbcontext

    public class TestDbcontext : DbContext
    {
        public TestDbcontext(DbContextOptions<TestDbcontext> options, DataBaseConnectionFactory connectionFactory) : base(GetOptions(connectionFactory))
        {
        }

        public DbSet<Users> Users { get; set; }

        public static DbContextOptions<TestDbcontext> GetOptions(DataBaseConnectionFactory connectionFactory)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbcontext>();
            optionsBuilder.UseMySql(connectionFactory.ConnectionString, optionsBuilder =>
            {
                optionsBuilder.EnableRetryOnFailure(2);
            });

            return optionsBuilder.Options;
        }
    }

Controller
  
    [HttpGet]
    public async Task<Users> Get()
    {
        return await _repositoryUser.FirstOrDefaultAsync(a => a.Id > 0);
    }

    [HttpGet]
    [Route("list")]
    [DatabaseChoose(true)]
    public async Task<IEnumerable<Users>> GetList()
    {
        var list = await _repositoryUser.GetListAsync(a => a.Id > 0);

        return list;
    }

    [HttpPost]
    public async Task<bool> Post()
    {
        Users user = new Users()
        {
            Name = "张三",
            Sex = Guid.NewGuid().ToString()
        };

        return await _repositoryUser.AddAsync(user);
    }
  
  
