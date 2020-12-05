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
        private Random random { get; set; }

        public DatabaseChooseOptions()
        {
            random = new Random();
        }

        public string WriteConnectionString { get; set; }
        public string[] ReadConnectionStrings { get; set; }

        /// <summary>
        /// 默认选择哪个连接字符串
        /// 不设置则使用Write
        /// </summary>
        public DatabaseChooseType DefaultChoose { get; set; } = DatabaseChooseType.Write;

        public string ReadConnectionString
        {
            get
            {
                if (ReadConnectionStrings.Length == 1)
                {
                    return ReadConnectionStrings[0];
                }
                int index = random.Next(0, ReadConnectionStrings.Length);
                return ReadConnectionStrings[index];
            }
        }
    }

    public enum DatabaseChooseType
    {
        Write,
        Read
    }
}