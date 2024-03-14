using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Common;

namespace WebAPIRestaurant.Core.Domain.Entities
{
    public class Dishe : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public int CantPerson {  get; set; }
        public string? DisheCategory { get; set; }
        public int? OrdenId { get; set; }
        public Orden? Orden { get; set; }
        public ICollection<DisheIngredient> DishesIngredients { get; set; }

    }
}
