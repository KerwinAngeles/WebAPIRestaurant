using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Orden;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class OrdenService : GenericService<SaveOrdenViewModel, OrdenViewModel, Orden>, IOrdenService
    {
        private readonly IOrdenRepository _OrdenRepository;
        private readonly IMapper _mapper;

        public OrdenService(IOrdenRepository ordenRepository, IMapper mapper) : base(ordenRepository, mapper)
        {
            _OrdenRepository = ordenRepository;
            _mapper = mapper;
        }
    }
}
