using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.DisheIngredient;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class DisheIngredientService : GenericService<SaveDisheIngredientViewModel, DisheIngredientViewModel, DisheIngredient>, IDisheIngredientService
    {
        private readonly IDisheIngredientRepositorycs _disheIngredientRepository;
        private readonly IMapper _mapper;

        public DisheIngredientService(IDisheIngredientRepositorycs disheIngredientRepository, IMapper mapper) : base(disheIngredientRepository, mapper)
        {
            _disheIngredientRepository = disheIngredientRepository;
            _mapper = mapper;
        }
    }
}
