using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.DatabaseChoose
{
    public class DataBaseConnectionFactory : IDataBaseConnectionFactory
    {
        public DataBaseConnectionFactory(DatabaseChooseOptions _options)
        {
            this.options = _options;
            if (string.IsNullOrWhiteSpace(this.options.WriteConnectionString) || string.IsNullOrWhiteSpace(this.options.ReadConnectionString))
            {
                throw new Exception("需要配置连接字符串");
            }

            this.DatabaseChooseType = this.options.DefaultChoose;
            this.ConnectionString = GetConnectionString(this.DatabaseChooseType);
        }

        public DatabaseChooseOptions options { get; set; }

        public string GetConnectionString()
        {
            return ConnectionString;
        }

        public void SetDatabaseChooseType(DatabaseChooseType chooseType)
        {
            this.DatabaseChooseType = chooseType;
            this.ConnectionString = GetConnectionString(chooseType);
        }

        /// <summary>
        /// 是否使用写的数据库字符串
        /// </summary>
        private DatabaseChooseType DatabaseChooseType { get; set; }

        /// <summary>
        /// 需要使用的连接字符串
        /// </summary>
        private string ConnectionString { get; set; }

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