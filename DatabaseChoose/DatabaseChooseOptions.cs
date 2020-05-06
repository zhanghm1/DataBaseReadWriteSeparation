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
        /// </summary>
        public DefaultChoose DefaultChoose { get; set; } = DefaultChoose.Read;
    }
    public enum DefaultChoose
    { 
        Write,
        Read
    }
}
