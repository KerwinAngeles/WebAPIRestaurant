using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.ViewModels.DisheIngredient;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Interfaces.Services
{
    public interface IDisheIngredientService : IGenericService<SaveDisheIngredientViewModel, DisheIngredientViewModel, DisheIngredient>
    {
    }
}
