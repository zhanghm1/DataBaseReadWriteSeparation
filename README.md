# DataBaseReadWriteSeparation
读写分离

ConfigureServices
          
    services.AddControllers(a=> {
        a.Filters.Add<DatabaseChooseFilter>();
    });

    string writeConnectionString = Configuration.GetConnectionString("writeDB");
    string readConnectionString = Configuration.GetConnectionString("readDB");
    services.AddDbContext<TestDbcontext>(options =>
            options.UseMySql(writeConnectionString));

    services.AddDatabaseChoose(a=> {
        a.WriteConnectionString = writeConnectionString;
        a.ReadConnectionString = readConnectionString;
        a.DefaultChoose = DefaultChoose.Write;
    });

EFRepository

    private readonly TestDbcontext _context;
    private readonly DbSet<T> _dbset;
    public EFRepository(TestDbcontext context, DataBaseConnectionFactory dataBaseConnectionFactory)
    {
    if (!string.IsNullOrWhiteSpace(dataBaseConnectionFactory.ConnectionString))
    {
        context.Database.GetDbConnection().ConnectionString = dataBaseConnectionFactory.ConnectionString;
    }
    this._context = context;
    this._dbset = context.Set<T>();  
    }
    
Controller
  
    [HttpPost]
    [DatabaseChoose(true)]
    public async Task<bool> post()
    {
        Users user = new Users() {
          Name="张三",
          Sex= Guid.NewGuid().ToString()
        };
        return await _repositoryUser.AddAsync(user);
    }
  
  
