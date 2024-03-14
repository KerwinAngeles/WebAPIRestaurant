using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.ViewModels.Orden;

namespace WebAPIRestaurant.Core.Application.ViewModels.Table
{
    public class TableViewModel
    {
        public virtual int Id { get; set; }
        public int CantPerson { get; set; }
        public string? Description { get; set; }
        public string? State { get; set; }
        public int OrdenId { get; set; }
        public OrdenViewModel Orden { get; set; }
    }
}
