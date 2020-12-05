using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DatabaseChoose
{
    /// <summary>
    /// 根据特性选择数据连接字符串
    /// IResourceFilter  在构造函数执行之前执行
    /// </summary>
    public class DatabaseChooseFilter : IResourceFilter
    {
        private IDataBaseConnectionFactory _dataBaseConnectionFactory;

        public DatabaseChooseFilter(IDataBaseConnectionFactory dataBaseConnectionFactory)
        {
            _dataBaseConnectionFactory = dataBaseConnectionFactory;
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
                    _dataBaseConnectionFactory.SetDatabaseChooseType(DatabaseChooseType.Write);
                }
            }
            else
            {
                if (context.HttpContext.Request.Method == HttpMethod.Get.Method)
                {
                    _dataBaseConnectionFactory.SetDatabaseChooseType(DatabaseChooseType.Read);
                }
                else if (context.HttpContext.Request.Method == HttpMethod.Post.Method)
                {
                    _dataBaseConnectionFactory.SetDatabaseChooseType(DatabaseChooseType.Write);
                }
                else if (context.HttpContext.Request.Method == HttpMethod.Put.Method)
                {
                    _dataBaseConnectionFactory.SetDatabaseChooseType(DatabaseChooseType.Write);
                }
                else if (context.HttpContext.Request.Method == HttpMethod.Delete.Method)
                {
                    _dataBaseConnectionFactory.SetDatabaseChooseType(DatabaseChooseType.Write);
                }
                else
                {
                    _dataBaseConnectionFactory.SetDatabaseChooseType(DatabaseChooseType.Read);
                }
            }
        }
    }
}