using DatabaseChoose;
using DataBaseReadWriteSeparation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseReadWriteSeparation.Data
{
    public class TestDbcontext : DbContext
    {
        public TestDbcontext(DbContextOptions<TestDbcontext> options, DataBaseConnectionFactory connectionFactory) : base(GetOptions(connectionFactory))
        {
        }

        public DbSet<Users> Users { get; set; }

        public static DbContextOptions<TestDbcontext> GetOptions(DataBaseConnectionFactory connectionFactory)
        {
            //随机选择读数据库节点
            var optionsBuilder = new DbContextOptionsBuilder<TestDbcontext>();
            optionsBuilder.UseMySql(connectionFactory.ConnectionString, optionsBuilder =>
            {
                optionsBuilder.EnableRetryOnFailure(2);
            });

            return optionsBuilder.Options;
        }
    }
}