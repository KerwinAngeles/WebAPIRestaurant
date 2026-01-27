using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Common;

namespace WebAPIRestaurant.Core.Domain.Entities
{
    public class Ingredient : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public ICollection<DisheIngredient> DishesIngredients { get; set; }
    }
}
