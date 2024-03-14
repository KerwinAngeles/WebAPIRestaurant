using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIRestaurant.Core.Application.ViewModels.Ingredient
{
    public class SaveIngredientViewModel
    {
        public virtual int Id { get; set; }
        public string? Name { get; set; }
    }
}
