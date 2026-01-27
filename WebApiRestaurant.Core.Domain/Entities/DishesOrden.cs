using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIRestaurant.Core.Domain.Entities
{
    public class DishesOrden
    {
        public int DishesId { get; set; }
        public int OrdensId { get; set; }
        public Dishe Dishe { get; set; }
        public Orden Orden { get; set; }
    }
}
