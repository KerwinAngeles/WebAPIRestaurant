using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.ViewModels.Ingredient
{
    public class IngredientViewModel
    {
        public virtual int Id { get; set; } 
        public string? Name { get; set; }
    }
}
