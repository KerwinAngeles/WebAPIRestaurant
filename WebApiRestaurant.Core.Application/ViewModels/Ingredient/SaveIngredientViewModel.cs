using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIRestaurant.Core.Application.ViewModels.Ingredient
{
    public class SaveIngredientViewModel
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "The name of ingredient is required")]
        public string? Name { get; set; }
    }
}
