using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Dishe;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class DisheService : GenericService<SaveDisheViewModel,  DisheViewModel, Dishe>, IDisheService
    {
        private readonly IDisheRepository _DisheRepository;
        private readonly IMapper _mapper;

        public DisheService(IDisheRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _DisheRepository = repository;
            _mapper = mapper;
        }

        public override async Task Add(SaveDisheViewModel sv)
        {
            List<DisheIngredient> disheIngredients = sv.DishesIngredients.Select(ingredientId => new DisheIngredient { IngredientId = ingredientId }).ToList();
            Dishe dishe = new();
            dishe.Id = sv.Id;
            dishe.Name = sv.Name;
            dishe.Price = sv.Price;
            dishe.CantPerson = sv.CantPerson;
            dishe.DisheCategory = sv.DisheCategory;
            dishe.DishesIngredients = disheIngredients;
            await _DisheRepository.AddAsync(dishe);
        }

        public async Task<List<DisheViewModel>> GetAllWithInclude()
        {
            var dishes = await _DisheRepository.GetAll();

            return dishes.Select(dishe => new DisheViewModel
            {
                Id = dishe.Id,
                Name = dishe.Name,
                Price = dishe.Price,
                CantPerson = dishe.CantPerson,
                DisheCategory = dishe.DisheCategory,
                Ingredients = dishe.DishesIngredients.Select(i => i.Ingredient.Name).ToList(),

            }).ToList();
           
        }
    }
}
