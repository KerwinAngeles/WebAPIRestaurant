using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Enums;
using WebAPIRestaurant.Core.Application.ViewModels.Orden;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.ViewModels.Dishe
{
    public class SaveDisheViewModel
    {
        [Required(ErrorMessage = "The name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Enter an amount valid at least 1")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Enter an amount at least 1 cant of person")]
        public int CantPerson { get; set; }
        [Required(ErrorMessage = "The category is required")]
        public string? DisheCategory { get; set; }
        public int? OrdenId { get; set; }
        public List<int> DishesIngredients { get; set; } = new List<int>();
    }
}
