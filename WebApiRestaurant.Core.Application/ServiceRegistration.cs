using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.Services;

namespace WebAPIRestaurant.Core.Application
{
    public static  class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region "Services"
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddTransient<IOrdenService, OrdenService>();
            services.AddTransient<IDisheService, DisheService>();
            services.AddTransient<ITableService, TableService>();
            #endregion
        }
    }
}
