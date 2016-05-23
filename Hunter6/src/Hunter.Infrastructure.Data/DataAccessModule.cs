using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Hunter.Infrastructure.Data
{
    public static class DataAccessModule
    {
        public static void MigrateProjectContext(this IServiceScope serviceScope)
        {
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            DomainContext domainContext = serviceProvider.GetService<DomainContext>();
            domainContext.Database.Migrate();
            serviceScope.ServiceProvider.GetService<DomainContext>().EnsureSeedData();
        }

        // todo !! moved to startup
        //        public static EntityFrameworkServicesBuilder AddDomainContext(
        //            this EntityFrameworkServicesBuilder builder, string connectionString)
        //        {
        //            return builder.AddDbContext<DomainContext>(
        //                options => options.UseSqlServer(connectionString));
        //        }
    }
}
