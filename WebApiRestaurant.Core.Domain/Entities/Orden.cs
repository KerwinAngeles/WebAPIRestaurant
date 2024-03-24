using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Common;

namespace WebAPIRestaurant.Core.Domain.Entities
{
    public class Orden : AuditableBaseEntity
    {
        public int SubTotal { get; set; }
        public string? State { get; set; }
        public Table Table { get; set; } = null!;
        public ICollection<DishesOrden> DishesOrden { get; set; }

    }
}
