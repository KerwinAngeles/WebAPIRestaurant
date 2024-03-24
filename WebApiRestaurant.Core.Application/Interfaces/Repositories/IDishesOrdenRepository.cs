using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Interfaces.Repositories
{
    public interface IDishesOrdenRepository : IGenericRepository<DishesOrden>
    {
        Task DeleteAllDisheById(DishesOrden dishesOrden);
        
        Task<List<DishesOrden>> GetAllDishesOrdenById(int Id);
    }
}
