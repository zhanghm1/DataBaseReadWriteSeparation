using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseChoose
{
    /// <summary>
    /// 根据特性选择数据连接字符串
    /// IResourceFilter  在构造函数执行之前执行
    /// </summary>
    public class DatabaseChooseFilter : IResourceFilter
    {
        private DataBaseConnectionFactory _dataBaseConnectionFactory;
        public IConfiguration Configuration { get; }

        public DatabaseChooseFilter(DataBaseConnectionFactory dataBaseConnectionFactory, IConfiguration configuration)
        {
            _dataBaseConnectionFactory = dataBaseConnectionFactory;
            Configuration = configuration;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            var dbWrite = (DatabaseChooseAttribute)controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(DatabaseChooseAttribute), false).FirstOrDefault();
            if (dbWrite != null)
            {
                if (dbWrite.IsWrite)
                {
                    _dataBaseConnectionFactory.SetIsWrite(true);
                }
            }
        }
    }
}
