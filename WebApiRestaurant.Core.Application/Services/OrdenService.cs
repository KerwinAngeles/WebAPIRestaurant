using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Enums;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
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
            Orden orden = new Orden();
            orden.Id = sv.Id;
            orden.SubTotal = sv.SubTotal;
            orden.State = StateOrden.EnProceso.ToString();
            orden.Dishes = dishe;
            var tableId = await _TableRepository.GetById(sv.TableId);

            if(tableId != null)
            {
                orden.Table = tableId;
            }
            
            await _OrdenRepository.AddAsync(orden);
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

        public override async Task<OrdenViewModel> GetById(int id)
        {
            var orden = await _OrdenRepository.GetById(id);
            try
            {
                if(orden != null)
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
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
