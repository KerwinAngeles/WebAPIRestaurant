using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Interfaces.Repositories
{
    public interface IDisheIngredientRepositorycs : IGenericRepository<DisheIngredient>
    {
        Task DeleteAllDisheById(DisheIngredient disheIngredient);
        Task<List<DisheIngredient>> GetAllDisheById(int Id);
    }
}
