using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseChoose
{
    public class DatabaseChooseAttribute:Attribute
    {
        /// <summary>
        /// 是否写
        /// true  使用writeDB
        /// false 使用readDB
        /// </summary>
        public bool IsWrite { get; set; }

        public DatabaseChooseAttribute(bool isWrite)
        {
            IsWrite = isWrite;
        }
    }
}
