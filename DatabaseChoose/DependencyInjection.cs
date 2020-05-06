using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseChoose
{
    public static class DependencyInjection
    {
        public static void AddDatabaseChoose(this IServiceCollection services,Action<DatabaseChooseOptions> action)
        {
            DatabaseChooseOptions  options = new DatabaseChooseOptions();
            action(options);
            services.AddSingleton(options);

            services.AddScoped<DataBaseConnectionFactory>();
            services.AddHttpContextAccessor();
        }
    }
}
