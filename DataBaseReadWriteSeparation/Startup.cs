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
            services.AddControllers(a =>
            {
                a.Filters.Add<DatabaseChooseFilter>();
            });

            string writeConnectionString = Configuration.GetConnectionString("writeDB");

            var readDBs = Configuration.GetSection("ConnectionStrings:readDBs");
            var readConnectionStrings = readDBs.Get<List<string>>();

            services.AddDbContext<TestDbcontext>();

            services.AddDatabaseChoose(a =>
            {
                a.WriteConnectionString = writeConnectionString;            // http method post put delete 使用write 库
                a.ReadConnectionStrings = readConnectionStrings.ToArray();  // http method get  使用read 库
            });

            services.AddScoped(typeof(EFRepository<>));

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