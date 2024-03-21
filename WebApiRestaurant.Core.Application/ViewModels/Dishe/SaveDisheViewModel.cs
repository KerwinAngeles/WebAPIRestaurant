using System;
using System.Collections.Generic;
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
        public string? Name { get; set; }
        public int Price { get; set; }
        public int CantPerson { get; set; }
        public string? DisheCategory { get; set; }
        public int? OrdenId { get; set; }
        public List<int> DishesIngredients { get; set; } = new List<int>();
    }
}
