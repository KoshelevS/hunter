using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Hunter.Infrastructure.Data
{
    public static class DataAccessModule
    {
        public static void MigrateProjectContext(this IServiceScope serviceScope)
        {
            serviceScope.ServiceProvider.GetService<ProjectContext>().Database.Migrate();
            serviceScope.ServiceProvider.GetService<ProjectContext>().EnsureSeedData();
        }

        public static EntityFrameworkServicesBuilder AddProjectContext(
            this EntityFrameworkServicesBuilder builder, string connectionString)
        {
            return builder.AddDbContext<ProjectContext>(
                options => options.UseSqlServer(connectionString));
        }
    }
}
