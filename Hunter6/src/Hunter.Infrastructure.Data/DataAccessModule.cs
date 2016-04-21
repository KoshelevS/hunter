using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Hunter.Infrastructure.Data
{
    public static class DataAccessModule
    {
        public static void MigrateProjectContext(this IServiceScope serviceScope)
        {
            serviceScope.ServiceProvider.GetService<DomainContext>().Database.Migrate();
            serviceScope.ServiceProvider.GetService<DomainContext>().EnsureSeedData();
        }

        public static EntityFrameworkServicesBuilder AddDomainContext(
            this EntityFrameworkServicesBuilder builder, string connectionString)
        {
            return builder.AddDbContext<DomainContext>(
                options => options.UseSqlServer(connectionString));
        }
    }
}
