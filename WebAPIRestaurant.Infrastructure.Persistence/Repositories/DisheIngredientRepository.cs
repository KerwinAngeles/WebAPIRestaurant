using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Domain.Entities;
using WebAPIRestaurant.Infrastructure.Persistence.Context;

namespace WebAPIRestaurant.Infrastructure.Persistence.Repositories
{
    public class DisheIngredientRepository : GenericRepository<DisheIngredient>, IDisheIngredientRepositorycs
    {
        private readonly ApplicationDbContext _context;
        public DisheIngredientRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAllDisheById(DisheIngredient disheIngredient)
        {
            _context.DishesIngredients.Remove(disheIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DisheIngredient>> GetAllDisheById(int Id)
        {
            return await _context.DishesIngredients.Where(id => id.DisheId == Id).ToListAsync();
        }
    }
}
