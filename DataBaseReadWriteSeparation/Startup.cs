using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseChoose;
using DataBaseReadWriteSeparation.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataBaseReadWriteSeparation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(a=> {
                a.Filters.Add<DatabaseChooseFilter>();
            });

            string writeConnectionString = Configuration.GetConnectionString("writeDB");
            string readConnectionString = Configuration.GetConnectionString("readDB");
            services.AddDbContext<TestDbcontext>(options =>
                    options.UseMySql(writeConnectionString));


            services.AddScoped(typeof(EFRepository<>));



            services.AddDatabaseChoose(a=> {
                a.WriteConnectionString = writeConnectionString;
                a.ReadConnectionString = readConnectionString;
                a.DefaultChoose = DefaultChoose.Write;
            });
            

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
