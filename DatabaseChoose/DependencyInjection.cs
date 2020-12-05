using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseChoose
{
    public static class DependencyInjection
    {
        public static void AddDatabaseChoose(this IServiceCollection services, Action<DatabaseChooseOptions> action)
        {
            services.Configure(action);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<DatabaseChooseOptions>>().Value);
            services.AddScoped<IDataBaseConnectionFactory, DataBaseConnectionFactory>();
        }
    }
}