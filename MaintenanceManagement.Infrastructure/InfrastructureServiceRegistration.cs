using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MaintenanceManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MaintenanceManagement.Application.Contracts;
using MaintenanceManagement.Infrastructure.Repositories;


namespace MaintenanceManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SQLContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
