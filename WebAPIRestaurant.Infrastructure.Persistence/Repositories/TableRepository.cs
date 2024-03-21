using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Domain.Entities;
using WebAPIRestaurant.Infrastructure.Persistence.Context;

namespace WebAPIRestaurant.Infrastructure.Persistence.Repositories
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        private readonly ApplicationDbContext _context;
        public TableRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Table> GetById(int id)
        {
            var table = await _context.Set<Table>()
                .Include(x => x.Orden)
                .ThenInclude(x => x.Dishes).ToListAsync();
            return table.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Table> GetByOrdenId(int id)
        {
            var table = await _context.Set<Table>().FirstOrDefaultAsync(x => x.OrdenId == id);
            return table;
        }
    }
}
