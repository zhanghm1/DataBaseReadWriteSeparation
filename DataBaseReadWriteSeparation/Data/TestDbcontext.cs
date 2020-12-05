using DataBaseReadWriteSeparation.Models;
using EFCore.DatabaseChoose;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseReadWriteSeparation.Data
{
    public class TestDbcontext : DbContext
    {
        public TestDbcontext(IDataBaseConnectionFactory connectionFactory) : base(GetOptions(connectionFactory))
        {
        }

        public DbSet<Users> Users { get; set; }

        /// <summary>
        /// 获取DbContextOptions
        /// </summary>
        /// <param name="connectionFactory"></param>
        /// <returns></returns>
        public static DbContextOptions<TestDbcontext> GetOptions(IDataBaseConnectionFactory connectionFactory)
        {
            //随机选择读数据库节点
            var optionsBuilder = new DbContextOptionsBuilder<TestDbcontext>();
            optionsBuilder.UseMySql(connectionFactory.GetConnectionString(), optionsBuilder =>
            {
                optionsBuilder.EnableRetryOnFailure(2);
            });

            return optionsBuilder.Options;
        }
    }
}