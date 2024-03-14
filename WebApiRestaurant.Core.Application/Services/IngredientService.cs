using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Ingredient;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class IngredientService : GenericService<SaveIngredientViewModel, IngredientViewModel, Ingredient>, IIngredientService
    {
        private readonly IIngredientRepository _IngredientRepository;
        private readonly IMapper _mapper;
        public IngredientService(IIngredientRepository repository, IMapper mapper) : base (repository, mapper)
        {
            _IngredientRepository = repository;
            _mapper = mapper;
        }
    }
}
