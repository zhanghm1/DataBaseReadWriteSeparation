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

            //设置默认值
            switch (this.options.DefaultChoose)
            {
                case DefaultChoose.Write:
                    this.ConnectionString = this.options.WriteConnectionString;
                    this.IsWrite = true;
                    break;
                case DefaultChoose.Read:
                    this.ConnectionString = this.options.ReadConnectionString;
                    this.IsWrite = false;
                    break;
            }
            
        }

        public DatabaseChooseOptions options { get; set; }
        public string ConnectionString { get; set; }
        public bool IsWrite { get; set; } = false;

        public void SetIsWrite(bool isWrite)
        {
            this.IsWrite = isWrite;
            if (this.IsWrite)
            {
                this.ConnectionString = this.options.WriteConnectionString;
            }
            else
            {
                this.ConnectionString = this.options.ReadConnectionString;
            }
        }
    }


}
