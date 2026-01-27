using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIRestaurant.Core.Application.ViewModels.Orden
{
    public class EditViewModel
    {
        public virtual int Id { get; set; }
        public string? State { get; set; }
        public int SubTotal { get; set; }
        public int TableId { get; set; }
        public List<int> DishesOrden { get; set; } = new List<int>();
    }
}
