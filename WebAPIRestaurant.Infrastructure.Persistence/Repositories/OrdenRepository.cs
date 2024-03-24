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
    public class OrdenRepository : GenericRepository<Orden>, IOrdenRepository
    {
        private readonly ApplicationDbContext _context;
        public OrdenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Orden>> GetAll()
        {
            var orden = await _context.Set<Orden>().Include(x => x.DishesOrden).ThenInclude(dishes => dishes.Dishe).ToListAsync();
            return orden;
        }

        public override async Task<Orden> GetById(int id)
        {
            var orden = await _context.Set<Orden>().Include(x => x.DishesOrden).ThenInclude(dishes => dishes.Dishe).ToListAsync();
            return orden.FirstOrDefault(x => x.Id == id);
        }
    }
}
