using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Orden;
using WebAPIRestaurant.Core.Application.ViewModels.Table;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class TableService : GenericService<SaveTableViewModel, TableViewModel, Table>, ITableService
    {
        private readonly ITableRepository _TableRepository;
        private readonly IOrdenRepository _OrdenRepository;
        private readonly IDisheRepository _DisheRepository;
        private readonly IMapper _mapper;

        public TableService(ITableRepository tableRepository, IMapper mapper, IOrdenRepository ordenRepository, IDisheRepository disheRepository) : base (tableRepository, mapper)
        {
            _TableRepository = tableRepository;
            _OrdenRepository = ordenRepository;
            _DisheRepository = disheRepository;
            _mapper = mapper;
        }

        public async Task UpdateTable(EditSaveViewModel ev, int id)
        {
            var table = await _TableRepository.GetById(id);
            table.CantPerson = ev.CantPerson;
            table.Description = ev.Description;
            await _TableRepository.UpdateAsync(table, id);
        }

        public async Task<OrdenViewModel> GetTableOrden(int id)
        {          
            var table = await _TableRepository.GetById(id);

            OrdenViewModel ovm = new OrdenViewModel();
            ovm.SubTotal = table.Orden.SubTotal;
            ovm.State = table.Orden.State;
            ovm.DishesNames = table.Orden.Dishes.Select(d => d.Name).ToList();

            return ovm;
        }
    }
}
