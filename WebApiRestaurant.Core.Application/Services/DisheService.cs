using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Interfaces.Repositories;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Dishe;
using WebAPIRestaurant.Core.Application.ViewModels.DisheIngredient;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Services
{
    public class DisheService : GenericService<SaveDisheViewModel,  DisheViewModel, Dishe>, IDisheService
    {
        private readonly IDisheRepository _DisheRepository;
        private readonly IDisheIngredientRepositorycs _disheIngredientRepository;
        private readonly IMapper _mapper;

        public DisheService(IDisheRepository repository, IMapper mapper, IDisheIngredientRepositorycs disheIngredient) : base(repository, mapper)
        {
            _DisheRepository = repository;
            _mapper = mapper;
            _disheIngredientRepository = disheIngredient;

        }

        public override async Task Add(SaveDisheViewModel sv)
        {
            List<DisheIngredient> disheIngredients = sv.DishesIngredients.Select(ingredientId => new DisheIngredient { IngredientId = ingredientId }).ToList();
            Dishe dishe = new();
            dishe.Name = sv.Name;
            dishe.Price = sv.Price;
            dishe.CantPerson = sv.CantPerson;
            dishe.DisheCategory = sv.DisheCategory;
            dishe.DishesIngredients = disheIngredients;
            await _DisheRepository.AddAsync(dishe);
        }

        public override async Task Update(SaveDisheViewModel sv, int id)
        {
            var dishe = await _DisheRepository.GetById(id);

            var disheIngedientAll = await _disheIngredientRepository.GetAllDisheById(id);

            foreach (var item in disheIngedientAll)
            {
                var disheIngredient = _disheIngredientRepository.DeleteAllDisheById(item);
            }

            if (dishe != null)
            {
                dishe.Name = sv.Name;
                dishe.Price = sv.Price;
                dishe.CantPerson = sv.CantPerson;
                dishe.DisheCategory= sv.DisheCategory;
                dishe.DishesIngredients = sv.DishesIngredients.Select(ingredient => new DisheIngredient { IngredientId = ingredient }).ToList();
            }

            await _DisheRepository.UpdateAsync(dishe, id);
        }

        public override async Task<List<DisheViewModel>> GetAll()
        {
            var dishes = await _DisheRepository.GetAll();

            return dishes.Select(dishe => new DisheViewModel
            {
                Id = dishe.Id,
                Name = dishe.Name,
                Price = dishe.Price,
                CantPerson = dishe.CantPerson,
                DisheCategory = dishe.DisheCategory,
                DishesIngredients = dishe.DishesIngredients.Select(i => i.Ingredient.Name).ToList(),

            }).ToList();
        
        }

        public override async Task<DisheViewModel> GetById(int id)
        {
            var dishe = await _DisheRepository.GetById(id);

            DisheViewModel sv = new DisheViewModel();

            if (dishe.DishesIngredients != null)
            {
                sv.Id = id;
                sv.Name = dishe.Name;
                sv.Price = dishe.Price;
                sv.CantPerson = dishe.CantPerson;
                sv.DishesIngredients = dishe.DishesIngredients.Select(i => i.Ingredient.Name).ToList();
                sv.DisheCategory = dishe.DisheCategory;
            }
         
            return sv;
        }
    }
}
