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
        private readonly IMapper _mapper;

        public OrdenService(IOrdenRepository ordenRepository, IMapper mapper, ITableRepository tableRepository, IDisheRepository disheRepository) : base(ordenRepository, mapper)
        {
            _OrdenRepository = ordenRepository;
            _TableRepository = tableRepository;
            _mapper = mapper;
            _disheRepository = disheRepository;
        }

        public override async Task Add (SaveOrdenViewModel sv)
        {
            List<Dishe> dishe = await _disheRepository.GetAll();
            var tableId = await _TableRepository.GetById(sv.TableId);

            Orden orden = new Orden();

            orden.Id = sv.Id;
            orden.SubTotal = sv.SubTotal;
            orden.State = StateOrden.EnProceso.ToString();
            orden.Table = tableId;
            orden.Dishes = new List<Dishe>();

            foreach (var item in sv.Dishes)
            {
                var dish = dishe.FirstOrDefault(x => x.Id == item);
                if(dish != null)
                {
                    orden.Dishes.Add(dish);
                }
            }
            //await _OrdenRepository.AddAsync(orden);
            await base.Add(sv);

        }

        public async Task UpdateOrden(EditViewModel sv, int id)
        {
            var orden = await _OrdenRepository.GetById(id);
            List<Dishe> dishe = await _disheRepository.GetAll();

            if (orden != null)
            {
                orden.SubTotal = sv.SubTotal;
                orden.State = sv.State;
                orden.Dishes = new List<Dishe>();

                foreach (var item in sv.Dishes)
                {
                    var dish = dishe.FirstOrDefault(x => x.Id == item);
                    if (dish != null)
                    {
                        orden.Dishes.Add(dish);
                    }
                }
            }

            await _OrdenRepository.UpdateAsync(orden, id);
        }
        public override async Task<List<OrdenViewModel>> GetAll()
        {
            var orden = await _OrdenRepository.GetAll();
            return orden.Select(x => new OrdenViewModel
            {
                SubTotal = x.SubTotal,
                State = x.State,
                DishesNames = x.Dishes.Select(y => y.Name).ToList()
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
            //await _OrdenRepository.DeleteAsync(orden);
            await base.Delete(id);
        }

        public override async Task<OrdenViewModel> GetById(int id)
        {
            var orden = await _OrdenRepository.GetById(id);

            if (orden != null)
            {
                OrdenViewModel ordenViewModel = new();
                ordenViewModel.SubTotal = orden.SubTotal;
                ordenViewModel.State = orden.State;
                ordenViewModel.DishesNames = orden.Dishes.Select(x => x.Name).ToList();
                return ordenViewModel;
            }
            else
            {
                return null;
            }

        }
    }
}
