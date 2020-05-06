using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseChoose
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class DatabaseChooseOptions
    {
        public string WriteConnectionString { get; set; }
        public string ReadConnectionString { get; set; }

        /// <summary>
        /// 默认选择哪个连接字符串
        /// 不设置则使用Read
        /// </summary>
        public DatabaseChooseType DefaultChoose { get; set; } = DatabaseChooseType.Read;
    }
    public enum DatabaseChooseType
    { 
        Write,
        Read
    }
}
