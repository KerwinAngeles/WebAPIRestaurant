using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIRestaurant.Core.Domain.Entities
{
    public class DisheIngredient
    {
        public int IngredientId { get; set; }
        public int DisheId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        public Dishe Dishe { get; set; } = null!;
    }
}
