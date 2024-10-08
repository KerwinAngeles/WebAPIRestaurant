﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Interfaces.Repositories
{
    public interface ITableRepository : IGenericRepository<Table>
    {
        Task<Table> GetByOrdenId(int id);
    }
}
