using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseReadWriteSeparation.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DataBaseReadWriteSeparation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var context = services.GetRequiredService<TestDbcontext>();
                        context.Database.Migrate();

                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                        logger.LogError(ex, "数据库初始化错误");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
