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
    public class DisheOrdenRepository : GenericRepository<DishesOrden>, IDishesOrdenRepository
    {
        private readonly ApplicationDbContext _context;
        public DisheOrdenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task DeleteAllDisheById(DishesOrden dishesOrden)
        {
            _context.DishesOrden.Remove(dishesOrden);
            await _context.SaveChangesAsync();
        }
        public async Task<List<DishesOrden>> GetAllDishesOrdenById(int Id)
        {
            return await _context.DishesOrden.Where(id => id.DishesId == Id).ToListAsync();
        }
    }
}
