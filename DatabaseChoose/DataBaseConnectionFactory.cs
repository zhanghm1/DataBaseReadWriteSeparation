using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseChoose
{
    public class DataBaseConnectionFactory
    {
        public DataBaseConnectionFactory(DatabaseChooseOptions _options)
        {
            this.options = _options;
            if (string.IsNullOrWhiteSpace(this.options.WriteConnectionString)|| string.IsNullOrWhiteSpace(this.options.ReadConnectionString))
            {
                throw new Exception("需要配置连接字符串");
            }


            this.DatabaseChooseType = this.options.DefaultChoose;
            this.ConnectionString = GetConnectionString(this.DatabaseChooseType);

        }

        public DatabaseChooseOptions options { get; set; }
        /// <summary>
        /// 需要使用的连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 是否使用写的数据库字符串
        /// </summary>
        public DatabaseChooseType  DatabaseChooseType { get; set; }

        public void SetDatabaseChooseType(DatabaseChooseType  chooseType)
        {
            this.DatabaseChooseType = chooseType;
            this.ConnectionString = GetConnectionString(chooseType);
        }
        public DbContext GetDbContext(DbContext context)
        {
            context.Database.GetDbConnection().ConnectionString = ConnectionString;
            return context;
        }

        private string GetConnectionString(DatabaseChooseType chooseType)
        {
            switch (chooseType)
            {
                case DatabaseChooseType.Write:
                    return this.options.WriteConnectionString;
                case DatabaseChooseType.Read:
                    return this.options.ReadConnectionString;
                default:
                    return null;
            }
        }
    }


}
