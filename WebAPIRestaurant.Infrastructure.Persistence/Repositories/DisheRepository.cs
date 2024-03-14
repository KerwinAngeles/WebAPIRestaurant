using Microsoft.EntityFrameworkCore;
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
    public class DisheRepository : GenericRepository<Dishe>, IDisheRepository
    {
        private readonly ApplicationDbContext _context;
        public DisheRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Dishe>> GetAll()
        {
            var dishes = await _context.Set<Dishe>().Include(x => x.DishesIngredients).ThenInclude(ingredient => ingredient.Ingredient).ToListAsync();
            return dishes;
        }
    }
}
