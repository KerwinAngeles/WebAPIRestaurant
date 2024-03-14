using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Table;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class TableService : GenericService<SaveTableViewModel, TableViewModel, Table>, ITableService
    {
        private readonly ITableRepository _TableRepository;
        private readonly IMapper _mapper;

        public TableService(ITableRepository tableRepository, IMapper mapper) : base (tableRepository, mapper)
        {
            _TableRepository = tableRepository;
            _mapper = mapper;
        }
    }
}
