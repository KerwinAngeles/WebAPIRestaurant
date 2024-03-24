using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Infrastructure.Persistence.Context;
using WebAPIRestaurant.Infrastructure.Persistence.Repositories;

namespace WebAPIRestaurant.Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region "ContextConfiguration"

            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString,
                m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            #endregion

            #region "Repositories"
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IDisheRepository, DisheRepository>();
            services.AddTransient<IOrdenRepository, OrdenRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IDisheIngredientRepositorycs, DisheIngredientRepository>();
            services.AddTransient<IDishesOrdenRepository, DisheOrdenRepository>();
            #endregion
        }
    }
}
