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
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        private readonly ApplicationDbContext _context;
        public TableRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
