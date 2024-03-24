using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.ViewModels.DisheOrden;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class DishesOrdenService : GenericService<SaveDishesOrdenViewModel, DishesOrdenViewModel, DishesOrden>
    {
        private readonly IDishesOrdenRepository _dishesOrdenRepository;
        private readonly IMapper _mapper;

        public DishesOrdenService(IDishesOrdenRepository dishesOrdenRepository, IMapper mapper) : base(dishesOrdenRepository, mapper)
        {
            _dishesOrdenRepository = dishesOrdenRepository;
            _mapper = mapper;
        }
    }
}
