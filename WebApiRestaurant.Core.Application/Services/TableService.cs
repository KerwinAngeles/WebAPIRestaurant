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

        public override async Task Add(SaveTableViewModel viewModel)
        {
            Table table = new Table();
            table.Id = viewModel.Id;
            table.CantPerson = viewModel.CantPerson;
            table.Description = viewModel.Description;
            table.StatusId = viewModel.StatusId;

            await base.Add(viewModel);
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

        public async Task ChangeStatus(int tableId, int statusId)
        {
            var table = await _TableRepository.GetById(tableId);
            table.Id = tableId;
            table.StatusId = statusId;
            await _TableRepository.UpdateAsync(table, tableId);
        }

        public override async Task<List<TableViewModel>> GetAll()
        {
            var table = await _TableRepository.GetAll();
            return table.Select( x => new TableViewModel 
            {
                Id = x.Id,
                CantPerson = x.CantPerson,
                Description = x.Description,
                StatusName = x.Status.Name

            }).ToList();
        }
    }
}
