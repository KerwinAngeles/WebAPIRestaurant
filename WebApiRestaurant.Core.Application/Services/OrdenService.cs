using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Enums;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Dishe;
using WebAPIRestaurant.Core.Application.ViewModels.Orden;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class OrdenService : GenericService<SaveOrdenViewModel, OrdenViewModel, Orden>, IOrdenService
    {
        private readonly IOrdenRepository _OrdenRepository;
        private readonly ITableRepository _TableRepository;
        private readonly IDisheRepository _disheRepository;
        private readonly IDishesOrdenRepository _dishesOrdenRepository;
        private readonly IMapper _mapper;

        public OrdenService(IOrdenRepository ordenRepository, IMapper mapper, ITableRepository tableRepository, IDisheRepository disheRepository, IDishesOrdenRepository dishesOrdenRepository) : base(ordenRepository, mapper)
        {
            _OrdenRepository = ordenRepository;
            _TableRepository = tableRepository;
            _mapper = mapper;
            _disheRepository = disheRepository;
            _dishesOrdenRepository = dishesOrdenRepository;
        }

        public override async Task Add (SaveOrdenViewModel sv)
        {
            List<DishesOrden> dishesOrden = sv.DishesOrden.Select(disheId => new DishesOrden{ DishesId = disheId}).ToList();
            var tableId = await _TableRepository.GetById(sv.TableId);

            Orden orden = new Orden();
            orden.Id = sv.Id;
            orden.SubTotal = sv.SubTotal;
            orden.State = StateOrden.EnProceso.ToString();
            orden.DishesOrden = dishesOrden;
            orden.Table = tableId;
            await _OrdenRepository.AddAsync(orden);
        }

        public async Task UpdateOrden(EditViewModel sv, int id)
        {
            var orden = await _OrdenRepository.GetById(id);
            var allDisheOrden = await _dishesOrdenRepository.GetAllDishesOrdenById(id);
            foreach (var item in allDisheOrden)
            {
                var dishesOrden = _dishesOrdenRepository.DeleteAllDisheById(item);
            }
            if (orden != null)
            {
                orden.SubTotal = sv.SubTotal;
                orden.State = sv.State;
                orden.DishesOrden = sv.DishesOrden.Select(dishesId => new DishesOrden { DishesId = dishesId }).ToList();
            }

            await _OrdenRepository.UpdateAsync(orden, id);
        }
        public override async Task<List<OrdenViewModel>> GetAll()
        {
            var orden = await _OrdenRepository.GetAll();
            return orden.Select(x => new OrdenViewModel
            {
                Id = x.Id,
                SubTotal = x.SubTotal,
                State = x.State,
                DishesNames = x.DishesOrden.Select(x => x.Dishe.Name).ToList(),
            }).ToList();

        }

        public override async Task Delete(int id)
        {
            var tableOrdenId = await _TableRepository.GetByOrdenId(id);

            var orden = await _OrdenRepository.GetById(id);
            if(tableOrdenId != null)
            {
                orden.Id = (int)tableOrdenId.OrdenId;

            }
            await _OrdenRepository.DeleteAsync(orden);
        }

        public override async Task<OrdenViewModel> GetById(int id)
        {
            var orden = await _OrdenRepository.GetById(id);

            if (orden != null)
            {
                OrdenViewModel ordenViewModel = new();
                ordenViewModel.Id = orden.Id;
                ordenViewModel.SubTotal = orden.SubTotal;
                ordenViewModel.State = orden.State;
                ordenViewModel.DishesNames = orden.DishesOrden.Select(x => x.Dishe.Name).ToList();
                return ordenViewModel;
            }
            else
            {
                return null;
            }

        }
    }
}
