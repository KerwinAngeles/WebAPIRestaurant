using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.ViewModels.Dishe;
using WebAPIRestaurant.Core.Application.ViewModels.Table;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.ViewModels.Orden
{
    public class OrdenViewModel
    {
        public virtual int Id { get; set; }
        public int SubTotal { get; set; }
        public string? State { get; set; }
        public TableViewModel Table { get; set; } = null!;
        public ICollection<DisheViewModel> Dishes { get; set; }
    }
}
